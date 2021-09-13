namespace Shared.View.Container
{
    public interface IViewMutableContainer<T> : IViewContainer<T>
    {
        void RegisterViews(params View[] toRegister);
    }
}