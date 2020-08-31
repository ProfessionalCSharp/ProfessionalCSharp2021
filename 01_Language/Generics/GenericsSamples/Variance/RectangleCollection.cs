using System;

namespace Wrox.ProCSharp.Generics
{
    public class RectangleCollection : IIndex<Rectangle>
    {
        private Rectangle[] _data = new[]
        {
            new Rectangle { Height=2, Width=5 },
            new Rectangle { Height=3, Width=7},
            new Rectangle { Height=4.5, Width=2.9}
        };

        private static RectangleCollection? s_coll;
        public static RectangleCollection GetRectangles() => s_coll ??= new RectangleCollection();

        public Rectangle this[int index]
        {
            get
            {
                if (index < 0 || index > _data.Length)
                    throw new ArgumentOutOfRangeException(nameof(index));
                return _data[index];
            }
        }
        public int Count => _data.Length;
    }
}
