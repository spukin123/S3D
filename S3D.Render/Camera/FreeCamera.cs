using System;
using System.Collections.Generic;
using System.Text;
//using Microsoft.DirectX;
using Gds.LiteConstruct.BusinessObjects;
using System.Windows.Media.Media3D;

namespace Gds.LiteConstruct.Rendering
{
    public class FreeCamera : CameraBase
    {
        public FreeCamera(PerspectiveCamera pCamera, Vector3 position, Vector3 look)
            : base(pCamera, position, position + look)
        {
        }

        public override bool CanMove
        {
            get { return true; }
        }

        public override void RotateCameraVertically(Angle angle)
        {
            Vector3 rotVec, look;
            Matrix matrix = Matrix.Identity;

            angle = (InverseVertically) ? angle.Inverse : angle;

            if (curVerAngle + angle < minVerAngle)
                angle = minVerAngle - curVerAngle;
            else
                if (curVerAngle + angle > maxVerAngle)
                    angle = maxVerAngle - curVerAngle;


            rotVec = Vector3.Cross(Look, new Vector3(0.0f, 0.0f, 1.0f));
            matrix = Matrix.RotationAxis(rotVec, angle.Radians);
            look = Vector3.TransformCoordinate(Look, matrix);
            lookAt = position + look;

            curVerAngle += angle;
        }

        public override void RotateCameraHorizontally(Angle angle)
        {
            Matrix matrix = Matrix.Identity;
            Vector3 look;

            angle = (InverseHorizontally) ? angle.Inverse : angle;

            matrix = Matrix.RotationZ(angle.Radians);
            look = Vector3.TransformCoordinate(Look, matrix);
            lookAt = position + look;
        }

        public override void Zoom(float length)
        {
            Vector3 displacement;
            displacement = Vector3Utils.SetLength(Look, length);
            position += displacement;
            lookAt += displacement;
        }

        public void MoveForward(float length)
        {
            Vector3 look = Look;
            position += Vector3Utils.SetLength(new Vector3(Look.X, Look.Y, 0f), length);
            lookAt = position + look;
        }

        public void MoveSide(float length)
        {
            Vector3 look = Look;
            Vector3 vector = new Vector3(-Look.Y, Look.X, 0.0f);
            position += Vector3Utils.SetLength(vector, length);
            lookAt = position + look;
        }
    }
}
