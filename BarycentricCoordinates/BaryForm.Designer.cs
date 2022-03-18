namespace BarycentricCoordinates
{
    partial class BaryForm
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
            this.OutputLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // OutputLabel
            // 
            this.OutputLabel.AutoSize = true;
            this.OutputLabel.Location = new System.Drawing.Point(753, 9);
            this.OutputLabel.Name = "OutputLabel";
            this.OutputLabel.Size = new System.Drawing.Size(35, 13);
            this.OutputLabel.TabIndex = 0;
            this.OutputLabel.Text = "label1";
            // 
            // BaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.OutputLabel);
            this.Name = "BaryForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.BaryForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.BaryForm_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BaryForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BaryForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BaryForm_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label OutputLabel;
    }
}

