﻿using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace DrawingStuff
{
    public class ShaderProgram : GLObject
    {
        public ShaderProgram(Shader vertexShader, Shader fragmentShader) : base(GL.CreateProgram(), GLType.Program)
        {
            VertexShader = vertexShader;
            FragmentShader = fragmentShader;
            GL.AttachShader(Handle, VertexShader.Handle);
            GL.AttachShader(Handle, FragmentShader.Handle);
            GL.LinkProgram(Handle);
        }

        private int GetUniformLocation(string name)
        {
            if (!UniformLocations.TryGetValue(name, out int location))
            {
                location = GL.GetUniformLocation(Handle, name);
                UniformLocations.Add(name, location);
            }
            return location;
        }

        public void SetMatrix3(string name, float[] matrix) => GL.UniformMatrix3(GetUniformLocation(name), 1, true, matrix);
        public void SetInt(string name, int value) => GL.Uniform1(GetUniformLocation(name), value);

        public void Begin() => GL.UseProgram(Handle);
        public void End() => GL.UseProgram(0);

        protected Shader VertexShader;
        protected Shader FragmentShader;
        public Dictionary<string, int> UniformLocations = new Dictionary<string, int>();


        public static ShaderProgram Colored = new ShaderProgram(
                    ResourceManager.GetVertexShader(@"ColoredTransformed.vsh"),
                    ResourceManager.GetFragmentShader(@"Colored.psh"));
        public static ShaderProgram Textured = new ShaderProgram(
                    ResourceManager.GetVertexShader(@"ColoredTexturedTransformed.vsh"),
                    ResourceManager.GetFragmentShader(@"ColoredTextured.psh"));  
    }
}
