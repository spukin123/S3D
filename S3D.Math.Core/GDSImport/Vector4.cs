using System;

namespace Gds.LiteConstruct.BusinessObjects
{
    internal class Vector4
    {
        private float z;
        private float v;

        public Vector4(float x, float y, float z, float v)
        {
            X = x;
            Y = y;
            this.z = z;
            this.v = v;
        }

        public float X { get; internal set; }
        public float W { get; internal set; }
        public float Y { get; internal set; }

        internal static Vector4 Transform(Vector4 vector4, Matrix mat)
        {
            throw new NotImplementedException();
        }
    }
}