using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris2
{
    public class Colors
    {
        public static Color GetColor(int nr)
        {
            switch (nr)
            {
                case 0:
                    return Color.Gray;
                case 1:
                    return Color.Blue;
                case 2:
                    return Color.Red;
                case 3:
                    return Color.Green;
                case 4:
                    return Color.Black;
                default:
                    return Color.Brown;
            }
        }
    }
}
