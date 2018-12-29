using OpenTK.Graphics.OpenGL4;
using System;
using System.Diagnostics;

namespace DrawingStuff
{
    public class Material 
    {
        public enum PrimitiveType { Triangles, Lines };
        public enum BlendMode { Solid, Transparent };

        protected Material(Color4 color, string texture, PrimitiveType primitiveType, ShaderProgram shaderProgram)
        {
            Primitive = primitiveType;
            Color = color;
            Texture = texture;
            ShaderProgram = shaderProgram;
        }

        public virtual void Begin(XForm worldToDevice)
        {
            SetVertexAttributes();
            ShaderProgram.Begin();
            ShaderProgram.SetMatrix3("transform", (Matrix3)worldToDevice);

            if (BlendType == BlendMode.Solid)
            {
                GL.Disable(EnableCap.Blend);
            }
            else if ( BlendType == BlendMode.Transparent )
            {
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            }
        }
    

        public virtual void End() => ShaderProgram.End();
        void SetVertexAttributes() => VertexDeclaration.SetAttributePointers();
        public virtual VertexDeclaration VertexDeclaration => throw new NotImplementedException();

        public override bool Equals(object obj) => obj is Material material && material == this;
        public override int GetHashCode() => base.GetHashCode();
        static public bool operator ==(Material m1, Material m2)
        {
            return m1?.Texture == m2?.Texture && m1?.Primitive == m2?.Primitive;
        }
        static public bool operator!=(Material m1, Material m2) => !(m1 == m2);

        public Color4 Color;
        public PrimitiveType Primitive;
        public BlendMode BlendType => (Color.A == 1.0f || Color.A == 0.0f) ? BlendMode.Solid : BlendMode.Transparent;
        protected ShaderProgram ShaderProgram;
        protected string Texture;

        protected static readonly ShaderProgram TransformedTexturedShader = new ShaderProgram(@"ColoredTexturedTransformed.vsh", @"ColoredTextured.psh");
        protected static readonly ShaderProgram TransformedColoredShader = new ShaderProgram(@"ColoredTransformed.vsh", @"Colored.psh");
    }


    public class TextureMaterial : Material
    {
        public TextureMaterial(string texture) : base(Color4.White, texture, PrimitiveType.Triangles, TransformedTexturedShader) { }
        public TextureMaterial(string texture, Color4 color) : base(color, texture, PrimitiveType.Triangles, TransformedTexturedShader) { }
        

        public override void Begin(XForm worldToDevice)
        {
            base.Begin(worldToDevice);
            Debug.Assert(Texture != null);
            ShaderProgram.SetInt("tex", 0);
            GL.ActiveTexture(TextureUnit.Texture0);
            ResourceManager.GetTexture(Texture).Bind();
        }

        public override void End()
        {
            base.End();
            Texture2D.UnBind();
        }

        public override VertexDeclaration VertexDeclaration => VertexDeclaration.TexturedVertex;
    }

    public class ColorMaterial : Material
    {
        public ColorMaterial(Color4 color, PrimitiveType primitiveType) : base(color, null, primitiveType, TransformedColoredShader) { }
        public override VertexDeclaration VertexDeclaration => VertexDeclaration.ColoredVertex;
    }
}
