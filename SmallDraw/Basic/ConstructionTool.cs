using System.Drawing;
using System.Windows.Forms;

namespace SmallDraw.Basic
{
    /// <summary>
    /// A constructor tool constructs new figures of type F for its associated canvas
    /// </summary>
    /// <typeparam name="F"></typeparam>
    public abstract class ConstructionTool<F> : BasicCanvasEventHandler where F : IFigure
    {
        #region fields
        /// <summary>
        /// the new figure
        /// </summary>
        protected F _newFigure;

        /// <summary>
        /// the starting point
        /// </summary>
        protected Point _start;
        #endregion

        #region constructors
        /// <summary>
        /// Instantiate a constructor tool for the given canvas
        /// </summary>
        /// <param name="canvas"></param>
        public ConstructionTool(ICanvas canvas)
            : base(canvas)
        { }
        #endregion

        /// <summary>
        /// Generate a new figure at the given location
        /// 
        /// This is an Abstract Factory Method
        /// </summary>
        /// <param name="p">the location for the figure</param>
        /// <returns></returns>
        protected abstract F NewFigureAt(Point p);

        #region overriding of BasicCanvasEventHandler
        public override void MouseDown(object sender, MouseEventArgs e)
        {
            base.MouseDown(sender, e);

            _start = e.Location;
            _newFigure = NewFigureAt(e.Location);
            _canvas.AddFigure(_newFigure);
        }

        public override void MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var oldBounds = _newFigure.Bounds;
            var newBounds = Util.Geometry.RectangleFromPoints(_start, e.Location);
            _newFigure.Location = newBounds.Location;
            _newFigure.Size = newBounds.Size;
            _canvas.Repaint(Rectangle.Inflate(oldBounds, 1, 1));
        }

        public override void MouseUp(object sender, MouseEventArgs e)
        {
            base.MouseUp(sender, e);

            _newFigure = default(F);
        }
        #endregion
    }
}
