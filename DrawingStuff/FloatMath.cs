using System;
using System.Diagnostics;

namespace DrawingStuff
{
    internal static class FloatMath
    {
        static public Random Random = new Random();

        static public float Sin(float x) => (float)Math.Sin(x);
        static public float Cos(float x) => (float)Math.Cos(x);
        static public float Atan2(float s, float c) => (float)Math.Atan2(s, c);
        public static float Pow(float a, float b) => (float)Math.Pow(a, b);
        public static float Pow(float a, int b) => (float)Math.Pow(a, b);
        static public float Sqrt(float x) => (float)Math.Sqrt(x);
        static public float Exp(float x) => (float)Math.Exp(x);
        static public float Abs(float x) => Math.Abs(x);
        static public float PI => (float)Math.PI;
        public static float Min(float a, float b) => (a > b) ? a : b;     
        public static float Max(float a, float b) => (a < b) ? a : b;
        public static int   Min(int a, int b) => (a > b) ? a : b;
        public static int   Max(int a, int b) => (a < b) ? a : b;
        public static float Fabs(float f) => (f < 0) ? -f : f;
        static public float Clamp(float value, float min, float max) => value < min ? min : value > max ? max : value;
        static public float Clamp01(float value) => Clamp(value, 0, 1);

        static public float Rand(float max) => (float)Random.NextDouble() * max;
        static public Vector2 RandomUnitVector => Vector2.FromAngle(RandomAngle);
        static public float RandomAngle => Rand(2.0f * PI);


        public static void Swap<T>(ref T a, ref T b)
        {
            T tmp = a;
            a = b;
            b = tmp;
        }

        static public float Min(float[] a)
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


        static public float Max(float[] a)
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
            static public Vector2 ClampCircleInsideRect(Vector2 cPos, float r, Rect rect) =>
                new Vector2(Clamp(cPos.X, rect.Left + r, rect.Right - r), Clamp(cPos.Y, rect.Bottom + r, rect.Top - r));   
        }
    }
}
