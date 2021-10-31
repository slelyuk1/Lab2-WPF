using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using Shared.View.Container;
using Shared.View.Navigator;
using UserStorage.Content;
using UserStorage.Managers;
using UserStorage.Models;
using UserStorage.ViewModel;

namespace UserStorage
{
    public partial class App
    {
        // todo move this variable
        public const string StorageResourceName = "Storage";
        public const string SerializationFile = @".\Saved.bin";

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var serializationFacade = new ResourceSerializationFacade(new BinaryFormatter(), SerializationFile);
            Storage storage = serializationFacade.Deserialize<Storage>(StorageResourceName) ?? new Storage();
            var window = new MainWindow(storage, serializationFacade);

            IViewMutableContainer<Type> viewContainer = new ContentTypeBasedViewContainer();
            IViewNavigator<Type> navigator = new ViewProviderBasedNavigator<Type>(window, viewContainer);

            var userInfoView = new UserInfoContent(new UserInfoViewModel(navigator, storage));
            var userInputView = new UserInputContent(new UserInputViewModel(navigator, storage));
            var usersView = new UsersContent(new UsersViewModel(navigator, storage));

            viewContainer.RegisterViews(userInfoView, userInputView, usersView);
            navigator.Navigate(typeof(UsersContent));
            window.Show();
        }
    }
}