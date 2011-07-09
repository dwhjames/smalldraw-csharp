using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SmallDraw.Figure.Line
{
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
            : base(canvas, start, new Size(1, 1))
        { }
        #endregion

        #region overriding BasicFigure
        public override void Paint(Graphics g)
        {
            g.DrawLine(Pens.Black, _startLocator.Location, _endLocator.Location);
        }

        protected override void InitializeHandles()
        {
            _startLocator = new Locator.PointLocator(_location);
            ((IObservable)_startLocator).AddObserver(this);
            _endLocator = new Locator.PointLocator(_location);
            ((IObservable)_endLocator).AddObserver(this);

            _handles = new List<IHandle>();
            _handles.Add(new Basic.LocatorHandle(new Locator.ProportionalLocator(this), _canvas));
            _handles.Add(new Basic.LocatorHandle(_startLocator, _canvas));
            _handles.Add(new Basic.LocatorHandle(_endLocator, _canvas));
        }

        public override System.Drawing.Rectangle Bounds
        {
            get
            {
                return Util.Geometry.RectangleFromPoints(_startLocator.Location, _endLocator.Location);
            }
        }

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

        public override void Translate(Size s)
        {
            var oldBounds = this.ExpandedBounds;
            _startLocator.Translate(s);
            _endLocator.Translate(s);
            _location += s;
            NotifyObservers();
            _canvas.Repaint(System.Drawing.Rectangle.Union(oldBounds, this.ExpandedBounds));
        }

        public override bool Touches(Point p)
        {
            return Util.Geometry.LinePointIntersect(this.Start, this.End, p);
        }
        #endregion

        #region implementation of IObserver
        void IObserver.Update()
        {
            NotifyObservers();
            _canvas.Repaint(this.ExpandedBounds);
        }
        #endregion

        public override string ToString()
        {
            var builder = new StringBuilder("Line ");
            builder.Append('(').Append(Bounds.X).Append(',').Append(Bounds.Y).Append(")@(").Append(Bounds.Width).Append(',').Append(Bounds.Height).Append(')');
            return builder.ToString();
        }

        /// <summary>
        /// Get and set the starting location of the line
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

        public int Length
        {
            get
            {
                return (int)Math.Sqrt(Util.Geometry.Pythagoras(_endLocator.X - _startLocator.X, _endLocator.Y - _startLocator.Y));
            }
        }
    }
}
