namespace SmallDraw.Figure.Polygon
{
    partial class NgonControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._comboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // _comboBox
            // 
            this._comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBox.FormattingEnabled = true;
            this._comboBox.Location = new System.Drawing.Point(123, 16);
            this._comboBox.Margin = new System.Windows.Forms.Padding(10);
            this._comboBox.Name = "_comboBox";
            this._comboBox.Size = new System.Drawing.Size(60, 21);
            this._comboBox.TabIndex = 1;
            this._comboBox.SelectedIndexChanged += new System.EventHandler(this._comboBox_SelectionChanged);
            // 
            // NgonControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._comboBox);
            this.Name = "NgonControl";
            this.Size = new System.Drawing.Size(200, 50);
            this.Controls.SetChildIndex(this._comboBox, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox _comboBox;
    }
}
