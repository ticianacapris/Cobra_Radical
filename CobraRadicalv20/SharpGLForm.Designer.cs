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
            this.panel1 = new System.Windows.Forms.Panel();
            this.Btn_Parar = new System.Windows.Forms.Button();
            this.Btn_Inverter_Escala = new System.Windows.Forms.Button();
            this.Btn_Experiencias = new System.Windows.Forms.Button();
            this.Btn_Inverter = new System.Windows.Forms.Button();
            this.Btn_Diminuir = new System.Windows.Forms.Button();
            this.Btn_Aumentar = new System.Windows.Forms.Button();
            this.Btn_TX_Menos = new System.Windows.Forms.Button();
            this.Btn_TX_Mais = new System.Windows.Forms.Button();
            this.Btn_Sair = new System.Windows.Forms.Button();
            this.Btn_LerModelo = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openGLControl
            // 
            this.openGLControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.openGLControl.AutoSize = true;
            this.openGLControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.openGLControl.DrawFPS = true;
            this.openGLControl.Location = new System.Drawing.Point(0, -2);
            this.openGLControl.Margin = new System.Windows.Forms.Padding(8);
            this.openGLControl.Name = "openGLControl";
            this.openGLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl.RenderContextType = SharpGL.RenderContextType.FBO;
            this.openGLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl.Size = new System.Drawing.Size(1052, 902);
            this.openGLControl.TabIndex = 0;
            this.openGLControl.OpenGLInitialized += new System.EventHandler(this.openGLControl_OpenGLInitialized);
            this.openGLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.openGLControl_OpenGLDraw);
            this.openGLControl.Resized += new System.EventHandler(this.openGLControl_Resized);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.Btn_Parar);
            this.panel1.Controls.Add(this.Btn_Inverter_Escala);
            this.panel1.Controls.Add(this.Btn_Experiencias);
            this.panel1.Controls.Add(this.Btn_Inverter);
            this.panel1.Controls.Add(this.Btn_Diminuir);
            this.panel1.Controls.Add(this.Btn_Aumentar);
            this.panel1.Controls.Add(this.Btn_TX_Menos);
            this.panel1.Controls.Add(this.Btn_TX_Mais);
            this.panel1.Controls.Add(this.Btn_Sair);
            this.panel1.Controls.Add(this.Btn_LerModelo);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(1052, -2);
            this.panel1.Margin = new System.Windows.Forms.Padding(6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 906);
            this.panel1.TabIndex = 3;
            // 
            // Btn_Parar
            // 
            this.Btn_Parar.Location = new System.Drawing.Point(56, 325);
            this.Btn_Parar.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_Parar.Name = "Btn_Parar";
            this.Btn_Parar.Size = new System.Drawing.Size(314, 158);
            this.Btn_Parar.TabIndex = 10;
            this.Btn_Parar.Text = "STOP";
            this.Btn_Parar.UseVisualStyleBackColor = true;
            this.Btn_Parar.Click += new System.EventHandler(this.Btn_Parar_Click);
            // 
            // Btn_Inverter_Escala
            // 
            this.Btn_Inverter_Escala.Location = new System.Drawing.Point(242, 602);
            this.Btn_Inverter_Escala.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_Inverter_Escala.Name = "Btn_Inverter_Escala";
            this.Btn_Inverter_Escala.Size = new System.Drawing.Size(202, 56);
            this.Btn_Inverter_Escala.TabIndex = 9;
            this.Btn_Inverter_Escala.Text = "Inv. Escala";
            this.Btn_Inverter_Escala.UseVisualStyleBackColor = true;
            this.Btn_Inverter_Escala.Click += new System.EventHandler(this.Btn_Inverter_Escala_Click);
            // 
            // Btn_Experiencias
            // 
            this.Btn_Experiencias.Location = new System.Drawing.Point(325, 104);
            this.Btn_Experiencias.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_Experiencias.Name = "Btn_Experiencias";
            this.Btn_Experiencias.Size = new System.Drawing.Size(150, 44);
            this.Btn_Experiencias.TabIndex = 8;
            this.Btn_Experiencias.Text = "Experiencias";
            this.Btn_Experiencias.UseVisualStyleBackColor = true;
            this.Btn_Experiencias.Click += new System.EventHandler(this.Btn_Experiencias_Click);
            // 
            // Btn_Inverter
            // 
            this.Btn_Inverter.Location = new System.Drawing.Point(20, 592);
            this.Btn_Inverter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Btn_Inverter.Name = "Btn_Inverter";
            this.Btn_Inverter.Size = new System.Drawing.Size(212, 66);
            this.Btn_Inverter.TabIndex = 7;
            this.Btn_Inverter.Text = "Inverter Rotacao";
            this.Btn_Inverter.UseVisualStyleBackColor = true;
            this.Btn_Inverter.Click += new System.EventHandler(this.Btn_Inverter_Click);
            // 
            // Btn_Diminuir
            // 
            this.Btn_Diminuir.Location = new System.Drawing.Point(56, 736);
            this.Btn_Diminuir.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Btn_Diminuir.Name = "Btn_Diminuir";
            this.Btn_Diminuir.Size = new System.Drawing.Size(350, 61);
            this.Btn_Diminuir.TabIndex = 6;
            this.Btn_Diminuir.Text = "Escala -";
            this.Btn_Diminuir.UseVisualStyleBackColor = true;
            this.Btn_Diminuir.Click += new System.EventHandler(this.Btn_Diminuir_Click);
            // 
            // Btn_Aumentar
            // 
            this.Btn_Aumentar.Location = new System.Drawing.Point(56, 666);
            this.Btn_Aumentar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Btn_Aumentar.Name = "Btn_Aumentar";
            this.Btn_Aumentar.Size = new System.Drawing.Size(350, 61);
            this.Btn_Aumentar.TabIndex = 5;
            this.Btn_Aumentar.Text = "Escala +";
            this.Btn_Aumentar.UseVisualStyleBackColor = true;
            this.Btn_Aumentar.Click += new System.EventHandler(this.Btn_Aumentar_Click);
            // 
            // Btn_TX_Menos
            // 
            this.Btn_TX_Menos.Location = new System.Drawing.Point(220, 516);
            this.Btn_TX_Menos.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_TX_Menos.Name = "Btn_TX_Menos";
            this.Btn_TX_Menos.Size = new System.Drawing.Size(150, 44);
            this.Btn_TX_Menos.TabIndex = 4;
            this.Btn_TX_Menos.Text = "TX-";
            this.Btn_TX_Menos.UseVisualStyleBackColor = true;
            this.Btn_TX_Menos.Click += new System.EventHandler(this.Btn_TX_Menos_Click);
            // 
            // Btn_TX_Mais
            // 
            this.Btn_TX_Mais.Location = new System.Drawing.Point(56, 516);
            this.Btn_TX_Mais.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_TX_Mais.Name = "Btn_TX_Mais";
            this.Btn_TX_Mais.Size = new System.Drawing.Size(150, 44);
            this.Btn_TX_Mais.TabIndex = 3;
            this.Btn_TX_Mais.Text = "TX+";
            this.Btn_TX_Mais.UseVisualStyleBackColor = true;
            this.Btn_TX_Mais.Click += new System.EventHandler(this.Btn_TX_Mais_Click);
            // 
            // Btn_Sair
            // 
            this.Btn_Sair.Location = new System.Drawing.Point(20, 814);
            this.Btn_Sair.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_Sair.Name = "Btn_Sair";
            this.Btn_Sair.Size = new System.Drawing.Size(350, 69);
            this.Btn_Sair.TabIndex = 2;
            this.Btn_Sair.Text = "SAIR";
            this.Btn_Sair.UseVisualStyleBackColor = true;
            this.Btn_Sair.Click += new System.EventHandler(this.Btn_Sair_Click);
            // 
            // Btn_LerModelo
            // 
            this.Btn_LerModelo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_LerModelo.Location = new System.Drawing.Point(18, 25);
            this.Btn_LerModelo.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_LerModelo.Name = "Btn_LerModelo";
            this.Btn_LerModelo.Size = new System.Drawing.Size(457, 67);
            this.Btn_LerModelo.TabIndex = 1;
            this.Btn_LerModelo.Text = "Ler Modelo";
            this.Btn_LerModelo.UseVisualStyleBackColor = true;
            this.Btn_LerModelo.Click += new System.EventHandler(this.Btn_LerModelo_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(18, 247);
            this.button1.Margin = new System.Windows.Forms.Padding(6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 44);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SharpGLForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1542, 903);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.openGLControl);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "SharpGLForm";
            this.Text = "SharpGL Form";
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpGL.OpenGLControl openGLControl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Btn_LerModelo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Btn_Sair;
        private System.Windows.Forms.Button Btn_TX_Mais;
        private System.Windows.Forms.Button Btn_TX_Menos;
        private System.Windows.Forms.Button Btn_Aumentar;
        private System.Windows.Forms.Button Btn_Diminuir;
        private System.Windows.Forms.Button Btn_Inverter;
        private System.Windows.Forms.Button Btn_Experiencias;
        private System.Windows.Forms.Button Btn_Inverter_Escala;
        private System.Windows.Forms.Button Btn_Parar;
    }
}

