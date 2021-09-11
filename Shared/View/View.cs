namespace Shared.Navigation
{
    public class View
    {
        public object Content { get; }
        public int MinHeight { get; }
        public int MinWidth { get; }

        public View(object content, int minHeight, int minWidth)
        {
            Content = content;
            MinHeight = minHeight;
            MinWidth = minWidth;
        }
    }
}