using Shared.Tool.View;

namespace Shared.View.Container
{
    public interface IMutableViewContainer : IViewContainer
    {
        void AddViewModelAware<TView, TViewModel>(IViewModelAware<TView, TViewModel> elementsToAdd);
    }
}