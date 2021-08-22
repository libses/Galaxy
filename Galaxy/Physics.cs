using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy
{
    public static class Physics
    {
        public static Vector GetGraviForce(Star first, Star second)
        {
            var force =  1 / ((first.Location - second.Location).Length * (first.Location - second.Location).Length + 9);
            return (first.Location - second.Location).Normal * force;
        }
    }
}
