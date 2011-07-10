using System;
using System.Drawing;

namespace SmallDraw.Figure.Rectangle
{
    /// <summary>
    /// A construction tool for filled rectangles
    /// </summary>
    public class FilledRectangleTool : Basic.ConstructionTool<FilledRectangleFigure>
    {
        /// <summary>
        /// Initialize a filled rectangle tool
        /// </summary>
        /// <param name="canvas">the associated canvas</param>
        public FilledRectangleTool(ICanvas canvas)
            : base(canvas)
        { }

        /// <summary>
        /// Create a new filled rectangle figure at a given location
        /// </summary>
        /// <param name="p">the location for the figure</param>
        /// <returns>a filled rectangle figure</returns>
        protected override FilledRectangleFigure NewFigureAt(Point p)
        {
            return new FilledRectangleFigure(_canvas, p, Size.Empty);
        }
    }
}
