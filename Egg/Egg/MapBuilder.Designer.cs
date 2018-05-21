namespace Egg
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
            this.txtFile = new System.Windows.Forms.TextBox();
            this.rad5 = new System.Windows.Forms.RadioButton();
            this.rad4 = new System.Windows.Forms.RadioButton();
            this.button151 = new System.Windows.Forms.Button();
            this.rad3 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.tabletWidth = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabletHeight = new System.Windows.Forms.TextBox();
            this.rad2 = new System.Windows.Forms.RadioButton();
            this.rad1 = new System.Windows.Forms.RadioButton();
            this.chkDelete = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(53, 679);
            this.txtFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(207, 22);
            this.txtFile.TabIndex = 31;
            this.txtFile.Text = "<text file name>";
            // 
            // rad5
            // 
            this.rad5.AutoSize = true;
            this.rad5.Checked = true;
            this.rad5.Location = new System.Drawing.Point(51, 438);
            this.rad5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rad5.Name = "rad5";
            this.rad5.Size = new System.Drawing.Size(74, 21);
            this.rad5.TabIndex = 30;
            this.rad5.TabStop = true;
            this.rad5.Text = "Normal";
            this.rad5.UseVisualStyleBackColor = true;
            // 
            // rad4
            // 
            this.rad4.AutoSize = true;
            this.rad4.Location = new System.Drawing.Point(51, 550);
            this.rad4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rad4.Name = "rad4";
            this.rad4.Size = new System.Drawing.Size(74, 21);
            this.rad4.TabIndex = 28;
            this.rad4.Text = "Moving";
            this.rad4.UseVisualStyleBackColor = true;
            // 
            // button151
            // 
            this.button151.Location = new System.Drawing.Point(51, 730);
            this.button151.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button151.Name = "button151";
            this.button151.Size = new System.Drawing.Size(217, 79);
            this.button151.TabIndex = 27;
            this.button151.Text = "Export to Text";
            this.button151.UseVisualStyleBackColor = true;
            this.button151.Click += new System.EventHandler(this.Export);
            // 
            // rad3
            // 
            this.rad3.AutoSize = true;
            this.rad3.Location = new System.Drawing.Point(51, 524);
            this.rad3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rad3.Name = "rad3";
            this.rad3.Size = new System.Drawing.Size(112, 21);
            this.rad3.TabIndex = 25;
            this.rad3.Text = "Non-Collision";
            this.rad3.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(169, 609);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 17);
            this.label3.TabIndex = 24;
            this.label3.Text = "Tablet Width:";
            // 
            // tabletWidth
            // 
            this.tabletWidth.Location = new System.Drawing.Point(173, 636);
            this.tabletWidth.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabletWidth.Name = "tabletWidth";
            this.tabletWidth.Size = new System.Drawing.Size(87, 22);
            this.tabletWidth.TabIndex = 23;
            this.tabletWidth.Text = "16";
            this.tabletWidth.TextChanged += new System.EventHandler(this.HeightWidthChange);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 609);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 17);
            this.label2.TabIndex = 22;
            this.label2.Text = "Tablet Height:";
            // 
            // tabletHeight
            // 
            this.tabletHeight.Location = new System.Drawing.Point(53, 636);
            this.tabletHeight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabletHeight.Name = "tabletHeight";
            this.tabletHeight.Size = new System.Drawing.Size(84, 22);
            this.tabletHeight.TabIndex = 21;
            this.tabletHeight.Text = "9";
            this.tabletHeight.TextChanged += new System.EventHandler(this.HeightWidthChange);
            // 
            // rad2
            // 
            this.rad2.AutoSize = true;
            this.rad2.Location = new System.Drawing.Point(51, 495);
            this.rad2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rad2.Name = "rad2";
            this.rad2.Size = new System.Drawing.Size(124, 21);
            this.rad2.TabIndex = 20;
            this.rad2.Text = "Non-Damaging";
            this.rad2.UseVisualStyleBackColor = true;
            // 
            // rad1
            // 
            this.rad1.AutoSize = true;
            this.rad1.Location = new System.Drawing.Point(51, 466);
            this.rad1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rad1.Name = "rad1";
            this.rad1.Size = new System.Drawing.Size(93, 21);
            this.rad1.TabIndex = 19;
            this.rad1.Text = "Damaging";
            this.rad1.UseVisualStyleBackColor = true;
            // 
            // chkDelete
            // 
            this.chkDelete.AutoSize = true;
            this.chkDelete.Location = new System.Drawing.Point(189, 439);
            this.chkDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkDelete.Name = "chkDelete";
            this.chkDelete.Size = new System.Drawing.Size(71, 21);
            this.chkDelete.TabIndex = 32;
            this.chkDelete.Text = "Delete";
            this.chkDelete.UseVisualStyleBackColor = true;
            this.chkDelete.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // Mappy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1581, 873);
            this.Controls.Add(this.chkDelete);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.rad5);
            this.Controls.Add(this.rad4);
            this.Controls.Add(this.button151);
            this.Controls.Add(this.rad3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tabletWidth);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tabletHeight);
            this.Controls.Add(this.rad2);
            this.Controls.Add(this.rad1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Mappy";
            this.Text = "MapBuilder";
            this.Load += new System.EventHandler(this.MapBuilder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.RadioButton rad5;
        private System.Windows.Forms.RadioButton rad4;
        private System.Windows.Forms.Button button151;
        private System.Windows.Forms.RadioButton rad3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tabletWidth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tabletHeight;
        private System.Windows.Forms.RadioButton rad2;
        private System.Windows.Forms.RadioButton rad1;
        private System.Windows.Forms.CheckBox chkDelete;
    }
}