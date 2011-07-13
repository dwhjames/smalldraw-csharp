using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SmallDraw.Figure.Rectangle
{
    /// <summary>
    /// A figure for rectangles
    /// </summary>
    public class RectangleFigure : Basic.BasicFigure
    {
        /// <summary>
        /// Initialize a rectangle figure
        /// </summary>
        /// <param name="canvas">the associated canvas</param>
        /// <param name="location">the location of the rectangle</param>
        /// <param name="size">the size of the rectangle</param>
        public RectangleFigure(ICanvas canvas, Point location, Size size)
            : base(canvas, location, size)
        {
            _handles.Add(new Basic.LocatorHandle(new Locator.TopLeftLocator(this), _canvas));
            _handles.Add(new Basic.LocatorHandle(new Locator.TopMidLocator(this), _canvas));
            _handles.Add(new Basic.LocatorHandle(new Locator.TopRightLocator(this), _canvas));
            _handles.Add(new Basic.LocatorHandle(new Locator.LeftMidLocator(this), _canvas));
            _handles.Add(new Basic.LocatorHandle(new Locator.RightMidLocator(this), _canvas));
            _handles.Add(new Basic.LocatorHandle(new Locator.BottomLeftLocator(this), _canvas));
            _handles.Add(new Basic.LocatorHandle(new Locator.BottomMidLocator(this), _canvas));
            _handles.Add(new Basic.LocatorHandle(new Locator.BottomRightLocator(this), _canvas));
        }

        /// <summary>
        /// Draw the rectangle
        /// </summary>
        /// <param name="g">the graphics context</param>
        public override void Paint(Graphics g)
        {
            g.DrawRectangle(Pens.Black, this.Bounds);
        }

        /// <summary>
        /// Test if a point touches the rectangle
        /// </summary>
        /// <param name="p">the point to test</param>
        /// <returns>true if the point touches any of the edges</returns>
        public override bool Touches(Point p)
        {
            Point lt = new Point(Bounds.Left, Bounds.Top), rt = new Point(Bounds.Right, Bounds.Top), lb = new Point(Bounds.Left, Bounds.Bottom), rb = new Point(Bounds.Right, Bounds.Bottom);
            return Util.Geometry.LinePointIntersect(lt, rt, p) || Util.Geometry.LinePointIntersect(lb, rb, p)
                || Util.Geometry.LinePointIntersect(lt, lb, p) || Util.Geometry.LinePointIntersect(rt, rb, p);
        }

        /// <summary>
        /// Generate a string representation of the rectangle
        /// </summary>
        /// <returns>a string representation</returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("Rectangle (").Append(Bounds.X).Append(',').Append(Bounds.Y).Append(")@(").Append(Bounds.Width).Append(',').Append(Bounds.Height).Append(')');
            return builder.ToString();
        }
    }
}
