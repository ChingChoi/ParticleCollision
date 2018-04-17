using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleCollision
{
    /// <summary>
    /// Structure for holding information of an object's velocity
    /// </summary>
    public struct Velocity
    {
        /// <summary>
        /// object's velocity in the x direction
        /// </summary>
        float x;
        /// <summary>
        /// object's velocity in the y direction
        /// </summary>
        float y;

        /// <summary>
        /// Constructor for the velocity
        /// </summary>
        /// <param name="x">velocity in x direction</param>
        /// <param name="y">velocity in y direction</param>
        public Velocity(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Getter and setters
        /// </summary>
        public float X { get => x; set => x = value; }
        public float Y { get => y; set => y = value; }
    }

    class Physics
    {
        /// <summary>
        /// Gravity's acceleration
        /// </summary>
        public const float GRAVITY = 9.8f;

        //public Point CalcDistanceTraveled(Velocity v, float acceleration, Point position)
        //{
        //    return null;
        //}
    }
}
