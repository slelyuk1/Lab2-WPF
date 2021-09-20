using System.Windows;

namespace Shared.View.Visualizer
{
    public interface IViewVisualizer
    {
        void Visualize(FrameworkElement toVisualize);
    }
}