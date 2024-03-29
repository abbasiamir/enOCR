using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace enOCR
{
    internal static class info
    {
        public static Bitmap[] pics;
        public static int currentPic=1;
        public static int route =1 ;
        public const int max_rout = 1;
        public const int max_scale = 1;
        public static int scale =1;
        public const int ct= 75;
     
        public static bool comparePicsDot(Bitmap first,Bitmap second,int a,int b)
        {
            if(Math.Abs((first.GetPixel(a,b).R-second.GetPixel(a,b).R))<=ct&&
                Math.Abs((first.GetPixel(a, b).G - second.GetPixel(a, b).G)) <= ct&&
                Math.Abs((first.GetPixel(a, b).B - second.GetPixel(a, b).B)) <= ct)
            {
                return true;
            }
            return false;
        }
        public static bool checkdot(int a, int b, int rgb,Bitmap picture)
        {
            

            if (picture.GetPixel(a, b).R <= rgb && picture.GetPixel(a, b).G <= rgb && picture.GetPixel(a, b).B <= rgb)
                return true;
            return false;
        }
        public static bool checkdot(int a, int b, int rgb, Bitmap picture,int i)
        {


            if (picture.GetPixel(a, b).R <= rgb && picture.GetPixel(a, b).G <= rgb && picture.GetPixel(a, b).B <= rgb)
                return true;
            return false;
        }
        public static bool checkdotGrater(int a, int b, int rgb, Bitmap picture)
        {
            //if(a==0&&b==0)
            //picture.Save(Application.StartupPath + "\\boreshes\\boreshbmp"  + ".jpg");

            if (picture.GetPixel(a, b).R < rgb && picture.GetPixel(a, b).G < rgb && picture.GetPixel(a, b).B < rgb)
                return true;
            return false;
        }
        public static Bitmap makeSubImage(int x1,int x2,int y1,int y2,Bitmap image)
        {
            if (y1 == y2)
                y2++;

            Bitmap bmp=new Bitmap(x2-x1,y2 - y1);
            int subx = 0;
            int suby=0;
            for (int i = x1; i < x2; i++)
            {
                
                for (int j = y1; j < y2; j++)
                {
                    if (suby >=( y2-y1))
                    {
                        suby = 0;
                        if(subx <=( x2-x1))
                        subx++;
                    }
                    Color color = image.GetPixel(i, j);
                    bmp.SetPixel(i-x1,j-y1,color);
                    suby++;
                }
                
            }
            return bmp;
        }
    }
}
