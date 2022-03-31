using S3D.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Media3D;
using System.Linq;
using System.Windows.Media;
using Microsoft.DirectX;

namespace S3D.Render
{
    public static class ConvertHelper
    {
        public static Point3DCollection GetPositions(List<AltPoint3D> points)
        {
            return new Point3DCollection(points.Select(x => {
                return new Point3D(x.X, x.Y, x.Z);
            }));
        }

        public static Int32Collection GetIndices(List<int> indices)
        {
            return new Int32Collection(indices);
        }

        public static Vector3D FromDXVector(Vector3 vec)
        {
            return new Vector3D(vec.X, vec.Y, vec.Z);
        }

        public static Vector3 ToDXVector(Vector3D vec)
        {
            return new Vector3((float)vec.X, (float)vec.Y, (float)vec.Z);
        }

        public static Vector3 ToDXVector(Point3D point)
        {
            return new Vector3((float)point.X, (float)point.Y, (float)point.Z);
        }
    }
}
