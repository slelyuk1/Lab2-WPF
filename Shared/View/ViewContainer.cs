using System;
using System.Collections.Generic;

namespace Shared.Navigation
{
    public class ViewContainer
    {
        private readonly IDictionary<Type, object> _views;

        public ViewContainer()
        {
            _views = new Dictionary<Type, object>();
        }

        public void RegisterView(View toAdd)
        {
            Type contentType = toAdd.Content.GetType();
            _views.Add(contentType, toAdd);
        }

        public View? GetView(Type contentType)
        {
            if (_views.TryGetValue(contentType, out object? view))
            {
                return (View) view;
            }

            return null;
        }
    }
}