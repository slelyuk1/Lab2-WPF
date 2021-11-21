using System.Windows;

namespace Shared.View.Container
{
    public interface IViewContainer<in TT>
    {
        TU? GetView<TU>(TT searchCriteria) where TU : FrameworkElement;
    }
}