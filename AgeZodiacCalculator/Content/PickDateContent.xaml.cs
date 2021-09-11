using System;
using AgeZodiacCalculator.Model;
using AgeZodiacCalculator.Model.Impl;
using AgeZodiacCalculator.ViewModel;

namespace AgeZodiacCalculator.Content
{
    public partial class PickDateContent
    {
        internal PickDateContent(PickDateViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}