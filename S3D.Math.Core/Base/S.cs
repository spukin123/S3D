using System;
using System.Collections.Generic;
using System.Text;

namespace S3D.Core.Base
{
    public static class S
    {
        public static void Show(double number)
        {
            Show(number.ToString());
        }

        public static void Show(float number)
        {
            Show(number.ToString());
        }

        public static void Show(int number)
        {
            Show(number.ToString());
        }

        public static void Show(string str)
        {
            Console.WriteLine(str);
        }
    }
}
