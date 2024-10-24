using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using System.Threading.Tasks;
using TaskThis.Controller;
using dotenv.net;

class Program
{
    private static GeminiController geminiController = new GeminiController();

    static async Task<int> Main(string[] args)
    {
        DotEnv.Load();

        RootCommand root = new RootCommand()
        {
            new Option<string>("-goal", "Description of your goal (required)"),
            new Option<int>("-work", "Your working time in minutes (default: 50min)"),
            new Option<int>("-rest", "Your resting time in minutes (default: 10min)")
        };

        root.Description = "A task creator that uses AI and a pomodoro timer!";

        root.Handler = CommandHandler.Create<string>(async (goal) =>
        {
            string? res = await geminiController.ProcessGoal(goal);
            if (!string.IsNullOrEmpty(res))
                Console.WriteLine(res);
            else
                Console.WriteLine("Couldn't process goal");
        });

        return await root.InvokeAsync(args);
    }
}