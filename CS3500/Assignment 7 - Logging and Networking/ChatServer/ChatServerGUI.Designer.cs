/// <summary>
/// Author: Drew Lawson and Peter Bruns
/// Team: NoSleep
/// Date: April 4, 2022
/// Course: CS 3500, University of Utah, School of Computing
/// Copyright CS 3500, Drew Lawson, and Peter Bruns - This work may not be copied for use in Academic Coursework
/// 
/// We, Drew Lawson and Peter Bruns, certify that I wrote this code from scratch and did not copy it in part or whole from
/// another source. All references used in the completion of the assignment are cited in my README file.
/// 
/// File Contents
/// 
/// Austomatically generated code for the layout of the ChatServerGUI.
/// 
///</summary>

namespace ChatServer
{
    partial class ChatServerGUI
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
            this.chatLog = new System.Windows.Forms.TextBox();
            this.participantList = new System.Windows.Forms.TextBox();
            this.serverAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.connectButton = new System.Windows.Forms.Button();
            this.messageBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chatLog
            // 
            this.chatLog.BackColor = System.Drawing.Color.White;
            this.chatLog.Location = new System.Drawing.Point(69, 230);
            this.chatLog.Multiline = true;
            this.chatLog.Name = "chatLog";
            this.chatLog.ReadOnly = true;
            this.chatLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.chatLog.Size = new System.Drawing.Size(524, 326);
            this.chatLog.TabIndex = 0;
            // 
            // participantList
            // 
            this.participantList.BackColor = System.Drawing.Color.White;
            this.participantList.Location = new System.Drawing.Point(659, 230);
            this.participantList.Multiline = true;
            this.participantList.Name = "participantList";
            this.participantList.ReadOnly = true;
            this.participantList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.participantList.Size = new System.Drawing.Size(329, 326);
            this.participantList.TabIndex = 1;
            // 
            // serverAddress
            // 
            this.serverAddress.Enabled = false;
            this.serverAddress.Location = new System.Drawing.Point(242, 21);
            this.serverAddress.Name = "serverAddress";
            this.serverAddress.Size = new System.Drawing.Size(247, 31);
            this.serverAddress.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Server IP address";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(69, 197);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Chat log";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(663, 190);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 25);
            this.label4.TabIndex = 7;
            this.label4.Text = "Participants";
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(70, 574);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(185, 34);
            this.connectButton.TabIndex = 8;
            this.connectButton.Text = "Shutdown Server";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // messageBox
            // 
            this.messageBox.Location = new System.Drawing.Point(242, 91);
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(523, 31);
            this.messageBox.TabIndex = 9;
            this.messageBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.messageBox_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 25);
            this.label1.TabIndex = 10;
            this.label1.Text = "Message All Participants";
            // 
            // ChatServerGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 642);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.serverAddress);
            this.Controls.Add(this.participantList);
            this.Controls.Add(this.chatLog);
            this.Name = "ChatServerGUI";
            this.Text = "ChatServer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatServer_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox chatLog;
        private TextBox participantList;
        private TextBox serverAddress;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button connectButton;
        private TextBox messageBox;
        private Label label1;
    }
}