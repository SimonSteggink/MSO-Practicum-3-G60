namespace Programming_Learning_App;

using System.IO;
using System.Windows;

public class GUI : CommonUI
{
    [STAThread]
    public override void Run()
    {
        Thread wpfThread = new Thread(() =>
        {
            var app = new Application();
            var window = new Window1();

            window.MessageSentToGui += (sender, command) =>
            {
                if (command == "LOAD_BASIC")
                {
                    BasicButtonClick();
                }
                if (command == "LOAD_ADVANCED")
                {
                    AdvancedButtonClick();
                }
                if (command == "LOAD_EXPERT")
                {
                    ExpertButtonClick();
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
        current = examples.Basic();
    }
    private void AdvancedButtonClick()
    {
        current = examples.Advanced();
    }
    private void ExpertButtonClick()
    {
        current = examples.Expert();
    }
    private void RunButtonClick()
    {
        Execute();
    }
    private void MetricButtonClick()
    {
        ShowMetrics();
    }
}
