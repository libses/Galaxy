using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Galaxy
{
    public partial class Form1 : Form
    {
        public Color GetColor(double speed)
        {
            speed = speed / 2;
            //5 is max
            //0.7 is white green x264
            //0.5 start greening
            //2.5 end greening
            double G = 0;
            double R = 0;
            double B = 0;
            if (speed >= 0.5 && speed <= 0.7)
            {
                G = (speed - 0.5) * 1275;
            }
            if (speed >= 0.7 && speed <= 2.5)
            {
                G = (1.39 - speed / 1.8)*255;
            }
            if (speed <= 0.7)
            {
                R = (speed * 1.42)*253;
            } 
            else
            {
                R = (1 + (0.7 - speed)/4.6)*253;
            }
            if (speed >= 0.5 && speed <= 0.7)
            {
                B = (speed - 0.5) * 1275;
            }
            if (speed > 0.7) { B = 255; }
            if (R < 0) R = 0;
            if (R > 255) R = 255;
            if (B > 255) B = 255;
            return Color.FromArgb((int)R, (int)G, (int)B);
        }
        public Form1()
        {
            Model.CreateRandomStars(210);
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
                args.Graphics.Clear(Color.FromArgb(10,10,30));
                foreach (var star in Model.stars)
                {
                    args.Graphics.FillEllipse((new Pen(GetColor(star.Speed.Length))).Brush,
                        (float)star.Location.X,
                        (float)star.Location.Y,
                        (float)(star.Mass),
                        (float)(star.Mass));
                }
            };
        }
    }
}
