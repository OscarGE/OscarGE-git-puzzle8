using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;



namespace Puzzle8
{
    class Nodo
    {
        public int[,] matrizEstado = new int[3, 3];
        private Localizacion locali1 = new Localizacion();
        private Localizacion locali2 = new Localizacion();
        private Localizacion locali3 = new Localizacion();
        private Localizacion locali4 = new Localizacion();
        private Localizacion locali5 = new Localizacion();
        private Localizacion locali6 = new Localizacion();
        private Localizacion locali7 = new Localizacion();
        private Localizacion locali8 = new Localizacion();
        private Localizacion locali9 = new Localizacion();
        
        private int h; // Número de piezas mal ubicadas.
        private int g; // Número de nodos avanzados;
        private int f; // h+g

        private int numCambio; //Número que se intercambia por el 9


        public Nodo(Localizacion locali1, Localizacion locali2, Localizacion locali3, Localizacion locali4, Localizacion locali5, Localizacion locali6, Localizacion locali7, Localizacion locali8, Localizacion locali9) 
       {
            this.matrizEstado[0, 0] = locali1.Num;
            this.matrizEstado[0, 1] = locali2.Num;
            this.matrizEstado[0, 2] = locali3.Num;
            this.matrizEstado[1, 0] = locali4.Num;
            this.matrizEstado[1, 1] = locali5.Num;
            this.matrizEstado[1, 2] = locali6.Num;
            this.matrizEstado[2, 0] = locali7.Num;
            this.matrizEstado[2, 1] = locali8.Num; 
            this.matrizEstado[2, 2] = locali9.Num;

            this.locali1 = locali1;
            this.locali2 = locali2;
            this.locali3 = locali3;
            this.locali4 = locali4;
            this.locali5 = locali5;
            this.locali6 = locali6;
            this.locali7 = locali7;
            this.locali8 = locali8;
            this.locali9 = locali9;
        }

        public Nodo(int num1, int num2, int num3, int num4, int num5, int num6, int num7, int num8, int num9, int h, int g)
        {
            this.matrizEstado[0, 0] = num1;
            this.matrizEstado[0, 1] = num2;
            this.matrizEstado[0, 2] = num3;
            this.matrizEstado[1, 0] = num4;
            this.matrizEstado[1, 1] = num5;
            this.matrizEstado[1, 2] = num6;
            this.matrizEstado[2, 0] = num7;
            this.matrizEstado[2, 1] = num8;
            this.matrizEstado[2, 2] = num9;

            this.h = h;
            this.g = g;
            this.f = this.h + this.g;
        }

        public Nodo(int num1, int num2, int num3, int num4, int num5, int num6, int num7, int num8, int num9, int h, int g, int numCambio)
        {
            this.matrizEstado[0, 0] = num1;
            this.matrizEstado[0, 1] = num2;
            this.matrizEstado[0, 2] = num3;
            this.matrizEstado[1, 0] = num4;
            this.matrizEstado[1, 1] = num5;
            this.matrizEstado[1, 2] = num6;
            this.matrizEstado[2, 0] = num7;
            this.matrizEstado[2, 1] = num8;
            this.matrizEstado[2, 2] = num9;

            this.h = h;
            this.g = g;
            this.f = this.h + this.g;
            this.numCambio = numCambio;
        }
        public Nodo()
        {

        }

        public int H { get => h; set => h = value; }
        public int G { get => g; set => g = value; }
        public int F { get => f; set => f = value; }
        public int NumCambio { get => numCambio; set => numCambio = value; }

        internal Localizacion Locali1 { get => locali1; set => locali1 = value; }
        internal Localizacion Locali2 { get => locali2; set => locali2 = value; }
        internal Localizacion Locali3 { get => locali3; set => locali3 = value; }
        internal Localizacion Locali4 { get => locali4; set => locali4 = value; }
        internal Localizacion Locali5 { get => locali5; set => locali5 = value; }
        internal Localizacion Locali6 { get => locali6; set => locali6 = value; }
        internal Localizacion Locali7 { get => locali7; set => locali7 = value; }
        internal Localizacion Locali8 { get => locali8; set => locali8 = value; }
        internal Localizacion Locali9 { get => locali9; set => locali9 = value; }
        

        public void imprimirMatriz()
        {
            for (int i = 0 ;i < 3; i++)
            {
               for(int j = 0; j < 3; j++)
                {
                    Debug.Write(matrizEstado[i,j]+" ");
                }
                Debug.Write("\n");
            }
        }

       

    }
}
