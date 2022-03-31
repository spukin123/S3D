using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Media3D;
using Gds.LiteConstruct.BusinessObjects;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.Rendering
{
    public class RotatableCamera : CameraBase
    {
        float minDistanceToLookAt = 10f;

        public RotatableCamera(PerspectiveCamera pCamera, Vector3 position, Vector3 lookAt)
            : base(pCamera, position, lookAt)
        {
        }

        public override bool CanMove
        {
            get { return false; }
        }

        public override void RotateCameraVertically(Angle angle)
        {
            Vector3 rotVec, tempVec, look;
            Matrix matrix = Matrix.Identity;

            angle = (InverseVertically) ? angle.Inverse : angle;

            if (curVerAngle + angle < minVerAngle)
                angle = minVerAngle - curVerAngle;
            else
                if (curVerAngle + angle > maxVerAngle)
                    angle = maxVerAngle - curVerAngle;

            rotVec = Vector3.Cross(Look, new Vector3(0.0f, 0.0f, 1.0f));
            matrix = Matrix.RotationAxis(rotVec, angle.Radians);
            tempVec = -Look;
            tempVec = Vector3.TransformCoordinate(tempVec, matrix);
            look = -tempVec;
            position = lookAt - look;
            
            curVerAngle += angle;
        }

        public override void RotateCameraHorizontally(Angle angle)
        {
            Matrix matrix = Matrix.Identity;
            Vector3 look;

            angle = (InverseHorizontally) ? angle.Inverse : angle;

            matrix = Matrix.RotationZ(angle.Radians);
            look = Vector3.TransformCoordinate(-Look, matrix);
            position = lookAt + look;
        }

        public override void Zoom(float length)
        {
            Vector3 displacement;
            displacement = Vector3Utils.SetLength(Look, length);
            position += displacement;

            if (Vector3Utils.DistanceBetweenPoints(position, lookAt) < minDistanceToLookAt)
            {
                position -= lookAt;
                position = Vector3Utils.SetLength(position, minDistanceToLookAt);
            }
            else
            {
                position += displacement;
            }
        }
    }
}
