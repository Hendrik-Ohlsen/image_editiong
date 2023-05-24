using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bild_graustufen {
    internal class editing {
        object lockObject = new object();
        public Bitmap to_greyscale_multi(Bitmap input) {

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
        public Bitmap brighten_up(Bitmap input) {
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
            for (int i = x; i < endx; i++) {
                for (int j = y; j < endy; j++) {
                    var offset = ((j * width) + i) * depth;
                    buffer[offset] += 25;
                    buffer[offset + 1] += 25;
                    buffer[offset + 2] += 25;
                }
            }
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

            var bitmap = new Bitmap(512, 250);
            var gfx = Graphics.FromImage(bitmap);
            gfx.Clear(Color.White);
            var black = new Pen(Color.Black, 2);

            for(int i = 0; i<distribution.Length; i++) {
                gfx.DrawLine(black, new Point(i *2, bitmap.Height), new Point(i *2, bitmap.Height - (int) ((float) distribution[i] / (float) distribution.Max()*bitmap.Height)));
                var maxi = distribution.Max();
                var poistin = distribution[i];
                float manuel_ration = (float) poistin / (float) maxi;
                double ratio = (distribution[i]*1 / distribution.Max()) * 100;
            }
            return bitmap;
        }
    }
}
