using TaskThis.Model;

namespace TaskThis.View;

class Menu
{
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

    public void viewTasks(List<TaskItem> list)
    {
        
    }
}