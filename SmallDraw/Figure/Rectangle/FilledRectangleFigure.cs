using System;
using System.Drawing;

namespace SmallDraw.Figure.Rectangle
{
    public class FilledRectangleFigure : RectangleFigure
    {
        public FilledRectangleFigure(ICanvas canvas, Point location, Size size)
            : base(canvas, location, size)
        { }

        public override void Paint(Graphics g)
        {
            g.FillRectangle(Brushes.LightGray, this.Bounds);
            g.DrawRectangle(Pens.Black, this.Bounds);
        }

        public override bool Touches(Point p)
        {
            return this.Bounds.Contains(p);
        }
    }
}
