namespace Egg_DevTool_Test
{
    partial class Mappy
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
            this.tabHolder = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.rad5 = new System.Windows.Forms.RadioButton();
            this.chkDeleter = new System.Windows.Forms.CheckBox();
            this.rad4 = new System.Windows.Forms.RadioButton();
            this.button151 = new System.Windows.Forms.Button();
            this.tileView = new System.Windows.Forms.PictureBox();
            this.rad3 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.tabletWidth = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabletHeight = new System.Windows.Forms.TextBox();
            this.rad2 = new System.Windows.Forms.RadioButton();
            this.rad1 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.boxSelect = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabHolder.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tileView)).BeginInit();
            this.SuspendLayout();
            // 
            // tabHolder
            // 
            this.tabHolder.Controls.Add(this.tabPage1);
            this.tabHolder.Controls.Add(this.tabPage2);
            this.tabHolder.Controls.Add(this.tabPage3);
            this.tabHolder.Location = new System.Drawing.Point(2, 1);
            this.tabHolder.Margin = new System.Windows.Forms.Padding(2);
            this.tabHolder.Name = "tabHolder";
            this.tabHolder.SelectedIndex = 0;
            this.tabHolder.Size = new System.Drawing.Size(1076, 637);
            this.tabHolder.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtFile);
            this.tabPage1.Controls.Add(this.rad5);
            this.tabPage1.Controls.Add(this.chkDeleter);
            this.tabPage1.Controls.Add(this.rad4);
            this.tabPage1.Controls.Add(this.button151);
            this.tabPage1.Controls.Add(this.tileView);
            this.tabPage1.Controls.Add(this.rad3);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.tabletWidth);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.tabletHeight);
            this.tabPage1.Controls.Add(this.rad2);
            this.tabPage1.Controls.Add(this.rad1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.boxSelect);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(1068, 611);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(37, 423);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(156, 20);
            this.txtFile.TabIndex = 16;
            this.txtFile.Text = "<text file name>";
            // 
            // rad5
            // 
            this.rad5.AutoSize = true;
            this.rad5.Checked = true;
            this.rad5.Location = new System.Drawing.Point(35, 227);
            this.rad5.Name = "rad5";
            this.rad5.Size = new System.Drawing.Size(58, 17);
            this.rad5.TabIndex = 15;
            this.rad5.TabStop = true;
            this.rad5.Text = "Normal";
            this.rad5.UseVisualStyleBackColor = true;
            // 
            // chkDeleter
            // 
            this.chkDeleter.AutoSize = true;
            this.chkDeleter.Location = new System.Drawing.Point(144, 57);
            this.chkDeleter.Name = "chkDeleter";
            this.chkDeleter.Size = new System.Drawing.Size(57, 17);
            this.chkDeleter.TabIndex = 14;
            this.chkDeleter.Text = "Delete";
            this.chkDeleter.UseVisualStyleBackColor = true;
            // 
            // rad4
            // 
            this.rad4.AutoSize = true;
            this.rad4.Location = new System.Drawing.Point(35, 318);
            this.rad4.Name = "rad4";
            this.rad4.Size = new System.Drawing.Size(60, 17);
            this.rad4.TabIndex = 13;
            this.rad4.Text = "Moving";
            this.rad4.UseVisualStyleBackColor = true;
            // 
            // button151
            // 
            this.button151.Location = new System.Drawing.Point(35, 465);
            this.button151.Name = "button151";
            this.button151.Size = new System.Drawing.Size(163, 64);
            this.button151.TabIndex = 12;
            this.button151.Text = "Export to Text";
            this.button151.UseVisualStyleBackColor = true;
            this.button151.Click += new System.EventHandler(this.Export);
            // 
            // tileView
            // 
            this.tileView.Location = new System.Drawing.Point(88, 145);
            this.tileView.Name = "tileView";
            this.tileView.Size = new System.Drawing.Size(50, 55);
            this.tileView.TabIndex = 11;
            this.tileView.TabStop = false;
            // 
            // rad3
            // 
            this.rad3.AutoSize = true;
            this.rad3.Location = new System.Drawing.Point(35, 297);
            this.rad3.Name = "rad3";
            this.rad3.Size = new System.Drawing.Size(86, 17);
            this.rad3.TabIndex = 10;
            this.rad3.Text = "Non-Collision";
            this.rad3.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(124, 366);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Tablet Width:";
            // 
            // tabletWidth
            // 
            this.tabletWidth.Location = new System.Drawing.Point(127, 388);
            this.tabletWidth.Name = "tabletWidth";
            this.tabletWidth.Size = new System.Drawing.Size(66, 20);
            this.tabletWidth.TabIndex = 7;
            this.tabletWidth.Text = "15";
            this.tabletWidth.TextChanged += new System.EventHandler(this.HeightWidthChange);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 366);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Tablet Height:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // tabletHeight
            // 
            this.tabletHeight.Location = new System.Drawing.Point(37, 388);
            this.tabletHeight.Name = "tabletHeight";
            this.tabletHeight.Size = new System.Drawing.Size(64, 20);
            this.tabletHeight.TabIndex = 5;
            this.tabletHeight.Text = "10";
            this.tabletHeight.TextChanged += new System.EventHandler(this.HeightWidthChange);
            // 
            // rad2
            // 
            this.rad2.AutoSize = true;
            this.rad2.Location = new System.Drawing.Point(35, 274);
            this.rad2.Name = "rad2";
            this.rad2.Size = new System.Drawing.Size(96, 17);
            this.rad2.TabIndex = 4;
            this.rad2.Text = "Non-Damaging";
            this.rad2.UseVisualStyleBackColor = true;
            // 
            // rad1
            // 
            this.rad1.AutoSize = true;
            this.rad1.Location = new System.Drawing.Point(35, 250);
            this.rad1.Name = "rad1";
            this.rad1.Size = new System.Drawing.Size(73, 17);
            this.rad1.TabIndex = 3;
            this.rad1.Text = "Damaging";
            this.rad1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tile:";
            // 
            // boxSelect
            // 
            this.boxSelect.FormattingEnabled = true;
            this.boxSelect.Items.AddRange(new object[] {
            "LTopLeft",
            "LTopMid",
            "LTopRight",
            "LMidLeft",
            "LMidRight",
            "LBotLeft",
            "LBotMid",
            "LBotRight",
            "",
            "dTopLeft",
            "dTopMid",
            "dTopRight",
            "dMidLeft",
            "dSolid",
            "",
            "nLeftTop",
            "nRightTop",
            "nLeftBot",
            "nRightBot",
            "dMidRight",
            "dBotLeft",
            "dBotMid",
            "dBotRight",
            "Delete"});
            this.boxSelect.Location = new System.Drawing.Point(35, 101);
            this.boxSelect.Name = "boxSelect";
            this.boxSelect.Size = new System.Drawing.Size(163, 21);
            this.boxSelect.TabIndex = 1;
            this.boxSelect.Text = "Choose Tile:";
            this.boxSelect.SelectedIndexChanged += new System.EventHandler(this.BoxIndexChanged);
            this.boxSelect.SelectedValueChanged += new System.EventHandler(this.BoxIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(1068, 611);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage3.Size = new System.Drawing.Size(1068, 611);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // Mappy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 639);
            this.Controls.Add(this.tabHolder);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Mappy";
            this.Text = "MapBuilder";
            this.Load += new System.EventHandler(this.MapBuilder_Load);
            this.tabHolder.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tileView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabHolder;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tabletHeight;
        private System.Windows.Forms.RadioButton rad2;
        private System.Windows.Forms.RadioButton rad1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox boxSelect;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tabletWidth;
        private System.Windows.Forms.PictureBox tileView;
        private System.Windows.Forms.RadioButton rad3;
        private System.Windows.Forms.Button button151;
        private System.Windows.Forms.RadioButton rad4;
        private System.Windows.Forms.CheckBox chkDeleter;
        private System.Windows.Forms.RadioButton rad5;
        private System.Windows.Forms.TextBox txtFile;
    }
}