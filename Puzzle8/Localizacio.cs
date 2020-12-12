using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle8
{
    class Localizacion
    {
        private int x;
        private int y;
        private int num;

        //Constructor
        public Localizacion(int x, int y, int num)
        {
            this.X = x;
            this.Y = y;
            this.Num = num;
        }
        public Localizacion()
        {

        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Num { get => num; set => num = value; }
    }
}
