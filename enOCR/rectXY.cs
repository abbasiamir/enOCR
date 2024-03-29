using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace enOCR
{
    public class rectXY
    {
       public int x1;
        public int y1;
        public int x2;
        public int y2;

        public rectXY()
        {
        }

        public rectXY(int X1, int Y1, int X2, int Y2)
        {
            this.x1 = X1;
            this.y1 = Y1;
            this.x2 = X2;
            this.y2 = Y2;
        }
    }
}
