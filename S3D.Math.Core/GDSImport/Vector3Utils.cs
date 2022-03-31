using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;
using System.Drawing;

namespace Gds.LiteConstruct.BusinessObjects
{
    public static class Vector3Utils
    {
        static public Vector3 SetLength(Vector3 vector, float length)
        {
            return Vector3.Scale(vector, length / vector.Length());
        }

        static public Angle AngleBetweenVectors(Vector3 vector1, Vector3 vector2)
        {
            float val = Vector3.Dot(vector1, vector2) / (vector1.Length() * vector2.Length());
            return Angle.FromRadians((float)Math.Acos((double)val));
        }

        static public float DistanceBetweenPoints(Vector3 vector1, Vector3 vector2)
        {
            Vector3 vector3 = vector1 - vector2;
            return vector3.Length();
        }

        static public Vector3 RotateVectorByAxis(Vector3 vector, Vector3 axis, Angle angle)
        {
            Matrix matrix = Matrix.Identity;
            matrix.RotateAxis(axis, angle.Radians);
            return Vector3.TransformCoordinate(vector, matrix);
        }

        static public Vector3 RotateVectorByZ(Vector3 vector, Angle angle)
        {
            return RotateVectorByAxis(vector, AlignedZVector, angle);
        }

        static public Vector3 AlignedXVector
        {
            get { return new Vector3(1f, 0f, 0f); }
        }

        static public Vector3 AlignedYVector
        {
            get { return new Vector3(0f, 1f, 0f); }
        }

        static public Vector3 AlignedZVector
        {
            get { return new Vector3(0f, 0f, 1f); }
        }

        static public Vector3 ZeroVector
        {
            get { return new Vector3(0f, 0f, 0f); }
        }

        static public bool VectorsHaveSameDirection(Vector3 vector1, Vector3 vector2)
        {
            return AngleBetweenVectors(vector1, vector2) < Angle.A90;
        }
        
        public static Vector3 Cross(Vector3 vec1, Vector3 vec2)
        {
            return new Vector3(vec1.Y * vec2.Z - vec1.Z * vec2.Y, vec1.Z * vec2.X - vec1.X * vec2.Z, vec1.X * vec2.Y - vec1.Y * vec2.X);
        }
        
        static public Angle TransitionAngleBetweenVectors(Vector3 startVec, Vector3 endVec, Vector3 positiveAxis)
        {
            Vector3 perp;

            perp = Vector3.Cross(startVec, endVec);
            if (Vector3Utils.VectorsHaveSameDirection(perp, positiveAxis))
            {
                Angle a = Vector3Utils.AngleBetweenVectors(startVec, endVec);
                //if ((decimal)a.Degrees < decimal.MinValue || (decimal)a.Degrees > decimal.MaxValue)
                //{
                //    int b;
                //    b = 5;
                //}
 
                return a;
            }
            else
            {
                Angle a = -Vector3Utils.AngleBetweenVectors(startVec, endVec);
                //if ((decimal)a.Degrees < decimal.MinValue || (decimal)a.Degrees > decimal.MaxValue)
                //{
                //    int b;
                //    b = 5;
                //}
                
                return a;
            }
        }

        static public Vector3 GetNormalizedPerpendicularTo(Vector3 vector)
        {
            Vector3 someVec = new Vector3(1.0001367f, 1.0007538f, 1.00052365f);
            return Vector3.Normalize(someVec - vector * (Vector3.Dot(someVec, vector) / Vector3.Dot(vector, vector)));
        }

        public static AxisAngle TransitionRotationByAxis(Vector3 startVec, Vector3 endVec)
        {
            Vector3 axis;
            axis = Vector3.Cross(startVec, endVec);

            Angle angle;
            angle = Vector3Utils.AngleBetweenVectors(startVec, endVec);

            return new AxisAngle(axis, angle);
        }

        static public RotationVector TransitionRotationByAxises(Vector3 startVec, Vector3 endVec)
        {
            Vector3 startVecProj, endVecProj;
            RotationVector rotation = new RotationVector();

            //x - axis
            startVecProj = new Vector3(0f, startVec.Y, startVec.Z);
            endVecProj = new Vector3(0f, endVec.Y, endVec.Z);

            float precision = 0.00001f;

            if (Vector3Utils.IsZeroVector(startVecProj, precision) ||
                Vector3Utils.IsZeroVector(endVecProj, precision) ||
                (Vector3Utils.IsAlignedY(startVecProj, precision) &&
                Vector3Utils.IsAlignedY(startVecProj, precision)) ||
                (Vector3Utils.IsAlignedZ(endVecProj, precision) &&
                Vector3Utils.IsAlignedZ(endVecProj, precision)))
            {
                rotation.X = Angle.A0;
            }
            else
            {
                rotation.X = Vector3Utils.TransitionAngleBetweenVectors(startVecProj, endVecProj, Vector3Utils.AlignedXVector);
            }

            //y - axis
            startVecProj = new Vector3(startVec.X, 0f, startVec.Z);
            endVecProj = new Vector3(endVec.X, 0f, endVec.Z);

            if (Vector3Utils.IsZeroVector(startVecProj, precision) ||
                Vector3Utils.IsZeroVector(endVecProj, precision) ||
                (Vector3Utils.IsAlignedX(startVecProj, precision) &&
                Vector3Utils.IsAlignedX(startVecProj, precision)) ||
                (Vector3Utils.IsAlignedZ(endVecProj, precision) &&
                Vector3Utils.IsAlignedZ(endVecProj, precision)))
            {
                rotation.Y = Angle.A0;
            }
            else
            {
                rotation.Y = Vector3Utils.TransitionAngleBetweenVectors(startVecProj, endVecProj, Vector3Utils.AlignedYVector);
            }

            //z - axis
            startVecProj = new Vector3(startVec.X, startVec.Y, 0f);
            endVecProj = new Vector3(endVec.X, endVec.Y, 0f);

            if (Vector3Utils.IsZeroVector(startVecProj, precision) ||
                Vector3Utils.IsZeroVector(endVecProj, precision) ||
                (Vector3Utils.IsAlignedX(startVecProj, precision) &&
                Vector3Utils.IsAlignedX(startVecProj, precision)) ||
                (Vector3Utils.IsAlignedY(endVecProj, precision) &&
                Vector3Utils.IsAlignedY(endVecProj, precision)))
            {
                rotation.Z = Angle.A0;
            }
            else
            {
                rotation.Z = Vector3Utils.TransitionAngleBetweenVectors(startVecProj, endVecProj, Vector3Utils.AlignedZVector);
            }

            return rotation;
        }

        public static bool IsAlignedX(Vector3 vector, float precision)
        {
            bool yAxis = false, zAxis = false;

            ValuesComparer.Precision = precision;
            if (ValuesComparer.FloatValuesEqual(0f, vector.Y))
            {
                yAxis = true;
            }
            if (ValuesComparer.FloatValuesEqual(0f, vector.Z))
            {
                zAxis = true;
            }

            return yAxis && zAxis;
        }

        public static bool IsAlignedY(Vector3 vector, float precision)
        {
            bool xAxis = false, zAxis = false;

            ValuesComparer.Precision = precision;
            if (ValuesComparer.FloatValuesEqual(0f, vector.X))
            {
                xAxis = true;
            }
            if (ValuesComparer.FloatValuesEqual(0f, vector.Z))
            {
                zAxis = true;
            }

            return xAxis && zAxis;
        }

        public static bool IsAlignedZ(Vector3 vector, float precision)
        {
            bool xAxis = false, yAxis = false;

            ValuesComparer.Precision = precision;
            if (ValuesComparer.FloatValuesEqual(0f, vector.X))
            {
                xAxis = true;
            }
            if (ValuesComparer.FloatValuesEqual(0f, vector.Y))
            {
                yAxis = true;
            }

            return xAxis && yAxis;
        }

        public static Point[] ProjectToScreen(Vector3[] vectors, Matrix viewMat, Matrix projMat, int width, int height)
        {
            Matrix mat = Matrix.Identity * viewMat * projMat;
            Point[] output = new Point[vectors.Length];

            Vector4 v4;
            for (int cnt1 = 0; cnt1 < vectors.Length; cnt1++)
            {
                v4 = Vector4.Transform(new Vector4(vectors[cnt1].X, vectors[cnt1].Y, vectors[cnt1].Z, 1f), mat);
                output[cnt1] = new Point((int)((v4.X / v4.W + 1) * (width / 2)), (int)((1 - v4.Y / v4.W) * (height / 2)));
            }

            return output;
        }

        static public bool IsZeroVector(Vector3 vector, float precision)
        {
            bool xEqualZero, yEqualZero, zEqualZero;
            
            ValuesComparer.Precision = precision;
            xEqualZero = ValuesComparer.FloatValuesEqual(0f, vector.X);
            yEqualZero = ValuesComparer.FloatValuesEqual(0f, vector.Y);
            zEqualZero = ValuesComparer.FloatValuesEqual(0f, vector.Z);

            return xEqualZero && yEqualZero && zEqualZero;
        }

        public static bool VectorsLieOnSameLine(Vector3 prevVec, Vector3 curVec, float precision)
        {
            prevVec = Vector3Utils.SetLength(prevVec, curVec.Length());

            bool res1, res2, res3;

            ValuesComparer.Precision = precision;
            res1 = ValuesComparer.FloatValuesEqual(curVec.X, prevVec.X);
            res2 = ValuesComparer.FloatValuesEqual(curVec.Y, prevVec.Y);
            res3 = ValuesComparer.FloatValuesEqual(curVec.Z, prevVec.Z);

            return res1 && res2 && res3;
        }
    }
}
