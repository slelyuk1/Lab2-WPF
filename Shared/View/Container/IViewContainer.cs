using System.Windows;

namespace Shared.View.Container
{
    public interface IViewContainer<in T>
    {
        FrameworkElement? GetView(T searchCriteria);
    }
}