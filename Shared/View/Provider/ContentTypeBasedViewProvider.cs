using System;
using System.Collections.Generic;

namespace Shared.View.Provider
{
    public class ContentTypeBasedViewProvider : IViewProvider<Type>
    {
        private readonly IDictionary<Type, object> _views;

        public ContentTypeBasedViewProvider(ICollection<View> views)
        {
            _views = new Dictionary<Type, object>();
            foreach (View view in views)
            {
                RegisterView(view);
            }
        }

        public View? GetView(Type contentType)
        {
            if (_views.TryGetValue(contentType, out object? view))
            {
                return (View) view;
            }

            return null;
        }

        private void RegisterView(View view)
        {
            Type contentType = view.Content.GetType();
            _views.Add(contentType, view);
        }
    }
}