using Gds.LiteConstruct.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Media3D;

namespace S3D.Core.MigrationTypes
{
    public static class V3M
    {
        public static Vector3 ToV3(Vector3D vec)
        {
            return new Vector3((float)vec.X, (float)vec.Y, (float)vec.Z);
        }

        public static Vector3D ToV3D(Vector3 vec)
        {
            return new Vector3D(vec.X, vec.Y, vec.Z);
        }
    }
}
