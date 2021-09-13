namespace Shared.View
{
    public class View
    {
        public string Title { get; }
        public int MinHeight { get; }
        public int MinWidth { get; }
        public object Content { get; }

        public View(string title, int minHeight, int minWidth, object content)
        {
            Title = title;
            MinHeight = minHeight;
            MinWidth = minWidth;
            Content = content;
        }
    }
}