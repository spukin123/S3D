using System;
using System.Collections.Generic;
using System.Text;

namespace S3D.Core.Base
{
    public class Mesh3D
    {
        private readonly List<AltPoint3D> verticesList = new List<AltPoint3D>();
        private readonly List<int> indicesList = new List<int>();

        public Mesh3D()
        {
        }

        public void AddVertice(AltPoint3D vertice)
        {
            verticesList.Add(vertice);
        }

        public void AddIndex(int index)
        {
            indicesList.Add(index);
        }

        public AltPoint3D[] Vertices { get { return verticesList.ToArray(); } }
        
        public int[] Indices { get { return indicesList.ToArray(); } }
    }
}
