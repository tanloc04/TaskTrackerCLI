using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskTrackerCLI
{
    public class TaskService
    {
        private const string FilePath = "tasks.json";

        private List<TaskItem> LoadTasks()
        {
            if (!File.Exists(FilePath))
            {
                return new List<TaskItem>();
            }

            string json = File.ReadAllText(FilePath);

            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<TaskItem>();
            }

            return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
        }
    }
}
