/*

using System.Collections.Generic;
using static DrawingStuff.FloatMath;


namespace DrawingStuff
{
    class ArbiterCache
    {
        public static ulong Key(Shape a, Shape b) => (ulong)(Max(a.Key, b.Key) >> 24)|((ulong)Min(a.Key, b.Key));

        public Arbiter TryGetValue(Shape a, Shape b)
        {
            if (Cache.TryGetValue(Key(a, b), out Arbiter value))
                return value;
            else
                return null;
        }

        public void Add(Arbiter arbiter)
        {
            Cache.Add(Key(arbiter.ShapeA, arbiter.ShapeB), arbiter);
        }

        Dictionary<ulong, Arbiter> Cache = new Dictionary<ulong, Arbiter>();
    }



    public partial class Space
    {
        ArbiterCache ArbiterCache = new ArbiterCache();

        public void CollideShapes(Shape a, Shape b)
        {
            CollisionInfo info = Collision.Collide(a, b);

            if (info == null)
                return;

            Arbiter arbiter = ArbiterCache.TryGetValue(a, b);

            if (arbiter == null)
            {
                arbiter = new Arbiter(a, b);
                ArbiterCache.Add(arbiter);
            }

            arbiter.Update(info);
            arbiter.stamp = stamp;
            Arbiters.Add(arbiter);
        }


        public bool ArbiterSetFilter(Arbiter arb)
        {
            var ticks = stamp - arb.stamp;

            // Arbiter was used last frame, but not this one
            if (ticks >= 1 && arb.state != ArbiterState.Cached)
            {
                arb.state = ArbiterState.Cached;
            }

            if (ticks >= CollisionPersistence)
            {
                //arb.Contacts.Clear();
                return false;
            }
            return true;
        }


        public void Step(float dt)
        {
            if (dt == 0.0f) return;

            stamp++;

            float prev_dt = CurrentTimeStep;
            CurrentTimeStep = dt;


            var bodies = dynamicBodies;

            Arbiters.Clear();


            // Integrate positions
            for (int i = 0; i < bodies.Count; i++)
            {
                bodies[i].p += (bodies[i].v + bodies[i].vBias) * dt;
                bodies[i].a += (bodies[i].w + bodies[i].wBias) * dt;
                bodies[i].SetTransform(bodies[i].p, bodies[i].a);
                bodies[i].vBias = Vector2.Zero;
                bodies[i].wBias = 0.0f;
            }

            for ( int i = 0; i < bodies.Count; i++ )
            {
                for (int j = i + 1; j < bodies.Count; j++)
                {
                    foreach (Shape shapeA in bodies[i].Shapes)
                    {
                        foreach (Shape shapeB in bodies[j].Shapes)
                        {
                            CollideShapes(shapeA, shapeB);
                        }
                    }
                }
            }


            for (int i = 0; i < Arbiters.Count; i++)
            {
                Arbiters[i].BodyA.Arbiters.Add(Arbiters[i]);
                Arbiters[i].BodyB.Arbiters.Add(Arbiters[i]);
            }

            foreach (var hash in this.cachedArbiters)
                if (!ArbiterSetFilter(hash))
                    cachedArbiters.Remove(hash);//, hash.Value); 


            for (int i = 0; i < Arbiters.Count; i++)
            {
                Arbiters[i].PreStep(dt, CollisionSlop, 1 - Pow(CollisionBias, dt));
            }


            float damping = Pow(Damping, dt);
            for (int i = 0; i < bodies.Count; i++)
            {
                if (bodies[i].IsDynamic)
                {
                    bodies[i].v = (bodies[i].v * damping + Gravity + bodies[i].f * bodies[i].mInv) * dt;
                    bodies[i].w = bodies[i].w * damping + bodies[i].t * bodies[i].iInv * dt;
                    bodies[i].f = Vector2.Zero;
                    bodies[i].t = 0.0f;
                }
            }

            // Apply cached impulses
            var dt_coef = (prev_dt == 0 ? 0 : dt / prev_dt);
            for (int i = 0; i < Arbiters.Count; i++)
            {
                Arbiters[i].ApplyCachedImpulse(dt_coef);
            }


            // Run the impulse solver.
            for (int i = 0; i < Iterations; i++)
            {
                for (int j = 0; j < Arbiters.Count; j++)
                {
                    Arbiters[j].ApplyImpulse(dt);
                }
            }
        }
    }
}

*/