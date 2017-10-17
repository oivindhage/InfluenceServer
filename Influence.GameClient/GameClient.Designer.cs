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
            this.btnListAllSessions = new System.Windows.Forms.Button();
            this.cmbCurrentGames = new System.Windows.Forms.ComboBox();
            this.radioAttackFrom = new System.Windows.Forms.RadioButton();
            this.radioAttackDestination = new System.Windows.Forms.RadioButton();
            this.radioReinforce = new System.Windows.Forms.RadioButton();
            this.lblAttackFrom = new System.Windows.Forms.Label();
            this.lblAttackTo = new System.Windows.Forms.Label();
            this.lblReinforce = new System.Windows.Forms.Label();
            this.btnCreateSession = new System.Windows.Forms.Button();
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
            this.picBoard.Click += new System.EventHandler(this.picBoard_Click);
            // 
            // rtxPlayerStatus
            // 
            this.rtxPlayerStatus.Location = new System.Drawing.Point(625, 452);
            this.rtxPlayerStatus.Name = "rtxPlayerStatus";
            this.rtxPlayerStatus.Size = new System.Drawing.Size(430, 379);
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
            this.cmbCurrentGames.Location = new System.Drawing.Point(159, 170);
            this.cmbCurrentGames.Name = "cmbCurrentGames";
            this.cmbCurrentGames.Size = new System.Drawing.Size(896, 32);
            this.cmbCurrentGames.TabIndex = 15;
            // 
            // radioAttackFrom
            // 
            this.radioAttackFrom.AutoSize = true;
            this.radioAttackFrom.Location = new System.Drawing.Point(625, 851);
            this.radioAttackFrom.Name = "radioAttackFrom";
            this.radioAttackFrom.Size = new System.Drawing.Size(135, 29);
            this.radioAttackFrom.TabIndex = 16;
            this.radioAttackFrom.TabStop = true;
            this.radioAttackFrom.Text = "Attack from";
            this.radioAttackFrom.UseVisualStyleBackColor = true;
            this.radioAttackFrom.CheckedChanged += new System.EventHandler(this.radioAttackFrom_CheckedChanged);
            // 
            // radioAttackDestination
            // 
            this.radioAttackDestination.AutoSize = true;
            this.radioAttackDestination.Location = new System.Drawing.Point(625, 886);
            this.radioAttackDestination.Name = "radioAttackDestination";
            this.radioAttackDestination.Size = new System.Drawing.Size(191, 29);
            this.radioAttackDestination.TabIndex = 17;
            this.radioAttackDestination.TabStop = true;
            this.radioAttackDestination.Text = "Attack destination";
            this.radioAttackDestination.UseVisualStyleBackColor = true;
            this.radioAttackDestination.CheckedChanged += new System.EventHandler(this.radioAttackDestination_CheckedChanged);
            // 
            // radioReinforce
            // 
            this.radioReinforce.AutoSize = true;
            this.radioReinforce.Location = new System.Drawing.Point(625, 921);
            this.radioReinforce.Name = "radioReinforce";
            this.radioReinforce.Size = new System.Drawing.Size(119, 29);
            this.radioReinforce.TabIndex = 18;
            this.radioReinforce.TabStop = true;
            this.radioReinforce.Text = "Reinforce";
            this.radioReinforce.UseVisualStyleBackColor = true;
            this.radioReinforce.CheckedChanged += new System.EventHandler(this.radioReinforce_CheckedChanged);
            // 
            // lblAttackFrom
            // 
            this.lblAttackFrom.AutoSize = true;
            this.lblAttackFrom.Location = new System.Drawing.Point(827, 851);
            this.lblAttackFrom.Name = "lblAttackFrom";
            this.lblAttackFrom.Size = new System.Drawing.Size(0, 25);
            this.lblAttackFrom.TabIndex = 19;
            // 
            // lblAttackTo
            // 
            this.lblAttackTo.AutoSize = true;
            this.lblAttackTo.Location = new System.Drawing.Point(827, 886);
            this.lblAttackTo.Name = "lblAttackTo";
            this.lblAttackTo.Size = new System.Drawing.Size(0, 25);
            this.lblAttackTo.TabIndex = 20;
            // 
            // lblReinforce
            // 
            this.lblReinforce.AutoSize = true;
            this.lblReinforce.Location = new System.Drawing.Point(827, 921);
            this.lblReinforce.Name = "lblReinforce";
            this.lblReinforce.Size = new System.Drawing.Size(0, 25);
            this.lblReinforce.TabIndex = 21;
            // 
            // btnCreateSession
            // 
            this.btnCreateSession.Location = new System.Drawing.Point(303, 367);
            this.btnCreateSession.Name = "btnCreateSession";
            this.btnCreateSession.Size = new System.Drawing.Size(278, 46);
            this.btnCreateSession.TabIndex = 22;
            this.btnCreateSession.Text = "Create session";
            this.btnCreateSession.UseVisualStyleBackColor = true;
            this.btnCreateSession.Click += new System.EventHandler(this.btnCreateSession_Click);
            // 
            // GameClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1869, 1442);
            this.Controls.Add(this.btnCreateSession);
            this.Controls.Add(this.lblReinforce);
            this.Controls.Add(this.lblAttackTo);
            this.Controls.Add(this.lblAttackFrom);
            this.Controls.Add(this.radioReinforce);
            this.Controls.Add(this.radioAttackDestination);
            this.Controls.Add(this.radioAttackFrom);
            this.Controls.Add(this.cmbCurrentGames);
            this.Controls.Add(this.btnListAllSessions);
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
        private System.Windows.Forms.Button btnListAllSessions;
        private System.Windows.Forms.ComboBox cmbCurrentGames;
        private System.Windows.Forms.RadioButton radioAttackFrom;
        private System.Windows.Forms.RadioButton radioAttackDestination;
        private System.Windows.Forms.RadioButton radioReinforce;
        private System.Windows.Forms.Label lblAttackFrom;
        private System.Windows.Forms.Label lblAttackTo;
        private System.Windows.Forms.Label lblReinforce;
        private System.Windows.Forms.Button btnCreateSession;
    }
}