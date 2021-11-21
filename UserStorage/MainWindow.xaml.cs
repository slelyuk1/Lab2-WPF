using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using Shared.View.Container;
using Shared.View.Visualizer;
using UserStorage.Content;
using UserStorage.Managers;
using UserStorage.Models;
using UserStorage.ViewModel;

namespace UserStorage
{
    public partial class MainWindow : IViewVisualizer
    {
        private readonly IViewContainer<Type> _viewContainer;
        private readonly AbstractSerializationFacade _serializationFacade;

        public MainWindow(IViewContainer<Type> viewContainer, AbstractSerializationFacade serializationFacade)
        {
            _viewContainer = viewContainer;
            _serializationFacade = serializationFacade;
            InitializeComponent();
        }

        public void Visualize(FrameworkElement toVisualize)
        {
            MinHeight = toVisualize.MinHeight;
            MinWidth = toVisualize.MinWidth;
            WindowContents.Content = toVisualize;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            var usersView = _viewContainer.GetView<UsersView>(typeof(UsersView));
            if (usersView == null)
            {
                throw new InvalidOperationException();
            }

            UsersViewModel viewModel = (UsersViewModel) usersView.DataContext;
            List<PersonInfo> people = new(viewModel.People);
            _serializationFacade.Serialize(App.PeopleResourceName, people);
            base.OnClosing(e);
        }
    }
}