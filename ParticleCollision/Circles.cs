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
        private int diameter;
        private Point p;
        private Pen pen;
        private SolidBrush brush;
        private Velocity v;
        private bool locked;

        /// <summary>
        /// Circle constructor
        /// </summary>
        /// <param name="diameter">radius of circle</param>
        /// <param name="x">x position of circle center</param>
        /// <param name="y">y position of circle center</param>
        public MCircle(int diameter, Point p)
        {
            Diameter = diameter;
            P = new Point(p.X, p.Y);
            pen = Pens.Cyan;
            brush = new SolidBrush(Color.Cyan);
            v = new Velocity(0, 0);
            Locked = true;
        }

        /// <summary>
        /// Getters and setters
        /// </summary>
        public int Diameter { get => diameter; set => diameter = value; }
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
                new Point(P.X - Diameter / 2, P.Y - Diameter / 2), 
                new Size(Diameter, Diameter)));
        }

        /// <summary>
        /// Draw a filled circle
        /// </summary>
        /// <param name="g">graphics object</param>
        public void DrawFilledCircle(Graphics g)
        {
            g.FillEllipse(brush, new Rectangle(
                new Point(P.X - Diameter / 2, P.Y - Diameter / 2), 
                new Size(Diameter, Diameter)));
        }
    }
}
