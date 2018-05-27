namespace MapCombiner
{
    partial class EditTileCount
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.editX = new System.Windows.Forms.TextBox();
            this.editY = new System.Windows.Forms.TextBox();
            this.buttonApply = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "X:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Y:";
            // 
            // editX
            // 
            this.editX.Location = new System.Drawing.Point(36, 10);
            this.editX.Name = "editX";
            this.editX.Size = new System.Drawing.Size(100, 20);
            this.editX.TabIndex = 1;
            this.editX.TextChanged += new System.EventHandler(this.editX_TextChanged);
            // 
            // editY
            // 
            this.editY.Location = new System.Drawing.Point(36, 34);
            this.editY.Name = "editY";
            this.editY.Size = new System.Drawing.Size(100, 20);
            this.editY.TabIndex = 2;
            this.editY.TextChanged += new System.EventHandler(this.editY_TextChanged);
            // 
            // buttonApply
            // 
            this.buttonApply.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonApply.Location = new System.Drawing.Point(46, 61);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 3;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            // 
            // EditTileCount
            // 
            this.AcceptButton = this.buttonApply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(157, 96);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.editY);
            this.Controls.Add(this.editX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EditTileCount";
            this.Text = "Tile Count";
            this.Load += new System.EventHandler(this.EditTileCount_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox editX;
        private System.Windows.Forms.TextBox editY;
        private System.Windows.Forms.Button buttonApply;
    }
}