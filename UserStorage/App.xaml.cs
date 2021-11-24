using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using Shared.View.Container;
using Shared.View.Navigator;
using UserStorage.Content;
using UserStorage.Managers;
using UserStorage.Models;
using UserStorage.View;
using UserStorage.ViewModel;

namespace UserStorage
{
    public partial class App
    {
        public const string PeopleResourceName = "People";
        private const string SerializationFile = @".\Saved.bin";

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var serializationFacade = new ResourceSerializationFacade(new BinaryFormatter(), SerializationFile);
            IList<PersonInfo> people = serializationFacade.Deserialize<List<PersonInfo>>(PeopleResourceName) ?? new List<PersonInfo>();

            IViewMutableContainer<Type> viewContainer = new ContentTypeBasedViewContainer();
            var window = new MainWindow(viewContainer, serializationFacade);
            IViewNavigator<Type> navigator = new ViewContainerBasedNavigator<Type>(window, viewContainer);

            TypeConverter chineseSignConverter = PersonInfo.ChineseSignConverter;
            TypeConverter westernSignConverter = PersonInfo.ChineseSignConverter;

            var userInputView = new UserInputView(new UserInputViewModel(navigator));
            var usersView = new UsersView(
                new UsersViewModel(navigator, new UsersModel(people), chineseSignConverter, westernSignConverter)
            );

            viewContainer.RegisterViews(userInputView, usersView);
            navigator.Navigate(typeof(UsersView));
            window.Show();
        }
    }
}