using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace SmallDraw.Figure.Line
{
    /// <summary>
    /// A connecting line figure is a line figure that may connect two other figures
    /// </summary>
    public class ConnectingLineFigure : Basic.BasicFigure, IObserver
    {
        #region fields
        /// <summary>
        /// The end point of the line.
        /// </summary>
        protected ILocator _endLocator;

        /// <summary>
        /// The possible figures found at the start and end of the line
        /// </summary>
        protected IFigure _startFigure, _endFigure;
        #endregion

        #region constructors
        /// <summary>
        /// Construct the line with the given start figure and end point
        /// </summary>
        /// <param name="canvas">the associated canvas</param>
        /// <param name="start">the starting figure</param>
        /// <param name="end">the end point</param>
        public ConnectingLineFigure(ICanvas canvas, IFigure start, Point end)
            : base(canvas, start.Location, Size.Empty)
        {
            // no handles for a connecting line figure
            // if an implementation of Translate is provided, the use the following handle
            // _handles.Add(new Basic.LocatorHandle(new Locator.ProportionalLocator(this), _canvas));

            this._startFigure = start;
            ((IObservable)this._startFigure).AddObserver(this);
            this._endLocator = new Locator.PointLocator(end);
        }
        #endregion

        #region overriding implementation of BasicFigure
        /// <summary>
        /// Draw the connecting line figure
        /// </summary>
        /// <param name="g"></param>
        public override void Paint(Graphics g)
        {
            g.DrawLine(Pens.Black, _startFigure.CenterLocator.Location, _endLocator.Location);
        }

        /// <summary>
        /// Get the bounds for the connecting line figure
        /// </summary>
        public override System.Drawing.Rectangle Bounds
        {
            get
            {
                return Util.Geometry.RectangleFromPoints(_startFigure.CenterLocator.Location, _endLocator.Location);
            }
        }

        /// <summary>
        /// Test if a given point touches the line
        /// </summary>
        /// <param name="p">the point to test</param>
        /// <returns>true if the point touches the line</returns>
        public override bool Touches(Point p)
        {
            if (_endFigure == null)
                return false;
            else
                return Util.Geometry.LinePointIntersect(_startFigure.CenterLocator.Location, _endFigure.CenterLocator.Location, p);
        }

        /// <summary>
        /// Get the location of the line.
        /// Setting the location is not supported.
        /// </summary>
        public override Point Location
        {
            get
            {
                return Util.Geometry.GetLineMidPoint(_startFigure.CenterLocator.Location, _endFigure.CenterLocator.Location);
            }
            set
            {
                throw new NotSupportedException("Cannot directly set location of connecting line figure");
            }
        }

        /// <summary>
        /// Get the size of the line.
        /// Setting the size is not supported.
        /// </summary>
        public override Size Size
        {
            get
            {
                var start = _startFigure.CenterLocator.Location;
                var end = _endFigure.CenterLocator.Location;
                return new Size(Math.Abs(start.X - end.X), Math.Abs(start.Y - end.Y));
            }
            set
            {
                throw new NotSupportedException("Cannot directly set size of connecting line figure");
            }
        }

        /// <summary>
        /// A connecting line figure cannot be translated.
        /// </summary>
        /// <param name="s">the translation dimensions</param>
        public override void Translate(Size s)
        {
            // empty implementation
        }
        #endregion

        #region implementation of IObserver
        /// <summary>
        /// Called by figures that are being observed by this line.
        /// When called the start and end points are updated.
        /// </summary>
        void IObserver.Update()
        {
            var oldBounds = this.ExpandedBounds;
            if (_endFigure != null)
            {
                _endLocator = _endFigure.CenterLocator;
            }
            var newBounds = this.ExpandedBounds;
            NotifyObservers();
            _canvas.Repaint(System.Drawing.Rectangle.Union(oldBounds, newBounds));
        }
        #endregion

        /// <summary>
        /// Generate a string representation of a connecting line figure
        /// </summary>
        /// <returns>a string representation</returns>
        public override string ToString()
        {
            var builder = new StringBuilder("ConnectingLineFigure ");
            builder.Append('(').Append(_startFigure.CenterLocator.X).Append(',').Append(_startFigure.CenterLocator.Y).Append(")-(").Append(_endLocator.X).Append(',').Append(_endLocator.Y).Append(')');
            return builder.ToString();    
        }

        /// <summary>
        /// Set the end point
        /// </summary>
        public Point EndLocation
        {
            set
            {
                var oldBounds = this.ExpandedBounds;
                _endLocator.Location = value;
                var newBounds = this.ExpandedBounds;
                _canvas.Repaint(System.Drawing.Rectangle.Union(oldBounds, newBounds));
            }
        }

        /// <summary>
        /// Get the end locator
        /// </summary>
        public ILocator EndLocator
        {
            get { return _endLocator; }
        }

        /// <summary>
        /// Set the end figure and set the end point value to be its center.
        /// The new new figure is only valid if it is not the start figure.
        /// </summary>
        public IFigure EndFigure
        {
            set
            {
                if (value != _startFigure)
                {
                    var oldBounds = this.ExpandedBounds;
                    _endFigure = value;
                    ((IObservable)_endFigure).AddObserver(this);
                    _endLocator = _endFigure.CenterLocator;
                    var newBounds = this.ExpandedBounds;
                    _canvas.Repaint(System.Drawing.Rectangle.Union(oldBounds, newBounds));
                }
            }
        }
    }
}
