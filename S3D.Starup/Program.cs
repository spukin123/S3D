using System;
using System.Linq;
using System.Threading.Tasks;

namespace S3D.Starup
{
    class Program
    {
        private static Func<double, double> circle = (x) => { return Math.Cos(Math.Asin(x / 3.0)) * 3.0; };
        private static Func<double, double> squareRoot = (x) => { return Math.Sqrt(x); };
        private static Func<double, double> exp = (x) => { return Math.Pow(x, 2); };

        private static int PhysObjectsCount = 1000;

        static void Main(string[] args)
        {
            #region Old
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


            //S3D.Core.Base.Mesh3D mesh = S3D.Core.Base.AlternativeMath.GetFigure3DMesh(circle, 0, 3, 2);

            //PrintArray<S3D.Core.Base.AltPoint3D>(
            //    mesh.Vertices.ToArray(),
            //    (x) =>
            //    {
            //        S3D.Core.Base.AltPoint3D vertice = (S3D.Core.Base.AltPoint3D)x;
            //        Console.Write("[X={0} Y={1} Z={2}]", vertice.X, vertice.Y, vertice.Z);
            //        Console.WriteLine();
            //    });

            //Console.WriteLine();

            //Console.WriteLine("Count: {0} ", mesh.Vertices.Count);

            //PrintArray<S3D.Core.Base.AltPoint3D>(
            //    mesh.Indices.ToArray(),
            //    (x) =>
            //    {
            //        int index = (int)x;
            //        Console.Write("Index={0}, ", index);
            //    });

            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine();
            #endregion

            Test(() =>
            {
                for (int i = 0; i < PhysObjectsCount; i++)
                {
                    for (int k = i; k < PhysObjectsCount; k++)
                    {
                        Load();
                    }
                }
            });

            //No sense.
            //Test(() =>
            //{
            //    Parallel.For(0, 1000000, x =>
            //    {
            //        Load();
            //    });
            //});
        }

        private static void ComputeEqualities()
        {
            //Nu dopustim OK.
            const int EqPower = 10;
            for (int i = 0; i < EqPower; i++)
            {
                var y = 1 / 3.0;
                var c = Math.Sqrt(Math.PI);
                Math.Cos(Math.PI);
            }
        }

        private static void Load()
        {
            ComputeEqualities();

            //int sidesCount = 6;
            //for (int i = 0; i < sidesCount; i++)
            //{
            //    for (int k = 0; k < sidesCount; k++)
            //    {
            //        Math.Sqrt(Math.PI);
            //        var res = Math.PI / Math.PI;
            //    }
            //}

            //Tut zhopa.
            //Math.Cos(Math.PI);
            //Math.Cos(Math.PI);
            //Math.Cos(Math.PI);
        }

        private static void Test(Action body)
        {
            DateTime ms1 = DateTime.Now;

            body();
            
            DateTime ms2 = DateTime.Now;
            Console.WriteLine("Lapse time = {0}", ms2 - ms1);
            Console.WriteLine("FPS = {0}", (int)(1000.0 / (ms2.Millisecond - ms1.Millisecond)));
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
