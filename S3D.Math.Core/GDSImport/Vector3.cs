using S3D.Core.MigrationTypes;
using System;
using System.Windows.Media.Media3D;

namespace Gds.LiteConstruct.BusinessObjects
{
    public class Vector3
    {
        private float x;
        private float y;
        private float z;

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public float X 
        {
            get { return x; }
            set { x = value; }
        }

        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        public float Z
        {
            get { return z; }
            set { z = value; }
        }

        public Vector3()
        {
        }

        public static Vector3 operator -(Vector3 vec)
        {
            return V3M.ToV3(-new Vector3D(vec.X, vec.Y, vec.Z));
        }

        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            return V3M.ToV3(new Vector3D(left.X, left.Y, left.Z) 
                + new Vector3D(right.X, right.Y, right.Z));
        }

        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return V3M.ToV3(new Vector3D(left.X, left.Y, left.Z)
                - new Vector3D(right.X, right.Y, right.Z));
        }

        public static Vector3 operator *(float right, Vector3 left)
        {
            return V3M.ToV3(right * new Vector3D(left.X, left.Y, left.Z));

        }

        public static Vector3 operator *(Vector3 left, float right)
        {
            return V3M.ToV3(right * new Vector3D(left.X, left.Y, left.Z));
        }

        internal static Vector3 Scale(Vector3 vector, object p)
        {
            return V3M.ToV3((double)p * new Vector3D(vector.X, vector.Y, vector.Z));
        }

        public static Vector3 TransformCoordinate(Vector3 look, Matrix matrix)
        {
            //TODO: Implement Matrix migration.
            //Matrix3D m
            throw new NotImplementedException();
        }

        internal float Length()
        {
            return (float)new Vector3D(X, Y, Z).Length;
        }

        internal static float Dot(Vector3 vector1, Vector3 vector2)
        {
            return (float)Vector3D.DotProduct(V3M.ToV3D(vector1), V3M.ToV3D(vector2));
        }

        public static Vector3 Cross(Vector3 startVec, Vector3 endVec)
        {
            return V3M.ToV3(Vector3D.CrossProduct(V3M.ToV3D(startVec), V3M.ToV3D(endVec)));
        }

        internal static Vector3 Normalize(object p)
        {
            Vector3 v3 = (Vector3)p;
            Vector3D n = new Vector3D(v3.X, v3.Y, v3.z);
            n.Normalize();

            return V3M.ToV3(n);
        }
    }
}