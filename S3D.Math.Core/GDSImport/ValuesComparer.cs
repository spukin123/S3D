using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.BusinessObjects
{
    public static class ValuesComparer
    {
        private static float precision;

        public static float Precision
        {
            get { return precision; }
            set { precision = value; }
        }

        public static bool FloatValuesEqual(float value1, float value2)
        {
            return Math.Abs(value1 - value2) <= precision;
        }

        public static bool FloatValuesEqual(float value1, float value2, float precision)
        {
            return Math.Abs(value1 - value2) <= precision;
        }
    }
}
