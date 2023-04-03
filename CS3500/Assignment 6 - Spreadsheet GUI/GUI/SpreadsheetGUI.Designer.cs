/// <summary>
/// Author: Drew Lawson and Peter Bruns
/// Team: EatSleepCode
/// Date: March 3, 2022
/// Course: CS 3500, University of Utah, School of Computing
/// Copyright CS 3500, Drew Lawson, and Peter Bruns - This work may not be copied for use in Academic Coursework
/// 
/// We, Drew Lawson and Peter Bruns, certify that I wrote this code from scratch and did not copy it in part or whole from
/// another source. All references used in the completion of the assignment are cited in my README file.
/// 
/// File Contents
/// 
/// Automatically generated code from Windows Form designer. We did not alter this in any way. Represents the 
/// spreadsheet GUI components.
/// 
///</summary>

namespace GUI
{
    partial class SpreadsheetGUI
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cellNameBox = new System.Windows.Forms.TextBox();
            this.valueBox = new System.Windows.Forms.TextBox();
            this.contentsBox = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.READMEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayDependenciesStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keyNavigationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dependencyButton = new System.Windows.Forms.Button();
            this.gridWidget = new SpreadsheetGrid_Core.SpreadsheetGridWidget();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.dependencyGrid = new System.Windows.Forms.DataGridView();
            this.Dependent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dependees = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dependencyGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cell Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(525, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Contents";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(118, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "Value";
            // 
            // cellNameBox
            // 
            this.cellNameBox.BackColor = System.Drawing.Color.White;
            this.cellNameBox.Enabled = false;
            this.cellNameBox.Location = new System.Drawing.Point(13, 88);
            this.cellNameBox.Name = "cellNameBox";
            this.cellNameBox.Size = new System.Drawing.Size(89, 31);
            this.cellNameBox.TabIndex = 4;
            // 
            // valueBox
            // 
            this.valueBox.BackColor = System.Drawing.Color.White;
            this.valueBox.Location = new System.Drawing.Point(118, 88);
            this.valueBox.Name = "valueBox";
            this.valueBox.ReadOnly = true;
            this.valueBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.valueBox.Size = new System.Drawing.Size(390, 31);
            this.valueBox.TabIndex = 5;
            this.valueBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.valueBox_KeyUp);
            // 
            // contentsBox
            // 
            this.contentsBox.Location = new System.Drawing.Point(525, 88);
            this.contentsBox.Name = "contentsBox";
            this.contentsBox.Size = new System.Drawing.Size(328, 31);
            this.contentsBox.TabIndex = 6;
            this.contentsBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.contentsBox_KeyUp);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1423, 33);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(54, 29);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(158, 34);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(158, 34);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(158, 34);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(158, 34);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.READMEToolStripMenuItem,
            this.displayDependenciesStripMenuItem,
            this.keyNavigationToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(65, 29);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // READMEToolStripMenuItem
            // 
            this.READMEToolStripMenuItem.Name = "READMEToolStripMenuItem";
            this.READMEToolStripMenuItem.Size = new System.Drawing.Size(288, 34);
            this.READMEToolStripMenuItem.Text = "README";
            this.READMEToolStripMenuItem.Click += new System.EventHandler(this.READMEToolStripMenuItem_Click);
            // 
            // displayDependenciesStripMenuItem
            // 
            this.displayDependenciesStripMenuItem.Name = "displayDependenciesStripMenuItem";
            this.displayDependenciesStripMenuItem.Size = new System.Drawing.Size(288, 34);
            this.displayDependenciesStripMenuItem.Text = "Display Dependencies";
            this.displayDependenciesStripMenuItem.Click += new System.EventHandler(this.displayDependenciesStripMenuItem_Click);
            // 
            // keyNavigationToolStripMenuItem
            // 
            this.keyNavigationToolStripMenuItem.Name = "keyNavigationToolStripMenuItem";
            this.keyNavigationToolStripMenuItem.Size = new System.Drawing.Size(288, 34);
            this.keyNavigationToolStripMenuItem.Text = "Key Navigation";
            this.keyNavigationToolStripMenuItem.Click += new System.EventHandler(this.keyNavigationToolStripMenuItem_Click);
            // 
            // dependencyButton
            // 
            this.dependencyButton.Location = new System.Drawing.Point(859, 41);
            this.dependencyButton.Name = "dependencyButton";
            this.dependencyButton.Size = new System.Drawing.Size(150, 82);
            this.dependencyButton.TabIndex = 8;
            this.dependencyButton.Text = "Show Dependencies";
            this.dependencyButton.UseVisualStyleBackColor = true;
            this.dependencyButton.Click += new System.EventHandler(this.dependencyButton_Click);
            // 
            // gridWidget
            // 
            this.gridWidget.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.gridWidget.Location = new System.Drawing.Point(0, 129);
            this.gridWidget.Name = "gridWidget";
            this.gridWidget.Size = new System.Drawing.Size(1009, 550);
            this.gridWidget.TabIndex = 10;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // dependencyGrid
            // 
            this.dependencyGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dependencyGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Dependent,
            this.Dependees});
            this.dependencyGrid.Location = new System.Drawing.Point(1035, 53);
            this.dependencyGrid.Name = "dependencyGrid";
            this.dependencyGrid.RowHeadersWidth = 62;
            this.dependencyGrid.RowTemplate.Height = 33;
            this.dependencyGrid.Size = new System.Drawing.Size(365, 511);
            this.dependencyGrid.TabIndex = 11;
            // 
            // Dependent
            // 
            this.Dependent.HeaderText = "Dependent";
            this.Dependent.MinimumWidth = 8;
            this.Dependent.Name = "Dependent";
            this.Dependent.ReadOnly = true;
            this.Dependent.Width = 150;
            // 
            // Dependees
            // 
            this.Dependees.HeaderText = "Dependees";
            this.Dependees.MinimumWidth = 8;
            this.Dependees.Name = "Dependees";
            this.Dependees.ReadOnly = true;
            this.Dependees.Width = 150;
            // 
            // SpreadsheetGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1423, 679);
            this.Controls.Add(this.dependencyGrid);
            this.Controls.Add(this.gridWidget);
            this.Controls.Add(this.dependencyButton);
            this.Controls.Add(this.contentsBox);
            this.Controls.Add(this.valueBox);
            this.Controls.Add(this.cellNameBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SpreadsheetGUI";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dependencyGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox cellNameBox;
        private TextBox valueBox;
        private TextBox contentsBox;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem READMEToolStripMenuItem;
        private ToolStripMenuItem displayDependenciesStripMenuItem;
        private Button dependencyButton;
        private SpreadsheetGrid_Core.SpreadsheetGridWidget gridWidget;
        private SaveFileDialog saveFileDialog;
        private OpenFileDialog openFileDialog;
        private DataGridView dependencyGrid;
        private DataGridViewTextBoxColumn Dependent;
        private DataGridViewTextBoxColumn Dependees;
        private ToolStripMenuItem keyNavigationToolStripMenuItem;
    }
}