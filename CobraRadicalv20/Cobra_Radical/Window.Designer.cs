namespace Cobra_Radical
{
    partial class Window
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
            this.m_Timer = new System.Windows.Forms.Timer(this.components);
            this.m_RestartBtn = new System.Windows.Forms.Button();
            this.scoreLbl = new System.Windows.Forms.Label();
            this.label_highscore = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_Timer
            // 
            this.m_Timer.Interval = 300;
            this.m_Timer.Tick += new System.EventHandler(this.OnTimerTick);
            // 
            // m_RestartBtn
            // 
            this.m_RestartBtn.Enabled = false;
            this.m_RestartBtn.Location = new System.Drawing.Point(0, 0);
            this.m_RestartBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.m_RestartBtn.Name = "m_RestartBtn";
            this.m_RestartBtn.Size = new System.Drawing.Size(50, 20);
            this.m_RestartBtn.TabIndex = 0;
            this.m_RestartBtn.Text = "Restart";
            this.m_RestartBtn.UseVisualStyleBackColor = true;
            this.m_RestartBtn.Click += new System.EventHandler(this.OnRestartBtnClick);
            // 
            // scoreLbl
            // 
            this.scoreLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scoreLbl.AutoSize = true;
            this.scoreLbl.Location = new System.Drawing.Point(250, 4);
            this.scoreLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.scoreLbl.Name = "scoreLbl";
            this.scoreLbl.Size = new System.Drawing.Size(74, 13);
            this.scoreLbl.TabIndex = 1;
            this.scoreLbl.Text = "Pontuação : 0";
            this.scoreLbl.Click += new System.EventHandler(this.scoreLbl_Click);
            // 
            // label_highscore
            // 
            this.label_highscore.AutoSize = true;
            this.label_highscore.Location = new System.Drawing.Point(54, 4);
            this.label_highscore.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_highscore.Name = "label_highscore";
            this.label_highscore.Size = new System.Drawing.Size(109, 13);
            this.label_highscore.TabIndex = 2;
            this.label_highscore.Text = "Pontuação máxima: 0";
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 338);
            this.Controls.Add(this.label_highscore);
            this.Controls.Add(this.scoreLbl);
            this.Controls.Add(this.m_RestartBtn);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Window";
            this.ShowIcon = false;
            this.Text = "Snake";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer m_Timer;
        private System.Windows.Forms.Button m_RestartBtn;
        private System.Windows.Forms.Label scoreLbl;
        private System.Windows.Forms.Label label_highscore;
    }
}

