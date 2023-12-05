using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace GEOWALL_E
{
    internal class Boton_personalizado : Button
    {
        private int borderSize = 0;

        private int borderRadius = 80;

        private Color borderColor = Color.PaleVioletRed;

        [Category("ABEL PONCE")]
        public int BorderSize
        {
            get => borderSize;
            set
            {
                borderSize = value;
                this.Invalidate();
            }
        }
        [Category("ABEL PONCE")]
        public int BorderRadius
        {
            get => borderRadius;
            set
            {
                borderRadius = value;
                this.Invalidate();
            }
        }
        [Category("ABEL PONCE")]
        public Color BorderColor 
        {
            get => borderColor;
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }
        [Category("ABEL PONCE")]
        public Color BackgroundColor
        {
            get { return this.BackColor; }
            set { this.BackColor = value; }
        }
        [Category("ABEL PONCE")]
        public Color TextColor
        {
            get { return this.ForeColor; }
            set { this.ForeColor = value; }
        }
        public Boton_personalizado() 
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(150, 40);
            this.BackColor = Color.Red;
            this.ForeColor = Color.White;
        }

        private GraphicsPath GetfigurePath( RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            return path;
        }
        protected override void OnPaint(PaintEventArgs e) 
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            RectangleF rectSurFace = new RectangleF(0, 0, this.Width, this.Height);
            RectangleF rectBorder = new RectangleF(1, 1, this.Width - 0.8F, this.Height - 1);

            //boton redondeado
            if(borderRadius > 2)
            {
                using (GraphicsPath pathSurface = GetfigurePath(rectSurFace, borderRadius))
                    using(GraphicsPath pathBorder = GetfigurePath(rectBorder, borderRadius - 1F))
                    using( Pen penSurface = new Pen(this.Parent.BackColor,2))
                    using( Pen penBorder = new Pen(borderColor, borderSize))
                {
                    penBorder.Alignment = PenAlignment.Inset;
                    // superficie del boton
                    this.Region = new Region(pathSurface);
                    //HD 
                    e.Graphics.DrawPath(penSurface, pathSurface);
                    // borde del boton
                    if(borderSize >= 1) e.Graphics.DrawPath(penBorder, pathBorder);
                }

            }
            //boton normal
            else
            {
                this.Region = new Region(rectSurFace);
                if(borderSize >= 1)
                {
                    using (Pen penBorder = new Pen(borderColor, borderSize))
                    {
                        penBorder.Alignment = PenAlignment.Inset;
                        e.Graphics.DrawRectangle(penBorder, 0, 0, this.Width - 1, this.Height - 1);
                    }
                }
            }
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.Parent.BackColorChanged += new EventHandler(Container_BackColorChanged);
        }

        private void Container_BackColorChanged(object? sender, EventArgs e)
        {
            if(this.DesignMode)
            {
                this.Invalidate();
            }
        }
    }
}
