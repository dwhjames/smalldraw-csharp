using System.Drawing;
using System.Collections.Generic;

namespace SmallDraw
{
    /// <summary>
    /// Figures are objects that can be drawn on a Canvas object,
    /// have bounding rectangles, can be moved and reshaped,
    /// and have a set of handles to allow the user to manipulate them.
    /// </summary>
    public interface IFigure
    {
        /// <summary>
        /// Draw the figure
        /// </summary>
        /// <param name="g">the graphics device to draw with figure</param>
        void Paint(Graphics g);

        /// <summary>
        /// Get the smallest rectangle containing the figure
        /// </summary>
        Rectangle Bounds { get; }

        /// <summary>
        /// Get the smallest rectangle containing the figure that also
        /// incloses any handles, assuming they are centered along
        /// the edges of the smallest rectangle enclosing the figure itself
        /// </summary>
        Rectangle ExpandedBounds { get; }

        /// <summary>
        /// Get the list of handles used the control this figure
        /// </summary>
        IEnumerable<IHandle> Handles { get; }

        /// <summary>
        /// Get the locator object that locates the center of the figure
        /// </summary>
        ILocator CenterLocator { get; }

        /// <summary>
        /// Get and set the selection status of the figure
        /// </summary>
        bool Selected { get; set; }

        /// <summary>
        /// Determine if the given point touches the figure
        /// </summary>
        /// <param name="p">the point to test</param>
        /// <returns>true if p touches the figure</returns>
        bool Touches(Point p);

        /// <summary>
        /// Get and set the position of the figure
        /// </summary>
        Point Location { get; set; }

        /// <summary>
        /// Move the Figure by the given width and height amounts
        /// </summary>
        /// <param name="s">width and height to move the figure</param>
        void Translate(Size s);

        /// <summary>
        /// Get and set the dimensions of the figure
        /// </summary>
        Size Size { get; set; }

        /// <summary>
        /// Create a locator that always locates a relative point on
        /// the figure, which then scales if the figure's dimensions
        /// change. E.g. if the method is called with arguments (0,0)
        /// then a locator that always points to the top left corner
        /// of the figure will be returned. If values (w/2,h/2) are
        /// used, assuming the figure's dimensions are (w,h), then the
        /// locator will point to the figure's center, even if the
        /// figure's dimensions change.
        /// </summary>
        /// <param name="p">the relative point</param>
        /// <returns>a relative locator</returns>
        ILocator RelativeLocator(Point p);

        /// <summary>
        /// Create a copy of the current figure that is structually
        /// identical to the current object but located at (x,y).
        /// This method is part of the solutions for the Prototype
        /// practical and not present in the base library. If you
        /// want to integrate the solutions into the SmallDraw
        /// framework you might have to rename the method you used
        /// for your prototype implementation to clone.
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <returns>a figure that is structually identical to the current object</returns>
        // IFigure clone(int x, int y);
    }

    #region Extension methods for IFigure
    public static class FigureExtensions
    {
        /// <summary>
        /// Conveience method for testing if a point touches a figure
        /// </summary>
        /// <param name="figure">the implementer of the figure interface</param>
        /// <param name="x">the x coordinate</param>
        /// <param name="y">the y coordinate</param>
        /// <returns>true if the point touches the figure</returns>
        public static bool Touches(this IFigure figure, int x, int y)
        {
            return figure.Touches(new Point(x, y));
        }

        /// <summary>
        /// Convenience method for getting a relative locator
        /// </summary>
        /// <param name="figure">the implementer of the figure interface</param>
        /// <param name="x">the x coordinate</param>
        /// <param name="y">the y coordinate</param>
        /// <returns>the relative locator</returns>
        public static ILocator RelativeLocator(this IFigure figure, int x, int y)
        {
            return figure.RelativeLocator(new Point(x, y));
        }

        /// <summary>
        /// Convenience method for translating a figure
        /// </summary>
        /// <param name="figure">the implementer of the figure interface</param>
        /// <param name="dx">the x axis translation</param>
        /// <param name="dy">the y axis translation</param>
        public static void Translate(this IFigure figure, int dx, int dy)
        {
            figure.Translate(new Size(dx, dy));
        }
    }
    #endregion

}
