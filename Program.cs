using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using TaskThis.Controller;
using TaskThis.View;
using dotenv.net;
using System.Text.Json;

namespace TaskThis;



class Program
{
    private static readonly string envPath = @"C:\Users\alexa\dev\TaskThis\.env";
    private static GeminiController geminiController = new GeminiController();
    private static Menu menu = new Menu();

    static async Task<int> Main(string[] args)
    {
        DotEnv.Load(new DotEnvOptions(true, [envPath]));

        RootCommand root = new RootCommand()
        {
            new Option<string>("-goal", "Description of your goal") {IsRequired = true},
            new Option<int>("-work", () => 50, "Your working time in minutes (default: 50min, max: 60min)"),
            new Option<int>("-rest", () => 10, "Your resting time in minutes (default: 10min, max: 30min)")
        };

        root.Description = "A task creator that uses AI and a pomodoro timer!";

        root.Handler = CommandHandler.Create<string, int, int>(async (goal, work, rest) =>
        {
            try
            {
                PomodoroController pomodoroController = new PomodoroController();
                menu.Processing();
                if (!pomodoroController.SetWork(work))
                        menu.ErrorMessage($"Working time {work}min isn't valid. Please choose a value between 1 and 60.");
                else if (!pomodoroController.SetRest(rest))
                    menu.ErrorMessage($"Resting time {rest}min isn't valid. Please choose a value between 1 and 60.");
                else if (await geminiController.ProcessGoal(goal) && geminiController.toDo is not null)
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
                                menu.StartPomodoro(pomodoroController.pomodoro);
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
            }
            catch (HttpRequestException httpError)
            {
                menu.ErrorMessage("The connection with the API couldn't be stabilish. Check your network!");
            }
            catch (JsonException jsonError)
            {
                menu.ErrorMessage("The API doesn't gave the expected response. Try again later!");
            }
            catch (Exception error)
            {
                menu.ErrorMessage(error.Message);
            }
        });

        return await root.InvokeAsync(args);
    }
}