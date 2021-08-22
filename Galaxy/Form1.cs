using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Galaxy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            Model.CreateRandomStars(200);
            DoubleBuffered = true;
            ClientSize = new Size(1280, 720);
            var timer = new Timer() { Interval = 17 };
            timer.Tick += (sender, args) =>
            {
                Invalidate();
                Model.Iterate();
            };
            timer.Start();
            Paint += (sender, args) =>
            {
                foreach (var star in Model.stars)
                {
                    args.Graphics.DrawRectangle(new Pen(Color.Blue),
                        (float)star.Location.X,
                        (float)star.Location.Y,
                        1, 1);
                }
            };
        }
    }
}
