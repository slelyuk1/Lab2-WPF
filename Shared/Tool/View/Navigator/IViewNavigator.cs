using System;
using System.Windows;

namespace Shared.Tool.View.Navigator
{
    public interface IViewNavigator
    {
        void ExecuteAndNavigate<TView, TViewModel>(Action<TView, TViewModel> beforeNavigation) where TView : FrameworkElement;

        void Navigate<TView>() where TView : FrameworkElement;
    }
}