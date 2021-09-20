using System.Windows;
using Shared.View.Visualizer;

namespace AgeZodiacCalculator
{
    public partial class ContentWindow : IViewVisualizer
    {
        public ContentWindow()
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