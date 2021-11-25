using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using PeopleInfoStorage.Model.Data;
using PeopleInfoStorage.Model.UI;
using PeopleInfoStorage.View;
using PeopleInfoStorage.ViewModel;
using Shared.Tool.Serialization;
using Shared.View.Container;
using Shared.View.Navigator;

namespace PeopleInfoStorage
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

            var personInputView = new PersonInputView(new PersonInputViewModel(navigator));
            var peopleView = new PeopleView(
                new PeopleViewModel(navigator, new PeopleModel(people), chineseSignConverter, westernSignConverter)
            );

            viewContainer.RegisterViews(personInputView, peopleView);
            navigator.Navigate(typeof(PeopleView));
            window.Show();
        }
    }
}