namespace Bild_graustufen {
    partial class Form1 {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent() {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnGreysacle = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnGreyMulti = new System.Windows.Forms.Button();
            this.btnBrightenup = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.getHistogram = new System.Windows.Forms.Button();
            this.txbBrightness = new System.Windows.Forms.TextBox();
            this.btnDeeps = new System.Windows.Forms.Button();
            this.btnLights = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(49, 39);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1082, 704);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnGreysacle
            // 
            this.btnGreysacle.Location = new System.Drawing.Point(1210, 183);
            this.btnGreysacle.Name = "btnGreysacle";
            this.btnGreysacle.Size = new System.Drawing.Size(128, 23);
            this.btnGreysacle.TabIndex = 1;
            this.btnGreysacle.Text = "s/w";
            this.btnGreysacle.UseVisualStyleBackColor = true;
            this.btnGreysacle.Click += new System.EventHandler(this.btnGreysacle_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(1210, 242);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(128, 23);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnGreyMulti
            // 
            this.btnGreyMulti.Location = new System.Drawing.Point(1210, 303);
            this.btnGreyMulti.Name = "btnGreyMulti";
            this.btnGreyMulti.Size = new System.Drawing.Size(128, 23);
            this.btnGreyMulti.TabIndex = 3;
            this.btnGreyMulti.Text = "s/w multithread";
            this.btnGreyMulti.UseVisualStyleBackColor = true;
            this.btnGreyMulti.Click += new System.EventHandler(this.btnGreyMulti_Click);
            // 
            // btnBrightenup
            // 
            this.btnBrightenup.Location = new System.Drawing.Point(1210, 357);
            this.btnBrightenup.Name = "btnBrightenup";
            this.btnBrightenup.Size = new System.Drawing.Size(128, 23);
            this.btnBrightenup.TabIndex = 4;
            this.btnBrightenup.Text = "heller";
            this.btnBrightenup.UseVisualStyleBackColor = true;
            this.btnBrightenup.Click += new System.EventHandler(this.btnBrightenup_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(1210, 39);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(417, 103);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // getHistogram
            // 
            this.getHistogram.Location = new System.Drawing.Point(1210, 421);
            this.getHistogram.Name = "getHistogram";
            this.getHistogram.Size = new System.Drawing.Size(128, 23);
            this.getHistogram.TabIndex = 6;
            this.getHistogram.Text = "Histogram";
            this.getHistogram.UseVisualStyleBackColor = true;
            this.getHistogram.Click += new System.EventHandler(this.getHistogram_Click);
            // 
            // txbBrightness
            // 
            this.txbBrightness.Location = new System.Drawing.Point(1373, 357);
            this.txbBrightness.Name = "txbBrightness";
            this.txbBrightness.Size = new System.Drawing.Size(100, 22);
            this.txbBrightness.TabIndex = 7;
            this.txbBrightness.Text = "25";
            // 
            // btnDeeps
            // 
            this.btnDeeps.Location = new System.Drawing.Point(1210, 477);
            this.btnDeeps.Name = "btnDeeps";
            this.btnDeeps.Size = new System.Drawing.Size(128, 23);
            this.btnDeeps.TabIndex = 8;
            this.btnDeeps.Text = "Tiefen";
            this.btnDeeps.UseVisualStyleBackColor = true;
            this.btnDeeps.Click += new System.EventHandler(this.btnDeeps_Click);
            // 
            // btnLights
            // 
            this.btnLights.Location = new System.Drawing.Point(1210, 543);
            this.btnLights.Name = "btnLights";
            this.btnLights.Size = new System.Drawing.Size(128, 23);
            this.btnLights.TabIndex = 9;
            this.btnLights.Text = "Lichter";
            this.btnLights.UseVisualStyleBackColor = true;
            this.btnLights.Click += new System.EventHandler(this.btnLights_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1713, 902);
            this.Controls.Add(this.btnLights);
            this.Controls.Add(this.btnDeeps);
            this.Controls.Add(this.txbBrightness);
            this.Controls.Add(this.getHistogram);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btnBrightenup);
            this.Controls.Add(this.btnGreyMulti);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnGreysacle);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnGreysacle;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnGreyMulti;
        private System.Windows.Forms.Button btnBrightenup;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button getHistogram;
        private System.Windows.Forms.TextBox txbBrightness;
        private System.Windows.Forms.Button btnDeeps;
        private System.Windows.Forms.Button btnLights;
    }
}

