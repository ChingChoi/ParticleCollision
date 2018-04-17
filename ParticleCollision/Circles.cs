using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleCollision
{
    class MCircle
    {
        private int radius;
        private Point p;
        private Pen pen;
        private SolidBrush brush;
        private Velocity v;
        private bool locked;

        /// <summary>
        /// Circle constructor
        /// </summary>
        /// <param name="radius">radius of circle</param>
        /// <param name="x">x position of circle center</param>
        /// <param name="y">y position of circle center</param>
        public MCircle(int radius, Point p)
        {
            Radius = radius;
            P = new Point(p.X, p.Y);
            pen = Pens.Cyan;
            brush = new SolidBrush(Color.Cyan);
            v = new Velocity(0, 0);
            Locked = false;
        }

        /// <summary>
        /// Getters and setters
        /// </summary>
        public int Radius { get => radius; set => radius = value; }
        public bool Locked { get => locked; set => locked = value; }
        public Point P { get => p; set => p = value; }
        public Velocity V { get => v; set => v = value; }

        /// <summary>
        /// Draw a circle
        /// </summary>
        /// <param name="g">graphics object</param>
        public void DrawCircle(Graphics g)
        {
            g.DrawEllipse(pen, new Rectangle(
                new Point(P.X - Radius / 2, P.Y - Radius / 2), 
                new Size(Radius, Radius)));
        }

        /// <summary>
        /// Draw a filled circle
        /// </summary>
        /// <param name="g">graphics object</param>
        public void DrawFilledCircle(Graphics g)
        {
            g.FillEllipse(brush, new Rectangle(
                new Point(P.X - Radius / 2, P.Y - Radius / 2), 
                new Size(Radius, Radius)));
        }
    }
}
