using OpenTK.Input;
using static DrawingStuff.FloatMath;

namespace DrawingStuff
{
    class CameraController 
    {
        public CameraController(Camera camera) => Camera = camera;

        public void Update(float dt)
        {
            Vector2 v = Vector2.Zero;

            if ( KeyPressed(Key.Up))
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
                Camera.Angle += AngularSpeed * dt;
            }
            if (KeyPressed(Key.S))
            {
                Camera.Angle -= AngularSpeed * dt;
            }
            if (KeyPressed(Key.Z))
            {
                Camera.UnitsPerPixel *= Exp(ZoomSpeed * dt);
            }
            if (KeyPressed(Key.X))
            {
                Camera.UnitsPerPixel *= Exp(-ZoomSpeed * dt);
            }
            if (v != Vector2.Zero)
            {
                v.Normalize();
            }
            Camera.Position += new Rotation2(Camera.Angle) * v * LinearSpeed * dt;
        }

        readonly float ZoomSpeed = 1.02f;
        readonly float AngularSpeed = 3f;
        readonly float LinearSpeed = 7.0f;
        Camera Camera;
        bool KeyPressed(Key key) => Keyboard.GetState()[key];
    }
}
