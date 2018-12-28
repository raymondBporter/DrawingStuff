using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace DrawingStuff
{
    public class ShaderProgram : GLObject
    {
        public ShaderProgram(string vertexShaderFile, string fragmentShaderFile) : base(GL.CreateProgram(), GLType.Program)
        {
            VertexShader = ResourceManager.GetVertexShader(vertexShaderFile);
            FragmentShader = ResourceManager.GetFragmentShader(fragmentShaderFile);
            GL.AttachShader(Handle, VertexShader.Handle);
            GL.AttachShader(Handle, FragmentShader.Handle);
            GL.LinkProgram(Handle);
        }

        public int GetUniformLocation(string name)
        {
            if (UniformLocations.TryGetValue(name, out int location))
                return location;
            int loc = GL.GetUniformLocation(Handle, name);
            UniformLocations.Add(name, loc);
            return loc;
        }

        public void SetMatrix3(string name, Matrix3 matrix) => GL.UniformMatrix3(GetUniformLocation(name), 1, true, (float[])matrix);
        public void SetInt(string name, int value) => GL.Uniform1(GetUniformLocation(name), value);

        public void Begin() => GL.UseProgram(Handle);
        public void End() => GL.UseProgram(0);

        protected Shader VertexShader;
        protected Shader FragmentShader;
        public Dictionary<string, int> UniformLocations = new Dictionary<string, int>();
    }
}
