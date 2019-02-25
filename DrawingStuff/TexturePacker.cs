using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

namespace DrawingStuff
{

    public class PackedTexture
    {
        public PackedTexture(Texture2D texture, Dictionary<string, Rect> subRects)
        {
            Texture = texture;
            SubRects = subRects;
        }

        public readonly Texture2D Texture;
        public readonly Dictionary<string, Rect> SubRects = new Dictionary<string, Rect>();
    }

    public class TexturePacker
    {
        internal class TextureToPack
        {
            public TextureToPack(string name, Bitmap bitmap)
            {
                Name = name;
                Bitmap = bitmap;
            }

            public string Name;
            public Bitmap Bitmap;
            public int Width => Bitmap.Width;
            public int Height => Bitmap.Height;
            public Rectangle BitmapRect => new Rectangle(0, 0, Width, Height);
            public Rect OutputRect;
        }

        public static PackedTexture PackTextures(List<string> textures, int width, int height)
        {
            List<TextureToPack> texturesToPack = new List<TextureToPack>(textures.Count);

            for (int i = 0; i < textures.Count; i++)
            {
                texturesToPack.Add(new TextureToPack(textures[i], new Bitmap(ResourceManager.TexturesPath + textures[i])));
            }
            
            Debug.Assert(PackRects(texturesToPack, width, height));

            int[] bin = new int[width * height];

            foreach (var texture in texturesToPack)
            {
                BitmapData data = texture.Bitmap.LockBits(texture.BitmapRect, ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);
                Rect subRect = texture.OutputRect;
                int x = 0, y = 0;
                for (int i = 0; i < data.Width * data.Height; i++, x++)
                {
                    int pixel = (data.Scan0 + i).ToInt32();
                    int i0 = (int)subRect.Left + width * (int)subRect.Bottom;
                    if (x > subRect.Right)
                    {
                        x = 0;
                        y++;
                    }
                    bin[i0 + x + y * width] = 0x777777;// pixel;
                }
                height = y + 1; // shrink packed texture height to minimum needed
                texture.Bitmap.UnlockBits(data);
            }

            Dictionary<string, Rect> outputRects = new Dictionary<string, Rect>();
            foreach (var output in texturesToPack)
            {
                outputRects.Add(output.Name, output.OutputRect);
            }
            return new PackedTexture(ResourceManager.GenerateTexture(bin, width, height), outputRects);
        }

        static bool PackRects(List<TextureToPack> rects, int packWidth, int packHeight)
        {
            float boundarySize = 1.0f;

            if (rects.Count == 0)
            {
                return false;
            }

            rects.Sort((x, y) => -Comparer<float>.Default.Compare(x.Height, y.Height));

            rects[0].OutputRect = new Rect(boundarySize, rects[0].Width + boundarySize, 
                                           boundarySize, rects[0].Height + boundarySize);
            float bottom = boundarySize;
            float nextBottom = rects[0].Height + 2 * boundarySize;
            float scanX = rects[0].Width + 2 * boundarySize;

            for (int i = 1; i < rects.Count; i++)
            {
                if (scanX + rects[i].Width + boundarySize < packWidth)
                {
                    rects[i].OutputRect = new Rect(scanX, scanX + rects[i].Width, bottom, bottom + rects[i].Height);
                    scanX += rects[i].Width + boundarySize;
                }
                else
                {
                    bottom = nextBottom;
                    nextBottom = bottom + rects[i].Height + boundarySize;
                    scanX = rects[i].Width + 2 * boundarySize;
                    if (bottom + rects[i].Height + boundarySize >= packHeight)
                        return false;
                    rects[i].OutputRect = new Rect(boundarySize, rects[i].Width + boundarySize, bottom, bottom + rects[i].Height);
                }
            }
            return true;
        }
    }
}
