﻿namespace Influence.GameClient
{
    partial class GameClient
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
            this.btnConnectToSession = new System.Windows.Forms.Button();
            this.lblSessionUrl = new System.Windows.Forms.Label();
            this.txtSessionUrl = new System.Windows.Forms.TextBox();
            this.lblClientId = new System.Windows.Forms.Label();
            this.txtClientId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPlayerNick = new System.Windows.Forms.TextBox();
            this.tmrPoll = new System.Windows.Forms.Timer(this.components);
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDrawStatus = new System.Windows.Forms.Button();
            this.picBoard = new System.Windows.Forms.PictureBox();
            this.rtxPlayerStatus = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSessionGuid = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConnectToSession
            // 
            this.btnConnectToSession.Location = new System.Drawing.Point(19, 248);
            this.btnConnectToSession.Name = "btnConnectToSession";
            this.btnConnectToSession.Size = new System.Drawing.Size(278, 46);
            this.btnConnectToSession.TabIndex = 0;
            this.btnConnectToSession.Text = "Connect to session";
            this.btnConnectToSession.UseVisualStyleBackColor = true;
            this.btnConnectToSession.Click += new System.EventHandler(this.btnConnectToSession_Click);
            // 
            // lblSessionUrl
            // 
            this.lblSessionUrl.AutoSize = true;
            this.lblSessionUrl.Location = new System.Drawing.Point(12, 24);
            this.lblSessionUrl.Name = "lblSessionUrl";
            this.lblSessionUrl.Size = new System.Drawing.Size(109, 25);
            this.lblSessionUrl.TabIndex = 1;
            this.lblSessionUrl.Text = "Session url";
            // 
            // txtSessionUrl
            // 
            this.txtSessionUrl.Location = new System.Drawing.Point(157, 21);
            this.txtSessionUrl.Name = "txtSessionUrl";
            this.txtSessionUrl.Size = new System.Drawing.Size(896, 29);
            this.txtSessionUrl.TabIndex = 2;
            // 
            // lblClientId
            // 
            this.lblClientId.AutoSize = true;
            this.lblClientId.Location = new System.Drawing.Point(12, 69);
            this.lblClientId.Name = "lblClientId";
            this.lblClientId.Size = new System.Drawing.Size(82, 25);
            this.lblClientId.TabIndex = 3;
            this.lblClientId.Text = "Client id";
            // 
            // txtClientId
            // 
            this.txtClientId.Location = new System.Drawing.Point(157, 69);
            this.txtClientId.Name = "txtClientId";
            this.txtClientId.Size = new System.Drawing.Size(896, 29);
            this.txtClientId.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Player name";
            // 
            // txtPlayerNick
            // 
            this.txtPlayerNick.Location = new System.Drawing.Point(157, 116);
            this.txtPlayerNick.Name = "txtPlayerNick";
            this.txtPlayerNick.Size = new System.Drawing.Size(896, 29);
            this.txtPlayerNick.TabIndex = 6;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(17, 930);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(1036, 346);
            this.txtStatus.TabIndex = 7;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(303, 248);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(278, 46);
            this.btnUpdate.TabIndex = 8;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnDrawStatus
            // 
            this.btnDrawStatus.Location = new System.Drawing.Point(587, 248);
            this.btnDrawStatus.Name = "btnDrawStatus";
            this.btnDrawStatus.Size = new System.Drawing.Size(278, 46);
            this.btnDrawStatus.TabIndex = 9;
            this.btnDrawStatus.Text = "Draw status";
            this.btnDrawStatus.UseVisualStyleBackColor = true;
            this.btnDrawStatus.Click += new System.EventHandler(this.btnDrawStatus_Click);
            // 
            // picBoard
            // 
            this.picBoard.Location = new System.Drawing.Point(19, 310);
            this.picBoard.Name = "picBoard";
            this.picBoard.Size = new System.Drawing.Size(600, 600);
            this.picBoard.TabIndex = 10;
            this.picBoard.TabStop = false;
            // 
            // rtxPlayerStatus
            // 
            this.rtxPlayerStatus.Location = new System.Drawing.Point(625, 310);
            this.rtxPlayerStatus.Name = "rtxPlayerStatus";
            this.rtxPlayerStatus.Size = new System.Drawing.Size(430, 591);
            this.rtxPlayerStatus.TabIndex = 11;
            this.rtxPlayerStatus.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 25);
            this.label2.TabIndex = 12;
            this.label2.Text = "Session GUID";
            // 
            // txtSessionGuid
            // 
            this.txtSessionGuid.Location = new System.Drawing.Point(157, 169);
            this.txtSessionGuid.Name = "txtSessionGuid";
            this.txtSessionGuid.Size = new System.Drawing.Size(896, 29);
            this.txtSessionGuid.TabIndex = 13;
            // 
            // GameClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1869, 1297);
            this.Controls.Add(this.txtSessionGuid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rtxPlayerStatus);
            this.Controls.Add(this.picBoard);
            this.Controls.Add(this.btnDrawStatus);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.txtPlayerNick);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtClientId);
            this.Controls.Add(this.lblClientId);
            this.Controls.Add(this.txtSessionUrl);
            this.Controls.Add(this.lblSessionUrl);
            this.Controls.Add(this.btnConnectToSession);
            this.Name = "GameClient";
            this.Text = "GameClient";
            this.Load += new System.EventHandler(this.GameClient_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBoard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnectToSession;
        private System.Windows.Forms.Label lblSessionUrl;
        private System.Windows.Forms.TextBox txtSessionUrl;
        private System.Windows.Forms.Label lblClientId;
        private System.Windows.Forms.TextBox txtClientId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPlayerNick;
        private System.Windows.Forms.Timer tmrPoll;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDrawStatus;
        private System.Windows.Forms.PictureBox picBoard;
        private System.Windows.Forms.RichTextBox rtxPlayerStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSessionGuid;
    }
}