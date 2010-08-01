namespace RedBadger.Xpf.Presentation.Controls
{
    using System;

    public class StackPanel : Panel
    {
        public Orientation Orientation { get; set; }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            bool isHorizontalOrientation = this.Orientation == Orientation.Horizontal;
            var finalRect = new Rect(arrangeSize);
            float width = 0f;
            float height = 0f;
            foreach (UIElement child in this.Children)
            {
                if (child != null)
                {
                    if (isHorizontalOrientation)
                    {
                        finalRect.X += width;
                        width = child.DesiredSize.Width;
                        finalRect.Width = width;
                        finalRect.Height = Math.Max(arrangeSize.Height, child.DesiredSize.Height);
                    }
                    else
                    {
                        finalRect.Y += height;
                        height = child.DesiredSize.Height;
                        finalRect.Height = height;
                        finalRect.Width = Math.Max(arrangeSize.Width, child.DesiredSize.Width);
                    }

                    child.Arrange(finalRect);
                }
            }

            return arrangeSize;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var size = new Size();
            bool isHorizontalOrientation = this.Orientation == Orientation.Horizontal;
            if (isHorizontalOrientation)
            {
                availableSize.Width = float.PositiveInfinity;
            }
            else
            {
                availableSize.Height = float.PositiveInfinity;
            }

            foreach (UIElement child in this.Children)
            {
                if (child != null)
                {
                    child.Measure(availableSize);
                    Size desiredSize = child.DesiredSize;
                    if (isHorizontalOrientation)
                    {
                        size.Width += desiredSize.Width;
                        size.Height = Math.Max(size.Height, desiredSize.Height);
                    }
                    else
                    {
                        size.Width = Math.Max(size.Width, desiredSize.Width);
                        size.Height += desiredSize.Height;
                    }
                }
            }

            return size;
        }
    }
}