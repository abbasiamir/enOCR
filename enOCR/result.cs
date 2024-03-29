using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace enOCR
{
    internal class result
    {
        public int xbegin = 0;
        public int xend = 0;
        public double persent = 0;
        public int index = 0;
        public string text = "";
        public result(int bg, int xe, double pr, int ind,string txt)
        {
            xbegin = bg;
            xend = xe;
            persent = pr;
            index = ind;
            text = txt;
        }
    }
}
