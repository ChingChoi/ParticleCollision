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
        private int x;
        private int y;
        private Pen pen;
        private SolidBrush brush;
        private Velocity velocity;

        /// <summary>
        /// Circle constructor
        /// </summary>
        /// <param name="radius">radius of circle</param>
        /// <param name="x">x position of circle center</param>
        /// <param name="y">y position of circle center</param>
        public MCircle(int radius, int x, int y)
        {
            this.Radius = radius;
            this.x = x;
            this.y = y;
            pen = Pens.Cyan;
            brush = new SolidBrush(Color.Cyan);
            velocity = new Velocity(0, 0);
        }

        public int Radius { get => radius; set => radius = value; }

        /// <summary>
        /// Draw a circle
        /// </summary>
        /// <param name="g">graphics object</param>
        public void DrawCircle(Graphics g)
        {
            g.DrawEllipse(pen, new Rectangle(
                new Point(x - Radius / 2, y - Radius / 2), 
                new Size(Radius, Radius)));
        }

        /// <summary>
        /// Draw a filled circle
        /// </summary>
        /// <param name="g">graphics object</param>
        public void DrawFilledCircle(Graphics g)
        {
            g.FillEllipse(brush, new Rectangle(
                new Point(x - Radius / 2, y - Radius / 2), 
                new Size(Radius, Radius)));
        }
    }
}
