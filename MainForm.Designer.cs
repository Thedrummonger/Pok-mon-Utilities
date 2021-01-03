namespace Pokemon_Utils
{
    partial class MainForm
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
            this.NUDDisplay = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.BTNPreview = new System.Windows.Forms.Button();
            this.BTNScan = new System.Windows.Forms.Button();
            this.LBResults = new System.Windows.Forms.ListBox();
            this.NUDTolerance = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NUDDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDTolerance)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Display With Game";
            // 
            // NUDDisplay
            // 
            this.NUDDisplay.Location = new System.Drawing.Point(142, 11);
            this.NUDDisplay.Name = "NUDDisplay";
            this.NUDDisplay.Size = new System.Drawing.Size(38, 20);
            this.NUDDisplay.TabIndex = 1;
            this.NUDDisplay.ValueChanged += new System.EventHandler(this.ChangeDisplay);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Your Screen Count: ";
            // 
            // BTNPreview
            // 
            this.BTNPreview.Location = new System.Drawing.Point(16, 48);
            this.BTNPreview.Name = "BTNPreview";
            this.BTNPreview.Size = new System.Drawing.Size(164, 23);
            this.BTNPreview.TabIndex = 3;
            this.BTNPreview.Text = "Preview Screen";
            this.BTNPreview.UseVisualStyleBackColor = true;
            this.BTNPreview.Click += new System.EventHandler(this.PreviewButtonClick);
            // 
            // BTNScan
            // 
            this.BTNScan.Location = new System.Drawing.Point(16, 77);
            this.BTNScan.Name = "BTNScan";
            this.BTNScan.Size = new System.Drawing.Size(164, 23);
            this.BTNScan.TabIndex = 4;
            this.BTNScan.Text = "Scan For Pokemon Data";
            this.BTNScan.UseVisualStyleBackColor = true;
            this.BTNScan.Click += new System.EventHandler(this.ScanButtonClick);
            // 
            // LBResults
            // 
            this.LBResults.FormattingEnabled = true;
            this.LBResults.Location = new System.Drawing.Point(16, 132);
            this.LBResults.Name = "LBResults";
            this.LBResults.Size = new System.Drawing.Size(164, 147);
            this.LBResults.Sorted = true;
            this.LBResults.TabIndex = 5;
            this.LBResults.DoubleClick += new System.EventHandler(this.ResultsListBox_DoubleClick);
            // 
            // NUDTolerance
            // 
            this.NUDTolerance.DecimalPlaces = 1;
            this.NUDTolerance.Location = new System.Drawing.Point(122, 106);
            this.NUDTolerance.Name = "NUDTolerance";
            this.NUDTolerance.Size = new System.Drawing.Size(58, 20);
            this.NUDTolerance.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Similarity Tolerance";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(193, 288);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.NUDTolerance);
            this.Controls.Add(this.LBResults);
            this.Controls.Add(this.BTNScan);
            this.Controls.Add(this.BTNPreview);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.NUDDisplay);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "PKM";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NUDDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDTolerance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown NUDDisplay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BTNPreview;
        private System.Windows.Forms.Button BTNScan;
        private System.Windows.Forms.ListBox LBResults;
        private System.Windows.Forms.NumericUpDown NUDTolerance;
        private System.Windows.Forms.Label label3;
    }
}

