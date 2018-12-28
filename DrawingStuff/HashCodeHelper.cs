namespace DrawingStuff
{
    internal static class HashCodeHelper
    {
        /// Combines two hash codes, useful for combining hash codes of individual vector elements
        internal static int CombineHashCodes(int h1, int h2) => (((h1 << 5) + h1) ^ h2);
        internal static int CombineHashCodes(int h1, int h2, int h3) => CombineHashCodes(CombineHashCodes(h1, h2), h3);
        internal static int CombineHashCodes(int h1, int h2, int h3, int h4) => CombineHashCodes(CombineHashCodes(CombineHashCodes(h1, h2), h3), h4);
    }
}