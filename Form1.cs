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
        object lockObject = new object();

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            string image_path = @"C:\Users\Hendr\OneDrive\Veröffentlichen\Instagram\Hochgeladen\IMG_4575-Bearbeitet.jpg";
            image = new Bitmap(image_path);

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
        private void multi_to_sw(Bitmap input) {
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
            BitmapData bmpData = image.LockBits(rect, ImageLockMode.ReadWrite, image.PixelFormat);
            try {
                int number_threads = Environment.ProcessorCount;
                Thread[] threads = new Thread[number_threads];

                    for (int i = 0; i < number_threads; i++) {
                        threads[i] = new Thread(() =>

                        {
                            for (int y = i; y < input.Height; y += number_threads) {
                                for (int x = 0; x < input.Width; x++) {
                                    lock (lockObject) {
                                        change_pixel(x, y, bmpData);
                                    }
                                }
                            }
                        });
                        threads[i].Start();
                }
                foreach(Thread thread in threads) {
                    thread.Join();
                }
            }
            finally {
                input.UnlockBits(bmpData);
            }
            //return input;
        }
        private Bitmap Test(Bitmap input) {

            //Bitmap bmp = Bitmap.FromFile(inputFile) as Bitmap;
            Bitmap bmp = new Bitmap(input);

            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            var data = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            var depth = Bitmap.GetPixelFormatSize(data.PixelFormat) / 8; //bytes per pixel

            var buffer = new byte[data.Width * data.Height * depth];

            //copy pixels to buffer
            Marshal.Copy(data.Scan0, buffer, 0, buffer.Length);

            Parallel.Invoke(
                () => {
                    //upper-left
                    Process_greyscale(buffer, 0, 0, data.Width / 2, data.Height / 2, data.Width, depth);
                },
                () => {
                    //lower-right
                    Process_greyscale(buffer, data.Width / 2, data.Height / 2, data.Width, data.Height, data.Width, depth);
                },
                () =>
                {
                    //upper-right
                    Process_greyscale(buffer, data.Width / 2, 0, data.Width, data.Height / 2, data.Width, depth);
                },
                () =>
                {
                    //lower-left
                    Process_greyscale(buffer, 0, data.Height / 2, data.Width, data.Height, data.Width, depth);
                }
            );

            //Copy the buffer back to image
            Marshal.Copy(buffer, 0, data.Scan0, buffer.Length);

            bmp.UnlockBits(data);

            //bmp.Save(outputFile, ImageFormat.Jpeg);
            return bmp;
        }

        void Process_greyscale(byte[] buffer, int x, int y, int endx, int endy, int width, int depth) {
            for (int i = x; i < endx; i++) {
                for (int j = y; j < endy; j++) {
                    var offset = ((j * width) + i) * depth;
                    // Dummy work    
                    // To grayscale (0.2126 R + 0.7152 G + 0.0722 B)
                    //var b = 0.2126 * buffer[offset + 0] + 0.7152 * buffer[offset + 1] + 0.0722 * buffer[offset + 2];
                    var b = (buffer[offset] + buffer[offset + 1] + buffer[offset + 2]) / 3;
                    buffer[offset + 0] = buffer[offset + 1] = buffer[offset + 2] = (byte)b;
                }
            }
        }
        private Bitmap brighten_up(Bitmap input) {
            Bitmap bmp = new Bitmap(input);

            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            var data = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            var depth = Bitmap.GetPixelFormatSize(data.PixelFormat) / 8; //bytes per pixel

            var buffer = new byte[data.Width * data.Height * depth];

            //copy pixels to buffer
            Marshal.Copy(data.Scan0, buffer, 0, buffer.Length);

            Parallel.Invoke(
                () => {
                    //upper-left
                    Process_brighten_up(buffer, 0, 0, data.Width / 2, data.Height / 2, data.Width, depth);
                },
                () => {
                    //lower-right
                    Process_brighten_up(buffer, data.Width / 2, data.Height / 2, data.Width, data.Height, data.Width, depth);
                },
                () =>
                {
                    //upper-right
                    Process_brighten_up(buffer, data.Width / 2, 0, data.Width, data.Height / 2, data.Width, depth);
                },
                () =>
                {
                    //lower-left
                    Process_brighten_up(buffer, 0, data.Height / 2, data.Width, data.Height, data.Width, depth);
                }
            );

            //Copy the buffer back to image
            Marshal.Copy(buffer, 0, data.Scan0, buffer.Length);

            bmp.UnlockBits(data);

            //bmp.Save(outputFile, ImageFormat.Jpeg);
            return bmp;
        }

        void Process_brighten_up(byte[] buffer, int x, int y, int endx, int endy, int width, int depth) {
            for(int i = x; i < endx; i++) {
                for(int j = y; j < endy; j++) {
                    var offset = ((j * width)+ i) * depth;
                    buffer[offset] += 25;
                    buffer[offset + 1] += 25;
                    buffer[offset + 2] += 25;
                }
            }
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
            pictureBox1.Image = Test(image);
            pictureBox1.Refresh();
            pictureBox1.Visible = true;
            //Test();
        }

        private void btnBrightenup_Click(object sender, EventArgs e) {
            pictureBox1.Image = brighten_up(image);
            pictureBox1.Refresh();
            pictureBox1.Visible = true;
        }
    }
}
