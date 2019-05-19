using System;

namespace SudokuSolver
{
    partial class View
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.IntersectionsBtn = new System.Windows.Forms.Button();
            this.HiddenSinBtn = new System.Windows.Forms.Button();
            this.hintsChk = new System.Windows.Forms.CheckBox();
            this.BruteForceBtn = new System.Windows.Forms.Button();
            this.SingleBtn = new System.Windows.Forms.Button();
            this.LoadBtn = new System.Windows.Forms.Button();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.FillRandomBtn = new System.Windows.Forms.Button();
            this.ClearBtn = new System.Windows.Forms.Button();
            this.mainMatrix = new System.Windows.Forms.TableLayoutPanel();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 676);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(917, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.IntersectionsBtn);
            this.splitContainer1.Panel1.Controls.Add(this.HiddenSinBtn);
            this.splitContainer1.Panel1.Controls.Add(this.hintsChk);
            this.splitContainer1.Panel1.Controls.Add(this.BruteForceBtn);
            this.splitContainer1.Panel1.Controls.Add(this.SingleBtn);
            this.splitContainer1.Panel1.Controls.Add(this.LoadBtn);
            this.splitContainer1.Panel1.Controls.Add(this.SaveBtn);
            this.splitContainer1.Panel1.Controls.Add(this.FillRandomBtn);
            this.splitContainer1.Panel1.Controls.Add(this.ClearBtn);
            this.splitContainer1.Panel1MinSize = 200;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.mainMatrix);
            this.splitContainer1.Size = new System.Drawing.Size(917, 676);
            this.splitContainer1.SplitterDistance = 213;
            this.splitContainer1.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 565);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 26);
            this.label3.TabIndex = 9;
            this.label3.Text = "This computer-like strategy test\r\nall options at once.";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 338);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 26);
            this.label2.TabIndex = 7;
            this.label2.Text = "Human-like strategies do only\r\none board sweep each click.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 205);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 104);
            this.label1.TabIndex = 6;
            this.label1.Text = "Keyboard \r\nNumbers 1-9 enters number.\r\nNumber 0 clear the cell.\r\n\r\nMouse\r\nClick i" +
    "n the cell to select.\r\nClick on the hint to fill number.\r\n\r\n";
            // 
            // IntersectionsBtn
            // 
            this.IntersectionsBtn.Location = new System.Drawing.Point(12, 466);
            this.IntersectionsBtn.Name = "IntersectionsBtn";
            this.IntersectionsBtn.Size = new System.Drawing.Size(187, 23);
            this.IntersectionsBtn.TabIndex = 7;
            this.IntersectionsBtn.Text = "Eliminate Intersections - one pass";
            this.IntersectionsBtn.UseVisualStyleBackColor = true;
            this.IntersectionsBtn.Click += new System.EventHandler(this.IntersectionsBtn_Click);
            // 
            // HiddenSinBtn
            // 
            this.HiddenSinBtn.Location = new System.Drawing.Point(12, 420);
            this.HiddenSinBtn.Name = "HiddenSinBtn";
            this.HiddenSinBtn.Size = new System.Drawing.Size(187, 23);
            this.HiddenSinBtn.TabIndex = 6;
            this.HiddenSinBtn.Text = "Hidden singles - one pass";
            this.HiddenSinBtn.UseVisualStyleBackColor = true;
            this.HiddenSinBtn.Click += new System.EventHandler(this.HiddenSinBtn_Click);
            // 
            // hintsChk
            // 
            this.hintsChk.AutoSize = true;
            this.hintsChk.Location = new System.Drawing.Point(66, 166);
            this.hintsChk.Name = "hintsChk";
            this.hintsChk.Size = new System.Drawing.Size(78, 17);
            this.hintsChk.TabIndex = 4;
            this.hintsChk.Text = "Show hints";
            this.hintsChk.UseVisualStyleBackColor = true;
            this.hintsChk.CheckedChanged += new System.EventHandler(this.HintsChk_CheckedChanged);
            // 
            // BruteForceBtn
            // 
            this.BruteForceBtn.Location = new System.Drawing.Point(12, 611);
            this.BruteForceBtn.Name = "BruteForceBtn";
            this.BruteForceBtn.Size = new System.Drawing.Size(187, 23);
            this.BruteForceBtn.TabIndex = 8;
            this.BruteForceBtn.Text = "Brute force";
            this.BruteForceBtn.UseVisualStyleBackColor = true;
            this.BruteForceBtn.Click += new System.EventHandler(this.BruteForceBtn_Click);
            // 
            // SingleBtn
            // 
            this.SingleBtn.Location = new System.Drawing.Point(12, 377);
            this.SingleBtn.Name = "SingleBtn";
            this.SingleBtn.Size = new System.Drawing.Size(187, 23);
            this.SingleBtn.TabIndex = 5;
            this.SingleBtn.Text = "Singles - one pass";
            this.SingleBtn.UseVisualStyleBackColor = true;
            this.SingleBtn.Click += new System.EventHandler(this.SinglesBtn_Click);
            // 
            // LoadBtn
            // 
            this.LoadBtn.Location = new System.Drawing.Point(12, 126);
            this.LoadBtn.Name = "LoadBtn";
            this.LoadBtn.Size = new System.Drawing.Size(187, 23);
            this.LoadBtn.TabIndex = 3;
            this.LoadBtn.Text = "Load";
            this.LoadBtn.UseVisualStyleBackColor = true;
            this.LoadBtn.Click += new System.EventHandler(this.LoadBtn_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.Location = new System.Drawing.Point(12, 88);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(187, 23);
            this.SaveBtn.TabIndex = 2;
            this.SaveBtn.Text = "Save";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // FillRandomBtn
            // 
            this.FillRandomBtn.Location = new System.Drawing.Point(12, 50);
            this.FillRandomBtn.Name = "FillRandomBtn";
            this.FillRandomBtn.Size = new System.Drawing.Size(187, 23);
            this.FillRandomBtn.TabIndex = 1;
            this.FillRandomBtn.Text = "Fill 5 Cells randomly";
            this.FillRandomBtn.UseVisualStyleBackColor = true;
            this.FillRandomBtn.Click += new System.EventHandler(this.FillRandomBtn_Click);
            // 
            // ClearBtn
            // 
            this.ClearBtn.Location = new System.Drawing.Point(12, 12);
            this.ClearBtn.Name = "ClearBtn";
            this.ClearBtn.Size = new System.Drawing.Size(187, 23);
            this.ClearBtn.TabIndex = 0;
            this.ClearBtn.Text = "Clear";
            this.ClearBtn.UseVisualStyleBackColor = true;
            this.ClearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // mainMatrix
            // 
            this.mainMatrix.AutoSize = true;
            this.mainMatrix.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mainMatrix.ColumnCount = 9;
            this.mainMatrix.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.mainMatrix.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.mainMatrix.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.mainMatrix.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.mainMatrix.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.mainMatrix.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.mainMatrix.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.mainMatrix.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.mainMatrix.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.mainMatrix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainMatrix.Font = new System.Drawing.Font("Arial Black", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mainMatrix.Location = new System.Drawing.Point(0, 0);
            this.mainMatrix.Margin = new System.Windows.Forms.Padding(10);
            this.mainMatrix.Name = "mainMatrix";
            this.mainMatrix.Padding = new System.Windows.Forms.Padding(10);
            this.mainMatrix.RowCount = 9;
            this.mainMatrix.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.mainMatrix.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.mainMatrix.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.mainMatrix.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.mainMatrix.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.mainMatrix.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.mainMatrix.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.mainMatrix.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.mainMatrix.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.mainMatrix.Size = new System.Drawing.Size(700, 676);
            this.mainMatrix.TabIndex = 0;
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(118, 17);
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 698);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "View";
            this.Text = "Sudoku solver";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button LoadBtn;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.Button FillRandomBtn;
        private System.Windows.Forms.Button ClearBtn;
        private System.Windows.Forms.TableLayoutPanel mainMatrix;
        private System.Windows.Forms.Button SingleBtn;
        private System.Windows.Forms.Button BruteForceBtn;
        private System.Windows.Forms.CheckBox hintsChk;
        private System.Windows.Forms.Button HiddenSinBtn;
        private System.Windows.Forms.Button IntersectionsBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
    }
}

