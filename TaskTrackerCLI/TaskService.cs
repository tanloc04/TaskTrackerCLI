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

        #region Load
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
        #endregion

        /// <summary>
        /// Đây là hàm được sử dụng để lưu list công việc
        /// </summary>
        /// <param name="tasks"></param>
        #region Save
        private void SaveTasks(List<TaskItem> tasks)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(tasks, options);
            File.WriteAllText(FilePath, json);
        }
        #endregion

        #region Create
        public void AddTask(string description)
        {
            var tasks = LoadTasks();

            int newId = tasks.Any() ? tasks.Max(t => t.Id) + 1 : 1;

            var newTask = new TaskItem
            {
                Id = newId,
                Description = description,
                Status = "todo",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            tasks.Add(newTask);
            SaveTasks(tasks);

            Console.WriteLine($"Task added successfully (ID: {newId})");
        }
        #endregion

        #region Update
        public void UpdateTask(int id, string newDescription)
        {
            var tasks = LoadTasks();

            var task = tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
            {
                Console.WriteLine($"Task with ID {id} not found.");
                return;
            }

            task.Description = newDescription;
            task.UpdatedAt = DateTime.Now;

            SaveTasks(tasks);
            Console.WriteLine($"Task ID {id} updated successfully!");
        }
        #endregion

        #region Delete
        public void DeleteTask (int id)
        {
            var tasks = LoadTasks();

            var task = tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
            {
                Console.WriteLine($"Task with ID {id} not found.");
                return;
            }

            tasks.Remove(task);

            SaveTasks(tasks);

            Console.WriteLine($"Task with ID {id} deleted successfully.");
        }
        #endregion

        #region UpdateStatus
        public void MarkTaskStatus(int id, string status)
        {
            var tasks = LoadTasks();

            var task = tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
            {
                Console.WriteLine($"Task ID {id} not found.");
                return;
            }
            task.Status = status;
            task.UpdatedAt = DateTime.Now;
            SaveTasks(tasks);
            Console.WriteLine($"Task ID {id} marked as {status}.");
        }
        #endregion

        #region FilterList
        public void ListTasks(string statusFilter = null)
        {
            var tasks = LoadTasks();

            var tasksToPrint = string.IsNullOrEmpty(statusFilter) ? tasks : tasks.Where(t => t.Status == statusFilter).ToList();

            if (tasksToPrint.Count == 0)
            {
                Console.WriteLine("No tasks found!");
                return;
            }

            foreach(var task in tasksToPrint)
            {
                Console.WriteLine($"[{task.Id}] {task.Description} - {task.Status}");
            }
        }
        #endregion

    }
}
