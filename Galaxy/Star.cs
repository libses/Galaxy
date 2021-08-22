using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy
{
    public class Star
    {
        public Vector Location { get; set; }
        
        public Vector Speed { get; set; }

        public Vector Acceleration { get; set; }
        
        public double Mass { get; set; }

        public Star(double x, double y, double m)
        {
            Location = new Vector(x, y);
            Speed = new Vector(0, 0);
            Acceleration = new Vector(0, 0);
            Mass = m;
        }
    }
}
