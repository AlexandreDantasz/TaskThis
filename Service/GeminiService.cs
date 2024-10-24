using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using TaskThis.Model;

namespace TaskThis.Service
{
    class GeminiService
    {

        private HttpClient Client = new HttpClient();
        private string Goal { get; set; } = string.Empty;

        public GeminiService(string goal)
        {
            Goal = goal;
        }

        public async Task<string> Answer()
        {
            string url = $"https://generativelanguage.googleapis.com/v1beta/models/{Environment.GetEnvironmentVariable("GEMINI_MODEL")}/:generateContent?key={Environment.GetEnvironmentVariable("GEMINI_KEY")}";

            GeminiRequest requestBody = new GeminiRequest
            {
                Contents = new[]
                {
                    new Content
                    {
                        Parts = new[]
                        {
                            new Part { Text = $"Gere uma lista de afazeres breve para atingir esse objetivo: {Goal}" }
                        }
                    }
                }
            };

            var jsonContent = JsonSerializer.Serialize(requestBody);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            
            HttpResponseMessage response = await Client.PostAsync(url, httpContent);

            string responseContent = await response.Content.ReadAsStringAsync();
            GeminiResponse? res = JsonSerializer.Deserialize<GeminiResponse>(responseContent);

            if (res != null)
                return res.Candidates[0].content.Parts[0].Text;
            else return string.Empty;
        }
    }
}