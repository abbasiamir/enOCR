using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace enOCR
{

    //
    // Source code recreated from a .class file by IntelliJ IDEA
    // (powered by FernFlower decompiler)
    //



    public class Pattern
    {
        String Name = "";
        //Setting setting = new Setting();
        public Color[][] pixels;
        public rectXY bound = new rectXY();

        public List<List<bool>> bPattern=new List<List<bool>>();
        public Bitmap pic;//= info.pics[info.currentPic];


        public Pattern(Bitmap b)
        {

          pic= b;
            this.make();
        }

        public void make()
        {
            // this.pixels = new pixels[pic.Width][pic.Height];
            //this.makecolorset();
            this.bound = this.makerect();
            MakebPattern();
        }

        public void MakebPattern()
        {
            rectXY from = this.makerect();
            //bool[] dim = new bool[from.x2 - from.x1 + 1]; 
            //this.bPattern. =dim[from.y2 - from.y1 + 1];
           
            for (int i =0; i <pic.Width; ++i)
            {
                List<bool> list = new List<bool>();
                for (int j =0; j <pic.Height; ++j)
                {
                   
                    if ((double)pic.GetPixel(i,j).R < setting.ptolerance && (double)pic.GetPixel(i,j).G< setting.ptolerance && (double)pic.GetPixel(i,j).B < setting.ptolerance)
                    {
                        list.Add(true);
                        //this.bPattern.Add(list);

                       
                    }
                    else
                    {
                        //this.bPattern[i - from.x1][j - from.y1] = true;
                        list.Add(false);
                        
                    }
                }
                this.bPattern.Add(list);
            }

        }

        //void makecolorset()
        //{
        //    int x = 0;
        //    int y = 0;
        //    int[] ar = new int[this.bufi.getWidth() * this.bufi.getHeight() * 3];
        //    int[] colorsettemp = this.bufi.getData().getPixels(0, 0, this.bufi.getWidth(), this.bufi.getHeight(), ar);
        //    int lenght = colorsettemp.length;
        //    int i = 0;

        //    while (i < lenght)
        //    {
        //        this.pixels[x][y] = new pixelcolor();
        //        this.pixels[x][y].R = colorsettemp[i++];
        //        this.pixels[x][y].G = colorsettemp[i++];
        //        this.pixels[x][y].B = colorsettemp[i++];
        //        ++x;
        //        if (x == this.bufi.getWidth())
        //        {
        //            x = 0;
        //            ++y;
        //        }
        //    }

        //}

        rectXY makerect()
        {
            int beginx = 0;
            //int endx = false;
            int beginy = 0;
           // int endy = false;
            String Mod = "black";
            bool all = true;
            int I = pic.Width;// this.bufi.getWidth();
            int J = pic.Height;// this.bufi.getHeight();
            int endx = I - 1;
            int endy = J - 1;
            bool first = true;

            int j;
            int i;
            for (j = 0; j < I; ++j)
            {
                all = true;

                for (i = 0; i < J; ++i)
                {
                    if (Mod == "black" && (!((double)pic.GetPixel(j,i).R> setting.ptolerance) || !((double)pic.GetPixel(j,i).G > setting.ptolerance) || !((double)pic.GetPixel(j,i).B >setting.ptolerance)))
                    {
                        if (first)
                        {
                            beginx = j;
                            first = false;
                        }

                        Mod = "white";
                    }

                    if (Mod == "white" && (!((double)pic.GetPixel(j, i).R > setting.ptolerance) || !((double)pic.GetPixel(j, i).G > setting.ptolerance) || !((double)pic.GetPixel(j, i).B > setting.ptolerance)))
                    {
                        all = false;
                    }
                }

                if (Mod == "white" && all)
                {
                    endx = j - 1;
                    Mod = "black";
                }
            }

            J = pic.Height;// this.bufi.getHeight();
            Mod = "black";
            first = true;

            for (j = 0; j < J; ++j)
            {
                all = true;

                for (i = beginx; i <= endx; ++i)
                {
                    if (Mod == "black" && (!((double)pic.GetPixel(i,j).R > setting.ptolerance) || !((double)pic.GetPixel(i,j).G > setting.ptolerance) || !((double)pic.GetPixel(i,j).B > setting.ptolerance)))
                    {
                        if (first)
                        {
                            beginy = j;
                            first = false;
                        }

                        Mod = "white";
                    }

                    if (Mod == "white" && (!((double)pic.GetPixel(i, j).R > setting.ptolerance) || !((double)pic.GetPixel(i, j).G > setting.ptolerance) || !((double)pic.GetPixel(i, j).B > setting.ptolerance)))
                    {
                        all = false;
                    }
                }

                if (Mod == "white" && all)
                {
                    endy = j - 1;
                    Mod = "black";
                }

                if (Mod == "white" && j == J - 1)
                {
                    endy = J - 1;
                }
            }

            return new rectXY(beginx, beginy, endx, endy);
        }
    }
}
