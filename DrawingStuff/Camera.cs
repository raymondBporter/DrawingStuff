using static DrawingStuff.XForm;

namespace DrawingStuff
{
    public class Camera
    {
        public Camera(float windowWidth, float windowHeight, float pixelsPerUnit)
        {
            UnitsPerPixel = 1.0f/pixelsPerUnit;
            WindowWidth = windowWidth;
            WindowHeight = windowHeight;
        }

        public Vector2 Position = Vector2.Zero;
        public float Angle = 0.0f;
        public float UnitsPerPixel;
        public float PixelsPerUnit => 1.0f / UnitsPerPixel;
        public float WindowWidth;
        public float WindowHeight;
        public float ViewWidth => WindowWidth * UnitsPerPixel;
        public float ViewHeight => WindowHeight * UnitsPerPixel;
        public XForm WorldToView => Rotation(-Angle) * Translation(-Position);
        public XForm WindowToWorld => ViewToWorld * WindowToView;
        public XForm ViewToWorld => new XForm(Angle, Position);
        public XForm ViewToDevice => Scale(2.0f / ViewWidth, 2.0f / ViewHeight);
        public XForm WorldToDevice => ViewToDevice * WorldToView;
        public XForm WindowToView => Scale(UnitsPerPixel) * ReflectionY * Translation(-new Vector2(WindowWidth / 2.0f, WindowHeight / 2.0f));
    }
}