using System;
using System.Drawing;
using OpenTK;
using OpenTK.Input;

namespace DrawingStuff
{
    public class Program : IDisposable
    {
        public Program(int width, int height) => Window = new OpenkTKWindow(this, width, height);
        public void Run() => Window.Run(); 
        protected virtual void OnLoad(EventArgs e) { }
        protected virtual void OnUnload(EventArgs e) { }
        protected virtual void OnResize(Size size) { }
        protected virtual void OnUpdateFrame(float deltaTime) { }
        protected virtual void OnRenderFrame(float deltaTime) { }
        protected void SwapBuffers() => Window.SwapBuffers();
        public virtual void Dispose() => Window.Dispose();
        protected OpenkTKWindow Window;
        public Vector2 MousePos => Window.MousePos;
        public int WindowWidth => Window.Width;
        public int WindowHeight => Window.Height;
        public Size WindowSize => Window.Size;
        public double Time => Window.Time;

        protected class OpenkTKWindow : GameWindow
        {
            public OpenkTKWindow(Program program, int width, int height) : base(width, height) => Program = program;    
            protected override void OnUpdateFrame(FrameEventArgs eventArgs)
            {
                Time += eventArgs.Time;
                Program.OnUpdateFrame((float)eventArgs.Time);
            }

            protected override void OnMouseMove(MouseMoveEventArgs e)
            {
                base.OnMouseMove(e);
                MousePos = new Vector2(e.X, e.Y); 
            }

            public Vector2 MousePos;

            protected override void OnLoad(EventArgs e) => Program.OnLoad(e);
            protected override void OnUnload(EventArgs e) => Program.OnUnload(e);
            protected override void OnResize(EventArgs e) => Program.OnResize(ClientSize);
            protected override void OnRenderFrame(FrameEventArgs eventArgs) => Program.OnRenderFrame((float)eventArgs.Time);
            new protected void SwapBuffers() => base.SwapBuffers();
            public double Time { get; private set; }
            private Program Program;
        }
    }
}
