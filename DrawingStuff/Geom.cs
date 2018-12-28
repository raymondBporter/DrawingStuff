namespace DrawingStuff
{
    public class Geom
    {
        public Geom(Vector2[] positions, Vector2[] texCoords, int[] indices)
        {
            Positions = positions;
            TexCoords = texCoords;
            Indices = indices;
        }

        public Vector2[] Positions = null;
        public Vector2[] TexCoords = null;
        public int[] Indices = null;
    }
}

