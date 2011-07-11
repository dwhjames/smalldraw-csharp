using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SmallDraw.Figure.Line
{
    /// <summary>
    /// A figure for lines
    /// </summary>
    public class LineFigure : Basic.BasicFigure, IObserver
    {
        #region fields
        /// <summary>
        /// the start and end locations of the line
        /// </summary>
        protected Locator.PointLocator _startLocator, _endLocator;
        #endregion

        #region constructors
        /// <summary>
        /// Instantiate a new line figure
        /// </summary>
        /// <param name="canvas">the associated canvas</param>
        /// <param name="start">the starting point</param>
        public LineFigure(ICanvas canvas, Point start)
            : base(canvas, start, Size.Empty)
        {
            _startLocator = new Locator.PointLocator(_location);
            ((IObservable)_startLocator).AddObserver(this);
            _endLocator = new Locator.PointLocator(_location);
            ((IObservable)_endLocator).AddObserver(this);

            _handles.Add(new Basic.LocatorHandle(new Locator.ProportionalLocator(this), _canvas));
            _handles.Add(new Basic.LocatorHandle(_startLocator, _canvas));
            _handles.Add(new Basic.LocatorHandle(_endLocator, _canvas));
        }
        #endregion

        #region overriding BasicFigure
        /// <summary>
        /// Draw the line
        /// </summary>
        /// <param name="g">the graphics context</param>
        public override void Paint(Graphics g)
        {
            g.DrawLine(Pens.Black, _startLocator.Location, _endLocator.Location);
        }

        /// <summary>
        /// Get the bounds for the line figure
        /// </summary>
        public override System.Drawing.Rectangle Bounds
        {
            get
            {
                return Util.Geometry.RectangleFromPoints(_startLocator.Location, _endLocator.Location);
            }
        }

        /// <summary>
        /// Get and set the location of the line
        /// </summary>
        public override Point Location
        {
            get
            {
                return base.Location;
            }
            set
            {
                this.Translate(value.X - this.Location.X, value.Y - this.Location.Y);
            }
        }

        /// <summary>
        /// Get and the size of the line.
        /// Setting the size is not supported.
        /// </summary>
        public override Size Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Translate the line by a width and a height
        /// </summary>
        /// <param name="s">the translation dimensions</param>
        public override void Translate(Size s)
        {
            var oldBounds = this.ExpandedBounds;
            _startLocator.Translate(s);
            _endLocator.Translate(s);
            NotifyObservers();
            _canvas.Repaint(System.Drawing.Rectangle.Union(oldBounds, this.ExpandedBounds));
        }

        /// <summary>
        /// Test if a given point touches the line
        /// </summary>
        /// <param name="p">the point to test</param>
        /// <returns>true if the point touches the line</returns>
        public override bool Touches(Point p)
        {
            return Util.Geometry.LinePointIntersect(this.Start, this.End, p);
        }
        #endregion

        #region implementation of IObserver
        /// <summary>
        /// Update the line figure when notified of a change
        /// </summary>
        void IObserver.Update()
        {
            RecomputeShapeFromBounds();
            NotifyObservers();
            _canvas.Repaint(this.ExpandedBounds);
        }
        #endregion

        /// <summary>
        /// Generate a string representation of the line
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var builder = new StringBuilder("Line ");
            builder.Append('(').Append(Bounds.X).Append(',').Append(Bounds.Y).Append(")@(").Append(Bounds.Width).Append(',').Append(Bounds.Height).Append(')');
            return builder.ToString();
        }

        /// <summary>
        /// Get and set the start location of the line
        /// </summary>
        public Point Start
        {
            get
            {
                return _startLocator.Location;
            }
            set
            {
                _startLocator.Location = value;
                // keep _location and _size in sync
                RecomputeShapeFromBounds();
            }
        }

        /// <summary>
        /// Get and set the end location of the line
        /// </summary>
        public Point End
        {
            get
            {
                return _endLocator.Location;
            }
            set
            {
                _endLocator.Location = value;
                // keep _location and _size in sync
                RecomputeShapeFromBounds();
            }
        }

        /// <summary>
        /// Get the length of the line
        /// </summary>
        public int Length
        {
            get
            {
                return (int)Math.Sqrt(Util.Geometry.Pythagoras(_endLocator.X - _startLocator.X, _endLocator.Y - _startLocator.Y));
            }
        }
    }
}
