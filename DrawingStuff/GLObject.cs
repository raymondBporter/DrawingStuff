using OpenTK.Graphics.OpenGL4;
using System;
using System.Diagnostics;
using System.Threading;

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
        static Thread MainThread = Thread.CurrentThread;

        #region IDisposable Support
        private bool Disposed = false; // To detect redundant calls
        public void Dispose()
        {
            if (!Disposed)
            {
                if (Thread.CurrentThread != MainThread)
                    Debug.Assert(false, "wrong thread disposed gl resource");
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
