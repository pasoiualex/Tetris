using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris2
{
    public class Shape
    {
        public int width, height;

        public int[,] dots;

        public Panel[,] DrawShape(Panel[,] map, int x, int y)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (dots[i, j] == 1)
                    {
                        map[i, j].BackColor = Color.Red;
                    }
                }

            }

            return map;
        }

        public void Rotate()
        {
            var temp = height;
            height = width;
            width = temp;
        }
    }
}
