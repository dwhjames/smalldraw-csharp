using System.Drawing;


namespace SmallDraw.Locator
{
    /// <summary>
    /// 
    /// </summary>
    public class ProportionalLocator : ILocator
    {
        #region fields
        /// <summary>
        /// the figure the locator is associated with
        /// </summary>
        protected IFigure _figure;

        /// <summary>
        /// the relative location
        /// </summary>
        protected SizeF _relative;
        #endregion

        #region constructors
        /// <summary>
        /// Instantiate a new proportional locator with the given
        /// figure and relative size
        /// </summary>
        /// <param name="figure">the associated figure</param>
        /// <param name="location">the relative location</param>
        public ProportionalLocator(IFigure figure, SizeF relative)
        {
            this._figure = figure;
            this._relative = relative;
        }

        public ProportionalLocator(IFigure figure, float rx, float ry)
            : this(figure, new SizeF(rx, ry))
        { }

        public ProportionalLocator(IFigure figure)
            : this(figure, 0.5f, 0.5f)
        { }
        #endregion

        #region implementation of ILocator
        /// <summary>
        /// Get x coordinate
        /// </summary>
        public int X
        {
            get
            {
                var r = _figure.Bounds;
                return Util.Geometry.Prorata(r.X, r.X + r.Width, _relative.Width);
            }
        }

        /// <summary>
        /// Get y coordinate
        /// </summary>
        public int Y
        {
            get
            {
                var r = _figure.Bounds;
                return Util.Geometry.Prorata(r.Y, r.Y + r.Height, _relative.Height);
            }
        }

        /// <summary>
        /// Get and set x,y, location
        /// </summary>
        public Point Location
        {
            get
            {
                return new Point(this.X, this.Y);
            }
            set
            {
                this.Translate(value.X - this.X, value.Y - this.Y);
            }
        }

        /// <summary>
        /// Translate the underlying figure through this locator
        /// </summary>
        /// <param name="s">the translation</param>
        public void Translate(Size s)
        {
            _figure.Translate(s);
        }
        #endregion
    }
}
