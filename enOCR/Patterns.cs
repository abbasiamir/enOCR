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



    public class Patterns
    {
        // Setting setting = new Setting();
        public List<string> Names = new List<string>();
        //ArrayList<BufferedImage> buffers = new ArrayList();
        public List<Pattern> pattern_s = new List<Pattern>();

        public Patterns()
        {

        }

        public void Create()
        {
            // this.buffers = this.load();
            this.makepatterns();
        }

        public Image[] load()
        {
            //ArrayList<BufferedImage> buffers = new ArrayList();
            //String dir = System.getProperty("user.dir");
            //File file = new File(dir + "/Patterns");
            //File[] file_2 = file.listFiles();


            string[] images_Names = Directory.GetFiles(Application.StartupPath + "Patterns_Croped");
            Image[] patrns = new Image[images_Names.Count()];
            for (int i = 0; i < images_Names.Count(); i++)
            {

                patrns[i] = Bitmap.FromFile(images_Names[i]);
                Names.Add(images_Names[i].Substring(images_Names[i].LastIndexOf("\\") + 1));

                //crop2((Bitmap)patrns[i], new rectXY(0, 0, patrns[i].Width, patrns[i].Height), Names[i]);
            }
            //for (int i = 0; i < images_Names.Count(); ++i)
            //{
            //    try
            //    {
            //        Bitmap img =Bitmap.file_2[i];
            //        buffers.add(img);
            //        this.Names.add(file_2[i].getName());
            //    }
            //    catch (Exception var7)
            //    {
            //        System.out.println(var7.getMessage());
            //    }
            //}

            return patrns;
        }

        void makepatterns()
        {
            //Iterator i$ = this.buffers.iterator();

            //while (i$.hasNext()) {
            //    BufferedImage buffer = (BufferedImage)i$.next();
            //    pattern p = new pattern(buffer, this.setting);
            //    p.MakebPattern();
            //    this.pattern_s.add(p);
            //}
            Image[] patrns = load();
            for (int i = 0; i < patrns.Count(); i++)
            {
                Pattern pattern = new Pattern((Bitmap)patrns[i]);
                pattern_s.Add(pattern);
            }

        }

        //public class colorset
        //{
        //    rectWH bound = new rectWH();
        //    pixelcolor[][] pixels;

        //    public colorset(rectWH b)
        //    {
        //        this.pixels = new pixelcolor[b.w][b.h];
        //        this.bound = b;
        //    }
        //}
        public void Crop(Bitmap img, rectXY bound, string imagename)
        {
            String Mode = "black";
            int xbegin = 0;
            int xend = 0;
            int ybegin = 0;
            int yend = 0;
            bool all = true;
            bool xbeginSetted = false;
            for (int i = bound.x1; i < bound.x2; ++i)
            {
                all = true;

                {

                    for (int j = bound.y1; j < bound.y2; ++j)
                    {

                        //if (!xbeginSetted)
                        //{
                        if (Mode == "black" && (img.GetPixel(i, j).R <= setting.RGB || img.GetPixel(i, j).G <= setting.RGB || img.GetPixel(i, j).B <= setting.RGB))
                        {
                            xbegin = i;
                            Mode = "white";
                            all = false;
                            xbeginSetted = true;
                        }
                        //}
                        if (Mode == "white" && (img.GetPixel(i, j).R <= setting.RGB || img.GetPixel(i, j).G <= setting.RGB || img.GetPixel(i, j).B <= setting.RGB))
                        {
                            all = false;
                        }

                    }
                }
                if (Mode == "white" && all)
                {
                    xend = i + 1;
                    //int xend = bound.x2;
                    Mode = "black";
                    bool all2 = true;
                    bool first = true;

                    for (int k = bound.y1; k < bound.y2; ++k)
                    {
                        all2 = true;

                        for (int m = xbegin; m < xend; ++m)
                        {
                            if (Mode == "black" && (img.GetPixel(m, k).R <= setting.RGB || img.GetPixel(m, k).G <= setting.RGB || img.GetPixel(m, k).B <= setting.RGB))
                            {
                                if (first)
                                {
                                    ybegin = k;
                                    first = false;
                                    break;
                                }

                                Mode = "white";
                                all2 = false;
                                break;
                            }

                            if (Mode == "white" && (img.GetPixel(m, k).G <= setting.RGB || img.GetPixel(m, k).B <= setting.RGB || img.GetPixel(m, k).G <= setting.RGB))
                            {
                                all2 = false;
                            }
                        }

                        if (Mode == "white" && all2)
                        {
                            yend = k - 1;
                            Mode = "black";
                        }

                        if (Mode == "white" && k == bound.y2)
                        {
                            yend = k;
                        }
                    }
                    rectXY rect = new rectXY(xbegin, ybegin, xend, yend);
                    //this.words.Add(new word(new rectXY(xbegin, ybegin, xend, yend)));

                    Mode = "black";


                }
            }
            Bitmap boresh = info.makeSubImage(xbegin, xend, ybegin, yend, img);

            boresh.Save(Application.StartupPath + "\\Patterns_Croped\\" + imagename);
        }
        public void crop2(Bitmap img, rectXY bound, string imagename)
        {
            int xbegin = 0;
            int ybegin = 0;
            int xend = 0;
            int yend = 0;
            for (int i = bound.x1; i < bound.x2; i++)
            {
                for (int j = bound.y1; j < bound.y2; j++)
                {
                    if (img.GetPixel(i, j).R <= setting.RGB && img.GetPixel(i, j).G <= setting.RGB && img.GetPixel(i, j).B <= setting.RGB)
                    {
                        xbegin = i;
                        i = bound.x2;
                        break;

                    }
                }
            }
            for (int j = bound.y1; j < bound.y2; j++)
            {
                for (int i = bound.x1; i < bound.x2; ++i)
                {
                    if (img.GetPixel(i, j).R <= setting.RGB && img.GetPixel(i, j).G <= setting.RGB && img.GetPixel(i, j).B <= setting.RGB)
                    {
                        ybegin = j;
                        j = bound.y2;
                        break;
                    }
                }
            }
            for (int i = bound.x2-1; i > bound.x1; i--)
            {
                for (int j = bound.y1; j < bound.y2; j++)
                {
                    if (img.GetPixel(i, j).R <= setting.RGB && img.GetPixel(i, j).G <= setting.RGB && img.GetPixel(i, j).B <= setting.RGB)
                    {
                        xend = i;
                        i = bound.x1;
                        break;

                    }
                }
            }
            for (int j = bound.y2-1; j > bound.y1; j--)
            {
                for (int i = bound.x1; i < bound.x2; ++i)
                {
                    if (img.GetPixel(i, j).R <= setting.RGB && img.GetPixel(i, j).G <= setting.RGB && img.GetPixel(i, j).B <= setting.RGB)
                    {
                        yend = j;
                        j = bound.y1;
                        break;
                    }
                }
            }
            Bitmap boresh = info.makeSubImage(xbegin, xend, ybegin, yend, img);

            boresh.Save(Application.StartupPath + "\\Patterns_Croped\\" + imagename);
        }
    }
}
