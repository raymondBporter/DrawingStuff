using System;
using System.Collections.Generic;
using System.Diagnostics;

/*
namespace DrawingStuff
{
    public class Body
    {
        internal Action<Vector2, float, float> velocity_func;
        internal Action<float> position_func;

        public float m;
        public float mInv;

        public float i;
        public float iInv;

        public Vector2 cog = Vector2.Zero;
        public Vector2 p = Vector2.Zero;
        public Vector2 v = Vector2.Zero;
        public Vector2 f = Vector2.Zero;

        public float a = 0.0f;
        public float w = 0.0f;
        public float t = 0.0f;

        public Transform Transform;

        public Vector2 vBias = Vector2.Zero;
        public float wBias = 0.0f;

        public Space space = null;

        public List<Shape> Shapes = null;
        public List<Arbiter> Arbiters = new List<Arbiter>();

        public bool IsDynamic
        {
            get => (m != float.PositiveInfinity);      
            set =>  SetBodyType(value);
        }

        public bool IsStatic
        {
            get => velocity_func == null && position_func == null;
            set
            {
                SetBodyType(false);
                velocity_func = null;
                position_func = null;
            }
        }

        public Body(List<Shape> shapes, Vector2 pos, float angle, bool dynamic)
        {
            Shapes = shapes;
            foreach (Shape shape in Shapes)
            {
                shape.Body = this;
            }
            p = pos;
            a = angle;
            Transform = new Transform(angle, pos);
        }

        public void SetBodyType(bool isDynamic)
        {
            if (isDynamic == IsDynamic)
                return;

            if (isDynamic)
            {
                AccumulateMassFromShapes(Shapes);
            }
            else
            {
                m = i = float.PositiveInfinity;
                mInv = iInv = 0.0f;
                v = Vector2.Zero;
                w = 0.0f;
            }

            if (space != null)
            {
                space.Bodies(!isDynamic).Remove(this);
                space.Bodies(isDynamic).Add(this);
            }
        }


        // Should *only* be called when shapes with mass info are modified, added or removed.
        public void AccumulateMassFromShapes(List<Shape> shapes)
        {
            if (!IsDynamic) return;

            // Reset the body's mass data.
            i = 0.0f;

            // Cache the position to realign it at the end.
            Vector2 pos = Position;


            m = 0.0f;
            cog = Vector2.Zero;
            foreach(Shape shape in shapes)
            {
                m += shape.m;
                cog += shape.m * shape.Centroid;
            }
            cog /= m;



            foreach (Shape shape in shapes)
            {
                float mass = shape.m;

                if (mass > 0.0f)
                {
                    float msum = m + mass;

                    i += mass * ( shape.GeometricMoment + Vector2.DistanceSq(cog, shape.Centroid) * mass  / msum);
                    m = msum;
                }
            }

            mInv = 1.0f / m;
            iInv = 1.0f / i;

            // Realign the body since the CoG has probably moved.
            Position = pos;
        }

        public Space Space { get => space; }
        

        public float Mass
        {
            get => m;
            set
            {
                Debug.Assert(IsDynamic);
                Debug.Assert(0.0f <= value && value < float.PositiveInfinity);
                m = value;
                mInv = 1.0f / value;
            }
        }

        public float Moment
        {
            get => i;
            set
            {
                i = value;
                iInv = 1.0f / value;
            }
        }

        public Rotation2 Rotation => Transform.R;
        
        

        public void AddShape(Shape shape)
        {
            Shapes.Add(shape);

            if (shape.m > 0.0f)
            {
                AccumulateMassFromShapes(Shapes);
            }
        }

        public void RemoveShape(Shape shape)
        {
            Shapes.Remove(shape);
            if (IsDynamic && shape.m > 0.0f)
            {
                AccumulateMassFromShapes(Shapes);
            }
        }



        public void SetTransform(Vector2 cogPos, float a)
        {
            Rotation2 rot = new Rotation2(a);
            Transform = new Transform(rot, cogPos - rot * cog);
        }


        public Vector2 Position
        {
            get => Transform.T;
            set
            {
                p = Rotation * cog + value;
                SetTransform(p, a);
            }
        }

   

        public Vector2 CenterOfGravity
        {
            get => cog;
            set => cog = value;
        }


        public Vector2 Velocity
        {
            get => v;
            set => v = value;
        }

        public Vector2 Force
        {
            get => f;
            set => f = value;
            
        }

        public float Angle
        {
            get => a;
            set
            {
                a = value;
                SetTransform(p, value);
            }
        }

        public float AngularVelocity
        {
            get => w;
            set => w = value; 
        }


        public float Torque
        {
            get => t;
            set => t = value;
        }


        public Vector2 LocalToWorld(Vector2 point) => Transform * point;
        public Vector2 WorldToLocal(Vector2 point) => Transform.Inverse *  point;


        public void ApplyForceAtWorldPoint(Vector2 force, Vector2 point)
        {
            f += force;
            t += Vector2.Cross(point - Transform * cog, force);
        }


        public void ApplyForceAtLocalPoint(Vector2 force, Vector2 point)
        {
            ApplyForceAtWorldPoint(Rotation * force, Transform * point);
        }

        public void ApplyImpulseAtWorldPoint(Vector2 impulse, Vector2 point)
        {
            v = v + impulse * mInv;
            w += iInv * Vector2.Cross(point - Transform.T, impulse);
        }

        public void apply_impulse(Vector2 j, Vector2 r)
        {
            v = v + j * mInv;
            w += iInv * Vector2.Cross(r, j);
        }


        public void apply_bias_impulse(Vector2 j, Vector2 r)
        {
            vBias = vBias + j * mInv;
            wBias += iInv * Vector2.Cross(r, j);
        }




        public float k_scalar(Vector2 r, Vector2 n)
        {
            float rcn = Vector2.Cross(r, n);
            return mInv + iInv * rcn * rcn;
        }


        public void ApplyImpulseAtLocalPoint(Vector2 impulse, Vector2 point)
        {
            ApplyImpulseAtWorldPoint(Rotation * impulse, Transform * point);
        }

        public Vector2 VelocityAtLocalPoint(Vector2 point) => v + (Rotation * (point - cog)).PerpCCW * w;
        public Vector2 VelocityAtWorldPoint(Vector2 point) => v + (point - Transform * cog).PerpCCW * w;



 

        /// Apply an force (in world coordinates) to the body at a point relative to the center of gravity (also in world coordinates).
        public void ApplyForce(Vector2 force, Vector2 r)
        {
            f += force;
            t += Vector2.Cross(r, force);
        }
    }
}
*/