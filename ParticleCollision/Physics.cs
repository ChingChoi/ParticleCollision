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
        double x;
        /// <summary>
        /// object's velocity in the y direction
        /// </summary>
        double y;

        /// <summary>
        /// Constructor for the velocity
        /// </summary>
        /// <param name="x">velocity in x direction</param>
        /// <param name="y">velocity in y direction</param>
        public Velocity(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Getter and setters
        /// </summary>
        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }
    }



    class Physics
    {
        /// <summary>
        /// Gravity in cm per second square
        /// each pixel represents one cm
        /// </summary>
        public const double GRAVITY = 980;

        /// <summary>
        /// Return the destination point calculated using current position,
        /// current velocity, current acceleration, and delta time
        /// </summary>
        /// <param name="v">velocity</param>
        /// <param name="a">acceleration</param>
        /// <param name="position">position</param>
        /// <returns>destination point</returns>
        public static Point DestinationPosition(Velocity v, Vector a, int t, Point position)
        {
            Point destination = new Point();
            destination.X = position.X + (int)DistanceTraveled(v.X, a.X, ParticleCollision.INTERVAL);
            destination.Y = position.Y + (int)DistanceTraveled(v.Y, a.Y, ParticleCollision.INTERVAL);
            return destination;
        }

        /// <summary>
        /// Boundary check for collision with a circle detecting range
        /// </summary>
        /// <param name="location">location</param>
        /// <param name="radius">radius</param>
        /// <param name="width">width of boundary</param>
        /// <param name="height">height of boundary</param>
        /// <returns></returns>
        public static bool BoundaryCollision(Point location, int radius, int width, int height)
        {
            if (location.X + radius > width || location.Y + radius > height || 
                location.X - radius < 0 || location.Y - radius < 0)
            {
                return true;
            }
            return false;
        }

        public static double FindFractionalTimeOnCollision(Velocity v, Vector a, Point position)
        {
            
            return 0;
        }

        /// <summary>
        /// Return the new velocity based on current velocity, acceleration, and delta time
        /// </summary>
        /// <param name="v">Velocity</param>
        /// <param name="a">Acceleration</param>
        /// <param name="t">Time elapsed</param>
        /// <returns>New velocity</returns>
        public static Velocity CalcVelocity(Velocity v, Vector a, int t)
        {
            Velocity newV = new Velocity();
            newV.X = v.X + a.X * t / 1000;
            newV.Y = v.Y + a.Y * t / 1000;
            return newV;
        }

        /// <summary>
        /// Return the distance traveled based on velocity, acceleration, and time
        /// </summary>
        /// <param name="v">Velocity</param>
        /// <param name="a">Acceleration</param>
        /// <param name="t">Time elapsed</param>
        /// <returns>distance travelled</returns>
        public static double DistanceTraveled(double v, double a, int t)
        {
            return v * t / 1000 + 0.5f * a * t / 1000 * t / 1000;
        }
    }
}
