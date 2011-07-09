using System;
using System.Drawing;
using System.Collections.Generic;

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
            : base(canvas, start.Location.X, start.Location.Y, 0, 0)
        {
            this._startFigure = start;
            ((IObservable)this._startFigure).AddObserver(this);
            this._endLocator = new Locator.PointLocator(end);
        }
        #endregion

        #region overriding implementation of BasicFigure
        public override void Paint(Graphics g)
        {
            g.DrawLine(Pens.Black, _startFigure.CenterLocator.Location, _endLocator.Location);
        }

        public override System.Drawing.Rectangle Bounds
        {
            get
            {
                var startPoint = _startFigure.CenterLocator.Location;
                var endPoint = _endLocator.Location;
                var rect = new System.Drawing.Rectangle(Math.Min(startPoint.X, endPoint.X),
                                                        Math.Min(startPoint.X, endPoint.Y),
                                                        Math.Abs(startPoint.X - endPoint.X),
                                                        Math.Abs(startPoint.Y - endPoint.Y));
                rect.Inflate(1, 1);
                return rect;
            }
        }

        public override bool Touches(Point p)
        {
            if (_endFigure == null)
                return false;
            else
                return Util.Geometry.LinePointIntersect(_startFigure.CenterLocator.Location, _endFigure.CenterLocator.Location, p);
        }

        public override Point Location
        {
            get
            {
                return Util.Geometry.GetLineMidPoint(_startFigure.CenterLocator.Location, _endFigure.CenterLocator.Location);
            }
            set
            {
                throw new NotSupportedException();
            }
        }

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
                throw new NotSupportedException();
            }
        }

        public override IEnumerable<IHandle> Handles
        {
            get
            {
                return new List<IHandle>();
            }
        }

        public override void Translate(Size s)
        { }

        protected override void InitializeHandles()
        {
            _handles = new List<IHandle>();
            _handles.Add(new Basic.LocatorHandle(new Locator.ProportionalLocator(this), _canvas));
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
            NotifyObservers();
            var newBounds = this.ExpandedBounds;
            _canvas.Repaint(System.Drawing.Rectangle.Union(oldBounds, newBounds));
        }
        #endregion

        public override string ToString()
        {
            return "ConnectingLineFigure (" + _startFigure.CenterLocator.X + "," + _startFigure.CenterLocator.Y + ")-(" + _endLocator.X + "," + _endLocator.Y + ")";
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
