using System;
using System.Collections.Generic;
using System.Text;

namespace S3D.Core.Base
{
    public class Mesh3D
    {
        private readonly List<AltPoint3D> verticesList = new List<AltPoint3D>();
        private readonly List<int> indicesList = new List<int>();

        private int verticeCount = 0;

        private readonly int sidesNumber;
        private int slicesCount = 2;

        public Mesh3D(int sidesNumber)
        {
            this.sidesNumber = sidesNumber;
        }

        public int SidesNumber { get { return sidesNumber; } }

        public void IncreaseSlices()
        {
            slicesCount++;
        }

        public int GetSlices()
        {
            return slicesCount;
        }

        public void AddVertice(AltPoint3D vertice)
        {
            verticesList.Add(vertice);
            verticeCount++;
        }

        public void AddVerticesRange(List<AltPoint3D> vertices)
        {
            verticesList.AddRange(vertices);
            verticeCount += vertices.Count;
        }

        private List<int> GetIndices()
        {
            for (int i = 0; i < slicesCount - 1; i++)
            {
                for (int k = i * sidesNumber; k < sidesNumber * (i + 1); k++)
                {
                    if (k == sidesNumber * (i + 1) - 1)
                    {
                        indicesList.AddRange(new int[] { k, k + sidesNumber, sidesNumber });
                        indicesList.AddRange(new int[] { k, sidesNumber, 0 });
                    }
                    else 
                    {
                        indicesList.AddRange(CreateLeftPoly(k));
                        indicesList.AddRange(CreateRightPoly(k));
                    }
                }
            }

            return indicesList;
        }

        private int[] CreateLeftPoly(int index)
        {
            return new int[] { index, index + sidesNumber, index + sidesNumber + 1 };
        }

        private int[] CreateRightPoly(int index)
        {
            return new int[] { index, index + sidesNumber + 1, index + 1 };
        }

        private List<AltPoint3D> GetMeshEndVertices()
        {
            List<AltPoint3D> endVertices = new List<AltPoint3D>();
            for (int i = verticeCount / 2; i < verticeCount; i++)
            {
                endVertices.Add(verticesList[i]);
            }

            return endVertices;
        }

        public Mesh3D Join(Mesh3D mesh)
        {
            Mesh3D joined = new Mesh3D(sidesNumber);

            joined.AddVerticesRange(Vertices);
            joined.AddVerticesRange(mesh.GetMeshEndVertices());
            joined.IncreaseSlices();

            return joined;
        }

        public static Mesh3D JoinAll(List<Mesh3D> meshes)
        {
            if (meshes.Count == 0)
                return null;

            Mesh3D joined = meshes[0];

            for (int i = 1; i < meshes.Count; i++)
            {
                joined = joined.Join(meshes[i]);
            }

            return joined;
        }

        public List<AltPoint3D> Vertices { get { return verticesList; } }

        public List<int> Indices { get { return GetIndices(); } }
    }
}
