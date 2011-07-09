using System.Drawing;

namespace SmallDraw.Locator
{
    /// <summary>
    /// A reshaping locator for the top left-hand corner
    /// </summary>
    class TopLeftLocator : ReshapingLocator
    {
        /// <summary>
        /// Initialize a locator for a figure
        /// </summary>
        /// <param name="figure">the associated figure</param>
        public TopLeftLocator(IFigure figure)
            : base(figure)
        { }

        /// <summary>
        /// Get the x coordinate
        /// </summary>
        public override int X
        {
            get { return _figure.Bounds.Left; }
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
                if ((value.X < rect.Right - MINWIDTH) && (value.Y < rect.Bottom - MINHEIGHT))
                {
                    _figure.Location = value;
                    _figure.Size = new Size(rect.Right - value.X, rect.Bottom - value.Y);
                }
            }
        }
    }
}
