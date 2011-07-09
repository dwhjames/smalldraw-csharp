using System;
using System.Drawing;
using System.Windows.Forms;

namespace SmallDraw.Figure.Polygon
{
    /// <summary>
    /// 
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
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="numPoints"></param>
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
        protected override PolygonFigure NewFigureAt(Point p)
        {
            return new PolygonFigure(_canvas, p);
        }

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

        public override void MouseMove(object sender, MouseEventArgs e)
        {
            if (_newFigure != null)
            {
                var oldBounds = _newFigure.ExpandedBounds;
                _newFigure.LastPoint = e.Location;
                _canvas.Repaint(System.Drawing.Rectangle.Union(_newFigure.Bounds, oldBounds));
            }
        }

        public override void MouseUp(object sender, MouseEventArgs e)
        {
            // don't set _newFigure to null - we still need to hold on to it
        }
        #endregion

    }
}
