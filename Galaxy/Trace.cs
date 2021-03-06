using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy
{
    public class Trace
    {
        public Vector Start;
        public Vector End;
        public Color color;
        public Trace(Vector start, Vector end, double speed, double mass, double length)
        {
            Start = (start - end) * 10 * length + start;
            End = new Vector(end.X + mass/2, end.Y + mass/2);
            color = Model.GetColor(speed);
            color = Color.FromArgb(color.R, color.G, color.B);
        }
    }
}
