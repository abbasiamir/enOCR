using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace enOCR
{
    public class line
    {
        int wordcounter = 1;
        public List<word> words = new List<word>();
        public rectXY bound = new rectXY();
        public Bitmap img;
        
        public void findwords()
        {
            String Mode = "black";
            int xbegin = 0;
            
            int ybegin = 0;
            int yend = 0;
            bool all = true;

            for (int i = this.bound.x1; i <= this.bound.x2; ++i)
            {
                all = true;

                for (int j = this.bound.y1; j <= this.bound.y2; ++j)
                {

                    if (Mode == "black" &&info.checkdot(i,j,setting.RGB,img)/* (img.GetPixel(i, j).R <= setting.RGB || img.GetPixel(i, j).G <= setting.RGB || img.GetPixel(i, j).B <= setting.RGB*/)
                    {
                        xbegin = i;
                        Mode = "white";
                        all = false;
                        break;
                    }

                    if (Mode == "white" &&info.checkdot(i,j,setting.RGB,img)/* (img.GetPixel(i, j).R <= setting.RGB || img.GetPixel(i, j).G <= setting.RGB || img.GetPixel(i, j).B <= setting.RGB)*/)
                    {
                        all = false;
                    }
                }

                if (Mode == "white" && all)
                {
                    int xend = i + 1;
                    //int xend = bound.x2;
                    Mode = "black";
                    bool all2 = true;
                    bool first = true;

                    for (int k = this.bound.y1; k <= this.bound.y2; ++k)
                    {
                        all2 = true;

                        for (int m = xbegin; m < xend; ++m)
                        {
                            if (Mode == "black" &&info.checkdot(m,k,setting.RGB,img))
                            {
                                if (first)
                                {
                                    ybegin = k;
                                    first = false;
                                }

                                Mode = "white";
                                all2 = false;
                                break;
                            }

                            if (Mode == "white" &&info.checkdot(m,k,setting.RGB,img))
                            {
                                all2 = false;
                            }
                        }

                        if (Mode == "white" && all2)
                        {
                            yend = k - 1;
                            Mode = "black";
                        }

                        if (Mode == "white" && k == this.bound.y2)
                        {
                            yend = k;
                        }
                    }

                    this.words.Add(new word(new rectXY(xbegin, ybegin, xend, yend)));
                    //Bitmap boresh = info.makeSubImage( xbegin,xend, ybegin, yend, img);
                    
                   // boresh.Save(Application.StartupPath + "\\boreshes\\boresh" + wordcounter++.ToString() + ".jpg");
                    Mode = "black";
                }
            }

        }

        public line(rectXY b,Bitmap imag)
        {
            this.bound = b;
            img = imag;
        }
    }

}

   
