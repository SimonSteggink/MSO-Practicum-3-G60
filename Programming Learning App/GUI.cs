namespace ProgrammingLearningApp;

using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Threading;

public class GUI : CommonUI
{
    private readonly MainViewModel viewModel;

    public GUI() 
    {
        viewModel = new MainViewModel(WorldGrid);
    }

    public void UpdateViewModel() 
    {
        viewModel.WorldGrid = WorldGrid;
    }

    [STAThread]
    public override void Run()
    {
        Thread wpfThread = new Thread(() =>
        {
            var app = new Application();
            var window = new Window1();
            window.DataContext = viewModel;

            window.MessageSentToGui += (sender, command) =>
            {
                if (command == "LOAD_BASIC")
                {
                    BasicButtonClick();
                }
                else if (command == "LOAD_ADVANCED")
                {
                    AdvancedButtonClick();
                }
                else if (command == "LOAD_EXPERT")
                {
                    ExpertButtonClick();
                }
                else if (command == "LOAD_FILE_PROGRAM")
                {
                    ProgramFileButtonClick(window);
                }
                else if (command == "LOAD_FILE_LEVEL")
                {
                    LevelFileButtonClick(window);
                }
                else if (command == "RUN")
                {
                    RunButtonClick();
                }
                else if (command == "METRICS")
                {
                    MetricButtonClick();
                }
            };

            app.Run(window);
        });

        wpfThread.SetApartmentState(ApartmentState.STA);
        wpfThread.Start();
        wpfThread.Join();
    }
    private void BasicButtonClick()
    {
        LoadProgram(Examples.Basic());
    }
    private void AdvancedButtonClick()
    {
        LoadProgram(Examples.Advanced());
    }
    private void ExpertButtonClick()
    {
        LoadProgram(Examples.Expert());
    }
    private void ProgramFileButtonClick(Window1 window)
    {
        string fileName = OpenDialog(window);

        try
        {
            LoadProgram(File.ReadAllText(fileName));
        }
        catch (Exception e)
        {
            viewModel.OutputText = "Could not load program: " + e.Message;
        }
    }

    private void LevelFileButtonClick(Window1 window)
    {
        string fileName = OpenDialog(window);

        try
        {
            LoadLevel(File.ReadAllText(fileName));
            FilePath = fileName;
        }
        catch (Exception e)
        {
            viewModel.OutputText = "Could not load Level: " + e.Message;
        }
    }

    private async void RunButtonClick()
    {
        try
        {
            await RunProgram();
        }
        catch (Exception e)
        {
            // The program has already stopped; tell the user why
            MessageBox.Show("The program was stopped: " + e.Message, "Program stopped", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private async Task RunProgram()
    {
        Current = Parser.Parse(viewModel.ProgramText);
        Current.OnProgramStateChanged += (latestGrid) =>
        {
            if (!Application.Current.Dispatcher.CheckAccess())
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    WorldGrid = latestGrid;
                    UpdateViewModel();
                });
                return;
            }
            WorldGrid = latestGrid;
            UpdateViewModel();
        };

        try
        {
            viewModel.OutputText = await Task.Run(() => Execute(500));
        }
        catch (IndexOutOfRangeException)
        {
            MessageBox.Show("The character left the grid.");
        }
        catch (InvalidOperationException e)
        {
            MessageBox.Show(e.Message);
        }
    }
    private void MetricButtonClick()
    {
        Current = Parser.Parse(viewModel.ProgramText);

        viewModel.OutputText = ShowMetrics();
    }

    private static string OpenDialog(Window1 window)
    {
        OpenFileDialog dialog = new OpenFileDialog();
        dialog.Filter = "Program files (*.txt)|*.txt|All files (*.*)|*.*";

        if (dialog.ShowDialog(window) != true)
        {
            return "";
        }

        return dialog.FileName;
    }

    // Shows the program text in the editor and parses it into the current program
    private void LoadProgram(string text)
    {
        viewModel.ProgramText = text;
        Current = Parser.Parse(text);
    }
    private void LoadLevel(string text)
    {
        WorldGrid = GridParser.Parse(text);
        UpdateViewModel();
    }
}
