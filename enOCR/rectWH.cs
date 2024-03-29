using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace enOCR
{
    public class rectWH
    {
        int x;
        int y;
        int w;
        int h;

        public rectWH(int X, int Y, int W, int H)
        {
            this.x = X;
            this.y = Y;
            this.w = W;
            this.h = H;
        }

        public rectWH()
        {
        }
    }
}
