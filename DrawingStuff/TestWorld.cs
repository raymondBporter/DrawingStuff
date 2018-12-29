using static DrawingStuff.FloatMath;

namespace DrawingStuff
{
    class TestWorld : IWorld
    {
        const int numVisuals = 100000;

        public TestWorld()
        {
            for ( int i = 0; i < numVisuals; i++ )
            {
                Scene.AddVisual(VisualFactory.RandomVisual());
                pos[i] = Vector2.Zero;
                vel[i] = RandomUnitVector * ( 0.1f +  Rand(0.1f));
                angle[i] = Rand(2.0f * PI);
                angleVel[i] = (0.5f + Rand(1.0f));

            }
        }

        public void HandleInput(Vector2 mouseWorldPos, float deltaTime, Camera camera) {   }

        public void Update(float deltaTime)
        {
            for (int i = 0; i < numVisuals; i++)
            {
                pos[i] += vel[i] * deltaTime;
                angle[i] += angleVel[i] * deltaTime;
                Scene.Visuals[i].Transform = new XForm(angle[i], pos[i]);
            }
        }

        public void Draw(Camera camera, float deltaTime)
        {
            Scene.Draw(camera);
        }

        Vector2[] pos = new Vector2[numVisuals];
        Vector2[] vel = new Vector2[numVisuals];
        float[] angle = new float[numVisuals];
        float[] angleVel = new float[numVisuals];
        Scene Scene = new Scene();
    }
}

