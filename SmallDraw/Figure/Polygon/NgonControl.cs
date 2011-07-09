using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmallDraw.Figure.Polygon
{
    public partial class NgonControl : Basic.ShapeControl
    {
        public NgonControl()
        {
            InitializeComponent();
        }

        public object[] SidesList
        {
            set
            {
                _comboBox.Items.Clear();
                _comboBox.Items.AddRange(value);
            }
        }

        private void _comboBox_SelectionChanged(object sender, EventArgs e)
        {
            var tool = this.ShapeTool as PolygonTool;
            if (tool == null)
            {
                throw new Exception("NgonControl was not bound to a PolygonTool");
            }
            else
            {
                tool.NumPoints = (int)_comboBox.SelectedItem;
            }
        }
    }
}
