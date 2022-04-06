using System;

namespace Gds.LiteConstruct.BusinessObjects
{
    public class Vector3
    {
        private float v1;
        private float v2;
        private float v3;

        public int Y { get; set; }
        public int Z { get; set; }
        public int X { get; set; }

        public Vector3(float v1, float v2, float v3)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }

        public Vector3()
        {
        }

        public static Vector3 operator -(Vector3 vec)
        {
            return null;
        }

        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            return null;
        }

        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return null;
        }

        public static Vector3 operator *(float right, Vector3 left)
        {
            return null;
        }

        public static Vector3 operator *(Vector3 left, float right)
        {
            return null;
        }

        internal static Vector3 Scale(Vector3 vector, object p)
        {
            throw new NotImplementedException();
        }

        public static Vector3 TransformCoordinate(Vector3 look, Matrix matrix)
        {
            throw new NotImplementedException();
        }

        internal float Length()
        {
            throw new NotImplementedException();
        }

        internal static float Dot(Vector3 vector1, Vector3 vector2)
        {
            throw new NotImplementedException();
        }

        public static Vector3 Cross(Vector3 startVec, Vector3 endVec)
        {
            throw new NotImplementedException();
        }

        internal static Vector3 Normalize(object p)
        {
            throw new NotImplementedException();
        }
    }
}