using System;
using System.Collections.Generic;
using System.Windows;

namespace Shared.View.Container
{
    public class ContentTypeBasedViewContainer : IViewMutableContainer<Type>
    {
        private readonly IDictionary<Type, FrameworkElement> _views;

        public ContentTypeBasedViewContainer()
        {
            _views = new Dictionary<Type, FrameworkElement>();
        }

        public ContentTypeBasedViewContainer(params FrameworkElement[] views) : this()
        {
            RegisterViews(views);
        }

        public FrameworkElement? GetView(Type contentType)
        {
            if (_views.TryGetValue(contentType, out FrameworkElement? view))
            {
                return view;
            }

            return null;
        }

        public void RegisterViews(params FrameworkElement[] views)
        {
            foreach (var view in views)
            {
                Type contentType = view.GetType();
                _views.Add(contentType, view);
            }
        }
    }
}