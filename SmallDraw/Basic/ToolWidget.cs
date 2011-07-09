using System;
using System.Drawing;
using System.Windows.Forms;

namespace SmallDraw.Basic
{
    /// <summary>
    /// Tool widgets are UI components used to add figures to a canvas object
    /// or perform some operation on selected figures.
    /// The class is an adapter for the implemented canvas event handler interface.
    /// </summary>
    public class ToolWidget : Control
    {
        #region fields
        /// <summary>
        /// the tool associated with this widget
        /// </summary>
        protected ICanvasEventHandler _handler;

        /// <summary>
        /// the canvas associated with this widget
        /// </summary>
        protected ICanvas _canvas;

        /// <summary>
        /// the button to activate the associated tool
        /// </summary>
        protected Button _button;
        #endregion

        #region constructors
        /// <summary>
        /// Initialize a tool widget with a name, and associated canvas and event handler
        /// </summary>
        /// <param name="name">the name of the widget, to be the button label</param>
        /// <param name="canvas">the associated canvas</param>
        /// <param name="handler">the associated handler</param>
        public ToolWidget(string name, ICanvas canvas, ICanvasEventHandler handler)
        {
            this._canvas = canvas;
            this._handler = handler;
            this._button = new Button();
            _button.Anchor = AnchorStyles.Left;
            _button.Text = name;
            _button.Click += new EventHandler(this.WidgetClick);
            this.Controls.Add(_button);
            this.Size = _button.Size;
        }
        #endregion

        /// <summary>
        /// Click handler for the button
        /// </summary>
        /// <param name="sender">the sending object</param>
        /// <param name="e">the event data</param>
        protected void WidgetClick(object sender, EventArgs e)
        {
            _canvas.ActiveTool = _handler;
        }
    }
}
