using System;
using Shared.View.Provider;
using Shared.View.Visualizer;

namespace Shared.View.Navigator
{
    public class ViewProviderBasedNavigator<T> : IViewNavigator<T>
    {
        private readonly IViewProvider<T> _viewProvider;

        public ViewProviderBasedNavigator(IViewProvider<T> viewProvider)
        {
            _viewProvider = viewProvider;
        }

        public void Navigate(IViewVisualizer visualizer, T viewIdentifier)
        {
            View? view = _viewProvider.GetView(viewIdentifier);
            if (view == null)
            {
                throw new InvalidOperationException("ViewProvider couldn't find view using search criteria: " + viewIdentifier);
            }

            visualizer.Visualize(view);
        }
    }
}