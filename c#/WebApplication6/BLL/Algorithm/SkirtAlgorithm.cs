using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.ComponentModel.DataAnnotations;

namespace BLL.Algorithm
{
    public static class SkirtAlgorithm
    {
        //public static int max = int.MaxValue;
        public static int max = 1000;

        public static indexInBitMapAlgorithm[] arr = new indexInBitMapAlgorithm[max];//מערך לאחסון זמני של פיקסלים בתמונה ע"מ להבדיל בין חצאית קפלים לפעמון
        public  static int size = 0;//כמות המקומות התפוסים במערך

        //אתחול המערך
        public static void initilizeGlobalArr()
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = new indexInBitMapAlgorithm();
            }
        }


        //מציאת נקודה עליון שמאל
        public static indexInBitMapAlgorithm UpLeft(Bitmap b)
        {

            for (int i = 1; i < b.Height - 1; i++)
            {
                for (int j = 1; j < (b.Width - 1) / 4; j++)
                {

                    var colorValue = b.GetPixel(j, i);
                    if (colorValue.R == 0 && colorValue.G == 0 && colorValue.B == 0)
                    {
                        indexInBitMapAlgorithm index = new indexInBitMapAlgorithm(j, i);
                        return index;
                    }

                }
            }
            return null;
        }

        //מציאת נקודה עליון שמאל משורה מסוימת
        public static int iFromSpecificJForLeftSlant(Bitmap b, float line)
        {
            for (int i = (int)line; i < b.Height - 1; i++)
            {
                for (int j = 1; j < b.Width - 1; j++)
                {

                    var colorValue = b.GetPixel(j, i);
                    if (colorValue.R == 0 && colorValue.G == 0 && colorValue.B == 0)
                    {
                        return j;
                    }

                }
            }
            return -1;
        }

        //מציאת נקודה עליון ימין משורה מסוימת
        public static int iFromSpecificJForRightSlant(Bitmap b, float line)
        {
            for (int i = (int)line; i < b.Height - 1; i++)
            {
                for (int j = b.Width - 1; j > 0; j--)
                {
                    var colorValue = b.GetPixel(j, i);
                    if (colorValue.R == 0 && colorValue.G == 0 && colorValue.B == 0)
                    {
                        return j;
                    }
                }
            }
            return -1;
        }

        //מציאת נקודה עליון ימין
        public static indexInBitMapAlgorithm UpRight(Bitmap b, indexInBitMapAlgorithm upLeft)
        {
            for (int i = 1; i < b.Height - 1; i++)
            {
                for (int j = b.Width - 1; j > b.Width / 4 * 3; j--)
                {
                    var colorValue = b.GetPixel(j, i);
                    if (colorValue.R == 0 && colorValue.G == 0 && colorValue.B == 0 && i != upLeft.i && j != upLeft.j)
                    {
                        indexInBitMapAlgorithm index = new indexInBitMapAlgorithm(j, i);
                        return index;
                    }

                }
            }
            return null;
        }

        //מציאת נקודה תחתון שמאל
        public static indexInBitMapAlgorithm checkLeftSlant(Bitmap b, indexInBitMapAlgorithm left, indexInBitMapAlgorithm right)
        {
            float x1 = left.i;
            float x2 = right.i;
            float middle = x1 + ((x2 - x1) / 2); //נקודת אמצע

            indexInBitMapAlgorithm DownLeft = checkPixelsOfLeftSide(b, left, middle);
            indexInBitMapAlgorithm prev = new indexInBitMapAlgorithm();
            if (DownLeft != null)
            {
                prev.i = DownLeft.i;
                prev.j = DownLeft.j;
            }
            else
            {
                prev = null;
            }

            while (DownLeft != null && DownLeft.j < b.Height && DownLeft.i > 0)
            {
                prev.i = DownLeft.i;
                prev.j = DownLeft.j;
                DownLeft = checkPixelsOfLeftSide(b, DownLeft, middle);
            }
            return prev;
        }

        public static indexInBitMapAlgorithm checkPixelsOfLeftSide(Bitmap b, indexInBitMapAlgorithm index, float middle)
        {
            for (int i = (int)index.j + 1; i < index.j + 15 && i < b.Height - 1; i++)
            {
                for (int j = 0; j < index.i + 5 && j < b.Width - 1; j++)
                {

                    var colorValue = b.GetPixel(j, i);
                    if (colorValue.R == 0 && colorValue.G == 0 && colorValue.B == 0)//&& index.j < middle
                    {
                        indexInBitMapAlgorithm indexInSlant = new indexInBitMapAlgorithm(j, i);
                        return indexInSlant;
                    }
                }
            }
            return null;
        }



        //מציאת נקודה תחתון ימין
        public static indexInBitMapAlgorithm checkRightSlant(Bitmap b, indexInBitMapAlgorithm left, indexInBitMapAlgorithm right)
        {
            float x1 = left.i;
            float x2 = right.i;
            float middle = x1 + ((x2 - x1) / 2); //נקודת אמצע

            indexInBitMapAlgorithm DownRight = checkPixelsOfRightSide(b, middle, right);
            indexInBitMapAlgorithm prev = new indexInBitMapAlgorithm();
            if (DownRight != null)
            {
                prev.i = DownRight.i;
                prev.j = DownRight.j;
            }
            else
            {
                prev = null;
            }

            while (DownRight != null)
            {
                prev.i = DownRight.i;
                prev.j = DownRight.j;
                DownRight = checkPixelsOfRightSide(b, middle, DownRight);
            }
            return prev;
        }

        public static indexInBitMapAlgorithm checkPixelsOfRightSide(Bitmap b, float middle, indexInBitMapAlgorithm index)
        {
            for (int i = (int)index.j + 1; i < index.j + 5 && i < b.Height; i++)
            {
                for (int j = (int)index.i + 5; j > index.i - 5 && j < b.Width; j--)
                {
                    var colorValue = b.GetPixel(j, i);
                    if (colorValue.R == 0 && colorValue.G == 0 && colorValue.B == 0) //&& index.i > middle
                    {
                        indexInBitMapAlgorithm indexInSlant = new indexInBitMapAlgorithm(j, i);
                        return indexInSlant;
                    }
                }
            }
            return null;
        }

        //פונקציה למציאת שיפוע בין 2 נקודות
        public static float findBevel(float x1, float x2, float y1, float y2)
        {
            if (x1 - x2 == 0)
            {
                return 0;
            }
            return (y1 - y2) / (x1 - x2);
        }


        //פונ למציאת b של משוואת הישר
        public static float findBOfEquasionLine(float bevel, indexInBitMapAlgorithm index)
        {
            return index.j - (bevel * index.i);
        }



        //פונקציה לבדיקת סוג חצאית
        public static string whichSkirt(indexInBitMapAlgorithm upLeft, indexInBitMapAlgorithm upRight, indexInBitMapAlgorithm downLeft, indexInBitMapAlgorithm downRight)
        {
            //בדיקה אם נקודת עליון שמאל שמאלית לנקודת עליון ימין
            //בדיקה אם נקודת תחתון שמאל שמאלית לנקודת תחתון ימין
            if (upLeft.i >= upRight.i || downLeft.i >= downRight.i)
            {
                return "not a skirt";
            }

            //bevelWaistLine-שיפוע קו המותניים
            float bevelWaistLine = findBevel(upRight.i, upLeft.i, upRight.j, upLeft.j);
            float len = downLeft.j - upLeft.j;
            if (Math.Abs(bevelWaistLine) < (1 / (len / 30)))//יש לבדוק על אחוזים ולא על מס קבוע
            {
                //bevelDownLine-שיפוע קו חצאית תחתון
                float bevelDownLine = findBevel(downRight.i, downLeft.i, downRight.j, downLeft.j);
                if (Math.Abs(bevelDownLine) < (1 / (len / 30)))//כנ"ל
                {
                    float upWidth = upRight.i - upLeft.i;
                    float downWidth = downRight.i - downLeft.i;

                    if (downWidth < upWidth * 1.05)
                    {
                        return "a streight skirt";
                    }
                    else
                    {
                        return "a A skirt";
                    }
                }
            }
            return "not a skirt";
        }



        public static indexInBitMapAlgorithm checkPartOfDownLine(Bitmap b, indexInBitMapAlgorithm downLeft, indexInBitMapAlgorithm downRight)
        {
            indexInBitMapAlgorithm upupLeft = null;
            indexInBitMapAlgorithm prev = new indexInBitMapAlgorithm();

            if (downLeft != null)
            {
                upupLeft = backNextPixelInDownLine(b, downLeft);
            }
            if (upupLeft == null)
            {
                prev = null;
            }
            else
            {
                prev.i = upupLeft.i;
                prev.j = upupLeft.j;
            }

            initilizeGlobalArr();//אתחול המערך הגלובלי

            while (upupLeft != null && upupLeft.i >= prev.i && upupLeft.i <= downRight.i)//&& upupLeft.j > 0
            {
                prev.i = upupLeft.i;
                prev.j = upupLeft.j;
                if (upupLeft != null)
                {
                    upupLeft = backNextPixelInDownLine(b, upupLeft);
                }
                if (size < arr.Length)
                {
                    arr[size].i = prev.i;
                    arr[size].j = prev.j;
                    size = size + 1;
                }
                else
                {
                    return prev;
                }
            }
            return prev;

        }



        //פןנ למציאת אלכסון אחורי לחצאית קפלים
        public static indexInBitMapAlgorithm backNextPixelInDownLine(Bitmap b, indexInBitMapAlgorithm index)
        {
            for (int i = (int)index.j; i > index.j - 3 && i > 0; i--)
            {
                for (int j = (int)index.i + 1; j < index.i + 3 && j < b.Width; j++)
                {
                    var colorValue = b.GetPixel(j, i);
                    if (colorValue.R == 0 && colorValue.G == 0 && colorValue.B == 0)
                    {
                        indexInBitMapAlgorithm backNextPixel = new indexInBitMapAlgorithm(j, i);
                        return backNextPixel;
                    }
                }
            }
            return null;
        }

        public static string probablyBellOrPleatedSkirt(Bitmap b, indexInBitMapAlgorithm start, indexInBitMapAlgorithm end)
        {
            if (start != null && end != null)
            {
                float bevel = findBevel(start.i, end.i, start.j, end.j);
                float bOfEquasionLine = findBOfEquasionLine(bevel, start);

                int count = 0;//מס הנקודות שמקיימות את משוואת הישר
                int countOfElementsInList = 0;//מס פיקסלים ברשימה
                for (int i = 0; i < size; i++)
                {
                    countOfElementsInList++;

                    indexInBitMapAlgorithm middle = arr[i];
                    if (middle.j == (bevel * middle.i) + bOfEquasionLine)
                    {
                        count++;
                    }
                }
                for (int i = 0; i < size; i++)
                {
                    arr[i] = null;
                }
                if (count / countOfElementsInList >= 0.75)
                {
                    return "probably pleated skirt";
                }
            }
            return "probably bell skirt";
        }





        public static string BellOrPleatedSkirt(Bitmap b, indexInBitMapAlgorithm downLeft, indexInBitMapAlgorithm downRight)
        {
            indexInBitMapAlgorithm prev = downLeft;
            indexInBitMapAlgorithm next = checkPartOfDownLine(b, downLeft, downRight);
            string whichSkirtProbably;
            int countProbablyPleatedSkirt = 0, countProbablyBellSkirt = 0;
            if (next != null)
            {
                while (next != null && next != downRight)
                {
                    whichSkirtProbably = probablyBellOrPleatedSkirt(b, prev, next);
                    if (whichSkirtProbably == "probably pleated skirt")
                    {
                        countProbablyPleatedSkirt++;
                    }
                    else
                    {
                        countProbablyBellSkirt++;
                    }

                    prev = checkSecondPartOfDownLine(b, next);
                    if (downRight != null)
                    {
                        next = checkPartOfDownLine(b, prev, downRight);
                    }
                }
            }

            if (countProbablyPleatedSkirt > countProbablyBellSkirt)
            {
                return "a pleated Skirt";
                ;
            }
            return "a bell Skirt";
        }



        public static indexInBitMapAlgorithm checkSecondPartOfDownLine(Bitmap b, indexInBitMapAlgorithm index)
        {
            indexInBitMapAlgorithm downdownLeft = nextPixelInDownLine(b, index);
            indexInBitMapAlgorithm prev = new indexInBitMapAlgorithm();
            if (downdownLeft == null)
            {
                prev = null;
            }
            else
            {
                prev.i = downdownLeft.i;
                prev.j = downdownLeft.j;
            }

            while (downdownLeft != null && downdownLeft.i <= prev.i)//&& upupLeft.j > 0
            {
                prev.i = downdownLeft.i;
                prev.j = downdownLeft.j;
                downdownLeft = nextPixelInDownLine(b, downdownLeft);
                if (downdownLeft == prev)
                {
                    return prev;
                }

            }
            return prev;
        }

        //פןנ למציאת אלכסון מצד ימין לחצאית קפלים
        public static indexInBitMapAlgorithm nextPixelInDownLine(Bitmap b, indexInBitMapAlgorithm index)
        {
            float prev_i = index.j + 1;
            float prev_j = index.i;
            for (int i = (int)index.j + 1; i < index.j + 3 && i < b.Height; i++)
            {
                for (int j = (int)index.i; j > index.i - 3 && j < b.Width; j--)
                {
                    var colorValue = b.GetPixel(j, i);
                    if (colorValue.R == 255 && colorValue.G == 255 && colorValue.B == 255)//מציאת נקודה לבנה ראשונה
                    {
                        indexInBitMapAlgorithm nextPixel = new indexInBitMapAlgorithm(prev_j, prev_i);//יצירת מיקום במטריצה -לפיקסל השחור הקודם לנקודה הלבנה
                        return nextPixel;
                    }
                    prev_j = j;
                }
                prev_i = i;
            }
            return null;
        }


        public static bool isASkirt(Bitmap b, indexInBitMapAlgorithm downLeft, indexInBitMapAlgorithm downRight)
        {
            float midddle_x = downLeft.i + (downRight.i - downLeft.i) / 2;//ערך נקודת האמצע
            indexInBitMapAlgorithm middle = findMiddleDownLine(b, downLeft, midddle_x);
            indexInBitMapAlgorithm prev = downLeft;
            indexInBitMapAlgorithm index = findNextPixelInDownLine(b, prev);
            while (index != null && index.i < middle.i)
            {
                prev = index;
                index = findNextPixelInDownLine(b, prev);
            }
            if (index != null)
            {
                prev = index;
                index = backNextPixelInDownLine(b, prev);
                while (index != null && index.i < downRight.i)
                {
                    prev = index;
                    index = backNextPixelInDownLine(b, prev);
                }
                if (index != null)
                {
                    return true;
                }
            }
            return false;
        }




        public static indexInBitMapAlgorithm findNextPixelInDownLine(Bitmap b, indexInBitMapAlgorithm index)
        {
            for (int i = (int)index.j + 1; i > index.j - 3 && i > 0; i--)
            {
                for (int j = (int)index.i + 1; j < index.i + 3 && j < b.Height; j++)
                {
                    var colorValue = b.GetPixel(j, i);
                    if (colorValue.R == 0 && colorValue.G == 0 && colorValue.B == 0)
                    {
                        indexInBitMapAlgorithm nextPixel = new indexInBitMapAlgorithm(j, i);
                        return nextPixel;
                    }
                }
            }
            return null;
        }

        //פונקיה למציאת נקודת אמצע של קו תחתון בחצאית פעמון קל
        public static indexInBitMapAlgorithm findMiddleDownLine(Bitmap b, indexInBitMapAlgorithm index, float middle)
        {
            for (int i = b.Height - 1; i >= index.j; i--)
            {
                for (int j = 0; j < b.Width; j++)
                {
                    var colorValue = b.GetPixel(j, i);
                    if (colorValue.R == 0 && colorValue.G == 0 && colorValue.B == 0)
                    {
                        indexInBitMapAlgorithm middleInDownLine = new indexInBitMapAlgorithm(middle, i);
                        return middleInDownLine;
                    }
                }
            }
            return null;
        }
    }
}
