using OpenTK.Graphics.OpenGL4;
using System;
using System.Runtime.InteropServices;

namespace DrawingStuff
{
    public class GLBuffer : GLObject
    {
        protected GLBuffer(BufferTarget target) : base(GL.GenBuffer(), GLType.Buffer) => Target = target;
        public void SetData<T>(T[] data, BufferUsageHint usageHint) where T : struct =>
            GL.BufferData(Target, (IntPtr)(Marshal.SizeOf(typeof(T)) * data.Length), data, usageHint);
        public void Bind() => GL.BindBuffer(Target, Handle);
        protected BufferTarget Target;
    };

    public class IndexBuffer : GLBuffer
    {
        public IndexBuffer() : base(BufferTarget.ElementArrayBuffer) { }
        public static void UnBind() => GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
    };

    public class VertexBuffer : GLBuffer
    {
        public VertexBuffer() : base(BufferTarget.ArrayBuffer) { }
        public static void UnBind() => GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
    };
}