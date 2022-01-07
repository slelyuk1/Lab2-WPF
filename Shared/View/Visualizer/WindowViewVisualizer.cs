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

            double? minHeight = double.IsNaN(toVisualize.MinHeight) || double.IsInfinity(toVisualize.MinHeight) ? null : toVisualize.MinHeight;
            double? minWidth = double.IsNaN(toVisualize.MinWidth) || double.IsInfinity(toVisualize.MinWidth) ? null : toVisualize.MinWidth;
            _window.MinHeight = minHeight ?? 0;
            _window.MinWidth = minWidth ?? 0;

            double? height = double.IsNaN(toVisualize.Height) || double.IsInfinity(toVisualize.Height) ? null : toVisualize.Height;
            double? width = double.IsNaN(toVisualize.Width) || double.IsInfinity(toVisualize.Width) ? null : toVisualize.Width;
            height ??= minHeight;
            width ??= minHeight;
            if (height != null)
            {
                _window.Height = (double) height;
            }

            if (width != null)
            {
                _window.Width = (double) width;
            }

            _window.Content = toVisualize;
        }
    }
}