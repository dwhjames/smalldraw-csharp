using System;
using System.Drawing;
using System.Windows.Forms;

namespace SmallDraw.Figure.Polygon
{
    /// <summary>
    /// 
    /// </summary>
    public class NgonToolWidget : Basic.ToolWidget
    {
        /// <summary>
        /// 
        /// </summary>
        protected ComboBox _comboBox;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        public NgonToolWidget(ICanvas canvas)
            : base("N-gon", canvas, new PolygonTool(canvas, 3))
        {
            _comboBox = new ComboBox();
            for (int i = 3; i < 9; i++)
            {
                _comboBox.Items.Add(i);
            }
            _comboBox.SelectedValueChanged += new EventHandler(SelectNgonSize);

            this.Controls.Add(_comboBox);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SelectNgonSize(object sender, EventArgs e)
        {
            ((PolygonTool)_handler).NumPoints = (int)_comboBox.SelectedValue;
        }
    }
}
