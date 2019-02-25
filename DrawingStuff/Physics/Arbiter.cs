using static DrawingStuff.FloatMath;
/*
namespace DrawingStuff
{
    public enum ArbiterState
    {
        // Arbiter is active and its the first collision.
        FirstCollision = 1,
        // Arbiter is active and its not the first collision.
        Normal = 2,
        // Collison is no longer active. A space will cache an arbiter for up to cpSpace.collisionPersistence more steps.
        Cached = 4,
        Invalidated = 5
    };

    public class Arbiter
    {
        public float e = 0;
        public float u = 0;
        public Shape ShapeA;
        public Shape ShapeB;
        public Body BodyA => ShapeA.Body;
        public Body BodyB => ShapeB.Body;
        public Contact Contact = null;
        Vector2 n;
        public bool swapped = false;
        public int stamp = 0;
        public ArbiterState state = ArbiterState.FirstCollision;

        public Arbiter(Shape a, Shape b)
        { 
            ShapeA = a; 
            ShapeB = b; 
        }



        public bool IsFirstContact => state == ArbiterState.FirstCollision;
        

        public bool Remove => state == ArbiterState.Invalidated;

        public Vector2 Normal => swapped ? -n : n;
        public Vector2 PointA => BodyA.p + Contact.r1;
        public Vector2 PointB => BodyB.p + Contact.r2;
        public float Depth => Vector2.Dot(PointB - PointA, n);


        public float Restitution => e;
        public float Friction => u;


        public void Update(CollisionInfo info)
        {

            // For collisions between two similar primitive types, the order could have been swapped since the last frame.
            ShapeA = info.a;
            ShapeB = info.b;

            Contact con = info.Contact;

            info.Contact.r1 -= ShapeA.Body.p;
            info.Contact.r2 -= ShapeB.Body.p;

            // Cached impulses are not zeroed at init time.
            con.jnAcc = con.jtAcc = 0.0f;


            Contact old = this.Contact;

            //if (Contact != null && ArbiterCache.Key(ShapeA, ShapeB))
            {
                // Copy the persistant contact information.
            //    con.jnAcc = old.jnAcc;
             //   con.jtAcc = old.jtAcc;
            }


            Contact = info.Contact;
            n = info.n;

            e = ShapeA.e * ShapeB.e;
            u = ShapeA.u * ShapeB.u;


            // mark it as new if it's been cached
            if (state == ArbiterState.Cached)
                state = ArbiterState.FirstCollision;

        }

        public static Vector2 relative_velocity(Body a, Body b, Vector2 r1, Vector2 r2)
        {
            Vector2 v1_sum = a.v + r1.PerpCCW * a.w;
            Vector2 v2_sum = b.v + r2.PerpCCW * b.w;
            return v2_sum - v1_sum;
        }

        public void PreStep(float dt, float slop, float bias)
        {
            // Calculate the mass normal and mass tangent.
            Contact.nMass = 1f / BodyA.k_scalar(Contact.r1, n) + BodyB.k_scalar(Contact.r2, n);
            Contact.tMass = 1f / BodyA.k_scalar(Contact.r1, n.PerpCCW) + BodyB.k_scalar(Contact.r2, n.PerpCCW);

            // Calculate the target bias velocity.
            float dist = Vector2.Dot(PointB - PointA, n);
            Contact.bias = -bias * Max(0.0f, dist + slop) / dt;
            Contact.jBias = 0.0f;
            // Calculate the target bounce velocity.
            Contact.bounce = e * Vector2.Dot(relative_velocity(BodyA, BodyB, Contact.r1, Contact.r2), n);
        }

 

        // Equal function for arbiterSet.
        public static bool SetEql(Shape[] shapes, Arbiter arb)
        {
            Shape a = shapes[0];
            Shape b = shapes[1];
            return ((a == arb.ShapeA && b == arb.ShapeB) || (b == arb.ShapeA && a == arb.ShapeB));
        }


        public void ApplyCachedImpulse(float dt_coef)
        {
            if (IsFirstContact)
                return;

            Vector2 j = new Rotation2(n.X, n.Y) * new Vector2(Contact.jnAcc, Contact.jtAcc);

            BodyA.apply_impulse(-j * dt_coef, Contact.r1);
            BodyB.apply_impulse(j * dt_coef, Contact.r2);
        }

        public void ApplyImpulse(float dt)
        {
            float friction = u;


            float nMass = Contact.nMass;
            Vector2 r1 = Contact.r1;
            Vector2 r2 = Contact.r2;

            Vector2 vb1 = BodyA.vBias + r1.PerpCCW * BodyA.wBias;
            Vector2 vb2 = BodyB.vBias + r2.PerpCCW * BodyB.wBias;
            Vector2 vr = relative_velocity(BodyA, BodyB, r1, r2);

            float vbn = Vector2.Dot(vb2 - vb1, n);
            float vrn = Vector2.Dot(vr, n);
            float vrt = Vector2.Dot(vr, n.PerpCCW);

            float jbn = (Contact.bias - vbn) * nMass;
            float jbnOld = Contact.jBias;
            Contact.jBias = Min(jbnOld + jbn, 0.0f);

            float jn = -(Contact.bounce + vrn) * nMass;
            float jnOld = Contact.jnAcc;
            Contact.jnAcc = Min(jnOld + jn, 0.0f);

            float jtMax = friction * Contact.jnAcc;
            float jt = -vrt * Contact.tMass;
            float jtOld = Contact.jtAcc;
            Contact.jtAcc = Clamp(jtOld + jt, -jtMax, jtMax);

            Vector2 jBias = n * (Contact.jBias - jbnOld);
            BodyA.apply_bias_impulse(-jBias, r1);
            BodyB.apply_bias_impulse(jBias, r2);

            Vector2 j = new Rotation2(n.X, n.Y) * new Vector2(Contact.jnAcc - jnOld, Contact.jtAcc - jtOld);
            BodyA.apply_impulse(-j, r1);
            BodyB.apply_impulse(j, r2);
        }
    }
}


    */