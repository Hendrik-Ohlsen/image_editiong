using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bild_graustufen {
    internal class editing {
        object lockObject = new object();
        public Bitmap to_greyscale_multi(Bitmap input) {
            Bitmap bmp = new Bitmap(input);

            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            var data = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            var depth = Bitmap.GetPixelFormatSize(data.PixelFormat) / 8; //bytes per pixel

            var buffer = new byte[data.Width * data.Height * depth];

            //copy pixels to buffer
            Marshal.Copy(data.Scan0, buffer, 0, buffer.Length);
            var amount_cores = Environment.ProcessorCount;
            Action[] processes = new Action[amount_cores];

            for (int i = 0; i < amount_cores; i++) {
                int x = (data.Width / amount_cores) * i;
                int y = 0;
                int endx = (data.Width / amount_cores) * (i + 1);
                int endy = data.Height;
                processes[i] = () =>{
                    Process_greyscale(buffer, x, y, endx , endy, data.Width, depth);
                };
            }

            Parallel.Invoke(processes);

            //Copy the buffer back to image
            Marshal.Copy(buffer, 0, data.Scan0, buffer.Length);

            bmp.UnlockBits(data);

            return bmp;
        }
        void Process_greyscale(byte[] buffer, int x, int y, int endx, int endy, int width, int depth) {
            for (int i = x; i < endx; i++) {
                for (int j = y; j < endy; j++) {
                    var offset = ((j * width) + i) * depth;
                    var b = (buffer[offset] + buffer[offset + 1] + buffer[offset + 2]) / 3;
                    buffer[offset + 0] = buffer[offset + 1] = buffer[offset + 2] = (byte)b;
                }
            }
        }
        public Bitmap brighten_up(Bitmap input, int change) {
            Bitmap bmp = new Bitmap(input);
            var pixel = bmp.GetPixel(0, 0);

            double[,,] image_hsv = new double[input.Width, input.Height, 3];

            //var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            //var data = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            //var depth = Bitmap.GetPixelFormatSize(data.PixelFormat) / 8; //bytes per pixel

            //var buffer = new byte[data.Width * data.Height * depth];

            //copy pixels to buffer
            //Marshal.Copy(data.Scan0, buffer, 0, buffer.Length);

            for (int i = 0; i < input.Width; i++) {
                for (int j = 0; j < input.Height; j++) {
                    var temp = RgbToHsv(bmp.GetPixel(i, j).R, bmp.GetPixel(i, j).G, bmp.GetPixel(i, j).B);
                    image_hsv[i, j, 0] = temp[0];
                    image_hsv[i, j, 1] = temp[1];
                    image_hsv[i, j, 2] = temp[2];
                }
            }

            for(int i = 0; i < input.Width; i++) {
                for (int j = 0; j < input.Height; j++) {
                    image_hsv[i, j, 2] *= 1 + ((double)change / 100);
                    //image_hsv[i, j, 2] *= 1.05;
                }
            }
            for(int i = 0; i < input.Width; i++) {
                for (int j = 0; j < input.Height; j++) {
                    var Pixe = HsvToRgb(image_hsv[i, j, 0], image_hsv[i, j, 1], image_hsv[i, j, 2]);
                    bmp.SetPixel(i, j, Pixe);
                }
            }

            //Parallel.Invoke(
            //    () => {
            //        //upper-left
            //        Process_brighten_up(buffer, 0, 0, data.Width / 2, data.Height / 2, data.Width, depth, change);
            //    },
            //    () => {
            //        //lower-right
            //        Process_brighten_up(buffer, data.Width / 2, data.Height / 2, data.Width, data.Height, data.Width, depth, change);
            //    },
            //    () =>
            //    {
            //        //upper-right
            //        Process_brighten_up(buffer, data.Width / 2, 0, data.Width, data.Height / 2, data.Width, depth, change);
            //    },
            //    () =>
            //    {
            //        //lower-left
            //        Process_brighten_up(buffer, 0, data.Height / 2, data.Width, data.Height, data.Width, depth, change);
            //    }
            //);

            ////Copy the buffer back to image
            //Marshal.Copy(buffer, 0, data.Scan0, buffer.Length);

            //bmp.UnlockBits(data);

            //bmp.Save(outputFile, ImageFormat.Jpeg);
            MessageBox.Show("fertig");
            return bmp;
        }

        private Bitmap distribution_to_bitmap(int[] distribution) {
            var bitmap = new Bitmap(512, 250);
            var gfx = Graphics.FromImage(bitmap);
            gfx.Clear(Color.White);
            var black = new Pen(Color.Black, 2);

            for (int i = 0; i < distribution.Length; i++) {
                gfx.DrawLine(black, new Point(i * 2, bitmap.Height), 
                    new Point(i * 2, bitmap.Height - (int)((float)distribution[i] / (float)distribution.Max() * bitmap.Height)));
                var maxi = distribution.Max();
                var poistin = distribution[i];
                float manuel_ration = (float)poistin / (float)maxi;
                double ratio = (distribution[i] * 1 / distribution.Max()) * 100;
            }
            return bitmap;
        }
        public Bitmap create_histogram(Bitmap image) {
            int[] distribution = new int[256];
            Bitmap grey = to_greyscale_multi(image);

            var rect = new Rectangle(0, 0, grey.Width, grey.Height);
            var data = grey.LockBits(rect, ImageLockMode.ReadWrite, grey.PixelFormat);

            var depth = Bitmap.GetPixelFormatSize(data.PixelFormat) / 8; //bytes per pixel
            var buffer = new byte[data.Width * data.Height * depth];

            //copy pixels to buffer
            Marshal.Copy(data.Scan0, buffer, 0, buffer.Length);

            //to get brightness of every pixel i+=4 is the right value
            for(int i = 0; i < buffer.Length; i+=8) {
                distribution[buffer[i]] += 1;
            }
            return distribution_to_bitmap(distribution);
        }
        private double[] RgbToHsv(double r, double g, double b) {
            double[] hsv = new double[3];
            r = r / 255.0;
            g = g / 255.0;
            b = b / 255.0;
            double max = new[] { r, g, b }.Max();
            double min = new[] { r, g, b }.Min();
            double delta = max - min;
            hsv[1] = max != 0 ? delta / max : 0;
            hsv[2] = max;
            if (hsv[1] == 0) {
                return hsv;
            }
            if (r == max) {
                hsv[0] = ((g - b) / delta);
            }
            else if (g == max) {
                hsv[0] = ((b - r) / delta) + 2.0;
            }
            else if (b == max) {
                hsv[0] = ((r - g) / delta) + 4.0;
            }
            hsv[0] *= 60.0;
            if (hsv[0] < 0) {
                hsv[0] += 360.0;
            }
            return hsv;
        }
        private Color HsvToRgb(double hue, double saturation, double value) {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - (double)Math.Floor(hue / 60);

            double v = Math.Max(Math.Min(value * 255, 255), 0);
            int p = Math.Max(Math.Min(Convert.ToInt32(v * (1 - saturation)), 255), 0);
            int q = Math.Max(Math.Min(Convert.ToInt32(v * (1 - f * saturation)), 255), 0);
            int t = Math.Max(Math.Min(Convert.ToInt32(v * (1 - (1 - f) * saturation)), 255), 0);

            if(hi == 0) {
                return Color.FromArgb((int)v, t, p);
            }
            else if (hi == 1) {
                return Color.FromArgb(q, (int)v, p);
            }
            else if (hi == 2) {
                return Color.FromArgb(p, (int)v, t);
            }
            else if (hi== 3) {
                return Color.FromArgb(p, q, (int)v);
            }
            else if (hi == 4) {
                return Color.FromArgb(t, p, (int)v);
            }
            else {
                return Color.FromArgb((int)v, p, q);
            }
        }
    }
}
