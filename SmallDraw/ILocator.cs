using System.Drawing;

namespace SmallDraw
{
    /// <summary>
    /// Locators represent locations in space that are derived from other sources.
    /// This interface allows types, such as handles, to be programmed to
    /// interfaces in a generic way.
    /// </summary>
    public interface ILocator
    {
        /// <summary>
        /// The locator's x coordinate
        /// </summary>
        int X { get; }

        /// <summary>
        /// The locator's y coordinate
        /// </summary>
        int Y { get; }

        /// <summary>
        /// The locator's position
        /// </summary>
        Point Location { get; set; }
        
        /// <summary>
        /// Translate the location of the ILocator
        /// </summary>
        /// <param name="s">the translation</param>
        void Translate(Size s);

    }

    #region Extension methods for ILocator
    public static class LocatorExtensions
    {
        /// <summary>
        /// Covenience method to set the position of a locator
        /// </summary>
        /// <param name="locator">the implementor of the ILocator interface</param>
        /// <param name="x">the x coordinate</param>
        /// <param name="y">the y coordinate</param>
        public static void SetLocation(this ILocator locator, int x, int y)
        {
            locator.Location = new Point(x, y);
        }

        /// <summary>
        /// Covenience method to translate a locator
        /// </summary>
        /// <param name="locator">the implementor of the ILocator interface</param>
        /// <param name="dx">the x axis translation</param>
        /// <param name="dy">the y axis translation</param>
        public static void Translate(this ILocator locator, int dx, int dy)
        {
            locator.Translate(new Size(dx, dy));
        }
    }
    #endregion
}
