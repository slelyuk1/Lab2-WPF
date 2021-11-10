using AgeZodiacCalculator.ViewModel;

namespace AgeZodiacCalculator.View
{
    public partial class PickDateView
    {
        internal PickDateView(PickDateViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}