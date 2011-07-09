using System;
using System.Drawing;

namespace SmallDraw.Figure.Rectangle
{
    /// <summary>
    /// The construction tool for rectangles
    /// </summary>
    public class RectangleTool : Basic.ConstructionTool<RectangleFigure>
    {
        public RectangleTool(ICanvas canvas)
            : base(canvas)
        { }

        protected override RectangleFigure NewFigureAt(Point p)
        {
            return new RectangleFigure(_canvas, p, Size.Empty);
        }
    }
}
