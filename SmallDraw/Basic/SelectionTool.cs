
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SmallDraw.Basic
{
    /// <summary>
    /// The selection tool is used to select figures on a canvas.
    /// It handles setting the selection rectangle of the canvas
    /// to select multiple figures, and selects single figures
    /// based on where the mouse is clicked.
    /// </summary>
    public class SelectionTool : BasicCanvasEventHandler
    {
        #region fields
        /// <summary>
        /// the selection box defining the canvas area
        /// in which selected figures would fall
        /// </summary>
        protected Rectangle _selectionBox;

        /// <summary>
        /// the point where the mouse was pressed before
        /// being dragged when defining the selection box
        /// </summary>
        protected Point _startPoint;

        /// <summary>
        /// the selected handle
        /// </summary>
        protected IHandle _selectedHandle;

        /// <summary>
        /// the selected figure
        /// </summary>
        protected IFigure _selectedFigure;

        /// <summary>
        /// the figure locator
        /// </summary>
        protected ILocator _figureLocator;
        #endregion

        #region constructors
        /// <summary>
        /// Instantiates a new selection tool
        /// </summary>
        /// <param name="canvas">the associated canvas</param>
        public SelectionTool(ICanvas canvas)
            : base(canvas)
        { }
        #endregion

        #region overriding implementation of BasicCanvasEventHandler
        public override bool Active
        {
            get
            {
                return base.Active;
            }
            set
            {
                // overide so that existing selection is not cleared
                _active = value;
            }
        }

        public override void MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            foreach (var f in _canvas.SelectedFigures)
            {
                foreach (var h in f.Handles)
                {
                    if (h.Touches(e.X, e.Y))
                        _selectedHandle = h;
                }
            }
            if (_selectedHandle != null) // mouse was pressed over a handle
            {
                _selectedHandle.Selected = true;
            }
            else
            {
                _canvas.ClearSelected();
                _selectedFigure = _canvas.FindFigureAtPoint(e.Location);
                if (_selectedFigure != null) // mouse was pressed over a figure
                {
                    _selectedFigure.Selected = true;
                    _canvas.TopFigure = _selectedFigure;
                    _figureLocator = _selectedFigure.RelativeLocator(e.Location);
                }
                else // just on the canvas, so make a selection rectangle
                {
                    _startPoint = e.Location;
                    _selectionBox = new Rectangle(e.Location, Size.Empty);
                    _canvas.SelectionRectangle = _selectionBox;
                    _canvas.Repaint(Rectangle.Inflate(_selectionBox, 1, 1));
                }
            }
        }

        public override void MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            if (_selectedHandle != null) // a locator is being dragged
            {
                _selectedHandle.Location = e.Location;
            }
            else if (_selectedFigure != null) // a figure is being dragged
            {
                _figureLocator.Location = e.Location;
            }
            else // resize the selection box
            {
                var oldBounds = Rectangle.Inflate(_selectionBox, 1, 1);
                // create new selection box
                _selectionBox = Util.Geometry.RectangleFromPoints(_startPoint, e.Location);
                _canvas.SelectionRectangle = _selectionBox;
                _canvas.Repaint(Rectangle.Union(oldBounds, Rectangle.Inflate(_selectionBox, 1, 1)));
            }
        }

        public override void MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            if (_selectedHandle != null)
            {
                _selectedHandle.Selected = false;
                _selectedHandle = null;
            }
            else if (_selectedFigure != null)
            {
                _selectedFigure = null;
            }
            else
            {
                _canvas.SelectionRectangle = Rectangle.Empty;
                _canvas.Repaint(Rectangle.Inflate(_selectionBox, 1, 1));
                _selectionBox = Rectangle.Empty;
            }
        }
        #endregion
    }
}
