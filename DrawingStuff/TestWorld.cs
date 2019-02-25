using System.Collections.Generic;
using static DrawingStuff.FloatMath;

namespace DrawingStuff
{
    internal class TestWorld
    {
        private const int numVisuals = 10000;
        List<Visual> Visuals = new List<Visual>(10000);


        public TestWorld(Camera camera)
        {
            Scene = new Scene(camera);


            for ( int i = 0; i < numVisuals; i++ )
            {
                Visuals.Add(VisualFactory.RandomVisual());
                pos[i] = new Vector2(20.0f- Rand(40), 20.0f - Rand(40));
                vel[i] = RandomUnitVector * Rand(3.0f);
                angle[i] = Rand(2.0f * PI);
                angleVel[i] = (1.5f + Rand(1.0f));
            }

            for (int i = 0; i < numVisuals; i++)
            {
                Visuals[i].WorldBoundingRect = Visuals[i].BoundingRect.Translated(Visuals[i].Transform.d);
                Scene.Add(Visuals[i]);
            }
        }

        public void HandleInput(Vector2 mouseWorldPos, float deltaTime)
        {

        }

        public void Update(float deltaTime)
        {
            for (int i = 0; i < numVisuals; i++)
            {
                pos[i] += vel[i] * deltaTime;
                angle[i] += angleVel[i] * deltaTime;
                Visuals[i].Transform = new XForm(angle[i], pos[i]);
                Visuals[i].WorldBoundingRect = Visuals[i].BoundingRect.Translated(Visuals[i].Transform.d);
            }
        }

        public void Draw(Camera camera, float deltaTime)
        {
            Scene.Draw();
        }

        private readonly Vector2[] pos = new Vector2[numVisuals];
        private readonly Vector2[] vel = new Vector2[numVisuals];
        private readonly float[] angle = new float[numVisuals];
        private readonly float[] angleVel = new float[numVisuals];
        private Scene Scene;
    }

}

