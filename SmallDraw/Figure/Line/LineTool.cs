using System;
using System.Drawing;
using System.Windows.Forms;

namespace SmallDraw.Figure.Line
{
    /// <summary>
    /// A construction tool for lines
    /// </summary>
    public class LineTool : Basic.ConstructionTool<LineFigure>
    {
        /// <summary>
        /// Instantiates a new line tool
        /// </summary>
        /// <param name="canvas">the associated canvas</param>
        public LineTool(ICanvas canvas)
            : base(canvas)
        { }

        protected override LineFigure NewFigureAt(Point p)
        {
            return new LineFigure(_canvas, p);
        }

        #region override ConstructionTool
        public override void MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var oldBounds = _newFigure.Bounds;
                _newFigure.End = e.Location;
                _canvas.Repaint(System.Drawing.Rectangle.Inflate(oldBounds, 1, 1));
            }
        }

        public override void MouseUp(object sender, MouseEventArgs e)
        {
            if (_newFigure.Length < 10) // remove short lines
            {
                _canvas.RemoveFigure(_newFigure);
            }

            base.MouseUp(sender, e);
        }
        #endregion
    }
}
