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
    /// <summary>
    /// An extension of the ShapeControl user control
    /// </summary>
    public partial class NgonControl : Basic.ShapeControl
    {
        /// <summary>
        /// Initialize an ngon control
        /// </summary>
        public NgonControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set the list of polygon sizes
        /// </summary>
        public object[] SidesList
        {
            set
            {
                _comboBox.Items.Clear();
                _comboBox.Items.AddRange(value);
            }
        }

        /// <summary>
        /// A handler for selection change events from the combo box
        /// </summary>
        /// <param name="sender">the object that generated the event</param>
        /// <param name="e">the event data</param>
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
