namespace SmallDraw
{
    partial class ShapeDraw
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._groupBox = new System.Windows.Forms.GroupBox();
            this._canvas = new SmallDraw.Basic.BasicCanvas();
            this._ngonTool = new SmallDraw.Figure.Polygon.NgonControl();
            this._pentagonTool = new SmallDraw.Basic.ShapeControl();
            this._triangleTool = new SmallDraw.Basic.ShapeControl();
            this._rectangleTool = new SmallDraw.Basic.ShapeControl();
            this._filledRectangleTool = new SmallDraw.Basic.ShapeControl();
            this._lineTool = new SmallDraw.Basic.ShapeControl();
            this._selectTool = new SmallDraw.Basic.ShapeControl();
            this._groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _groupBox
            // 
            this._groupBox.Controls.Add(this._ngonTool);
            this._groupBox.Controls.Add(this._pentagonTool);
            this._groupBox.Controls.Add(this._triangleTool);
            this._groupBox.Controls.Add(this._rectangleTool);
            this._groupBox.Controls.Add(this._filledRectangleTool);
            this._groupBox.Controls.Add(this._lineTool);
            this._groupBox.Controls.Add(this._selectTool);
            this._groupBox.Location = new System.Drawing.Point(12, 12);
            this._groupBox.Name = "_groupBox";
            this._groupBox.Size = new System.Drawing.Size(213, 549);
            this._groupBox.TabIndex = 0;
            this._groupBox.TabStop = false;
            this._groupBox.Text = "Tools";
            // 
            // _canvas
            // 
            this._canvas.Location = new System.Drawing.Point(231, 12);
            this._canvas.Name = "_canvas";
            this._canvas.Size = new System.Drawing.Size(549, 549);
            this._canvas.TabIndex = 1;
            // 
            // _ngonTool
            // 
            this._ngonTool.Canvas = null;
            this._ngonTool.Location = new System.Drawing.Point(6, 355);
            this._ngonTool.Name = "_ngonTool";
            this._ngonTool.ShapeTool = null;
            this._ngonTool.Size = new System.Drawing.Size(200, 50);
            this._ngonTool.TabIndex = 2;
            // 
            // _pentagonTool
            // 
            this._pentagonTool.Canvas = null;
            this._pentagonTool.Location = new System.Drawing.Point(46, 299);
            this._pentagonTool.Name = "_pentagonTool";
            this._pentagonTool.ShapeTool = null;
            this._pentagonTool.Size = new System.Drawing.Size(120, 50);
            this._pentagonTool.TabIndex = 1;
            // 
            // _triangleTool
            // 
            this._triangleTool.Canvas = null;
            this._triangleTool.Location = new System.Drawing.Point(46, 243);
            this._triangleTool.Name = "_triangleTool";
            this._triangleTool.ShapeTool = null;
            this._triangleTool.Size = new System.Drawing.Size(120, 50);
            this._triangleTool.TabIndex = 2;
            // 
            // _rectangleTool
            // 
            this._rectangleTool.Canvas = null;
            this._rectangleTool.Location = new System.Drawing.Point(46, 131);
            this._rectangleTool.Name = "_rectangleTool";
            this._rectangleTool.ShapeTool = null;
            this._rectangleTool.Size = new System.Drawing.Size(120, 50);
            this._rectangleTool.TabIndex = 1;
            // 
            // _filledRectangleTool
            // 
            this._filledRectangleTool.Canvas = null;
            this._filledRectangleTool.Location = new System.Drawing.Point(46, 187);
            this._filledRectangleTool.Name = "_filledRectangleTool";
            this._filledRectangleTool.ShapeTool = null;
            this._filledRectangleTool.Size = new System.Drawing.Size(120, 50);
            this._filledRectangleTool.TabIndex = 1;
            // 
            // _lineTool
            // 
            this._lineTool.Canvas = null;
            this._lineTool.Location = new System.Drawing.Point(46, 75);
            this._lineTool.Name = "_lineTool";
            this._lineTool.ShapeTool = null;
            this._lineTool.Size = new System.Drawing.Size(120, 50);
            this._lineTool.TabIndex = 1;
            // 
            // _selectTool
            // 
            this._selectTool.Canvas = null;
            this._selectTool.Location = new System.Drawing.Point(46, 19);
            this._selectTool.Name = "_selectTool";
            this._selectTool.ShapeTool = null;
            this._selectTool.Size = new System.Drawing.Size(120, 50);
            this._selectTool.TabIndex = 1;
            // 
            // ShapeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this._canvas);
            this.Controls.Add(this._groupBox);
            this.Name = "ShapeForm";
            this.Text = "ShapeForm";
            this._groupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox _groupBox;
        protected Basic.ShapeControl _lineTool;
        protected Basic.ShapeControl _selectTool;
        protected Basic.ShapeControl _pentagonTool;
        protected Basic.ShapeControl _triangleTool;
        protected Basic.ShapeControl _rectangleTool;
        protected Basic.ShapeControl _filledRectangleTool;
        protected Basic.BasicCanvas _canvas;
        protected Figure.Polygon.NgonControl _ngonTool;
    }
}