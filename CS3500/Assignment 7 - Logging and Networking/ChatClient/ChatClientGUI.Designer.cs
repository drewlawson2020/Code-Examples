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
/// Austomatically generated code for the layout of the ChatClientGUI.
/// 
///</summary>

namespace ChatClient
{
    partial class ChatClientGUI
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
            this.serverAddressBox = new System.Windows.Forms.TextBox();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.textToSend = new System.Windows.Forms.TextBox();
            this.chatLog = new System.Windows.Forms.TextBox();
            this.participantsList = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // serverAddressBox
            // 
            this.serverAddressBox.Location = new System.Drawing.Point(206, 42);
            this.serverAddressBox.Name = "serverAddressBox";
            this.serverAddressBox.Size = new System.Drawing.Size(150, 31);
            this.serverAddressBox.TabIndex = 0;
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(206, 101);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(150, 31);
            this.nameBox.TabIndex = 1;
            this.nameBox.TextChanged += new System.EventHandler(this.nameBox_TextChanged);
            this.nameBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nameBox_KeyDown);
            // 
            // connectButton
            // 
            this.connectButton.Enabled = false;
            this.connectButton.Location = new System.Drawing.Point(223, 151);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(112, 34);
            this.connectButton.TabIndex = 2;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // textToSend
            // 
            this.textToSend.Enabled = false;
            this.textToSend.Location = new System.Drawing.Point(206, 212);
            this.textToSend.Name = "textToSend";
            this.textToSend.Size = new System.Drawing.Size(330, 31);
            this.textToSend.TabIndex = 4;
            this.textToSend.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textToSend_KeyDown);
            // 
            // chatLog
            // 
            this.chatLog.BackColor = System.Drawing.Color.White;
            this.chatLog.Location = new System.Drawing.Point(105, 339);
            this.chatLog.Multiline = true;
            this.chatLog.Name = "chatLog";
            this.chatLog.ReadOnly = true;
            this.chatLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.chatLog.Size = new System.Drawing.Size(823, 327);
            this.chatLog.TabIndex = 5;
            // 
            // participantsList
            // 
            this.participantsList.BackColor = System.Drawing.Color.White;
            this.participantsList.Location = new System.Drawing.Point(587, 58);
            this.participantsList.Multiline = true;
            this.participantsList.Name = "participantsList";
            this.participantsList.ReadOnly = true;
            this.participantsList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.participantsList.Size = new System.Drawing.Size(341, 220);
            this.participantsList.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "Server name/address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(94, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "Username";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(84, 218);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "Text to send";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(112, 303);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 25);
            this.label4.TabIndex = 10;
            this.label4.Text = "Chat log";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(587, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 25);
            this.label5.TabIndex = 11;
            this.label5.Text = "Participants list";
            // 
            // ChatClientGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 709);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.participantsList);
            this.Controls.Add(this.chatLog);
            this.Controls.Add(this.textToSend);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.serverAddressBox);
            this.Name = "ChatClientGUI";
            this.Text = "ChatClient";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatClient_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox serverAddressBox;
        private TextBox nameBox;
        private Button connectButton;
        private TextBox textToSend;
        private TextBox chatLog;
        private TextBox participantsList;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
    }
}