using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Shared.Tool.View;

namespace Shared.View.Container
{
    public class DefaultViewContainer : IMutableViewContainer
    {
        private readonly IDictionary<Type, ISet<object>> _viewToViewModelAware;
        private readonly IDictionary<Type, ISet<object>> _viewModelToViewModelAware;

        public DefaultViewContainer()
        {
            _viewToViewModelAware = new Dictionary<Type, ISet<object>>();
            _viewModelToViewModelAware = new Dictionary<Type, ISet<object>>();
        }

        public void AddViewModelAware<TView, TViewModel>(IViewModelAware<TView, TViewModel> toAdd)
        {
            Type viewType = typeof(TView);
            if (_viewToViewModelAware.TryGetValue(viewType, out ISet<object> foundForViewType))
            {
                foundForViewType.Add(toAdd);
            }
            else
            {
                _viewToViewModelAware.Add(viewType, InitializeForStorage(toAdd));
            }

            Type viewModelType = typeof(TViewModel);
            if (_viewModelToViewModelAware.TryGetValue(viewModelType, out ISet<object> foundForViewModelType))
            {
                foundForViewModelType.Add(toAdd);
            }
            else
            {
                _viewModelToViewModelAware.Add(viewModelType, InitializeForStorage(toAdd));
            }
        }

        public IViewModelAware<TView, TViewModel> GetRequiredViewModelAware<TView, TViewModel>()
        {
            IViewModelAware<TView, TViewModel>? viewModelAware = GetViewModelAware<TView, TViewModel>();
            if (viewModelAware == null)
            {
                throw new InvalidDataException(
                    "Couldn't find IViewModelAware for viewType=" + typeof(TView) + ", viewModelType=" + typeof(TViewModel)
                );
            }

            return viewModelAware;
        }

        public IViewModelAware<TView, TViewModel>? GetViewModelAware<TView, TViewModel>()
        {
            Type viewType = typeof(TView);
            Type viewModelType = typeof(TViewModel);

            ISet<object> foundByView = FindNeededSet(_viewToViewModelAware, viewType);
            ISet<object> foundByViewModel = FindNeededSet(_viewModelToViewModelAware, viewModelType);

            ISet<object> result = IntersectionOfSets<TView, TViewModel>(foundByView, foundByViewModel);
            if (foundByView.Count != 1)
            {
                return null;
            }

            return (IViewModelAware<TView, TViewModel>) result.First();
        }

        protected virtual ISet<object> FindNeededSet(IDictionary<Type, ISet<object>> typeToValue, Type toFindBy)
        {
            if (typeToValue.TryGetValue(toFindBy, out ISet<object>? found))
            {
                return InitializeForReturn(found);
            }

            ISet<object> result = InitializeForReturn();
            foreach (KeyValuePair<Type, ISet<object>> entry in typeToValue)
            {
                Type foundType = entry.Key;
                if (foundType == toFindBy || foundType.IsSubclassOf(toFindBy))
                {
                    result.UnionWith(entry.Value);
                }
            }

            return result;
        }

        protected virtual ISet<object> IntersectionOfSets<TView, TViewModel>(ISet<object> foundByView, ISet<object> foundByViewModel)
        {
            if (foundByView.Count < 1)
            {
                return foundByViewModel;
            }

            if (foundByViewModel.Count < 1)
            {
                return foundByView;
            }

            foundByView.IntersectWith(foundByViewModel);
            return foundByView;
        }

        protected virtual ISet<object> InitializeForStorage(object initialItem)
        {
            return new HashSet<object> {initialItem};
        }

        protected virtual ISet<object> InitializeForReturn(IEnumerable<object>? other = null)
        {
            return other != null ? new HashSet<object>(other) : new HashSet<object>();
        }
    }
}