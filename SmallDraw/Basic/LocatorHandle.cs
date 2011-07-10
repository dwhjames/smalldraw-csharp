using System;
using System.Drawing;

namespace SmallDraw.Basic
{
    /// <summary>
    /// This handle uses a locator to determine and change its position
    /// </summary>
    public class LocatorHandle : IHandle
    {
        #region fields
        /// <summary>
        /// constant dimension values for every handle
        /// </summary>
        public const int WIDTH = 8, HEIGHT = 8;

        /// <summary>
        /// the selection status of the handle
        /// </summary>
        protected bool _selected = false;

        /// <summary>
        /// the locator used to determine and change position
        /// </summary>
        protected ILocator _locator;

        /// <summary>
        /// the associated canvas
        /// </summary>
        protected ICanvas _canvas;
        #endregion

        #region constructors
        /// <summary>
        /// Instantiate a locator handle
        /// </summary>
        /// <param name="locator">the locator</param>
        /// <param name="canvas">the associated canvas</param>
        public LocatorHandle(ILocator locator, ICanvas canvas)
        {
            this._locator = locator;
            this._canvas = canvas;
        }
        #endregion

        #region implementation of IHandle
        /// <summary>
        /// Draw the handle as a black box if not selected,
        /// or a red box if selected.
        /// </summary>
        /// <param name="g"></param>
        public virtual void Paint(Graphics g)
        {
            var p = Selected ? Pens.Red : Pens.Black;
            g.DrawRectangle(p, this.Bounds);
        }

        /// <summary>
        /// Get the bounds of the handle
        /// </summary>
        public Rectangle Bounds
        {
            get { return new Rectangle(_locator.X - WIDTH / 2, _locator.Y - HEIGHT / 2, WIDTH, HEIGHT); }
        }

        /// <summary>
        /// Get and set the selection status of the handle
        /// </summary>
        public virtual bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
                var r = this.Bounds;
                r.Inflate(1, 1);
                _canvas.Repaint(r);
            }
        }

        /// <summary>
        /// Test if a given point touches the handle
        /// </summary>
        /// <param name="p">the point to test</param>
        /// <returns>true if the point touches the handle</returns>
        public bool Touches(Point p)
        {
            return Math.Abs(_locator.X - p.X) <= WIDTH / 2 && Math.Abs(_locator.Y - p.Y) <= HEIGHT / 2;
        }

        /// <summary>
        /// Get the underlying locator for this handle
        /// </summary>
        public ILocator Locator
        {
            get { return _locator; }
        }

        /// <summary>
        /// Get and set the location of the handle.
        /// </summary>
        public virtual Point Location
        {
            get
            {
                return _locator.Location;
            }
            set
            {
                var oldBounds = this.Bounds;
                oldBounds.Inflate(1, 1);
                _locator.Location = value;
                var newBounds = this.Bounds;
                newBounds.Inflate(1, 1);
                _canvas.Repaint(Rectangle.Union(oldBounds, newBounds));
            }
        }

        /// <summary>
        /// Translate the handle by a width and height
        /// </summary>
        /// <param name="s">the translation dimensions</param>
        public void Translate(Size s)
        {
            var oldBounds = this.Bounds;
            oldBounds.Inflate(1, 1);
            _locator.Translate(s);
            var newBounds = this.Bounds;
            newBounds.Inflate(1, 1);
            _canvas.Repaint(Rectangle.Union(oldBounds, newBounds));
        }
        #endregion
    }
}
