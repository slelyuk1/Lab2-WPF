using System;

namespace Shared.View.Navigator
{
    public interface IViewNavigator<in T>
    {
        void ExecuteAndNavigate<TU>(T viewIdentifier, Action<TU> beforeNavigation);

        void Navigate(T viewIdentifier);
    }
}