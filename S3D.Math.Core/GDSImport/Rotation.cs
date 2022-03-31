using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.BusinessObjects
{
    [Serializable]
    public abstract class Rotation
    {
        public abstract Matrix GetRotationMatrix();
        public abstract void Normalize();
    }
}
