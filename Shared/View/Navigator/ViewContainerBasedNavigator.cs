using System;
using System.Windows;
using Shared.View.Container;
using Shared.View.Visualizer;

namespace Shared.View.Navigator
{
    public class ViewContainerBasedNavigator<T> : IViewNavigator<T>
    {
        // ReSharper disable once StaticMemberInGenericType
        private static readonly Action<object> EmptyAction = _ => { };

        private readonly IViewVisualizer _visualizer;
        private readonly IViewContainer<T> _viewContainer;

        public ViewContainerBasedNavigator(IViewVisualizer visualizer, IViewContainer<T> viewContainer)
        {
            _visualizer = visualizer;
            _viewContainer = viewContainer;
        }

        public void ExecuteAndNavigate<TU>(T viewIdentifier, Action<TU> beforeNavigation)
        {
            var view = _viewContainer.GetView<FrameworkElement>(viewIdentifier);
            if (view == null)
            {
                throw new InvalidOperationException("ViewProvider couldn't find view using search criteria: " + viewIdentifier);
            }

            TU actionParameter;
            if (view is TU concreteView)
            {
                actionParameter = concreteView;
            }
            else if (view.DataContext is TU viewModel)
            {
                actionParameter = viewModel;
            }
            else
            {
                throw new InvalidOperationException("Couldn't extract action parameter with viewID=" + viewIdentifier + " type=" + typeof(TU));
            }

            beforeNavigation.Invoke(actionParameter);
            _visualizer.Visualize(view);
        }

        public void Navigate(T viewIdentifier)
        {
            ExecuteAndNavigate<FrameworkElement>(viewIdentifier, EmptyAction);
        }
    }
}