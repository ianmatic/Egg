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
            this.chkDeleter = new System.Windows.Forms.CheckBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.button151 = new System.Windows.Forms.Button();
            this.tileView = new System.Windows.Forms.PictureBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.tabletWidth = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabletHeight = new System.Windows.Forms.TextBox();
            this.radioDam2 = new System.Windows.Forms.RadioButton();
            this.radioDam1 = new System.Windows.Forms.RadioButton();
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
            this.tabHolder.Location = new System.Drawing.Point(3, 1);
            this.tabHolder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabHolder.Name = "tabHolder";
            this.tabHolder.SelectedIndex = 0;
            this.tabHolder.Size = new System.Drawing.Size(1435, 784);
            this.tabHolder.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chkDeleter);
            this.tabPage1.Controls.Add(this.radioButton2);
            this.tabPage1.Controls.Add(this.button151);
            this.tabPage1.Controls.Add(this.tileView);
            this.tabPage1.Controls.Add(this.radioButton1);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.tabletWidth);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.tabletHeight);
            this.tabPage1.Controls.Add(this.radioDam2);
            this.tabPage1.Controls.Add(this.radioDam1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.boxSelect);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Size = new System.Drawing.Size(1427, 755);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // chkDeleter
            // 
            this.chkDeleter.AutoSize = true;
            this.chkDeleter.Location = new System.Drawing.Point(47, 406);
            this.chkDeleter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkDeleter.Name = "chkDeleter";
            this.chkDeleter.Size = new System.Drawing.Size(71, 21);
            this.chkDeleter.TabIndex = 14;
            this.chkDeleter.Text = "Delete";
            this.chkDeleter.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(47, 356);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(74, 21);
            this.radioButton2.TabIndex = 13;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Moving";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // button151
            // 
            this.button151.Location = new System.Drawing.Point(47, 572);
            this.button151.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button151.Name = "button151";
            this.button151.Size = new System.Drawing.Size(217, 79);
            this.button151.TabIndex = 12;
            this.button151.Text = "Export to Text";
            this.button151.UseVisualStyleBackColor = true;
            this.button151.Click += new System.EventHandler(this.Export);
            // 
            // tileView
            // 
            this.tileView.Location = new System.Drawing.Point(117, 178);
            this.tileView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tileView.Name = "tileView";
            this.tileView.Size = new System.Drawing.Size(67, 68);
            this.tileView.TabIndex = 11;
            this.tileView.TabStop = false;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(47, 330);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(112, 21);
            this.radioButton1.TabIndex = 10;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Non-Collision";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(179, 453);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Tablet Width:";
            // 
            // tabletWidth
            // 
            this.tabletWidth.Location = new System.Drawing.Point(183, 476);
            this.tabletWidth.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabletWidth.Name = "tabletWidth";
            this.tabletWidth.Size = new System.Drawing.Size(87, 22);
            this.tabletWidth.TabIndex = 7;
            this.tabletWidth.Text = "15";
            this.tabletWidth.TextChanged += new System.EventHandler(this.HeightWidthChange);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 453);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Tablet Height:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // tabletHeight
            // 
            this.tabletHeight.Location = new System.Drawing.Point(65, 476);
            this.tabletHeight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabletHeight.Name = "tabletHeight";
            this.tabletHeight.Size = new System.Drawing.Size(84, 22);
            this.tabletHeight.TabIndex = 5;
            this.tabletHeight.Text = "10";
            this.tabletHeight.TextChanged += new System.EventHandler(this.HeightWidthChange);
            // 
            // radioDam2
            // 
            this.radioDam2.AutoSize = true;
            this.radioDam2.Location = new System.Drawing.Point(47, 302);
            this.radioDam2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioDam2.Name = "radioDam2";
            this.radioDam2.Size = new System.Drawing.Size(124, 21);
            this.radioDam2.TabIndex = 4;
            this.radioDam2.TabStop = true;
            this.radioDam2.Text = "Non-Damaging";
            this.radioDam2.UseVisualStyleBackColor = true;
            // 
            // radioDam1
            // 
            this.radioDam1.AutoSize = true;
            this.radioDam1.Location = new System.Drawing.Point(47, 273);
            this.radioDam1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioDam1.Name = "radioDam1";
            this.radioDam1.Size = new System.Drawing.Size(93, 21);
            this.radioDam1.TabIndex = 3;
            this.radioDam1.TabStop = true;
            this.radioDam1.Text = "Damaging";
            this.radioDam1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 105);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 17);
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
            "dTopLeft",
            "dTopMid",
            "dTopRight",
            "dMidLeft",
            "dSolid",
            "dMidRight",
            "dBotLeft",
            "dBotMid",
            "dBotRight",
            "Delete"});
            this.boxSelect.Location = new System.Drawing.Point(47, 124);
            this.boxSelect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.boxSelect.Name = "boxSelect";
            this.boxSelect.Size = new System.Drawing.Size(216, 24);
            this.boxSelect.TabIndex = 1;
            this.boxSelect.Text = "Choose Tile:";
            this.boxSelect.SelectedIndexChanged += new System.EventHandler(this.BoxIndexChanged);
            this.boxSelect.SelectedValueChanged += new System.EventHandler(this.BoxIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Size = new System.Drawing.Size(1427, 755);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Size = new System.Drawing.Size(1427, 755);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // Mappy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1439, 786);
            this.Controls.Add(this.tabHolder);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
        private System.Windows.Forms.RadioButton radioDam2;
        private System.Windows.Forms.RadioButton radioDam1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox boxSelect;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tabletWidth;
        private System.Windows.Forms.PictureBox tileView;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button button151;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.CheckBox chkDeleter;
    }
}