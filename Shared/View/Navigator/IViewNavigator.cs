using System;
using System.Windows;

namespace Shared.View.Navigator
{
    public interface IViewNavigator
    {
        void ExecuteAndNavigate<TView, TViewModel>(Action<TView, TViewModel> beforeNavigation) where TView : FrameworkElement;

        void Navigate<TView>() where TView : FrameworkElement;
    }
}