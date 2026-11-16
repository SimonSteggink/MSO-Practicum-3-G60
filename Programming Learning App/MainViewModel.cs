namespace ProgrammingLearningApp;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

// Holds the state the window shows; the text boxes are bound to these properties
public class MainViewModel : INotifyPropertyChanged
{
    private string programText = "";
    private string outputText = "<output>";
    private Grid worldGrid;
    public ObservableCollection<CellType> Cells { get; } = new();

    public MainViewModel(Grid grid) 
    {
        worldGrid = grid;

        BuildCells();
    }

    public void BuildCells() 
    {
        Cells.Clear();

        for (int i = 0; i < worldGrid.Rows; i++)
        {
            for (int j = 0; j < worldGrid.Columns; j++)
            {
                Cells.Add(worldGrid.Cells[i, j]);
            }
        }
    }

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

    public Grid WorldGrid 
    {
        get => worldGrid;
        set 
        {
            worldGrid = value;
            BuildCells();
            OnPropertyChanged(nameof(WorldGrid));
        }
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
