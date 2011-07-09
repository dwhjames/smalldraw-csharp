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
    public partial class ShapeControl : UserControl
    {
        protected ICanvas _canvas;

        protected ICanvasEventHandler _handler;

        public ShapeControl()
        {
            InitializeComponent();
        }

        public ICanvas Canvas
        {
            get { return _canvas; }
            set { _canvas = value; }
        }

        public virtual ICanvasEventHandler ShapeTool
        {
            get { return _handler; }
            set { _handler = value; }
        }

        public override string Text
        {
            get { return _button.Text; }
            set { _button.Text = value; }
        }

        private void _button_Click(object sender, EventArgs e)
        {
            if (_canvas == null) throw new Exception("A canvas was not set for a ShapeControl");
            if (_handler == null) throw new Exception("A canvas handler was not set for a ShapeControl");

            _canvas.ActiveTool = _handler;
        }
    }
}
