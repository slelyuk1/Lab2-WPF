using Shared.View;
using Shared.View.Visualizer;

namespace AgeZodiacCalculator
{
    public partial class ContentWindow : IViewVisualizer
    {
        public ContentWindow()
        {
            InitializeComponent();
        }

        public void Visualize(View toVisualize)
        {
            Title = toVisualize.Title;
            MinHeight = toVisualize.MinHeight;
            MinWidth = toVisualize.MinWidth;
            ContentControl.Content = toVisualize.Content;
        }
    }
}