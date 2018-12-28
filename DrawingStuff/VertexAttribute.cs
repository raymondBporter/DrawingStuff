using OpenTK.Graphics.OpenGL4;

namespace DrawingStuff
{
    public class VertexAttribute
    {
        readonly int Index;
        readonly int Size;
        readonly int Stride;
        readonly int Offset;
        readonly VertexAttribPointerType AttributeType;

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

