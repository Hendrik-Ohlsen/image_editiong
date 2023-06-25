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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnGreyMulti = new System.Windows.Forms.Button();
            this.btnBrightenup = new System.Windows.Forms.Button();
            this.pbHistogram = new System.Windows.Forms.PictureBox();
            this.txbBrightness = new System.Windows.Forms.TextBox();
            this.btnDeeps = new System.Windows.Forms.Button();
            this.btnLights = new System.Windows.Forms.Button();
            this.btnSharpen = new System.Windows.Forms.Button();
            this.btnContrast = new System.Windows.Forms.Button();
            this.txbContrast = new System.Windows.Forms.TextBox();
            this.btnSaturation = new System.Windows.Forms.Button();
            this.txbSaturation = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnVignetting = new System.Windows.Forms.Button();
            this.btnTint = new System.Windows.Forms.Button();
            this.trbBrightness = new System.Windows.Forms.TrackBar();
            this.trbDepths = new System.Windows.Forms.TrackBar();
            this.trbLights = new System.Windows.Forms.TrackBar();
            this.txbDepths = new System.Windows.Forms.TextBox();
            this.txbLights = new System.Windows.Forms.TextBox();
            this.txbTint = new System.Windows.Forms.TextBox();
            this.trbTint = new System.Windows.Forms.TrackBar();
            this.trbContrast = new System.Windows.Forms.TrackBar();
            this.trbSaturation = new System.Windows.Forms.TrackBar();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnChangeImage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHistogram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbDepths)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbLights)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbTint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbContrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbSaturation)).BeginInit();
            this.SuspendLayout();
            // 
            // pbImage
            // 
            this.pbImage.Location = new System.Drawing.Point(49, 39);
            this.pbImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(1083, 704);
            this.pbImage.TabIndex = 0;
            this.pbImage.TabStop = false;
            this.pbImage.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(1495, 194);
            this.btnReset.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(128, 23);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "Zurücksetzen";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnGreyMulti
            // 
            this.btnGreyMulti.Location = new System.Drawing.Point(1211, 256);
            this.btnGreyMulti.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGreyMulti.Name = "btnGreyMulti";
            this.btnGreyMulti.Size = new System.Drawing.Size(128, 23);
            this.btnGreyMulti.TabIndex = 3;
            this.btnGreyMulti.Text = "Schwarz-Weiß";
            this.btnGreyMulti.UseVisualStyleBackColor = true;
            this.btnGreyMulti.Click += new System.EventHandler(this.btnGreyMulti_Click);
            // 
            // btnBrightenup
            // 
            this.btnBrightenup.Location = new System.Drawing.Point(1211, 310);
            this.btnBrightenup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBrightenup.Name = "btnBrightenup";
            this.btnBrightenup.Size = new System.Drawing.Size(128, 23);
            this.btnBrightenup.TabIndex = 4;
            this.btnBrightenup.Text = "Helligkeit";
            this.btnBrightenup.UseVisualStyleBackColor = true;
            this.btnBrightenup.Click += new System.EventHandler(this.btnBrightenup_Click);
            // 
            // pbHistogram
            // 
            this.pbHistogram.Location = new System.Drawing.Point(1211, 39);
            this.pbHistogram.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pbHistogram.Name = "pbHistogram";
            this.pbHistogram.Size = new System.Drawing.Size(417, 103);
            this.pbHistogram.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbHistogram.TabIndex = 5;
            this.pbHistogram.TabStop = false;
            // 
            // txbBrightness
            // 
            this.txbBrightness.Location = new System.Drawing.Point(1373, 310);
            this.txbBrightness.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txbBrightness.Name = "txbBrightness";
            this.txbBrightness.Size = new System.Drawing.Size(100, 22);
            this.txbBrightness.TabIndex = 7;
            this.txbBrightness.TextChanged += new System.EventHandler(this.txbBrightness_TextChanged);
            this.txbBrightness.Enter += new System.EventHandler(this.txbBrightness_Enter);
            // 
            // btnDeeps
            // 
            this.btnDeeps.Location = new System.Drawing.Point(1211, 571);
            this.btnDeeps.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDeeps.Name = "btnDeeps";
            this.btnDeeps.Size = new System.Drawing.Size(128, 23);
            this.btnDeeps.TabIndex = 8;
            this.btnDeeps.Text = "Tiefen";
            this.btnDeeps.UseVisualStyleBackColor = true;
            this.btnDeeps.Click += new System.EventHandler(this.btnDeeps_Click);
            // 
            // btnLights
            // 
            this.btnLights.Location = new System.Drawing.Point(1211, 512);
            this.btnLights.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLights.Name = "btnLights";
            this.btnLights.Size = new System.Drawing.Size(128, 23);
            this.btnLights.TabIndex = 9;
            this.btnLights.Text = "Lichter";
            this.btnLights.UseVisualStyleBackColor = true;
            this.btnLights.Click += new System.EventHandler(this.btnLights_Click);
            // 
            // btnSharpen
            // 
            this.btnSharpen.Location = new System.Drawing.Point(1211, 720);
            this.btnSharpen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSharpen.Name = "btnSharpen";
            this.btnSharpen.Size = new System.Drawing.Size(128, 23);
            this.btnSharpen.TabIndex = 10;
            this.btnSharpen.Text = "Schärfen";
            this.btnSharpen.UseVisualStyleBackColor = true;
            this.btnSharpen.Click += new System.EventHandler(this.btnSharpen_Click);
            // 
            // btnContrast
            // 
            this.btnContrast.Location = new System.Drawing.Point(1211, 429);
            this.btnContrast.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnContrast.Name = "btnContrast";
            this.btnContrast.Size = new System.Drawing.Size(128, 23);
            this.btnContrast.TabIndex = 11;
            this.btnContrast.Text = "Kontrast";
            this.btnContrast.UseVisualStyleBackColor = true;
            this.btnContrast.Click += new System.EventHandler(this.btnContrast_Click);
            // 
            // txbContrast
            // 
            this.txbContrast.Location = new System.Drawing.Point(1373, 429);
            this.txbContrast.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txbContrast.Name = "txbContrast";
            this.txbContrast.Size = new System.Drawing.Size(100, 22);
            this.txbContrast.TabIndex = 12;
            this.txbContrast.TextChanged += new System.EventHandler(this.txbContrast_TextChanged);
            // 
            // btnSaturation
            // 
            this.btnSaturation.Location = new System.Drawing.Point(1211, 654);
            this.btnSaturation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSaturation.Name = "btnSaturation";
            this.btnSaturation.Size = new System.Drawing.Size(128, 23);
            this.btnSaturation.TabIndex = 13;
            this.btnSaturation.Text = "Sättigung";
            this.btnSaturation.UseVisualStyleBackColor = true;
            this.btnSaturation.Click += new System.EventHandler(this.btnSaturation_Click);
            // 
            // txbSaturation
            // 
            this.txbSaturation.Location = new System.Drawing.Point(1373, 654);
            this.txbSaturation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txbSaturation.Name = "txbSaturation";
            this.txbSaturation.Size = new System.Drawing.Size(100, 22);
            this.txbSaturation.TabIndex = 14;
            this.txbSaturation.TextChanged += new System.EventHandler(this.txbSaturation_TextChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(1211, 823);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(128, 23);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "Speichern";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnVignetting
            // 
            this.btnVignetting.Location = new System.Drawing.Point(1211, 759);
            this.btnVignetting.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnVignetting.Name = "btnVignetting";
            this.btnVignetting.Size = new System.Drawing.Size(128, 23);
            this.btnVignetting.TabIndex = 19;
            this.btnVignetting.Text = "Vignettierung";
            this.btnVignetting.UseVisualStyleBackColor = true;
            this.btnVignetting.Click += new System.EventHandler(this.btnVignetting_Click);
            // 
            // btnTint
            // 
            this.btnTint.Location = new System.Drawing.Point(1211, 362);
            this.btnTint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTint.Name = "btnTint";
            this.btnTint.Size = new System.Drawing.Size(128, 23);
            this.btnTint.TabIndex = 20;
            this.btnTint.Text = "Tonung";
            this.btnTint.UseVisualStyleBackColor = true;
            this.btnTint.Click += new System.EventHandler(this.btnTint_Click);
            // 
            // trbBrightness
            // 
            this.trbBrightness.Location = new System.Drawing.Point(1495, 310);
            this.trbBrightness.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trbBrightness.Maximum = 100;
            this.trbBrightness.Minimum = -100;
            this.trbBrightness.Name = "trbBrightness";
            this.trbBrightness.Size = new System.Drawing.Size(133, 56);
            this.trbBrightness.TabIndex = 21;
            this.trbBrightness.Scroll += new System.EventHandler(this.trbBrightness_Scroll);
            // 
            // trbDepths
            // 
            this.trbDepths.Location = new System.Drawing.Point(1495, 571);
            this.trbDepths.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trbDepths.Maximum = 100;
            this.trbDepths.Minimum = -100;
            this.trbDepths.Name = "trbDepths";
            this.trbDepths.Size = new System.Drawing.Size(133, 56);
            this.trbDepths.TabIndex = 22;
            this.trbDepths.Scroll += new System.EventHandler(this.trbDepths_Scroll);
            // 
            // trbLights
            // 
            this.trbLights.Location = new System.Drawing.Point(1495, 508);
            this.trbLights.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trbLights.Maximum = 100;
            this.trbLights.Minimum = -100;
            this.trbLights.Name = "trbLights";
            this.trbLights.Size = new System.Drawing.Size(133, 56);
            this.trbLights.TabIndex = 23;
            this.trbLights.Scroll += new System.EventHandler(this.trbLights_Scroll);
            // 
            // txbDepths
            // 
            this.txbDepths.Location = new System.Drawing.Point(1373, 571);
            this.txbDepths.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txbDepths.Name = "txbDepths";
            this.txbDepths.Size = new System.Drawing.Size(100, 22);
            this.txbDepths.TabIndex = 24;
            this.txbDepths.TextChanged += new System.EventHandler(this.txbDepths_TextChanged);
            // 
            // txbLights
            // 
            this.txbLights.Location = new System.Drawing.Point(1373, 512);
            this.txbLights.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txbLights.Name = "txbLights";
            this.txbLights.Size = new System.Drawing.Size(100, 22);
            this.txbLights.TabIndex = 25;
            this.txbLights.TextChanged += new System.EventHandler(this.txbLights_TextChanged);
            // 
            // txbTint
            // 
            this.txbTint.Location = new System.Drawing.Point(1373, 362);
            this.txbTint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txbTint.Name = "txbTint";
            this.txbTint.Size = new System.Drawing.Size(100, 22);
            this.txbTint.TabIndex = 26;
            this.txbTint.TextChanged += new System.EventHandler(this.txbTint_TextChanged);
            // 
            // trbTint
            // 
            this.trbTint.Location = new System.Drawing.Point(1495, 362);
            this.trbTint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trbTint.Maximum = 100;
            this.trbTint.Minimum = -100;
            this.trbTint.Name = "trbTint";
            this.trbTint.Size = new System.Drawing.Size(133, 56);
            this.trbTint.TabIndex = 27;
            this.trbTint.Scroll += new System.EventHandler(this.trbTint_Scroll);
            // 
            // trbContrast
            // 
            this.trbContrast.Location = new System.Drawing.Point(1495, 413);
            this.trbContrast.Maximum = 100;
            this.trbContrast.Minimum = -100;
            this.trbContrast.Name = "trbContrast";
            this.trbContrast.Size = new System.Drawing.Size(133, 56);
            this.trbContrast.TabIndex = 28;
            this.trbContrast.Scroll += new System.EventHandler(this.trbContrast_Scroll);
            // 
            // trbSaturation
            // 
            this.trbSaturation.Location = new System.Drawing.Point(1495, 654);
            this.trbSaturation.Maximum = 100;
            this.trbSaturation.Minimum = -100;
            this.trbSaturation.Name = "trbSaturation";
            this.trbSaturation.Size = new System.Drawing.Size(133, 56);
            this.trbSaturation.TabIndex = 29;
            this.trbSaturation.Scroll += new System.EventHandler(this.trbSaturation_Scroll);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // btnChangeImage
            // 
            this.btnChangeImage.Location = new System.Drawing.Point(1364, 194);
            this.btnChangeImage.Name = "btnChangeImage";
            this.btnChangeImage.Size = new System.Drawing.Size(109, 23);
            this.btnChangeImage.TabIndex = 30;
            this.btnChangeImage.Text = "Anderes Bild";
            this.btnChangeImage.UseVisualStyleBackColor = true;
            this.btnChangeImage.Click += new System.EventHandler(this.btnChangeImage_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1713, 930);
            this.Controls.Add(this.btnChangeImage);
            this.Controls.Add(this.trbSaturation);
            this.Controls.Add(this.trbContrast);
            this.Controls.Add(this.trbTint);
            this.Controls.Add(this.txbTint);
            this.Controls.Add(this.txbLights);
            this.Controls.Add(this.txbDepths);
            this.Controls.Add(this.trbLights);
            this.Controls.Add(this.trbDepths);
            this.Controls.Add(this.trbBrightness);
            this.Controls.Add(this.btnTint);
            this.Controls.Add(this.btnVignetting);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txbSaturation);
            this.Controls.Add(this.btnSaturation);
            this.Controls.Add(this.txbContrast);
            this.Controls.Add(this.btnContrast);
            this.Controls.Add(this.btnSharpen);
            this.Controls.Add(this.btnLights);
            this.Controls.Add(this.btnDeeps);
            this.Controls.Add(this.txbBrightness);
            this.Controls.Add(this.pbHistogram);
            this.Controls.Add(this.btnBrightenup);
            this.Controls.Add(this.btnGreyMulti);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.pbImage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Bildbearbeitung";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHistogram)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbBrightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbDepths)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbLights)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbTint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbContrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbSaturation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnGreyMulti;
        private System.Windows.Forms.Button btnBrightenup;
        private System.Windows.Forms.PictureBox pbHistogram;
        private System.Windows.Forms.TextBox txbBrightness;
        private System.Windows.Forms.Button btnDeeps;
        private System.Windows.Forms.Button btnLights;
        private System.Windows.Forms.Button btnSharpen;
        private System.Windows.Forms.Button btnContrast;
        private System.Windows.Forms.TextBox txbContrast;
        private System.Windows.Forms.Button btnSaturation;
        private System.Windows.Forms.TextBox txbSaturation;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnVignetting;
        private System.Windows.Forms.Button btnTint;
        private System.Windows.Forms.TrackBar trbBrightness;
        private System.Windows.Forms.TrackBar trbDepths;
        private System.Windows.Forms.TrackBar trbLights;
        private System.Windows.Forms.TextBox txbDepths;
        private System.Windows.Forms.TextBox txbLights;
        private System.Windows.Forms.TextBox txbTint;
        private System.Windows.Forms.TrackBar trbTint;
        private System.Windows.Forms.TrackBar trbContrast;
        private System.Windows.Forms.TrackBar trbSaturation;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnChangeImage;
    }
}

