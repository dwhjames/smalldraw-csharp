using System.Windows.Forms;

namespace SmallDraw
{
    /// <summary>
    /// An interface for handling events coming from a Canvas object.
    /// </summary>
    public interface ICanvasEventHandler
    {
        /// <summary>
        /// Get and set the activity state of this handler,
        /// this is set to true when the handler becomes the
        /// current active handler for its associated canvas.
        /// </summary>
        bool Active { get; set; }

        /// <summary>
        /// A handler for MouseDown mouse events
        /// </summary>
        /// <param name="sender">the sending object</param>
        /// <param name="e">the data of the mouse event</param>
        void MouseDown(object sender, MouseEventArgs e);

        /// <summary>
        /// A handler for MouseMove mouse events
        /// </summary>
        /// <param name="sender">the sending object</param>
        /// <param name="e">the data of the mouse event</param>
        void MouseMove(object sender, MouseEventArgs e);

        /// <summary>
        /// A handler for MouseUp mouse events
        /// </summary>
        /// <param name="sender">the sending object</param>
        /// <param name="e">the data of the mouse event</param>
        void MouseUp(object sender, MouseEventArgs e);
    }
}
