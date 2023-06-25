using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Bild_graustufen {
    public partial class Form1 : Form {
        Bitmap image;
        editing func;
        string image_path;

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            image = get_file();
            func = new editing();

            //pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
            //var max_widt = Width / 2;
            //pbImage.ClientSize = new Size(image.Width / 10, image.Height / 10);
            //pbImage.ClientSize = new Size(max_widt, Height / 2);

            change_image(pbImage, image);
            change_image(pbHistogram, func.create_histogram(image), PictureBoxSizeMode.StretchImage);
            //txbFileName.Text = image_path;
        }
        private Bitmap get_file() {
            var codecs = ImageCodecInfo.GetImageEncoders();
            var codecFilter = "Image Files|";
            foreach (var codec in codecs) {
                codecFilter += codec.FilenameExtension + ";";
            }
            openFileDialog.Filter = codecFilter;

            openFileDialog.Title = string.Empty;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFileDialog.ShowDialog();
            try {
                image_path = openFileDialog.FileName;
                return new Bitmap(openFileDialog.FileName);
            }
            catch {
                MessageBox.Show("Keine Datei ausgewählt!");
                get_file();
            }
            return new Bitmap(openFileDialog.FileName);
        }
        private bool save_file(Bitmap bmp) {
            saveFileDialog1.Filter = "JPEG | .jpeg";
            saveFileDialog1.Title = string.Empty;
            saveFileDialog1.InitialDirectory = image_path;
            saveFileDialog1.ShowDialog();
            try {
                bmp.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
                return true;
            }
            catch {
                MessageBox.Show("Fehler beim Speichern");
                save_file(bmp);
            }
            return true;
        }
        private void change_value_bar(TrackBar trb, int value) {
            try {
                trb.Value = value;
            }
            catch {
                MessageBox.Show("Der Wert liegt außerhalb des zugelassenen Bereiches. 0 Wurde übergeben");
                trb.Value = 0;
            }
        }
        private int check_input(TextBox txbobx) {
            if (txbobx.Text == "" || txbobx.Text =="-") {
                return 0;
            }
            try {
                return Convert.ToInt32(txbobx.Text);
            }
            catch {
                MessageBox.Show("Der eingegbene Wert ist keine Zahl gültig. " +
                    "Es wurde 0 übergeben.");
                txbobx.Text = 0.ToString();
                return 0;
            }
        }
        private void change_image(PictureBox pictureBox, Bitmap image, PictureBoxSizeMode pbsm = PictureBoxSizeMode.Zoom) {
            pictureBox.SizeMode = pbsm;
            //pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Image = image;
            pictureBox.Refresh();
            pictureBox.Visible = true;
        }
        private void btnReset_Click(object sender, EventArgs e) {
            change_image(pbImage, image = new Bitmap(image_path));
            change_image(pbHistogram, func.create_histogram(image), PictureBoxSizeMode.StretchImage);
        }
        private void btnGreyMulti_Click(object sender, EventArgs e) {
            change_image(pbImage, image = func.to_greyscale(image));
        }

        private void btnBrightenup_Click(object sender, EventArgs e) {
            change_image(pbImage, image = func.brighten_up(image, trbBrightness.Value));
            change_image(pbHistogram, func.create_histogram(image), PictureBoxSizeMode.StretchImage);
        }

        private void btnDeeps_Click(object sender, EventArgs e) {
            change_image(pbImage, image = func.restore_depths(image, trbDepths.Value));
            change_image(pbHistogram, func.create_histogram(image), PictureBoxSizeMode.StretchImage);
        }

        private void btnLights_Click(object sender, EventArgs e) {
            change_image(pbImage, image = func.restore_lights(image, trbLights.Value));
            change_image(pbHistogram, func.create_histogram(image), PictureBoxSizeMode.StretchImage);
        }

        private void btnSharpen_Click(object sender, EventArgs e) {
            var sharp_util = new sharpening();
            change_image(pbImage, image = sharp_util.Sharpen(image));
        }

        private void btnContrast_Click(object sender, EventArgs e) {
            change_image(pbImage,image = func.change_contrast(image, trbContrast.Value));
            change_image(pbHistogram, func.create_histogram(image), PictureBoxSizeMode.StretchImage);
        }

        private void btnSaturation_Click(object sender, EventArgs e) {
            change_image(pbImage, image = func.change_saturation(image, trbSaturation.Value));
            change_image(pbHistogram, func.create_histogram(image), PictureBoxSizeMode.StretchImage);
        }

        private void btnSave_Click(object sender, EventArgs e) {
            save_file(image);
        }
        private void btnVignetting_Click(object sender, EventArgs e) {
            change_image(pbImage, image = func.create_vignette(image));
            change_image(pbHistogram, func.create_histogram(image), PictureBoxSizeMode.StretchImage);
        }

        private void btnTint_Click(object sender, EventArgs e) {
            change_image(pbImage, image = func.Tint(image, trbTint.Value));
        }

        private void trbBrightness_Scroll(object sender, EventArgs e) {
            txbBrightness.Text = trbBrightness.Value.ToString();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e) {
            change_image(pbHistogram, func.create_histogram(image), PictureBoxSizeMode.StretchImage);
        }

        private void trbDepths_Scroll(object sender, EventArgs e) {
            txbDepths.Text = trbDepths.Value.ToString();
        }
        private void trbLights_Scroll(object sender, EventArgs e) {
            txbLights.Text = trbLights.Value.ToString();
        }

        private void txbBrightness_TextChanged(object sender, EventArgs e) {
            change_value_bar(trbBrightness, check_input(txbBrightness));
        }

        private void txbBrightness_Enter(object sender, EventArgs e) {
            change_image(pbImage, func.brighten_up(image, trbBrightness.Value));
        }

        private void txbDepths_TextChanged(object sender, EventArgs e) {
            change_value_bar(trbDepths, check_input(txbDepths));
        }

        private void txbLights_TextChanged(object sender, EventArgs e) {
            change_value_bar(trbLights, check_input(txbLights));
        }

        private void trbTint_Scroll(object sender, EventArgs e) {
            txbTint.Text = trbTint.Value.ToString();
        }

        private void txbTint_TextChanged(object sender, EventArgs e) {
            change_value_bar(trbTint, check_input(txbTint));
        }

        private void trbContrast_Scroll(object sender, EventArgs e) {
            txbContrast.Text = trbContrast.Value.ToString();
        }

        private void txbContrast_TextChanged(object sender, EventArgs e) {
            change_value_bar(trbContrast, check_input(txbContrast));
        }

        private void trbSaturation_Scroll(object sender, EventArgs e) {
            txbSaturation.Text = trbSaturation.Value.ToString();
        }

        private void txbSaturation_TextChanged(object sender, EventArgs e) {
            change_value_bar(trbSaturation, check_input(txbSaturation));
        }

        private void btnChangeImage_Click(object sender, EventArgs e) {
            change_image(pbImage, image = get_file());
            change_image(pbHistogram, func.create_histogram(image), PictureBoxSizeMode.StretchImage);
        }
    }
}