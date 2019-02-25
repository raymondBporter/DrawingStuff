using System.Collections.Generic;

namespace DrawingStuff
{
    public class Visual
    {
        public Visual(Vector2[] positions, Vector2[] texCoords, int[] indices, Color4 color, float zCoord, Material material, bool useSoftwareTransform, Rect boudingRect)
        {
            Positions = positions;
            TexCoords = texCoords;
            Indices = indices;
            Color = color;
            ZCoord = zCoord;
            Material = material;
            UseTransform = useSoftwareTransform;
            BoundingRect = boudingRect;
        }

        public Visual(Vector2[] positions, int[] indices, Color4 color, float zCoord, Material material, bool useSoftwareTransform, Rect boudingRect) 
            : this(positions, null, indices, color, zCoord, material, useSoftwareTransform, boudingRect) { }

        public Vector2[] Positions = null;
        public Vector2[] TexCoords = null;
        public int[] Indices = null;
        public float ZCoord;
        public Color4 Color;
        public bool UseTransform;
        public XForm Transform = XForm.Identity;
        public Material Material;
        public readonly Rect BoundingRect;
        public Rect WorldBoundingRect;
        public int ContactGroup = -1;
        public List<Visual> Contacts = new List<Visual>();
    }
}

