using System;
using System.Collections.Generic;
using System.Drawing;

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

        public static List<double> GetDiffuseZeroToN(double from, double n, int discrete)
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
            Mesh3D mesh = new Mesh3D();

            mesh.AddVertice(new AltPoint3D(1, 2, 3));
            mesh.AddVertice(new AltPoint3D(5, 6, 7));
            mesh.AddVertice(new AltPoint3D(9, 10, 11));

            mesh.AddIndex(1);
            mesh.AddIndex(2);
            mesh.AddIndex(3);
            mesh.AddIndex(4);
            mesh.AddIndex(5);
            mesh.AddIndex(6);
            mesh.AddIndex(7);
            mesh.AddIndex(8);
            mesh.AddIndex(9);

            //Matrix3D

            return mesh;
        }

        private static double GetDiscreteFigure3DVolumeCore(double from, double figureHeight, int discrete, Func<double, double> splineFunc)
        {
            var figureVolume = 0.0;
            var diffuseValues = GetDiffuseZeroToN(from, figureHeight, discrete);

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
