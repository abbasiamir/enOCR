using System.Diagnostics.Eventing.Reader;

namespace enOCR
{
    //
    // Source code recreated from a .class file by IntelliJ IDEA
    // (powered by FernFlower decompiler)
    //



    public class PatternRecognition
    {
        // Setting setting = new Setting();
        Patterns patt;
        rectXY bound = new rectXY();
        //Bitmap img = info.pics[info.currentPic];

        public PatternRecognition()
        {
            //  this.setting = sett;
            this.patt = new Patterns();
            this.patt.Create();
        }


        public List<String> MakeText(Bitmap img, RichTextBox list)
        {
            picture pic = new picture(false, img);
            List<String> outtext = new List<string>();
            String pagetext = "";
            String linetext = "";
            double scaletemp = 0.0;

            for (int p = pic.pages.Count() - 1; p >= 0; --p)
            {
                page pg = (page)pic.pages[p];
                String wrdtext = "";

                for (int l = 0; l < pg.Lines.Count; ++l)
                {
                    line line1 = pg.Lines[l];
                    //double scale = this.findSingles(p, l, line1, pic, img);
                    //scale = 1.0 / scale;
                    ;

                    Bitmap lineimg = info.makeSubImage(line1.bound.x1, line1.bound.x2, line1.bound.y1, line1.bound.y2, img);
                    rectXY b = new rectXY(0, 0, lineimg.Width - 1, lineimg.Height - 1);
                    //BufferedImage lineimgs = this.Scale(lineimg, scale, b);
                    //new rectXY(0, 0, lineimgs.getWidth() - 1, lineimgs.getHeight() - 1);
                    picture linepic = new picture(true, lineimg);
                    //picture.page linepage = (picture.page)linepic.pages.get(0);
                    //line = (picture.line)linepage.Lines.get(0);
                    linetext = "";
                    if (line1.bound.y2 - line1.bound.y1 > 3)
                    {
                        for (int w = 0; w < line1.words.Count; ++w)
                        {
                            word wrd = line1.words[w];
                            word wrd2;

                            wrdtext = "";
                            List<List<result>> listall = new List<List<result>>();
                            wrdtext = this.getWrdText(pic, wrd,0,false,ref listall,list);
                            linetext += wrdtext;
                            //list.Text += wrdtext;
                            list.Update();
                            list.ScrollToCaret();
                            if (w + 1 < line1.words.Count)
                            {
                                wrd2 = line1.words[w + 1];
                                if (wrd2.bounds.x1 - wrd.bounds.x2 >= 18)
                                    list.Text += " ";
                            }
                           
                        }

                        list.Text+= "\r\n";
                        //for (int i = 0; i < linetext.Length; i++)
                        //{
                        //list.Text += linetext;
                        //list.Update();
                        //}

                        //list.Update();
                        
                        pagetext = pagetext + linetext;
                    }
                }

                outtext.Add(pagetext);
            }

            return outtext;
        }

        double getratio(Pattern p)
        {
            double h = (double)(p.bound.y2 - p.bound.y1 + 1);
            double w = (double)(p.bound.x2 - p.bound.x1 + 1);
            return h / w;
        }

        /*    double findSingles(int pagenum, int linenum, line line, picture imag)
            {
                double finallscale = 1.0;
                double comptemp = 100000.0;

                for (int i = 0; i < this.patt.pattern_s.Count(); ++i)
                {
                    int j;
                    if (i == 81 || i == 27 || i == 35)
                    {
                        j = 1;
                        ++j;
                    }

                    if (((String)this.patt.Names[i]).IndexOf((char)49) != -1)
                    {
                        for (j = 0; j < line.words.Count; ++j)
                        {
                            Pattern p = (Pattern)this.patt.pattern_s[i];
                            word w = (word)line.words[j];
                            if (w.bounds.x2 - w.bounds.x1 > 3 && w.bounds.y2 - w.bounds.y1 > 3)
                            {
                                double h = (double)(w.bounds.y2 - w.bounds.y1 + 1);
                                double wid = (double)(w.bounds.x2 - w.bounds.x1 + 1);
                                double wratio = h / wid;
                                double pratio = this.getratio(p);
                                if (Math.Abs(pratio - wratio) < 0.5)
                                {
                                    rectXY rect = new rectXY();
                                    rect.x1 = w.bounds.x1;
                                    rect.x2 = w.bounds.x2;
                                    rect.y1 = w.bounds.y1;
                                    rect.y2 = w.bounds.y2;
                                    double pheight = (double)(p.bound.y2 - p.bound.y1 + 1);
                                    double rectheight = (double)(rect.y2 - rect.y1 + 1);
                                    double scale = rectheight / pheight;
                                    //BufferedImage scbuff = this.Scale(p.bufi, scale, p.bound);
                                    Pattern temp = new Pattern(imag.img);
                                    temp.MakebPattern();
                                    bool[][] tempboolean = this.getboolean(new rectXY(w.bounds.x1, w.bounds.y1, w.bounds.x2, w.bounds.y2), imag);
                                    double comp = this.compare2(tempboolean, temp.bPattern);
                                    if (comp < comptemp)
                                    {
                                        finallscale = scale;
                                        comptemp = comp;
                                    }
                                }
                            }
                        }
                    }
                }

                return finallscale;
            }*/
        int counter = 1;
        const int maxTelorans = 90;
        String getWrdText(picture picall, word word,int xbegin, bool yfromtop,ref List<List<result>> resultsall,RichTextBox rtb1)
        {
           
            int xtemp = word.bounds.x2;
            List<double> comparetemp;
            int[] detectedx = new int[10];
            setting.telorance = maxTelorans;
            bool single = false;
            int Deep = 1;
            bool replace = false;
            Bitmap boresh = info.makeSubImage(word.bounds.x1, word.bounds.x2, word.bounds.y1, word.bounds.y2+1, picall.img);
            picture wordpic = new picture(false, boresh);
            boresh.Save(Application.StartupPath + "\\boreshes\\boresh" + counter++.ToString() + ".jpg");
            
            //compareresult1 = 0;
            //compareresult2 = 0;
            int endx=-1;
            int countopt = 0;
            List<int> indexes = new List<int>();
            List<int> endxes = new List<int>();
            List<result> results = new List<result>();
            if(resultsall==null)
                resultsall = new List<List<result>>();
            while (xbegin < wordpic.img.Width-7)
            {
                
                single = false;
               
                results = new List<result>();
                List<result> resultstemp = new List<result>();
                for (int i = 0; i < this.patt.pattern_s.Count; i++)
                {
                    if (counter==4&&i==84)
                    {
                        bool var26 = true;

                    }
                    //Pattern pattern = new Pattern(patt.pattern_s[i].pic);
                    Pattern pattern = (Pattern)this.patt.pattern_s[i];
                    string ch = normalizename(patt.Names[i]);
                    if (xbegin==0&&Math.Abs(pattern.pic.Width - wordpic.img.Width) <= 17 && Math.Abs(pattern.pic.Height - wordpic.img.Height) <= 17)
                    {
                        single = true;
                        //pattern.pic = new Bitmap(pattern.pic, new Size(wordpic.img.Width, wordpic.img.Height));
                    }
                    else
                    {
                        single = false;
                        
                        //pattern.pic =this.patt.pattern_s[i].pic;
                    }

                    
                    endx = xbegin + pattern.pic.Width;
                    
                    if (endx <= wordpic.img.Width + 15 || single)
                    {


                       
                        int beginy = 0;
                        if (!yfromtop)
                            beginy = findybegin(wordpic, endx, xbegin);
                        else
                            beginy = findybeginfromend(wordpic, endx, xbegin);

                        int endy = beginy + pattern.pic.Height;
                        //if (single)
                        //{
                        //    xbegin = 0;
                        //    endx = wordpic.img.Width;
                        //    beginy = 0;
                        //    endy = wordpic.img.Height;
                        //}
                       
                        if ((endy <= (wordpic.img.Height + 15) && checkendy(wordpic, xbegin, endx, beginy, endy)) || single)
                        {
                           
                            if (!(ch == "." && !single))
                            {
                                comparetemp = this.compare5(pattern.pic, wordpic, xbegin, endx, beginy, endy, setting.telorance, Deep);
                                
                                if (comparetemp.Count>0)

                                {
                                    if(comparetemp[1] > 0)
                                    results.Add(new result(xbegin, endx, comparetemp[1], i, normalizename(patt.Names[i])));
                                    break;
                                }
                            }
                        }

                    
                    }
                    
                    if (!yfromtop&&(ch=="i"||ch=="j"))
                    {
                        yfromtop = true;
                        i-=1;
                       
                    }
                    else if(yfromtop==true )
                        yfromtop=false;
                }
               
                if (results.Count > 0)
                    setting.telorance = maxTelorans;
               
                if (results.Count == 0)
                {
                    // setting.telorance -= 5;
                    if (countopt ==0)
                    {
                        //setting.telorance -= 5;
                        //countopt++;
                        info.route += 1;
                        info.scale += 1;
                    }
                    //else if (countopt == 1)
                    //{
                    //    setting.telorance -= 5;
                    //    //countopt++;
                    //}
                    //else if(countopt > 1 )
                    //{
                    //if (info.scale < 2)
                    //{
                    else if (countopt == 1)
                    {
                        //setting.telorance = 85;
                        info.route += 1;
                        info.scale += 1;

                    }
                    //else if (countopt == 2)
                    //{
                    //    setting.telorance = 75;
                    //}
                    //countopt++;
                    //}
                    //else
                    //{
                    else if (countopt == 2)
                    {

                        //setting.telorance = 85;
                        info.route++;
                        info.scale++;
                    }
                    else if (countopt == 3)
                    {

                       // setting.telorance = 85;
                        info.route++;
                        info.scale++;
                    }
                    else
                        return "";
                    //else if(setting.telorance>70&&countopt>4)
                    //    setting.telorance -=5;

                    countopt++;
                    //}
                    //}
                    /* else if (countopt > 4 && countopt < 7&& resultsall.Count >= 1)
                     {
                         setting.telorance = maxTelorans;
                         replace = true;
                             Deep++;
                             countopt = 0;

                             resultstemp = resultsall.Last();

                             if (resultsall.Count >= 2)
                                 xbegin = resultsall[resultsall.Count - 2].First().xend;
                             else
                                 xbegin = 0;
                         //}
                     }
                     else if((countopt >5&&countopt<8))
                     {
                         setting.telorance = maxTelorans;
                         if (resultstemp.Count > 0)
                         {
                             resultsall.Remove(resultsall.Last());
                             resultsall.Add(resultstemp);
                         }
                         if (resultsall.Count > 0)
                             xbegin = resultsall.Last().First().xend;
                         else
                             xbegin = 0;
                         Deep = 1;
                         if(countopt==6)
                         setting.telorance = 75;
                         else if (countopt == 7)
                             setting.telorance = 70;
                         countopt  ++;

                     }

                     else if(countopt == 8)
                     {
                         xbegin = wordpic.img.Width;
                     }*/

                }
                else
                {
                    setting.telorance = maxTelorans;
                    countopt = 0;
                    Deep = 1;
                    resultstemp = new List<result>();
                    info.route = info.max_rout;
                    info.scale = info.max_scale;

                }




               
                if (results.Count > 0)
                {
                    setting.telorance = maxTelorans;
                    //results = (List<result>)results.OrderByDescending(x => x.persent).ToList();
                    xbegin = results[0].xend;
                    //if(replace)
                    //{
                    //    resultsall.Remove(resultsall.Last());
                    //    replace = false;
                    //}
                    rtb1.Text += results[0].text;
                    rtb1.Update();
                    rtb1.ScrollToCaret();
                    resultsall.Add(results);
                    countopt = 0;
                    if (findblank(wordpic, results[0].xend))
                    {
                        xbegin = wordpic.img.Width;
                        //break;
                    }
                }

            }
           

            return getText(resultsall);
        }
        bool findblank(picture pic,int endx)
        {
            if (endx+1 >= pic.img.Width)
                return false;
            for (int i = endx+1; i < pic.img.Width; i++)
            {
                for (int j = 0; j < pic.img.Height; j++)
                {
                    //if (j == 24)
                    //{
                    //    int a = 1;
                    //}
                    if (info.checkdot(i, j, setting.RGB, pic.img))
                        return false;
                }
            }
            return true;
        }
        string getText(List<List<result>> list)
        {
            string text = "";
            foreach (var item in list)
            {
                if(item.Count > 0)
                     text += item[0].text;
            }
            return text;
        }
        List<List<result>> optimizeresults(picture pic,word word,List<List<result>> results)
        {
           // List<List<result>> optList = new List<List<result>>();
            
            //for (int i = 0; i < results.Count; i++)
            //{

            //    //for (int j = 0; j < results[i].Count; j++)
            //    //{
            //    if (results[i].Count > 0 )
            //    {
            //        optList.Add(results[i]);
            //       // tryCount = 0;
            //    }
            //    else
            //    {
                    if (results.Last().Count>0)
                    {
                        //bool yfromend = false;
                        //if (tryCount == 0)
                        //    yfromend = true;
                        //else
                        //{
                            //optList.Remove(optList.Last());
                            results.Last().Remove(results.Last()[0]);

                        //}
                        //tryCount++;
                           // string outtext = getWrdText(pic, word, results.Last()[0].xbegin, false, ref results);
                            //optList = optimizeresults(pic, word, optList);
                        
                    }
                
               // }
            
            return results;
        }
        string normalizename(string txt)
        {
            if (txt[0] == '$')
                txt = txt.Remove(0, 2);
            if (txt.Contains("_"))
                txt = txt.Remove(txt.IndexOf("_"), 1);
            txt = txt.Remove(txt.IndexOf("."));
            switch (txt)
            {
                //case "slash":
                //    txt = "/";
                //    break;
                case "BraketClose":
                    txt = "}";
                    break;
                case "BraketOpen":
                    txt = "{";
                    break;
                case "dot":
                    txt = ".";
                    break;
                case "DQout":
                    txt.Append('"');
                    break;
                case "GrtrThan":
                    txt = ">";
                    break;
                case "lessThan":
                    txt = "<";
                    break;
                case "PClose":
                    txt = ")";
                    break;
                case "POpen":
                    txt = "(";
                    break;
                case "slash":
                    txt = "/";
                    break;
                case "star":
                    txt = "*";
                    break;
                case "Question":
                    txt = "?";
                    break;
                case "pipe":
                    txt = "|";
                    break;
                case "Qout":
                    txt = "'";
                    break;
                case "Dslash":
                    txt = "//";
                    break;
            }
            return txt;
        }
        List<List<result>> SortList(List<List<result>> list)
        {
            double persent = 0;
            int index = 0;
            List<result> result = new List<result>();
            for (int i = 0; i < list.Count; i++)
            {

                list[i] = list[i].OrderByDescending(x => x.persent).ToList();
            }
            return list;
        }
        bool checkendy(picture pic, int beginx, int endx, int beginy, int endy)
        {
            if (Math.Abs(endy - pic.img.Height) <= 5)
                return true;
            int w=endx - beginx;
            w = w / 3;
            for (int i = beginx+w; i <= endx-w; i++)
            {
                for (int j = endy + 3; j < endy + 15; j++)
                {

                    if(j<pic.img.Height&&endx-w<pic.img.Width)
                    if (info.checkdot(i, j, setting.RGB, pic.img,1))
                        return false;
                }
            }
            return true;
        }
        int xbeginexact(picture pic, int xbegin, word wrd)
        {
            while (xbegin < wrd.bounds.x2)
            {
                for (int i = wrd.bounds.y1; i < wrd.bounds.y2; i++)
                {
                    if (info.checkdot(xbegin, i, setting.RGB, pic.img,2))
                        return i;
                }
                xbegin++;
            }
            return xbegin;
        }
        int findybegin(picture pic, int end, int begin)
        {
          //  int w=(end-begin)/3;
            for (int j = 0; j < pic.img.Height; ++j)
            {

                for (int i = begin; i <= end; ++i)
                {
                    if (i < pic.img.Width&&i>=0)
                        if (info.checkdot(i, j, setting.RGB, pic.img,3))
                        {
                            return j;
                        }
                }
            }

            return 0;
        }
        int findybeginfromend(picture pic, int end, int begin)
        {
            string Mode = "white";
            if (end > pic.img.Width)
                end = pic.img.Width;
            for (int j = pic.img.Height-1; j >= 0; j--)
            {
                for (int i = begin; i < end; i++)
                {
                    if (info.checkdot(i, j, setting.RGB, pic.img,4))
                    {
                        if (i == 29)
                        {
                            int z = 1;
                        }
                        Mode = "black";
                        break;
                    }
                    if (i == end-1 && Mode == "black")
                        return j;
                }
            }
            return 0;
        }
        int[] findys(picture pic, word wr, int end, int begin, int ybegin, int yend)
        {
            int[] ys = new int[yend - ybegin + 1];
            int element = 0;

            for (int i = begin; i <= end; ++i)
            {
                for (int j = ybegin; j <= yend; ++j)
                {
                    if (info.checkdot(i, j, setting.RGB, pic.img))
                    {
                        ys[element++] = j - ybegin;
                        break;
                    }
                }
            }

            return ys;
        }

        int[] findysp(Pattern p)
        {
            int[] ys = new int[p.bPattern.Count()];
            int element = 0;

            for (int i = 0; i < p.bPattern.Count(); ++i)
            {
                for (int j = 0; j < p.bPattern[0].Count(); ++j)
                {
                    if (p.bPattern[i][j])
                    {
                        ys[element++] = j;
                        break;
                    }
                }
            }

            return ys;
        }

        double compareys(picture pic, word wrd, int begin, int end, int ybegin, int yend, Pattern p)
        {
            int[] ys = this.findys(pic, wrd, end, begin, ybegin, yend);
            int[] ysp = this.findysp(p);
            double wrong = 0.0;
            if (ys.Length == ysp.Length)
            {
                for (int i = 0; i < ys.Length; ++i)
                {
                    wrong += (double)Math.Abs(ys[i] - ysp[i]);
                }

                return wrong / (double)ys.Length;
            }
            return 1000;
        }

        int findyleft(picture pic, word wr, int end)
        {
            for (int j = wr.bounds.y1; j <= wr.bounds.y2; ++j)
            {
                if (info.checkdot(end, j, setting.RGB, pic.img))
                {
                    return j - wr.bounds.y1;
                }
            }

            return -1;
        }

        int findyright(picture pic, word wr, int begin)
        {
            for (int j = wr.bounds.y1; j <= wr.bounds.y2; ++j)
            {
                if (info.checkdot(begin, j, setting.RGB, pic.img))
                {
                    return j - wr.bounds.y1;
                }
            }

            return -1;
        }

        int findyrightp(Pattern p)
        {
            for (int j = 0; j < p.bPattern[0].Count(); ++j)
            {
                if (p.bPattern[p.bPattern.Count() - 1][j])
                {
                    return j;
                }
            }

            return 0;
        }

        int findyleftp(Pattern p)
        {
            for (int j = 0; j < p.bPattern[0].Count(); ++j)
            {
                if (p.bPattern[0][j])
                {
                    return j;
                }
            }

            return 0;
        }

        int findyend(picture pic, word wr, int end, int begin)
        {
            for (int j = pic.img.Height - 1; j >= 0; j--)
            {

                for (int i = begin; i <= end; i++)

                {
                    if (i < pic.img.Width)
                        if (info.checkdot(i, j, setting.RGB, pic.img))
                        {
                            return j;
                        }
                }
            }

            return wr.bounds.y2;
        }
        List<List<bool>> findbrect(int x1, int y1, int x2, int y2, picture pic)
        {
            rectXY rect = new rectXY(x1, y1, x2, y2);
            return getboolean(rect, pic);
        }
        List<double> compare(List<List<bool>> b1, picture pic, int beginx, int endx, int beginy, int endy)
        {
            List<List<double>> results = new List<List<double>>();
            List<double> resultsend = new List<double>();
            for (int i = -2; i <= 2; i++)
            {
                for (int j = -2; j <= 2; j++)
                {
                    //if (j + b1[0].Count == pic.img.Height + 2)
                    //    break;
                    //if (beginy - j >= 0 && beginx - i >= 0)
                    //{

                    //    resultsend = (comparing(b1, findbrect(beginx - i, beginy - j, endx - i, endy - j, pic)));
                    //    resultsend.Add(endx - i);
                    //    results.Add(resultsend);
                    //    //if(beginx-i >= 0)
                    //    //{
                    //    //    results.Add(comparing(b1,findbrect(beginx-i,beginy-j,endx-i, endy-j,pic)));
                    //    //    resultsend.Add(endx-i);
                    //    //}
                    //}
                    if (endy + j < pic.img.Height && endx + i < pic.img.Width&&beginx+i>=0&&beginy+j>=0&&endx+i>=0&&endy+j>=0)
                    {

                        resultsend = (comparing(b1, findbrect(beginx + i, beginy + j, endx + i, endy + j, pic)));
                        resultsend.Add(endx + i);
                        results.Add(resultsend);
                        //if (beginx + i <pic.img.Width)
                        //{
                        //    results.Add(comparing(b1, findbrect(beginx + i, beginy + j, endx+i, endy+j,pic)));
                        //    resultsend.Add(endx+i);
                        //}
                    }
                    //if (beginy - j >= 0 && beginx + i > pic.img.Width)
                    //{

                    //    //results.Add(comparing(b1, findbrect(beginx - i, beginy - j, endx - i, endy - j,pic)));
                    //    //resultsend.Add(endx - i);
                    //    if (beginx + i < pic.img.Width && beginy - j >= 0)
                    //    {
                    //        resultsend = (comparing(b1, findbrect(beginx + i, beginy - j, endx + i, endy - j, pic)));
                    //        resultsend.Add(endx + i);
                    //        results.Add(resultsend);
                    //    }
                    //}
                    //if (beginy + j < pic.img.Height && endx - i < pic.img.Width && endx - i >= 0)
                    //{

                    //    //results.Add(comparing(b1, findbrect(beginx + i, beginy + j, endx + i, endy + j,pic)));
                    //    //resultsend.Add(endx + i);
                    //    if (beginx - i >= 0)
                    //    {
                    //        resultsend = (comparing(b1, findbrect(beginx - i, beginy + j, endx - i, endy + j, pic)));
                    //        resultsend.Add(endx - i);
                    //        results.Add(resultsend);
                    //    }
                    //}
                    if (beginy + j < pic.img.Height && endx< pic.img.Width )
                    {

                        //results.Add(comparing(b1, findbrect(beginx + i, beginy + j, endx + i, endy + j,pic)));
                        //resultsend.Add(endx + i);
                        if (beginx + i >= 0&&beginy+j>=0)
                        {
                            resultsend = (comparing(b1, findbrect(beginx + i, beginy + j, endx, endy, pic)));
                            resultsend.Add(endx);
                            results.Add(resultsend);
                        }
                    }
                    if (endy + j < pic.img.Height && endx+i < pic.img.Width)
                    {

                        //results.Add(comparing(b1, findbrect(beginx + i, beginy + j, endx + i, endy + j,pic)));
                        //resultsend.Add(endx + i);
                        if (endx + i >= 0 && endy + j >= 0)
                        {
                            resultsend = (comparing(b1, findbrect(beginx, beginy, endx+i, endy+i, pic)));
                            resultsend.Add(endx+i);
                            results.Add(resultsend);
                        }
                    }


                }

            }
            double maxadd = 0;
            double maxp = 0;
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i][0] > maxp)
                {
                    maxp = results[i][0];
                    //maxadd= results[i][1];
                    endx = (int)results[i][2];
                }
            }
            List<double> list = new List<double>();
            list.Add(maxadd);
            list.Add(maxp);
            list.Add(endx);

            return list;

        }
        List<double> compare4(List<List<bool>> b1, picture pic, int beginx, int endx, int beginy, int endy)
        {
            int w = info.max_rout;
            List<List<double>> results = new List<List<double>>();
            List<double> resultsend = new List<double>();
            for (int i = -w; i <= w; i++)
            {
                for (int j = -w; j <= w; j++)
                {
                    for (int l = -w; l <= w; l++)
                    {
                        for (int m = -w; m <= w; m++)
                        {
                            if (endy + m <= pic.img.Height && endx + l <= pic.img.Width && beginx + i >= 0 && beginy + j >= 0 && endx + l >= 0 && endy + m >= 0)
                            {
                                int beginw=beginx + i;
                                int endw = endx + l;
                                int beginh = beginy + j;
                                int endh = endy + m;
                                if (Math.Abs(endw-beginw) == b1.Count && Math.Abs(endh-beginh) == b1[0].Count)
                                {
                                    resultsend = (comparing(b1, findbrect(beginx + i, beginy + j, endx + l, endy + m, pic)));
                                    resultsend.Add(endx+l );
                                    results.Add(resultsend);
                                }
                            }
                        }

                    }

                }

            }
            double maxadd = 0;
            double maxp = 0;
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i][0] > maxp)
                {
                    maxp = results[i][0];
                    //maxadd= results[i][1];
                    endx = (int)results[i][2];
                }
            }
            List<double> list = new List<double>();
            list.Add(maxadd);
            list.Add(maxp);
            list.Add(endx);

            return list;

        }
        List<double> compare6(Bitmap pattern, picture pic, int beginx, int endx, int beginy, int endy,double minTolorance,int deepCount )
        {
            int w = info.route;
            int routeStep = 1;
            int s = info.scale;
            int scaleStep = 1;
            int deep = 1;
            //List<List<double>> results = new List<List<double>>();
            List<double> resultsend = new List<double>();
            int i = 0;
            while (i <= info.route)
            {
                int j = 0;
                while (j <= info.route)
                {
                    //int l = 0;
                    //while (l <= info.max_rout)
                    //{
                    //    int m = 0;
                    //    while (m <= info.max_rout)
                    //    {
                    int a = 0;
                    while (a <= info.scale)
                    {

                        int b = 0;
                        while (b <= info.scale)
                        {
                            
                            Bitmap tempPattern = new Bitmap(pattern, new Size(pattern.Width + a, pattern.Height + b));
                            if (tempPattern.Height + beginy + j <= pic.img.Height && tempPattern.Width + beginx + i <= pic.img.Width && beginx + i >= 0 && beginy + j >= 0)
                            {
                                int beginw = beginx + i;
                                int endw = tempPattern.Width + beginx + i;
                                int beginh = beginy + j;
                                int endh = tempPattern.Height + beginy + j;

                                //if (Math.Abs(endw - beginw) == tempPattern.Width && Math.Abs(endh - beginh) == tempPattern.Height)
                                //{

                                double percent = (comparingPics(info.makeSubImage(beginx + i, +beginx + i + tempPattern.Width, beginy + j, +beginy + j + tempPattern.Height, pic.img), tempPattern));
                                if (percent >= minTolorance)
                                {
                                    if (deep == deepCount)
                                    {

                                        resultsend.Add(0);
                                        resultsend.Add(percent);
                                        resultsend.Add(+beginx + tempPattern.Width + i);
                                        return resultsend;
                                    }
                                    else
                                        deep++;

                                }
                                else if(percent<0&&i==0&&j==0&&a==0&&b==0)
                                    return resultsend;
                            }

                            b=addsteps(b, scaleStep);
                        }
                        a=addsteps(a, scaleStep);
                    }
                    j=addsteps(j, routeStep);
                }
                i=addsteps(i, routeStep);

            }
            

            return resultsend;

        }
        List<double> compare5(Bitmap pattern, picture pic, int beginx, int endx, int beginy, int endy, double minTolorance, int deepCount)
        {
            int w = info.route;
            int routeStep = 1;
            int s = info.scale;
            int scaleStep = 1;
            int deep = 1;
            //List<List<double>> results = new List<List<double>>();
            List<double> resultsend = new List<double>();
           
           
                    //int l = 0;
                    //while (l <= info.max_rout)
                    //{
                    //    int m = 0;
                    //    while (m <= info.max_rout)
                    //    {
                    int a = 0;
            while (a <= info.scale)
            {

                int b = 0;
                while (b <= info.scale)
                {
                    int i = 0;
                    while (i <= info.route)
                    {
                        int j = 0;
                        while (j <= info.route)
                        {
                            Bitmap tempPattern = new Bitmap(pattern, new Size(pattern.Width + a, pattern.Height + b));
                            if (tempPattern.Height + beginy + j <= pic.img.Height && tempPattern.Width + beginx + i <= pic.img.Width && beginx + i >= 0 && beginy + j >= 0)
                            {
                                int beginw = beginx + i;
                                int endw = tempPattern.Width + beginx + i;
                                int beginh = beginy + j;
                                int endh = tempPattern.Height + beginy + j;

                                //if (Math.Abs(endw - beginw) == tempPattern.Width && Math.Abs(endh - beginh) == tempPattern.Height)
                                //{

                                double percent = (comparingPics(info.makeSubImage(beginx + i, +beginx + i + tempPattern.Width, beginy + j, +beginy + j + tempPattern.Height, pic.img), tempPattern));
                                if (percent >= minTolorance)
                                {
                                    if (deep == deepCount)
                                    {

                                        resultsend.Add(0);
                                        resultsend.Add(percent);
                                        resultsend.Add(+beginx + tempPattern.Width + i);
                                        return resultsend;
                                    }
                                    else
                                        deep++;

                                }
                                else if (percent < 0 && i == 0 && j == 0 && a == 0 && b == 0)
                                    return resultsend;
                            }


                            j = addsteps(j, routeStep);
                        }
                        i = addsteps(i, routeStep);

                    }
                    b = addsteps(b, scaleStep);
                }
                a = addsteps(a, scaleStep);
            }


            return resultsend;

        }
        int addsteps(int v,int step)
        {
            if (v > 0)
                v = -v;
            else if (v < 0)
                v = -v + step;
            else if (v == 0)
                v += step;
            return v;
        }
        int bccounter = 0;
        int ptcounter = 0;
        List<double> comparing(List<List<bool>> b1, List<List<bool>> b2)
        {

            int linetrue = 0;
            int linefalse = 0;
            
            if (b1.Count != b2.Count || b1[0].Count != b2[0].Count)
            {
                List<double> result0 = new List<double>();
                result0.Add(0);
                result0.Add(0);
                return result0;
            }

            for (int j = 0; j < b1[0].Count; j++)
            {


                for (int i = 0; i < b1.Count; i++)
                {


                    if (b1[i][j] == b2[i][j])
                    {

                        linetrue++;
                    }
                    else
                    {

                        linefalse++;
                    }


                }



            }
            List<double> result = new List<double>();
            result.Add(((linetrue - linefalse) * 100) / (linetrue + linefalse));
            result.Add(linetrue - linefalse);
            return result;
        }
        double comparingPics(Bitmap boresh,Bitmap ptrn)
        {

            int linetrue = 0;
            int linefalse = 0;
            if (bccounter == 0 && ptcounter == 0)
            {
                /*if (File.Exists(Application.StartupPath + "\\boreshescompare\\boresh" + ".jpg"))
                {
                    File.Delete(Application.StartupPath + "\\boreshescompare\\boresh" + ".jpg");
                    File.Delete(Application.StartupPath + "\\boreshescompare\\pattern" + ".jpg");
                }
                else
                {
                    boresh.Save(Application.StartupPath + "\\boreshescompare\\boresh" + ".jpg");
                    ptrn.Save(Application.StartupPath + "\\boreshescompare\\pattern" + ".jpg");
                }*/
            }
            //if (boresh.Width!=ptrn.Width || boresh.Height!=ptrn.Height)
            //{
            //    return -1;
            //}

            for (int j = 0; j < ptrn.Height; j++)
            {


                for (int i = 0; i <ptrn.Width; i++)
                {


                    if (info.comparePicsDot(boresh,ptrn,i,j))
                    {

                        linetrue++;
                    }
                    else
                    {

                        linefalse++;
                    }


                }



            }
            double result =0;
            result=(((linetrue - linefalse) * 100) / (linetrue + linefalse));
           // result.Add(linetrue - linefalse);
            return result;
        }
        double compare2(List<List<bool>> b1, List<List<bool>> b2)
        {

            int ymin;
            double correctcount = 0.0;
            double wrong = 0;
            int ymax;
            int xmin;
            int xmax;
            if (b1.Count() >= b2.Count())
            {
                xmin = b2.Count();
                xmax = b1.Count();
            }
            else
            {
                xmin = b1.Count();
                xmax = b2.Count();
            }

            //int ymin;
            //int ymax;
            if (b1[0].Count() >= b2[0].Count())
            {
                ymin = b2[0].Count();
                ymax = b1[0].Count();
            }
            else
            {
                ymin = b1[0].Count();
                ymax = b2[0].Count();
            }

            for (int i = 0; i < b1.Count; ++i)
            {
                for (int j = 0; j < b1[0].Count; ++j)
                {
                    if (b1[i][j] == b2[i][j])
                    {
                        ++correctcount;
                    }
                    else
                    {
                        wrong++;


                    }
                }
            }
            // return correctcount;
            double count = (double)(b1.Count * b1[0].Count);
            //double wrongcount = count - correctcount;
            //return wrongcount / count * 100.0;
            return correctcount / (wrong * count);
        }

        double compare3(bool[][] pb, bool[][] prect)
        {
            double correct = 0.0;
            double wrong = 0.0;

            for (int i = 0; i < pb.Length; ++i)
            {
                for (int j = 0; j < pb[0].Length; ++j)
                {
                    if (pb[i][j])
                    {
                        if (prect[i][j])
                        {
                            ++correct;
                        }
                        else
                        {
                            ++wrong;
                        }
                    }
                }
            }

            return wrong / (correct + wrong) * 100.0;
        }

        /*BufferedImage Scale(BufferedImage srcimage, double scale, rectXY bound)
        {
            int width = (int)((double)(bound.x2 - bound.x1 + 1) * scale);
            int height = (int)((double)(bound.y2 - bound.y1 + 1) * scale);
            if (width == 0)
            {
                width = 1;
            }

            if (height == 0)
            {
                height = 1;
            }

            BufferedImage newbuff1 = new BufferedImage(width, height, 4);
            srcimage = srcimage.getSubimage(bound.x1, bound.y1, bound.x2 - bound.x1 + 1, bound.y2 - bound.y1 + 1);
            Graphics2D graphics = newbuff1.createGraphics();
            graphics.setRenderingHint(RenderingHints.KEY_INTERPOLATION, RenderingHints.VALUE_INTERPOLATION_BILINEAR);
            graphics.drawImage(srcimage, 0, 0, width, height, (ImageObserver)null);
            graphics.dispose();
            return newbuff1;
        }

        BufferedImage Scale0(BufferedImage srcimage, double scale)
        {
            int width = (int)((double)srcimage.getWidth() * scale);
            int height = (int)((double)srcimage.getHeight() * scale);
            if (width == 0)
            {
                width = 1;
            }

            if (height == 0)
            {
                height = 1;
            }

            BufferedImage newbuff = new BufferedImage(width, height, 1);
            Graphics2D graphics = newbuff.createGraphics();
            graphics.setRenderingHint(RenderingHints.KEY_INTERPOLATION, RenderingHints.VALUE_INTERPOLATION_BILINEAR);
            graphics.drawImage(srcimage, 0, 0, width, height, (ImageObserver)null);
            graphics.dispose();
            return newbuff;
        }*/

        List<List<bool>> getboolean(rectXY rect, picture pic)
        {
            int w = rect.x2 - rect.x1;
            int h = rect.y2 - rect.y1;
            List<List<bool>> temp = new List<List<bool>>();

            for (int i = rect.x1; i < rect.x2; i++)
            {
                List<bool> vs = new List<bool>();
                for (int j = rect.y1; j < rect.y2; j++)
                {
                    if (i < pic.img.Width && j < pic.img.Height)
                    {

                        if (info.checkdot(i, j, setting.RGB, pic.img))
                        {
                            vs.Add(true);
                        }
                        else
                        {
                            vs.Add(false);
                        }

                    }



                }
                if (vs.Count > 0)
                    temp.Add(vs);
            }
            return temp;
        }


    }
}



