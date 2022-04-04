namespace ThreeDimentionalDemo
{
    partial class ThreeeDimentionalForm
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
            this.components = new System.ComponentModel.Container();
            this.XSpeedUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.YSpeedUpDown = new System.Windows.Forms.NumericUpDown();
            this.ZSpeedUpDown = new System.Windows.Forms.NumericUpDown();
            this.RenderTimer = new System.Windows.Forms.Timer(this.components);
            this.ImageScene = new System.Windows.Forms.PictureBox();
            this.CameraX = new System.Windows.Forms.NumericUpDown();
            this.CameraY = new System.Windows.Forms.NumericUpDown();
            this.CameraZ = new System.Windows.Forms.NumericUpDown();
            this.CameraRz = new System.Windows.Forms.NumericUpDown();
            this.CameraRy = new System.Windows.Forms.NumericUpDown();
            this.CameraRx = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.XSpeedUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YSpeedUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ZSpeedUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageScene)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraRz)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraRy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraRx)).BeginInit();
            this.SuspendLayout();
            // 
            // XSpeedUpDown
            // 
            this.XSpeedUpDown.Location = new System.Drawing.Point(668, 28);
            this.XSpeedUpDown.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.XSpeedUpDown.Name = "XSpeedUpDown";
            this.XSpeedUpDown.Size = new System.Drawing.Size(120, 20);
            this.XSpeedUpDown.TabIndex = 0;
            this.XSpeedUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(668, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Скорость";
            // 
            // YSpeedUpDown
            // 
            this.YSpeedUpDown.Location = new System.Drawing.Point(668, 54);
            this.YSpeedUpDown.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.YSpeedUpDown.Name = "YSpeedUpDown";
            this.YSpeedUpDown.Size = new System.Drawing.Size(120, 20);
            this.YSpeedUpDown.TabIndex = 3;
            this.YSpeedUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // ZSpeedUpDown
            // 
            this.ZSpeedUpDown.Location = new System.Drawing.Point(668, 80);
            this.ZSpeedUpDown.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.ZSpeedUpDown.Name = "ZSpeedUpDown";
            this.ZSpeedUpDown.Size = new System.Drawing.Size(120, 20);
            this.ZSpeedUpDown.TabIndex = 4;
            this.ZSpeedUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            -2147483648});
            // 
            // RenderTimer
            // 
            this.RenderTimer.Tick += new System.EventHandler(this.RenderTimer_Tick);
            // 
            // ImageScene
            // 
            this.ImageScene.Location = new System.Drawing.Point(12, 12);
            this.ImageScene.Name = "ImageScene";
            this.ImageScene.Size = new System.Drawing.Size(650, 426);
            this.ImageScene.TabIndex = 5;
            this.ImageScene.TabStop = false;
            // 
            // CameraX
            // 
            this.CameraX.Location = new System.Drawing.Point(668, 228);
            this.CameraX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.CameraX.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.CameraX.Name = "CameraX";
            this.CameraX.Size = new System.Drawing.Size(120, 20);
            this.CameraX.TabIndex = 6;
            this.CameraX.Value = new decimal(new int[] {
            3,
            0,
            0,
            -2147483648});
            this.CameraX.ValueChanged += new System.EventHandler(this.CameraX_ValueChanged);
            // 
            // CameraY
            // 
            this.CameraY.Location = new System.Drawing.Point(668, 254);
            this.CameraY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.CameraY.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.CameraY.Name = "CameraY";
            this.CameraY.Size = new System.Drawing.Size(120, 20);
            this.CameraY.TabIndex = 7;
            this.CameraY.Value = new decimal(new int[] {
            3,
            0,
            0,
            -2147483648});
            this.CameraY.ValueChanged += new System.EventHandler(this.CameraY_ValueChanged);
            // 
            // CameraZ
            // 
            this.CameraZ.Location = new System.Drawing.Point(668, 280);
            this.CameraZ.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.CameraZ.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.CameraZ.Name = "CameraZ";
            this.CameraZ.Size = new System.Drawing.Size(120, 20);
            this.CameraZ.TabIndex = 8;
            this.CameraZ.Value = new decimal(new int[] {
            3,
            0,
            0,
            -2147483648});
            this.CameraZ.ValueChanged += new System.EventHandler(this.CameraZ_ValueChanged);
            // 
            // CameraRz
            // 
            this.CameraRz.Location = new System.Drawing.Point(668, 378);
            this.CameraRz.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.CameraRz.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.CameraRz.Name = "CameraRz";
            this.CameraRz.Size = new System.Drawing.Size(120, 20);
            this.CameraRz.TabIndex = 11;
            this.CameraRz.Value = new decimal(new int[] {
            3,
            0,
            0,
            -2147483648});
            this.CameraRz.ValueChanged += new System.EventHandler(this.CameraRz_ValueChanged);
            // 
            // CameraRy
            // 
            this.CameraRy.Location = new System.Drawing.Point(668, 352);
            this.CameraRy.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.CameraRy.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.CameraRy.Name = "CameraRy";
            this.CameraRy.Size = new System.Drawing.Size(120, 20);
            this.CameraRy.TabIndex = 10;
            this.CameraRy.Value = new decimal(new int[] {
            3,
            0,
            0,
            -2147483648});
            this.CameraRy.ValueChanged += new System.EventHandler(this.CameraRy_ValueChanged);
            // 
            // CameraRx
            // 
            this.CameraRx.Location = new System.Drawing.Point(668, 326);
            this.CameraRx.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.CameraRx.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.CameraRx.Name = "CameraRx";
            this.CameraRx.Size = new System.Drawing.Size(120, 20);
            this.CameraRx.TabIndex = 9;
            this.CameraRx.Value = new decimal(new int[] {
            3,
            0,
            0,
            -2147483648});
            this.CameraRx.ValueChanged += new System.EventHandler(this.CameraRx_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(671, 209);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Позиция камеры";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(668, 307);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Поворот камеры";
            // 
            // ThreeeDimentionalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CameraRz);
            this.Controls.Add(this.CameraRy);
            this.Controls.Add(this.CameraRx);
            this.Controls.Add(this.CameraZ);
            this.Controls.Add(this.CameraY);
            this.Controls.Add(this.CameraX);
            this.Controls.Add(this.ImageScene);
            this.Controls.Add(this.ZSpeedUpDown);
            this.Controls.Add(this.YSpeedUpDown);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.XSpeedUpDown);
            this.Name = "ThreeeDimentionalForm";
            this.Text = "ThreeDimentionalForm";
            this.Load += new System.EventHandler(this.ThreeeDimentionalForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ThreeeDimentionalForm_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.XSpeedUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YSpeedUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ZSpeedUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageScene)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraRz)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraRy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraRx)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown XSpeedUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown YSpeedUpDown;
        private System.Windows.Forms.NumericUpDown ZSpeedUpDown;
        private System.Windows.Forms.Timer RenderTimer;
        private System.Windows.Forms.PictureBox ImageScene;
        private System.Windows.Forms.NumericUpDown CameraX;
        private System.Windows.Forms.NumericUpDown CameraY;
        private System.Windows.Forms.NumericUpDown CameraZ;
        private System.Windows.Forms.NumericUpDown CameraRz;
        private System.Windows.Forms.NumericUpDown CameraRy;
        private System.Windows.Forms.NumericUpDown CameraRx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

