using System;

namespace Gds.LiteConstruct.BusinessObjects
{
    internal class Vector4
    {
        private int z;
        private float v;

        public Vector4(int x, int y, int z, float v)
        {
            X = x;
            Y = y;
            this.z = z;
            this.v = v;
        }

        public int X { get; internal set; }
        public int W { get; internal set; }
        public int Y { get; internal set; }

        internal static Vector4 Transform(Vector4 vector4, Matrix mat)
        {
            throw new NotImplementedException();
        }
    }
}