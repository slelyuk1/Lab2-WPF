using System;
using System.Windows;
using Shared.Tool.View.Container;
using Shared.Tool.View.Visualizer;

namespace Shared.Tool.View.Navigator
{
    public class ViewContainerBasedNavigator : IViewNavigator
    {
        private readonly IViewVisualizer _visualizer;

        public ViewContainerBasedNavigator(IViewVisualizer visualizer, IViewContainer container)
        {
            _visualizer = visualizer;
            Container = container;
        }

        public IViewContainer Container { get; }

        public void ExecuteAndNavigate<TView, TViewModel>(Action<TView, TViewModel> beforeNavigation) where TView : FrameworkElement
        {
            IViewModelAware<TView, TViewModel> viewModelAware = Container.GetRequiredViewModelAware<TView, TViewModel>();
            TView view = viewModelAware.GetView();
            beforeNavigation.Invoke(view, viewModelAware.GetViewModel());
            _visualizer.Visualize(view);
        }

        public void Navigate<TView>() where TView : FrameworkElement
        {
            ExecuteAndNavigate<TView, object>((_, _) => { });
        }
    }
}