namespace SharpGL_CG_TDM
{
    partial class SharpGLForm
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
            this.openGLControl = new SharpGL.OpenGLControl();
            this.maxPontLabel = new System.Windows.Forms.Label();
            this.pontLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            this.SuspendLayout();
            // 
            // openGLControl
            // 
            this.openGLControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.openGLControl.AutoSize = true;
            this.openGLControl.BackColor = System.Drawing.Color.Transparent;
            this.openGLControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.openGLControl.DrawFPS = true;
            this.openGLControl.Location = new System.Drawing.Point(0, -2);
            this.openGLControl.Margin = new System.Windows.Forms.Padding(8);
            this.openGLControl.Name = "openGLControl";
            this.openGLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl.RenderContextType = SharpGL.RenderContextType.FBO;
            this.openGLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl.Size = new System.Drawing.Size(1548, 902);
            this.openGLControl.TabIndex = 0;
            this.openGLControl.OpenGLInitialized += new System.EventHandler(this.openGLControl_OpenGLInitialized);
            this.openGLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.openGLControl_OpenGLDraw);
            this.openGLControl.Resized += new System.EventHandler(this.openGLControl_Resized);
            // 
            // maxPontLabel
            // 
            this.maxPontLabel.AutoSize = true;
            this.maxPontLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.maxPontLabel.Location = new System.Drawing.Point(29, 32);
            this.maxPontLabel.Name = "maxPontLabel";
            this.maxPontLabel.Size = new System.Drawing.Size(201, 25);
            this.maxPontLabel.TabIndex = 12;
            this.maxPontLabel.Text = "Pontuação máxima:";
            this.maxPontLabel.Click += new System.EventHandler(this.maxPontLabel_Click);
            // 
            // pontLabel
            // 
            this.pontLabel.AutoSize = true;
            this.pontLabel.BackColor = System.Drawing.Color.Transparent;
            this.pontLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.pontLabel.Location = new System.Drawing.Point(329, 32);
            this.pontLabel.Name = "pontLabel";
            this.pontLabel.Size = new System.Drawing.Size(127, 25);
            this.pontLabel.TabIndex = 11;
            this.pontLabel.Text = "Pontuação: ";
            // 
            // SharpGLForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1542, 903);
            this.Controls.Add(this.pontLabel);
            this.Controls.Add(this.maxPontLabel);
            this.Controls.Add(this.openGLControl);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "SharpGLForm";
            this.Text = "SharpGL Form";
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpGL.OpenGLControl openGLControl;
        private System.Windows.Forms.Label maxPontLabel;
        private System.Windows.Forms.Label pontLabel;
    }
}

