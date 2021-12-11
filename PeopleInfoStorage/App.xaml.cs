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
                    serviceCollection.AddSingleton(new SerializationFacade(new BinaryFormatter()));
                    serviceCollection.AddSingleton<MainWindow, MainWindow>();
                    serviceCollection.AddSingleton<IViewVisualizer, MainWindow>(serviceProvider => serviceProvider.GetRequiredService<MainWindow>());

                    serviceCollection.AddSingleton<IMutableViewContainer, DefaultViewContainer>();
                    serviceCollection.AddSingleton<IViewContainer>(serviceProvider => serviceProvider.GetRequiredService<IMutableViewContainer>());
                    serviceCollection.AddSingleton<IViewNavigator, ViewContainerBasedNavigator>();

                    serviceCollection.AddSingleton<PersonInputViewModel>();
                    serviceCollection.AddSingleton<PersonInputView>();

                    serviceCollection.AddSingleton(serviceProvider =>
                    {
                        var appLogger = serviceProvider.GetRequiredService<ILogger<App>>();
                        var serializationFacade = serviceProvider.GetRequiredService<SerializationFacade>();
                        IList<PersonInfo> people = GetSavedPeopleInfo(serializationFacade, new FileSerializer(SerializationFile), appLogger);
                        return new PeopleModel(people, serviceProvider.GetRequiredService<ILogger<PeopleModel>>());
                    });
                    serviceCollection.AddSingleton<PeopleViewModel>();
                    serviceCollection.AddSingleton<PeopleView>();
                })
                .Build();

            IServiceProvider serviceProvider = host.Services;
            var container = serviceProvider.GetRequiredService<IMutableViewContainer>();
            var navigator = serviceProvider.GetRequiredService<IViewNavigator>();

            var personInputView = serviceProvider.GetRequiredService<PersonInputView>();
            container.AddViewModelAware(new DefaultViewModelAware<PersonInputView, PersonInputViewModel>(personInputView));
            var peopleView = serviceProvider.GetRequiredService<PeopleView>();
            container.AddViewModelAware(new DefaultViewModelAware<PeopleView, PeopleViewModel>(peopleView));

            navigator.Navigate<PeopleView>();
            serviceProvider.GetRequiredService<MainWindow>().Show();
        }

        private static IList<PersonInfo> GetSavedPeopleInfo(SerializationFacade serializationFacade, ISerializer serializer, ILogger<App> logger)
        {
            IDictionary<string, object>? deserializedEntries = serializationFacade.Deserialize(serializer);
            if (deserializedEntries == null)
            {
                logger.LogWarning("Couldn't deserialize using serializer: {Serializer}", serializer);
                return new List<PersonInfo>();
            }

            if (!deserializedEntries.TryGetValue(PeopleResourceName, out object peopleObject))
            {
                logger.LogWarning("Couldn't find resource by resource name: {ResourceName}", PeopleResourceName);
                return new List<PersonInfo>();
            }

            if (peopleObject is not IList<PersonInfo> people)
            {
                logger.LogWarning("Found resource is not of expected type: {Resource}", peopleObject);
                return new List<PersonInfo>();
            }

            return people;
        }
    }
}