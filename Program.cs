using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using System.Threading.Tasks;
using TaskThis.Controller;
using TaskThis.View;
using dotenv.net;
using System.ComponentModel.Design;

namespace TaskThis;

class Program
{
    private static GeminiController geminiController = new GeminiController();
    private static Menu menu = new Menu();

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
            menu.Processing();
            if (await geminiController.ProcessGoal(goal) && geminiController.toDo is not null)
            {
                menu.TaskDone();
                do 
                {
                    switch (menu.Options())
                    {
                        case 1:
                            menu.ListTasks(geminiController.toDo);
                            break;
                        
                        case 2:
                            break;

                        default:
                            // Significa que a tecla escape foi clicada, a aplicação deve ser fechada
                            Environment.Exit(0);
                            break;
                    }
                }
                while (true);
            }
            else
                menu.ErrorMessage("Couldn't process goal");
        });

        return await root.InvokeAsync(args);
    }
}