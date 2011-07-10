using System;
using System.Drawing;
using System.Windows.Forms;

namespace SmallDraw.Figure.Polygon
{
    /// <summary>
    /// A construction tool for polygons
    /// </summary>
    public class PolygonTool : Basic.ConstructionTool<PolygonFigure>
    {
        #region fields
        /// <summary>
        /// the number of points
        /// </summary>
        protected int _numPoints;
        #endregion

        #region constructors
        /// <summary>
        /// Initialize a polygon figure
        /// </summary>
        /// <param name="canvas">the associated canvas</param>
        /// <param name="numPoints">the number of points</param>
        public PolygonTool(ICanvas canvas, int numPoints)
            : base(canvas)
        {
            this._numPoints = numPoints;
        }
        #endregion

        /// <summary>
        /// Set the number of points for the polygon that this tool will create
        /// </summary>
        public int NumPoints
        {
            set
            {
                _numPoints = value;
            }
        }

        #region overiding the ConstructionTool
        /// <summary>
        /// Create a new polygon figure at a given location
        /// </summary>
        /// <param name="p">the location for the figure</param>
        /// <returns>a polygon figure</returns>
        protected override PolygonFigure NewFigureAt(Point p)
        {
            return new PolygonFigure(_canvas, p);
        }

        /// <summary>
        /// The handler for mouse down events
        /// </summary>
        /// <param name="sender">the object that raised the event</param>
        /// <param name="e">the mouse data</param>
        public override void MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            if (_newFigure == null)
            {
                base.MouseDown(sender, e);
                _newFigure.AddPoint(_start); // place the first point twice in the polygon, this one will be moved by MouseMove
            }
            else if (_newFigure.CountPoints < _numPoints)
            {
                var oldBounds = _newFigure.Bounds;
                _newFigure.AddPoint(e.Location);
                _canvas.Repaint(System.Drawing.Rectangle.Union(_newFigure.Bounds, oldBounds));
            }
            else
            {
                var oldBounds = _newFigure.Bounds;
                _newFigure = null;
                _canvas.Repaint(oldBounds);
            }
        }

        /// <summary>
        /// The handler for mouse move events
        /// </summary>
        /// <param name="sender">the object that raised the event</param>
        /// <param name="e">the mouse data</param>
        public override void MouseMove(object sender, MouseEventArgs e)
        {
            if (_newFigure != null)
            {
                var oldBounds = _newFigure.ExpandedBounds;
                _newFigure.LastPoint = e.Location;
                _canvas.Repaint(System.Drawing.Rectangle.Union(_newFigure.Bounds, oldBounds));
            }
        }

        /// <summary>
        /// The handler for mouse up events
        /// </summary>
        /// <param name="sender">the object that raised the event</param>
        /// <param name="e">the mouse data</param>
        public override void MouseUp(object sender, MouseEventArgs e)
        {
            // don't set _newFigure to null - we still need to hold on to it
        }
        #endregion

    }
}
