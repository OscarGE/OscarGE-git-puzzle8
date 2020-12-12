using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Media;

namespace Puzzle8
{
    public partial class modoInteligente : Form
    {
        public static List<int> listaInicioConfigu = new List<int>(); //Lista que almacena la configuración inicio
        public static int[,] matrizMetaConfigu = new int[3, 3];//Matriz que almacena la configuración meta

        static Stopwatch SW = new Stopwatch(); //Cariable para el cronometro.
        public SoundPlayer musica = new SoundPlayer(@"Payday 2 - Backstab.wav");//musica


        //Variables de Localización X y Y en el tablero.
        //Primer renglón:
        const int UNO_X = 3, UNO_Y = 3,/**/ DOS_X = 109, DOS_Y = 3,/**/TRES_X = 215, TRES_Y = 3;
        //Primer renglón:
        const int CUATRO_X = 3, CUATRO_Y = 109,/**/ CINCO_X = 109, CINCO_Y = 109,/**/ SEIS_X = 215, SEIS_Y = 109;
        //Primer renglón:
        const int SIETE_X = 3, SIETE_Y = 215,/**/ OCHO_X = 109, OCHO_Y = 215,/**/ NUEVE_X = 215, NUEVE_Y = 215;

        //Inicialización vacía de las objetos Localizacion.  
        static Localizacion locali1 = new Localizacion();
        static Localizacion locali2 = new Localizacion();

        static Localizacion locali3 = new Localizacion();
        static Localizacion locali4 = new Localizacion();
        static Localizacion locali5 = new Localizacion();
        static Localizacion locali6 = new Localizacion();
        static Localizacion locali7 = new Localizacion();
        static Localizacion locali8 = new Localizacion();
        static Localizacion locali9 = new Localizacion();

        //static Nodo nodoMeta; //Variable que almacenara el nodo meta;
        static Nodo nodoInicio; //Variable que almacenara el nodo inicio;

        static int puntero = 0;



        static List<Nodo> listaAbierta = new List<Nodo>();
        static List<Nodo> listaCerrada = new List<Nodo>();
        public modoInteligente()
        {
            InitializeComponent();
            CenterToScreen();
        }
        public modoInteligente(List<int> listaInicio, List<int> listaMeta)
        {
            InitializeComponent();
            CenterToScreen();
            musica.Play();
            listaInicioConfigu = listaInicio;
            int k = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++) 
                { 
                    matrizMetaConfigu[i,j] = listaMeta[k];
                    k++;
                }
            }

                    

            /*List<Localizacion> configuListaLocaliMeta = new List<Localizacion>();
            configuListaLocaliMeta = llenarListaLocali(configuListaLocaliMeta,listaMetaConfigu);
            nodoMeta = new Nodo(configuListaLocaliMeta[0], configuListaLocaliMeta[1], configuListaLocaliMeta[2], configuListaLocaliMeta[3], configuListaLocaliMeta[4], configuListaLocaliMeta[5], configuListaLocaliMeta[6], configuListaLocaliMeta[7], configuListaLocaliMeta[8]);
            */
            List<Localizacion> configuListaLocaliInicio = new List<Localizacion>();//Lista con las localizaciones de configuraciones   
            configuListaLocaliInicio = llenarListaLocali(configuListaLocaliInicio, listaInicioConfigu);
            nodoInicio = new Nodo(configuListaLocaliInicio[0], configuListaLocaliInicio[1], configuListaLocaliInicio[2], configuListaLocaliInicio[3], configuListaLocaliInicio[4], configuListaLocaliInicio[5], configuListaLocaliInicio[6], configuListaLocaliInicio[7], configuListaLocaliInicio[8]);

            labelPosi1.Text = listaMeta[0].ToString();
            labelPosi2.Text = listaMeta[1].ToString();
            labelPosi3.Text = listaMeta[2].ToString();
            labelPosi4.Text = listaMeta[3].ToString();
            labelPosi5.Text = listaMeta[4].ToString();
            labelPosi6.Text = listaMeta[5].ToString();
            labelPosi7.Text = listaMeta[6].ToString();
            labelPosi8.Text = listaMeta[7].ToString();
            labelPosi9.Text = listaMeta[8].ToString();

            colorLabels(labelPosi1, labelPosi2, labelPosi3, labelPosi4, labelPosi5, labelPosi6, labelPosi7, labelPosi8, labelPosi9);
            actualizarLocalizacionDeBotones(listaInicioConfigu, nodoInicio, button1, button2, button3, button4, button5, button6, button7, button8, button9);
            pictureBoxIzquierda.Enabled = false;
            pictureBoxDerecha.Enabled = false;
        }

        //Función que retorna una lista llena de los objetos locali.
        static List<Localizacion> llenarListaLocali(List<Localizacion> listaLocali, List<int> listaNum)
        {
            for (int i = 0; i < 9; i++)//For que inicializará los objetos Localizacion y los añadirá a la listaLocali
            {
                if (i == 0)
                {
                    locali1.X = UNO_X; locali1.Y = UNO_Y; locali1.Num = listaNum[i];
                    listaLocali.Add(locali1);
                }
                else if (i == 1)
                {
                    locali2.X = DOS_X; locali2.Y = DOS_Y; locali2.Num = listaNum[i];
                    listaLocali.Add(locali2);
                }
                else if (i == 2)
                {
                    locali3.X = TRES_X; locali3.Y = TRES_Y; locali3.Num = listaNum[i];
                    listaLocali.Add(locali3);
                }
                else if (i == 3)
                {
                    locali4.X = CUATRO_X; locali4.Y = CUATRO_Y; locali4.Num = listaNum[i];
                    listaLocali.Add(locali4);
                }
                else if (i == 4)
                {
                    locali5.X = CINCO_X; locali5.Y = CINCO_Y; locali5.Num = listaNum[i];
                    listaLocali.Add(locali5);
                }
                else if (i == 5)
                {
                    locali6.X = SEIS_X; locali6.Y = SEIS_Y; locali6.Num = listaNum[i];
                    listaLocali.Add(locali6);
                }
                else if (i == 6)
                {
                    locali7.X = SIETE_X; locali7.Y = SIETE_Y; locali7.Num = listaNum[i];
                    listaLocali.Add(locali7);
                }
                else if (i == 7)
                {
                    locali8.X = OCHO_X; locali8.Y = OCHO_Y; locali8.Num = listaNum[i];
                    listaLocali.Add(locali8);
                }
                else if (i == 8)
                {
                    locali9.X = NUEVE_X; locali9.Y = NUEVE_Y; locali9.Num = listaNum[i];
                    listaLocali.Add(locali9);
                }
            }
            return listaLocali;
        }

        static void actualizarLocalizacionDeBotones(List<int> listaNum, Nodo nodoInicio, Button button1, Button button2, Button button3, Button button4, Button button5, Button button6, Button button7, Button button8, Button button9)
        {
            List<Localizacion> listaLocali = new List<Localizacion>();
            listaLocali.Add(nodoInicio.Locali1);
            listaLocali.Add(nodoInicio.Locali2);
            listaLocali.Add(nodoInicio.Locali3);
            listaLocali.Add(nodoInicio.Locali4);
            listaLocali.Add(nodoInicio.Locali5);
            listaLocali.Add(nodoInicio.Locali6);
            listaLocali.Add(nodoInicio.Locali7);
            listaLocali.Add(nodoInicio.Locali8);
            listaLocali.Add(nodoInicio.Locali9);
            for (int i = 0; i < 9; i++)
            {
                if (listaNum[i] == 1)
                {
                    button1.Location = new Point(listaLocali[i].X, listaLocali[i].Y);
                }
                else if (listaNum[i] == 2)
                {
                    button2.Location = new Point(listaLocali[i].X, listaLocali[i].Y);
                }
                else if (listaNum[i] == 3)
                {
                    button3.Location = new Point(listaLocali[i].X, listaLocali[i].Y);
                }
                else if (listaNum[i] == 4)
                {
                    button4.Location = new Point(listaLocali[i].X, listaLocali[i].Y);
                }
                else if (listaNum[i] == 5)
                {
                    button5.Location = new Point(listaLocali[i].X, listaLocali[i].Y);
                }
                else if (listaNum[i] == 6)
                {
                    button6.Location = new Point(listaLocali[i].X, listaLocali[i].Y);
                }
                else if (listaNum[i] == 7)
                {
                    button7.Location = new Point(listaLocali[i].X, listaLocali[i].Y);
                }
                else if (listaNum[i] == 8)
                {
                    button8.Location = new Point(listaLocali[i].X, listaLocali[i].Y);
                }
                else if (listaNum[i] == 9)
                {
                    button9.Location = new Point(listaLocali[i].X, listaLocali[i].Y);
                }

            }
        }
        static void actualizarLocalizacionDeBotones(List<int> listaNum, List<Localizacion> listaLocali, Button button1, Button button2, Button button3, Button button4, Button button5, Button button6, Button button7, Button button8, Button button9)
        {
            for (int i = 0; i < 9; i++)
            {
                if (listaNum[i] == 1)
                {
                    button1.Location = new Point(listaLocali[i].X, listaLocali[i].Y);
                }
                else if (listaNum[i] == 2)
                {
                    button2.Location = new Point(listaLocali[i].X, listaLocali[i].Y);
                }
                else if (listaNum[i] == 3)
                {
                    button3.Location = new Point(listaLocali[i].X, listaLocali[i].Y);
                }
                else if (listaNum[i] == 4)
                {
                    button4.Location = new Point(listaLocali[i].X, listaLocali[i].Y);
                }
                else if (listaNum[i] == 5)
                {
                    button5.Location = new Point(listaLocali[i].X, listaLocali[i].Y);
                }
                else if (listaNum[i] == 6)
                {
                    button6.Location = new Point(listaLocali[i].X, listaLocali[i].Y);
                }
                else if (listaNum[i] == 7)
                {
                    button7.Location = new Point(listaLocali[i].X, listaLocali[i].Y);
                }
                else if (listaNum[i] == 8)
                {
                    button8.Location = new Point(listaLocali[i].X, listaLocali[i].Y);
                }
                else if (listaNum[i] == 9)
                {
                    button9.Location = new Point(listaLocali[i].X, listaLocali[i].Y);
                }

            }
        }

        static void colorLabels(Label label1, Label label2, Label label3, Label label4, Label label5, Label label6, Label label7, Label label8, Label label9)
        {
            switch (Int32.Parse(label1.Text))
            {
                case 1: label1.BackColor = Color.Red; label1.ForeColor = Color.Black; break;
                case 2: label1.BackColor = Color.LimeGreen; label1.ForeColor = Color.Black; break;
                case 3: label1.BackColor = Color.DarkOrange; label1.ForeColor = Color.Black; break;
                case 4: label1.BackColor = Color.RoyalBlue; label1.ForeColor = Color.Black; break;
                case 5: label1.BackColor = Color.Purple; label1.ForeColor = Color.Black; break;
                case 6: label1.BackColor = Color.Yellow; label1.ForeColor = Color.Black; break;
                case 7: label1.BackColor = Color.Red; label1.ForeColor = Color.Black; break;
                case 8: label1.BackColor = Color.LimeGreen; label1.ForeColor = Color.Black; break;
                case 9:label1.BackColor = Color.White; label1.ForeColor = Color.LightGray; break;
            }
            switch (Int32.Parse(label2.Text))
            {
                case 1: label2.BackColor = Color.Red; label2.ForeColor = Color.Black; break;
                case 2: label2.BackColor = Color.LimeGreen; label2.ForeColor = Color.Black; break;
                case 3: label2.BackColor = Color.DarkOrange; label2.ForeColor = Color.Black; break;
                case 4: label2.BackColor = Color.RoyalBlue; label2.ForeColor = Color.Black; break;
                case 5: label2.BackColor = Color.Purple; label2.ForeColor = Color.Black; break;
                case 6: label2.BackColor = Color.Yellow; label2.ForeColor = Color.Black; break;
                case 7: label2.BackColor = Color.Red; label2.ForeColor = Color.Black; break;
                case 8: label2.BackColor = Color.LimeGreen; label2.ForeColor = Color.Black; break;
                case 9: label2.BackColor = Color.White; label2.ForeColor = Color.LightGray; break;
            }
            switch (Int32.Parse(label3.Text))
            {
                case 1: label3.BackColor = Color.Red; label3.ForeColor = Color.Black; break;
                case 2: label3.BackColor = Color.LimeGreen; label3.ForeColor = Color.Black; break;
                case 3: label3.BackColor = Color.DarkOrange; label3.ForeColor = Color.Black; break;
                case 4: label3.BackColor = Color.RoyalBlue; label3.ForeColor = Color.Black; break;
                case 5: label3.BackColor = Color.Purple; label3.ForeColor = Color.Black; break;
                case 6: label3.BackColor = Color.Yellow; label3.ForeColor = Color.Black; break;
                case 7: label3.BackColor = Color.Red; label3.ForeColor = Color.Black; break;
                case 8: label3.BackColor = Color.LimeGreen; label3.ForeColor = Color.Black; break;
                case 9: label3.BackColor = Color.White; label3.ForeColor = Color.LightGray; break;
            }
            switch (Int32.Parse(label4.Text))
            {
                case 1: label4.BackColor = Color.Red; label4.ForeColor = Color.Black; break;
                case 2: label4.BackColor = Color.LimeGreen; label4.ForeColor = Color.Black; break;
                case 3: label4.BackColor = Color.DarkOrange; label4.ForeColor = Color.Black; break;
                case 4: label4.BackColor = Color.RoyalBlue; label4.ForeColor = Color.Black; break;
                case 5: label4.BackColor = Color.Purple; label4.ForeColor = Color.Black; break;
                case 6: label4.BackColor = Color.Yellow; label4.ForeColor = Color.Black; break;
                case 7: label4.BackColor = Color.Red; label4.ForeColor = Color.Black; break;
                case 8: label4.BackColor = Color.LimeGreen; label4.ForeColor = Color.Black; break;
                case 9: label4.BackColor = Color.White; label4.ForeColor = Color.LightGray; break;
            }
            switch (Int32.Parse(label5.Text))
            {
                case 1: label5.BackColor = Color.Red; label5.ForeColor = Color.Black; break;
                case 2: label5.BackColor = Color.LimeGreen; label5.ForeColor = Color.Black; break;
                case 3: label5.BackColor = Color.DarkOrange; label5.ForeColor = Color.Black; break;
                case 4: label5.BackColor = Color.RoyalBlue; label5.ForeColor = Color.Black; break;
                case 5: label5.BackColor = Color.Purple; label5.ForeColor = Color.Black; break;
                case 6: label5.BackColor = Color.Yellow; label5.ForeColor = Color.Black; break;
                case 7: label5.BackColor = Color.Red; label5.ForeColor = Color.Black; break;
                case 8: label5.BackColor = Color.LimeGreen; label5.ForeColor = Color.Black; break;
                case 9: label5.BackColor = Color.White; label5.ForeColor = Color.LightGray; break;
            }
            switch (Int32.Parse(label6.Text))
            {
                case 1: label6.BackColor = Color.Red; label6.ForeColor = Color.Black; break;
                case 2: label6.BackColor = Color.LimeGreen; label6.ForeColor = Color.Black; break;
                case 3: label6.BackColor = Color.DarkOrange; label6.ForeColor = Color.Black; break;
                case 4: label6.BackColor = Color.RoyalBlue; label6.ForeColor = Color.Black; break;
                case 5: label6.BackColor = Color.Purple; label6.ForeColor = Color.Black; break;
                case 6: label6.BackColor = Color.Yellow; label6.ForeColor = Color.Black; break;
                case 7: label6.BackColor = Color.Red; label6.ForeColor = Color.Black; break;
                case 8: label6.BackColor = Color.LimeGreen; label6.ForeColor = Color.Black; break;
                case 9: label6.BackColor = Color.White; label6.ForeColor = Color.LightGray; break;
            }
            switch (Int32.Parse(label7.Text))
            {
                case 1: label7.BackColor = Color.Red; label7.ForeColor = Color.Black; break;
                case 2: label7.BackColor = Color.LimeGreen; label7.ForeColor = Color.Black; break;
                case 3: label7.BackColor = Color.DarkOrange; label7.ForeColor = Color.Black; break;
                case 4: label7.BackColor = Color.RoyalBlue; label7.ForeColor = Color.Black; break;
                case 5: label7.BackColor = Color.Purple; label7.ForeColor = Color.Black; break;
                case 6: label7.BackColor = Color.Yellow; label7.ForeColor = Color.Black; break;
                case 7: label7.BackColor = Color.Red; label7.ForeColor = Color.Black; break;
                case 8: label7.BackColor = Color.LimeGreen; label7.ForeColor = Color.Black; break;
                case 9: label7.BackColor = Color.White; label7.ForeColor = Color.LightGray; break;
            }
            
            switch (Int32.Parse(label8.Text))
            {
                case 1: label8.BackColor = Color.Red; label8.ForeColor = Color.Black; break;
                case 2: label8.BackColor = Color.LimeGreen; label8.ForeColor = Color.Black; break;
                case 3: label8.BackColor = Color.DarkOrange; label8.ForeColor = Color.Black; break;
                case 4: label8.BackColor = Color.RoyalBlue; label8.ForeColor = Color.Black; break;
                case 5: label8.BackColor = Color.Purple; label8.ForeColor = Color.Black; break;
                case 6: label8.BackColor = Color.Yellow; label8.ForeColor = Color.Black; break;
                case 7: label8.BackColor = Color.Red; label8.ForeColor = Color.Black; break;
                case 8: label8.BackColor = Color.LimeGreen; label8.ForeColor = Color.Black; break;
                case 9: label8.BackColor = Color.White; label8.ForeColor = Color.LightGray; break;
            }
            switch (Int32.Parse(label9.Text))
            {
                case 1: label9.BackColor = Color.Red; label9.ForeColor = Color.Black; break;
                case 2: label9.BackColor = Color.LimeGreen; label9.ForeColor = Color.Black; break;
                case 3: label9.BackColor = Color.DarkOrange; label9.ForeColor = Color.Black; break;
                case 4: label9.BackColor = Color.RoyalBlue; label9.ForeColor = Color.Black; break;
                case 5: label9.BackColor = Color.Purple; label9.ForeColor = Color.Black; break;
                case 6: label9.BackColor = Color.Yellow; label9.ForeColor = Color.Black; break;
                case 7: label9.BackColor = Color.Red; label9.ForeColor = Color.Black; break;
                case 8: label9.BackColor = Color.LimeGreen; label9.ForeColor = Color.Black; break;
                case 9: label9.BackColor = Color.White; label9.ForeColor = Color.LightGray; break;
            }
        }

        static int coincidencias()
        {
            int cont = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (matrizMetaConfigu[i, j] == listaCerrada[(listaCerrada.Count)-1].matrizEstado[i, j])
                    {
                        cont++;
                    }
                }
            }
            return cont;

        }
        static int coincidencias(int[,] mat1, int[,]mat2)
        {
            int cont = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (mat1[i, j] == mat2[i, j])
                    {
                        cont++;
                    }
                }
            }
            return cont;
        }
        static int diferencias(List<int> lista)
        {
            int cont = 0;
            int k = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (matrizMetaConfigu[i, j] != lista[k])
                    {
                        cont++;
                    }
                    k++;
                }
            }
            return cont-1;
        }

        static void expandirYAñadirAListaAbierta()
        {
            //Se almacena el numero en las cariables de casillas. 
            int casilla1 = listaCerrada[(listaCerrada.Count) - 1].matrizEstado[0, 0]; int casilla2 = listaCerrada[(listaCerrada.Count) - 1].matrizEstado[0, 1]; int casilla3 = listaCerrada[(listaCerrada.Count) - 1].matrizEstado[0, 2];
            int casilla4 = listaCerrada[(listaCerrada.Count) - 1].matrizEstado[1, 0]; int casilla5 = listaCerrada[(listaCerrada.Count) - 1].matrizEstado[1, 1]; int casilla6 = listaCerrada[(listaCerrada.Count) - 1].matrizEstado[1, 2];
            int casilla7 = listaCerrada[(listaCerrada.Count) - 1].matrizEstado[2, 0]; int casilla8 = listaCerrada[(listaCerrada.Count) - 1].matrizEstado[2, 1]; int casilla9 = listaCerrada[(listaCerrada.Count) - 1].matrizEstado[2, 2];
            
            if (listaCerrada[(listaCerrada.Count)-1].matrizEstado[0,0] == 9)//Se crean dos nodos
            {
                //Se crea una lista de configuración intercambiando la casilla1 con la casilla4. 
                List<int> abajo = new List<int> {casilla4, casilla2 , casilla3 , casilla1 , casilla5 , casilla6 , casilla7 , casilla8 , casilla9};
                Nodo nuevoNodo1 = new Nodo(abajo[0], abajo[1], abajo[2], abajo[3], abajo[4], abajo[5], abajo[6], abajo[7], abajo[8], diferencias(abajo),listaCerrada.Count);
                listaAbierta.Add(nuevoNodo1);

                //Se crea una lista de configuración intercambiando la casilla1 con la casilla2. 
                List<int> derecha = new List<int> { casilla2, casilla1, casilla3, casilla4, casilla5, casilla6, casilla7, casilla8, casilla9 };
                Nodo nuevoNodo2 = new Nodo(derecha[0], derecha[1], derecha[2], derecha[3], derecha[4], derecha[5], derecha[6], derecha[7], derecha[8], diferencias(derecha), listaCerrada.Count);
                listaAbierta.Add(nuevoNodo2);

                return;
            }
            else if (listaCerrada[(listaCerrada.Count) - 1].matrizEstado[0, 1] == 9)//Se crean tres nodos
            {
                //Se crea una lista de configuracón intercambiando la casilla2 con la casilla1
                List<int> izquierda = new List<int> { casilla2, casilla1, casilla3, casilla4, casilla5, casilla6, casilla7, casilla8, casilla9 };
                Nodo nuevoNodo1 = new Nodo ( izquierda[0], izquierda[1], izquierda[2], izquierda[3], izquierda[4], izquierda[5], izquierda[6], izquierda[7], izquierda[8], diferencias(izquierda), listaCerrada.Count );
                listaAbierta.Add(nuevoNodo1);

                //Se crea una lista de configuración intercambiando la casilla2 con la casilla5.
                List<int> abajo = new List<int> { casilla1, casilla5, casilla3, casilla4, casilla2, casilla6, casilla7, casilla8, casilla9 };
                Nodo nuevoNodo2 = new Nodo(abajo[0], abajo[1], abajo[2], abajo[3], abajo[4], abajo[5], abajo[6], abajo[7], abajo[8], diferencias(abajo), listaCerrada.Count);
                listaAbierta.Add(nuevoNodo2);

                //Se crea una lista de configuración intercambiando la casilla2 con la casilla3.
                List<int> derecha = new List<int> { casilla1, casilla3, casilla2, casilla4, casilla5, casilla6, casilla7, casilla8, casilla9 };
                Nodo nuevoNodo3 = new Nodo(derecha[0], derecha[1], derecha[2], derecha[3], derecha[4], derecha[5], derecha[6], derecha[7], derecha[8], diferencias(derecha), listaCerrada.Count);
                listaAbierta.Add(nuevoNodo3);

                return;
            }
            else if (listaCerrada[(listaCerrada.Count) - 1].matrizEstado[0, 2] == 9)//Se crean dos nodos
            {
                //Se crea una lista de configuración intercambiando la casilla3 con la casilla2.
                List<int> izquierda = new List<int> { casilla1, casilla3, casilla2, casilla4, casilla5, casilla6, casilla7, casilla8, casilla9 };
                Nodo nuevoNodo1 = new Nodo(izquierda[0], izquierda[1], izquierda[2], izquierda[3], izquierda[4], izquierda[5], izquierda[6], izquierda[7], izquierda[8], diferencias(izquierda), listaCerrada.Count);
                listaAbierta.Add(nuevoNodo1);

                //Se crea una lista de configuración intercambiando la casilla3 con la casilla6.
                List<int> abajo = new List<int> { casilla1, casilla2, casilla6, casilla4, casilla5, casilla3, casilla7, casilla8, casilla9 };
                Nodo nuevoNodo2 = new Nodo(abajo[0], abajo[1], abajo[2], abajo[3], abajo[4], abajo[5], abajo[6], abajo[7], abajo[8], diferencias(abajo), listaCerrada.Count);
                listaAbierta.Add(nuevoNodo2);

                return;
            }
            else if (listaCerrada[(listaCerrada.Count) - 1].matrizEstado[1, 0] == 9)//Se crean tres nodos
            {
                //Se crea una lista de configuración intercambiando la casilla4 con la casilla1.
                List<int> arriba = new List<int> { casilla4, casilla2, casilla3, casilla1, casilla5, casilla6, casilla7, casilla8, casilla9 };
                Nodo nuevoNodo1 = new Nodo(arriba[0], arriba[1], arriba[2], arriba[3], arriba[4], arriba[5], arriba[6], arriba[7], arriba[8], diferencias(arriba), listaCerrada.Count);
                listaAbierta.Add(nuevoNodo1);

                //Se crea una lista de configuración intercambiando la casilla4 con la casilla7.
                List<int> abajo = new List<int> { casilla1, casilla2, casilla3, casilla7, casilla5, casilla6, casilla4, casilla8, casilla9 };
                Nodo nuevoNodo2 = new Nodo(abajo[0], abajo[1], abajo[2], abajo[3], abajo[4], abajo[5], abajo[6], abajo[7], abajo[8], diferencias(abajo), listaCerrada.Count);
                listaAbierta.Add(nuevoNodo2);

                //Se crea una lista de configuración intercambiando la casilla4 con la casilla5.
                List<int> derecha = new List<int> { casilla1, casilla2, casilla3, casilla5, casilla4, casilla6, casilla7, casilla8, casilla9 };
                Nodo nuevoNodo3 = new Nodo(derecha[0], derecha[1], derecha[2], derecha[3], derecha[4], derecha[5], derecha[6], derecha[7], derecha[8], diferencias(derecha), listaCerrada.Count);
                listaAbierta.Add(nuevoNodo3);

                return;
            }
            else if (listaCerrada[(listaCerrada.Count) - 1].matrizEstado[1, 1] == 9) //Se crean cuatro nodos
            {
                //Se crea una lista de configuración intercambiando la casilla5 con la casilla4.
                List<int> izquierda = new List<int> { casilla1, casilla2, casilla3, casilla5, casilla4, casilla6, casilla7, casilla8, casilla9 };
                Nodo nuevoNodo1 = new Nodo(izquierda[0], izquierda[1], izquierda[2], izquierda[3], izquierda[4], izquierda[5], izquierda[6], izquierda[7], izquierda[8], diferencias(izquierda), listaCerrada.Count);
                listaAbierta.Add(nuevoNodo1);

                //Se crea una lista de configuración intercambiando la casilla5 con la casilla8.
                List<int> abajo = new List<int> { casilla1, casilla2, casilla3, casilla4, casilla8, casilla6, casilla7, casilla5, casilla9 };
                Nodo nuevoNodo2 = new Nodo(abajo[0], abajo[1], abajo[2], abajo[3], abajo[4], abajo[5], abajo[6], abajo[7], abajo[8], diferencias(abajo), listaCerrada.Count);
                listaAbierta.Add(nuevoNodo2);

                //Se crea una lista de configuración intercambiando la casilla5 con la casilla6.
                List<int> derecha = new List<int> { casilla1, casilla2, casilla3, casilla4, casilla6, casilla5, casilla7, casilla8, casilla9 };
                Nodo nuevoNodo3 = new Nodo(derecha[0], derecha[1], derecha[2], derecha[3], derecha[4], derecha[5], derecha[6], derecha[7], derecha[8], diferencias(derecha), listaCerrada.Count);
                listaAbierta.Add(nuevoNodo3);

                //Se crea una lista de configuración intercambiando la casilla5 con la casilla2.
                List<int> arriba = new List<int> { casilla1, casilla5, casilla3, casilla4, casilla2, casilla6, casilla7, casilla8, casilla9 };
                Nodo nuevoNodo4 = new Nodo(arriba[0], arriba[1], arriba[2], arriba[3], arriba[4], arriba[5], arriba[6], arriba[7], arriba[8], diferencias(arriba), listaCerrada.Count);
                listaAbierta.Add(nuevoNodo4);

                return;
            }
            else if (listaCerrada[(listaCerrada.Count) - 1].matrizEstado[1, 2] == 9)//Se crean tres nodos
            {
                //Se crea una lista de configuración intercambiando la casilla6 con la casilla5.
                List<int> izquierda = new List<int> { casilla1, casilla2, casilla3, casilla4, casilla6, casilla5, casilla7, casilla8, casilla9 };
                Nodo nuevoNodo1 = new Nodo(izquierda[0], izquierda[1], izquierda[2], izquierda[3], izquierda[4], izquierda[5], izquierda[6], izquierda[7], izquierda[8], diferencias(izquierda), listaCerrada.Count);
                listaAbierta.Add(nuevoNodo1);

                //Se crea una lista de configuración intercambiando la casilla6 con la casilla9.
                List<int> abajo = new List<int> { casilla1, casilla2, casilla3, casilla4, casilla5, casilla9, casilla7, casilla8, casilla6 };
                Nodo nuevoNodo2 = new Nodo(abajo[0], abajo[1], abajo[2], abajo[3], abajo[4], abajo[5], abajo[6], abajo[7], abajo[8], diferencias(abajo), listaCerrada.Count);
                listaAbierta.Add(nuevoNodo2);

                //Se crea una lista de configuración intercambiando la casilla6 con la casilla3.
                List<int> arriba = new List<int> { casilla1, casilla2, casilla6, casilla4, casilla5, casilla3, casilla7, casilla8, casilla9 };
                Nodo nuevoNodo3 = new Nodo(arriba[0], arriba[1], arriba[2], arriba[3], arriba[4], arriba[5], arriba[6], arriba[7], arriba[8], diferencias(arriba), listaCerrada.Count);
                listaAbierta.Add(nuevoNodo3);

                return;
            }
            else if (listaCerrada[(listaCerrada.Count) - 1].matrizEstado[2, 0] == 9)//Se crean dos nodos
            {
                //Se crea una lista de configuración intercambiando la casilla7 con la casilla4.
                List<int> arriba = new List<int> { casilla1, casilla2, casilla3, casilla7, casilla5, casilla6, casilla4, casilla8, casilla9 };
                Nodo nuevoNodo1 = new Nodo(arriba[0], arriba[1], arriba[2], arriba[3], arriba[4], arriba[5], arriba[6], arriba[7], arriba[8], diferencias(arriba), listaCerrada.Count);
                listaAbierta.Add(nuevoNodo1);

                //Se crea una lista de configuración intercambiando la casilla7 con la casilla8.
                List<int> derecha = new List<int> { casilla1, casilla2, casilla3, casilla4, casilla5, casilla6, casilla8, casilla7, casilla9 };
                Nodo nuevoNodo2 = new Nodo(derecha[0], derecha[1], derecha[2], derecha[3], derecha[4], derecha[5], derecha[6], derecha[7], derecha[8], diferencias(derecha), listaCerrada.Count);
                listaAbierta.Add(nuevoNodo2);
                return;
            }
            else if (listaCerrada[(listaCerrada.Count) - 1].matrizEstado[2, 1] == 9)//Se crean tres nodos
            {
                //Se crea una lista de configuración intercambiando la casilla8 con la casilla7.
                List<int> izquierda = new List<int> { casilla1, casilla2, casilla3, casilla4, casilla5, casilla6, casilla8, casilla7, casilla9 };
                Nodo nuevoNodo1 = new Nodo(izquierda[0], izquierda[1], izquierda[2], izquierda[3], izquierda[4], izquierda[5], izquierda[6], izquierda[7], izquierda[8], diferencias(izquierda), listaCerrada.Count);
                listaAbierta.Add(nuevoNodo1);

                //Se crea una lista de configuración intercambiando la casilla8 con la casilla5.
                List<int> arriba = new List<int> { casilla1, casilla2, casilla3, casilla4, casilla8, casilla6, casilla7, casilla5, casilla9 };
                Nodo nuevoNodo2 = new Nodo(arriba[0], arriba[1], arriba[2], arriba[3], arriba[4], arriba[5], arriba[6], arriba[7], arriba[8], diferencias(arriba), listaCerrada.Count);
                listaAbierta.Add(nuevoNodo2);

                //Se crea una lista de configuración intercambiando la casilla8 con la casilla9.
                List<int> derecha = new List<int> { casilla1, casilla2, casilla3, casilla4, casilla5, casilla6, casilla7, casilla9, casilla8 };
                Nodo nuevoNodo3 = new Nodo(derecha[0], derecha[1], derecha[2], derecha[3], derecha[4], derecha[5], derecha[6], derecha[7], derecha[8], diferencias(derecha), listaCerrada.Count);
                listaAbierta.Add(nuevoNodo3);

                return;
            }
            else if (listaCerrada[(listaCerrada.Count) - 1].matrizEstado[2, 2] == 9)//Se crean dos nodos
            {
                //Se crea una lista de configuración intercambiando la casilla9 con la casilla8.
                List<int> izquierda = new List<int> { casilla1, casilla2, casilla3, casilla4, casilla5, casilla6, casilla7, casilla9, casilla8 };
                Nodo nuevoNodo1 = new Nodo(izquierda[0], izquierda[1], izquierda[2], izquierda[3], izquierda[4], izquierda[5], izquierda[6], izquierda[7], izquierda[8], diferencias(izquierda), listaCerrada.Count);
                listaAbierta.Add(nuevoNodo1);

                //Se crea una lista de configuración intercambiando la casilla9 con la casilla6.
                List<int> arriba = new List<int> { casilla1, casilla2, casilla3, casilla4, casilla5, casilla9, casilla7, casilla8, casilla6 };
                Nodo nuevoNodo2 = new Nodo(arriba[0], arriba[1], arriba[2], arriba[3], arriba[4], arriba[5], arriba[6], arriba[7], arriba[8], diferencias(arriba), listaCerrada.Count);
                listaAbierta.Add(nuevoNodo2);
                return;
            }
        }
        static Nodo menorF()
        {
            if (listaCerrada.Count==1)
            {
                Nodo nuevoNodoCerrado = listaAbierta[0];
                for (int i=0; i<listaAbierta.Count;i++)
                {
                    if (listaAbierta[i].F<nuevoNodoCerrado.F)
                    {
                        nuevoNodoCerrado = listaAbierta[i];
                    }
                }
                return nuevoNodoCerrado;
            }
            else
            {
                for (int i=0; i<listaAbierta.Count;i++)
                {
                    if (coincidencias(listaCerrada[(listaCerrada.Count) - 2].matrizEstado, listaAbierta[i].matrizEstado)==9)
                    {
                        listaAbierta.Remove(listaAbierta[i]);
                    }
                }
                

                Nodo nuevoNodoCerrado = listaAbierta[0];
                for (int i = 0; i < listaAbierta.Count; i++)
                {
                    if (listaAbierta[i].F < nuevoNodoCerrado.F)
                    {
                        nuevoNodoCerrado = listaAbierta[i];
                    }
                }
                return nuevoNodoCerrado;
            }
        }

        //Botónes=================================================================================================================================================
        private void btnResolver_Click(object sender, EventArgs e)
        {
            
            listaCerrada.Add(nodoInicio); //Se añade el nodo inicio a la lista cerrada.

            while (coincidencias() != 9 && listaCerrada.Count<100)
            {
                expandirYAñadirAListaAbierta();
                listaCerrada.Add(menorF());
                listaAbierta.Clear();
            }
            if (listaCerrada.Count >= 99)
            {
                MessageBox.Show("La lista cerrada ha superado los 100 nodos","Error");
                musica.Stop();
            }
            else
            {
                MessageBox.Show("El puzzle ha sido resuelto", "Bien");
                List<Localizacion> configuListaLocali = new List<Localizacion>();//Lista con las localizaciones de configuraciones   
                List<int> listaNum = new List<int>(); 
                for (int i=0; i<3;i++)
                {
                    for(int j = 0; j < 3; j++)
                    {
                        listaNum.Add(listaCerrada.Last().matrizEstado[i,j]);
                    }
                }
                configuListaLocali = llenarListaLocali(configuListaLocali, listaNum);//Se llenan las listas de configuraciones asignando valores a las coordenadas de los objetos locali
                actualizarLocalizacionDeBotones(listaNum, configuListaLocali, button1, button2, button3, button4, button5, button6, button7, button8, button9);
                pictureBoxIzquierda.Enabled = true;
                pictureBoxDerecha.Enabled = true;
                puntero = listaCerrada.Count;
                labelPuntero.Text = puntero.ToString();
                btnResolver.Enabled = false;
            }
        }
        private void buttonRegresar_Click(object sender, EventArgs e)
        {
            menuPrincipal menu = new menuPrincipal();
            this.Visible = false;
            menu.Visible = true;
            musica.Stop();
        }

        private void buttonSalir2_Click(object sender, EventArgs e)
        {
            menuPrincipal menu = new menuPrincipal();
            menu.Dispose();
            this.Dispose();
            Application.Exit();
        }

        private void pictureBoxIzquierda_Click(object sender, EventArgs e)
        {
            if (puntero != 1)
            {
                puntero--;
                List<Localizacion> configuListaLocali = new List<Localizacion>();//Lista con las localizaciones de configuraciones   
                List<int> listaNum = new List<int>();
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        listaNum.Add(listaCerrada[puntero-1].matrizEstado[i,j]);
                    }
                }
                configuListaLocali = llenarListaLocali(configuListaLocali, listaNum);//Se llenan las listas de configuraciones asignando valores a las coordenadas de los objetos locali
                actualizarLocalizacionDeBotones(listaNum, configuListaLocali, button1, button2, button3, button4, button5, button6, button7, button8, button9);
                labelPuntero.Text = puntero.ToString();
            }
        }

        private void pictureBoxDerecha_Click(object sender, EventArgs e)
        {
            if(puntero != listaCerrada.Count)
            {
                puntero++;
                List<Localizacion> configuListaLocali = new List<Localizacion>();//Lista con las localizaciones de configuraciones   
                List<int> listaNum = new List<int>();
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        listaNum.Add(listaCerrada[puntero - 1].matrizEstado[i, j]);
                    }
                }
                configuListaLocali = llenarListaLocali(configuListaLocali, listaNum);//Se llenan las listas de configuraciones asignando valores a las coordenadas de los objetos locali
                actualizarLocalizacionDeBotones(listaNum, configuListaLocali, button1, button2, button3, button4, button5, button6, button7, button8, button9);
                labelPuntero.Text = puntero.ToString();
            }
            
        }
    }
}
