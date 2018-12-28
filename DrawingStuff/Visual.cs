namespace DrawingStuff
{
    public class Visual
    {
        public Visual(Geom geom, Material material, float zCoord, bool useTransform)
        {
            Geom = geom;
            Material = material;
            ZCoord = zCoord;
            UseTransform = useTransform;
        }

        public bool UseTransform;
        public XForm Transform = XForm.Identity;
        public Material Material;
        public Geom Geom;
        public float ZCoord;
    }
}
