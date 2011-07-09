using System;
using System.Drawing;
using System.Windows.Forms;

namespace SmallDraw
{
    /// <summary>
    /// 
    /// </summary>
    public class ShapeDraw : Form
    {
        /// <summary>
        /// 
        /// </summary>
        protected FlowLayoutPanel _toolPanel = new FlowLayoutPanel();

        /// <summary>
        /// 
        /// </summary>
        protected Basic.BasicCanvas _canvas = new Basic.BasicCanvas();

        public ShapeDraw()
            : base()
        {
            this.Text = "Shapes";
            this.ClientSize = new Size(800, 600);
            //_toolPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom);
            _toolPanel.Location = new Point(0, 0);
            _toolPanel.Size = new Size(200, 600);
            _toolPanel.FlowDirection = FlowDirection.TopDown;
            CreateToolPanel();

            //_canvas.Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom);
            _canvas.Location = new Point(200, 0);
            _canvas.Size = new Size(600, 600);

            this.Controls.Add(_toolPanel);
            this.Controls.Add(_canvas);
        }

        private void CreateToolPanel()
        {
            var label = new Label();
            label.Text = "Tools";
            _canvas.Controls.Add(label);
            var selectionTool = new Basic.SelectionTool(_canvas);
            _canvas.ActiveTool = selectionTool;
            AddTool("Select", selectionTool);
            AddTool("Line", new Figure.Line.LineTool(_canvas));
            AddTool("Rectangle", new Figure.Rectangle.RectangleTool(_canvas));
            AddTool("Filled Rectangle", new Figure.Rectangle.FilledRectangleTool(_canvas));
            AddTool("Triangle", new Figure.Polygon.PolygonTool(_canvas, 3));
            AddTool("Pentagon", new Figure.Polygon.PolygonTool(_canvas, 5));
            AddTool(new Figure.Polygon.NgonToolWidget(_canvas));
        }

        private void AddTool(Control control)
        {
            _toolPanel.Controls.Add(control);
        }

        private void AddTool(string name, ICanvasEventHandler handler)
        {
            this.AddTool(new Basic.ToolWidget(name, _canvas, handler));
        }


        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.Run(new ShapeDraw());
        }
    }
}
