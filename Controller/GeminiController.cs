using TaskThis.Service;
using TaskThis.Model;
using System.Threading.Tasks;

namespace TaskThis.Controller
{
    class GeminiController
    {
        public List<TaskItem>? toDo { get; set; } = null;
        public async Task<bool> ProcessGoal(string? Goal)
        {
            if (!string.IsNullOrEmpty(Goal) && Goal.Length < 150)
            {
                GeminiService gemini = new GeminiService(Goal);
                toDo = await gemini.Answer();

                return toDo != null;
            }

            return false;
        }
    }
}