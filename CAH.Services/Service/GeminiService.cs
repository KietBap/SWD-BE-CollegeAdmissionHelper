using CAH.ModelViews.AIChatBotModelView;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace CAH.Services.Service
{
    public class GeminiService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;
        public GeminiService(IConfiguration configuration, HttpClient httpClient)
        {
            _apiKey = configuration["AI:GeminiApiKey"];
            _httpClient = httpClient;

            Console.WriteLine($"Gemini API Key: {_apiKey}");
        }
        public async Task<GeminiResponseModelView> GenerateTextAsync(string prompt)
        {
            try
            {
                // Lấy API Key từ biến môi trường trên Azure
                var apiKey = Environment.GetEnvironmentVariable("GEMINI_API_KEY") ?? _apiKey;
                if (string.IsNullOrEmpty(apiKey))
                {
                    throw new Exception("API Key is missing. Make sure it is set in environment variables.");
                }


                var requestBody = new
                {
                    contents = new[]
                    {
                new
                {
                    parts = new[]
                    {
                        new { text = $"{prompt}" }
                    }
                }
            }
                };

                var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
                var requestUrl = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={apiKey}";


                var response = await _httpClient.PostAsync(requestUrl, jsonContent);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {responseContent}");
                    throw new Exception($"Error calling Gemini API: {response.StatusCode} - {responseContent}");
                }

                using JsonDocument doc = JsonDocument.Parse(responseContent);
                var root = doc.RootElement;

                // Kiểm tra nếu response có dữ liệu hợp lệ
                if (!root.TryGetProperty("candidates", out var candidates) || candidates.GetArrayLength() == 0)
                {
                    throw new Exception($"Invalid response format from Gemini API: {responseContent}");
                }

                var generatedText = candidates[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();

                return new GeminiResponseModelView()
                {
                    Content = generatedText,
                    Time = DateTime.Now
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GenerateTextAsync: {ex.Message}");
                return new GeminiResponseModelView()
                {
                    Content = $"Đã xảy ra lỗi khi gọi API Gemini. {ex.Message}",
                    Time = DateTime.Now
                };
            }
        }

    }
}
