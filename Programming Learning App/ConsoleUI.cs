namespace ProgrammingLearningApp;

using System.IO;
using System.Windows;

public class ConsoleUI : CommonUI
{
    public async override void Run()
    {
        while (Current == null)
        {
            Console.WriteLine("Choose a program:");
            Console.WriteLine("1. Basic example");
            Console.WriteLine("2. Advanced example");
            Console.WriteLine("3. Expert example");
            Console.WriteLine("4. Random example");
            Console.WriteLine("Or type the location of a program file.");

            string? input = Console.ReadLine();
            if (input == null)
            {
                return;
            }

            try
            {
                switch (input)
                {
                    case "1":
                        Current = Parser.Parse(Examples.Basic());
                        break;
                    case "2":
                        Current = Parser.Parse(Examples.Advanced());
                        break;
                    case "3":
                        Current = Parser.Parse(Examples.Expert());
                        break;
                    case "4":
                        Current = Parser.Parse(Examples.Random());
                        break;
                    default:
                        Current = Parser.Parse(File.ReadAllText(input.Trim().Trim('"')));
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not load program: " + e.Message);
            }
        }

        while (true)
        {
            Console.WriteLine("1. Execute program");
            Console.WriteLine("2. Calculate metrics");

            string? input = Console.ReadLine();

            if (input == null)
            {
                return;
            }
            else if (input == "1")
            {
                try
                {
                    Console.WriteLine(await Execute(0));
                }
                catch (Exception e)
                {
                    Console.WriteLine("The program was stopped: " + e.Message);
                }
                break;
            }
            else if (input == "2")
            {
                Console.WriteLine(ShowMetrics());
                break;
            }
        }
    }
}
