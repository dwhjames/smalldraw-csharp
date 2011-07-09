
using System.Drawing;

namespace SmallDraw.Locator
{
    /// <summary>
    /// An abstract base class for reshaping locators.
    /// Subclasses will reshape the associated figure
    /// </summary>
    abstract class ReshapingLocator : ILocator
    {
        /// <summary>
        /// The constant minimum width and height for figures
        /// </summary>
        public const int MINWIDTH = 20, MINHEIGHT = 20;

        /// <summary>
        /// the associated figure
        /// </summary>
        protected IFigure _figure;

        /// <summary>
        /// Initialize a reshaping locator for a figure
        /// </summary>
        /// <param name="figure">the associated figure</param>
        public ReshapingLocator(IFigure figure)
        {
            this._figure = figure;
        }

        /// <summary>
        /// Get the x coordinate
        /// </summary>
        public abstract int X
        { get; }

        /// <summary>
        /// Get the y coordinate
        /// </summary>
        public abstract int Y
        { get; }
        
        /// <summary>
        /// Get and set the x,y location
        /// </summary>
        public abstract Point Location
        { get; set; }

        /// <summary>
        /// Translate the x,y location by a width and height
        /// </summary>
        /// <param name="s">the width and height of the translation</param>
        public void Translate(Size s)
        {
            this.SetLocation(this.X + s.Width, this.Y + s.Height);
        }
    }
}
