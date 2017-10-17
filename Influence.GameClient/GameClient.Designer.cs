namespace Influence.GameClient
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
            this.txtSessionBaseUrl = new System.Windows.Forms.TextBox();
            this.lblClientId = new System.Windows.Forms.Label();
            this.txtPlayerId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPlayerName = new System.Windows.Forms.TextBox();
            this.tmrPoll = new System.Windows.Forms.Timer(this.components);
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.btnShowSessionDetails = new System.Windows.Forms.Button();
            this.btnDrawStatus = new System.Windows.Forms.Button();
            this.picBoard = new System.Windows.Forms.PictureBox();
            this.rtxPlayerStatus = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSessionGuid = new System.Windows.Forms.TextBox();
            this.btnListAllSessions = new System.Windows.Forms.Button();
            this.cmbCurrentGames = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConnectToSession
            // 
            this.btnConnectToSession.Location = new System.Drawing.Point(19, 303);
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
            this.lblSessionUrl.Location = new System.Drawing.Point(14, 25);
            this.lblSessionUrl.Name = "lblSessionUrl";
            this.lblSessionUrl.Size = new System.Drawing.Size(109, 25);
            this.lblSessionUrl.TabIndex = 1;
            this.lblSessionUrl.Text = "Session url";
            // 
            // txtSessionBaseUrl
            // 
            this.txtSessionBaseUrl.Location = new System.Drawing.Point(157, 21);
            this.txtSessionBaseUrl.Name = "txtSessionBaseUrl";
            this.txtSessionBaseUrl.Size = new System.Drawing.Size(896, 29);
            this.txtSessionBaseUrl.TabIndex = 2;
            // 
            // lblClientId
            // 
            this.lblClientId.AutoSize = true;
            this.lblClientId.Location = new System.Drawing.Point(15, 69);
            this.lblClientId.Name = "lblClientId";
            this.lblClientId.Size = new System.Drawing.Size(82, 25);
            this.lblClientId.TabIndex = 3;
            this.lblClientId.Text = "Client id";
            // 
            // txtPlayerId
            // 
            this.txtPlayerId.Location = new System.Drawing.Point(157, 69);
            this.txtPlayerId.Name = "txtPlayerId";
            this.txtPlayerId.Size = new System.Drawing.Size(896, 29);
            this.txtPlayerId.TabIndex = 4;
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
            // txtPlayerName
            // 
            this.txtPlayerName.Location = new System.Drawing.Point(157, 116);
            this.txtPlayerName.Name = "txtPlayerName";
            this.txtPlayerName.Size = new System.Drawing.Size(896, 29);
            this.txtPlayerName.TabIndex = 6;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(17, 1072);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(1036, 346);
            this.txtStatus.TabIndex = 7;
            // 
            // btnShowSessionDetails
            // 
            this.btnShowSessionDetails.Location = new System.Drawing.Point(303, 303);
            this.btnShowSessionDetails.Name = "btnShowSessionDetails";
            this.btnShowSessionDetails.Size = new System.Drawing.Size(278, 46);
            this.btnShowSessionDetails.TabIndex = 8;
            this.btnShowSessionDetails.Text = "Show session details";
            this.btnShowSessionDetails.UseVisualStyleBackColor = true;
            this.btnShowSessionDetails.Click += new System.EventHandler(this.btnShowSessionDetails_Click);
            // 
            // btnDrawStatus
            // 
            this.btnDrawStatus.Location = new System.Drawing.Point(587, 303);
            this.btnDrawStatus.Name = "btnDrawStatus";
            this.btnDrawStatus.Size = new System.Drawing.Size(278, 46);
            this.btnDrawStatus.TabIndex = 9;
            this.btnDrawStatus.Text = "Draw status";
            this.btnDrawStatus.UseVisualStyleBackColor = true;
            this.btnDrawStatus.Click += new System.EventHandler(this.btnDrawStatus_Click);
            // 
            // picBoard
            // 
            this.picBoard.Location = new System.Drawing.Point(19, 452);
            this.picBoard.Name = "picBoard";
            this.picBoard.Size = new System.Drawing.Size(600, 600);
            this.picBoard.TabIndex = 10;
            this.picBoard.TabStop = false;
            // 
            // rtxPlayerStatus
            // 
            this.rtxPlayerStatus.Location = new System.Drawing.Point(625, 452);
            this.rtxPlayerStatus.Name = "rtxPlayerStatus";
            this.rtxPlayerStatus.Size = new System.Drawing.Size(430, 600);
            this.rtxPlayerStatus.TabIndex = 11;
            this.rtxPlayerStatus.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 173);
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
            // btnListAllSessions
            // 
            this.btnListAllSessions.Location = new System.Drawing.Point(19, 367);
            this.btnListAllSessions.Name = "btnListAllSessions";
            this.btnListAllSessions.Size = new System.Drawing.Size(278, 46);
            this.btnListAllSessions.TabIndex = 14;
            this.btnListAllSessions.Text = "List all sessions";
            this.btnListAllSessions.UseVisualStyleBackColor = true;
            this.btnListAllSessions.Click += new System.EventHandler(this.btnListAllSessions_Click);
            // 
            // cmbCurrentGames
            // 
            this.cmbCurrentGames.Enabled = false;
            this.cmbCurrentGames.FormattingEnabled = true;
            this.cmbCurrentGames.Location = new System.Drawing.Point(157, 213);
            this.cmbCurrentGames.Name = "cmbCurrentGames";
            this.cmbCurrentGames.Size = new System.Drawing.Size(896, 32);
            this.cmbCurrentGames.TabIndex = 15;
            this.cmbCurrentGames.SelectedIndexChanged += new System.EventHandler(this.cmbCurrentGames_SelectedIndexChanged);
            // 
            // GameClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1869, 1442);
            this.Controls.Add(this.cmbCurrentGames);
            this.Controls.Add(this.btnListAllSessions);
            this.Controls.Add(this.txtSessionGuid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rtxPlayerStatus);
            this.Controls.Add(this.picBoard);
            this.Controls.Add(this.btnDrawStatus);
            this.Controls.Add(this.btnShowSessionDetails);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.txtPlayerName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPlayerId);
            this.Controls.Add(this.lblClientId);
            this.Controls.Add(this.txtSessionBaseUrl);
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
        private System.Windows.Forms.TextBox txtSessionBaseUrl;
        private System.Windows.Forms.Label lblClientId;
        private System.Windows.Forms.TextBox txtPlayerId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPlayerName;
        private System.Windows.Forms.Timer tmrPoll;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Button btnShowSessionDetails;
        private System.Windows.Forms.Button btnDrawStatus;
        private System.Windows.Forms.PictureBox picBoard;
        private System.Windows.Forms.RichTextBox rtxPlayerStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSessionGuid;
        private System.Windows.Forms.Button btnListAllSessions;
        private System.Windows.Forms.ComboBox cmbCurrentGames;
    }
}