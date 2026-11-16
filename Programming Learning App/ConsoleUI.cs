namespace Programming_Learning_App;

using System.IO;
using System.Windows;

public class ConsoleUI : CommonUI
{
    public async override void Run()
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
                        current = parser.Parse(examples.Basic());
                        break;
                    case "2":
                        current = parser.Parse(examples.Advanced());
                        break;
                    case "3":
                        current = parser.Parse(examples.Expert());
                        break;
                    case "4":
                        current = parser.Parse(examples.Random());
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
                Console.WriteLine(await Execute(0));
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
