﻿namespace Bild_graustufen {
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.btnGreysacle.Location = new System.Drawing.Point(1214, 39);
            this.btnGreysacle.Name = "btnGreysacle";
            this.btnGreysacle.Size = new System.Drawing.Size(128, 23);
            this.btnGreysacle.TabIndex = 1;
            this.btnGreysacle.Text = "s/w";
            this.btnGreysacle.UseVisualStyleBackColor = true;
            this.btnGreysacle.Click += new System.EventHandler(this.btnGreysacle_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(1214, 98);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(128, 23);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnGreyMulti
            // 
            this.btnGreyMulti.Location = new System.Drawing.Point(1214, 159);
            this.btnGreyMulti.Name = "btnGreyMulti";
            this.btnGreyMulti.Size = new System.Drawing.Size(128, 23);
            this.btnGreyMulti.TabIndex = 3;
            this.btnGreyMulti.Text = "s/w multithread";
            this.btnGreyMulti.UseVisualStyleBackColor = true;
            this.btnGreyMulti.Click += new System.EventHandler(this.btnGreyMulti_Click);
            // 
            // btnBrightenup
            // 
            this.btnBrightenup.Location = new System.Drawing.Point(1214, 213);
            this.btnBrightenup.Name = "btnBrightenup";
            this.btnBrightenup.Size = new System.Drawing.Size(128, 23);
            this.btnBrightenup.TabIndex = 4;
            this.btnBrightenup.Text = "heller";
            this.btnBrightenup.UseVisualStyleBackColor = true;
            this.btnBrightenup.Click += new System.EventHandler(this.btnBrightenup_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1459, 845);
            this.Controls.Add(this.btnBrightenup);
            this.Controls.Add(this.btnGreyMulti);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnGreysacle);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnGreysacle;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnGreyMulti;
        private System.Windows.Forms.Button btnBrightenup;
    }
}
