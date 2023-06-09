﻿using System;
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
using System.Drawing.Drawing2D;

namespace Bild_graustufen {
    internal class editing {
        object lockObject = new object();
        public Bitmap to_greyscale(Bitmap input) {
            Bitmap bmp = new Bitmap(input);
            var pixel = bmp.GetPixel(0, 0);


            var bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);

            int bytesPerPixel = Bitmap.GetPixelFormatSize(bmp.PixelFormat) / 8;

            int height = bmp.Height;
            int width = bmp.Width;
            int stride = bmpData.Stride;

            unsafe {
                byte* pointer = (byte*)bmpData.Scan0;

                Parallel.For(0, width, i =>
                {
                    Parallel.For(0, height, j =>
                    {
                        byte* pix = pointer + j * stride + i * bytesPerPixel;
                        var mean = (byte) ((pix[2] + pix[1] + pix[0])/3);
                        pix[0] = pix[1] = pix[2] = mean;
                    });
                });
                bmp.UnlockBits(bmpData);
                return bmp;
            }
        }
        public Bitmap brighten_up(Bitmap input, int change, int threshold = 255) {
            double threshold_norm = threshold / 255;
            var image_hsv = image_RGB_to_HSV(input);
            int height = input.Height;
            int width = input.Width;

            if (threshold == 255) {
                Parallel.For(0, width, i =>
                {
                    Parallel.For(0, height, j =>
                    {
                        image_hsv[i, j, 2] *= 1 + ((double)change / 100);
                    });
                });
            }
            else if (threshold == 25) {
                Parallel.For(0, width, i =>
                    {
                        Parallel.For(0, height, j =>
                        {
                            if (image_hsv[i, j, 2] < 0.25) {
                                image_hsv[i, j, 2] *= 1 + ((double)change / 100);
                            }
                        });
                    });
            }
            else if (threshold == 190) {
                Parallel.For(0, width, i =>
                {
                    Parallel.For(0, height, j =>
                    {
                        if (image_hsv[i, j, 2] > 0.74) {
                            image_hsv[i, j, 2] *= 1 - ((double)change / 100);
                        }
                    });
                });
            }
            var final = image_HSV_to_RGB(image_hsv, input);
            return final;
        }

        private Bitmap distribution_to_bitmap(int[] distribution) {
            var bitmap = new Bitmap(512, 250);
            var gfx = Graphics.FromImage(bitmap);
                gfx.Clear(Color.White);
            var black = new Pen(Color.Black, 2);

            for (int i = 0; i < distribution.Length; i++) {
                gfx.DrawLine(black, new Point(i * 2, bitmap.Height),
                    new Point(i * 2, bitmap.Height - (int)((float)distribution[i] / (float)distribution.Max() * bitmap.Height)));
            }
            return bitmap;
        }
        private byte[] bitmap_to_buffer(Bitmap input) {
            var rect = new Rectangle(0, 0, input.Width, input.Height);
            var data = input.LockBits(rect, ImageLockMode.ReadWrite, input.PixelFormat);

            var depth = Bitmap.GetPixelFormatSize(data.PixelFormat) / 8; //bytes per pixel
            var buffer = new byte[data.Width * data.Height * depth];

            //copy pixels to buffer
            Marshal.Copy(data.Scan0, buffer, 0, buffer.Length);
            return buffer;
        }
        private int[] create_distribution(Bitmap input) {
            int[] distribution = new int[256];
            var buffer = bitmap_to_buffer(to_greyscale(input));

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
            if (g > max) {
                max = g;
            }
            if (b > max) {
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
            if (g < min) {
                min = g;
            }
            if (b < min) {
                min = b;
            }
            return min;
        }
        private int find_min(int a, int b) {
            if (a < b) {
                return a;
            }
            return b;
        }
        private double[,,] image_RGB_to_HSV(Bitmap input) {
            Bitmap bmp = new Bitmap(input);
            var pixel = bmp.GetPixel(0, 0);

            double[,,] image_hsv = new double[input.Width, input.Height, 3];

            var bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);

            int bytesPerPixel = Bitmap.GetPixelFormatSize(bmp.PixelFormat) / 8;

            int height = bmp.Height;
            int width = bmp.Width;
            int stride = bmpData.Stride;

            unsafe {
                byte* pointer = (byte*)bmpData.Scan0;

                Parallel.For(0, width, i =>
                {
                    Parallel.For(0, height, j =>
                    {
                        byte* pix = pointer + j * stride + i * bytesPerPixel;
                        var temp = Pixel_RgbToHsv(pix[2], pix[1], pix[0]);
                        image_hsv[i, j, 0] = temp[0];
                        image_hsv[i, j, 1] = temp[1];
                        image_hsv[i, j, 2] = temp[2];
                    });
                });
                bmp.UnlockBits(bmpData);
            }

            return image_hsv;
        }
        private Bitmap image_HSV_to_RGB(double[,,] image_hsv, Bitmap input) {
            Bitmap bmp = new Bitmap(input);
            var bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);

            int bytesPerPixel = Bitmap.GetPixelFormatSize(bmp.PixelFormat) / 8;

            int height = bmp.Height;
            int width = bmp.Width;
            int stride = bmpData.Stride;

            unsafe {
                byte* pointer = (byte*)bmpData.Scan0;

                //pointer = (byte*)bmpData.Scan0;
                Parallel.For(0, width, i =>
                {
                    Parallel.For(0, height, j =>
                    {
                        byte* pix = pointer + j * stride + i * bytesPerPixel;
                        var Pixe = Pixel_HSV_to_RGB(image_hsv[i, j, 0], image_hsv[i, j, 1], image_hsv[i, j, 2]);
                        pix[0] = Pixe.B;
                        pix[1] = Pixe.G;
                        pix[2] = Pixe.R;
                    });
                });

                bmp.UnlockBits(bmpData);
            }
            return bmp;
        }
        private double[] Pixel_RgbToHsv(double r, double g, double b) {
            double[] hsv = new double[3];
            var test = Color.FromArgb(255, (int) r, (int) g, (int) b);
            r /= 255.0;
            g /= 255.0;
            b /= 255.0;
            var max = find_max(r, g, b);
            var min = find_min(r, g, b);
            double delta = max - min;
            hsv[1] = max != 0 ? delta / max : 0;
            hsv[2] = max;
            if (hsv[1] == 0) {
                return hsv;
            }
            if (r == max) {
                hsv[0] = 60* ((g - b) / delta);
            }
            else if (g == max) {
                hsv[0] = 60* (((b - r) / delta) + 2.0);
            }
            else if (b == max) {
                hsv[0] = 60 * (((r - g) / delta) + 4.0);
            }
            if (hsv[0] < 0) {
                hsv[0] += 360.0;
            }
            return hsv;
        }
        private Color Pixel_HSV_to_RGB(double hue, double saturation, double value) {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            int v = Math.Max(Math.Min(Convert.ToInt32(value * 255), 255), 0);
            int p = Math.Max(Math.Min(Convert.ToInt32(v * (1 - saturation)), 255), 0);
            int q = Math.Max(Math.Min(Convert.ToInt32(v * (1 - f * saturation)), 255), 0);
            int t = Math.Max(Math.Min(Convert.ToInt32(v * (1 - (1 - f) * saturation)), 255), 0);

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }
        public Bitmap show_black(Bitmap input) {
            var bmp = new Bitmap(input);

            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            var data = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);

            var depth = Bitmap.GetPixelFormatSize(data.PixelFormat) / 8; //bytes per pixel
            var buffer = new byte[data.Width * data.Height * depth];

            //copy pixels to buffer
            Marshal.Copy(data.Scan0, buffer, 0, buffer.Length);

            for (int i = 0; i < buffer.Length; i += 4) {
                if (buffer[i] + buffer[i + 1] + buffer[i + 2] == 0) {
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

            bmp.UnlockBits(data);
            return bmp;
        }
        public Bitmap restore_depths(Bitmap input, int change) {
            return brighten_up(input, change, 25);
        }
        public Bitmap restore_lights(Bitmap input, int change) {
            return brighten_up(input, change, 190);
        }
        public Bitmap change_contrast(Bitmap input, int contrast) {
            var image_hsv = image_RGB_to_HSV(input);
            int height = input.Height;
            int width = input.Width;
                {
                    Parallel.For(0, width, i =>
                    {
                        Parallel.For(0, height, j =>
                        {
                            if (image_hsv[i, j, 2] > 0.75) { 
                                image_hsv[i, j, 2] *= 1 + ((double)contrast / 100);
                            }
                            else if (image_hsv[i, j, 2] < 0.25) {
                                image_hsv[i, j, 2] *= 1 - ((double)contrast / 100);
                            }
                        });
                    });
                return image_HSV_to_RGB(image_hsv, input);
            }
        }
        public Bitmap change_saturation(Bitmap input, int change) {
            var image_hsv = image_RGB_to_HSV(input);
                int height = input.Height;
                int width = input.Width;
                    Parallel.For(0, width, i =>
                    {
                        Parallel.For(0, height, j =>
                         {
                            image_hsv[i, j, 1] *= 1 + ((double)change / 100);
                         });
                     });

                return image_HSV_to_RGB(image_hsv, input);
            }

        private void paint_vignette(Graphics g, Rectangle bounds) {
            Rectangle ellipsebounds = bounds;
            ellipsebounds.Offset(-ellipsebounds.X, -ellipsebounds.Y);
            double roundness = 0.75; 
            int x = ellipsebounds.Width - (int)Math.Round(roundness * ellipsebounds.Width);
            int y = ellipsebounds.Height - (int)Math.Round(roundness * ellipsebounds.Height);
            ellipsebounds.Inflate(x, y);

            using (GraphicsPath path = new GraphicsPath()) {
                path.AddEllipse(ellipsebounds);
                using (PathGradientBrush brush = new PathGradientBrush(path)) {
                    brush.WrapMode = WrapMode.Tile;
                    brush.CenterColor = Color.FromArgb(0, 0, 0, 0);
                    //brush.SurroundColors = new Color[] { Color.FromArgb(255, 0, 0, 0) };
                    brush.SurroundColors = new Color[] { Color.Black };
                    Blend blend = new Blend() {
                        Positions = new float[] { 0.0f, 0.2f, 0.4f, 0.6f, 0.8f, 1.0F },
                        Factors = new float[] { 0.0f, 0.5f, 1f , 1f, 1.0f, 1.0f }
                    };
                    brush.Blend = blend;
                    Region oldClip = g.Clip;
                    g.Clip = new Region(bounds);
                    g.FillRectangle(brush, ellipsebounds);
                    g.Clip = oldClip;
                }
            }
        }

        public Bitmap create_vignette(Bitmap b) {
            Bitmap final = new Bitmap(b);
            using (Graphics g = Graphics.FromImage(final)) {
                paint_vignette(g, new Rectangle(0, 0, final.Width, final.Height));
                return final;
            }
        }
        public Bitmap Tint(Bitmap input, int tint = 0) {
            var tint_norm = tint / 100f;
            int red, green, blue;
            red = green = blue = 0;

            var output = new Bitmap(input);
            var rect = new Rectangle(0, 0, output.Width, output.Height);
            var data = output.LockBits(rect, ImageLockMode.ReadWrite, output.PixelFormat);
            var depth = Bitmap.GetPixelFormatSize(data.PixelFormat) / 8; //bytes per pixel
            var buffer = new byte[data.Width * data.Height * depth];

            //copy pixels to buffer
            Marshal.Copy(data.Scan0, buffer, 0, buffer.Length);
            var pix = input.GetPixel(0, 0);
            var ne_pix = input.GetPixel(0, 1);
            var sth = buffer.Length/(input.Width*input.Height);

            for (int x = 0; x<buffer.Length; x+=4)
            {
                blue = buffer[x];
                green = buffer[x+1];
                red = buffer[x+2];

                    if (tint_norm > 0) {
                    //Toning to yellow
                    red = (int)(red + (255 - red) * tint_norm);
                    green = (int)(green + (255 - green) * tint_norm);

                    }
                    //Toning to blue
                    else if (tint_norm < 0) {
                    green = (int)(green + (255 - green) * Math.Abs(tint_norm));
                    blue = (int)(blue + (255 - blue) * Math.Abs(tint_norm));
                    }
                buffer[x] = (byte) blue;
                buffer[x + 1] = (byte)green;
                buffer[x+2] = (byte)red;
            }
            Marshal.Copy(buffer, 0, data.Scan0, buffer.Length);
            output.UnlockBits(data);
            return output;
            }
    }
    } 
