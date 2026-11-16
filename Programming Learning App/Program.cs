using ProgrammingLearningApp;


while (true)
{
    Console.WriteLine("Choose a UI (g for GUI and c for ConsoleUI): ");

    string? input = Console.ReadLine();
    if (input == null) {
        return;
    } else if (input == "g") {
        new GUI().Run();
    } else if (input == "c") {
        new ConsoleUI().Run();
    } else {
        continue;
    }
    break;
}
