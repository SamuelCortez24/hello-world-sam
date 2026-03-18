using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // List to store grades
        List<double> grades = new List<double>();
        string? input;

        Console.WriteLine("=== Student Grade Calculator ===");

        // Loop to collect grades
        while (true)
        {
            Console.Write("Enter a grade (or type 'done'): ");
            input = Console.ReadLine();

            // Check if user wants to stop
            if (input != null && input.ToLower() == "done")
            {
                break;
            }

            // Validate input
            if (double.TryParse(input, out double grade))
            {
                if (grade >= 0 && grade <= 100)
                {
                    grades.Add(grade);
                }
                else
                {
                    Console.WriteLine("Please enter a grade between 0 and 100.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }

        // Check if no grades were entered
        if (grades.Count == 0)
        {
            Console.WriteLine("No grades entered.");
            return;
        }

        // Calculate results
        double average = CalculateAverage(grades);
        string letter = GetLetterGrade(average);

        // Display results
        Console.WriteLine($"Average: {average:F2}");
        Console.WriteLine($"Final Grade: {letter}");

        // Save results to file (REQUIREMENT)
        SaveResultsToFile(average, letter);
    }

    // Function to calculate average
    static double CalculateAverage(List<double> grades)
    {
        double sum = 0;

        foreach (double g in grades)
        {
            sum += g;
        }

        return sum / grades.Count;
    }

    // Function to determine letter grade
    static string GetLetterGrade(double avg)
    {
        if (avg >= 90) return "A";
        else if (avg >= 80) return "B";
        else if (avg >= 70) return "C";
        else if (avg >= 60) return "D";
        else return "F";
    }

    // Function to save results to a file
    static void SaveResultsToFile(double average, string letter)
    {
        string filePath = "grades.txt";

        string content = $"Average: {average:F2}\nFinal Grade: {letter}";

        File.WriteAllText(filePath, content);

        Console.WriteLine("Results saved to grades.txt");
    }
}