using Gds.LiteConstruct.Rendering;
using S3D.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace S3D.Render
{
    public class Scene
    {
        private static Func<double, double> circle = (x) => { return Math.Cos(Math.Asin(x / 3.0)) * 3.0; };
        private static Func<double, double> squareRoot = (x) => { return Math.Sqrt(x); };
        private static Func<double, double> exp = (x) => { return Math.Pow(x, 2); };

        private readonly Viewport3D v3D;

        private MeshGeometry3D mesh3D;
        private GeometryModel3D model;

        private readonly RotatableCamera camera;

        public Scene(Viewport3D v3D)
        {
            this.v3D = v3D;

            PerspectiveCamera pCamera = v3D.Camera as PerspectiveCamera;
            camera = new RotatableCamera(pCamera, ConvertHelper.ToDXVector(pCamera.Position), 
                ConvertHelper.ToDXVector(pCamera.LookDirection));
        }

        public void RenderApply()
        {
            model = (v3D.Children[1] as ModelVisual3D).Content as GeometryModel3D;
            mesh3D = model.Geometry as MeshGeometry3D;
            
            Mesh3D mesh = AlternativeMath.GetFigure3DMesh(circle, 0, 3, 2);

            mesh3D.Positions = ConvertHelper.GetPositions(mesh.Vertices);
            mesh3D.TriangleIndices = ConvertHelper.GetIndices(mesh.Indices);
        }

        public void Rotate(double x, double y, double z)
        {
            ((model.Transform as RotateTransform3D).Rotation as AxisAngleRotation3D).Angle = z;
        }
    }
}
