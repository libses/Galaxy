using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy
{
    public static class Model
    {
        public static readonly List<Star> stars = new List<Star>();
        static readonly int xRes = 1280;
        static readonly int yRes = 720;
        public static double GetRandom(Random random)
        {
            double u1 = 1.0 - random.NextDouble();
            double u2 = 1.0 - random.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal =
             0.5 + 0.15 * randStdNormal;
            return randNormal;
        }
        public static void CreateRandomStars(int starCount)
        {
            Random random = new Random();
            for (int i = 0; i < starCount; i++)
            {
                stars.Add(new Star(GetRandom(random) * xRes, GetRandom(random) * yRes));
            }
        }
        public static void Iterate()
        {
            for (int i = 0; i < stars.Count; i++)
            {
                stars[i].Acceleration = new Vector(0, 0);
                for (int j = i + 1; j < stars.Count; j++)
                {
                    var force = Physics.GetGraviForce(stars[i], stars[j]);
                    stars[i].Acceleration += force * -1;
                    stars[j].Acceleration += force;
                }
                stars[i].Location += stars[i].Speed + stars[i].Acceleration * (1d / 2d);
                stars[i].Speed += stars[i].Acceleration;
            }
        }

    }
}
