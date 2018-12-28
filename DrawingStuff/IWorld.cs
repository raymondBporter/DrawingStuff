
    namespace DrawingStuff
    {
        public interface IWorld
        {
            void HandleInput(Vector2 mouseWorldPos, float deltaTime, Camera camera);
            void Update(float deltaTime);
            void Draw(Camera camera, float deltaTime);
        }
    }

