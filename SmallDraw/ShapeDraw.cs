using System;
using System.Drawing;
using System.Windows.Forms;

namespace SmallDraw
{
    public partial class ShapeDraw : Form
    {
        public ShapeDraw()
        {
            InitializeComponent();

            _selectTool.Text = "Select";
            _selectTool.Canvas = _canvas;
            var tool = new Basic.SelectionTool(_canvas);
            _selectTool.ShapeTool = tool;
            _canvas.ActiveTool = tool;
            _selectTool.Focus();
            
            _lineTool.Text = "Line";
            _lineTool.Canvas = _canvas;
            _lineTool.ShapeTool = new Figure.Line.LineTool(_canvas);

            _rectangleTool.Text = "Rectangle";
            _rectangleTool.Canvas = _canvas;
            _rectangleTool.ShapeTool = new Figure.Rectangle.RectangleTool(_canvas);

            _filledRectangleTool.Text = "Filled Rectangle";
            _filledRectangleTool.Canvas = _canvas;
            _filledRectangleTool.ShapeTool = new Figure.Rectangle.FilledRectangleTool(_canvas);

            _triangleTool.Text = "Triangle";
            _triangleTool.Canvas = _canvas;
            _triangleTool.ShapeTool = new Figure.Polygon.PolygonTool(_canvas, 3);

            _pentagonTool.Text = "Pentagon";
            _pentagonTool.Canvas = _canvas;
            _pentagonTool.ShapeTool = new Figure.Polygon.PolygonTool(_canvas, 5);

            _ngonTool.Text = "N-gon";
            _ngonTool.Canvas = _canvas;
            _ngonTool.ShapeTool = new Figure.Polygon.PolygonTool(_canvas, 3);
            _ngonTool.SidesList = new object[] { 3, 4, 5, 6, 7, 8, 9 };
        }
    }
}
