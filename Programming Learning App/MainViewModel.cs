namespace Programming_Learning_App;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

// Holds the state the window shows; the text boxes are bound to these properties
public class MainViewModel : INotifyPropertyChanged
{
    private string programText = "";
    private string outputText = "<output>";
    public Grid worldGrid { get; }
    public ObservableCollection<CellType> Cells { get; } = new();

    public MainViewModel() 
    {
        CellType[,] grid = new CellType[2,2];

        grid[0, 0] = CellType.Blocked;
        grid[0, 1] = CellType.Open;
        grid[1, 0] = CellType.Blocked;
        grid[1, 1] = CellType.Blocked;

        worldGrid = new Grid(2, 2, grid);

        BuildCells();
    }

    private void BuildCells() 
    {
        ObservableCollection<CellType> Cellse = new();

        for (int i = 0; i < worldGrid.rows; i++)
        {
            for (int j = 0; j < worldGrid.columns; j++)
            {
                Cells.Add(worldGrid.grid[i, j]);
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

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
