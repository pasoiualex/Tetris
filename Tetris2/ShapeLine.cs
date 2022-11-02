using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris2
{
    public class ShapeLine : Shape
    {
        public ShapeLine()
        {
            width = 1;
            height = 4;
            dots = new int[,]
            {
                {1 },
                {1 },
                {1 },
                {1 }
            };
        }
    }
}
