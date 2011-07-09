using System.Drawing;

namespace SmallDraw.Locator
{
    /// <summary>
    /// A reshaping locator for the middle of the top edge
    /// </summary>
    class TopMidLocator : ReshapingLocator
    {
        /// <summary>
        /// Initialize a locator for a figure
        /// </summary>
        /// <param name="figure">the associate figure</param>
        public TopMidLocator(IFigure figure)
            : base(figure)
        { }

        /// <summary>
        /// Get the x coordinate
        /// </summary>
        public override int X
        {
            get { return _figure.Bounds.Left + (_figure.Bounds.Width / 2); }
        }

        /// <summary>
        /// Get the y coodinate
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
                if (value.Y < rect.Bottom - MINHEIGHT)
                {
                    _figure.Location = new Point(rect.X, value.Y);
                    _figure.Size = new Size(rect.Width, rect.Bottom - value.Y);
                }
            }
        }
    }
}
