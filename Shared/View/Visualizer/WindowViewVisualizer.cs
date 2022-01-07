using System.Windows;

namespace Shared.View.Visualizer
{
    public class WindowViewVisualizer : IViewVisualizer
    {
        private readonly Window _window;

        public WindowViewVisualizer(Window window)
        {
            _window = window;
        }

        public void Visualize(FrameworkElement toVisualize)
        {
            _window.Title = toVisualize.Name;
            _window.MinHeight = toVisualize.MinHeight;
            _window.MinWidth = toVisualize.MinWidth;
            _window.Content = toVisualize;
        }
    }
}