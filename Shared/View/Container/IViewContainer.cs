namespace Shared.View.Container
{
    public interface IViewContainer<in T>
    {
        View? GetView(T searchCriteria);
    }
}