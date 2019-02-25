using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using System.Linq;

namespace DrawingStuff
{
    internal class Scene 
    {
        public Scene(Camera camera)
        {
            Camera = camera;
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }


        public void Draw()
        {
  

            Batch.SetWorldToDevice(Camera.WorldToDevice);

            foreach (var visualList in OpaqueVisuals.Values)
            {
                if (visualList.Count != 0)
                {
                    visualList.Sort((x,y) => Comparer<float>.Default.Compare(x.ZCoord, y.ZCoord));
                    Batch.Begin(visualList.First().Material);
                    foreach (Visual visual in visualList)
                    {
                        if (IntersectRects(visual.WorldBoundingRect, Camera.WorldBoundingRect))
                        { 
                            Batch.AddGeom(visual, visual.Material, visual.Transform, visual.UseTransform);
                        }
                    }
                    Batch.End();
                }
            }


            
            TransparentVisuals.Sort((x,y) => -Comparer<float>.Default.Compare(x.ZCoord, y.ZCoord));
            Material currentMaterial = TransparentVisuals.FirstOrDefault()?.Material;
            if (currentMaterial != null)
            {
                Batch.Begin(currentMaterial);
                foreach (Visual visual in TransparentVisuals)
                {
                    if (IntersectRects(visual.WorldBoundingRect, Camera.WorldBoundingRect))
                    {
                        if (!visual.Material.CanBatchWith(currentMaterial))
                        {
                            Batch.End();
                            currentMaterial = visual.Material;
                            Batch.Begin(visual.Material);
                        }
                        Batch.AddGeom(visual, visual.Material, visual.Transform, visual.UseTransform);
                    }
                }
                Batch.End();
            }
            

            Batch.DrawDebugLines();
        }

        public static bool IntersectRects(Rect a, Rect b)
          => !(a.Right < b.Left || a.Left > b.Right || a.Bottom > b.Top || a.Top < b.Bottom);

        public void Add(Visual visual)
        {
            if (visual.Color.A > 0.1f && visual.Color.A < 0.99f)
            {
                TransparentVisuals.Add(visual);
            }
            else if (OpaqueVisuals.TryGetValue(visual.Material, out List<Visual> list))
            {
                list.Add(visual);
            }
            else
            {
                OpaqueVisuals.Add(visual.Material, new List<Visual> { visual });
            }
        }

        public bool Remove(Visual visual)
        {
            bool found = false;
            if (OpaqueVisuals.TryGetValue(visual.Material, out List<Visual> list))
            {
                found = list.Remove(visual);
            }
            if ( !found )
            {
                found = TransparentVisuals.Remove(visual);
            }
            return found;
        }

        private Camera Camera;
        private Batch Batch = new Batch(256000);
        private List<Visual> TransparentVisuals = new List<Visual>();
        private Dictionary<Material, List<Visual>> OpaqueVisuals = new Dictionary<Material, List<Visual>>();
    }
}
