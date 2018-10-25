namespace Influence.SampleBot
{
    partial class Form1
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
            this.btnCreateGame = new System.Windows.Forms.Button();
            this.btnAddTwoBots = new System.Windows.Forms.Button();
            this.btnStartGame = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnStep = new System.Windows.Forms.Button();
            this.chkAutostep = new System.Windows.Forms.CheckBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.lblStepDelayMs = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCreateGame
            // 
            this.btnCreateGame.Location = new System.Drawing.Point(13, 13);
            this.btnCreateGame.Name = "btnCreateGame";
            this.btnCreateGame.Size = new System.Drawing.Size(86, 23);
            this.btnCreateGame.TabIndex = 0;
            this.btnCreateGame.Text = "Create game";
            this.btnCreateGame.UseVisualStyleBackColor = true;
            this.btnCreateGame.Click += new System.EventHandler(this.btnCreateGame_Click);
            // 
            // btnAddTwoBots
            // 
            this.btnAddTwoBots.Location = new System.Drawing.Point(105, 13);
            this.btnAddTwoBots.Name = "btnAddTwoBots";
            this.btnAddTwoBots.Size = new System.Drawing.Size(86, 23);
            this.btnAddTwoBots.TabIndex = 1;
            this.btnAddTwoBots.Text = "Add 2 bots";
            this.btnAddTwoBots.UseVisualStyleBackColor = true;
            this.btnAddTwoBots.Click += new System.EventHandler(this.btnAddTwoBots_Click);
            // 
            // btnStartGame
            // 
            this.btnStartGame.Location = new System.Drawing.Point(200, 13);
            this.btnStartGame.Name = "btnStartGame";
            this.btnStartGame.Size = new System.Drawing.Size(98, 23);
            this.btnStartGame.TabIndex = 2;
            this.btnStartGame.Text = "Start / new game";
            this.btnStartGame.UseVisualStyleBackColor = true;
            this.btnStartGame.Click += new System.EventHandler(this.btnStartGame_Click);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(13, 53);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(775, 338);
            this.txtLog.TabIndex = 3;
            // 
            // btnStep
            // 
            this.btnStep.Location = new System.Drawing.Point(335, 12);
            this.btnStep.Name = "btnStep";
            this.btnStep.Size = new System.Drawing.Size(86, 23);
            this.btnStep.TabIndex = 4;
            this.btnStep.Text = "Next step";
            this.btnStep.UseVisualStyleBackColor = true;
            this.btnStep.Click += new System.EventHandler(this.btnStep_Click);
            // 
            // chkAutostep
            // 
            this.chkAutostep.AutoSize = true;
            this.chkAutostep.Location = new System.Drawing.Point(428, 16);
            this.chkAutostep.Name = "chkAutostep";
            this.chkAutostep.Size = new System.Drawing.Size(71, 17);
            this.chkAutostep.TabIndex = 5;
            this.chkAutostep.Text = "Auto step";
            this.chkAutostep.UseVisualStyleBackColor = true;
            this.chkAutostep.CheckedChanged += new System.EventHandler(this.chkAutostep_CheckedChanged);
            // 
            // trackBar1
            // 
            this.trackBar1.LargeChange = 50;
            this.trackBar1.Location = new System.Drawing.Point(509, 7);
            this.trackBar1.Maximum = 3000;
            this.trackBar1.Minimum = 50;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(104, 45);
            this.trackBar1.SmallChange = 50;
            this.trackBar1.TabIndex = 6;
            this.trackBar1.TickFrequency = 50;
            this.trackBar1.Value = 100;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // lblStepDelayMs
            // 
            this.lblStepDelayMs.AutoSize = true;
            this.lblStepDelayMs.Location = new System.Drawing.Point(620, 17);
            this.lblStepDelayMs.Name = "lblStepDelayMs";
            this.lblStepDelayMs.Size = new System.Drawing.Size(0, 13);
            this.lblStepDelayMs.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 403);
            this.Controls.Add(this.lblStepDelayMs);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.chkAutostep);
            this.Controls.Add(this.btnStep);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnStartGame);
            this.Controls.Add(this.btnAddTwoBots);
            this.Controls.Add(this.btnCreateGame);
            this.Name = "Form1";
            this.Text = "Influence - sample bot fight";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreateGame;
        private System.Windows.Forms.Button btnAddTwoBots;
        private System.Windows.Forms.Button btnStartGame;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnStep;
        private System.Windows.Forms.CheckBox chkAutostep;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label lblStepDelayMs;
    }
}

