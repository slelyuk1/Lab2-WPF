using System.Windows;

namespace Shared.View.Container
{
    public interface IViewMutableContainer<in T> : IViewContainer<T>
    {
        void RegisterViews(params FrameworkElement[] toRegister);
    }
}