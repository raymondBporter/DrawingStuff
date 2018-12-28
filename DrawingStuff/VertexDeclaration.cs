using OpenTK.Graphics.OpenGL4;

namespace DrawingStuff
{
    public class VertexDeclaration
    {
        public VertexDeclaration(VertexAttribute[] attributes, int size)
        {
            Size = size;
            Attributes = attributes;
        }

        public void SetAttributePointers()
        {
            foreach (VertexAttribute attribute in Attributes)
                attribute.SetAttributePointer();
        }

        public static VertexDeclaration ColoredVertex => new VertexDeclaration(new VertexAttribute[2]
        {
            new VertexAttribute(0, 3, VertexAttribPointerType.Float, sizeof(float) * 7, 0),                 //x, y, z
            new VertexAttribute(1, 4, VertexAttribPointerType.Float, sizeof(float) * 7, 3 * sizeof(float))  //r, g, b, a
        }, 7);

       
        public static VertexDeclaration TexturedVertex => new VertexDeclaration(new VertexAttribute[3]
        {    
            new VertexAttribute(0, 3, VertexAttribPointerType.Float, sizeof(float) * 9, 0),                 //x, y, z                
            new VertexAttribute(1, 4, VertexAttribPointerType.Float, sizeof(float) * 9, 3 * sizeof(float)), //r, g, b, a 
            new VertexAttribute(2, 2, VertexAttribPointerType.Float, sizeof(float) * 9, 7 * sizeof(float))  //u, v
        }, 9);

        public readonly VertexAttribute[] Attributes;
        public int Size;
    }
}

