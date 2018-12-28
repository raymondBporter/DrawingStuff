using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using OpenTK.Graphics.OpenGL4;

namespace DrawingStuff
{
    static class ResourceManager
    {
        public static Texture2D GetTexture(string filename) => GetResource(TexturesPath + filename, Textures, LoadTexture);
        public static Shader GetFragmentShader(string filename) => GetResource(ShadersPath + filename, Shaders, LoadFragmentShader);
        public static Shader GetVertexShader(string filename) => GetResource(ShadersPath + filename, Shaders, LoadVertexShader);

        static Resource GetResource<Resource>(string filename, Dictionary<string, Resource> loadedResources, Func<string, Resource> loadResourceFunc)
        {
            if (loadedResources.ContainsKey(filename))
                return loadedResources[filename];
            var resource = loadResourceFunc(filename);
            loadedResources.Add(filename, resource);
            return resource;
        }

        static string GetTextFromFile(string filename)
        {
            try
            {
                return new StreamReader(filename).ReadToEnd();  
            }
            catch (Exception e)
            {
                Debug.Assert(false, filename + " could not be read: " + e.Message);
                return "";
            }
        }

        static Shader LoadFragmentShader(string filename) => LoadShader(ShaderType.FragmentShader, filename);
        static Shader LoadVertexShader(string filename) => LoadShader(ShaderType.VertexShader, filename);
        static Shader LoadShader(ShaderType shaderType, string filename) => new Shader(shaderType, GetTextFromFile(filename));
        static Texture2D LoadTexture(string filename)
        {
            Bitmap bitmap = new Bitmap(filename);         
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
            Texture2D texture = new Texture2D();
            texture.Bind();
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData data = bitmap.LockBits(rect, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL4.PixelFormat.Rgba, PixelType.UnsignedByte, data.Scan0);
            bitmap.UnlockBits(data);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            return texture;
        }

        static readonly Dictionary<string, Shader> Shaders = new Dictionary<string, Shader>();
        static readonly Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();
        public const string TexturesPath = "../../Textures/";
        public const string ShadersPath = "../../Shaders/";
    }
}

