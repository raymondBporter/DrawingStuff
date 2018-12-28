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
                Batch.AddVisuals(opaqueBatch.ToList());
                Batch.Draw(opaqueBatch.First().Material, camera.WorldToDevice);
                Batch.Clear();
            }
           
            var transparentVisuals = Visuals.Where(visual => visual.Material.BlendType == Material.BlendMode.Transparent).
                                             OrderBy((visual) => visual.ZCoord, Comparer<float>.Default).
                                             ThenBy((visual) => visual.Material);

            Material currentMaterial = transparentVisuals.FirstOrDefault()?.Material;
            foreach (Visual transparentVisual in transparentVisuals)
            {
                if (transparentVisual.Material != currentMaterial)
                {
                    Batch.Draw(currentMaterial, camera.WorldToDevice);
                    Batch.Clear();
                }
                currentMaterial = transparentVisual.Material;
                Batch.AddVisual(transparentVisual);
            }
            Batch.Draw(currentMaterial, camera.WorldToDevice);
            Batch.Clear();           
        }

        public void AddVisual(Visual visual) => Visuals.Add(visual);
        Batch Batch = new Batch();
        public List<Visual> Visuals = new List<Visual>();
    }
}


