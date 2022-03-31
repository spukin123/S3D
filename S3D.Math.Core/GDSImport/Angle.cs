using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.BusinessObjects
{
    [Serializable]
    public struct Angle
    {
        private float radians;

        public float Radians
        {
            get { return radians; }
        }

        public float Degrees
        {
            get { return RadiansToDegrees(radians); }
        }

        public Angle Inverse
        {
            get { return new Angle(-radians); }
        }

        public static readonly float Pi = 3.14159F;

        #region Angle values

        public static Angle A0
        {
            get { return new Angle(0f); }
        }

        public static Angle A10
        {
            get { return new Angle(Pi / 18f); }
        }

        public static Angle A15
        {
            get { return new Angle(Pi / 12f); }
        }

        public static Angle A30
        {
            get { return new Angle(Pi / 6f); }
        }

        public static Angle A45
        {
            get { return new Angle(Pi / 4f); }
        }

        public static Angle A60
        {
            get { return new Angle(Pi / 3f); }
        }

        public static Angle A90
        {
            get { return new Angle(Pi / 2f); }
        }

        public static Angle A120
        {
            get { return new Angle(Pi / 1.5f); }
        }

        public static Angle A180
        {
            get { return new Angle(Pi); }
        }

		public static Angle A270
		{
			get { return new Angle(Pi * 1.5f); }
		}

        public static Angle A360
        {
            get { return new Angle(Pi * 2f); }
        }

        #endregion

        public Angle(float radians)
        {
            this.radians = radians;
        }

        public static Angle FromRadians(float radians)
        {
            return new Angle(radians);
        }

        public static Angle FromDegrees(float degrees)
        {
            return new Angle(DegreesToRadians(degrees));
        }

        public static float DegreesToRadians(float degrees)
        {
            return (degrees * Pi / 180F);
        }

        public static float RadiansToDegrees(float radians)
        {
            return (radians * 180F / Pi);
        }

        public bool Normalized
        {
            get { return !(this > Angle.A360 || this < -Angle.A360); }
        }

        public void Normalize()
        {
            int sign;
            sign = (int)(Radians / (float)Math.Abs(Radians));
            radians = sign * ((float)Math.Abs(Radians) - Angle.A360.Radians);
        }

        #region Operators

        public static Angle operator +(Angle a1, Angle a2)
        {
            return new Angle(a1.radians + a2.radians);
        }

        public static Angle operator -(Angle a1, Angle a2)
        {
            return new Angle(a1.radians - a2.radians);
        }

        public static Angle operator -(Angle a1)
        {
            return new Angle(-a1.radians);
        }

        public static bool operator <(Angle a1, Angle a2)
        {
            if (a1.radians < a2.radians)
                return true;
            else
                return false;
        }

        public static bool operator >(Angle a1, Angle a2)
        {
            if (a1.radians > a2.radians)
                return true;
            else
                return false;
        }

        public static bool operator <=(Angle a1, Angle a2)
        {
            if (a1.radians <= a2.radians)
                return true;
            else
                return false;
        }

        public static bool operator >=(Angle a1, Angle a2)
        {
            if (a1.radians >= a2.radians)
                return true;
            else
                return false;
        }

        public static bool operator ==(Angle a1, Angle a2)
        {
            return a1.Radians == a2.Radians;
        }

        public static bool operator !=(Angle a1, Angle a2)
        {
            return !(a1 == a2);
        }

        #endregion

		public override int GetHashCode()
		{
			return radians.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return obj is Angle && this == (Angle)obj;
		}
    }
}
