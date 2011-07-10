using System;
using System.Drawing;

namespace SmallDraw.Figure.Line
{
    /// <summary>
    /// This handle is used to create an manipulate a connecting line figure
    /// rather than move the figure it's associated with
    /// </summary>
    public class ConnectorHandle : Basic.LocatorHandle
    {
        #region fields
        /// <summary>
        /// the figure the handle is associated with
        /// </summary>
        protected IFigure _owner;

        /// <summary>
        /// the line that is created when the handle is selected
        /// </summary>
        protected ConnectingLineFigure _newLine;

        /// <summary>
        /// the end point of the current line
        /// which can be used to reshape the line
        /// </summary>
        protected Point _endPoint;
        #endregion

        #region constructors
        /// <summary>
        /// Initialize the handle with the given owner and point location
        /// from which the handle's locator is constructed
        /// </summary>
        /// <param name="locator">the locator</param>
        /// <param name="canvas">the associated canvas</param>
        /// <param name="owner">the owning figure</param>
        public ConnectorHandle(ILocator locator, ICanvas canvas, IFigure owner)
            : base(locator, canvas)
        {
            this._owner = owner;
        }
        #endregion

        #region overriding of LocatorHandle
        /// <summary>
        /// Draw the connector handle.
        /// Red if selected, black if not.
        /// </summary>
        /// <param name="g">the graphics context</param>
        public override void Paint(Graphics g)
        {
            var b = _selected ? Brushes.Red : Brushes.Black;
            g.FillEllipse(b, _locator.X - WIDTH / 2, _locator.Y - HEIGHT / 2, WIDTH, HEIGHT);
        }

        /// <summary>
        /// Get and set the location of the handle
        /// </summary>
        public override Point Location
        {
            get
            {
                return base.Location;
            }
            set
            {
                if (_newLine != null)
                    _newLine.EndLocation = value;
            }
        }

        /// <summary>
        /// If select is true then construct a new point for endpoint and a new line
        /// </summary>
        public override bool Selected
        {
            get
            {
                return base.Selected;
            }
            set
            {
                base.Selected = value;
                if (value)
                {
                    _endPoint = _locator.Location;
                    _newLine = new ConnectingLineFigure(_canvas, _owner, _endPoint);
                    _canvas.AddFigure(_newLine);
                }
                else
                {
                    var end = _newLine.EndLocator;
                    var f = _canvas.FindFigureAtPoint(end.Location);
                    if (f == null || f == _owner)
                    {
                        _canvas.RemoveFigure(_newLine);
                    }
                    else
                    {
                        _newLine.EndFigure = f;
                    }
                    _newLine = null;
                }
            }
        }
        #endregion

        /// <summary>
        /// Get the line this handle creates.
        /// </summary>
        public ConnectingLineFigure Line
        {
            get { return _newLine; }
        }
    }
}
