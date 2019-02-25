using System.Collections.Generic;
using System.Linq;

using static DrawingStuff.FloatMath;

namespace DrawingStuff
{
    public class VisualFactory
    {
        public static Visual RectVisual(float width, float height, Color4 color, Material material, float zCoord, bool useTransform = true) =>
            new Visual(
                RectVertices(width, height),
                RectTexCoords(material),
                RectFillIndices,
                color,
                zCoord,
                material,
                useTransform,
                RectBoundingRect(width, height)
                );

        public static Visual CircleVisual(float radius, Color4 color, Material material, float zCoord, bool useTransform = true) =>
            new Visual(
                CircleVertices(radius),
                TexCoords(CircleVertices(radius), CircleBoudingRect(radius), new Rect(1, 101, 1, 201), ResourceManager.GetTexture("table.png")),
                CircleFillIndices,
                color,
                zCoord,
                TextureMaterial("table.png"),
                useTransform,
                CircleBoudingRect(radius));

        public static Visual CapsuleVisual(float length, float radius, Color4 color, Material material, float zCoord) =>
            new Visual(
                CapsuleVertices(length, radius),
                null,
                CapsuleFillIndices,
                color,
                zCoord,
                material,
                true,
                CapsuleBoudingRect(length, radius));


        public static Material FillMaterial = new Material();

        public static Material TextureMaterial(string texture)
        {
            if (!TextureMaterialSet.TryGetValue(texture, out Material material))
            {
                material = new Material(texture);
                TextureMaterialSet.Add(texture, material);
            }
            return material;
        }


        public static Visual RandomVisual()
        {
            float randomZ = Rand(1.0f);
            float rand = Rand(4);
           // if (rand > 3.0f)
          //      return RectVisual(4.0f, 0.4f, new Color4(0.0f, 1.0f, 1.0f, 1.0f), LineMaterial, 0.1f);
         //   if (rand > 2.4f)
                 return RectVisual(4f, .4f, new Color4(1.0f, 0.0f, 0.0f, 1.0f), TextureMaterial("table.png"), 0.2f);
           // if (rand > 2.0f)
          //      return RectVisual(4f, 0.4f, new Color4(0.0f, 1.0f, 0.0f, 1.0f), LineMaterial, 0.3f);
            //if (rand > 1.6f)
             //   return CircleVisual(2f + Rand(1.1f), new Color4(0.3f, 1.0f, 0.4f, 0.3f), FillMaterial, 0.4f);
          //  if (rand > 0.1f)
             //   return CircleVisual(2.1f, new Color4(1.0f, 0.0f, 0.0f, 1.0f), FillMaterial, 0.5f);
            //else//if (rand > .05f)
              //  return CircleVisual(3.1f + Rand(0.1f), new Color4(1.0f, 1.0f, 0.0f, 1.0f), FillMaterial, 0.6f);
          //  else
          //      return RectVisual(2.2f, 1.4f, new Color4(0.0f, 0.0f, 1.0f, 1.0f), TextureMaterial("table.png"), 0.7f);
        }

        private const int numCircleVerts = 64;

        private static Vector2[] CircleVertices(float radius) =>
            new Vector2[numCircleVerts].Select((_, n) => radius * Vector2.FromAngle(2.0f * PI * n / numCircleVerts)).ToArray();

        private static Vector2[] CapsuleVertices(float length, float radius)
        {
            Vector2[] verts = new Vector2[numCircleVerts + 2];
            for (int i = 0; i < numCircleVerts / 2 + 1; i++)
            {
                verts[i] = radius * Vector2.FromAngle(-PI / 2.0f + PI * i / (numCircleVerts / 2)) + new Vector2(length / 2, 0);
            }
            for (int i = 0; i < numCircleVerts / 2 + 1; i++)
            {
                verts[numCircleVerts / 2 + i] = radius * Vector2.FromAngle(PI / 2.0f + PI * i / (numCircleVerts / 2)) + new Vector2(-length / 2, 0);
            }
            return verts;
        }

        private static Vector2[] RectVertices(float w, float h) =>
            new Vector2[] { new Vector2(-w / 2, -h / 2), new Vector2(w / 2, -h / 2), new Vector2(w / 2, h / 2), new Vector2(-w / 2, h / 2) };

       // private static int[] RectIndices(Primitive primitive) => primitive == Primitive.Lines ? RectLineIndices : RectFillIndices;
       // private static int[] CircleIndices(Primitive primitive) => primitive == Primitive.Lines ? CircleLineIndices : CircleFillIndices;
       // private static int[] CapsuleIndices(Primitive primitive) => primitive == Primitive.Lines ? CapsuleLineIndices : CapsuleFillIndices;

        private static readonly int[] RectFillIndices = ConvexFillIndices(4);
        private static readonly int[] RectLineIndices = ConvexLineIndices(4);
        private static readonly int[] CircleFillIndices = ConvexFillIndices(numCircleVerts);
        private static readonly int[] CircleLineIndices = ConvexLineIndices(numCircleVerts);
        private static readonly int[] CapsuleFillIndices = ConvexFillIndices(numCircleVerts + 2);
        private static readonly int[] CapsuleLineIndices = ConvexLineIndices(numCircleVerts + 2);

        private static int[] ConvexFillIndices(int numVerts)
        {
            int[] list = new int[(numVerts - 1) * 3];
            for (int i = 0; i < numVerts - 1; i++)
            {
                list[i * 3] = 0;
                list[i * 3 + 1] = i;
                list[i * 3 + 2] = i + 1;
            }
            return list;
        }

        private static int[] ConvexLineIndices(int numVerts)
        {
            int[] list = new int[numVerts * 2];
            for (int i = 0; i < numVerts * 2 - 2; i += 2)
            {
                list[i + 0] = i / 2;
                list[i + 1] = i / 2 + 1;
            }
            list[numVerts * 2 - 2] = numVerts - 1;
            list[numVerts * 2 - 1] = 0;
            return list;
        }

        public static Dictionary<string, Material> TextureMaterialSet = new Dictionary<string, Material>();


        private static readonly Vector2[] _RectTexCoords = new Vector2[4] { new Vector2(0, 1), new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1) };

        private static Vector2[] RectTexCoords(Material material) => material.Texture != null ? _RectTexCoords : null;


        static Rect RectBoundingRect(float w, float h)
        {
            float r = Sqrt(w * w / 4 + h * h / 4);
            return new Rect(Vector2.Zero, 2 * r, 2 * r);
        }

        static Rect CircleBoudingRect(float r) => new Rect(Vector2.Zero, 2 * r, 2 * r);

        static Rect CapsuleBoudingRect(float l, float r) => new Rect(Vector2.Zero, 2 * (l + r), 2 * (l + r));

        static Rect LineBoudingRect(Vector2 a, Vector2 b)
        {
            float xMin, xMax, yMin, yMax;
            if (a.X < b.X)
            {
                xMin = a.X;
                xMax = b.X;
            }
            else
            {
                xMin = b.X;
                xMax = a.X;
            }
            if (a.Y < b.Y)
            {
                yMin = a.Y;
                yMax = b.Y;
            }
            else
            {
                yMin = b.Y;
                yMax = a.Y;
            }
            return new Rect(xMin, xMax, yMin, yMax);
        }




        static Vector2[] TexCoords(Vector2[] vertices, Rect localTextureRect, Rect textureSubRect, Texture2D texture)
        {
            Vector2[] texCoords = new Vector2[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                texCoords[i].X = (textureSubRect.Left + textureSubRect.Width * (vertices[i].X - localTextureRect.Left) / localTextureRect.Width ) / texture.Width;
                texCoords[i].Y = (textureSubRect.Bottom + textureSubRect.Height * (vertices[i].Y - localTextureRect.Bottom) / localTextureRect.Height ) / texture.Height;
            }
            return texCoords;
        }

       
    }
}

