using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy
{
    public class Vector
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }
        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y);
        }
        public static Vector operator *(Vector v, double value)
        {
            return new Vector(v.X * value, v.Y * value);
        }

        public double Length { get
            {
                return Math.Sqrt(Y * Y + X * X);
            } }
        public Vector Normal { get
            {
                if (Length == 0) { return new Vector(0, 0); }
                return this * (1 / (this.Length));
            } }
    }
}
