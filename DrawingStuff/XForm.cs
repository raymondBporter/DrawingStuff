namespace DrawingStuff
{
    public struct XForm
    {
        public XForm(Matrix2 mat, Vector2 vec) { A = mat; d = vec; }
        public XForm(float scale, float rotAngle, Vector2 translation) : this(scale * Matrix2.Rotation(rotAngle), translation) { }
        public XForm(float rotAngle, Vector2 translation) : this(Matrix2.Rotation(rotAngle), translation) { }  

        public static explicit operator Matrix3(XForm x) => new Matrix3(x.A.Row0.X, x.A.Row0.Y, x.d.X, x.A.Row1.X, x.A.Row1.Y, x.d.Y, 0.0f, 0.0f, 1.0f);
        public static Vector2 operator *(XForm A, Vector2 x) => A.A * x + A.d;
        public static XForm operator *(XForm A, XForm B) => new XForm(A.A * B.A, A.A * B.d + A.d);
        public static XForm Identity = new XForm(Matrix2.Identity, Vector2.Zero);    
        public static XForm Rotation(float r) => new XForm(Matrix2.Rotation(r), Vector2.Zero);    
        public static XForm Translation(Vector2 d) => new XForm(Matrix2.Identity, d);
        public static XForm Scale(float s) => new XForm(Matrix2.Scale(s), Vector2.Zero);
        public static XForm Scale(float sx, float sy) => new XForm(Matrix2.Scale(sx, sy), Vector2.Zero);
        public static XForm ReflectionX => new XForm(Matrix2.ReflectionX, Vector2.Zero);
        public static XForm ReflectionY => new XForm(Matrix2.ReflectionY, Vector2.Zero);

        Matrix2 A;
        Vector2 d;
    }
}
