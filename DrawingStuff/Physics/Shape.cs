/*
using System;
using static DrawingStuff.Vector2;
using static DrawingStuff.FloatMath;

namespace DrawingStuff
{
    public class Shape
    {
        static int KeyGen = 0;

        public Body Body { get; set; } = null;
        public float e = 0.0f;
        public float u = 0.0f;
        public float m;
        public int Key;

        protected Shape(Body body, float density, float elasticity, float friction)
        {
            Body = body;
            m = density * Area;
            e = elasticity;
            u = friction;
            Key = KeyGen++;
        }

        public virtual Vector2 Centroid => throw new NotImplementedException();
        public virtual float Area => throw new NotImplementedException();
        public virtual float GeometricMoment => throw new NotImplementedException();
    };


    public class Circle : Shape
    {
        public Vector2 c, tc;
        public float r;

        public Circle(Body body, float radius, Vector2 offset, float density, float elasticity, float friction) 
            : base(body, density, elasticity, friction)
        {
            c = offset;
            r = radius;
        }

        public override Vector2 Centroid => c;
        public override float Area => PI * r * r;
        public override float GeometricMoment => 0.5f * (r * r) + Centroid.LengthSq;
    }

    public class Capsule : Shape
    {
        public Vector2 a, b, n;
        public Vector2 ta, tb, tn;
        public float r;


        public Capsule(Body body, Vector2 a, Vector2 b, float r ,float density, float elasticity, float friction) 
            : base(body, density, elasticity, friction)
        { 
            this.a = a;
            this.b = b;
            n = (b - a).Normalized.PerpCCW;
            this.r = r;
        }

        public Vector2 A => a;
        public Vector2 B => b;
        public Vector2 Normal => n;

        public override Vector2 Centroid => (a + b)/2.0f;
        public override float Area => PI * r * r + r * (b-a).Length;
        public override float GeometricMoment => (Pow(Distance(b, a) + 2.0f * r, 2) + 4.0f * r* r) / 12.0f + Centroid.LengthSq;
    }
}
*/