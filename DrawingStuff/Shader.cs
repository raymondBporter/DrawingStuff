using OpenTK.Graphics.OpenGL4;
using System.Diagnostics;

namespace DrawingStuff
{
    public class Shader : GLObject
    {
        public Shader(ShaderType shaderType, string source) : base(GL.CreateShader(shaderType), GLType.Program)
        {
            GL.ShaderSource(Handle, source);
            Compile();
            Debug.Assert(IsCompiled(), GetErrorText());
        }

        private void Compile() => GL.CompileShader(Handle);

        public bool IsCompiled()
        {
            GL.GetShader(Handle, ShaderParameter.CompileStatus, out int isCompiled);
            return isCompiled != 0;
        }

        public string GetErrorText()
        {
            if (IsCompiled())
            {
                GL.GetShader(Handle, ShaderParameter.InfoLogLength, out int maxLength);
                GL.GetShaderInfoLog(Handle, maxLength, out maxLength, out string infoLog);
                return infoLog;
            }
            else return "Shader is not compiled";
        }
    }
}
