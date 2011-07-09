
using System.Drawing;

namespace SmallDraw
{
    /// <summary>
    /// Handles are graphical objects that are used to manipulate figures
    /// </summary>
    public interface IHandle
    {
        /// <summary>
        /// Paint the handle
        /// </summary>
        /// <param name="g">Graphics context to paint with</param>
        void Paint(Graphics g);

        /// <summary>
        /// The boundary area of the handle,
        /// used for checking when selected.
        /// </summary>
        Rectangle Bounds { get; }

        /// <summary>
        /// The selection status of the handle.
        /// </summary>
        bool Selected { get; set; }

        /// <summary>
        /// Return true if the given point touches the handle,
        /// ie. is in the bounding rectangle.
        /// </summary>
        /// <param name="p">the (x,y) point</param>
        /// <returns>true if (x,y) touches the handle</returns>
        bool Touches(Point p);

        /// <summary>
        /// The locator the handle uses to determine its position.
        /// </summary>
        ILocator Locator { get; }

        /// <summary>
        /// The location in the canvas that the handle occupies.
        /// Setting involves calling the same method on the locator.
        /// </summary>
        Point Location { get; set; }

        /// <summary>
        /// Tranlate the handle by the given values,
        /// which involves calling the same method on the locator
        /// </summary>
        /// <param name="s">the translation</param>
        void Translate(Size s);
    }

    #region Extension methods for IHandle
    /// <summary>
    /// Extension methods on the IHandle interface
    /// </summary>
    public static class HandleExtensions
    {
        /// <summary>
        /// Conveience method for setting the location of a handle
        /// </summary>
        /// <param name="handle">the implementer of the handle interface</param>
        /// <param name="x">the x coordinate</param>
        /// <param name="y">the y coordinate</param>
        public static void SetLocation(this IHandle handle, int x, int y)
        {
            handle.Location = new Point(x, y);
        }

        /// <summary>
        /// Conveience method for testing if a point touches a handle
        /// </summary>
        /// <param name="handle">the implementer of the handle interface</param>
        /// <param name="x">the x coordinate</param>
        /// <param name="y">the y coordinate</param>
        /// <returns>true if the point touches the handle</returns>
        public static bool Touches(this IHandle handle, int x, int y)
        {
            return handle.Touches(new Point(x, y));
        }

        /// <summary>
        /// Covenience method for translating the location of a handle
        /// </summary>
        /// <param name="handle">the implementer of the handle interface</param>
        /// <param name="p">the x and y translations</param>
        public static void Translate(this IHandle handle, int dx, int dy)
        {
            handle.Translate(new Size(dx, dy));
        }
    }
    #endregion
}
