using OpenTK.Graphics.OpenGL4;

namespace DrawingStuff
{
    public class VertexAttribute
    {
        private readonly int Index;
        private readonly int Size;
        private readonly int Stride;
        private readonly int Offset;
        private readonly VertexAttribPointerType AttributeType;

        public VertexAttribute(int index, int size, VertexAttribPointerType attributeType, int stride, int offset)
        {
            Size = size;
            Index = index;
            AttributeType = attributeType;
            Stride = stride;
            Offset = offset;
        }

        public void SetAttributePointer()
        {
            GL.EnableVertexAttribArray(Index);
            GL.VertexAttribPointer(Index, Size, AttributeType, false, Stride, Offset);
        }
    }
}

