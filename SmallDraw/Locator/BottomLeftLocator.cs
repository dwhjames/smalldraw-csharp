
using System;
using System.Drawing;

namespace SmallDraw.Locator
{
    /// <summary>
    /// A reshaping locator for the bottom left-hand corner
    /// </summary>
    class BottomLeftLocator : ReshapingLocator
    {
        /// <summary>
        /// Initialize a locator for a figure
        /// </summary>
        /// <param name="figure">the associated figure</param>
        public BottomLeftLocator(IFigure figure)
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
                // if x is to the left of the right edge, and y is below the top edge
                if ((value.X < rect.Right - MINWIDTH) && (rect.Top + MINHEIGHT < value.Y))
                {
                    _figure.Location = new Point(value.X, rect.Y);
                    _figure.Size = new Size(rect.Right - value.X, value.Y - rect.Top);
                }
            }
        }
    }
}
