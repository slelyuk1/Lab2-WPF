using System;
using Shared.View.Container;
using Shared.View.Visualizer;

namespace Shared.View.Navigator
{
    public class ViewProviderBasedNavigator<T> : IViewNavigator<T>
    {
        private readonly IViewContainer<T> _viewContainer;

        public IViewVisualizer Visualizer { get; }

        public ViewProviderBasedNavigator(IViewVisualizer visualizer, IViewContainer<T> viewContainer)
        {
            Visualizer = visualizer;
            _viewContainer = viewContainer;
        }

        public void Navigate(T viewIdentifier)
        {
            View? view = _viewContainer.GetView(viewIdentifier);
            if (view == null)
            {
                throw new InvalidOperationException("ViewProvider couldn't find view using search criteria: " + viewIdentifier);
            }

            Visualizer.Visualize(view);
        }
    }
}