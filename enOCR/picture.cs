
using enOCR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static enOCR.picture;

namespace enOCR
{
    //
    // Source code recreated from a .class file by IntelliJ IDEA
    // (powered by FernFlower decompiler)
    //



    public class picture
    {
        public bool Isline;
        //public Setting setting = new Setting();
        public List<page> pages = new List<page>();
        // public colorset colorsetmain;
        public Bitmap img;
        public picture( bool isline,Bitmap b)
        {
            img = b;
           
            this.Isline = isline;
            //this.makecolorset();
            this.MakePages();
        }

        void findpages()
        {
            //Bitmap pic = info.pics[info.currentPic];
            dot begin = new dot(0, 0);
            dot end = new dot(0, 0);
            String Mod = "black";
            bool all = true;
            int I = img.Width;
            int J = img.Height;
            //if (this.Isline)
            //{
                this.pages.Add(new page(new rectXY(0, 0, I - 1, J - 1),img));
            //}
           /* else
            {
                int j;
                int i;
                for (j = 0; j < I; ++j)
                {
                    all = true;

                    for (i = 0; i < J; ++i)
                    {
                        if (Mod == "black" &&(info.chechdot(j,i,setting.RGB,img)))
                        {
                            begin = new dot(j, 0);
                            Mod = "white";
                        }

                        if (Mod == "white" && (info.chechdot(j, i, setting.RGB, img)))
                        {
                            all = false;
                        }
                    }

                    if (Mod == "white" && all)
                    {
                        if ((double)(j - begin.x) >setting.Minpagesize)
                        {
                            end = new dot(j, 0);
                            j = I;
                        }
                        else
                        {
                            all = false;
                            Mod = "black";
                        }
                    }
                }

                J = img.Height;
                Mod = "black";

                for (j = 0; j < J; ++j)
                {
                    all = true;

                    for (i = 0; i < end.x; ++i)
                    {
                        if (Mod == "black" && info.chechdot(i,j,setting.RGB,img))
                        {
                            begin.y = j;
                            Mod = "white";
                        }

                        if (Mod == "white" && (info.chechdot(i,j,setting.RGB,img)))
                        {
                            all = false;
                        }
                    }

                    if (Mod == "white" && !all)
                    {
                        end.y = j;
                    }
                }

                this.pages.Add(new page(new rectXY(begin.x - 1, begin.y, end.x - 1, end.y),img));
                Mod = "black";
                dot begin2 = new dot(0, 0);
                dot end2 = new dot(0, 0);
                I = img.Width;// this.bufi.getWidth();
                J = img.Height;// this.bufi.getHeight();

                //int j;
                //int i;
                for (j = I - 1; j > 0; --j)
                {
                    all = true;

                    for (i = 0; i < J; ++i)
                    {
                        if (Mod == "black" && ((info.chechdot(j,i,setting.RGB,img))))
                        {
                            begin2 = new dot(j, 0);
                            Mod = "white";
                        }

                        if (Mod == "white" && (info.chechdot(j,i,setting.RGB,img)))
                        {
                            all = false;
                        }
                    }

                    if (Mod == "white" && all)
                    {
                        if ((double)(begin2.x - j) > setting.Minpagesize)
                        {
                            end2 = new dot(j, 0);
                            break;
                        }

                        all = false;
                        Mod = "black";
                    }
                }

                J = img.Height;// this.bufi.getHeight();
                I = img.Width;// this.bufi.getWidth();
                Mod = "black";

                for (j = 0; j < J; ++j)
                {
                    all = true;

                    for (i = I - 1; i > end2.x; --i)
                    {
                        if (Mod == "black" && (info.chechdot(i,j,setting.RGB,img)))
                        {
                            begin2.y = j;
                            Mod = "white";
                        }

                        if (Mod == "white" && (info.chechdot(i, j, setting.RGB, img)))
                        {
                            all = false;
                        }
                    }

                    if (Mod == "white" && !all)
                    {
                        end2.y = j;
                    }
                }

                if (begin2.x != end.x - 1)
                {
                    this.pages.Add(new page(new rectXY(end2.x, begin2.y, begin2.x, end2.y),img));
                }

            }*/
        }

        //void makecolorset()
        //{
        //    int x = 0;
        //    int y = 0;
        //    this.colorsetmain = new colorset(new rectWH(0, 0, this.bufi.getWidth(), this.bufi.getHeight()));
        //    int[] ar = new int[this.bufi.getWidth() * this.bufi.getHeight() * 3];
        //    int[] colorsettemp = this.bufi.getData().getPixels(0, 0, this.bufi.getWidth(), this.bufi.getHeight(), ar);
        //    int lenght = colorsettemp.length;
        //    int i = 0;

        //    while (i < lenght)
        //    {
        //        this.colorsetmain.pixels[x][y][0] = colorsettemp[i++];
        //        this.colorsetmain.pixels[x][y][1] = colorsettemp[i++];
        //        this.colorsetmain.pixels[x][y][2] = colorsettemp[i++];
        //        ++x;
        //        if (x == this.bufi.getWidth())
        //        {
        //            x = 0;
        //            ++y;
        //        }
        //    }

        //}

        void MakePages()
        {
           // new ArrayList();
            this.findpages();

            for (int i = 0; i < this.pages.Count; ++i)
            {
                page p = (page)this.pages[i];
                p.MakeLines();
            }

        }

        public class bitwisepettern
        {
            public bitwisepettern()
            {
            }
        }

        public class pattern
        {
            rectWH bound = new rectWH();

            public pattern()
            {
            }
        }

        //public class colorset
        //{
        //    rectWH bound = new rectWH();
        //    int[][][] pixels;

        //    public colorset(rectWH b)
        //    {
        //        this.pixels = new int[b.w][b.h][3];
        //        this.bound = b;
        //    }
        //}

    }
}

        