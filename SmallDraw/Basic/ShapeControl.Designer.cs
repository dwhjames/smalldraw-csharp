namespace SmallDraw.Basic
{
    partial class ShapeControl
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
            this._button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _button
            // 
            this._button.Location = new System.Drawing.Point(10, 10);
            this._button.Margin = new System.Windows.Forms.Padding(10);
            this._button.Name = "_button";
            this._button.Size = new System.Drawing.Size(100, 30);
            this._button.TabIndex = 0;
            this._button.UseVisualStyleBackColor = true;
            this._button.Click += new System.EventHandler(this.ButtonClick);
            // 
            // ShapeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._button);
            this.Name = "ShapeControl";
            this.Size = new System.Drawing.Size(120, 50);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _button;
    }
}
