using System;
using System.Drawing;

namespace SmallDraw.Figure.Rectangle
{
    /// <summary>
    /// A construction tool for filled rectangles
    /// </summary>
    public class FilledRectangleTool : Basic.ConstructionTool<FilledRectangleFigure>
    {
        public FilledRectangleTool(ICanvas canvas)
            : base(canvas)
        { }

        protected override FilledRectangleFigure NewFigureAt(Point p)
        {
            return new FilledRectangleFigure(_canvas, p, Size.Empty);
        }
    }
}
