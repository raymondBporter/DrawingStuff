using OpenTK.Graphics.OpenGL4;

namespace DrawingStuff
{
    public class Texture2D : GLObject
    {
        public Texture2D() : base(GL.GenTexture(), GLType.Texture) { }
        public void Bind() => GL.BindTexture(TextureTarget.Texture2D, Handle);
        static public void UnBind() => GL.BindTexture(TextureTarget.Texture2D, 0);
    }
}
