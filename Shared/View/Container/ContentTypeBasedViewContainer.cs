using System;
using System.Collections.Generic;

namespace Shared.View.Container
{
    public class ContentTypeBasedViewContainer : IViewMutableContainer<Type>
    {
        private readonly IDictionary<Type, object> _views;

        public ContentTypeBasedViewContainer()
        {
            _views = new Dictionary<Type, object>();
        }

        public ContentTypeBasedViewContainer(params View[] views) : this()
        {
            RegisterViews(views);
        }

        public View? GetView(Type contentType)
        {
            if (_views.TryGetValue(contentType, out object? view))
            {
                return (View) view;
            }

            return null;
        }

        public void RegisterViews(params View[] views)
        {
            foreach (var view in views)
            {
                Type contentType = view.Content.GetType();
                _views.Add(contentType, view);
            }
        }
    }
}