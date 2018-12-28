using System.Diagnostics;

namespace DrawingStuff
{
    public struct Rect
    {

        public Rect(Vector2 center, Vector2 size)
        {
            Debug.Assert(size.X > 0 && size.Y > 0);
            Center = center;
            Size = size;
        }

        public Rect(float xMin, float xMax, float yMin, float yMax)
        : this(new Vector2((xMin + xMax) / 2.0f, (yMin + yMax) / 2.0f), new Vector2(xMax - xMin, yMax - yMin)) { }
        

        public Vector2 Center { get; set; }
        public float Width
        {
            get => Size.X; 
            set => Size = new Vector2(value, Height);
        }

        public float Height
        {
            get => Size.Y; 
            set => Size = new Vector2(Width, value); 
        }

        public float Left => Center.X - Width / 2.0f;
        public float Right => Center.X + Width / 2.0f;
        public float Bottom => Center.Y - Height / 2.0f;
        public float Top => Center.Y + Height / 2.0f;
        public Vector2 BottomLeft => new Vector2(Left, Bottom);
        public Vector2 BottomRight => new Vector2(Right, Bottom);
        public Vector2 TopLeft => new Vector2(Left, Top);
        public Vector2 TopRight => new Vector2(Right, Top);
        public Vector2[] Vertices => new Vector2[4] { BottomLeft, TopLeft, TopRight, BottomRight };
        public Vector2 Size;
    }
}
