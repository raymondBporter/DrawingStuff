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

        public int Handle;
        public GLType GlType;


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
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
                }

                disposedValue = true;
            }
        }

         ~GLObject() => Dispose(false);
         

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
