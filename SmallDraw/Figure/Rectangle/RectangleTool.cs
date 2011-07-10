using System;
using System.Drawing;

namespace SmallDraw.Figure.Rectangle
{
    /// <summary>
    /// The construction tool for rectangles
    /// </summary>
    public class RectangleTool : Basic.ConstructionTool<RectangleFigure>
    {
        /// <summary>
        /// Initialize a rectangle tool
        /// </summary>
        /// <param name="canvas"></param>
        public RectangleTool(ICanvas canvas)
            : base(canvas)
        { }

        /// <summary>
        /// Create a new rectangle figure at a given location
        /// </summary>
        /// <param name="p">the location for the figure</param>
        /// <returns>a rectangle figure</returns>
        protected override RectangleFigure NewFigureAt(Point p)
        {
            return new RectangleFigure(_canvas, p, Size.Empty);
        }
    }
}
