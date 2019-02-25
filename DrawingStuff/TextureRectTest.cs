namespace DrawingStuff
{
    class TextureRectTest
    {
        Rect LocalSpaceRect;
        Rect TextureSpaceRect; // x, y, w, h


        Rect TextureRect; // 0, 0, w, h

        // 
        // TextureRect.Left = 0
        // TextureRect.Bottom = 0
        // x_texture = TextureSpaceRect.Left + TextureSpaceRect.Width ( x_local - LocalSpaceRect.Left ) / LocalSpaceRect.Width 
        // u = x_texture / texture.Width  
        // 




        Vector2[] TexCoords(Vector2[] vertices)
        {
            Vector2[] texCoords = new Vector2[vertices.Length];

            for ( int i = 0; i < vertices.Length; i++ )
            {
                texCoords[i].X = ( TextureSpaceRect.Left + TextureSpaceRect.Width * (vertices[i].X - LocalSpaceRect.Left) / LocalSpaceRect.Width ) / TextureRect.Width;
                texCoords[i].Y = ( TextureSpaceRect.Bottom + TextureSpaceRect.Height * (vertices[i].Y - LocalSpaceRect.Bottom) / LocalSpaceRect.Height ) / TextureRect.Height;
            }
            return texCoords;
        }
    }
}
