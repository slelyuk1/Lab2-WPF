using System;
using System.Collections.Generic;
using System.Windows;

namespace Shared.Tool.View
{
    public class DefaultViewModelAware<TView, TViewModel> : IViewModelAware<TView, TViewModel>, IEquatable<DefaultViewModelAware<TView, TViewModel>>
        where TView : FrameworkElement
    {
        private readonly TView _view;
        private TViewModel _viewModel;

        public DefaultViewModelAware(TView view)
        {
            _view = view;
            _viewModel = (TViewModel) view.DataContext;
            view.DataContextChanged += (_, args) => _viewModel = (TViewModel) args.NewValue;
        }

        public TView GetView()
        {
            return _view;
        }

        public TViewModel GetViewModel()
        {
            return _viewModel;
        }

        public bool Equals(DefaultViewModelAware<TView, TViewModel>? other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return ReferenceEquals(this, other) || EqualityComparer<TView>.Default.Equals(_view, other._view);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == this.GetType() && Equals((DefaultViewModelAware<TView, TViewModel>) obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<TView>.Default.GetHashCode(_view);
        }
    }
}