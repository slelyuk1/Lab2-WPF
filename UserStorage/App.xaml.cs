using System;
using System.Windows;
using Shared.View.Container;
using Shared.View.Navigator;
using UserStorage.Content;
using UserStorage.Models;
using UserStorage.ViewModel;

namespace UserStorage
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // todo fix serialization/deserialization process
            var data = new Storage();
            var window = new MainWindow(data);

            IViewMutableContainer<Type> viewContainer = new ContentTypeBasedViewContainer();
            IViewNavigator<Type> navigator = new ViewProviderBasedNavigator<Type>(window, viewContainer);

            var userInfoView = new UserInfoContent(new UserInfoViewModel(navigator, data));
            var userInputView = new UserInputContent(new UserInputViewModel(navigator, data));
            var usersView = new UsersContent(new UsersViewModel(navigator, data));

            viewContainer.RegisterViews(userInfoView, userInputView, usersView);
            navigator.Navigate(typeof(UsersContent));
            window.Show();
        }
    }
}