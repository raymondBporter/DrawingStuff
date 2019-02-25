/*

using System.Collections.Generic;
using static DrawingStuff.FloatMath;

namespace DrawingStuff
{
    public partial class Space
    {
        public bool CollisionEnabled = true;

        /// Number of iterations to use in the impulse solver to solve contacts.
        public int Iterations { get; set; } = 10;

        public Vector2 Gravity { get; set; } = Vector2.Zero;

        /// Damping rate expressed as the fraction of velocity bodies retain each second.
        /// A value of 0.9 would mean that each body's velocity will drop 10% per second.
        /// The default value is 1.0, meaning no damping is applied.
        /// @note This damping value is different than those of cpDampedSpring and cpDampedRotarySpring.
        public float Damping { get; set; } = 1.0f;


        /// Amount of encouraged penetration between colliding shapes.
        /// Used to reduce oscillating contacts and keep the collision cache warm.
        /// Defaults to 0.1. If you have poor simulation quality,
        /// increase this number as much as possible without allowing visible amounts of overlap.
        public float CollisionSlop { get; set; } = 0.1f;

        /// Determines how fast overlapping shapes are pushed apart.
        /// Expressed as a fraction of the error remaining after each second.
        /// Defaults to pow(1.0 - 0.1, 60.0) meaning that Chipmunk fixes 10% of overlap each frame at 60Hz.
        public float CollisionBias { get; set; } = Pow(1f - 0.1f, 60f);

        /// Number of frames that contact information should persist.
        /// Defaults to 3. There is probably never a reason to change this value.
        public int CollisionPersistence { get; set; } = 3;

        public float CurrentTimeStep { get; set; }

        public int stamp = 0;

        public List<Body> dynamicBodies = new List<Body>();
        public List<Body> staticBodies = new List<Body>();

        public List<Body> Bodies(bool dynamic) => dynamic ? dynamicBodies : staticBodies;


        public List<Arbiter> Arbiters { get; set; } = new List<Arbiter>();
        public List<Arbiter> cachedArbiters = new List<Arbiter>();
   

        /// Add a rigid body to the simulation.
        public void AddBody(Body body)
        {
            Bodies(body.IsDynamic).Add(body);
            body.space = this;
        }


        public void FilterArbiters(Body body, Shape filter)
        {

            foreach (Arbiter arb in cachedArbiters)
            {
                // Match on the filter shape, or if it's null the filter body
                if ((body == arb.BodyA && (filter == arb.ShapeA || filter == null)) ||
                    (body == arb.BodyB && (filter == arb.ShapeB || filter == null)))
                {
                    // Call separate when removing shapes.
                    if (filter != null && arb.state != ArbiterState.Cached)
                    {
                        arb.state = ArbiterState.Invalidated;
                    }

                    Arbiters.Remove(arb);
                    cachedArbiters.Add(arb);
                }
            }
        }

        public void UncacheArbiter(Arbiter arb)
        {
   //         cachedArbiters.Remove(arb.Key);
            Arbiters.Remove(arb);
        }
    }
}
*/