namespace DrawingStuff
{
    public struct Rect
    {
        public Rect(Vector2 center, float width, float height)
        {
            Left = center.X - width / 2.0f;
            Right = center.X + width / 2.0f;
            Bottom = center.Y - height / 2.0f;
            Top = center.Y + height / 2.0f;
        }

        public Rect(float left, float right, float bottom, float top)
        {
            Left = left;
            Right = right;
            Bottom = bottom;
            Top = top;
        }
        
        public Vector2 Center
        {
            get => new Vector2((Left + Right)/2, (Top + Bottom)/2);
            set
            {
                float w = Width;
                float h = Height;
                Left = value.X - w / 2;
                Right = value.X + w / 2;
                Top = value.Y - h / 2;
                Bottom = value.Y + h / 2;
            }
        }

        public float Width
        {
            get => Right - Left;
            set => this = new Rect(Center, value, Height);
        }

        public float Height
        {
            get => Top - Bottom;
            set => this = new Rect(Center, Width, value);
        }

        public Rect Translated(Vector2 translation)
        {
            return new Rect(
                Left + translation.X,
                Right + translation.X,
                Bottom + translation.Y,
                Top + translation.Y);
        }

        public float Left;
        public float Right;
        public float Bottom;
        public float Top;

        public Vector2 BottomLeft => new Vector2(Left, Bottom);
        public Vector2 BottomRight => new Vector2(Right, Bottom);
        public Vector2 TopLeft => new Vector2(Left, Top);
        public Vector2 TopRight => new Vector2(Right, Top);
        public Vector2[] Vertices => new Vector2[4] { BottomLeft, TopLeft, TopRight, BottomRight };

        public static bool IntersectRects(Rect a, Rect b)
            => !(a.Right < b.Left || a.Left > b.Right || a.Bottom > b.Top || a.Top < b.Bottom);

        public static bool RectContainsRect(Rect container, Rect containee)
            => container.Left < containee.Left && container.Right > containee.Right && container.Top > containee.Top && container.Bottom < containee.Bottom;
    }
}
