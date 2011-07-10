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

        /// <summary>
        /// Initialize the list of handles for the polygon figure
        /// </summary>
        protected override void InitializeHandles()
        {
            _handles = new List<IHandle>();
        }
        #endregion

        /// <summary>
        /// Add a point to the polygon
        /// </summary>
        /// <param name="p"></param>
        public void AddPoint(Point p)
        {
            var pl = new Locator.PointLocator(p);
            ((IObservable)pl).AddObserver(this);
            _locators.Add(pl);
            _handles.Add(new Basic.LocatorHandle(pl, _canvas));
            RecomputeShapeFromBounds();
        }

        /// <summary>
        /// Set the location of the last point in the polygon
        /// </summary>
        public Point LastPoint
        {
            set
            {
                _locators[_locators.Count - 1].Location = value;
            }
        }

        /// <summary>
        /// Get the number of points in the polygon
        /// </summary>
        public int CountPoints
        {
            get
            {
                return _locators.Count;
            }
        }

        #region overriding BasicFigure
        /// <summary>
        /// Get a locator for the center of this figure
        /// </summary>
        public override ILocator CenterLocator
        {
            get
            {
                return new Locator.ProportionalLocator(this);
            }
        }

        /// <summary>
        /// Get the bounds of the polygon figure
        /// </summary>
        public override System.Drawing.Rectangle Bounds
        {
            get
            {
                // start with a unit rectangle for the first point
                var bounds = new System.Drawing.Rectangle(_locators[0].Location, new Size(1, 1));
                foreach (var l in _locators)
                {
                    // grow the bounds with unit rectangles for each point in the polygon
                    bounds = System.Drawing.Rectangle.Union(bounds, new System.Drawing.Rectangle(l.Location, new Size(1, 1)));
                }
                return bounds;
            }
        }

        /// <summary>
        /// Draw the polygon
        /// </summary>
        /// <param name="g">the graphics context</param>
        public override void Paint(Graphics g)
        {
            var poly = new Point[_locators.Count];
            for (int i = 0; i < _locators.Count; i++)
            {
                poly[i] = _locators[i].Location;
            }
            g.DrawPolygon(Pens.Black, poly);
        }

        /// <summary>
        /// Test if a point touches the polygon
        /// </summary>
        /// <param name="p">the point to test</param>
        /// <returns>true if the point touches any of the edges</returns>
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

        /// <summary>
        /// Translate the polygon by a width and height
        /// </summary>
        /// <param name="s">the translation dimensions</param>
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

        /// <summary>
        /// Get and set the location of the polygon
        /// </summary>
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

        /// <summary>
        /// Get and set the size of the polygon
        /// </summary>
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

        /// <summary>
        /// Generate a string representation of the polygon
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Update the polygon when notified of a change
        /// </summary>
        void IObserver.Update()
        {
            RecomputeShapeFromBounds();
            NotifyObservers();
            _canvas.Repaint(this.ExpandedBounds);
        }
        #endregion
    }
}
