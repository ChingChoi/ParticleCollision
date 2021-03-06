﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace ParticleCollision
{
    public partial class ParticleCollision : Form
    {
        /// <summary>
        /// For overriding movement of window
        /// </summary>
        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;
        /// <summary>
        /// Custom GUI offset
        /// </summary>
        private const int CLOSE_FORM_HORZ_OFFSET = 30;
        private const int PANEL_VERT_OFFSET = 25;
        private const int PICTUREBOX_OFFSET = 25;
        private const int PANEL_HEIGHT_OFFSET = 27;
        private const int MAX_FORM_HORZ_OFFSET = 60;
        private const int MIN_FORM_HORZ_OFFSET = 90;
        private const float BUTTON_FONT_SIZE = 8f;
        private const int LEFT_OFFSET = 14;
        public const int WIDTH = 1920;
        public const int HEIGHT = 1080;
        /// <summary>
        /// Main GUI variable
        /// </summary>
        private Panel panel;
        private CustomButton closeForm;
        private CustomButton minForm;
        private CustomButton maxForm;
        private Color themeColor;
        private Color themeBackgroundColor;
        private Color themeBackgroundColorTwo;
        private TextBox title;
        private PictureBox pictureBoxMain;
        /// <summary>
        /// Variables for drawing
        /// </summary>
        private Graphics G;
        private Timer drawingTimer;
        public const int INTERVAL = 20;
        private bool isDragging;
        private int curX;
        private int curY;
        private int curXX;
        private int curYY;
        private float hForce;
        private float vForce;
        /// <summary>
        /// Object variables
        /// </summary>
        List<MCircle> circles;
        MCircle curCircle;

        /// <summary>
        /// Driver constructor for particle collision
        /// </summary>
        public ParticleCollision()
        {
            this.themeBackgroundColor = Color.FromArgb(175, 0, 0, 0);
            this.themeBackgroundColorTwo = Color.FromArgb(100, 0, 0, 0);
            this.themeColor = Color.Cyan;
  //          this.themeColor = Color.FromArgb(200, 144, 238, 144);
            this.circles = new List<MCircle>();
            DoubleBuffered = true;

            InitializeComponent();
            InitializeCustom();
            InitializeDrawing();
            CustomizeMenuStrip(menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            panel.Invalidate();
        }

        /// <summary>
        /// Initializes custom GUI
        /// </summary>
        private void InitializeCustom()
        {
            //
            // Control
            //
            Width = WIDTH;
            Height = HEIGHT;
            pictureBoxMain = new PictureBox();
            pictureBoxMain.Location = new Point(PICTUREBOX_OFFSET, PANEL_HEIGHT_OFFSET + PICTUREBOX_OFFSET);
            pictureBoxMain.Name = "pictureBoxMain";
            pictureBoxMain.TabStop = false;
            pictureBoxMain.BackColor = themeBackgroundColorTwo;
            pictureBoxMain.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxMain.MouseDown += new MouseEventHandler(pictureBoxMain_MouseDown);
            pictureBoxMain.MouseMove += new MouseEventHandler(pictureBoxMain_MouseMove);
            pictureBoxMain.MouseUp += new MouseEventHandler(pictureBoxMain_MouseUp);
//            pictureBoxMain.Image = Properties.Resources.deepSeaTwo;
            // 
            // panel1
            // 
            panel = new Panel();
            panel.BackColor = System.Drawing.Color.Transparent;
            panel.Controls.Add(this.menuStrip);
            panel.Location = new System.Drawing.Point(0, 25);
            panel.Name = "panel1";
            panel.Size = new System.Drawing.Size(this.Width, this.Height - 25);
            panel.Controls.Add(pictureBoxMain);
            // 
            // closeForm
            // 
            closeForm = new CustomButton();
            closeForm.ForeColor = themeColor;
            closeForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            closeForm.FlatAppearance.BorderSize = 0;
            closeForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            closeForm.Location = new System.Drawing.Point(this.Width - 45, 0);
            closeForm.Name = "closeForm";
            closeForm.Size = new System.Drawing.Size(30, 25);
            closeForm.TabIndex = 6;
            closeForm.Text = "X";
            closeForm.UseVisualStyleBackColor = true;
            closeForm.Click += new System.EventHandler(closeForm_Click);
            // 
            // maxForm
            // 
            maxForm = new CustomButton();
            maxForm.ForeColor = themeColor;
            maxForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            maxForm.FlatAppearance.BorderSize = 0;
            maxForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            maxForm.Location = new System.Drawing.Point(this.Width - 75, 0);
            maxForm.Name = "maxForm";
            maxForm.Size = new System.Drawing.Size(30, 25);
            maxForm.TabIndex = 5;
            maxForm.TabStop = false;
            maxForm.Text = "⎕";
            maxForm.UseMnemonic = false;
            maxForm.UseVisualStyleBackColor = true;
            maxForm.Click += new System.EventHandler(maxForm_Click);
            // 
            // minForm
            // 
            minForm = new CustomButton();
            minForm.ForeColor = themeColor;
            minForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            minForm.FlatAppearance.BorderSize = 0;
            minForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            minForm.Font = new System.Drawing.Font("Microsoft Sans Serif",
                BUTTON_FONT_SIZE, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            minForm.Location = new System.Drawing.Point(this.Width - 105, 0);
            minForm.Name = "minForm";
            minForm.Size = new System.Drawing.Size(30, 25);
            minForm.TabIndex = 4;
            minForm.TabStop = false;
            minForm.Text = "_";
            minForm.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            minForm.UseMnemonic = false;
            minForm.UseVisualStyleBackColor = true;
            minForm.Click += new System.EventHandler(minForm_Click);
            //
            // title
            //
            title = new TextBox();
            title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            title.BorderStyle = System.Windows.Forms.BorderStyle.None;
            title.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F,
                System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title.Location = new System.Drawing.Point(LEFT_OFFSET, 4);
            title.Name = "Title";
            title.Size = new System.Drawing.Size(163, 16);
            title.ReadOnly = true;
            title.Enabled = false;
            title.TabStop = false;
            title.ForeColor = themeColor;
            title.Text = "Particle Collision";
            //
            // ImageCompressor
            //
            Resize += new System.EventHandler(this.WindowResize);
            Controls.Add(panel);
            Controls.Add(maxForm);
            Controls.Add(title);
            Controls.Add(minForm);
            Controls.Add(closeForm);
            BackColor = Color.FromArgb(35, 35, 35);
        }

        /// <summary>
        /// Initialize variables for drawing
        /// </summary>
        private void InitializeDrawing()
        {
            G = pictureBoxMain.CreateGraphics();
            drawingTimer = new Timer();
            drawingTimer.Tick += new EventHandler(drawCricles);
            drawingTimer.Interval = INTERVAL;
            drawingTimer.Start();
        }

        /// <summary>
        /// Closes the form when closeForm button is clicked
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event</param>
        private void closeForm_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Minimizes the form when minForm button is clicked
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event</param>
        private void minForm_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// Maximize the form when maxForm button is clicked
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event</param>
        private void maxForm_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        /// <summary>
        /// Enable dragging of panel
        /// </summary>
        /// <param name="m">message</param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
            {
                m.Result = (IntPtr)(HT_CAPTION);
            }
        }

        /// <summary>
        /// Customize menuStrip's color
        /// </summary>
        /// <param name="menuStrip">Menustrip object</param>
        private void CustomizeMenuStrip(MenuStrip menuStrip)
        {
            menuStrip.Renderer = new MyRenderer(themeBackgroundColor);
            menuStrip.BackColor = Color.Transparent;
            menuStrip.ForeColor = themeColor;
        }

        /// <summary>
        /// Redraw form when resized
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event</param>
        private void WindowResize(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                Control control = (Control)sender;
                int w = control.Size.Width;
                int h = control.Size.Height;
                closeForm.Location = new Point(w - CLOSE_FORM_HORZ_OFFSET, 0);
                panel.Size = new Size(w, h - PANEL_VERT_OFFSET);
                maxForm.Location = new Point(w - MAX_FORM_HORZ_OFFSET, 0);
                minForm.Location = new Point(w - MIN_FORM_HORZ_OFFSET, 0);
                pictureBoxMain.Size = new Size(w - PICTUREBOX_OFFSET * 2, h - 54 - PICTUREBOX_OFFSET * 2);
            }
        }

        /// <summary>
        /// Custom tool strip renderer
        /// </summary>
        private class MyRenderer : ToolStripProfessionalRenderer
        {
            public MyRenderer(Color themeBackgroundColor) : base(new MyColors(themeBackgroundColor)) { }

            protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
            {
                var toolStripMenuItem = e.Item as ToolStripMenuItem;
                if (toolStripMenuItem != null)
                {
                    e.ArrowColor = Color.FromArgb(200, 144, 238, 144);
                }
                base.OnRenderArrow(e);
            }
        }

        /// <summary>
        /// Class that override specific form item color
        /// </summary>
        private class MyColors : ProfessionalColorTable
        {
            private Color themeBackgroundColor;
            public MyColors(Color themeBackgroundColor)
            {
                this.themeBackgroundColor = themeBackgroundColor;
            }
            public override Color MenuItemSelected
            {
                get { return themeBackgroundColor; }
            }
            public override Color ButtonSelectedGradientMiddle
            {
                get { return Color.Transparent; }
            }
            public override Color ButtonSelectedHighlight
            {
                get { return Color.Transparent; }
            }
            public override Color ButtonCheckedGradientBegin
            {
                get { return themeBackgroundColor; }
            }
            public override Color ButtonCheckedGradientEnd
            {
                get { return themeBackgroundColor; }
            }
            public override Color ButtonSelectedBorder
            {
                get { return Color.FromArgb(200, 144, 238, 144); }
            }
            public override Color ToolStripDropDownBackground
            {
                get { return themeBackgroundColor; }
            }
            public override Color CheckSelectedBackground
            {
                get { return themeBackgroundColor; }
            }
            public override Color MenuItemSelectedGradientBegin
            {
                get { return themeBackgroundColor; }
            }
            public override Color MenuItemSelectedGradientEnd
            {
                get { return themeBackgroundColor; }
            }
            public override Color MenuItemBorder
            {
                get { return Color.Black; }
            }
            public override Color MenuItemPressedGradientBegin
            {
                get { return Color.Transparent; }
            }
            public override Color CheckBackground
            {
                get { return themeBackgroundColor; }
            }
            public override Color CheckPressedBackground
            {
                get { return themeBackgroundColor; }
            }
            public override Color ImageMarginGradientBegin
            {
                get { return Color.Transparent; }
            }
            public override Color ImageMarginGradientMiddle
            {
                get { return Color.Transparent; }
            }
            public override Color ImageMarginGradientEnd
            {
                get { return Color.Transparent; }
            }
            public override Color MenuItemPressedGradientEnd
            {
                get { return Color.Transparent; }
            }
        }

        /// <summary>
        /// Custom button that override Button
        /// </summary>
        public class CustomButton : Button
        {
            protected override bool ShowFocusCues
            {
                get
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Generate circle on a specific spot
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event</param>
        private void circleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MCircle circle = new MCircle(100, new Point(500, 200));
            circles.Add(circle);
        }

        /// <summary>
        /// Draw circle on mouse click and enable mouse drag
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event</param>
        private void pictureBoxMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int cX = e.X;
                int cY = e.Y;
                Random random = new Random();
                int diameter = (int)(100 * random.NextDouble()) + 50;
                curCircle = new MCircle(diameter, new Point(cX, cY));
                circles.Add(curCircle);
                curX = cX;
                curY = cY;
                curXX = e.X;
                curYY = e.Y;
                isDragging = true;
            }
        }

        /// <summary>
        /// Draw line on mouse drag
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event</param>
        private void pictureBoxMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                curXX = e.X;
                curYY = e.Y;
                using (Graphics g = pictureBoxMain.CreateGraphics())
                {
                    g.DrawLine(Pens.Red, new Point(curX, curY), new Point(e.X, e.Y));
                }
                hForce = (curX - curXX) * 20;
                vForce = (curY - curYY) * 20;

            }
        }

        /// <summary>
        /// Disable dragging on mouse up
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event</param>
        private void pictureBoxMain_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
            curCircle.Locked = false;
            curX = -1;
            curY = -1;
            circles.Last().V = new Velocity(hForce, vForce);
            hForce = 0;
            vForce = 0;
        }

        /// <summary>
        /// Draw all circles per interval
        /// </summary>
        /// <param name="sender">sender object<param>
        /// <param name="e">event</param>
        private void drawCricles(object sender, EventArgs e)
        {
            if (circles != null)
            {
                using (Graphics g = pictureBoxMain.CreateGraphics())
                {
                    pictureBoxMain.Refresh();
                    foreach (MCircle c in circles)
                    {
                        if (!c.Locked)
                        {
                            double timeElapsed = -1;
                            int count = 0;
                            while(timeElapsed != 0 && count < 3)
                            {
                                timeElapsed = MotionCalc(c);
                                count++;
                            }
                            c.DrawCircle(g);
                        }
                        else
                        {
                            c.DrawCircle(g);
                        }
                        if (isDragging && curX != -1)
                        {
                            g.DrawLine(Pens.Red, new Point(curX, curY), new Point(curXX, curYY));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Support method for calculating motion of circles
        /// </summary>
        /// <param name="c">Circle object</param>
        /// <returns>Time elapsed, if 0 means no collision</returns>
        private double MotionCalc(MCircle c)
        {
            double timeElapsed = 0;
            Point tempDestination = Physics.DestinationPosition(c.V, c.a, INTERVAL, c.P);
            Vector collisionVector = Physics.BoundaryCollision(tempDestination, c.Diameter / 2,
                pictureBoxMain.Width, pictureBoxMain.Height);
            if (collisionVector.X == 0 && collisionVector.Y == 0)
            {
                c.P = new Point(tempDestination.X, tempDestination.Y);
                c.V = Physics.CalcVelocity(c.V, c.a, INTERVAL);
                c.a = Physics.CalcAcceleration(c.V, 100, c.Diameter / 2);
                return timeElapsed;
            }
            else
            {
                if (collisionVector.X != 0)
                {
                    timeElapsed = Physics.TimeElapsed(c.V.X, c.a.X, (int)collisionVector.X);
                    c.P = Physics.DestinationPosition(c.V, c.a, timeElapsed, c.P);
                    c.V = Physics.CalcVelocity(c.V, c.a, timeElapsed);
                    c.V = new Velocity(-c.V.X, c.V.Y);
                    c.a = Physics.CalcAcceleration(c.V, 100, c.Diameter / 2);
                    c.P = Physics.DestinationPosition(c.V, c.a, INTERVAL - timeElapsed, c.P);
                }
                else
                {
                    timeElapsed = Physics.TimeElapsed(c.V.Y, c.a.Y, (int)collisionVector.Y);
                    c.P = Physics.DestinationPosition(c.V, c.a, timeElapsed, c.P);
                    c.V = Physics.CalcVelocity(c.V, c.a, timeElapsed);
                    c.V = new Velocity(c.V.X, -c.V.Y);
                    c.a = Physics.CalcAcceleration(c.V, 100, c.Diameter / 2);
                    c.P = Physics.DestinationPosition(c.V, c.a, INTERVAL - timeElapsed, c.P);
                }
                return timeElapsed;
            }
        }
    }
}
