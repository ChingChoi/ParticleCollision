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
            this.pen = Pens.Cyan;
            this.brush = new SolidBrush(Color.Cyan);
        }

        /// <summary>
        /// Draw a circle
        /// </summary>
        /// <param name="g"></param>
        public void DrawCircle(Graphics g)
        {
            g.DrawEllipse(pen, new Rectangle(new Point(x, y), new Size(radius, radius)));
        }

        /// <summary>
        /// Draw a filled circle
        /// </summary>
        /// <param name="g"></param>
        public void DrawFilledCircle(Graphics g)
        {
            g.FillEllipse(brush, new Rectangle(new Point(x, y), new Size(radius, radius)));
        }
    }
}
