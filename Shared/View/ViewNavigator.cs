using System;
using Shared.Window;

namespace Shared.Navigation
{
    public class ViewNavigator
    {
        private readonly ViewContainer _viewContainer;

        public ViewNavigator(ViewContainer viewContainer)
        {
            _viewContainer = viewContainer;
        }

        public void Navigate(IViewVisualizer visualizer, Type contentType)
        {
            View? view = _viewContainer.GetView(contentType);
            if (view == null)
            {
                throw new InvalidOperationException("Tried to navigate to non-existent view with content type: " + contentType);
            }

            visualizer.Visualize(view);
        }
    }
}