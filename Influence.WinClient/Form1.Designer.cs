namespace Influence.WinClient
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
            this.btnViewSessions = new System.Windows.Forms.Button();
            this.lstOutput = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnViewSessions
            // 
            this.btnViewSessions.Location = new System.Drawing.Point(13, 13);
            this.btnViewSessions.Name = "btnViewSessions";
            this.btnViewSessions.Size = new System.Drawing.Size(89, 23);
            this.btnViewSessions.TabIndex = 0;
            this.btnViewSessions.Text = "View sessions";
            this.btnViewSessions.UseVisualStyleBackColor = true;
            this.btnViewSessions.Click += new System.EventHandler(this.btnViewSessions_Click);
            // 
            // lstOutput
            // 
            this.lstOutput.FormattingEnabled = true;
            this.lstOutput.Location = new System.Drawing.Point(13, 134);
            this.lstOutput.Name = "lstOutput";
            this.lstOutput.Size = new System.Drawing.Size(639, 277);
            this.lstOutput.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 423);
            this.Controls.Add(this.lstOutput);
            this.Controls.Add(this.btnViewSessions);
            this.Name = "Form1";
            this.Text = "Influence WinClient";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnViewSessions;
        private System.Windows.Forms.ListBox lstOutput;
    }
}

