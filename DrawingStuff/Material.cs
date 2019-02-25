using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace DrawingStuff
{
    public class Material 
    {
        public Material(string texture = null)
        {
            Texture = texture;
            ShaderProgram = (Texture == null) ? ShaderProgram.Colored : ShaderProgram.Textured;

            packed = TexturePacker.PackTextures(new List<string> { "table.png" }, 202, 202);

        }

        public void Begin(float[] worldToDeviceFloats)
        {
            VertexDeclaration.SetAttributePointers();

            ShaderProgram.Begin();
            ShaderProgram.SetMatrix3("transform", worldToDeviceFloats);
            if (Texture != null)
            {
                ShaderProgram.SetInt("tex", 0);
                GL.ActiveTexture(TextureUnit.Texture0);
                ResourceManager.GetTexture(Texture).Bind();
              //packed.Texture.Bind();
            }
        }

        public void End()
        {
            ShaderProgram.End();
            if ( Texture != null )
                Texture2D.UnBind();
        }

        public VertexDeclaration VertexDeclaration => Texture == null ? VertexDeclaration.ColoredVertex : VertexDeclaration.TexturedVertex;
        public bool CanBatchWith(Material m) => Texture == m?.Texture && ShaderProgram == m?.ShaderProgram;

        public ShaderProgram ShaderProgram { get; }
        public string Texture { get; set; }
        PackedTexture packed;
    }
}
