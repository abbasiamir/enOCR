using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace enOCR
{
    public class word
    {
       public rectXY bounds = new rectXY();
       public String text;

        public word(rectXY b)
        {
            this.bounds = b;
        }

        String MakeText()
        {
            return null;
        }
    }
}
