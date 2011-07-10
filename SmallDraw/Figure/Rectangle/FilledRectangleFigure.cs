using System;
using System.Drawing;

namespace SmallDraw.Figure.Rectangle
{
    /// <summary>
    /// A figure for filled rectangles
    /// </summary>
    public class FilledRectangleFigure : RectangleFigure
    {
        /// <summary>
        /// Initialize a filled rectangle figure
        /// </summary>
        /// <param name="canvas">the associated canvas</param>
        /// <param name="location">the location of the rectangle</param>
        /// <param name="size">the size of the rectangle</param>
        public FilledRectangleFigure(ICanvas canvas, Point location, Size size)
            : base(canvas, location, size)
        { }

        /// <summary>
        /// Draw the filled rectangle figure
        /// </summary>
        /// <param name="g">the graphics context</param>
        public override void Paint(Graphics g)
        {
            g.FillRectangle(Brushes.LightGray, this.Bounds);
            g.DrawRectangle(Pens.Black, this.Bounds);
        }

        /// <summary>
        /// Test if a point touches the filled rectangle
        /// </summary>
        /// <param name="p">the point to test</param>
        /// <returns>true if the point is contained in the rectangle</returns>
        public override bool Touches(Point p)
        {
            return this.Bounds.Contains(p);
        }
    }
}
