using System.Collections.Generic;
using static DrawingStuff.Rect;

namespace DrawingStuff
{
    interface IBroadphase
    {
        void Add(Visual visual);
        void Clear();
        List<(Visual, Visual)> ComputePairs();
    }

    class NSquaredBroadphase : IBroadphase
    {
        private List<Visual> Visuals = new List<Visual>();

        public void Add(Visual visual) => Visuals.Add(visual);

        public void Clear() => Visuals.Clear();

        public List<(Visual, Visual)> ComputePairs()
        {
            List<(Visual, Visual)> pairs = new List<(Visual, Visual)>();

            for ( int i = 0; i < Visuals.Count; i++ )
            {
                for ( int j = i + 1; j < Visuals.Count; j++ )
                {
                    if ( IntersectRects(Visuals[i].WorldBoundingRect, Visuals[j].WorldBoundingRect) )
                    {
                        Visuals[i].Contacts.Add(Visuals[j]);
                        Visuals[j].Contacts.Add(Visuals[i]);
                        pairs.Add((Visuals[i], Visuals[j]));
                    }
                }
            }
            return pairs;
        }
    }
}
