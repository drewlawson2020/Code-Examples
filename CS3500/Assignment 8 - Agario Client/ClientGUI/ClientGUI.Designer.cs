/// <summary>
/// Author: Drew Lawson and Peter Bruns
/// Team: NotSoSuperMario
/// Date: April 14, 2022
/// Course: CS 3500, University of Utah, School of Computing
/// Copyright CS 3500, Drew Lawson, and Peter Bruns - This work may not be copied for use in Academic Coursework
/// 
/// We, Drew Lawson and Peter Bruns, certify that I wrote this code from scratch and did not copy it in part or whole from
/// another source. All references used in the completion of the assignment are cited in my README file.
/// 
/// File Contents
/// 
/// Automatically generated design layout code for the client GUI application.
/// 
///</summary>

namespace ClientGUI
{
    partial class ClientGUI
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
            this.playerNameBox = new System.Windows.Forms.TextBox();
            this.serverAddressBox = new System.Windows.Forms.TextBox();
            this.enterNameLabel = new System.Windows.Forms.Label();
            this.serverAddressLabel = new System.Windows.Forms.Label();
            this.connectButton = new System.Windows.Forms.Button();
            this.errorMessageLabel = new System.Windows.Forms.Label();
            this.massLabel = new System.Windows.Forms.Label();
            this.numberOfFoodText = new System.Windows.Forms.Label();
            this.numberOfPlayerText = new System.Windows.Forms.Label();
            this.positionText = new System.Windows.Forms.Label();
            this.playerMassLabel = new System.Windows.Forms.Label();
            this.numberOfPlayersLabel = new System.Windows.Forms.Label();
            this.numberOfFoodsLabel = new System.Windows.Forms.Label();
            this.positionLabel = new System.Windows.Forms.Label();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cursorPositionLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // playerNameBox
            // 
            this.playerNameBox.Location = new System.Drawing.Point(181, 189);
            this.playerNameBox.Name = "playerNameBox";
            this.playerNameBox.Size = new System.Drawing.Size(269, 31);
            this.playerNameBox.TabIndex = 0;
            this.playerNameBox.TextChanged += new System.EventHandler(this.playerNameBox_TextChanged);
            // 
            // serverAddressBox
            // 
            this.serverAddressBox.Location = new System.Drawing.Point(181, 259);
            this.serverAddressBox.Name = "serverAddressBox";
            this.serverAddressBox.Size = new System.Drawing.Size(269, 31);
            this.serverAddressBox.TabIndex = 1;
            this.serverAddressBox.TextChanged += new System.EventHandler(this.serverAddressBox_TextChanged);
            // 
            // enterNameLabel
            // 
            this.enterNameLabel.AutoSize = true;
            this.enterNameLabel.Location = new System.Drawing.Point(29, 189);
            this.enterNameLabel.Name = "enterNameLabel";
            this.enterNameLabel.Size = new System.Drawing.Size(146, 25);
            this.enterNameLabel.TabIndex = 2;
            this.enterNameLabel.Text = "Enter your name:";
            // 
            // serverAddressLabel
            // 
            this.serverAddressLabel.AutoSize = true;
            this.serverAddressLabel.Location = new System.Drawing.Point(40, 262);
            this.serverAddressLabel.Name = "serverAddressLabel";
            this.serverAddressLabel.Size = new System.Drawing.Size(135, 25);
            this.serverAddressLabel.TabIndex = 3;
            this.serverAddressLabel.Text = "Server Address:";
            // 
            // connectButton
            // 
            this.connectButton.Enabled = false;
            this.connectButton.Location = new System.Drawing.Point(244, 334);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(112, 34);
            this.connectButton.TabIndex = 4;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // errorMessageLabel
            // 
            this.errorMessageLabel.AutoSize = true;
            this.errorMessageLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.errorMessageLabel.ForeColor = System.Drawing.Color.Red;
            this.errorMessageLabel.Location = new System.Drawing.Point(200, 399);
            this.errorMessageLabel.Name = "errorMessageLabel";
            this.errorMessageLabel.Size = new System.Drawing.Size(189, 32);
            this.errorMessageLabel.TabIndex = 5;
            this.errorMessageLabel.Text = "Error Messages";
            this.errorMessageLabel.Visible = false;
            // 
            // massLabel
            // 
            this.massLabel.AutoSize = true;
            this.massLabel.Location = new System.Drawing.Point(1047, 39);
            this.massLabel.Name = "massLabel";
            this.massLabel.Size = new System.Drawing.Size(109, 25);
            this.massLabel.TabIndex = 6;
            this.massLabel.Text = "Player Mass:";
            // 
            // numberOfFoodText
            // 
            this.numberOfFoodText.AutoSize = true;
            this.numberOfFoodText.Location = new System.Drawing.Point(998, 118);
            this.numberOfFoodText.Name = "numberOfFoodText";
            this.numberOfFoodText.Size = new System.Drawing.Size(158, 25);
            this.numberOfFoodText.TabIndex = 7;
            this.numberOfFoodText.Text = "Number of Foods:";
            // 
            // numberOfPlayerText
            // 
            this.numberOfPlayerText.AutoSize = true;
            this.numberOfPlayerText.Location = new System.Drawing.Point(1001, 77);
            this.numberOfPlayerText.Name = "numberOfPlayerText";
            this.numberOfPlayerText.Size = new System.Drawing.Size(163, 25);
            this.numberOfPlayerText.TabIndex = 8;
            this.numberOfPlayerText.Text = "Number of Players:";
            // 
            // positionText
            // 
            this.positionText.AutoSize = true;
            this.positionText.Location = new System.Drawing.Point(1005, 161);
            this.positionText.Name = "positionText";
            this.positionText.Size = new System.Drawing.Size(151, 25);
            this.positionText.TabIndex = 9;
            this.positionText.Text = "Position in World:";
            // 
            // playerMassLabel
            // 
            this.playerMassLabel.AutoSize = true;
            this.playerMassLabel.Location = new System.Drawing.Point(1162, 39);
            this.playerMassLabel.Name = "playerMassLabel";
            this.playerMassLabel.Size = new System.Drawing.Size(0, 25);
            this.playerMassLabel.TabIndex = 10;
            // 
            // numberOfPlayersLabel
            // 
            this.numberOfPlayersLabel.AutoSize = true;
            this.numberOfPlayersLabel.Location = new System.Drawing.Point(1170, 77);
            this.numberOfPlayersLabel.Name = "numberOfPlayersLabel";
            this.numberOfPlayersLabel.Size = new System.Drawing.Size(0, 25);
            this.numberOfPlayersLabel.TabIndex = 11;
            // 
            // numberOfFoodsLabel
            // 
            this.numberOfFoodsLabel.AutoSize = true;
            this.numberOfFoodsLabel.Location = new System.Drawing.Point(1162, 118);
            this.numberOfFoodsLabel.Name = "numberOfFoodsLabel";
            this.numberOfFoodsLabel.Size = new System.Drawing.Size(0, 25);
            this.numberOfFoodsLabel.TabIndex = 12;
            // 
            // positionLabel
            // 
            this.positionLabel.AutoSize = true;
            this.positionLabel.Location = new System.Drawing.Point(1162, 161);
            this.positionLabel.Name = "positionLabel";
            this.positionLabel.Size = new System.Drawing.Size(0, 25);
            this.positionLabel.TabIndex = 13;
            // 
            // disconnectButton
            // 
            this.disconnectButton.Enabled = false;
            this.disconnectButton.Location = new System.Drawing.Point(1053, 272);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(112, 34);
            this.disconnectButton.TabIndex = 14;
            this.disconnectButton.Text = "Disconnect";
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1017, 204);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 25);
            this.label1.TabIndex = 15;
            this.label1.Text = "Cursor positon: ";
            // 
            // cursorPositionLabel
            // 
            this.cursorPositionLabel.AutoSize = true;
            this.cursorPositionLabel.Location = new System.Drawing.Point(1162, 204);
            this.cursorPositionLabel.Name = "cursorPositionLabel";
            this.cursorPositionLabel.Size = new System.Drawing.Size(0, 25);
            this.cursorPositionLabel.TabIndex = 16;
            // 
            // ClientGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.ClientSize = new System.Drawing.Size(1278, 944);
            this.Controls.Add(this.cursorPositionLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.disconnectButton);
            this.Controls.Add(this.positionLabel);
            this.Controls.Add(this.numberOfFoodsLabel);
            this.Controls.Add(this.numberOfPlayersLabel);
            this.Controls.Add(this.playerMassLabel);
            this.Controls.Add(this.positionText);
            this.Controls.Add(this.numberOfPlayerText);
            this.Controls.Add(this.numberOfFoodText);
            this.Controls.Add(this.massLabel);
            this.Controls.Add(this.errorMessageLabel);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.serverAddressLabel);
            this.Controls.Add(this.enterNameLabel);
            this.Controls.Add(this.serverAddressBox);
            this.Controls.Add(this.playerNameBox);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "ClientGUI";
            this.Text = "Fauxgar.Io";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ClientGUI_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox playerNameBox;
        private TextBox serverAddressBox;
        private Label enterNameLabel;
        private Label serverAddressLabel;
        private Button connectButton;
        private Label errorMessageLabel;
        private Label massLabel;
        private Label numberOfFoodText;
        private Label numberOfPlayerText;
        private Label positionText;
        private Label playerMassLabel;
        private Label numberOfPlayersLabel;
        private Label numberOfFoodsLabel;
        private Label positionLabel;
        private Button disconnectButton;
        private Label label1;
        private Label cursorPositionLabel;
    }
}