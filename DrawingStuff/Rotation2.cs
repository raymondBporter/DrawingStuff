using static DrawingStuff.FloatMath;

namespace DrawingStuff
{
    public struct Rotation2
    {
        public Rotation2(float Cos, float Sin) { c = Cos; s = Sin; }
        public Rotation2(float angle) : this(Cos(angle), Sin(angle)) {}    

        public static Rotation2 Identity => new Rotation2(1.0f, 0.0f);

        public static Vector2 operator * (Rotation2 R, Vector2 v) => new Vector2(v.X * R.c - v.Y * R.s, v.X * R.s + v.Y * R.c);
        public static Rotation2 operator * (Rotation2 a, Rotation2 b) => new Rotation2(a.c * b.c - a.s * b.s, a.s * b.c + a.c * b.s);
        public static Rotation2 operator - (Rotation2 r) => new Rotation2(-r.c, -r.s);
        public float Angle() => Atan2(s, c);

        float c, s;
    }
}
