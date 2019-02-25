using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

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

        public void SetWorldToDevice(XForm transform)
        {
            MatrixFloats[0] = transform.A.Row0.X;
            MatrixFloats[1] = transform.A.Row0.Y;
            MatrixFloats[2] = transform.d.X;
            MatrixFloats[3] = transform.A.Row1.X;
            MatrixFloats[4] = transform.A.Row1.Y;
            MatrixFloats[5] = transform.d.Y;
            MatrixFloats[6] = 0.0f;
            MatrixFloats[7] = 0.0f;
            MatrixFloats[8] = 1.0f;
        }

        public void Begin(Material material)
        {
            BatchBegan = true;
            CurrentMaterial = material;
            BindBuffers();
            CurrentMaterial.Begin(MatrixFloats);
        }

        private void Flush()
        {
            if (NumVertices == 0)
                return;
            VertexBuffer.SetData(VertexData, BufferUsageHint.StreamDraw);
            IndexBuffer.SetData(IndexData, BufferUsageHint.StreamDraw);
            GL.DrawElements(BeginMode.Triangles, NumIndices, DrawElementsType.UnsignedInt, 0);
            NumIndices = 0;
            NumVertices = 0;
        }
        
        public void End()
        {
            if (!BatchBegan)
                throw new System.Exception("Call Batch Begin");
            Flush();
            CurrentMaterial.End();
            UnBindBuffers();
            BatchBegan = false;
        }

        private void BindBuffers()
        {
            VertexArray.Bind();
            VertexBuffer.Bind();
            IndexBuffer.Bind();
        }

        private void UnBindBuffers()
        {
            IndexBuffer.UnBind();
            VertexBuffer.UnBind();
            VertexArray.UnBind();
        }


        public void AddGeom(Visual geom, Material material,  XForm transform, bool useSoftwareTransform)
        {
            if (!BatchBegan)
                throw new System.Exception("Call Batch Begin");

            int size = material.VertexDeclaration.Size;

            if (size * ( NumVertices + geom.Positions.Length ) > MaxVertices )
            {
                Flush();
            }

            for (int i = 0; i < geom.Indices.Length; i++)
            {
                IndexData[NumIndices + i] = NumVertices + geom.Indices[i];
            }
            NumIndices += geom.Indices.Length;

            for (int i = 0; i < geom.Positions.Length; i++)
            {
                Vector2 pos;
                if (useSoftwareTransform)
                {
                    pos.X = transform.A.Row0.X * geom.Positions[i].X + transform.A.Row1.X * geom.Positions[i].Y + transform.d.X;
                    pos.Y = transform.A.Row0.Y * geom.Positions[i].X + transform.A.Row1.Y * geom.Positions[i].Y + transform.d.Y;
                }
                else
                {
                    pos = geom.Positions[i];
                }
                VertexData[NumVertices * size + 0] = pos.X;
                VertexData[NumVertices * size + 1] = pos.Y;
                VertexData[NumVertices * size + 2] = geom.ZCoord;
                VertexData[NumVertices * size + 3] = geom.Color.R;
                VertexData[NumVertices * size + 4] = geom.Color.G;
                VertexData[NumVertices * size + 5] = geom.Color.B;
                VertexData[NumVertices * size + 6] = geom.Color.A;

                if (geom.TexCoords != null)
                {
                    VertexData[NumVertices * size + 7] = geom.TexCoords[i].X;
                    VertexData[NumVertices * size + 8] = geom.TexCoords[i].Y;
                }
                NumVertices++;
            }
        }

        public void AddLine(Vector2 a, Vector2 b, Color4 color)
        {
            DebugLineBufferIndices.Add(DebugLineBufferVertices.Count / 7);
            DebugLineBufferIndices.Add(DebugLineBufferVertices.Count/7 + 1);

            DebugLineBufferVertices.AddRange(new float[] 
                {
                    a.X, a.Y, 0.4f, color.R, color.G, color.B, color.A,
                    b.X, b.Y, 0.4f, color.R, color.G, color.B, color.A
                } );
        }

        public void DrawRect(Rect rect, Color4 color)
        {
            Vector2[] verts = rect.Vertices;
            AddLine(verts[0], verts[1], color);
            AddLine(verts[1], verts[2], color);
            AddLine(verts[2], verts[3], color);
            AddLine(verts[3], verts[0], color);
        }


        public void DrawDebugLines()
        {
            BindBuffers();
            VisualFactory.FillMaterial.Begin(MatrixFloats);
            VertexBuffer.SetData(DebugLineBufferVertices.ToArray(), BufferUsageHint.StreamDraw);
            IndexBuffer.SetData(DebugLineBufferIndices.ToArray(), BufferUsageHint.StreamDraw);
            GL.DrawElements(BeginMode.Lines, DebugLineBufferIndices.Count, DrawElementsType.UnsignedInt, 0);
            VisualFactory.FillMaterial.End();
            UnBindBuffers();
            DebugLineBufferVertices.Clear();
            DebugLineBufferIndices.Clear();
        }


        private bool BatchBegan = false;
        private Material CurrentMaterial = null;
        private readonly float[] MatrixFloats = new float[9];
        private int NumVertices = 0;
        private int NumIndices;
        private readonly float[] VertexData;
        private readonly int[] IndexData;
        public int MaxVertices { get; private set; }


        private readonly List<float> DebugLineBufferVertices = new List<float>();
        private readonly List<int> DebugLineBufferIndices = new List<int>();


        private VertexArray VertexArray = new VertexArray();
        private VertexBuffer VertexBuffer = new VertexBuffer();
        private IndexBuffer IndexBuffer = new IndexBuffer();
    }
}