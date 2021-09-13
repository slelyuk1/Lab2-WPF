using Shared.View;
using Shared.View.Visualizer;

namespace TaskManager
{
    public partial class MainWindow : IViewVisualizer
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Visualize(View toVisualize)
        {
            MinHeight = toVisualize.MinHeight;
            MinWidth = toVisualize.MinWidth;
            WindowContent.Content = toVisualize.Content;
        }
    }
}