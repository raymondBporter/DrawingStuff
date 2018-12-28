using System;
using System.Diagnostics;

namespace DrawingStuff
{
    static class FloatMath
    {
        static public Random Random = new Random();
        static public float Sin(float x) => (float)Math.Sin(x);
        static public float Cos(float x) => (float)Math.Cos(x);
        static public float Atan2(float s, float c) => (float)Math.Atan2(s, c);

        static public float Sqrt(float x) => (float)Math.Sqrt(x);
        static public float Exp(float x) => (float)Math.Exp(x);
        static public float Abs(float x) => (float)Math.Abs(x);
        static public float PI => (float)Math.PI;

        static public float Rand(float max) => (float)Random.NextDouble() * max;
      
        static public Vector2 RandomUnitVector => new Vector2(Cos(RandomAngle), Sin(RandomAngle));
        static float RandomAngle => Rand(2.0f * PI);

        static public float Min(params float[] a)
        {
            Debug.Assert(a != null || a.Length != 0);
            float min = a[0];
            for ( int i = 1; i < a.Length; i++ )
            {
                if (a[i] < min)
                    min = a[i];
            }
            return min;
        }


        static public float Max(params float[] a)
        {
            Debug.Assert(a != null || a.Length != 0);
            float max = a[0];
            for (int i = 1; i < a.Length; i++)
            {
                if (a[i] > max)
                    max = a[i];
            }
            return max;
        }


        public static class Clamper
        {
            static public float Clamp(float value, float min, float max) => value < min ? min : value > max ? max : value;

            static public Vector2 ClampCircleInsideRect(Vector2 cPos, float r, Rect rect) =>
                new Vector2(Clamp(cPos.X, rect.Left + r, rect.Right - r), Clamp(cPos.Y, rect.Bottom + r, rect.Top - r));
            
        }
    }
}
