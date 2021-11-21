using System.Windows;

namespace Shared.View.Container
{
    public interface IViewMutableContainer<in TT> : IViewContainer<TT>
    {
        void RegisterViews(params FrameworkElement[] toRegister);
    }
}