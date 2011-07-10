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

        /// <summary>
        /// Create a line figure at a given location
        /// </summary>
        /// <param name="p">the location for the new figure</param>
        /// <returns>a line figure</returns>
        protected override LineFigure NewFigureAt(Point p)
        {
            return new LineFigure(_canvas, p);
        }

        #region override ConstructionTool
        /// <summary>
        /// A handler for mouse move events
        /// </summary>
        /// <param name="sender">the object that generated the event</param>
        /// <param name="e">the event data</param>
        public override void MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var oldBounds = _newFigure.Bounds;
                _newFigure.End = e.Location;
                _canvas.Repaint(System.Drawing.Rectangle.Inflate(oldBounds, 1, 1));
            }
        }

        /// <summary>
        /// A handler for mouse up events
        /// </summary>
        /// <param name="sender">the object that generated the event</param>
        /// <param name="e">the event data</param>
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
