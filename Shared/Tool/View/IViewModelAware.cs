namespace Shared.Tool.View
{
    public interface IViewModelAware<out TView, out TViewModel>
    {
        TView GetView();
        TViewModel GetViewModel();
    }
}