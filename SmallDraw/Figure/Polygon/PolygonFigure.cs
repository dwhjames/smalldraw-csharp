using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SmallDraw.Figure.Polygon
{
    /// <summary>
    /// A figure for polygons
    /// </summary>
    public class PolygonFigure : Basic.BasicFigure, IObserver
    {
        #region fields
        /// <summary>
        /// the locators defining the verticies of the polygon
        /// </summary>
        protected IList<ILocator> _locators;
        #endregion

        #region constructors
        /// <summary>
        /// Initialize a new polygon figure with a starting point
        /// </summary>
        /// <param name="canvas">the associated canvas</param>
        /// <param name="start">the starting point</param>
        public PolygonFigure(ICanvas canvas, Point start)
            : base(canvas, start, Size.Empty)
        {
            _locators = new List<ILocator>();
            AddPoint(start);
        }

        protected override void InitializeHandles()
        {
            _handles = new List<IHandle>();
        }
        #endregion

        public void AddPoint(Point p)
        {
            var pl = new Locator.PointLocator(p);
            ((IObservable)pl).AddObserver(this);
            _locators.Add(pl);
            RecomputeShapeFromBounds();
        }

        public Point LastPoint
        {
            set
            {
                _locators[_locators.Count - 1].Location = value;
            }
        }

        public int CountPoints
        {
            get
            {
                return _locators.Count;
            }
        }

        #region overriding BasicFigure
        public override IEnumerable<IHandle> Handles
        {
            get
            {
                _handles = new List<IHandle>(_locators.Count);
                foreach (var l in _locators)
                {
                    _handles.Add(new Basic.LocatorHandle(l, _canvas));
                }
                return _handles;
            }
        }

        public override ILocator CenterLocator
        {
            get
            {
                return new Locator.ProportionalLocator(this);
            }
        }

        public override System.Drawing.Rectangle Bounds
        {
            get
            {
                var bounds = new System.Drawing.Rectangle(_locators[0].Location, Size.Empty);
                foreach (var l in _locators)
                {
                    bounds = Util.Geometry.ExtendRectangleWithPoint(bounds, l.Location);
                }
                return bounds;
            }
        }

        public override void Paint(System.Drawing.Graphics g)
        {
            var poly = new Point[_locators.Count];
            for (int i = 0; i < _locators.Count; i++)
            {
                poly[i] = _locators[i].Location;
            }
            g.DrawPolygon(Pens.Black, poly);
        }

        public override bool Touches(Point p)
        {
            int i = 0;

            // invariant: no intersection with edges between points 0 -- 1 -- .. -- i
            while (i < CountPoints
                && !Util.Geometry.LinePointIntersect(_locators[i].Location, _locators[(i + 1) % CountPoints].Location, p))
            {
                i++;
            }
            return i < CountPoints; // if i < CountPoints then the loop stopped when a line was touched
        }

        public override void Translate(Size s)
        {
            var oldBounds = this.ExpandedBounds;
            foreach (var l in _locators)
            {
                l.Translate(s);
            }
            RecomputeShapeFromBounds();
            NotifyObservers();
            _canvas.Repaint(System.Drawing.Rectangle.Union(this.ExpandedBounds, oldBounds));
        }

        public override Point Location
        {
            get
            {
                return base.Location;
            }
            set
            {
                this.Translate(value.X - _location.X, value.Y - _location.Y);
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
                var oldBounds = this.ExpandedBounds;
                var bounds = this.Bounds;
                float swidth = bounds.Width == 0 ? 1 : (float)value.Width / bounds.Width;
                float sheight = bounds.Height == 0 ? 1 : (float)value.Height / bounds.Height;

                foreach (ILocator l in _locators)
                {
                    l.SetLocation(Util.Geometry.Prorata(bounds.X, l.X, swidth),
                                  Util.Geometry.Prorata(bounds.X, l.Y, sheight));
                }

                RecomputeShapeFromBounds();
                NotifyObservers();
                _canvas.Repaint(System.Drawing.Rectangle.Union(this.ExpandedBounds, oldBounds));
            }
        }
        #endregion

        public override string ToString()
        {
            var builder = new StringBuilder("Polygon ");
            foreach (var l in _locators)
            {
                builder.Append('(').Append(l.X).Append(',').Append(l.Y).Append(')');
            }
            return builder.ToString();
        }

        #region implmentation of IObserver
        void IObserver.Update()
        {
            RecomputeShapeFromBounds();
            NotifyObservers();
            _canvas.Repaint(this.ExpandedBounds);
        }
        #endregion
    }
}
