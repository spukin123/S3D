using System;

namespace Gds.LiteConstruct.BusinessObjects
{
    public class Quaternion
    {
        private float sx;
        private float v1;
        private float v2;
        private float cx;

        public Quaternion(float sx, float v1, float v2, float cx)
        {
            this.sx = sx;
            this.v1 = v1;
            this.v2 = v2;
            this.cx = cx;
        }

        public static bool operator ==(Quaternion left, Quaternion right)
        {
            return false;
        }

        public static bool operator !=(Quaternion left, Quaternion right)
        {
            return false;
        }

        public static Quaternion operator +(Quaternion left, Quaternion right)
        {
            return null;
        }

        public static Quaternion operator *(Quaternion left, Quaternion right)
        {
            return null;
        }

        public static Quaternion operator -(Quaternion left, Quaternion right)
        {
            return null;
        }


        public Quaternion Identity { get; internal set; }
        public double W { get; internal set; }
        public double X { get; internal set; }
        public double Y { get; internal set; }
        public double Z { get; internal set; }

        internal void ToAxisAngle(Quaternion quaternion, ref Vector3 axis, ref float angle)
        {
            throw new NotImplementedException();
        }

        internal void RotateAxis(Vector3 axis, float radians)
        {
            throw new NotImplementedException();
        }
    }
}