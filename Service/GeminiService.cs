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

        private static string TrimJsonResponse(string str)
        {
            return str.TrimStart('`').TrimEnd('`');
        }

        public async Task<List<TaskItem>?> Answer()
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
                            new Part { Text = $@"Gere uma lista de afazeres, na seguinte sintaxe sem Markdown e foque apenas em entregar um JSON onde cada elemento é um dicionário onde a chave é o titulo e o valor é a descrição
                            Exemplo: 
                                [
                                  {{
                                    ""Titulo"": ""Criar a interface do usuário"",
                                    ""Descricao"": ""Desenvolver o layout do aplicativo, incluindo a tela inicial, a tela do Talking Tom e as opções de interação.""
                                  }}
                                ]

                            Essa lista deve ser focada em atingir o seguinte objetivo: {Goal}" }
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
                return JsonSerializer.Deserialize<List<TaskItem>>(TrimJsonResponse(res.Candidates[0].content.Parts[0].Text));
            else return null;
        }
    }
}