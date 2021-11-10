using System.Windows;
using Shared.View.Visualizer;

namespace TaskManager
{
    public partial class MainWindow : IViewVisualizer
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Visualize(FrameworkElement toVisualize)
        {
            Title = toVisualize.Name;
            MinHeight = toVisualize.MinHeight;
            MinWidth = toVisualize.MinWidth;
            ContentControl.Content = toVisualize;
        }
    }
}