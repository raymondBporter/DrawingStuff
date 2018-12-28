using static DrawingStuff.FloatMath;
using static DrawingStuff.Vector2;

namespace DrawingStuff
{
    public struct Matrix2
    {
        public static readonly Matrix2 Identity = new Matrix2(1.0f, 0.0f, 0.0f, 1.0f);
        public static readonly Matrix2 Zero = new Matrix2(Vector2.Zero, Vector2.Zero);

        public Vector2 Row0;
        public Vector2 Row1;
        public Vector2 Col0 => new Vector2(Row0.X, Row1.X);
        public Vector2 Col1 => new Vector2(Row0.Y, Row1.Y);

        public Matrix2(Vector2 row0, Vector2 row1) { Row0 = row0; Row1 = row1; }

        public Matrix2(float m00, float m01, float m10, float m11) : this(new Vector2(m00, m01), new Vector2(m10, m11)){ }
        

        public static Matrix2 Rotation(float angle) => new Matrix2(Cos(angle), -Sin(angle), Sin(angle), Cos(angle));
        public static Matrix2 Scale(float s) => new Matrix2(s, 0.0f, 0.0f, s);
        public static Matrix2 Scale(float sx, float sy) => new Matrix2(sx, 0.0f, 0.0f, sy);
        public static Matrix2 ReflectionX => new Matrix2(-1.0f, 0.0f, 0.0f, 1.0f);
        public static Matrix2 ReflectionY => new Matrix2(1.0f, 0.0f, 0.0f, -1.0f);
        public static Matrix2 Transpose(Matrix2 m) => new Matrix2(m.Col0, m.Col1);
        
        public static Matrix2 operator * (Matrix2 l, Matrix2 r) => new Matrix2(Dot(l.Row0, r.Col0), Dot(l.Row0, r.Col1), Dot(l.Row1, r.Col0), Dot(l.Row1, r.Col1));
        public static Vector2 operator * (Matrix2 m, Vector2 v) => new Vector2(Dot(m.Row0, v), Dot(m.Row1, v));
        public static Matrix2 operator * (float f, Matrix2 m) => new Matrix2(f * m.Row0, f * m.Row1);
        public static explicit operator float[] (Matrix2 m) => new float[4] { m.Row0.X, m.Row0.Y, m.Row1.X, m.Row1.Y };
    }



}