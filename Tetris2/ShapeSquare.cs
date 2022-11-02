using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris2
{
    public class ShapeSquare : Shape
    {
        public ShapeSquare()
        {
            width = 2;
            height = 2;
            dots = new int[,]
            {
                {1, 1 },
                {1, 1 }
            };
        }
    }
}
