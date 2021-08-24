using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy
{
    public static class Physics
    {
        public static void TryMergeStars(Star first, Star second, double chance, double distance)
        {
            var rand = new Random();
            if ((first.Location - second.Location).Length < distance 
                && rand.NextDouble() < chance)
            {
                Model.starsRemove.Add(first);
                Model.starsRemove.Add(second);
                var star = new Star(first.Location.X, second.Location.Y, first.Mass + second.Mass)
                {
                    Acceleration = first.Acceleration + second.Acceleration,
                    Speed = first.Speed + second.Speed
                };
                Model.starsAdd.Add(star);
            }
        }
        public static Vector GetGraviForce(Star first, Star second, double G)
        {
            var force =  first.Mass * second.Mass * G  / (((first.Location  - second.Location).Length + 65) * ((first.Location - second.Location).Length + 65));
            return (first.Location - second.Location).Normal * force;
        }
    }
}
