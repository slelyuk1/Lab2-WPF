using System;
using System.ComponentModel;
using System.Windows;
using AgeZodiacCalculator.Content;
using AgeZodiacCalculator.Info;
using AgeZodiacCalculator.Model;
using AgeZodiacCalculator.Model.Impl;
using AgeZodiacCalculator.ViewModel;
using Shared.Navigation;

namespace AgeZodiacCalculator
{
    public partial class AgeZodiacCalculatorApp
    {
        
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var window = new ContentWindow();

            TypeConverter ageInfoConverter = TypeDescriptor.GetConverter(typeof(AgeInfo));
            TypeConverter chineseSignConverter = TypeDescriptor.GetConverter(typeof(ChineseSign));
            TypeConverter westernSignConverter = TypeDescriptor.GetConverter(typeof(WesternSign));

            IPickDateModel pickDateModel = new ConverterBasedPickDateModel(DateTime.Now, chineseSignConverter, westernSignConverter);
            var pickDateViewModel = new PickDateViewModel(pickDateModel, ageInfoConverter, chineseSignConverter, westernSignConverter);
            var pickDateContent = new PickDateContent(pickDateViewModel);
            var pickDateView = new View(pickDateContent, 300, 300);

            var viewContainer = new ViewContainer();
            viewContainer.RegisterView(pickDateView);

            var navigator = new ViewNavigator(viewContainer);
            navigator.Navigate(window, typeof(PickDateContent));

            window.Show();
        }
    }
}