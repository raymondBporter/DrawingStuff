namespace DrawingStuff
{
    public struct Transform
    {
        public Transform(Rotation2 rot, Vector2 trans) { T = trans; R = rot; }
        public Transform(float rot, Vector2 trans) : this(new Rotation2(rot), trans) { }

        public static Vector2 operator * (Transform A, Vector2 x) => A.R * x + A.T;
        public static Transform operator * (Transform A, Transform B) => new Transform(A.R * B.R, A.R * B.T + A.T);

        public Vector2 T;
        public Rotation2 R;
    }
}
