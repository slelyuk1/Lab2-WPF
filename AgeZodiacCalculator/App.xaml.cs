using System;
using System.ComponentModel;
using System.Windows;
using AgeZodiacCalculator.Model.UI;
using AgeZodiacCalculator.Model.UI.Impl;
using AgeZodiacCalculator.View;
using AgeZodiacCalculator.ViewModel;
using Shared.Model.Data;
using Shared.Tool.View;
using Shared.View.Container;
using Shared.View.Navigator;

namespace AgeZodiacCalculator
{
    public partial class AgeZodiacCalculatorApp
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            // todo logging
            // todo make safe type conversions
            // todo use LINQ
            var window = new ContentWindow();

            TypeConverter chineseSignConverter = TypeDescriptor.GetConverter(typeof(ChineseSign));
            TypeConverter westernSignConverter = TypeDescriptor.GetConverter(typeof(WesternSign));

            IPickDateModel pickDateModel = new ConverterBasedPickDateModel(DateTime.Now, chineseSignConverter, westernSignConverter);
            var pickDateContent = new PickDateView(new PickDateViewModel(pickDateModel));
            var container = new DefaultViewContainer();
            container.AddViewModelAware(new DefaultViewModelAware<PickDateView, PickDateViewModel>(pickDateContent));
            IViewNavigator navigator = new ViewContainerBasedNavigator(window, container);
            navigator.Navigate<PickDateView>();

            window.Show();
        }
    }
}