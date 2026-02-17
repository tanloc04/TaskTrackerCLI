namespace TaskTrackerCLI
{
    internal class Program
    {
        static readonly TaskService taskService = new TaskService();
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: task-cli <command> [arguments]");
                Console.WriteLine("Commands: add, update, delete, mark-in-progress, mark-done, list");
                return;
            }

            string command = args[0].ToLower();

            try
            {
                switch (command)
                {
                    case "add":
                        if (args.Length < 2)
                            Console.WriteLine("Error: Description is required.");
                        else
                            taskService.AddTask(args[1]);
                        break;

                    case "update":
                        if (args.Length < 3)
                            Console.WriteLine("Error: ID and New Description are required.");
                        else
                            taskService.UpdateTask(int.Parse(args[1]), args[2]);
                        break;

                    case "delete":
                        if (args.Length < 2)
                            Console.WriteLine("Error: ID is required.");
                        else
                            taskService.DeleteTask(int.Parse(args[1]));
                        break;

                    case "mark-in-progress":
                        if (args.Length < 2)
                            Console.WriteLine("Error: ID is required.");
                        else
                            taskService.MarkTaskStatus(int.Parse(args[1]), "in-progress");
                        break;

                    case "mark-done":
                        if (args.Length < 2)
                            Console.WriteLine("Error: ID is required.");
                        else
                            taskService.MarkTaskStatus(int.Parse(args[1]), "done");
                        break;

                    case "list":
                        if (args.Length > 1)
                            taskService.ListTasks(args[1]);
                        else
                            taskService.ListTasks();
                        break;

                    default:
                        Console.WriteLine($"Unknown command: {command}");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Error must be a number!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            Console.ReadKey();
        }
    }
}
