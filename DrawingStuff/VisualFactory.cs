using System.Linq;
using static DrawingStuff.Material;
using static DrawingStuff.FloatMath;

namespace DrawingStuff
{
    public class VisualFactory
    {
        public static Visual RectVisual(Rect rect, Material material, float z, bool useTransform = true) =>
            new Visual(new Geom(rect.Vertices, RectTexCoords(material), RectIndices(material.Primitive)), material, z, useTransform);
        public static Visual CircleVisual(float radius, Material material, float z, bool useTransform = true) =>
            new Visual(new Geom(CircleVerts(radius), null, CircleIndices(material.Primitive)), material, z, useTransform);


        public static TextureMaterial tableMaterial = new TextureMaterial("table.png");
        public static ColorMaterial blueMaterial = new ColorMaterial(new Color4(1.0f, 0.0f, 0.0f, 0.5f), PrimitiveType.Triangles);
        public static ColorMaterial blueLineMaterial = new ColorMaterial(Color4.Blue, PrimitiveType.Lines);
        public static ColorMaterial blackLineMaterial = new ColorMaterial(Color4.Black, PrimitiveType.Lines);

        public static ColorMaterial greenMaterial = new ColorMaterial(Color4.Green, PrimitiveType.Triangles);


        public static Visual RandomVisual( )
        {
            float randomZ = Rand(1.0f);
            float rand = Rand(5);


            if (rand > 4.0f)
                return RectVisual(new Rect(-0.2f, .2f, -0.2f, .2f), blackLineMaterial, randomZ);
            else if ( rand > 3.0f )
                return RectVisual(new Rect(-0.2f, .2f, -0.2f, .2f), tableMaterial, randomZ);
            else if ( rand > 2.0f )
                return CircleVisual(0.1f + Rand(0.1f), blueMaterial, randomZ);
            else if ( rand > 1.0f )
                return CircleVisual(0.2f, greenMaterial, randomZ);
            else
                return CircleVisual(0.1f + Rand(0.1f), blueLineMaterial, randomZ);
    }

        const int numCircleVerts = 64;

        static Vector2[] CircleVerts(float radius) =>
            new Vector2[numCircleVerts].Select((_, n) => radius * Vector2.FromAngle(2.0f * PI * n / numCircleVerts)).ToArray();

        static readonly int[] RectFillIndices = { 0, 1, 2, 0, 2, 3 };
        static readonly int[] RectLineIndices = { 0, 1, 1, 2, 2, 3, 3, 0 };
        static int[] RectIndices(PrimitiveType primitiveType) => primitiveType == PrimitiveType.Lines ? RectLineIndices : RectFillIndices;

        static int[] CircleFillIndices
        {
            get
            {
                int[] list = new int[(numCircleVerts - 1) * 3];
                for (int i = 0; i < numCircleVerts - 1; i++)
                {
                    list[i * 3] = 0;
                    list[i * 3 + 1] = i;
                    list[i * 3 + 2] = i + 1;
                }
                return list;
            }
        }

        static int[] CircleLineIndices
        {
            get
            {
                int[] list = new int[numCircleVerts * 2];
                for (int i = 0; i < numCircleVerts * 2 - 2; i += 2)
                {
                    list[i + 0] = i / 2;
                    list[i + 1] = i / 2 + 1;
                }
                list[numCircleVerts * 2 - 2] = numCircleVerts - 1;
                list[numCircleVerts * 2 - 1] = 0;
                return list;
            }
        }



        static int[] CircleIndices(PrimitiveType primitiveType) => primitiveType == PrimitiveType.Lines ? CircleLineIndices : CircleFillIndices;

       
        static Vector2[] RectTexCoords() => new Vector2[4] { new Vector2(0, 1), new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1) };
        static Vector2[] RectTexCoords(Material material) => material is TextureMaterial ? RectTexCoords() : null;
    }
}

