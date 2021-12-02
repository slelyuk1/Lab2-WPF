using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PeopleInfoStorage.Model.Data;
using PeopleInfoStorage.Model.UI;
using PeopleInfoStorage.View;
using PeopleInfoStorage.ViewModel;
using Shared.Tool.Serialization;
using Shared.Tool.View;
using Shared.View.Container;
using Shared.View.Navigator;
using Shared.View.Visualizer;

namespace PeopleInfoStorage
{
    public partial class App
    {
        public const string PeopleResourceName = "People";
        public const string SerializationFile = @".\Saved.bin";

        private void OnStartup(object sender, StartupEventArgs e)
        {
            // todo logging
            // todo make safe type conversions
            // todo use LINQ

            IHost host = Host.CreateDefaultBuilder(e.Args)
                .ConfigureLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.AddConsole();
                })
                .ConfigureServices(serviceCollection =>
                {
                    serviceCollection.AddSingleton<AbstractSerializationFacade>(
                        new ResourceSerializationFacade(new BinaryFormatter(), SerializationFile)
                    );
                    serviceCollection.AddSingleton<MainWindow, MainWindow>();
                    serviceCollection.AddSingleton<IViewVisualizer, MainWindow>(serviceProvider => serviceProvider.GetRequiredService<MainWindow>());

                    serviceCollection.AddSingleton<IMutableViewContainer, DefaultViewContainer>();
                    serviceCollection.AddSingleton<IViewContainer>(serviceProvider => serviceProvider.GetRequiredService<IMutableViewContainer>());

                    serviceCollection.AddSingleton<IViewNavigator, ViewContainerBasedNavigator>();
                })
                .Build();

            IServiceProvider serviceProvider = host.Services;
            var container = serviceProvider.GetRequiredService<IMutableViewContainer>();
            var navigator = serviceProvider.GetRequiredService<IViewNavigator>();
            var serializationFacade = serviceProvider.GetRequiredService<AbstractSerializationFacade>();

            var personInput = new DefaultViewModelAware<PersonInputView, PersonInputViewModel>(
                new PersonInputView(new PersonInputViewModel(navigator))
            );

            IList<PersonInfo> people = serializationFacade.Deserialize<List<PersonInfo>>(PeopleResourceName) ?? new List<PersonInfo>();
            var peopleView = new DefaultViewModelAware<PeopleView, PeopleViewModel>(
                new PeopleView(new PeopleViewModel(navigator, new PeopleModel(people)))
            );

            container.AddViewModelAware(personInput);
            container.AddViewModelAware(peopleView);

            navigator.Navigate<PeopleView>();
            serviceProvider.GetRequiredService<MainWindow>().Show();
        }
    }
}