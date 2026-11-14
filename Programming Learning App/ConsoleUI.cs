namespace Programming_Learning_App;

using System.IO;
using System.Windows;

public class ConsoleUI : CommonUI
{
    public override void Run()
    {
        while (current == null)
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
                        current = examples.Basic();
                        break;
                    case "2":
                        current = examples.Advanced();
                        break;
                    case "3":
                        current = examples.Expert();
                        break;
                    case "4":
                        current = examples.Random();
                        break;
                    default:
                        current = parser.Parse(File.ReadAllText(input.Trim().Trim('"')));
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
                Execute();
                break;
            }
            else if (input == "2")
            {
                ShowMetrics();
                break;
            }
        }
    }
}
