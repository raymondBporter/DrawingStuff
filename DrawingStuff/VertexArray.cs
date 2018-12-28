using OpenTK.Graphics.OpenGL4;

namespace DrawingStuff
{
    public class VertexArray : GLObject
    {
        public VertexArray() : base(GL.GenVertexArray(), GLType.VertexArray) { }
        public void Bind() => GL.BindVertexArray(Handle);
        public static void UnBind() => GL.BindVertexArray(0);
    }
}
