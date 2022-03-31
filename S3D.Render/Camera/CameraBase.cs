using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;
using Gds.LiteConstruct.BusinessObjects;
using System.Windows.Media.Media3D;

namespace Gds.LiteConstruct.Rendering
{
    public abstract class CameraBase
    {
        protected PerspectiveCamera pCamera = null;
        protected Vector3 position;
        
        public Vector3 Look
        {
            get { return lookAt - position; }
        }

        protected Vector3 lookAt;

        protected Angle curVerAngle;
        protected Angle maxVerAngle = Angle.FromDegrees(60);
        protected Angle minVerAngle = Angle.FromDegrees(-60);

        private bool inverseHorizontally = false;

        public bool InverseHorizontally
        {
            get { return inverseHorizontally; }
            set { inverseHorizontally = value; }
        }

        private bool inverseVertically = false;

        public bool InverseVertically
        {
            get { return inverseVertically; }
            set { inverseVertically = value; }
        }

        protected CameraBase(PerspectiveCamera pCamera, Vector3 position, Vector3 lookAt)
        {
            this.pCamera = pCamera;
            this.position = position;
            this.lookAt = lookAt;

            Vector3 horLook;
            horLook = Look;
            horLook.Z = 0;
            this.curVerAngle = Vector3Utils.AngleBetweenVectors(Look, horLook);
            if (Look.Z < 0.0f)
            {
                this.curVerAngle = Angle.FromRadians(-curVerAngle.Radians);
            }
            if (curVerAngle > maxVerAngle || curVerAngle < minVerAngle)
            {
#warning TODO: Normalization of look
                //curVerAngle = Angle.A0;
                //look.Z = 0f;
                //look.Normalize();
            }
        }

        public Matrix GetViewMatrix()
        {
            Matrix matrix = Matrix.Identity;
            matrix = Matrix.LookAtLH(position, lookAt, new Vector3(0f, 0f, 1f));
            return matrix;
        }

        public abstract bool CanMove { get; }
        public abstract void Zoom(float length);
        public abstract void RotateCameraVertically(Angle angle);
        public abstract void RotateCameraHorizontally(Angle angle);
    }
}
