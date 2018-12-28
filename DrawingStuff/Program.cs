using OpenTK.Graphics.OpenGL4;
using System.Drawing;

namespace DrawingStuff
{
    public class AppWindow : Program 
    {
        public AppWindow() : base(1024, 768)
        {
            Camera = new Camera(WindowWidth, WindowHeight, 90.0f);
            CameraController = new CameraController(Camera);
            GL.Viewport(WindowSize);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            World = new TestWorld();
        }

        protected override void OnResize(Size windowSize)
        {
            GL.Viewport(windowSize);
            Camera.WindowWidth = windowSize.Width;
            Camera.WindowHeight = windowSize.Height;
        }

        protected override void OnRenderFrame(float deltaTime)
        {
            GL.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            World.Draw(Camera, deltaTime);
            SwapBuffers();
        }

        protected override void OnUpdateFrame(float deltaTime)
        {
            CameraController.Update(deltaTime);
            World.HandleInput(Camera.WindowToWorld * MousePos, deltaTime, Camera);
            World.Update(deltaTime);
        }

        Camera Camera;
        CameraController CameraController;
        IWorld World;

        //[STAThread]
        public static void Main()
        {          
            AppWindow window = new AppWindow();
            window.Run();
        }
    }
}