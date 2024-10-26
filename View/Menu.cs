using System.Net.Sockets;
using System.Text;
using TaskThis.Model;

namespace TaskThis.View;

class Menu
{
    private volatile bool playPomodoro = true, isExitPressed = false;

    public void Processing()
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("TaskThis > Processing...");
        Console.ResetColor();
    }

    public void ErrorMessage(string message)
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Error.WriteLine($"TaskThis > {message}");
        Console.ResetColor();
    }

    public void TaskDone()
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("TaskThis > Done!");
        Console.ResetColor();
    }

    private static void ClearLetter()
    {
        Console.Write("\b \b");
    }

    private static void Header(string title)
    {
        for (int i = 0; i < (Console.WindowWidth - title.Length) / 2 ; i++)
            Console.Write("-");

        Console.Write(title);

        for (int i = 0; i < (Console.WindowWidth / 2) - 18; i++)
            Console.Write("-");

        Console.Write('\n');
    }

    private static void TabToCenter(int titleSize)
    {
        for (int i = 0; i < (Console.WindowWidth - titleSize - 1) / 2 ; i++)
            Console.Write(' ');
    }

    private static string ReadInput()
    {
        StringBuilder resBuilder = new StringBuilder();
        ConsoleKeyInfo input;
        while (true)
        {
            input = Console.ReadKey(intercept: true);

            if (input.Key == ConsoleKey.Escape)
            {
                Console.Write("Exit");
                return "TaskThis > ESC";
            }
                
            
            if (input.Key == ConsoleKey.Enter)
                return resBuilder.ToString();

            if (input.Key == ConsoleKey.Backspace && resBuilder.Length > 0)
            {
                resBuilder.Remove(resBuilder.Length - 1, 1);
                ClearLetter();
            }
            else if (input.Key != ConsoleKey.Backspace)
            {
                resBuilder.Append(input.KeyChar);
                Console.Write(input.KeyChar);
            }
            
        }
    }

    private static void Shutdown()
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine("TaskThis > Shutdown...");
        Console.ResetColor();
    }

    private static bool OptionsInput(ref short inputRef)
    {
        short input;
        Console.Write("Answer > ");
        string strInput = ReadInput();

        Console.Write("\n");

        if (strInput.Equals("TaskThis > ESC"))
        {
            inputRef = -1;
            Shutdown();
            return true;
        }

        if (short.TryParse(strInput, out input))
        {
            inputRef = input;
            return input >= 1 && input <= 2;
        }

        return false;
    }

    public short Options()
    {
        short option = 0;
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Yellow;
        string title = " TaskThis Options ";
        Header(title);
        Console.WriteLine("Press ESC to exit");
        TabToCenter(title.Length);
        Console.WriteLine("1 - List your tasks");
        TabToCenter(title.Length);
        Console.WriteLine("2 - Start Pomodoro\n");
        while (!OptionsInput(ref option))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error > Invalid option");
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        Console.ResetColor();

        return option;
    }

    public void ListTasks(List<TaskItem> list)
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Cyan;

        Console.Clear();

        Header(" Your Tasks ");

        Console.WriteLine();

        int i = 0;

        foreach (var a in list)
        {
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"[{i++}] {a.Title}: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(a.Description);
        }

        Console.WriteLine();

    }

    public void StartPomodoro(Pomodoro pomodoro)
    {
        bool firstTime = false;
        playPomodoro = true;
        isExitPressed = false;
        
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine();
        string title = " Pomodoro Timer ";
        Header(title);

        int workMinutes = pomodoro.Work;
        int workSeconds = 0;

        Console.WriteLine("Press ESC to exit");
        Console.WriteLine("Press P to pause or play");
        Console.WriteLine();

        Thread keyValues = new Thread(() => {
            do
            {
                ConsoleKeyInfo input = Console.ReadKey(intercept: true);
                if (input.Key == ConsoleKey.Escape)
                {
                    isExitPressed = true;
                    Console.Clear();
                }
                    
                else if (input.Key == ConsoleKey.P)
                    playPomodoro = !playPomodoro;

                Thread.Sleep(200);
            } while (!isExitPressed);
        });

        keyValues.Start();

        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Work timer: ");

        while (!isExitPressed && (workMinutes != 0 || workSeconds != 0))
        {
            if (playPomodoro)
            {
                Thread.Sleep(1000);
                workSeconds--;

                if (workSeconds == -1)
                {
                    if (workMinutes > 0)
                    {
                        workSeconds = 59;
                        workMinutes--;
                    }
                }

                if (firstTime)
                {
                    for (int i = 0; i < 5; i++)
                        ClearLetter();
                }

                if (!isExitPressed)
                {
                    Console.Write((workMinutes >= 10) ? workMinutes.ToString() : $"0{workMinutes}");
                    Console.Write(":");
                    Console.Write((workSeconds >= 10) ? workSeconds.ToString() : $"0{workSeconds}");
                }
                

                firstTime = true;
            }
            
            
        }

        Console.WriteLine();
        
        // If exit wasn't press we need to start resting timer
        if (!isExitPressed)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            firstTime = false;

            int restMinutes = pomodoro.Rest;
            int restSeconds = 0;

            Console.Write("Rest timer: ");

            while (!isExitPressed && (restMinutes != 0 || restSeconds != 0))
            {
                if (playPomodoro)
                {
                    Thread.Sleep(1000);
                    restSeconds--;

                    if (restSeconds == -1)
                    {
                        if (restMinutes > 0)
                        {
                            restSeconds = 59;
                            restMinutes--;
                        }
                    }

                    if (firstTime)
                    {
                        for (int i = 0; i < 5; i++)
                            ClearLetter();
                    }

                    if (!isExitPressed)
                    {
                        Console.Write((restMinutes >= 10) ? restMinutes.ToString() : $"0{restMinutes}");
                        Console.Write(":");
                        Console.Write((restSeconds >= 10) ? restSeconds.ToString() : $"0{restSeconds}");
                    }

                    firstTime = true;
                }            
            }
        }
    }

}