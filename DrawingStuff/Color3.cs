using System.Runtime.InteropServices;
using static DrawingStuff.FloatMath;

namespace DrawingStuff
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Color3 
    {
        public float R { get; set; }
        public float G { get; set; }
        public float B { get; set; }

        public Color3(float red, float green, float blue)
        {
            R = red;
            G = green;
            B = blue;
        }

        public void Clamp()
        {
            R = Clamp01(R);
            G = Clamp01(G);
            B = Clamp01(B);
        }

        public override bool Equals(object obj) => obj is Color3 c && this == c;
        public override int GetHashCode() => HashCodeHelper.CombineHashCodes(R.GetHashCode(), B.GetHashCode(), G.GetHashCode());
        public float Intensity => (R + G + B) / 3.0f;
        public override string ToString() => $"Color3({R}, {G}, {B})";      
        public static bool operator ==(Color3 u, Color3 v) => u.R == v.R && u.G == v.G && u.B == v.B;
        public static bool operator !=(Color3 u, Color3 v) => !Equals(u, v);
        public static explicit operator float[] (Color3 color) => new float[3] { color.R, color.G, color.B };
        public static explicit operator Color4(Color3 color) => new Color4(color.R, color.G, color.B, 1.0f);
    }
}
