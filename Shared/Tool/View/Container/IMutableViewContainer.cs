namespace Shared.Tool.View.Container
{
    public interface IMutableViewContainer : IViewContainer
    {
        void AddViewModelAware<TView, TViewModel>(IViewModelAware<TView, TViewModel> elementsToAdd);
    }
}