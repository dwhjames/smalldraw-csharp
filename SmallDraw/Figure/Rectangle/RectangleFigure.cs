using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SmallDraw.Figure.Rectangle
{
    public class RectangleFigure : Basic.BasicFigure
    {
        public RectangleFigure(ICanvas canvas, Point location, Size size)
            : base(canvas, location, size)
        { }

        protected override void InitializeHandles()
        {
            _handles = new List<IHandle>();
            _handles.Add(new Basic.LocatorHandle(new Locator.ProportionalLocator(this), _canvas));
            _handles.Add(new Line.ConnectorHandle(new Locator.ProportionalLocator(this, 0.33f, 0.33f), _canvas, this));
            _handles.Add(new Basic.LocatorHandle(new Locator.TopLeftLocator(this), _canvas));
            _handles.Add(new Basic.LocatorHandle(new Locator.TopMidLocator(this), _canvas));
            _handles.Add(new Basic.LocatorHandle(new Locator.TopRightLocator(this), _canvas));
            _handles.Add(new Basic.LocatorHandle(new Locator.LeftMidLocator(this), _canvas));
            _handles.Add(new Basic.LocatorHandle(new Locator.RightMidLocator(this), _canvas));
            _handles.Add(new Basic.LocatorHandle(new Locator.BottomLeftLocator(this), _canvas));
            _handles.Add(new Basic.LocatorHandle(new Locator.BottomMidLocator(this), _canvas));
            _handles.Add(new Basic.LocatorHandle(new Locator.BottomRightLocator(this), _canvas));
        }

        public override void Paint(Graphics g)
        {
            g.DrawRectangle(Pens.Black, this.Bounds);
        }

        public override bool Touches(Point p)
        {
            Point lt = new Point(Bounds.Left, Bounds.Top), rt = new Point(Bounds.Right, Bounds.Top), lb = new Point(Bounds.Left, Bounds.Bottom), rb = new Point(Bounds.Right, Bounds.Bottom);
            return Util.Geometry.LinePointIntersect(lt, rt, p) || Util.Geometry.LinePointIntersect(lb, rb, p)
                || Util.Geometry.LinePointIntersect(lt, lb, p) || Util.Geometry.LinePointIntersect(rt, rb, p);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("Rectangle (").Append(Bounds.X).Append(',').Append(Bounds.Y).Append(")@(").Append(Bounds.Width).Append(',').Append(Bounds.Height).Append(')');
            return builder.ToString();
        }
    }
}
