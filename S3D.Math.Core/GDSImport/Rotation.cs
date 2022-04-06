using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.BusinessObjects
{
    [Serializable]
    public abstract class Rotation
    {
        public abstract Matrix GetRotationMatrix();
        public abstract void Normalize();
    }
}
