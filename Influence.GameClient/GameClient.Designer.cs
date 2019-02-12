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
            this.btnJoinSession = new System.Windows.Forms.Button();
            this.lblSessionUrl = new System.Windows.Forms.Label();
            this.txtSessionBaseUrl = new System.Windows.Forms.TextBox();
            this.lblClientId = new System.Windows.Forms.Label();
            this.txtPlayerId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPlayerName = new System.Windows.Forms.TextBox();
            this.tmrPoll = new System.Windows.Forms.Timer(this.components);
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.btnShowSessionDetails = new System.Windows.Forms.Button();
            this.picBoard = new System.Windows.Forms.PictureBox();
            this.rtxPlayerStatus = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnListAllSessions = new System.Windows.Forms.Button();
            this.cmbCurrentGames = new System.Windows.Forms.ComboBox();
            this.lblAttackFrom = new System.Windows.Forms.Label();
            this.lblAttackTo = new System.Windows.Forms.Label();
            this.lblReinforce = new System.Windows.Forms.Label();
            this.btnCreateSession = new System.Windows.Forms.Button();
            this.btnStartSession = new System.Windows.Forms.Button();
            this.btnEndAttack = new System.Windows.Forms.Button();
            this.btnEndReinforce = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // btnJoinSession
            // 
            this.btnJoinSession.Location = new System.Drawing.Point(587, 260);
            this.btnJoinSession.Margin = new System.Windows.Forms.Padding(4);
            this.btnJoinSession.Name = "btnJoinSession";
            this.btnJoinSession.Size = new System.Drawing.Size(279, 46);
            this.btnJoinSession.TabIndex = 0;
            this.btnJoinSession.Text = "Join session";
            this.btnJoinSession.UseVisualStyleBackColor = true;
            this.btnJoinSession.Click += new System.EventHandler(this.btnJoinSession_Click);
            // 
            // lblSessionUrl
            // 
            this.lblSessionUrl.AutoSize = true;
            this.lblSessionUrl.Location = new System.Drawing.Point(15, 26);
            this.lblSessionUrl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSessionUrl.Name = "lblSessionUrl";
            this.lblSessionUrl.Size = new System.Drawing.Size(109, 25);
            this.lblSessionUrl.TabIndex = 1;
            this.lblSessionUrl.Text = "Session url";
            // 
            // txtSessionBaseUrl
            // 
            this.txtSessionBaseUrl.Location = new System.Drawing.Point(158, 20);
            this.txtSessionBaseUrl.Margin = new System.Windows.Forms.Padding(4);
            this.txtSessionBaseUrl.Name = "txtSessionBaseUrl";
            this.txtSessionBaseUrl.Size = new System.Drawing.Size(990, 29);
            this.txtSessionBaseUrl.TabIndex = 2;
            this.txtSessionBaseUrl.TextChanged += new System.EventHandler(this.txtSessionBaseUrl_TextChanged);
            // 
            // lblClientId
            // 
            this.lblClientId.AutoSize = true;
            this.lblClientId.Location = new System.Drawing.Point(15, 68);
            this.lblClientId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClientId.Name = "lblClientId";
            this.lblClientId.Size = new System.Drawing.Size(82, 25);
            this.lblClientId.TabIndex = 3;
            this.lblClientId.Text = "Client id";
            // 
            // txtPlayerId
            // 
            this.txtPlayerId.Location = new System.Drawing.Point(158, 68);
            this.txtPlayerId.Margin = new System.Windows.Forms.Padding(4);
            this.txtPlayerId.Name = "txtPlayerId";
            this.txtPlayerId.Size = new System.Drawing.Size(990, 29);
            this.txtPlayerId.TabIndex = 4;
            this.txtPlayerId.TextChanged += new System.EventHandler(this.txtPlayerId_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 120);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Player name";
            // 
            // txtPlayerName
            // 
            this.txtPlayerName.Location = new System.Drawing.Point(158, 116);
            this.txtPlayerName.Margin = new System.Windows.Forms.Padding(4);
            this.txtPlayerName.Name = "txtPlayerName";
            this.txtPlayerName.Size = new System.Drawing.Size(990, 29);
            this.txtPlayerName.TabIndex = 6;
            // 
            // tmrPoll
            // 
            this.tmrPoll.Enabled = true;
            this.tmrPoll.Tick += new System.EventHandler(this.tmrPoll_Tick);
            // 
            // txtStatus
            // 
            this.txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatus.Location = new System.Drawing.Point(13, 1074);
            this.txtStatus.Margin = new System.Windows.Forms.Padding(4);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(1804, 105);
            this.txtStatus.TabIndex = 7;
            // 
            // btnShowSessionDetails
            // 
            this.btnShowSessionDetails.Location = new System.Drawing.Point(17, 314);
            this.btnShowSessionDetails.Margin = new System.Windows.Forms.Padding(4);
            this.btnShowSessionDetails.Name = "btnShowSessionDetails";
            this.btnShowSessionDetails.Size = new System.Drawing.Size(279, 46);
            this.btnShowSessionDetails.TabIndex = 8;
            this.btnShowSessionDetails.Text = "Show session details";
            this.btnShowSessionDetails.UseVisualStyleBackColor = true;
            this.btnShowSessionDetails.Click += new System.EventHandler(this.btnShowSessionDetails_Click);
            // 
            // picBoard
            // 
            this.picBoard.Location = new System.Drawing.Point(18, 452);
            this.picBoard.Margin = new System.Windows.Forms.Padding(4);
            this.picBoard.Name = "picBoard";
            this.picBoard.Size = new System.Drawing.Size(600, 600);
            this.picBoard.TabIndex = 10;
            this.picBoard.TabStop = false;
            this.picBoard.Click += new System.EventHandler(this.picBoard_Click);
            // 
            // rtxPlayerStatus
            // 
            this.rtxPlayerStatus.Location = new System.Drawing.Point(625, 452);
            this.rtxPlayerStatus.Margin = new System.Windows.Forms.Padding(4);
            this.rtxPlayerStatus.Name = "rtxPlayerStatus";
            this.rtxPlayerStatus.Size = new System.Drawing.Size(525, 379);
            this.rtxPlayerStatus.TabIndex = 11;
            this.rtxPlayerStatus.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 174);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 25);
            this.label2.TabIndex = 12;
            this.label2.Text = "Session GUID";
            // 
            // btnListAllSessions
            // 
            this.btnListAllSessions.Location = new System.Drawing.Point(301, 260);
            this.btnListAllSessions.Margin = new System.Windows.Forms.Padding(4);
            this.btnListAllSessions.Name = "btnListAllSessions";
            this.btnListAllSessions.Size = new System.Drawing.Size(279, 46);
            this.btnListAllSessions.TabIndex = 14;
            this.btnListAllSessions.Text = "List all sessions";
            this.btnListAllSessions.UseVisualStyleBackColor = true;
            this.btnListAllSessions.Click += new System.EventHandler(this.btnListAllSessions_Click);
            // 
            // cmbCurrentGames
            // 
            this.cmbCurrentGames.Enabled = false;
            this.cmbCurrentGames.FormattingEnabled = true;
            this.cmbCurrentGames.Location = new System.Drawing.Point(160, 170);
            this.cmbCurrentGames.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCurrentGames.Name = "cmbCurrentGames";
            this.cmbCurrentGames.Size = new System.Drawing.Size(990, 32);
            this.cmbCurrentGames.TabIndex = 15;
            // 
            // lblAttackFrom
            // 
            this.lblAttackFrom.AutoSize = true;
            this.lblAttackFrom.Location = new System.Drawing.Point(827, 851);
            this.lblAttackFrom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAttackFrom.Name = "lblAttackFrom";
            this.lblAttackFrom.Size = new System.Drawing.Size(0, 25);
            this.lblAttackFrom.TabIndex = 19;
            // 
            // lblAttackTo
            // 
            this.lblAttackTo.AutoSize = true;
            this.lblAttackTo.Location = new System.Drawing.Point(827, 886);
            this.lblAttackTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAttackTo.Name = "lblAttackTo";
            this.lblAttackTo.Size = new System.Drawing.Size(0, 25);
            this.lblAttackTo.TabIndex = 20;
            // 
            // lblReinforce
            // 
            this.lblReinforce.AutoSize = true;
            this.lblReinforce.Location = new System.Drawing.Point(827, 921);
            this.lblReinforce.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReinforce.Name = "lblReinforce";
            this.lblReinforce.Size = new System.Drawing.Size(0, 25);
            this.lblReinforce.TabIndex = 21;
            // 
            // btnCreateSession
            // 
            this.btnCreateSession.Location = new System.Drawing.Point(17, 260);
            this.btnCreateSession.Margin = new System.Windows.Forms.Padding(4);
            this.btnCreateSession.Name = "btnCreateSession";
            this.btnCreateSession.Size = new System.Drawing.Size(279, 46);
            this.btnCreateSession.TabIndex = 22;
            this.btnCreateSession.Text = "Create session";
            this.btnCreateSession.UseVisualStyleBackColor = true;
            this.btnCreateSession.Click += new System.EventHandler(this.btnCreateSession_Click);
            // 
            // btnStartSession
            // 
            this.btnStartSession.Location = new System.Drawing.Point(873, 260);
            this.btnStartSession.Margin = new System.Windows.Forms.Padding(4);
            this.btnStartSession.Name = "btnStartSession";
            this.btnStartSession.Size = new System.Drawing.Size(279, 46);
            this.btnStartSession.TabIndex = 23;
            this.btnStartSession.Text = "Start session";
            this.btnStartSession.UseVisualStyleBackColor = true;
            this.btnStartSession.Click += new System.EventHandler(this.btnStartSession_Click);
            // 
            // btnEndAttack
            // 
            this.btnEndAttack.Location = new System.Drawing.Point(587, 318);
            this.btnEndAttack.Margin = new System.Windows.Forms.Padding(6);
            this.btnEndAttack.Name = "btnEndAttack";
            this.btnEndAttack.Size = new System.Drawing.Size(279, 42);
            this.btnEndAttack.TabIndex = 24;
            this.btnEndAttack.Text = "End attack";
            this.btnEndAttack.UseVisualStyleBackColor = true;
            this.btnEndAttack.Click += new System.EventHandler(this.btnEndAttack_Click);
            // 
            // btnEndReinforce
            // 
            this.btnEndReinforce.Location = new System.Drawing.Point(873, 318);
            this.btnEndReinforce.Margin = new System.Windows.Forms.Padding(6);
            this.btnEndReinforce.Name = "btnEndReinforce";
            this.btnEndReinforce.Size = new System.Drawing.Size(279, 42);
            this.btnEndReinforce.TabIndex = 25;
            this.btnEndReinforce.Text = "End reinforce";
            this.btnEndReinforce.UseVisualStyleBackColor = true;
            this.btnEndReinforce.Click += new System.EventHandler(this.btnEndReinforce_Click);
            // 
            // GameClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1844, 1549);
            this.Controls.Add(this.btnEndReinforce);
            this.Controls.Add(this.btnEndAttack);
            this.Controls.Add(this.btnStartSession);
            this.Controls.Add(this.btnCreateSession);
            this.Controls.Add(this.lblReinforce);
            this.Controls.Add(this.lblAttackTo);
            this.Controls.Add(this.lblAttackFrom);
            this.Controls.Add(this.cmbCurrentGames);
            this.Controls.Add(this.btnListAllSessions);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rtxPlayerStatus);
            this.Controls.Add(this.picBoard);
            this.Controls.Add(this.btnShowSessionDetails);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.txtPlayerName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPlayerId);
            this.Controls.Add(this.lblClientId);
            this.Controls.Add(this.txtSessionBaseUrl);
            this.Controls.Add(this.lblSessionUrl);
            this.Controls.Add(this.btnJoinSession);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "GameClient";
            this.Text = "GameClient";
            this.Load += new System.EventHandler(this.GameClient_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBoard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnJoinSession;
        private System.Windows.Forms.Label lblSessionUrl;
        private System.Windows.Forms.TextBox txtSessionBaseUrl;
        private System.Windows.Forms.Label lblClientId;
        private System.Windows.Forms.TextBox txtPlayerId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPlayerName;
        private System.Windows.Forms.Timer tmrPoll;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Button btnShowSessionDetails;
        private System.Windows.Forms.PictureBox picBoard;
        private System.Windows.Forms.RichTextBox rtxPlayerStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnListAllSessions;
        private System.Windows.Forms.ComboBox cmbCurrentGames;
        private System.Windows.Forms.Label lblAttackFrom;
        private System.Windows.Forms.Label lblAttackTo;
        private System.Windows.Forms.Label lblReinforce;
        private System.Windows.Forms.Button btnCreateSession;
        private System.Windows.Forms.Button btnStartSession;
        private System.Windows.Forms.Button btnEndAttack;
        private System.Windows.Forms.Button btnEndReinforce;
    }
}