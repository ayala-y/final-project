using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BLL.Algorithm
{
    public static class TempToImageAlgorithm
    {
        public static Bitmap convertToBlackAndWhite(Bitmap b)
        {
            Bitmap bitmap = new Bitmap(b);

            for (int row = 0; row < b.Width; row++) // Indicates row number
            {
                for (int column = 0; column < b.Height; column++) // Indicate column number
                {
                    var colorValue = b.GetPixel(row, column); // Get the color pixel

                    var averageValue = ((int)colorValue.R + (int)colorValue.B + (int)colorValue.G) / 3;

                    Color newColor = averageValue > 128 ? Color.White : Color.Black;
                    bitmap.SetPixel(row, column, newColor);
                }
            }
            return bitmap;
        }

        public static Bitmap disturbancesRemoval(Bitmap b)
        {
            int[] mask = new int[9];
            Color c;
            for (int ii = 0; ii < b.Width; ii++)
            {
                for (int jj = 0; jj < b.Height; jj++)
                {
                    if (ii - 1 >= 0 && jj - 1 >= 0)
                    {
                        c = b.GetPixel(ii - 1, jj - 1);
                        mask[0] = Convert.ToInt16(c.R);
                    }
                    else
                    {
                        mask[0] = 0;
                    }
                    if (jj - 1 >= 0 && ii + 1 < b.Width)
                    {
                        c = b.GetPixel(ii + 1, jj - 1);
                        mask[1] = Convert.ToInt16(c.R);
                    }
                    else
                    {
                        mask[1] = 0;
                    }
                    if (jj - 1 >= 0)
                    {
                        c = b.GetPixel(ii, jj - 1);
                        mask[2] = Convert.ToInt16(c.R);
                    }
                    else
                    {
                        mask[2] = 0;
                    }
                    if (ii + 1 < b.Width)
                    {
                        c = b.GetPixel(ii + 1, jj);
                        mask[3] = Convert.ToInt16(c.R);
                    }
                    else
                    {
                        mask[3] = 0;
                    }
                    if (ii - 1 >= 0)
                    {
                        c = b.GetPixel(ii - 1, jj);
                        mask[4] = Convert.ToInt16(c.R);
                    }
                    else
                    {
                        mask[4] = 0;
                    }
                    if (ii - 1 >= 0 && jj + 1 < b.Height)
                    {
                        c = b.GetPixel(ii - 1, jj + 1);
                        mask[5] = Convert.ToInt16(c.R);
                    }
                    else
                    {
                        mask[5] = 0;
                    }
                    if (jj + 1 < b.Height)
                    {
                        c = b.GetPixel(ii, jj + 1);
                        mask[6] = Convert.ToInt16(c.R);
                    }
                    else
                    {
                        mask[6] = 0;
                    }
                    if (ii + 1 < b.Width && jj + 1 < b.Height)
                    {
                        c = b.GetPixel(ii + 1, jj + 1);
                        mask[7] = Convert.ToInt16(c.R);
                    }
                    else
                    {
                        mask[7] = 0;
                    }
                    c = b.GetPixel(ii, jj);
                    mask[8] = Convert.ToInt16(c.R);
                    Array.Sort(mask);
                    int mid = mask[4];
                    b.SetPixel(ii, jj, Color.FromArgb(mid, mid, mid));
                }
            }
            return b;
        }
    }
}
