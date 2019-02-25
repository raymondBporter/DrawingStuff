using OpenTK.Graphics.OpenGL4;

namespace DrawingStuff
{
    public class Texture2D : GLObject
    {
        public Texture2D(int width, int height) : base(GL.GenTexture(), GLType.Texture)
        {
            Width = width;
            Height = height;
        }
        public void Bind() => GL.BindTexture(TextureTarget.Texture2D, Handle);
        static public void UnBind() => GL.BindTexture(TextureTarget.Texture2D, 0);

        public readonly int Width;
        public readonly int Height;
    }
}
