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
        public Form1()
        {
            Model.CreateRandomStars(180);
            DoubleBuffered = true;
            ClientSize = new Size(1280, 720);
            var timer = new Timer() { Interval = 17 };
            timer.Tick += (sender, args) =>
            {
                Model.Iterate();
                Invalidate();
            };
            timer.Start();
            Paint += (sender, args) =>
            {
                args.Graphics.Clear(Color.FromArgb(10,10,30));
                for (int i = 0; i < Model.stars.Count - 1; i++)
                {
                    var star = Model.stars[i];
                    args.Graphics.FillEllipse((new Pen(Model.GetColor(star.Speed.Length))).Brush,
                        (float)star.Location.X,
                        (float)star.Location.Y,
                        (float)(star.Mass / (star.Acceleration.Length + 0.2)),
                        (float)(star.Mass / (star.Acceleration.Length + 0.2)));
                    //if (Model.traces.Count > i)
                    //{
                    //    var trace = Model.traces[i];
                    //    float x1 = (float)trace.Start.X;
                    //    float x2 = (float)trace.End.X;
                    //    float y1 = (float)trace.Start.Y;
                    //    float y2 = (float)trace.End.Y;
                    //    var pen = new Pen(trace.color);
                    //    pen.Width = (float)star.Mass;
                    //    args.Graphics.DrawLine(pen, x1, y1, x2, y2);
                        
                    //}
                }
            };
        }
    }
}
