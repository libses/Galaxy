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
            var rand = new Random();
            if ((first.Location - second.Location).Length < 0.1 && rand.NextDouble() > 1)
            {
                Model.starsRemove.Add(first);
                Model.starsRemove.Add(second);
                var star = new Star(first.Location.X, second.Location.Y, first.Mass + second.Mass);
                star.Acceleration = first.Acceleration + second.Acceleration;
                star.Speed = first.Speed + second.Speed;
                Model.starsAdd.Add(star);
            }
            var force =  first.Mass * second.Mass * 0.003  / (((first.Location  - second.Location).Length + 65) * ((first.Location - second.Location).Length + 65));
            return (first.Location - second.Location).Normal * force;
        }
    }
}
