using System.Collections.Generic;
using static DrawingStuff.Rect;

namespace DrawingStuff
{
    class QuadTree : IBroadphase
    {
        public QuadTree(Rect rect, int depth)
        {
            Rect = rect;
            depth--;
            if (depth > 0)
            {
                Children = new QuadTree[4];
                Vector2 childSize = new Vector2(rect.Width, rect.Height) / 2.0f;
                Vector2 d = childSize / 2.0f;
                Children[0] = new QuadTree(new Rect(rect.Center + new Vector2( d.X, d.Y), childSize.X, childSize.Y), depth); //TR
                Children[1] = new QuadTree(new Rect(rect.Center + new Vector2(-d.X, d.Y), childSize.X, childSize.Y), depth); //TL
                Children[2] = new QuadTree(new Rect(rect.Center + new Vector2(-d.X,-d.Y), childSize.X, childSize.Y), depth); //BL
                Children[3] = new QuadTree(new Rect(rect.Center + new Vector2( d.X,-d.Y), childSize.X, childSize.Y), depth); //BR
            }
        }


        public void Draw(Batch batch)
        {
            if (Visuals.Count != 0 )
                batch.DrawRect(Rect, Color4.Green);
            if (Children!= null)
            {
                foreach (var child in Children)
                {
                    child.Draw(batch);
                }
            }
        }

        public void Add(Visual visual)
        {
            if ( Children == null )
            {
                Visuals.Add(visual);
            }
            else if ( RectContainsRect(Children[0].Rect, visual.WorldBoundingRect) )
            {
                Children[0].Add(visual);
            }
            else if ( RectContainsRect(Children[1].Rect, visual.WorldBoundingRect))
            {
                Children[1].Add(visual);
            }
            else if ( RectContainsRect(Children[2].Rect, visual.WorldBoundingRect))
            {
                Children[2].Add(visual);
            }
            else if ( RectContainsRect(Children[3].Rect, visual.WorldBoundingRect))
            {
                Children[3].Add(visual);
            }
            else
            {
                Visuals.Add(visual);
            }
        }

        public List<(Visual, Visual)> ComputePairs()
        {
            List<(Visual, Visual)> pairs = new List<(Visual, Visual)>();
            QuerryContacts(null, pairs);
            return pairs;
        }

        void QuerryContacts(List<Visual> others, List<(Visual, Visual)> pairs)
        {
            for (int i = 0; i < Visuals.Count; i++)
            {
                for (int j = i + 1; j < Visuals.Count; j++)
                {
                    if (IntersectRects(Visuals[i].WorldBoundingRect, Visuals[j].WorldBoundingRect))
                    {
                        pairs.Add((Visuals[i], Visuals[j]));
                        Visuals[i].Contacts.Add(Visuals[j]);
                        Visuals[j].Contacts.Add(Visuals[i]);
                    }
                }
            }

            if (others != null)
            {
                foreach (Visual visual in Visuals)
                {
                    foreach (Visual other in others)
                    {
                        if (IntersectRects(visual.WorldBoundingRect, other.WorldBoundingRect))
                        {
                            pairs.Add((other, visual));
                            visual.Contacts.Add(other);
                            other.Contacts.Add(visual);
                        }
                    }
                }
            }
            if (Children != null)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (others != null)
                    {
                        Children[i].QuerryContacts(others, pairs);
                    }
                    Children[i].QuerryContacts(Visuals, pairs);

                }
            }
        }

        public void Clear( )
        {
            Visuals.Clear();

            if (Children != null)
            {
                for (int i = 0; i < 4; i++)
                {
                    Children[i].Clear();
                }
            }
        }

        Rect Rect;
        List<Visual> Visuals = new List<Visual>();
        readonly QuadTree[] Children = null;
    }
}



