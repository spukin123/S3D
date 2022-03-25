using System;
using System.Collections.Generic;
using System.Text;

namespace S3D.Core.Base
{
    public class AltPoint3D
    {
        public AltPoint3D()
        {
        }

        public AltPoint3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }
}
