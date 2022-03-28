using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace S3D.Render
{
    public class Scene
    {
        private readonly Viewport3D v3D;

        public Scene(Viewport3D v3D)
        {
            this.v3D = v3D;
        }

        public void RenderApply()
        {
            GeometryModel3D model = (v3D.Children[1] as ModelVisual3D).Content as GeometryModel3D;
            MeshGeometry3D mesh3D = model.Geometry as MeshGeometry3D;

            //Next here.
        }
    }
}
