using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleCollision
{
    class Vector
    {
        private double x;
        private double y;

        /// <summary>
        /// Constructor for vector
        /// </summary>
        /// <param name="x">x magnetude</param>
        /// <param name="y">y magnetude</param>
        public Vector(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Getters and setters
        /// </summary>
        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }
    }
}
