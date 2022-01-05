﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using PeopleInfoStorage.Model.Data;
using PeopleInfoStorage.View;
using PeopleInfoStorage.ViewModel;
using Shared.Tool.Serialization;
using Shared.Tool.Serialization.Serializer;
using Shared.View.Container;
using Shared.View.Visualizer;

namespace PeopleInfoStorage
{
    public partial class MainWindow : IViewVisualizer
    {
        private readonly IViewContainer _viewContainer;
        private readonly SerializationFacade _serializationFacade;

        public MainWindow(IViewContainer viewContainer, SerializationFacade serializationFacade)
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
            PeopleViewModel viewModel = _viewContainer.GetRequiredViewModelAware<PeopleView, PeopleViewModel>().GetViewModel();
            List<PersonInfo> people = new(viewModel.People);
            _serializationFacade.Serialize(new FileSerializer(App.SerializationFile), App.PeopleResourceName, people);
            base.OnClosing(e);
        }
    }
}