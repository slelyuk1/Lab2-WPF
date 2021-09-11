using Shared.Window;

namespace AgeZodiacCalculator
{
    public partial class ContentWindow : IViewVisualizer
    {
        public ContentWindow()
        {
            InitializeComponent();
        }

        public void Visualize(Shared.Navigation.View toVisualize)
        {
            MinHeight = toVisualize.MinHeight;
            MinWidth = toVisualize.MinWidth;
            ContentControl.Content = toVisualize.Content;
        }
    }
}