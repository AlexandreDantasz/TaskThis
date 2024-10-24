using TaskThis.Service;
using System.Threading.Tasks;

namespace TaskThis.Controller
{
    class GeminiController
    {
        public async Task<string?> ProcessGoal(string? Goal)
        {
            if (!string.IsNullOrEmpty(Goal) && Goal.Length < 150)
            {
                GeminiService gemini = new GeminiService(Goal);
                return await gemini.Answer();
            }

            return null;
        }
    }
}