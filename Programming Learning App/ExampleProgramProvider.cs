namespace Programming_Learning_App;

using System.IO;

// Returns the text of example programs, so the UI can both show and parse it
public class ExampleProgramProvider
{
    private Random rand;

    public ExampleProgramProvider()
    {
        rand = new Random();
    }

    public string Basic()
    {
        return LoadRandomFrom("UserPrograms/Basic");
    }
    public string Advanced()
    {
        return LoadRandomFrom("UserPrograms/Advanced");
    }
    public string Expert()
    {
        return LoadRandomFrom("UserPrograms/Expert");
    }
    public string Random()
    {
        switch (rand.Next(3))
        {
            case 0:
                return Basic();
            case 1:
                return Advanced();
            default:
                return Expert();
        }
    }

    // Each level has its own folder with several programs and we get a random one from any folder
    private string LoadRandomFrom(string folder)
    {
        string[] files = Directory.GetFiles(folder, "*.txt");
        return File.ReadAllText(files[rand.Next(files.Length)]);
    }
}
