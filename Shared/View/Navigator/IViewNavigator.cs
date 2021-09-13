using Shared.View.Visualizer;

namespace Shared.View.Navigator
{
    public interface IViewNavigator<in T>
    {
        public IViewVisualizer Visualizer { get; }
        public void Navigate(T viewIdentifier);
    }
}