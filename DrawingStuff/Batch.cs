using OpenTK.Graphics.OpenGL4;
using System;


namespace DrawingStuff
{
    public class Batch 
    {
        public Batch(int maxVertices)
        {
            MaxVertices = maxVertices;
            VertexData = new float[MaxVertices];
            IndexData = new int[maxVertices * 3];
            NumVertices = 0;
            NumIndices = 0;
         }

        public void Begin(Material material, XForm worldToDevice)
        {
            BatchBegan = true;
            Material = material;
            WorldToDevice = worldToDevice;
        }

        void Flush()
        {
            if (NumVertices == 0)
                return;
            BindBuffers();
            VertexBuffer.SetData(VertexData, BufferUsageHint.StreamDraw);
            IndexBuffer.SetData(IndexData, BufferUsageHint.StreamDraw);
            Material.Begin(WorldToDevice);
            GL.DrawElements(BeginMode(Material.Primitive), NumIndices, DrawElementsType.UnsignedInt, 0);
            Material.End();
            UnBindBuffers();
            NumIndices = 0;
            NumVertices = 0;
        }

        BeginMode BeginMode(Material.PrimitiveType primitiveType) => 
            primitiveType == Material.PrimitiveType.Lines ? OpenTK.Graphics.OpenGL4.BeginMode.Lines : OpenTK.Graphics.OpenGL4.BeginMode.Triangles;
        
        public void End()
        {
            if (!BatchBegan) throw new Exception("Call Batch Begin");
            BatchBegan = false;
            Flush();
        }
        

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


        public void AddGeom(Geom geom, float z, XForm transform, bool useSoftwareTransform)
        {
            int size = Material.VertexDeclaration.Size;

            if (!BatchBegan) throw new Exception("Call Batch Begin");
            
            if (size * ( NumVertices + geom.Positions.Length ) > MaxVertices )
            {
                Flush();
            }

            int oldNumIndices = NumIndices;
            NumIndices = oldNumIndices + geom.Indices.Length;
            for ( int i = 0; i < geom.Indices.Length; i++ )
            {
                IndexData[oldNumIndices + i] = NumVertices + geom.Indices[i];
            }

            for (int i = 0; i < geom.Positions.Length; i++)
            {
                Vector2 v = useSoftwareTransform ? transform * geom.Positions[i] :  geom.Positions[i];
        
                VertexData[NumVertices * size + 0] = v.X;
                VertexData[NumVertices * size + 1] = v.Y;
                VertexData[NumVertices * size + 2] = z;
                VertexData[NumVertices * size + 3] = Material.Color.R;
                VertexData[NumVertices * size + 4] = Material.Color.G;
                VertexData[NumVertices * size + 5] = Material.Color.B;
                VertexData[NumVertices * size + 6] = Material.Color.A;
                if (geom.TexCoords != null)
                {
                    VertexData[NumVertices * size + 7] = geom.TexCoords[i].X;
                    VertexData[NumVertices * size + 8] = geom.TexCoords[i].Y;
                }
                NumVertices++;
            }
        }

        bool BatchBegan = false;

        Material Material = null;
        XForm WorldToDevice;

        int NumVertices = 0;
        int NumIndices;
        float[] VertexData;
        int[] IndexData;
        public int MaxVertices { get; private set; }

        VertexArray VertexArray = new VertexArray();
        VertexBuffer VertexBuffer = new VertexBuffer();
        IndexBuffer IndexBuffer = new IndexBuffer();    }
}