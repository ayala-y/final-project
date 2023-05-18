using Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Algorithm
{
    public class Algorithm
    {
        public static string algorithms(Bitmap img)
        {
            string whichSkirt;
            Boolean flag = false;

          Bitmap b=  TempToImageAlgorithm.convertToBlackAndWhite(img);
           Bitmap bb= TempToImageAlgorithm.disturbancesRemoval(b);
            indexInBitMapAlgorithm upLeft = SkirtAlgorithm.UpLeft(bb);
            if (upLeft != null)
            {
                indexInBitMapAlgorithm upRight = SkirtAlgorithm.UpRight(bb, upLeft);
                if (upRight != null)
                {
                    indexInBitMapAlgorithm downLeft = SkirtAlgorithm.checkLeftSlant(bb, upLeft, upRight);//s.temp2(img);
                    if (downLeft != null)
                    {
                        indexInBitMapAlgorithm downRight = SkirtAlgorithm.checkRightSlant(bb, upLeft, upRight);
                        if (downRight != null)
                        {
                            indexInBitMapAlgorithm middleOfLeftSlant = new indexInBitMapAlgorithm(0, (upLeft.j + downLeft.j) / 2);
                            indexInBitMapAlgorithm middleOfRightSlant = new indexInBitMapAlgorithm(0, (upRight.j + downRight.j) / 2);

                            middleOfLeftSlant.i = SkirtAlgorithm.iFromSpecificJForLeftSlant(bb, middleOfLeftSlant.j);
                            middleOfRightSlant.i = SkirtAlgorithm.iFromSpecificJForRightSlant(bb, middleOfRightSlant.j);


                            flag = true;
                            whichSkirt = SkirtAlgorithm.whichSkirt(middleOfLeftSlant, middleOfRightSlant, downLeft, downRight);

                            if (whichSkirt == "not a skirt")
                            {
                                return "not a skirt";
                            }
                            else
                            {

                                if (whichSkirt == "a streight skirt")
                                {
                                    return "a streight skirt";
                                }
                                else
                                {
                                    bool isASkirt = SkirtAlgorithm.isASkirt(bb, downLeft, downRight);
                                    if (isASkirt == true)
                                    {
                                        return "a A skirt";
                                    }
                                    else
                                    {
                                        whichSkirt = SkirtAlgorithm.BellOrPleatedSkirt(bb, downLeft, downRight);
                                        if (whichSkirt == "a pleated Skirt")
                                        {
                                            return "a pleated skirt";
                                        }
                                        else
                                        {
                                           return "a bell skirt";
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
            if (!flag)
            {
                return null;
            }

            return null;
        }
    }
}

