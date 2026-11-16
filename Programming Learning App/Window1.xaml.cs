using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProgrammingLearningApp
{
    public partial class Window1 : Window
    {
        public event EventHandler<string>? MessageSentToGui;

        public Window1()
        {
            InitializeComponent();
        }

        private void BasicButtonClick(object sender, RoutedEventArgs e)
        {
            MessageSentToGui?.Invoke(this, "LOAD_BASIC");
        }

        private void AdvancedButtonClick(object sender, RoutedEventArgs e)
        {
            MessageSentToGui?.Invoke(this, "LOAD_ADVANCED");
        }

        private void ExpertButtonClick(object sender, RoutedEventArgs e)
        {
            MessageSentToGui?.Invoke(this, "LOAD_EXPERT");
        }

        private void ProgramFileButtonClick(object sender, RoutedEventArgs e)
        {
            MessageSentToGui?.Invoke(this, "LOAD_FILE_PROGRAM");
        }

        private void LevelFileButtonClick(object sender, RoutedEventArgs e)
        {
            MessageSentToGui?.Invoke(this, "LOAD_FILE_LEVEL");
        }

        private void RunButtonClick(object sender, RoutedEventArgs e)
        {
            MessageSentToGui?.Invoke(this, "RUN");
        }

        private void MetricButtonClick(object sender, RoutedEventArgs e)
        {
            MessageSentToGui?.Invoke(this, "METRICS");
        }
    }
}
