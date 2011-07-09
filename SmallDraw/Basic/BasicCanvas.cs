using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SmallDraw.Basic
{
    /// <summary>
    /// The canvas defines a drawing area on which a set of figures are drawn.
    /// The class provides methods for managing which figures are drawn and
    /// determining which are selected by the user.
    /// </summary>
    public class BasicCanvas : Control, ICanvas
    {
        #region fields
        /// <summary>
        /// the current active tool used to listen to mouse events from the canvas
        /// </summary>
        protected ICanvasEventHandler _activeHandler;

        /// <summary>
        /// the list of figures drawn on this canvas
        /// </summary>
        protected IList<IFigure> _figures = new List<IFigure>();

        /// <summary>
        /// any figure within this rectangle, if non-empty, is set as being selected
        /// </summary>
        protected Rectangle _selectionRectangle = Rectangle.Empty;
        #endregion

        #region constructors
        /// <summary>
        /// Initialize a canvas
        /// </summary>
        public BasicCanvas()
            : base()
        {
            this.DoubleBuffered = true;
        }
        #endregion

        #region overriding System.Windows.Forms.Control

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.White, e.ClipRectangle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;

            // g.FillRectangle(Brushes.White, this.Bounds);

            // shade the selection rectangle under all figures
            if (!_selectionRectangle.IsEmpty)
            {
                g.FillRectangle(Brushes.WhiteSmoke, _selectionRectangle);
            }

            // paint each figure, with handles if selected
            foreach (var f in _figures)
            {
                f.Paint(g);
                if (f.Selected)
                {
                    foreach (var h in f.Handles)
                    {
                        h.Paint(g);
                    }
                }
            }

            // now redraw the outline of the selection rectangle, on top of all figures
            if (!_selectionRectangle.IsEmpty)
            {
                g.DrawRectangle(Pens.Red, _selectionRectangle);
            }
        }
        #endregion

        #region implementation of ICanvas
        /// <summary>
        /// Set the active tool for the canvas
        /// </summary>
        public ICanvasEventHandler ActiveTool
        {
            set
            {
                if (_activeHandler != null)
                {
                    _activeHandler.Active = false;
                    this.MouseDown -= new MouseEventHandler(_activeHandler.MouseDown);
                    this.MouseMove -= new MouseEventHandler(_activeHandler.MouseMove);
                    this.MouseUp -= new MouseEventHandler(_activeHandler.MouseUp);
                }

                _activeHandler = value;
                this.MouseDown += new MouseEventHandler(_activeHandler.MouseDown);
                this.MouseMove += new MouseEventHandler(_activeHandler.MouseMove);
                this.MouseUp += new MouseEventHandler(_activeHandler.MouseUp);
                _activeHandler.Active = true;
            }
        }

        /// <summary>
        /// Add a figure to the canvas
        /// </summary>
        /// <param name="figure">the figure to be added</param>
        public void AddFigure(IFigure figure)
        {
            if (!_figures.Contains(figure))
            {
                // add to the end of the list
                _figures.Add(figure);
            }
            Repaint(figure.Bounds);
        }

        /// <summary>
        /// Remove a figure from the canvas
        /// </summary>
        /// <param name="figure">the figure to be be removed</param>
        public void RemoveFigure(IFigure figure)
        {
            _figures.Remove(figure);
            Repaint(figure.ExpandedBounds);
        }

        /// <summary>
        /// Get an enumeration of the figures on the canvas
        /// </summary>
        public IEnumerable<IFigure> Figures
        {
            get { return _figures; }
        }

        /// <summary>
        /// Find the figure, if it exists, at a point
        /// </summary>
        /// <param name="p">the point to test</param>
        /// <returns>the figure at the point, or null if none</returns>
        public IFigure FindFigureAtPoint(Point p)
        {
            foreach (var f in _figures)
            {
                if (f.Touches(p))
                {
                    return f;
                }
            }
            return null;
        }

        /// <summary>
        /// Set the bounds of the selection on the canvas
        /// </summary>
        public Rectangle SelectionRectangle
        {
            set
            {
                _selectionRectangle = value;
                if (!value.IsEmpty)
                {
                    foreach (var f in _figures)
                    {
                        f.Selected = value.Contains(f.Bounds);
                    }
                }
            }
        }

        /// <summary>
        /// Clear the selection on the canvas
        /// </summary>
        public void ClearSelected()
        {
            foreach (var f in _figures)
            {
                f.Selected = false;
            }
        }

        /// <summary>
        /// Get an enumeration of the selected figures on the canvas
        /// </summary>
        public IEnumerable<IFigure> SelectedFigures
        {
            get
            {
                var selectedFigures = new List<IFigure>();
                foreach (var f in _figures)
                {
                    if (f.Selected)
                    {
                        selectedFigures.Add(f);
                    }
                }
                return selectedFigures;
            }
        }

        /// <summary>
        /// Set the top-most figure on the canvas
        /// </summary>
        public IFigure TopFigure
        {
            set
            {
                _figures.Remove(value);
                _figures.Add(value);
            }
        }

        public void Repaint(Rectangle r)
        {
            this.Invalidate();
        }
        #endregion
    }
}
