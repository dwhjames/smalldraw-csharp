using System.Drawing;

namespace SmallDraw.Locator
{
    /// <summary>
    /// A reshaping locator for the top right-hand corner
    /// </summary>
    class TopRightLocator : ReshapingLocator
    {
        /// <summary>
        /// Initialize the locator for a figure
        /// </summary>
        /// <param name="figure">the associated figure</param>
        public TopRightLocator(IFigure figure)
            : base(figure)
        { }

        /// <summary>
        /// Get the x coordinate
        /// </summary>
        public override int X
        {
            get { return _figure.Bounds.Right; }
        }

        /// <summary>
        /// Get the y coordinate
        /// </summary>
        public override int Y
        {
            get { return _figure.Bounds.Top; }
        }

        /// <summary>
        /// Get and set the x,y location
        /// </summary>
        public override Point Location
        {
            get
            {
                return new Point(this.X, this.Y);
            }
            set
            {
                var rect = _figure.Bounds;
                if ((rect.Left + MINWIDTH < value.X) && (value.Y < rect.Bottom - MINHEIGHT))
                {
                    _figure.Location = new Point(rect.X, value.Y);
                    _figure.Size = new Size(value.X - rect.Left, rect.Bottom - value.Y);
                }
            }
        }
    }
}
