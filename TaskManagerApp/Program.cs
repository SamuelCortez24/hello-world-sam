using System;
using System.Collections.Generic;
using System.IO;

class TaskItem
{
    public string Name { get; set; }
    public bool IsCompleted { get; set; }

    public TaskItem(string name)
    {
        Name = name;
        IsCompleted = false;
    }

    public override string ToString()
    {
        return $"{Name}|{IsCompleted}";
    }

    public static TaskItem FromString(string data)
    {
        try
        {
            string[] parts = data.Split('|');
            if (parts.Length != 2) return null;

            TaskItem task = new TaskItem(parts[0]);
            task.IsCompleted = bool.Parse(parts[1]);
            return task;
        }
        catch
        {
            return null;
        }
    }
}

class Program
{
    static List<TaskItem> tasks = new List<TaskItem>();
    static string filePath = "tasks.txt";

    static void Main(string[] args)
    {
        LoadTasks();

        while (true)
        {
            Console.WriteLine("\n=== Student Task Manager ===");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. View Tasks");
            Console.WriteLine("3. Mark Task as Completed");
            Console.WriteLine("4. Delete Task");
            Console.WriteLine("5. Exit");

            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    ShowTasks();
                    break;
                case "3":
                    CompleteTask();
                    break;
                case "4":
                    DeleteTask();
                    break;
                case "5":
                    SaveTasks();
                    Console.WriteLine("Tasks saved. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    static void AddTask()
    {
        Console.Write("Enter task name: ");
        string name = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(name))
        {
            tasks.Add(new TaskItem(name));
            Console.WriteLine("Task added.");
        }
        else
        {
            Console.WriteLine("Invalid task name.");
        }
    }

    static void ShowTasks()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks available.");
            return;
        }

        for (int i = 0; i < tasks.Count; i++)
        {
            string status = tasks[i].IsCompleted ? "[✔]" : "[ ]";
            Console.WriteLine($"{i + 1}. {status} {tasks[i].Name}");
        }
    }

    static void CompleteTask()
    {
        ShowTasks();
        Console.Write("Enter task number to complete: ");

        if (int.TryParse(Console.ReadLine(), out int index))
        {
            if (index > 0 && index <= tasks.Count)
            {
                tasks[index - 1].IsCompleted = true;
                Console.WriteLine("Task marked as completed.");
            }
            else
            {
                Console.WriteLine("Invalid number.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }
    }

    static void DeleteTask()
    {
        ShowTasks();
        Console.Write("Enter task number to delete: ");

        if (int.TryParse(Console.ReadLine(), out int index))
        {
            if (index > 0 && index <= tasks.Count)
            {
                tasks.RemoveAt(index - 1);
                Console.WriteLine("Task deleted.");
            }
            else
            {
                Console.WriteLine("Invalid number.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }
    }

    static void SaveTasks()
    {
        try
        {
            List<string> lines = new List<string>();

            foreach (var task in tasks)
            {
                lines.Add(task.ToString());
            }

            File.WriteAllLines(filePath, lines);
        }
        catch
        {
            Console.WriteLine("Error saving tasks.");
        }
    }

    static void LoadTasks()
    {
        try
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (var line in lines)
                {
                    TaskItem task = TaskItem.FromString(line);
                    if (task != null)
                    {
                        tasks.Add(task);
                    }
                }
            }
        }
        catch
        {
            Console.WriteLine("Error loading tasks.");
        }
    }
}