using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;

#pragma warning disable 8766

namespace Shared.ComponentModel.Adapter
{
    public class GenericCollectionViewAdapter<T> : ICollectionView, IEnumerable<T>
    {
        private readonly ICollectionView _implementation;

        public GenericCollectionViewAdapter(ICollectionView implementation)
        {
            _implementation = implementation;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new GenericEnumeratorAdapter<T>(GetEnumerator());
        }

        public IEnumerator GetEnumerator()
        {
            return _implementation.GetEnumerator();
        }

        public event NotifyCollectionChangedEventHandler? CollectionChanged
        {
            add => _implementation.CollectionChanged += value;
            remove => _implementation.CollectionChanged -= value;
        }

        public bool Contains(object item)
        {
            return Contains((T) item);
        }

        public bool Contains(T item)
        {
            return _implementation.Contains(item);
        }

        public void Refresh()
        {
            _implementation.Refresh();
        }

        public IDisposable DeferRefresh()
        {
            return _implementation.DeferRefresh();
        }

        public bool MoveCurrentToFirst()
        {
            return _implementation.MoveCurrentToFirst();
        }

        public bool MoveCurrentToLast()
        {
            return _implementation.MoveCurrentToLast();
        }

        public bool MoveCurrentToNext()
        {
            return _implementation.MoveCurrentToNext();
        }

        public bool MoveCurrentToPrevious()
        {
            return _implementation.MoveCurrentToPrevious();
        }

        public bool MoveCurrentTo(object item)
        {
            return MoveCurrentTo((T) item);
        }

        public bool MoveCurrentTo(T item)
        {
            return _implementation.MoveCurrentTo(item);
        }

        public bool MoveCurrentToPosition(int position)
        {
            return _implementation.MoveCurrentToPosition(position);
        }

        public CultureInfo Culture
        {
            get => _implementation.Culture;
            set => _implementation.Culture = value;
        }

        public IEnumerable SourceCollection => _implementation.SourceCollection;

        Predicate<object?> ICollectionView.Filter
        {
            get => obj => Filter((T?) obj);
            set => Filter = typedObj => value(typedObj);
        }

        public Predicate<T?> Filter
        {
            get => typedObj => _implementation.Filter(typedObj);
            set => _implementation.Filter = obj => value((T) obj);
        }

        public bool CanFilter => _implementation.CanFilter;

        public SortDescriptionCollection SortDescriptions => _implementation.SortDescriptions;

        public bool CanSort => _implementation.CanSort;

        public bool CanGroup => _implementation.CanGroup;

        public ObservableCollection<GroupDescription> GroupDescriptions => _implementation.GroupDescriptions;

        // todo make generic
        public ReadOnlyObservableCollection<object> Groups => _implementation.Groups;

        public bool IsEmpty => _implementation.IsEmpty;

        // todo understand what it means
        object? ICollectionView.CurrentItem => CurrentItem;

        public T? CurrentItem => (T?) _implementation.CurrentItem;

        public int CurrentPosition => _implementation.CurrentPosition;

        public bool IsCurrentAfterLast => _implementation.IsCurrentAfterLast;

        public bool IsCurrentBeforeFirst => _implementation.IsCurrentBeforeFirst;

        public event CurrentChangingEventHandler? CurrentChanging
        {
            add => _implementation.CurrentChanging += value;
            remove => _implementation.CurrentChanging -= value;
        }

        public event EventHandler? CurrentChanged
        {
            add => _implementation.CurrentChanged += value;
            remove => _implementation.CurrentChanged -= value;
        }
    }
}