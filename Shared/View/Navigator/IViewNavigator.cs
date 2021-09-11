using Shared.View.Visualizer;

namespace Shared.View.Navigator
{
    public interface IViewNavigator<in T>
    {
        public void Navigate(IViewVisualizer visualizer, T viewIdentifier);
    }
}