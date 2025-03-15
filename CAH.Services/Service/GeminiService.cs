using CAH.ModelViews.AIChatBotModelView;
using Firebase.Database;
using Firebase.Database.Query;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Http;
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

        public GeminiService(IConfiguration configuration, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _apiKey = configuration["AI:GeminiApiKey"];
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
                    throw new Exception("API Key is missing. Make sure it is set in environment variables.");
                }

                // Chuẩn bị lịch sử trò chuyện để gửi lên API
                var conversationParts = chatHistory
                    .Select(msg => new
                    {
                        parts = new[] { new { text = $"{msg.Sender}: {msg.Message}" } }
                    })
                    .ToList();

                // Thêm prompt mới vào cuối danh sách
                conversationParts.Add(new
                {
                    parts = new[] { new { text = $"User: {prompt}" } }
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
