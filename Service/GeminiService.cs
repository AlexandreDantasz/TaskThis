using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using TaskThis.Model;

namespace TaskThis.Service
{
    class GeminiService
    {

        private static readonly HttpClient Client = new HttpClient();
        private string Goal { get; set; } = string.Empty;

        public GeminiService(string goal)
        {
            Goal = goal;
        }

        private static string TrimJsonResponse(string str)
        {
            return str.TrimStart('`').TrimEnd('`');
        }

        public async Task<List<TaskItem>?> Answer()
        {
            string? model = Environment.GetEnvironmentVariable("GEMINI_MODEL");
            string? key = Environment.GetEnvironmentVariable("GEMINI_KEY");

            if (string.IsNullOrWhiteSpace(model) || string.IsNullOrEmpty(key))
                throw new Exception("Environment variables GEMINI_MODEL or GEMINI_KEY are not set");

            string url = $"https://generativelanguage.googleapis.com/v1beta/models/{model}/:generateContent?key={key}";

            GeminiRequest requestBody = new GeminiRequest
            {
                Contents = new[]
                {
                    new Content
                    {
                        Parts = new[]
                        {
                            new Part { Text = $@"Generate a to-do list in the following syntax without Markdown and focus solely on delivering a JSON where each element is a dictionary, with the key being the title and the value being the description.
                            Example: 
                                [
                                  {{
                                    ""Title"": ""Create the user interface"",
                                    ""Description"": ""Develop the app layout, including the home screen, the Talking Tom screen, and interaction options.""
                                  }}
                                ]

                            This list should focus on achieving the following goal: {Goal}" }
                        }
                    }
                }
            };

            var jsonContent = JsonSerializer.Serialize(requestBody);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            
            HttpResponseMessage response = await Client.PostAsync(url, httpContent);

            string responseContent = await response.Content.ReadAsStringAsync();
            GeminiResponse? res = JsonSerializer.Deserialize<GeminiResponse>(responseContent);

            if (res != null && res?.Candidates[0].content.Parts[0].Text is not null)
                return JsonSerializer.Deserialize<List<TaskItem>>(TrimJsonResponse(res.Candidates[0].content.Parts[0].Text));
            else return null;
        }
    }
}