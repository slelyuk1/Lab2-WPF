using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared.Tool.View;
using Shared.Tool.View.Container;
using Shared.Tool.View.Navigator;
using Shared.Tool.View.Visualizer;
using TaskManager.Model.UI;
using TaskManager.View;
using TaskManager.ViewModel;

namespace TaskManager
{
    public partial class App
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            IHost host = Host.CreateDefaultBuilder()
                .ConfigureLogging(loggingBuilder =>
                {
                    loggingBuilder
                        .ClearProviders()
                        .AddConsole(options => options.LogToStandardErrorThreshold = LogLevel.Information);
                })
                .ConfigureServices(serviceCollection =>
                {
                    serviceCollection.AddSingleton<ContentWindow>();
                    serviceCollection.AddSingleton<IViewVisualizer>(provider =>
                        new WindowViewVisualizer(provider.GetRequiredService<ContentWindow>())
                    );
                    serviceCollection.AddSingleton<IMutableViewContainer, DefaultViewContainer>();
                    serviceCollection.AddSingleton<IViewContainer>(provider => provider.GetRequiredService<IMutableViewContainer>());
                    serviceCollection.AddSingleton<IViewNavigator, ViewContainerBasedNavigator>();

                    serviceCollection.AddSingleton<ProcessesInfoModel>();
                    serviceCollection.AddSingleton<ProcessesInfoViewModel>();
                    serviceCollection.AddSingleton<ProcessesInfoView>();
                })
                .Build();

            IServiceProvider serviceProvider = host.Services;
            var container = serviceProvider.GetRequiredService<IMutableViewContainer>();
            var pickDateView = serviceProvider.GetRequiredService<ProcessesInfoView>();
            container.AddViewModelAware(new DefaultViewModelAware<ProcessesInfoView, ProcessesInfoViewModel>(pickDateView));

            var navigator = serviceProvider.GetRequiredService<IViewNavigator>();
            var window = serviceProvider.GetRequiredService<ContentWindow>();
            navigator.Navigate<ProcessesInfoView>();
            window.Show();
        }
    }
}