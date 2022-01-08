namespace Shared.Tool.View.Container
{
    public interface IViewContainer
    {
        IViewModelAware<TView, TViewModel> GetRequiredViewModelAware<TView, TViewModel>();
        IViewModelAware<TView, TViewModel>? GetViewModelAware<TView, TViewModel>();
    }
}