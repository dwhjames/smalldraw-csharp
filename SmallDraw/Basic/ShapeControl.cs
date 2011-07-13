using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmallDraw.Basic
{
    /// <summary>
    /// A user control for constructing shapes on a canvas
    /// </summary>
    public partial class ShapeControl : UserControl
    {
        /// <summary>
        /// the associated canvas
        /// </summary>
        protected ICanvas _canvas;

        /// <summary>
        /// the associated canvas event handler
        /// </summary>
        protected ICanvasEventHandler _handler;

        /// <summary>
        /// Initialize the ShapeControl UI
        /// </summary>
        public ShapeControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Get and set the associated canvas
        /// </summary>
        public ICanvas Canvas
        {
            get { return _canvas; }
            set { _canvas = value; }
        }

        /// <summary>
        /// Get and set the associated canvas event handler
        /// </summary>
        public virtual ICanvasEventHandler ShapeTool
        {
            get { return _handler; }
            set { _handler = value; }
        }

        /// <summary>
        /// Get and set the text of the button on the control
        /// </summary>
        public override string Text
        {
            get { return _button.Text; }
            set { _button.Text = value; }
        }

        /// <summary>
        /// A handler for click events from the button on the control
        /// </summary>
        /// <param name="sender">the object that generated the event</param>
        /// <param name="e">the event data</param>
        protected virtual void ButtonClick(object sender, EventArgs e)
        {
            if (_canvas == null) throw new Exception("A canvas was not set for a ShapeControl");
            if (_handler == null) throw new Exception("A canvas handler was not set for a ShapeControl");

            _canvas.ActiveTool = _handler;
        }
    }
}
