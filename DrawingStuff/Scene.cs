using System.Collections.Generic;
using System.Linq;

namespace DrawingStuff
{

    class Scene 
    {     
        public void Draw(Camera camera)
        {
            var opaqueBatches = Visuals.Where(visual => visual.Material.BlendType == Material.BlendMode.Solid).
                                        GroupBy(visual => visual.Material);

            foreach (var opaqueBatch in opaqueBatches)
            {
                Batch.Begin(opaqueBatch.First().Material, camera.WorldToDevice);

                foreach (Visual visual in opaqueBatch)
                {
                    Batch.AddGeom(visual.Geom, visual.ZCoord, visual.Transform, visual.UseTransform);
                }

                Batch.End();
            }
           
            var transparentVisuals = Visuals.Where(visual => visual.Material.BlendType == Material.BlendMode.Transparent).
                                             OrderBy((visual) => visual.ZCoord, Comparer<float>.Default).
                                             ThenBy((visual) => visual.Material);

            Material currentMaterial = transparentVisuals.FirstOrDefault()?.Material;

            if (currentMaterial == null)
                return;

            Batch.Begin(currentMaterial, camera.WorldToDevice);

            foreach (Visual transparentVisual in transparentVisuals)
            {
                if (transparentVisual.Material != currentMaterial)
                {
                    Batch.End();
                    Batch.Begin(currentMaterial, camera.WorldToDevice);
                }

                currentMaterial = transparentVisual.Material;
                Batch.AddGeom(transparentVisual.Geom, transparentVisual.ZCoord, transparentVisual.Transform, transparentVisual.UseTransform);
            }

            Batch.End();
        }

        public void AddVisual(Visual visual) => Visuals.Add(visual);
        Batch Batch = new Batch(256000);
        public List<Visual> Visuals = new List<Visual>();
    }
}


