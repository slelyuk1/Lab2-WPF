namespace AgeZodiacCalculator.Navigation.View
{
    internal class View
    {
        public string Name { get; }
        public int MinWidth { get; }
        public int MinHeight { get; }

        private readonly object _content;

        public View(string name, object content, int minWidth, int minHeight)
        {
            Name = name;
            _content = content;
            MinWidth = minWidth;
            MinHeight = minHeight;
        }

        public virtual object Content()
        {
            return _content;
        }
    }

    internal class View<T> : View
    {
        public View(string name, T content, int minWidth, int minHeight) : base(name, content, minWidth, minHeight)
        {
        }

        public override string Content()
        {
            return "";
        }
    }
}