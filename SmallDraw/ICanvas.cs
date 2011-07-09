
using System.Drawing;
using System.Collections.Generic;

namespace SmallDraw
{
    public interface ICanvas
    {
        /// <summary>
        /// Sets the active tool and unsets the previous active tool if there was
        /// one. The active tool is set as the mouse event handler for the canvas.
        /// </summary>
        ICanvasEventHandler ActiveTool { set; }

        /// <summary>
        /// Add the give figure to the set of figures to draw.
        /// </summary>
        /// <param name="figure">the new figure</param>
        void AddFigure(IFigure figure);

        /// <summary>
        /// Remove a figure from the canvas
        /// </summary>
        /// <param name="figure">the figure</param>
        void RemoveFigure(IFigure figure);

        /// <summary>
        /// Get a list of all figures
        /// </summary>
        IEnumerable<IFigure> Figures { get; }

        /// <summary>
        /// If a figure is at the given coordinates, then return it,
        /// otherwise return null
        /// </summary>
        /// <param name="p">x,y coordinates</param>
        /// <returns>the figure, if it exists, at x,y</returns>
        IFigure FindFigureAtPoint(Point p);

        /// <summary>
        /// Set the selection rectange to the given rectangle.
        /// Any figure that falls within the rectangle has its
        /// selection set to true.
        /// If the rectangle is empty, the selection rectangle
        /// is set to empty and the rectangle is not drawn
        /// nor is the selection state changed.
        /// </summary>
        Rectangle SelectionRectangle { set; }

        /// <summary>
        /// Set the selection state of all figures to false
        /// </summary>
        void ClearSelected();

        /// <summary>
        /// Get a list of all selected figures, which may be empty
        /// </summary>
        IEnumerable<IFigure> SelectedFigures { get; }

        /// <summary>
        /// Sets the given figure to be the top-most in the drawing order,
        /// that is it's drawn last.
        /// </summary>
        IFigure TopFigure { set; }

        /// <summary>
        /// Repaint the canvas in the given area, such that any figures
        /// that fall in the area are redrawn after the area is cleared
        /// </summary>
        /// <param name="r">the area to redraw</param>
        void Repaint(Rectangle r);
    }

    #region Extension methods for ICanvas
    public static class CanvasExtensions
    {
        /// <summary>
        /// Convenience method to find a figure at a point
        /// </summary>
        /// <param name="canvas">the implementer of the ICanvas interface</param>
        /// <param name="x">the x coordinate</param>
        /// <param name="y">the y coordinate</param>
        /// <returns>the figure, if any, at this point</returns>
        public static IFigure FindFigureAtPoint(this ICanvas canvas, int x, int y)
        {
            return canvas.FindFigureAtPoint(new Point(x, y));
        }
    }
    #endregion
}
