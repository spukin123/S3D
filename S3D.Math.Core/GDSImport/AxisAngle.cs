using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.BusinessObjects
{
    public class AxisAngle : Rotation
    {
        private Vector3 axis;
        public Vector3 Axis
        {
            get { return axis; }
            set { axis = value; }
        }

        private Angle rotationAngle;
        public Angle RotationAngle
        {
            get { return rotationAngle; }
            set { rotationAngle = value; }
        }

        private Quaternion quaternion;
        public Quaternion Quaternion
        {
            get { return quaternion; }
        }

		public AxisAngle()
		{
		}

        public AxisAngle(Quaternion quaternion)
        {
            this.quaternion = quaternion;

            axis = new Vector3();//Vector3.Empty;

            float angle = 0f;
            Quaternion.ToAxisAngle(this.quaternion, ref axis, ref angle);
            rotationAngle = new Angle(angle);
        }

        public AxisAngle(Vector3 axis, Angle rotationAngle)
        {
            this.axis = Vector3.Normalize(axis);
            this.rotationAngle = rotationAngle;
            
            quaternion = Quaternion.Identity;
            quaternion.RotateAxis(this.axis, this.rotationAngle.Radians);
        }

        public static AxisAngle operator *(AxisAngle rot1, AxisAngle rot2)
        {
            return new AxisAngle(rot1.Quaternion * rot2.Quaternion);
        }

        public static AxisAngle operator +(AxisAngle rot1, AxisAngle rot2)
        {
            return new AxisAngle(rot1.Quaternion + rot2.Quaternion);
        }

        //Work good
        public RotationVector ToRotationVector()
        {
            Angle byX, byY, byZ;

            //1
            //byY = new Angle((float)Math.Atan2(2f * quaternion.Y * quaternion.W - 2f * quaternion.X * quaternion.Z, 1 - 2f * Math.Pow(quaternion.Y, 2f) - 2f * Math.Pow(quaternion.Z, 2f)));
            //byX = new Angle((float)Math.Asin(2f * quaternion.X * quaternion.Y + 2f * quaternion.Z * quaternion.W));
            //byZ = new Angle((float)Math.Atan2(2f * quaternion.X * quaternion.W - 2f * quaternion.Y * quaternion.Z, 1 - 2f * Math.Pow(quaternion.X, 2f) - 2f * Math.Pow(quaternion.Z, 2f)));

            //2
            //byZ = new Angle((float)Math.Atan2(2f * axis.Y * rotationAngle.Radians - 2f * axis.X * axis.Z, 1 - 2f * Math.Pow(axis.Y, 2f) - 2f * Math.Pow(axis.Z, 2f)));
            //byX = new Angle((float)Math.Asin(2f * axis.X * axis.Y + 2f * axis.Z * rotationAngle.Radians));
            //byY = new Angle((float)Math.Atan2(2f * axis.X * rotationAngle.Radians - 2f * axis.Y * axis.Z, 1 - 2f * Math.Pow(axis.X, 2f) - 2f * Math.Pow(axis.Z, 2f)));

            float sqw, sqx, sqy, sqz;
            sqw = (float)Math.Pow(quaternion.W, 2f);
            sqx = (float)Math.Pow(quaternion.X, 2f);
            sqy = (float)Math.Pow(quaternion.Y, 2f);
            sqz = (float)Math.Pow(quaternion.Z, 2f);

            byX = new Angle((float)Math.Atan2(2.0f * (quaternion.Y * quaternion.Z + quaternion.X * quaternion.W), (-sqx - sqy + sqz + sqw)));
            byY = new Angle((float)Math.Asin(-2.0f * (quaternion.X * quaternion.Z - quaternion.Y * quaternion.W)));
            byZ = new Angle((float)Math.Atan2(2.0f * (quaternion.X * quaternion.Y + quaternion.Z * quaternion.W), (sqx - sqy - sqz + sqw)));

            return new RotationVector(byX, byY, byZ);
        }

        public override Matrix GetRotationMatrix()
        {
            Matrix mat = Matrix.Identity;
            mat.RotateAxis(axis, rotationAngle.Radians);
            return mat;
        }

        public override void Normalize()
        {
            if (!rotationAngle.Normalized)
            {
                rotationAngle.Normalize();
                quaternion.RotateAxis(axis, rotationAngle.Radians);
            }
        }
    }
}
