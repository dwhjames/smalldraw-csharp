
using System.Drawing;

namespace SmallDraw.Locator
{
    /// <summary>
    /// A reshaping location for the bottom right-hand corner
    /// </summary>
    class BottomRightLocator : ReshapingLocator
    {
        /// <summary>
        /// Initialize the locator for a figure
        /// </summary>
        /// <param name="figure">the associated figure</param>
        public BottomRightLocator(IFigure figure)
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
            get { return _figure.Bounds.Bottom; }
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
                if ((rect.Left + MINWIDTH < value.X) && (rect.Top + MINHEIGHT < value.Y))
                {
                    _figure.Size = new Size(value.X - rect.Left, value.Y - rect.Top);
                }
            }
        }
    }
}
