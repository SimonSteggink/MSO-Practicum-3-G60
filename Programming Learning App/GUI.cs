namespace Programming_Learning_App;

using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Threading;

public class GUI : CommonUI
{
    private MainViewModel viewModel;

    public GUI() 
    {
        viewModel = new MainViewModel(worldGrid);
    }

    public void UpdateViewModel() 
    {
        viewModel.WorldGrid = worldGrid;
    }

    public GUI()
    {
        grid = viewModel.worldGrid;
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
                else if (command == "LOAD_FILE")
                {
                    FileButtonClick(window);
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
        LoadProgram(examples.Basic());
    }
    private void AdvancedButtonClick()
    {
        LoadProgram(examples.Advanced());
    }
    private void ExpertButtonClick()
    {
        LoadProgram(examples.Expert());
    }
    private void FileButtonClick(Window1 window)
    {
        OpenFileDialog dialog = new OpenFileDialog();
        dialog.Filter = "Program files (*.txt)|*.txt|All files (*.*)|*.*";

        if (dialog.ShowDialog(window) != true)
        {
            return;
        }

        try
        {
            LoadProgram(File.ReadAllText(dialog.FileName));
        }
        catch (Exception e)
        {
            viewModel.OutputText = "Could not load program: " + e.Message;
        }
    }
    private async void RunButtonClick()
    {
        current = parser.Parse(viewModel.ProgramText);
        current.OnProgramStateChanged += async (latestGrid) =>
        {
            if (!Application.Current.Dispatcher.CheckAccess())
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    worldGrid = latestGrid;
                    UpdateViewModel();
                });
                return;
            }
            worldGrid = latestGrid;
            UpdateViewModel();
        };

        viewModel.OutputText = await Task.Run(() => Execute(500));
    }
    private void MetricButtonClick()
    {
        current = parser.Parse(viewModel.ProgramText);

        viewModel.OutputText = ShowMetrics();
    }

    // Shows the program text in the editor and parses it into the current program
    private void LoadProgram(string text)
    {
        viewModel.ProgramText = text;
        current = parser.Parse(text);
    }
}
