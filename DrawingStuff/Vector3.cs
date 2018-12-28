using System.Runtime.InteropServices;
using static DrawingStuff.FloatMath;

namespace DrawingStuff
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3
    {
        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3(Vector2 xy, float z) : this(xy.X, xy.Y, z) { }

        public static readonly Vector3 Zero = new Vector3(0.0f, 0.0f, 0.0f);
        public static readonly Vector3 XAxis = new Vector3(1.0f, 0.0f, 0.0f);
        public static readonly Vector3 YAxis = new Vector3(0.0f, 1.0f, 0.0f);
        public static readonly Vector3 ZAxis = new Vector3(0.0f, 0.0f, 1.0f);

        public void Normalize() => this = Normalized;
        public Vector3 Normalized => (LengthSq < 1e-15f) ? Zero : this / Length;
        public static float Dot(Vector3 u, Vector3 v) => u.X * v.X + u.Y * v.Y + u.Z * v.Z;
        public float Length => Sqrt(X * X + Y * Y + Z * Z);
        public float LengthSq => X * X + Y * Y + Z * Z;
        public static Vector3 operator -(Vector3 v) => new Vector3(-v.X, -v.Y, -v.Z);
        public static Vector3 operator +(Vector3 u, Vector3 v) => new Vector3(u.X + v.X, u.Y + v.Y, u.Z + v.Z);
        public static Vector3 operator -(Vector3 u, Vector3 v) => new Vector3(u.X - v.X, u.Y - v.Y, u.Z - v.Z);
        public static Vector3 operator *(float s, Vector3 v) => new Vector3(s * v.X, s * v.Y, s * v.Z);
        public static Vector3 operator *(Vector3 v, float s) => new Vector3(s * v.X, s * v.Y, s * v.Z);
        public static Vector3 operator /(Vector3 v, float s) => new Vector3(v.X / s, v.Y / s, v.Z / s);
        public Vector2 XY => new Vector2(X, Y);
        public static explicit operator float[] (Vector3 v) => new float[3] { v.X, v.Y, v.Z };

        public float X;
        public float Y;
        public float Z;
    }
}
