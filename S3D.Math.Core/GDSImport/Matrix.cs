using System;

namespace Gds.LiteConstruct.BusinessObjects
{
    public class Matrix
    {
        public static Matrix Identity { get; internal set; }

        internal void RotateAxis(Vector3 axis, float radians)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(Matrix left, Matrix right)
        {
            return false;
        }
        public static bool operator !=(Matrix left, Matrix right)
        {
            return false;
        }

        public static Matrix operator +(Matrix left, Matrix right)
        {
            return null;
        }

        public static Matrix operator -(Matrix left, Matrix right)
        {
            return null;
        }

        public static Matrix operator *(Matrix left, Matrix right)
        {
            return null;
        }

        public static Matrix LookAtLH(Vector3 position, Vector3 lookAt, Vector3 vector3)
        {
            return null;
        }

        public static Matrix RotationAxis(Vector3 rotVec, float radians)
        {
            throw new NotImplementedException();
        }

        public static Matrix RotationZ(float radians)
        {
            throw new NotImplementedException();
        }
    }
}