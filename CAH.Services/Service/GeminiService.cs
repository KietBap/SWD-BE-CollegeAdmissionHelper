using CAH.Contract.Repositories.Entity;
using CAH.Contract.Repositories.Interface;
using CAH.ModelViews.AIChatBotModelView;
using Firebase.Database;
using Firebase.Database.Query;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace CAH.Services.Service
{
    public class GeminiService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;
        private readonly FirebaseClient _firebaseClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public GeminiService(IConfiguration configuration, HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _apiKey = configuration["AI:GeminiApiKey"];
            _unitOfWork = unitOfWork;
            _httpClient = httpClient;

            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.FromFile("collegeadmissionhelper-firebase-adminsdk-fbsvc-7898fc81a5.json")
                });
            }

            _firebaseClient = new FirebaseClient("https://collegeadmissionhelper.asia-southeast1.firebasedatabase.app/");
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<GeminiResponseModelView> GenerateTextAsync(string prompt, List<ChatMessage>? chatHistory)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirst("userId")?.Value;

                var apiKey = Environment.GetEnvironmentVariable("GEMINI_API_KEY") ?? _apiKey;
                if (string.IsNullOrEmpty(apiKey))
                {
                    throw new Exception("API  Key is missing. Make sure it is set in environment variables.");
                }

                // Chuẩn bị lịch sử trò chuyện để gửi lên API
                var conversationParts = chatHistory
                    .Select(msg => new
                    {
                        parts = new[] { new { text = msg.Message } },
                        role = msg.Sender == "user" ? "user" : "model"
                    })
                    .ToList();

                // Nếu người dùng hỏi về điểm số, truy vấn danh sách điểm
                if (prompt.Contains("điểm của tôi") || prompt.Contains("diem cua toi") || prompt.Contains("my score"))
                {
                    var scores = await (from score in _unitOfWork.GetRepository<SubjectScore>().Entities
                                        join subject in _unitOfWork.GetRepository<Subject>().Entities on score.SubjectId equals subject.Id
                                        where score.UserId.ToString() == userId && !score.DeletedTime.HasValue
                                        orderby score.Date descending
                                        select new
                                        {
                                            subject.Name,
                                            score.Score,
                                            score.ExamType,
                                            score.Date
                                        }).ToListAsync();

                    string scoreText = scores.Any()
                        ? string.Join("\n", scores.Select(s => $"{s.Name} ({s.ExamType}): {s.Score} điểm vào ngày {s.Date:dd/MM/yyyy}"))
                        : "Tôi chưa có điểm nào được lưu trong hệ thống.";

                    // Thêm danh sách điểm như một phần hội thoại
                    conversationParts.Add(new
                    {
                        parts = new[] { new { text = $"Danh sách điểm của tôi:\n{scoreText}" } },
                        role = "model"
                    });
                }

                // Thêm prompt mới vào cuối danh sách
                conversationParts.Add(new
                {
                    parts = new[] { new { text = "Dựa vào cuộc hội thoại và danh sách điểm ( tôi đưa cho bạn ở trên nếu có ) của tôi và bạn ở trên để tiếp tục cuộc nói chuyện này và không cần nhắc lại ( nếu không có lịch sử chat thì bỏ qua không cần nhắc ) : " + prompt } },
                    role = "user"
                });

                var requestBody = new { contents = conversationParts };

                var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
                var requestUrl = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={apiKey}";

                var response = await _httpClient.PostAsync(requestUrl, jsonContent);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error calling Gemini API: {response.StatusCode} - {responseContent}");
                }

                using JsonDocument doc = JsonDocument.Parse(responseContent);
                var root = doc.RootElement;

                if (!root.TryGetProperty("candidates", out var candidates) || candidates.GetArrayLength() == 0)
                {
                    throw new Exception("Invalid response format from Gemini API.");
                }

                var generatedText = candidates[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();

                var responseModel = new GeminiResponseModelView
                {
                    Content = generatedText,
                    Time = DateTime.Now
                };

                await SaveChatHistoryAsync(userId, prompt, generatedText);

                return responseModel;
            }
            catch (Exception ex)
            {
                return new GeminiResponseModelView
                {
                    Content = $"Đã xảy ra lỗi: {ex.Message}",
                    Time = DateTime.Now
                };
            }
        }

        private async Task SaveChatHistoryAsync(string userId, string userMessage, string botResponse)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return;
            }

            var conversationId = DateTime.UtcNow.ToString("yyyyMMddHHmmss");

            var userMessageData = new
            {
                Sender = "user",
                Text = userMessage,
                Timestamp = DateTime.UtcNow
            };

            var botMessageData = new
            {
                Sender = "bot",
                Text = botResponse,
                Timestamp = DateTime.UtcNow.AddSeconds(2)
            };

            // Lưu tin nhắn của User
            await _firebaseClient
                .Child("ChatHistory")
                .Child(userId)
                .Child(conversationId)
                .Child("messages")
                .PostAsync(userMessageData);

            // Lưu tin nhắn của Bot
            await _firebaseClient
                .Child("ChatHistory")
                .Child(userId)
                .Child(conversationId)
                .Child("messages")
                .PostAsync(botMessageData);
        }

    }
}
