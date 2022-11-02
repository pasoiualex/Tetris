using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris2
{
    public class ShapeLMirrored : Shape
    {
        public ShapeLMirrored()
        {
            width = 4;
            height = 2;
            dots = new int[,]
            {
                {   0, 0, 0, 1  },
                {   1, 1, 1, 1  }
            };
        }
    }
}
