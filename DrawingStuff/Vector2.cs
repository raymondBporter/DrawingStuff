using System.Runtime.InteropServices;
using static DrawingStuff.FloatMath;

namespace DrawingStuff
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2 
	{
        public Vector2(float x, float y) { X = x; Y = y; }

        public static readonly Vector2 Zero	= new Vector2(0.0f, 0.0f);
		public static readonly Vector2 XAxis = new Vector2(1.0f, 0.0f);
		public static readonly Vector2 YAxis = new Vector2(0.0f, 1.0f);

        public static Vector2 FromAngle(float radians) => new Vector2(Cos(radians), Sin(radians));
        public static float Dot(Vector2 u, Vector2 v) => u.X * v.X + u.Y * v.Y;
        public static float Cross(Vector2 u, Vector2 v) => u.X * v.Y - u.Y * v.X;

        public static float Distance(Vector2 u, Vector2 v) => (u - v).Length;
        

        public static float DistanceSq(Vector2 u, Vector2 v) =>(u - v).LengthSq;
        

        public float Length => Sqrt(X * X + Y * Y); 
        public float LengthSq => X * X + Y * Y;
        public Vector2 Normalized => (LengthSq < 1e-15f) ? Zero : this / Length;
        public void Normalize() => this = Normalized;
        /// Clamp v to length len.
        public Vector2 Clamped(float maxLen) => (Dot(this, this) > maxLen * maxLen) ? Normalized * maxLen : this;
        
        public override bool Equals(object obj) => obj is Vector2 v && this == v;
        public override int GetHashCode() => HashCodeHelper.CombineHashCodes(X.GetHashCode(), Y.GetHashCode());
        public Vector2 PerpCCW => new Vector2(-Y, X);
        public static Vector2 Lerp(Vector2 v1, Vector2 v2, float t) => v1 * (1.0f - t) + v2 * t;
 

        public static Vector2 operator-(Vector2 v) => new Vector2(-v.X, -v.Y);
        public static bool operator ==(Vector2 u, Vector2 v) => u.X == v.X && u.Y == v.Y;
        public static bool operator !=(Vector2 u, Vector2 v) => !(u == v);
        public static Vector2 operator +(Vector2 u, Vector2 v) => new Vector2(u.X + v.X, u.Y + v.Y);       
        public static Vector2 operator-(Vector2 u, Vector2 v) => new Vector2(u.X - v.X, u.Y - v.Y);
        public static Vector2 operator *(float s, Vector2 v) => new Vector2(s * v.X, s * v.Y);
        public static Vector2 operator*(Vector2 v, float s) => new Vector2(s * v.X, s * v.Y);
        public static Vector2 operator/(Vector2 v, float s) => new Vector2(v.X / s, v.Y / s);
		public static explicit operator float[](Vector2 v) => new float[2] { v.X, v.Y };

        public float X;
        public float Y;
    }
}
