namespace Shared.View.Provider
{
    public interface IViewProvider<in T>
    {
        View? GetView(T searchCriteria);
    }
}