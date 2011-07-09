using System.Windows.Forms;

namespace SmallDraw.Basic
{
    /// <summary>
    /// A basic implementation of the ICanvasEventHandler interface,
    /// which provides base methods for the mouse events.
    /// This is used as the base class for tools.
    /// </summary>
    public class BasicCanvasEventHandler : ICanvasEventHandler
    {
        #region fields
        /// <summary>
        /// the activity state
        /// </summary>
        protected bool _active = false;

        /// <summary>
        /// the canvas associated with this event handler
        /// </summary>
        protected ICanvas _canvas;
        #endregion

        #region constructors
        /// <summary>
        /// Construct a handler for a canvas
        /// </summary>
        /// <param name="canvas">the canvas to associate with</param>
        public BasicCanvasEventHandler(ICanvas canvas)
        {
            this._canvas = canvas;
        }
        #endregion

        #region implementation of ICanvasEventHandler
        /// <summary>
        /// Get and set the activity state of this handler.
        /// If the handler becomes active, clear the canvas' selections.
        /// </summary>
        public virtual bool Active
        {
            get
            {
                return _active;
            }
            set
            {
                this._active = value;
                if (value)
                    _canvas.ClearSelected();
            }
        }

        public virtual void MouseDown(object sender, MouseEventArgs e)
        { }

        public virtual void MouseMove(object sender, MouseEventArgs e)
        { }

        public virtual void MouseUp(object sender, MouseEventArgs e)
        { }
        #endregion
    }
}
