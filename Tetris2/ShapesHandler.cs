using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris2
{
    class ShapesHandler
    {
        Random r = new Random();

        public Shape GetRandomShape()
        {
            int nextNr = r.Next(100);
            if (nextNr % 7 == 0)
            {
                return new ShapeSquare();
            }
            else if (nextNr % 7 == 1)
            {
                return new ShapeLine();
            }
            else if (nextNr % 7 == 2)
            {
                return new ShapeZ();
            }
            else if (nextNr % 7 == 3)
            {
                return new ShapeS();
            }
            else if (nextNr % 7 == 4)
            {
                return new ShapeT();
            }
            else if (nextNr % 7 == 5)
            {
                return new ShapeL();
            }
            else
            {
                return new ShapeLMirrored();
            }
        }
    }
}
