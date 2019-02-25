using System.Collections.Generic;
using System.Linq;
using static System.Diagnostics.Debug;
using static DrawingStuff.FloatMath;

namespace DrawingStuff
{
    public struct Vector
    {

        public Vector(int n) => arr = new float[n];

        public static Vector Zero(int n)
        {
            Vector v = new Vector(n);
            for (int i = 0; i < n; i++)
            {
                v[i] = 0;
            }
            return v;
        }


        public void Zero()
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = 0;
            }
        }

        public static Vector One(int n)
        {
            Vector v = new Vector(n);
            for (int i = 0; i < n; i++)
            {
                v[i] = 1;
            }
            return v;
        }

        public int Dim => arr.Length;


        public static float Dot(Vector a, Vector b)
        {
            Assert(a.Dim == b.Dim);
            float sum = 0;
            for (int i = 0; i < a.Dim; i++)
            {
                sum += a[i] * b[i];
            }
            return sum;
        }

        public static float DistanceSq(Vector a, Vector b)
        {
            Assert(a.Dim == b.Dim);
            float sum = 0;
            for (int i = 0; i < a.Dim; i++)
            {
                float k = a[i] - b[i];
                sum += k * k;
            }
            return sum;
        }


        public float LengthSq => Dot(this, this);
        public float Length => Sqrt(Dot(this, this));


        public float this[int i]
        {
            get => arr[i];
            set => arr[i] = value;
        }


        public float Avg
        {
            get
            {
                float sum = 0;
                foreach (float f in arr)
                {
                    sum += f;
                }
                return sum / Dim;
            }
        }

        public static Vector operator -(Vector a, Vector b)
        {
            Assert(a.Dim == b.Dim);
            Vector c = new Vector(a.Dim);
            for (int i = 0; i < c.Length; i++)
            {
                c[i] = a[i] - b[i];
            }
            return c;
        }

        public static Vector operator +(Vector a, Vector b)
        {
            Assert(a.Dim == b.Dim);
            Vector c = new Vector(a.Dim);
            for (int i = 0; i < c.Dim; i++)
            {

                c[i] = a[i] + b[i];
            }
            return c;
        }


        public static void Add(Vector a, Vector b, ref Vector result)
        {
            Assert(a.Dim == b.Dim && a.Dim == result.Dim);
            for (int i = 0; i < a.Dim; i++)
            {
                result[i] = a[i] + b[i];
            }
        }

        public static Vector operator /(Vector a, float f)
        {
            Vector c = new Vector(a.Dim);
            for (int i = 0; i < c.Dim; i++)
            {
                c[i] = a[i] / f;
            }
            return c;
        }


        public void Div(float f)
        {
            for (int i = 0; i < Dim; i++)
            {
                this[i] = this[i] / f;
            }
        }

        /*
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


        public static Vector2 operator -(Vector2 v) => new Vector2(-v.X, -v.Y);
        public static bool operator ==(Vector2 u, Vector2 v) => u.X == v.X && u.Y == v.Y;
        public static bool operator !=(Vector2 u, Vector2 v) => !(u == v);
        public static Vector2 operator +(Vector2 u, Vector2 v) => new Vector2(u.X + v.X, u.Y + v.Y);
        public static Vector2 operator -(Vector2 u, Vector2 v) => new Vector2(u.X - v.X, u.Y - v.Y);
        public static Vector2 operator *(float s, Vector2 v) => new Vector2(s * v.X, s * v.Y);
        public static Vector2 operator *(Vector2 v, float s) => new Vector2(s * v.X, s * v.Y);
        public static Vector2 operator /(Vector2 v, float s) => new Vector2(v.X / s, v.Y / s);
        public static explicit operator float[] (Vector2 v) => new float[2] { v.X, v.Y };
        */
        private float[] arr;
    }

    internal class VectorFunc
    {
        private static float Avg(float[] a)
        {
            float sum = 0;
            foreach (float f in a)
            {
                sum += f;
            }
            return sum / a.Length;
        }

        private static float Dot(float[] a, float[] b)
        {
            Assert(a.Length == b.Length);
            float sum = 0;
            for (int i = 0; i < a.Length; i++)
            {
                sum += a[i] * b[i];
            }
            return sum;
        }

        private static float LengthSq(float[] a) => Dot(a, a);
        private static float Length(float[] a) => Sqrt(LengthSq(a));

        private static float Std(float[] a)
        {
            float sum = 0;
            float avg = Avg(a);
            foreach (float f in a)// ( int i = 0; i < a.Length; i++ )
            {
                float k = f - avg;
                sum += k * k;
            }
            return Sqrt(sum / a.Length);
        }
        private static float RmsSq(float[] a) => Square(Avg(a)) + Square(Std(a));
        private static float Square(float f) => f * f;
    }

    internal class KMeans
    {
        private readonly int Dim;                     // The dimension of the vectors
        private readonly int NumGroups;               // The number of partitions
        private readonly int[] GroupNumbers;          // Which group the ith vector belongs too
        private readonly int[] GroupSizes;            // How many vectors are in the ith group
        private readonly Vector[] Representatives;    // Representative vector for each group
        private readonly Vector[] Vectors;            // The vectors to partition
        private readonly int NumVectors;              // The count of the vectors to partition
        private readonly int NumIterations;           // How many times we iterate the KMeans algorithm

        public KMeans(Vector[] vecs, int numGroups, int numIterations)
        {
            Assert(vecs.Length >= numGroups);
            Dim = vecs[0].Dim;
            NumGroups = numGroups;
            Vectors = vecs;
            NumVectors = vecs.Length;
            NumIterations = numIterations;
            GroupNumbers = new int[NumVectors];
            GroupSizes = new int[NumGroups];
            Representatives = new Vector[NumGroups];
            for (int i = 0; i < NumGroups; i++)
            {
                Representatives[i] = new Vector(Dim);
            }
            InitializeRepresentatives();
            InitializeGroupNumbers();
        }


        public int[] FindGroups( )
        {
            for (int i = 0; i < NumIterations; i++)
            {
                PartitionVectors();
                UpdateRepresentatives();
            }
            return GroupNumbers;
        }



        private void InitializeGroupNumbers()
        {
            for (int i = 0; i < NumVectors; i++)
            {
                GroupNumbers[i] = Random.Next(NumGroups);
            }
        }


        private void InitializeRepresentatives()
        {
            for (int i = 0; i < NumGroups; i++)
            {
                int randVectorIndex = Random.Next(NumVectors);

                for (int j = 0; j < Vectors[i].Dim; j++)
                {
                    Representatives[i][j] = Vectors[randVectorIndex][j];
                }
            }
        }

        private void PartitionVectors()
        {
            for (int i = 0; i < NumVectors; i++)
            {
                float min = Vector.DistanceSq(Vectors[i], Representatives[0]);
                int minIndex = 0;
                for (int j = 1; j < NumGroups; j++)
                {
                    float f = Vector.DistanceSq(Vectors[i], Representatives[j]);
                    if (f < min)
                    {
                        min = f;
                        minIndex = j;
                    }
                }

                GroupNumbers[i] = minIndex;
            }
        }

        private void UpdateRepresentatives()
        {
            ZeroArray(GroupSizes);
            foreach (Vector r in Representatives)
            {
                r.Zero();
            }
 
            for (int i = 0; i < NumVectors; i++)
            {
                int index = GroupNumbers[i];
                GroupSizes[index]++;
                Vector.Add(Representatives[index], Vectors[i], ref Representatives[index]);
            }

            for (int i = 0; i < NumGroups; i++)
            {
                if (GroupSizes[i] != 0)
                {
                    Representatives[i].Div(GroupSizes[i]);
                }
            }
        }

        private void ZeroArray(int[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = 0;
            }
        }
    };
}
