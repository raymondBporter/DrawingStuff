using OpenTK.Graphics.OpenGL4;
using System;


namespace DrawingStuff
{
    public class GLObject : IDisposable
    {
        public enum GLType { Buffer, VertexArray, Program, Texture };

        public GLObject(int handle, GLType glType)
        {
            Handle = handle;
            GlType = glType;
        }

        public int Handle { get; protected set;  }
        public GLType GlType;

        #region IDisposable Support
        private bool Disposed = false; // To detect redundant calls
        public void Dispose()
        {
            if (!Disposed)
            {
                switch (GlType)
                {
                    case GLType.Buffer:
                        GL.DeleteBuffer(Handle);
                        break;
                    case GLType.Program:
                        GL.DeleteProgram(Handle);
                        break;
                    case GLType.VertexArray:
                        GL.DeleteVertexArray(Handle);
                        break;
                    case GLType.Texture:
                        GL.DeleteTexture(Handle);
                        break;
                    default:
                        throw new NotImplementedException();

                }
                Disposed = true;
            }
        }
        #endregion IDisposable Support
    }
    
}
