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
        /// Air density
        /// </summary>
        public static double P = 9;//1.225; //air = 1.225, //water = 9, //no resistance = 9

        /// <summary>
        /// Drag coefficient for a sphere
        /// </summary>
        public const double Cd = 0.47;

        /// <summary>
        /// Return the destination point calculated using current position,
        /// current velocity, current acceleration, and delta time
        /// </summary>
        /// <param name="v">velocity</param>
        /// <param name="a">acceleration</param>
        /// <param name="position">position</param>
        /// <returns>destination point</returns>
        public static Point DestinationPosition(Velocity v, Vector a, double t, Point position)
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
        public static Vector BoundaryCollision(Point location, int radius, int width, int height)
        {
            Vector v = new Vector(0, 0);
            if (location.X + radius > width || location.Y + radius > height || 
                location.X - radius < 0 || location.Y - radius < 0)
            {
                int[] min = new int[4];
                min[0] = -(location.X - radius);
                min[1] = location.X + radius - width;
                min[2] = -(location.Y - radius);
                min[3] = location.Y + radius - height;
                int minD = int.MaxValue;
                for (int i = 0; i < min.Length; i++)
                {
                    if (minD > min[i] && min[i] >= 0)
                    {
                        minD = min[i];
                        if (i < 2)
                        {
                            v.X = minD;
                            v.Y = 0;
                        }
                        else
                        {
                            v.X = 0;
                            v.Y = minD;
                        }
                    }
                }
                return v;
            }
            return v;
        }
        
        /// <summary>
        /// Return the new velocity based on current velocity, acceleration, and delta time
        /// </summary>
        /// <param name="v">Velocity</param>
        /// <param name="a">Acceleration</param>
        /// <param name="t">Time elapsed</param>
        /// <returns>New velocity</returns>
        public static Velocity CalcVelocity(Velocity v, Vector a, double t)
        {
            Velocity newV = new Velocity();
            newV.X = v.X + a.X * t / 1000;
            newV.Y = v.Y + a.Y * t / 1000;
            return newV;
        }

        public static Vector CalcAcceleration(Velocity v, double m, double radius)
        {
            Vector a = new Vector(0, 0);
            int sY;
            int sX;
            if (v.Y < 0)
            {
                sY = -1;
            }
            else
            {
                sY = 1;
            }
            if (v.X < 0)
            {
                sX = -1;
            }
            else
            {
                sX = 1;
            }
            a.X = (100 - (P * Math.PI * radius / 100 * radius / 100 * v.X * v.X * sX * 0.5 * Cd)) / 100;
            a.Y = (GRAVITY * 100 - (P * Math.PI * radius/100 * radius/100 * v.Y *v.Y *sY*0.5 * Cd)) / 100;
            return a;
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

        /// <summary>
        /// Determine how much time elapsed to the point of collision
        /// </summary>
        /// <param name="v">velocity</param>
        /// <param name="a">acceleration</param>
        /// <param name="d">distance traveled</param>
        /// <returns></returns>
        public static double TimeElapsed(double v, double a, int d)
        {
            double t1 = (-v + Math.Sqrt((v * v - (4 * 0.5 * -d)))) / 100;
            double t2 = (-v - Math.Sqrt((v * v - (4 * 0.5 * -d)))) / 100;
            return t1 > t2 ? t1 : t2;
        }
    }
}
