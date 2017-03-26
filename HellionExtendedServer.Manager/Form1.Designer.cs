namespace HellionExtendedServer.Manager
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
            this.m_consoleTextBot = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // m_consoleTextBot
            // 
            this.m_consoleTextBot.Location = new System.Drawing.Point(168, 46);
            this.m_consoleTextBot.Multiline = true;
            this.m_consoleTextBot.Name = "m_consoleTextBot";
            this.m_consoleTextBot.Size = new System.Drawing.Size(488, 329);
            this.m_consoleTextBot.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 457);
            this.Controls.Add(this.m_consoleTextBot);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_consoleTextBot;
    }
}

