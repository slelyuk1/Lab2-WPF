using System;
using System.ComponentModel;
using System.Windows;
using AgeZodiacCalculator.Info;
using AgeZodiacCalculator.Model;
using AgeZodiacCalculator.Model.Impl;
using AgeZodiacCalculator.View;
using AgeZodiacCalculator.ViewModel;
using Shared.View.Container;
using Shared.View.Navigator;

namespace AgeZodiacCalculator
{
    public partial class AgeZodiacCalculatorApp
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var window = new ContentWindow();

            TypeConverter chineseSignConverter = TypeDescriptor.GetConverter(typeof(ChineseSign));
            TypeConverter westernSignConverter = TypeDescriptor.GetConverter(typeof(WesternSign));
            
            IPickDateModel pickDateModel = new ConverterBasedPickDateModel(DateTime.Now, chineseSignConverter, westernSignConverter);
            var pickDateViewModel = new PickDateViewModel(pickDateModel);
            var pickDateContent = new PickDateView(pickDateViewModel);

            IViewNavigator<Type> navigator = new ViewProviderBasedNavigator<Type>(window, new ContentTypeBasedViewContainer(pickDateContent));
            navigator.Navigate(typeof(PickDateView));

            window.Show();
        }
    }
}