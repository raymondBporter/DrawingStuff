using OpenTK.Input;
using static DrawingStuff.XForm;
using static DrawingStuff.FloatMath;

namespace DrawingStuff
{
    public class Camera : IWorldBoundingRect
    {
        public Camera(float windowWidth, float windowHeight, float pixelsPerUnit)
        {
            PixelsPerUnit = pixelsPerUnit;
            WindowWidth = windowWidth;
            WindowHeight = windowHeight;
        }

        public Vector2 Position = Vector2.Zero;
        public float Angle = 0.0f;
        public float UnitsPerPixel;
        public float PixelsPerUnit
        {
            get => 1.0f / UnitsPerPixel;
            set { UnitsPerPixel = 1.0f / value; }
        }
        public float WindowWidth;
        public float WindowHeight;
        public Vector2 WindowSize => new Vector2(WindowWidth, WindowHeight);
        public float ViewWidth => WindowWidth * UnitsPerPixel;
        public float ViewHeight => WindowHeight * UnitsPerPixel;
        public Vector2 ViewSize => new Vector2(ViewWidth, ViewHeight);
        public XForm WorldToView => Rotation(-Angle) * Translation(-Position);
        public XForm WindowToWorld => ViewToWorld * WindowToView;
        public XForm ViewToWorld => new XForm(Angle, Position);
        public XForm ViewToDevice => Scale(2.0f / ViewWidth, 2.0f / ViewHeight);
        public XForm WorldToDevice => ViewToDevice * WorldToView;
        public XForm WindowToView => Scale(UnitsPerPixel) * ReflectionY * Translation(-WindowSize / 2.0f);

        public Rect WorldBoundingRect { get; private set; }

        public void UpdateBoundingRect()
        {
            float xMin, xMax, yMin, yMax;
            Vector2[] viewRectVerts = new Rect(Vector2.Zero, ViewWidth, ViewHeight).Vertices;

            Vector2 v = ViewToWorld * viewRectVerts[0];
            xMin = xMax = v.X;
            yMin = yMax = v.Y;

            for (int i = 1; i < 4; i++)
            {
                v = ViewToWorld * viewRectVerts[i];
                if (v.X < xMin)
                {
                    xMin = v.X;
                }
                if (v.X > xMax)
                {
                    xMax = v.X;
                }
                if (v.Y < yMin)
                {
                    yMin = v.Y;
                }
                if (v.Y > yMax)
                {
                    yMax = v.Y;
                }
            }
            WorldBoundingRect = new Rect(xMin, xMax, yMin, yMax);
        }


        public void HandleInput(float dt)
        {
            Vector2 v = Vector2.Zero;

            if (KeyPressed(Key.Up))
            {
                v += Vector2.YAxis;
            }
            if (KeyPressed(Key.Down))
            {
                v -= Vector2.YAxis;
            }
            if (KeyPressed(Key.Left))
            {
                v -= Vector2.XAxis;
            }
            if (KeyPressed(Key.Right))
            {
                v += Vector2.XAxis;
            }
            if (KeyPressed(Key.A))
            {
                Angle += AngularSpeed * dt;
            }
            if (KeyPressed(Key.S))
            {
                Angle -= AngularSpeed * dt;
            }
            if (KeyPressed(Key.Z))
            {
                UnitsPerPixel *= Exp(ZoomSpeed * dt);
            }
            if (KeyPressed(Key.X))
            {
                UnitsPerPixel *= Exp(-ZoomSpeed * dt);
            }
            if (v != Vector2.Zero)
            {
                v.Normalize();
            }
            Position += new Rotation2(Angle) * v * LinearSpeed * dt;
            UpdateBoundingRect();
        }

        private readonly float ZoomSpeed = 1.02f;
        private readonly float AngularSpeed = 3f;
        private readonly float LinearSpeed = 27.0f;

        private bool KeyPressed(Key key) => Keyboard.GetState()[key];
    }
}