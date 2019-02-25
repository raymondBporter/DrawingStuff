using System.Collections.Generic;
using static DrawingStuff.FloatMath;

namespace DrawingStuff
{
    internal class ClusterTestWorld
    {
        private const int numVisuals = 1000;

        List<Visual> Visuals = new List<Visual>(numVisuals);

        public ClusterTestWorld(Camera camera)
        {
                    Scene = new Scene(camera);

        Vector[] vectors = new Vector[numVisuals];

            Vector2[] bias = new Vector2[4];
            bias[0] = 2.0f * new Vector2(Rand(1.0f), Rand(1.0f));
            bias[1] = 2.0f * new Vector2(Rand(1.0f), Rand(1.0f));
            bias[2] = 2.0f * new Vector2(Rand(1.0f), Rand(1.0f));
            bias[3] = 2.0f * new Vector2(Rand(1.0f), Rand(1.0f));


            for (int i = 0; i < numVisuals; i++)
            {
                int randIndex = (int)Rand(3.999999f);
                Vector2 pos = new Vector2(Rand(1.0f), Rand(1.0f));// + bias[randIndex];
                vectors[i] = new Vector(2);
                vectors[i][0] = pos.X;
                vectors[i][1] = pos.Y;
            }

            KMeans cluster = new KMeans(vectors, 3, 30);
            int[] groups = cluster.FindGroups();

            for (int i = 0; i < numVisuals; i++)
            {
                Color4 color;
                if (groups[i] == 0)
                {
                    color = Color4.Blue;
                }
                else if (groups[i] == 1)
                {
                    color = Color4.Green;
                }
                else //if (groups[i] == 2)
                {
                    color = Color4.Red;
                }
                //else //( groups[i] == 3 )
                {
                    //m = VisualFactory.tranparentColorMat;
                }

                Visual visual = VisualFactory.CircleVisual(0.05f, color, VisualFactory.FillMaterial, 0.5f);
                Visuals.Add(visual);
            }

            for (int i = 0; i < numVisuals; i++)
            {
                Visuals[i].Transform = new XForm(0, new Vector2(vectors[i][0], vectors[i][1]));
            }


        }

        public void HandleInput(Vector2 mouseWorldPos, float deltaTime, Camera camera)
        {

        }

        public void Update(float deltaTime)
        {

        }

        public void Draw(Camera camera, float deltaTime)
        {

            for (int i = 0; i < numVisuals; i++)
            {
                Scene.Add(Visuals[i]);
            }
            Scene.Draw();
        }

        private readonly Vector2[] pos = new Vector2[numVisuals];
        private Scene Scene;
    }
}

