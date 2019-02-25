using System;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace DrawingStuff
{
    public class Program : GameWindow
    {
        public Program(int width, int height) : base(width, height)
        {
            Camera = new Camera(width, height, 9.0f);
            GL.Viewport(ClientSize);
            World = new TestWorld(Camera);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            Time += e.Time; 
            DeltaTime = (float)e.Time;
            Camera.HandleInput(DeltaTime);
            World.HandleInput(Camera.WindowToWorld * MousePosition, DeltaTime);
            World.Update(DeltaTime);
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            MousePosition = new Vector2(e.X, e.Y);
            MouseDelta = new Vector2(e.XDelta, e.YDelta);
        }

        protected override void OnLoad(EventArgs e) { }
        protected override void OnUnload(EventArgs e) { }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(ClientSize);
            Camera.WindowWidth = ClientSize.Width;
            Camera.WindowHeight = ClientSize.Height;
        }

        protected override void OnRenderFrame(FrameEventArgs eventArgs)
        {
            GL.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            World.Draw(Camera, DeltaTime);
            SwapBuffers();
        }

        private double Time;
        private Vector2 MousePosition;
        private Vector2 MouseDelta;
        private float DeltaTime;
        private Camera Camera;
        private TestWorld World;

        //[STAThread]
        public static void Main()
        {
            Program program = new Program(1024, 786);
            program.Run();
        }
    }
    
}
