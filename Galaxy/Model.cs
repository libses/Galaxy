using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy
{
    public static class Model
    {
        public static readonly List<Star> stars = new List<Star>();

        public static readonly List<Star> starsRemove = new List<Star>();

        public static readonly List<Star> starsAdd = new List<Star>();

        public static readonly List<Trace> traces = new List<Trace>();

        static readonly int xRes = 1600;

        static readonly int yRes = 800;

        public static Random random = new Random();

        public static Color GetColor(double speed)
        {
            speed /= 2;
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
                G = (1.39 - speed / 1.8) * 255;
            }
            if (speed > 2.5)
            {
                G = 0;
            }
            if (speed <= 0.7)
            {
                R = (speed * 1.42) * 253;
            }
            else
            {
                R = (1 + (0.7 - speed) / 4.6) * 253;
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

        public static Trace MoveStarAndGetTrace(Star first)
        {
            var oldLocation = first.Location;
            first.Location += first.Speed + first.Acceleration * (1d / 2d);
            first.Speed += first.Acceleration;
            return new Trace(oldLocation, first.Location, first.Speed.Length, first.Mass, 10);
        }
        
        public static double GetRandomP(Random random)
        {
            double u1 = 1.0 - random.NextDouble();
            double u2 = 1.0 - random.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal =
             0.5 + 0.15 * randStdNormal;
            return randNormal;
        }

        public static double GetRandomM(Random random)
        {
            double u1 = 1.0 - random.NextDouble();
            double u2 = 1.0 - random.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal =
             3 + 2 * randStdNormal;
            if (randNormal > 0) return randNormal;
            else return 3;
        }

        public static void CreateRandomStars(int starCount)
        {
            var center = new Vector(xRes / 2, yRes / 2);
            for (int i = 0; i < starCount; i++)
            {
                var newStar = new Star(GetRandomP(random) * xRes, GetRandomP(random) * yRes, GetRandomM(random));
                var toCenter = newStar.Location - center;

                newStar.Speed = new Vector(toCenter.Y/400, toCenter.X / -400);
                //newStar.Acceleration = toCenter * -10;
                stars.Add(newStar);
            }
            stars.Add(new Star(xRes / 2, yRes / 2, 30000));
        }

        public static void Iterate()
        {
            traces.Clear();
            for (int i = 0; i < stars.Count; i++)
            {
                var first = stars[i];
                first.Acceleration = new Vector(0, 0);
                for (int j = i + 1; j < stars.Count; j++)
                {
                    ApplyGravitation(first, stars[j], 0.005);
                    //Physics.TryMergeStars();
                }
                traces.Add(MoveStarAndGetTrace(first));
            }
            ReplaceStars(starsRemove, starsAdd);
        }

        public static void ReplaceStars(List<Star> starsRemove, List<Star> starsAdd)
        {
            starsRemove.Select(x => stars.Remove(x));
            starsRemove.Clear();
            foreach (var star in starsAdd)
            {
                stars.Add(star);
            }
            starsAdd.Clear();
        }

        public static void ApplyGravitation(Star first, Star second, double G)
        {
            var force = Physics.GetGraviForce(first, second, G, 2);
            first.Acceleration += (force * -1) * (1 / first.Mass);
            second.Acceleration += force * (1 / second.Mass);
        }

    }
}
