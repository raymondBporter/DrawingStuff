using System;
using System.Collections.Generic;
using static DrawingStuff.FloatMath;
using static DrawingStuff.Vector2;
/*
namespace DrawingStuff
{
    public class Collision
    {
        public static CollisionInfo CircleToCircle(Shape cir1, Shape cir2)
        {
            Circle c1 = cir1 as Circle;
            Circle c2 = cir2 as Circle;

            float mindist = c1.r + c2.r;
            Vector2 delta = c2.tc - c1.tc;
            float distsq = delta.LengthSq;

            if (distsq < mindist * mindist)
            {
                float dist = Sqrt(distsq);
                Vector2 n = (dist > 0.0f ? delta * (1.0f / dist) : new Vector2(1.0f, 0.0f));
                return new CollisionInfo(c1, c2, n, new Contact(c1.tc + n * c1.r, c2.tc - n * c2.r));
            }
            return null;
        }

        public static CollisionInfo CircleToSegment(Shape circle, Shape capsule)
        {
            Circle circ = circle as Circle;
            Capsule cap = capsule as Capsule;

            // Find the closest point on the segment to the circle.
            Vector2 d = cap.tb - cap.ta;
            float t = Clamp01(Dot(d, circ.tc - cap.ta) / d.LengthSq);
            Vector2 closest = cap.ta + d * Clamp01(Dot(d, circ.tc - cap.ta) / d.LengthSq);

            // Compare the radii of the two shapes to see if they are colliding.
            float mindist = circ.r + cap.r;
            Vector2 delta = (closest - circ.tc);
            float distsq = delta.LengthSq;
            if (distsq < mindist * mindist)
            {
                if (t != 0.0f && t != 1.0f)
                {
                    float dist = Sqrt(distsq);
                    Vector2 n = dist > 0 ? delta / dist : cap.tn;
                    return new CollisionInfo(circ, cap, n, new Contact(circ.tc + n * circ.r, closest + (-n * cap.r)) );
                }
            }
            return null;
        }

        public static Func<Shape, Shape, CollisionInfo>[] CollisionFuncs = new Func<Shape, Shape, CollisionInfo>[2]
        {
            CircleToCircle,
            CircleToSegment
        };

        public static CollisionInfo Collide(Shape a, Shape b)
        {
            if (a is Capsule && b is Circle)
                Swap(ref a, ref b);
            return CollisionFuncs[b is Capsule ? 1 : 0](a, b);
        }

    }

}

    */