using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Media3D;

namespace S3D.Core.Base
{
    public static class AlternativeMath
    {
        private const int PiDiscrete = 10000;

        public static double GetCustomPi()
        {
            return PiDiscrete * Math.Sin(DegreesToRadians(180.0) / PiDiscrete);
        }

        public static double GetUsualPi()
        {
            return Math.PI;
        }

        private static double DegreesToRadians(double angle)
        {
            return angle * Math.PI / 180.0;
        }

        public static List<double> GetDiffuse(double from, double n, int discrete)
        {
            var returnData = new List<double>();
            double step = n / (discrete - 1);

            for (var k = 0; k < discrete; k++)
            {
                returnData.Add(from + k * step);
            }

            return returnData;
        }

        public static double GetCircleSquare(double radius)
        {
            return GetCustomPi() * Math.Pow(radius, 2.0);
        }
        
        //Obsolete
        //public static double GetSphereVolume2(double side)
        //{
        //    double radius = Math.Sqrt(2 * Math.Pow(side / 2.0, 2));
            
        //    return GetDiscreteSphereVolumeCore(radius, 2, x => { return SplineFunc(x, radius); });
        //}

        public static double GetDiscreteFigure3DVolume(Func<double, double> spline, double from, double height, int discrete)
        {
            return GetDiscreteFigure3DVolumeCore(from, height, discrete, spline);
        }

        public static Mesh3D GetFigure3DMesh(Func<double, double> spline, double from, double height, int discrete)
        {
            var diffuseValues = GetDiffuse(from, height, discrete);
            double curSectorX = from;

            List<Mesh3D> meshes = new List<Mesh3D>();

            for (var i = 0; i < diffuseValues.Count - 1; i++)
            {
                var h = Math.Abs(diffuseValues[i]) - Math.Abs(diffuseValues[i + 1]);
                var r = spline(from);
                var R = spline(from + h);

                Vector3D vecR = new Vector3D(curSectorX, R, 0.0);
                Vector3D vecr = new Vector3D(curSectorX, r, 0.0);

                meshes.Add(CreateSectorMesh(vecR, vecr, (int)GetSidesCount(discrete)));
                
                curSectorX += h;
            }
            
            return Mesh3D.JoinAll(meshes);
        }

        private static Mesh3D CreateSectorMesh(Vector3D largeVec, Vector3D smallVec, int sides)
        {
            Mesh3D mesh = new Mesh3D(sides);
            
            PushSectorPoints(mesh, largeVec);
            PushSectorPoints(mesh, smallVec);
            
            return mesh;
        }

        private static void PushSectorPoints(Mesh3D mesh, Vector3D vec)
        {
            Matrix3D matrix = Matrix3D.Identity;
            Quaternion q = new Quaternion(new Vector3D(-1.0, 0.0, 0.0), 360.0 / mesh.SidesNumber);
            matrix.Rotate(q);

            mesh.AddVertice(new AltPoint3D(vec.X, vec.Y, vec.Z));
            Vector3D trVec = vec;
            for (int i = 1; i < mesh.SidesNumber; i++)
            {
                trVec = matrix.Transform(trVec);
                mesh.AddVertice(new AltPoint3D(trVec.X, trVec.Y, trVec.Z)); ;
            }
        }

        private static double GetDiscreteFigure3DVolumeCore(double from, double figureHeight, int discrete, Func<double, double> splineFunc)
        {
            var figureVolume = 0.0;
            var diffuseValues = GetDiffuse(from, figureHeight, discrete);

            for (var i = 0; i < diffuseValues.Count - 1; i++)
            {
                var h = from + (diffuseValues[i + 1] - diffuseValues[i]);
                var R = splineFunc(from + diffuseValues[i]);
                var r = splineFunc(from + diffuseValues[i + 1]);

                //figureVolume += GetCutConeVolume(r, R, h);
                figureVolume += GetCutPyramidVolume(r, R, h, discrete);
            }

            return 2.0 * figureVolume;
        }

        public static double GetCutPyramidVolume(double rMin, double rMax, double h, int discrete)
        {
            Func<double, double> S = (r) =>
            {
                double a2 = DegreesToRadians(360.0 / GetSidesCount(discrete)) / 2.0;
                return GetSidesCount(discrete) * (Math.Pow(r, 2) * Math.Sin(a2) * Math.Cos(a2));
            };

            return 0;
            //return 1.0 / 3.0 * h * ();
        }

        public static double GetCutConeVolume(double rMin, double rMax, double h)
        {
            return 1.0 / 3.0 * GetUsualPi() * h * (Math.Pow(rMax, 2.0) + rMax * rMin + Math.Pow(rMin, 2.0));
        }

        private static double GetSidesCount(int discrete)
        {
            return (discrete * 4) - 4;
        }

        public static double GetIntegralStepSquare(PointF pNPrev, PointF pNNext)
        {
            return ((pNNext.X - pNPrev.X) * pNNext.Y) +
                ((pNNext.X - pNPrev.X) * pNNext.Y - (pNNext.X - pNPrev.X) * pNPrev.Y) / 2.0;
        }

        public static double GetIntegral(Func<double, double> f, double integrateTo, int discrete)
        {
            float x = 0f;
            float xStep = (float)integrateTo / discrete;

            double integral = 0.0;

            for (var i = 0; i < discrete; i++)
            {
                integral += GetIntegralStepSquare(new PointF(x, (float)f(x)),
                    new PointF(x + xStep, (float)f((double)(x + xStep))));

                x += xStep;
            }

            return integral;
        }
    }
}
