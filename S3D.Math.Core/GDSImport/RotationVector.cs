using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.BusinessObjects
{
    [Serializable]
    public class RotationVector : Rotation
    {
        protected Angle x;

        public Angle X
        {
            get { return x; }
            set { x = value; }
        }

        protected Angle y;

        public Angle Y
        {
            get { return y; }
            set { y = value; }
        }

        protected Angle z;

        public Angle Z
        {
            get { return z; }
            set { z = value; }
        }

        public RotationVector()
        {
        }

        public RotationVector(Angle x, Angle y, Angle z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override Matrix GetRotationMatrix()
        {
            //return Matrix.RotationYawPitchRoll(y.Radians, x.Radians, z.Radians);

            return ToQuaternion().GetRotationMatrix();
        }

        public static RotationVector operator +(RotationVector vec1, RotationVector vec2)
        {
            return new RotationVector(vec1.X + vec2.X, vec1.Y + vec2.Y, vec1.Z + vec2.Z);
        }

        public override void Normalize()
        {
            if (!x.Normalized)
            {
                x.Normalize();
            }
            if (!y.Normalized)
            {
                y.Normalize();
            }
            if (!z.Normalized)
            {
                z.Normalize();
            }
        }

        public bool HasRotation
        {
            get { return !(x == Angle.A0 && y == Angle.A0 && z == Angle.A0); }
        }

        //Works good
        public AxisAngle ToQuaternion()
        {
            //1
            //float c1, c2, c3;
            //float s1, s2, s3;

            //c1 = (float)Math.Cos(y.Radians / 2f);
            //c2 = (float)Math.Cos(x.Radians / 2f); //z
            //c3 = (float)Math.Cos(z.Radians / 2f); //x

            //s1 = (float)Math.Sin(y.Radians / 2f);
            //s2 = (float)Math.Sin(x.Radians / 2f);
            //s3 = (float)Math.Sin(z.Radians / 2f);

            //float qX, qY, qZ, w;
            //w = c1 * c2 * c3 - s1 * s2 * s3;
            //qX = s1 * s2 * c3 + c1 * c2 * s3;
            //qY = s1 * c2 * c3 + c1 * s2 * s3;
            //qZ = c1 * s2 * c3 - s1 * c2 * s3;

            //return new RotationByAxis(new Quaternion(qX, qY, qZ, w));

            //2
            Quaternion qX, qY, qZ;

            float cx, cy, cz, sx, sy, sz;
            cx = (float)Math.Cos(x.Radians / 2f);
            cy = (float)Math.Cos(y.Radians / 2f);
            cz = (float)Math.Cos(z.Radians / 2f);
            sx = (float)Math.Sin(x.Radians / 2f);
            sy = (float)Math.Sin(y.Radians / 2f);
            sz = (float)Math.Sin(z.Radians / 2f);

            qX = new Quaternion(sx, 0f, 0f, cx);
            qY = new Quaternion(0f, sy, 0f, cy);
            qZ = new Quaternion(0f, 0f, sz, cz);

            return new AxisAngle(qX * qY * qZ);

            //3
            //float c1, c2, c3, s1, s2, s3;
            //c1 = (float)Math.Cos(x.Radians);
            //c2 = (float)Math.Cos(y.Radians);
            //c3 = (float)Math.Cos(z.Radians);
            //s1 = (float)Math.Sin(x.Radians);
            //s2 = (float)Math.Sin(y.Radians);
            //s3 = (float)Math.Sin(z.Radians);

            //float qX, qY, qZ, w;

            //w = (float)Math.Sqrt(1f + c1 * c2 + c1 * c3 - s1 * s2 * s3 + c2 * c3) / 2f;
            //qX = (c2 * s3 + c1 * s3 + s1 * s2 * c3) / (4f * w);//y
            //qY = (s1 * c2 + s1 * c3 + c1 * s2 * s3) / (4f * w);//z
            //qZ = (-s1 * s3 + c1 * s2 * c3 + s2) /(4f * w);//x

            ////-- Transform to left-handed

            //RotationByAxis temp;
            //temp = new RotationByAxis(new Quaternion(qX, qY, qZ, w));

            //RotationVector v;
            //v = temp.ToEuler();

            //return temp;
        }
    }
}
