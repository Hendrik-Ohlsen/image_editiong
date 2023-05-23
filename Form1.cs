using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Bild_graustufen {
    public partial class Form1 : Form {
        Bitmap image;
        editing func;

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            string image_path = @"C:\Users\Hendr\OneDrive\Veröffentlichen\Instagram\Hochgeladen\IMG_4575-Bearbeitet.jpg";
            image = new Bitmap(image_path);
            func = new editing();

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.ClientSize = new Size(image.Width / 10, image.Height / 10);

            show_color_image();
        }
        private Bitmap to_sw(Bitmap input) {
            Bitmap s_w = new Bitmap(input);
            for (int i = 0; i < s_w.Width; i++) {
                for (int j = 0; j < s_w.Height; j++) {
                    s_w.SetPixel(i, j, mean_pixel(s_w.GetPixel(i, j)));
                }
            }
            return s_w;
        }
        private Color mean_pixel(Color pixel) {
            int value = (pixel.R + pixel.G + pixel.B) / 3;
            return Color.FromArgb(value, value, value);
        }

        private void change_pixel(int x, int y, BitmapData bmpData) {
            unsafe {
                byte* ptr = (byte*)bmpData.Scan0;
                int byte_per_pixel = Bitmap.GetPixelFormatSize(bmpData.PixelFormat)/8;
                int stride = bmpData.Stride;

                byte* pixel = ptr + (y * stride) + (x * byte_per_pixel);
                byte gray_scale_Value = (byte)((pixel[0] + pixel[1] + pixel[2]+ pixel[3])/3);

                pixel[0] = gray_scale_Value;
                pixel[1] = gray_scale_Value;
                pixel[2] = gray_scale_Value;
            }
        }
        private void show_color_image() {
            pictureBox1.Image = image;
            pictureBox1.Refresh();
            pictureBox1.Visible = true;
        }

        private void btnGreysacle_Click(object sender, EventArgs e) {
            pictureBox1.Image = to_sw(image);
            pictureBox1.Refresh();
            pictureBox1.Visible = true;
        }

        private void btnReset_Click(object sender, EventArgs e) {
            show_color_image();
        }

        private void btnGreyMulti_Click(object sender, EventArgs e) {
            //multi_to_sw(image);
            pictureBox1.Image = func.to_greyscale_multi(image);
            pictureBox1.Refresh();
            pictureBox1.Visible = true;
            //Test();
        }

        private void btnBrightenup_Click(object sender, EventArgs e) {
            pictureBox1.Image = func.brighten_up(image);
            pictureBox1.Refresh();
            pictureBox1.Visible = true;
        }
    }
}
