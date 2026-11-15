namespace Programming_Learning_App;

using System.ComponentModel;
using System.Runtime.CompilerServices;

// Holds the state the window shows; the text boxes are bound to these properties
public class MainViewModel : INotifyPropertyChanged
{
    private string programText = "";
    private string outputText = "<output>";

    public event PropertyChangedEventHandler? PropertyChanged;

    public string ProgramText
    {
        get => programText;
        set
        {
            programText = value;
            OnPropertyChanged();
        }
    }

    public string OutputText
    {
        get => outputText;
        set
        {
            outputText = value;
            OnPropertyChanged();
        }
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
