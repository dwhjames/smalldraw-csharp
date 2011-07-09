
using System.Drawing;

namespace SmallDraw.Locator
{
    /// <summary>
    /// A reshaping locator for the middle of the bottom edge
    /// </summary>
    class BottomMidLocator : ReshapingLocator
    {
        /// <summary>
        /// Initialize a locator for a figure
        /// </summary>
        /// <param name="figure">the associated figure</param>
        public BottomMidLocator(IFigure figure)
            : base(figure)
        { }

        /// <summary>
        /// Get the x coordinate
        /// </summary>
        public override int X
        {
            get { return _figure.Bounds.Left + (_figure.Size.Width / 2); }
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
                if (rect.Top + MINHEIGHT < value.Y)
                {
                    _figure.Size = new Size(rect.Width, value.Y - rect.Top);
                }
            }
        }
    }
}
