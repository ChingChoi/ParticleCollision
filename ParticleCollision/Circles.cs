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
        struct Velocity
        {
            float x;
            float y;

            public Velocity(float x, float y)
            {
                this.x = x;
                this.y = y;
            }

            public float X { get => x; set => x = value; }
            public float Y { get => y; set => y = value; }
        }
        private int radius;
        private int x;
        private int y;
        private Pen pen;
        private SolidBrush brush;
        private Velocity velocity;

        /// <summary>
        /// Circle constructor
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public MCircle(int radius, int x, int y)
        {
            this.radius = radius;
            this.x = x;
            this.y = y;
            pen = Pens.Cyan;
            brush = new SolidBrush(Color.Cyan);
            velocity = new Velocity(0, 0);
        }

        /// <summary>
        /// Draw a circle
        /// </summary>
        /// <param name="g"></param>
        public void DrawCircle(Graphics g)
        {
            g.DrawEllipse(pen, new Rectangle(
                new Point(x - radius / 2, y - radius / 2), 
                new Size(radius, radius)));
        }

        /// <summary>
        /// Draw a filled circle
        /// </summary>
        /// <param name="g"></param>
        public void DrawFilledCircle(Graphics g)
        {
            g.FillEllipse(brush, new Rectangle(
                new Point(x - radius / 2, y - radius / 2), 
                new Size(radius, radius)));
        }
    }
}
