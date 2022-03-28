﻿using System;
using System.Linq;

namespace S3D.Starup
{
    class Program
    {
        private static Func<double, double> circle = (x) => { return Math.Cos(Math.Asin(x / 3.0)) * 3.0; };
        private static Func<double, double> squareRoot = (x) => { return Math.Sqrt(x); };
        private static Func<double, double> exp = (x) => { return Math.Pow(x, 2); };

        static void Main(string[] args)
        {
            //Console.WriteLine("Sphere volume = {0}(cm3)", 
            //    AlternativeMath.Actual.AlternativeMath.GetDiscreteSphere3DVolume(3, 1000));

            //Console.WriteLine("Sphere volume by spline = {0}(cm3)",
            //    AlternativeMath.Actual.AlternativeMath.GetDiscreteFigure3DVolume(circle,
            //    0, 3, 1000));

            //Console.WriteLine("Figure volume = {0}(cm3)",
            //    AlternativeMath.Actual.AlternativeMath.GetDiscreteFigure3DVolume(squareRoot,
            //    0, 4, 1000));

            //Console.WriteLine("Exponent volume = {0}(cm3)",
            //    AlternativeMath.Actual.AlternativeMath.GetDiscreteFigure3DVolume(exp,
            //    0, 2, 1000));


            S3D.Core.Base.Mesh3D mesh = S3D.Core.Base.AlternativeMath.GetFigure3DMesh(circle, 0, 3, 4);

            PrintArray<S3D.Core.Base.AltPoint3D>(
                mesh.Vertices.ToArray(),
                (x) =>
                {
                    S3D.Core.Base.AltPoint3D vertice = (S3D.Core.Base.AltPoint3D)x;
                    Console.Write("[X={0} Y={1} Z={2}]", vertice.X, vertice.Y, vertice.Z);
                    Console.WriteLine();
                });

            Console.WriteLine();

            Console.WriteLine("Count: {0} ", mesh.Vertices.Count);

            PrintArray<S3D.Core.Base.AltPoint3D>(
                mesh.Indices.ToArray(),
                (x) =>
                {
                    int index = (int)x;
                    Console.Write("Index={0}, ", index);
                });

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void PrintArray<T>(Array array, Action<object> item)
        {
            for (int i = 0; i < array.Length; i++)
            {
                item(array.GetValue(i));
            }
        }
    }
}
