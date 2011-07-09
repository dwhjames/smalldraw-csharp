
using System.Drawing;

namespace SmallDraw.Locator
{
    /// <summary>
    /// A reshaping locator for the middle of the left-hand side
    /// </summary>
    class LeftMidLocator : ReshapingLocator
    {
        /// <summary>
        /// Initialize a locator for a figure
        /// </summary>
        /// <param name="figure">the associated figure</param>
        public LeftMidLocator(IFigure figure)
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
            get { return _figure.Bounds.Top + (_figure.Size.Height / 2); }
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
                if (value.X < rect.Right - MINWIDTH)
                {
                    _figure.Location = new Point(value.X, rect.Y);
                    _figure.Size = new Size(rect.Right - value.X, rect.Height);
                }
            }
        }
    }
}
