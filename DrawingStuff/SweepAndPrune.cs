using System;
using System.Collections.Generic;


namespace DrawingStuff
{
    class SweepAndPrune : IBroadphase
    {
        private List<Visual> Visuals = new List<Visual>();

        public void Add(Visual visual) => Visuals.Add(visual);

        public void Clear() => Visuals.Clear();

        public List<(Visual, Visual)> ComputePairs()
        {
            List<(Visual, Visual)> pairs = new List<(Visual, Visual)>();

            if (Visuals.Count == 0)
            {
                return pairs;
            }

            Visuals.Sort((x, y) => Comparer<float>.Default.Compare(x.WorldBoundingRect.Left, y.WorldBoundingRect.Left));

            Visual[] active = new Visual[Visuals.Count];
            Visual[] buffer = new Visual[Visuals.Count];

            active[0] = Visuals[0];
            int numActive = 1;

            for (int i = 1; i < Visuals.Count; i++)
            {
                for (int j = 0; j < numActive;)
                {
                    if (Visuals[i].WorldBoundingRect.Left < active[j].WorldBoundingRect.Right)
                    {
                        if (!(Visuals[i].WorldBoundingRect.Bottom > active[j].WorldBoundingRect.Top || Visuals[i].WorldBoundingRect.Top < active[j].WorldBoundingRect.Bottom))
                        {
                            Visuals[i].Contacts.Add(active[j]);
                            active[j].Contacts.Add(Visuals[i]);
                       
                            pairs.Add((Visuals[i], active[j]));
                        }
                        //active[numActive] = Visuals[i];
                        // DoY(active, numActive + 1, buffer, pairs);
                        j++;
                    }
                    else
                    {
                        active[j] = active[numActive - 1];
                        numActive--;
                    }
                }
                active[numActive] = Visuals[i];
                numActive++;
            }
            return pairs;
        }

        class BottomCompare : IComparer<Visual>
        {
            public int Compare(Visual x, Visual y)
            {
                return Comparer<float>.Default.Compare(x.WorldBoundingRect.Bottom, y.WorldBoundingRect.Bottom);
            }
        }

        BottomCompare bottomCompare = new BottomCompare();


        void DoY(Visual[] visuals, int numVisuals, Visual[] active, List<(Visual, Visual)> pairs)
        {
            int numActive = 1;
            active[0] = visuals[0];
            Array.Sort(visuals, 0, numVisuals, bottomCompare);

            for (int i = 1; i < numVisuals; i++)
            {
                for (int j = 0; j < numActive;)
                {
                    if (Visuals[i].WorldBoundingRect.Bottom < active[j].WorldBoundingRect.Top)
                    {

                        visuals[i].Contacts.Add(active[j]);
                        active[j].Contacts.Add(visuals[i]);
                        pairs.Add((visuals[i], active[j]));
                        j++;
                    }
                    else
                    {
                        active[j] = active[numActive - 1];
                        numActive--;
                    }
                }
                active[numActive] = visuals[i];
                numActive++;
            }
        }
    }
}