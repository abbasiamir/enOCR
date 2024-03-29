using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace enOCR
{
    public class page
    {
       public  List<line> Lines = new List<line>();
       public rectXY bound = new rectXY();
        public Bitmap img;
        int counter = 1;
        public page(rectXY c,Bitmap b)
        {
            this.bound = c;
            this.img = b;
        }

        void findlines()
        {
            String Mode = "black";
            int ybegin = 0;
            int yend = 0;
            bool all = true;

            for (int j = this.bound.y1; j <= this.bound.y2; ++j)
            {
                all = true;

                for (int i = this.bound.x1; i <= this.bound.x2; ++i)
                {
                    if (Mode == "black" && info.checkdot(i, j, setting.RGB,img)/*(picture.this.colorsetmain.pixels[i][j][0] <= picture.this.setting.RGB || picture.this.colorsetmain.pixels[i][j][1] <= picture.this.setting.RGB || picture.this.colorsetmain.pixels[i][j][2] <= picture.this.setting.RGB)*/)
                    {
                        ybegin = j;
                        Mode = "white";
                        all = false;
                        break;
                    }

                    if (Mode == "white" && info.checkdot(i, j, setting.RGB,img))
                    {
                        all = false;
                    }
                }

                if (Mode == "white" && all)
                {
                    this.Lines.Add(new line(new rectXY(this.bound.x1, ybegin, this.bound.x2, j),img));
                    //Bitmap boresh = info.makeSubImage(this.bound.x1, this.bound.x2,ybegin, j, img);

                   // boresh.Save(Application.StartupPath + "\\boreshes\\boreshline" + counter++.ToString() + ".jpg");

                    Mode = "black";
                }
            }

            if (this.Lines.Count == 0)
            {
                this.Lines.Add(new line(this.bound,img));
               // Bitmap boresh = info.makeSubImage(this.bound.x1, this.bound.x2, this.bound.y1,this.bound.y2, img);

               // boresh.Save(Application.StartupPath + "\\boreshes\\boreshline" + counter++.ToString() + ".jpg");

            }

        }

       public void MakeLines()
        {
            this.findlines();

            for (int i = 0; i < this.Lines.Count; ++i)
            {
                line l = (line)Lines[i];
                l.findwords();
            }

        }
    }
}
   

