using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<double> grades = new List<double>();
        string? input;

        Console.WriteLine("=== Student Grade Calculator ===");

        while (true)
        {
            Console.Write("Enter a grade (or type 'done'): ");
            input = Console.ReadLine();

            // Verificamos null y luego comparamos
            if (input != null && input.ToLower() == "done")
            {
                break;
            }

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

        if (grades.Count == 0)
        {
            Console.WriteLine("No grades entered.");
            return;
        }

        double average = CalculateAverage(grades);
        string letter = GetLetterGrade(average);

        Console.WriteLine($"Average: {average:F2}");
        Console.WriteLine($"Final Grade: {letter}");
    }

    static double CalculateAverage(List<double> grades)
    {
        double sum = 0;

        foreach (double g in grades)
        {
            sum += g;
        }

        return sum / grades.Count;
    }

    static string GetLetterGrade(double avg)
    {
        if (avg >= 90) return "A";
        else if (avg >= 80) return "B";
        else if (avg >= 70) return "C";
        else if (avg >= 60) return "D";
        else return "F";
    }
}