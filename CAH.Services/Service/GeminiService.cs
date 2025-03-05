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
        }
        public async Task<GeminiResponseModelView> GenerateTextAsync(string prompt)
        {

            var requestBody = new
            {
                contents = new[]
                {
                new
                {
                    parts = new[]
                    {
                        new { text = prompt}
                    }
                }
            }
            };
            var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            var requestUrl = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={_apiKey}";
            var response = await _httpClient.PostAsync(requestUrl, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                using JsonDocument doc = JsonDocument.Parse(responseContent);
                var root = doc.RootElement;

                // Lấy nội dung chính từ JSON
                var generatedText = root
                    .GetProperty("candidates")[0]
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
            return null;
        }
    }
}
