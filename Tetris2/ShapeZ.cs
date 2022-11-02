using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris2
{
    public class ShapeZ : Shape
    {
        public ShapeZ()
        {
            width = 3;
            height = 2;
            dots = new int[,]
            {
                {   1, 1, 0},
                {   0, 1, 1}
            };
        }
    }
}
