using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Bild_graustufen {
    internal class sharpening {
        editing util = new editing();

        public Bitmap sharpe(Bitmap input) {
            var grey = util.to_greyscale_multi(input);
            var der_right = derivative_to_right(grey);
            var der_under = derivative_under(grey);
            var der_mean = mean(der_under, der_right);
            var edges = thresholding(der_mean);
            return derivate_visual(edges);
        }
        private int[,] derivative_to_right(Bitmap input) {
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
            int[,] derivative = new int[width, height];

            unsafe {
                byte* pointer = (byte*)bmpData.Scan0;

                Parallel.For(0, width, i =>
                {
                    Parallel.For(0, height, j =>
                    {
                        byte* pix = pointer + j * stride + i * bytesPerPixel;
                        byte* pix_next = pointer + j * stride + i +1 * bytesPerPixel;
                        derivative[i, j] = pix_next[0] - pix[0];
                        //}
                    });
                });
                return derivative;
            }
        }
        private int[,] derivative_under(Bitmap input) {
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
            int[,] derivative = new int[width, height];

            unsafe {
                byte* pointer = (byte*)bmpData.Scan0;

                Parallel.For(0, width, i =>
                {
                    Parallel.For(0, height, j =>
                    {
                        byte* pix = pointer + j * stride + i * bytesPerPixel;
                        byte* pix_next = pointer + j + 1 * stride + i * bytesPerPixel;
                        derivative[i, j] = pix_next[0] - pix[0];
                        //}
                    });
                });
                return derivative;
            }
        }
        private int[,] mean(int[,] der_1,  int[,] der_2) {
            int[,] mean_der = new int[der_1.GetLength(0), der_2.GetLength(1)];
            Parallel.For(0, der_1.GetLength(0), i =>
            {
                Parallel.For(0, der_1.GetLength(1), j =>
                {
                    mean_der[i, j] = Math.Max(Math.Min(((der_1[i, j] + der_2[i, j]) / 2),255), 0);
                });
            });
            return mean_der;
        }
        private int[,] thresholding(int[,] derivative, int threshold = 128) {
            int[,] edges = new int[derivative.GetLength(0), derivative.GetLength(1)];
            Parallel.For(0, derivative.GetLength(0), i =>
            {
                Parallel.For(0, derivative.GetLength(1), j =>
                {
                    edges[i, j] = (derivative[i, j]> threshold) ? derivative[i, j] : 0;
                });
            });
            return edges;
        }
        private Bitmap derivate_visual(int[,] derivative) {
            var bmp_derivate = new Bitmap(derivative.GetLength(0), derivative.GetLength(1));
            for(int i = 0; i< derivative.GetLength(0)-1; i++)
            {
                for(int j = 0; j< derivative.GetLength(1)-1; j++)
                {
                    bmp_derivate.SetPixel(i, j, Color.FromArgb(Math.Abs(derivative[i, j]), Math.Abs(derivative[i, j]), Math.Abs(derivative[i, j])));
                }
            }
            return bmp_derivate;
        }
    }
}
