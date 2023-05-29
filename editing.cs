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
using static System.Net.Mime.MediaTypeNames;

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
        private struct HSV {
            public double Hue;
            public double Saturation;
            public double Value;
        }
        private HSV doubleArrToHSV(double[] doubles) {
            var HSV = new HSV();
            HSV.Hue = doubles[0];
            HSV.Saturation = doubles[1];
            HSV.Value = doubles[2];
            return HSV;
        }
        public Bitmap brighten_up(Bitmap input, int change, int threshold = 255) {
            Bitmap bmp = new Bitmap(input);
            //bmp = to_greyscale_multi(bmp);
            var pixel = bmp.GetPixel(0, 0);

            double[,,] image_hsv = new double[input.Width, input.Height, 3];

            var bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);

            int bytesPerPixel = Bitmap.GetPixelFormatSize(bmp.PixelFormat) / 8;

            int height = bmp.Height;
            int width = bmp.Width;
            int stride = bmpData.Stride;
        //divide in n parts/taks

            unsafe {
                byte* pointer = (byte*)bmpData.Scan0;

                //for (int i = 0; i < width; i++) {
                Parallel.For(0, width, i =>
                {
                    //for (int j = 0; j < height; j++) {
                    Parallel.For(0, height, j =>
                    {
                        byte* pix = pointer + j * stride + i * bytesPerPixel;
                        var temp = RgbToHsv(pix[2], pix[1], pix[0]);
                        image_hsv[i, j, 0] = temp[0];
                        image_hsv[i, j, 1] = temp[1];
                        image_hsv[i, j, 2] = temp[2];
                        //}
                    });
                });
                //}

                //for (int i = 0; i < width; i++) {
                if (threshold == 255) {
                    Parallel.For(0, width, i =>
                    {
                        //for (int j = 0; j < height; j++) {
                        Parallel.For(0, height, j =>
                        {
                            image_hsv[i, j, 2] *= 1 + ((double)change / 100);
                            //}
                        });
                    });
                    //}
                }
                else if (threshold == 25) {
                    Parallel.For(0, width, i =>
                    {
                        //for (int j = 0; j < height; j++) {
                        Parallel.For(0, height, j =>
                        {
                            if (image_hsv[i, j, 2] < 0.25) {
                                image_hsv[i, j, 2] *= 1 + ((double)change / 100);
                            }
                            //}
                        });
                    });
                }
                else if (threshold == 190) {
                    Parallel.For(0, width, i =>
                    {
                        //for (int j = 0; j < height; j++) {
                        Parallel.For(0, height, j =>
                        {
                            if (image_hsv[i, j, 2] > 0.74) {
                                image_hsv[i, j, 2] *= 1 - ((double)change / 100);
                            }
                            //}
                        });
                    });
                }
                pointer = (byte*)bmpData.Scan0;
                //for (int i = 0; i < width; i++) {
                Parallel.For(0, width, i =>
                {
                    //for (int j = 0; j < height; j++) {
                    Parallel.For(0, height, j =>
                    {
                        byte* pix = pointer + j * stride + i * bytesPerPixel;
                        var Pixe = HsvToRgb(image_hsv[i, j, 0], image_hsv[i, j, 1], image_hsv[i, j, 2]);
                        //bmp.SetPixel(i, j, Pixe);
                        pix[0] = Pixe.B;
                        pix[1] = Pixe.G;
                        pix[2] = Pixe.R;
                    });
                });
                //}

                //for(int i = 0; i < width; i++) {
                //    for (int j = 0; j < height; j++) {
                //        byte* pix = pointer + j * bmpData.Stride + i * bytesPerPixel;
                //        var fac = 1 + change / 100d * 0.114;
                //        pix[0] = (byte) (pix[0] + (1+change/100d*0.114));
                //        pix[1] = (byte) (pix[1] + (1 + change / 100d*0.587));
                //        pix[2] = (byte) (pix[2] + (1 + change / 100d * 0.299));
                //    }
                //}
                bmp.UnlockBits(bmpData);
            }
            image_hsv = null;

            MessageBox.Show("fertig");
            return bmp;
        }
        //private void process_to_hsv(int[])

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
        private int[] create_distribution(Bitmap image) {
            int[] distribution = new int[256];
            Bitmap grey = to_greyscale_multi(image);

            var rect = new Rectangle(0, 0, grey.Width, grey.Height);
            var data = grey.LockBits(rect, ImageLockMode.ReadWrite, grey.PixelFormat);

            var depth = Bitmap.GetPixelFormatSize(data.PixelFormat) / 8; //bytes per pixel
            var buffer = new byte[data.Width * data.Height * depth];

            //copy pixels to buffer
            Marshal.Copy(data.Scan0, buffer, 0, buffer.Length);

            //to get brightness of every pixel i+=4 is the right value
            for (int i = 0; i < buffer.Length; i += 8) {
                distribution[buffer[i]] += 1;
            }
            return distribution;
        }
        public Bitmap create_histogram(Bitmap image) {
            return distribution_to_bitmap(create_distribution(image));
        }
        private double find_max(double r, double g, double b) {
            var max = r;
            if(g > max) {
                max = g;
            }
            if(b > max) {
                max = b;
            }
            return max;
        }
        private int find_max(int a, int b) {
            if (a > b) {
                return a;
            }
            return b;
        }
        private double find_min(double r, double g, double b) {
            var min = r;
            if ( g < min) {
                min = g;
            }
            if (b < min) {
                min =b;
            }
            return min;
        }
        private int find_min(int a, int b) {
            if (a < b) {
                return a;
            }
            return b;
        }
        private double[] RgbToHsv(double r, double g, double b) {
            double[] hsv = new double[3];
            r /= 255.0;
            g /= 255.0;
            b /= 255.0;
            //double max_ = new[] { r, g, b }.Max();
            var max = find_max(r, g, b);
            //var ma = Math.Max(r, Math.Max(g,b));
            //double min_ = new[] { r, g, b }.Min();
            var min = find_min(r, g, b);
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

            switch (hi) {
                case 0:
                    return Color.FromArgb((int)v, t, p);

                case 1:
                    return Color.FromArgb(q, (int)v, p);

                case 2:
                    return Color.FromArgb(p, (int)v, t);

                case 3:
                    return Color.FromArgb(p, q, (int)t);

                case 4:
                    return Color.FromArgb(t, p, (int)v);

                default:
                    return Color.FromArgb((int)v, p, q);
            }
        }
        public Bitmap show_black(Bitmap bmp) {
            var input = new Bitmap(bmp);

            var rect = new Rectangle(0, 0, input.Width, input.Height);
            var data = input.LockBits(rect, ImageLockMode.ReadWrite, input.PixelFormat);

            var depth = Bitmap.GetPixelFormatSize(data.PixelFormat) / 8; //bytes per pixel
            var buffer = new byte[data.Width * data.Height * depth];

            //copy pixels to buffer
            Marshal.Copy(data.Scan0, buffer, 0, buffer.Length);

            //to get brightness of every pixel i+=4 is the right value
            for (int i = 0; i < buffer.Length; i += 4) {
                if (buffer[i] + buffer[i+1] + buffer[i+2] == 0) {
                    buffer[i] = 255;
                    buffer[i + 1] = 0;
                    buffer[i + 2] = 0;
                }
                else if (buffer[i] + buffer[i + 1] + buffer[i + 2] == 765) {
                    buffer[i] = 0;
                    buffer[i + 1] = 0;
                    buffer[i + 2] = 255;
                }
            }
            Marshal.Copy(buffer, 0, data.Scan0, buffer.Length);

            input.UnlockBits(data);
            MessageBox.Show("FERTIG");
            return input;
        }
        public Bitmap depths(Bitmap input) {
            var distribution = create_distribution(input);
            return brighten_up(input, 50, 25);
        }
        public Bitmap lights(Bitmap input) {
            return brighten_up(input, -50, 190);
        }
    }
}
