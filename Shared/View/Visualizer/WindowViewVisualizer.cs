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
            double? minHeight = MeasureIsSpecified(toVisualize.MinHeight) ? toVisualize.MinHeight : null;
            double? minWidth = MeasureIsSpecified(toVisualize.MinWidth) ? toVisualize.MinWidth : null;

            _window.MinHeight = minHeight ?? _window.MinHeight;
            _window.MinWidth = minWidth ?? _window.MinWidth;

            double? height = MeasureIsSpecified(toVisualize.Height) ? toVisualize.Height : minHeight;
            double? width = MeasureIsSpecified(toVisualize.Width) ? toVisualize.Width : minWidth;
            _window.Height = height ?? _window.Height;
            _window.Width = width ?? _window.Width;

            _window.Content = toVisualize;
        }

        private static bool MeasureIsSpecified(double measure)
        {
            return !double.IsNaN(measure) && !double.IsInfinity(measure);
        }
    }
}