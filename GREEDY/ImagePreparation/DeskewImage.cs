using System;
using System.Drawing;
using System.Linq;

namespace GREEDY.ImagePreparation
{
    public class DeskewImage
    {
        // Representation of a line in the image.  
        private struct HougLine
        {
            public int Count;
            public int Index;
            public double Alpha;
        }

        // The range of angles to search for lines
        private const double ALPHA_START = -20;
        private const double ALPHA_STEP = 0.2;
        private const int STEPS = 40 * 5;
        private const double STEP = 1;

        private double[] _sinA;
        private double[] _cosA;
        private double _min;
        private int _count;
        private int[] _hMatrix;

        Bitmap _internalBmp;

        public Bitmap Deskew(Bitmap image)
        {
            _internalBmp = new Bitmap(image);
            return RotateImage(image, -(float)GetSkewAngle());
        }

        private void Init()
        {
            // Precalculation of sin and cos.
            _cosA = new double[STEPS];
            _sinA = new double[STEPS];

            for (int i = 0; i < STEPS; i++)
            {
                double angle = GetAlpha(i) * Math.PI / 180.0;
                _sinA[i] = Math.Sin(angle);
                _cosA[i] = Math.Cos(angle);
            }
            _min = -_internalBmp.Width;
            _count = (int)(2 * (_internalBmp.Width + _internalBmp.Height) / STEP);
            _hMatrix = new int[_count * STEPS];
        }

        // Calculate all lines through the point (x,y)
        private void Calc(int x, int y)
        {
            for (int alpha = 0; alpha < STEPS; alpha++)
            {
                double d = y * _cosA[alpha] - x * _sinA[alpha];
                int index = Convert.ToInt32(d - _min);
                index = index * STEPS + alpha;
                try
                {
                    _hMatrix[index]++;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }
        }

        private Bitmap RotateImage(Bitmap bmp, float angle)
        {
            Bitmap rotatedImage = new Bitmap(bmp.Width, bmp.Height);
            using (Graphics g = Graphics.FromImage(rotatedImage))
            {
                g.TranslateTransform(bmp.Width / 2, bmp.Height / 2);
                g.RotateTransform(angle);
                g.TranslateTransform(-bmp.Width / 2, -bmp.Height / 2);
                g.DrawImage(bmp, new Point(0, 0));
            }
            return rotatedImage;
        }

        // Calculate the skew angle of the image cBmp.
        private double GetSkewAngle()
        {
            Init();
            // Hough Transformation
            Calc();
            int top = 20;
            HougLine[] hl = GetTop(top);
            return hl.Sum(x => x.Alpha) / top;
        }

        // Calculate the Count lines in the image with most points.
        private HougLine[] GetTop(int count)
        {
            HougLine[] hl = new HougLine[count];
            for (int i = 0; i < _hMatrix.Length; i++)
            {
                if (_hMatrix[i] > hl[count - 1].Count)
                {
                    hl[count - 1].Count = _hMatrix[i];
                    hl[count - 1].Index = i;
                    int j = count - 1;
                    while (j > 0 && hl[j].Count > hl[j - 1].Count)
                    {
                        HougLine tmp = hl[j];
                        hl[j] = hl[j - 1];
                        hl[j - 1] = tmp;
                        j--;
                    }
                }
            }

            for (int i = 0; i < count; i++)
            {
                int dIndex = hl[i].Index / STEPS;
                int alphaIndex = hl[i].Index - dIndex * STEPS;
                hl[i].Alpha = GetAlpha(alphaIndex);
            }
            return hl;
        }

        // Hough Transforamtion:
        private void Calc()
        {
            int hMin = _internalBmp.Height / 4;
            int hMax = _internalBmp.Height * 3 / 4;

            for (int y = hMin; y <= hMax; y++)
            {
                for (int x = 1; x < _internalBmp.Width - 1; x++)
                {
                    if (IsBlack(x, y) && !IsBlack(x, y + 1))
                    {
                        Calc(x, y);
                    }
                }
            }
        }

        private bool IsBlack(int x, int y)
        {
            Color c = _internalBmp.GetPixel(x, y);
            double luminance = (c.R * 0.299) + (c.G * 0.587) + (c.B * 0.114);
            return luminance < 140;
        }

        private static double GetAlpha(int index)
        {
            return ALPHA_START + index * ALPHA_STEP;
        }
    }
}