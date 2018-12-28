using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using System.Linq;

namespace DrawingStuff
{
    public class Batch 
    {
        public void BufferData(BufferUsageHint usageHint)
        {
            VertexBuffer.SetData(VertexData, usageHint);
            IndexBuffer.SetData(Indices.ToArray(), usageHint);
        }

        public void Draw(Material material, XForm worldToDevice)
        {
            if (NumVertices == 0)
                return;
            BindBuffers();
            BufferData(BufferUsageHint.StreamDraw);
            material.Begin(worldToDevice);
            GL.DrawElements(BeginMode(material.Primitive), Indices.Count, DrawElementsType.UnsignedInt, 0);
            material.End();
            UnBindBuffers();
        }

        BeginMode BeginMode(Material.PrimitiveType primitiveType) => 
            primitiveType == Material.PrimitiveType.Lines ? OpenTK.Graphics.OpenGL4.BeginMode.Lines : OpenTK.Graphics.OpenGL4.BeginMode.Triangles;
        
        

        void BindBuffers()
        {
            VertexArray.Bind();
            VertexBuffer.Bind();
            IndexBuffer.Bind();
        }

        void UnBindBuffers()
        {
            IndexBuffer.UnBind();
            VertexBuffer.UnBind();
            VertexArray.UnBind();
        }

        public void Clear()
        {
            Indices.Clear();
            NumVertices = 0;
        }


        public void AddVisuals(List<Visual> visuals)
        {
            foreach (var visual in visuals)
            {
                AddVisual(visual.Geom, visual.Material, visual.Transform, visual.UseTransform, visual.ZCoord);
            }
        }

        public void AddVisual(Visual visual) => AddVisual(visual.Geom, visual.Material, visual.Transform, visual.UseTransform, visual.ZCoord);
        

        public void AddVisual(Geom geom, Material material, XForm transform, bool useSoftwareTransform, float zCoord)
        {
            Indices.AddRange(geom.Indices.Select(index => index + NumVertices));
            int size = material.VertexDeclaration.Size;
            for (int i = 0; i < geom.Positions.Length; i++)
            {
                Vector2 v = geom.Positions[i];
                if ( useSoftwareTransform )
                {
                    v = transform * v;
                }
                VertexData[NumVertices * size + 0] = v.X;
                VertexData[NumVertices * size + 1] = v.Y;
                VertexData[NumVertices * size + 2] = zCoord;
                VertexData[NumVertices * size + 3] = material.Color.R;
                VertexData[NumVertices * size + 4] = material.Color.G;
                VertexData[NumVertices * size + 5] = material.Color.B;
                VertexData[NumVertices * size + 6] = material.Color.A;
                if (geom.TexCoords != null)
                {
                    VertexData[NumVertices * size + 7] = geom.TexCoords[i].X;
                    VertexData[NumVertices * size + 8] = geom.TexCoords[i].Y;
                }
                NumVertices++;
            }
        }

        int NumVertices = 0;
        protected float[] VertexData = new float[64000];
        protected List<int> Indices = new List<int>();
        VertexArray VertexArray = new VertexArray();
        VertexBuffer VertexBuffer = new VertexBuffer();
        IndexBuffer IndexBuffer = new IndexBuffer();
    }
}