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
    public partial class modoManual : Form
    {
        static Stopwatch SW = new Stopwatch(); //Cariable para el cronometro.
        public SoundPlayer musica = new SoundPlayer(@"Payday 2 - Backstab.wav");//musica



        //Variables de Localización X y Y en el tablero.
        //Primer renglón:
        const int UNO_X = 3, UNO_Y = 3,/**/ DOS_X = 109, DOS_Y = 3,/**/TRES_X = 215, TRES_Y = 3;
        //Primer renglón:
        const int CUATRO_X = 3, CUATRO_Y = 109,/**/ CINCO_X = 109, CINCO_Y = 109,/**/ SEIS_X = 215, SEIS_Y = 109;
        //Primer renglón:
        const int SIETE_X = 3, SIETE_Y = 215,/**/ OCHO_X = 109, OCHO_Y = 215,/**/ NUEVE_X = 215, NUEVE_Y = 215;

        static List<Nodo> movimientos = new List<Nodo>(); //Lista que alacena nodos con su respectiva configuración de tablero. 

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

        public static int unidad = 106; //Representa la distancia que hay entre localizaciones.

        static Nodo nodoMeta; //Variable que almacenara el nodo meta;

        public static int nivel1 = 5, nivel2 = 10, nivel3 = 50; //Represenata el número de intercambios aleatorios.
        public static int nivelSelect;
        public static int puntos=0, puntuacion=0, pistas=0, tiempo=0;//variables de puntuacion

        //Para las pistas 
        static List<Nodo> listaAbierta = new List<Nodo>();
        static int pistasCont = 0;


        public modoManual()
        {
            InitializeComponent();
            panel1.Enabled = false;
            CenterToScreen();
            comboNivel.Enabled = false;
            btnIniciarJuego.Enabled = false;
            panel2.Enabled = false;
            buttonNuevoJuego.Enabled = false;
            buttonPista.Enabled = false;
            labelCasilla.Enabled = false;
             
            musica.Play();
        }

        //Funciones=========================================================================================================================================================

        static void swapBotones(Button boton, Button botonVacio) //Función que intecambia un botón con el botón vacío.  
        {
            //swap de botón.
            //Variables temporales que almacenan las coordenadas X y Y de los botónes.
            int tempBotonX = boton.Location.X;
            int tempBotonY = boton.Location.Y;
            int tempBotonVacioX = botonVacio.Location.X;
            int tempBotonVacioY = botonVacio.Location.Y;
            //Se intercambiadno la localización de los botónes intercambiadno las variables temporales de sus coordenadas.
            boton.Location = new Point(tempBotonVacioX, tempBotonVacioY);
            botonVacio.Location = new Point(tempBotonX, tempBotonY);
        }
        static Localizacion obtenerLoacliBoton(int x, int y) //Función que retorna un objeto Localizacion dependiendo de sus coordenadas. 
        {
            if (x == locali1.X && y == locali1.Y)
            {
                return locali1;
            }
            else if (x == locali2.X && y == locali2.Y)
            {
                return locali2;
            }
            else if (x == locali3.X && y == locali3.Y)
            {
                return locali3;
            }
            else if (x == locali4.X && y == locali4.Y)
            {
                return locali4;
            }
            else if (x == locali5.X && y == locali5.Y)
            {
                return locali5;
            }
            else if (x == locali6.X && y == locali6.Y)
            {
                return locali6;
            }
            else if (x == locali7.X && y == locali7.Y)
            {
                return locali7;
            }
            else if (x == locali8.X && y == locali8.Y)
            {
                return locali8;
            }
            else if (x == locali9.X && y == locali9.Y)
            {
                return locali9;
            }
            else
            {
                MessageBox.Show("Error 101");
                return null;
            }
        }

        static Button obtenerBoton(int x, int y, Button button1, Button button2, Button button3, Button button4, Button button5, Button button6, Button button7, Button button8, Button button9) //Función que retorna un objeto Button dependiendo de sus coordenadas. 
        {
            if (x == button1.Location.X && y == button1.Location.Y) 
            {
                return button1;
            }
            else if (x == button2.Location.X && y == button2.Location.Y)
            {
                return button2;
            }
            else if (x == button3.Location.X && y == button3.Location.Y)
            {
                return button3;
            }
            else if (x == button4.Location.X && y == button4.Location.Y)
            {
                return button4;
            }
            else if (x == button5.Location.X && y == button5.Location.Y)
            {
                return button5;
            }
            else if (x == button6.Location.X && y == button6.Location.Y)
            {
                return button6;
            }
            else if (x == button7.Location.X && y == button7.Location.Y)
            {
                return button7;
            }
            else if (x == button8.Location.X && y == button8.Location.Y)
            {
                return button8;
            }
            else if (x == button9.Location.X && y == button9.Location.Y)
            {
                return button9;
            }
            else
            {
                MessageBox.Show("Error 101");
                return null;
            }
        }
        static void swapNumMat(Localizacion locali1, Localizacion locali2)//Función que intecambia el valor Num de los objetos Localizacion. 
        {
            //Intercambio importante para el atributo matrizEstado de los objetos Nodo.
            int tempNum = locali1.Num;
            locali1.Num = locali2.Num;
            locali2.Num = tempNum;
        }
        static bool swapPosible(Button boton, Button botonVacio)//Función que retorna un valor verdadero si un intercambio es vlido.
        {
            //Para que un intercambio sea vlido se determina si el botón vacío esta al lado del botón clickeado 
                //Verificando lado izquierdo 
            if (boton.Location.X + unidad == botonVacio.Location.X && boton.Location.Y == botonVacio.Location.Y)
            {
                return true;
            }
                //Verificando lado derecho
            else if (boton.Location.X - unidad == botonVacio.Location.X && boton.Location.Y == botonVacio.Location.Y)
            {
                return true;
            }
                //Verificando arriba
            else if (boton.Location.X == botonVacio.Location.X && boton.Location.Y - unidad == botonVacio.Location.Y)
            {
                return true;
            }
                //Verficando abajo
            else if (boton.Location.X == botonVacio.Location.X && boton.Location.Y + unidad == botonVacio.Location.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static void crearNuevoNodo()//Funcón que crea un nuevo objeto nodo y lo añade a la lista movimientos.
        {
            //Se inicializa con tosos los objetos locali para determinar su configuración en la matrizEstado. 
            Nodo nuevoNodo = new Nodo(locali1, locali2, locali3, locali4, locali5, locali6, locali7, locali8, locali9);
            movimientos.Add(nuevoNodo);
        }

        //Función que retorna una lista de enteros aleatorios(Funcion en desuso).
        static List<int> llenarListaDeAleatorios(List<int> listaNum, int inferior, int superior)
        {
            int numAleatorio; //Variable auxiliar para número aleatorio 

            for (int i = inferior; i < superior; i++) //For que llena la listaNum sin repetir números
            {
                Random rdn = new Random();
                numAleatorio = rdn.Next(1, 10);

                if (listaNum.Contains(numAleatorio))
                {
                    i--;
                }
                else
                {
                    listaNum.Add(numAleatorio);
                }

            }
            return listaNum;
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
        static void colorLabels( Label labelGray, Label labelBlack1, Label labelBlack2, Label labelBlack3, Label labelBlack4, Label labelBlack5, Label labelBlack6, Label labelBlack7, Label labelBlack8)
        {
            labelGray.ForeColor = Color.LightGray;
            labelGray.BackColor = Color.White;
            if(labelBlack1.Text == "1")
            {
                labelBlack1.BackColor = Color.Red;
                labelBlack1.ForeColor = Color.Black;
            }
            if(labelBlack2.Text == "2")
            {
                labelBlack2.BackColor = Color.LimeGreen;
                labelBlack2.ForeColor = Color.Black;
            }
            if (labelBlack3.Text == "3")
            {
                labelBlack3.BackColor = Color.DarkOrange;
                labelBlack3.ForeColor = Color.Black;
            }
            if (labelBlack4.Text == "4")
            {
                labelBlack4.BackColor = Color.RoyalBlue;
                labelBlack4.ForeColor = Color.Black;
            }
            if (labelBlack5.Text == "5")
            {
                labelBlack5.BackColor = Color.Purple;
                labelBlack5.ForeColor = Color.Black;
            }
            if (labelBlack6.Text == "6")
            {
                labelBlack6.BackColor = Color.Yellow;
                labelBlack6.ForeColor = Color.Black;
            }
            if (labelBlack7.Text == "7")
            {
                labelBlack7.BackColor = Color.Red;
                labelBlack7.ForeColor = Color.Black;
            }
            if (labelBlack8.Text == "8")
            {
                labelBlack8.BackColor = Color.LimeGreen;
                labelBlack8.ForeColor = Color.Black;
            }

        }

        static int coincidencias()
        {
            int cont = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (nodoMeta.matrizEstado[i, j] == movimientos[movimientos.Count - 1].matrizEstado[i, j])
                    {
                        cont++;
                    }
                }
            }
            return cont;

        }
        static void revolver(Button button1, Button button2, Button button3, Button button4, Button button5, Button button6, Button button7, Button button8, Button button9)
        {
            int numIntercambios = 0; //Variable que almacenará los valores de las variables de nivel.
            int numAleatorio; //Variable auxiliar para número aleatorio 
            Random rdn = new Random();
            Button temBoton;
            switch (nivelSelect)//Relaciona el valor nivelSelect para asignarle un valor a numIntercambios.
            {
                case 1: numIntercambios = nivel1; break;
                case 2: numIntercambios = nivel2; break;
                case 3: numIntercambios = nivel3; break;
            }


            while (numIntercambios > 0)//El ciclo se repetirá un número de veces correspondiente a las variables den nivel.
            {
                if (locali1.Num == 9)
                {
                    numAleatorio = rdn.Next(1, 2 + 1); //Dos posibles direcciones
                    switch (numAleatorio)
                    {
                        case 1: //Abajo
                            temBoton = obtenerBoton(CUATRO_X, CUATRO_Y, button1, button2, button3, button4, button5, button6, button7, button8, button9);
                            swapBotones(temBoton, button9);
                            swapNumMat(obtenerLoacliBoton(temBoton.Location.X, temBoton.Location.Y), obtenerLoacliBoton(button9.Location.X, button9.Location.Y));
                            break;
                        case 2: //Derecha
                            temBoton = obtenerBoton(DOS_X, DOS_Y, button1, button2, button3, button4, button5, button6, button7, button8, button9);
                            swapBotones(temBoton, button9);
                            swapNumMat(obtenerLoacliBoton(temBoton.Location.X, temBoton.Location.Y), obtenerLoacliBoton(button9.Location.X, button9.Location.Y));
                            break;
                    }
                    Debug.WriteLine("9 en Locali1, aleatorio: " + numAleatorio);
                }
                else if (locali2.Num == 9)
                {
                    numAleatorio = rdn.Next(1, 3 + 1); //Tres posibles direcciones
                    switch (numAleatorio)
                    {
                        case 1: //Izquierda
                            temBoton = obtenerBoton(UNO_X, UNO_Y, button1, button2, button3, button4, button5, button6, button7, button8, button9);
                            swapBotones(temBoton, button9);
                            swapNumMat(obtenerLoacliBoton(temBoton.Location.X, temBoton.Location.Y), obtenerLoacliBoton(button9.Location.X, button9.Location.Y));
                            break;
                        case 2: //Abajo
                            temBoton = obtenerBoton(CINCO_X, CINCO_Y, button1, button2, button3, button4, button5, button6, button7, button8, button9);
                            swapBotones(temBoton, button9);
                            swapNumMat(obtenerLoacliBoton(temBoton.Location.X, temBoton.Location.Y), obtenerLoacliBoton(button9.Location.X, button9.Location.Y));
                            break;
                        case 3: //Derecha
                            temBoton = obtenerBoton(TRES_X, TRES_Y, button1, button2, button3, button4, button5, button6, button7, button8, button9);
                            swapBotones(temBoton, button9);
                            swapNumMat(obtenerLoacliBoton(temBoton.Location.X, temBoton.Location.Y), obtenerLoacliBoton(button9.Location.X, button9.Location.Y));
                            break;

                    }
                    Debug.WriteLine("9 en Locali2, aleatorio: " + numAleatorio);
                }
                else if (locali3.Num == 9)
                {
                    numAleatorio = rdn.Next(1, 2 + 1); //Dos posibles direcciones
                    switch (numAleatorio)
                    {
                        case 1: //Izquierda
                            temBoton = obtenerBoton(DOS_X, DOS_Y, button1, button2, button3, button4, button5, button6, button7, button8, button9);
                            swapBotones(temBoton, button9);
                            swapNumMat(obtenerLoacliBoton(temBoton.Location.X, temBoton.Location.Y), obtenerLoacliBoton(button9.Location.X, button9.Location.Y));
                            break;
                        case 2: //Abajo
                            temBoton = obtenerBoton(SEIS_X, SEIS_Y, button1, button2, button3, button4, button5, button6, button7, button8, button9);
                            swapBotones(temBoton, button9);
                            swapNumMat(obtenerLoacliBoton(temBoton.Location.X, temBoton.Location.Y), obtenerLoacliBoton(button9.Location.X, button9.Location.Y));
                            break;
                    }
                    Debug.WriteLine("9 en Locali3, aleatorio: " + numAleatorio);
                }
                else if (locali4.Num == 9)
                {
                    numAleatorio = rdn.Next(1, 3 + 1); //Tres posibles direcciones
                    switch (numAleatorio)
                    {
                        case 1: //Arriba
                            temBoton = obtenerBoton(UNO_X, UNO_Y, button1, button2, button3, button4, button5, button6, button7, button8, button9);
                            swapBotones(temBoton, button9);
                            swapNumMat(obtenerLoacliBoton(temBoton.Location.X, temBoton.Location.Y), obtenerLoacliBoton(button9.Location.X, button9.Location.Y));
                            break;
                        case 2: //Abajo
                            temBoton = obtenerBoton(SIETE_X, SIETE_Y, button1, button2, button3, button4, button5, button6, button7, button8, button9);
                            swapBotones(temBoton, button9);
                            swapNumMat(obtenerLoacliBoton(temBoton.Location.X, temBoton.Location.Y), obtenerLoacliBoton(button9.Location.X, button9.Location.Y));
                            break;
                        case 3: //Derecha
                            temBoton = obtenerBoton(CINCO_X, CINCO_Y, button1, button2, button3, button4, button5, button6, button7, button8, button9);
                            swapBotones(temBoton, button9);
                            swapNumMat(obtenerLoacliBoton(temBoton.Location.X, temBoton.Location.Y), obtenerLoacliBoton(button9.Location.X, button9.Location.Y));
                            break;
                    }
                    Debug.WriteLine("9 en Locali4, aleatorio: " + numAleatorio);
                }
                else if (locali5.Num == 9)
                {
                    numAleatorio = rdn.Next(1, 4 + 1); //Cuatro posibles direcciones
                    switch (numAleatorio)
                    {
                        case 1: //Izquierda
                            temBoton = obtenerBoton(CUATRO_X, CUATRO_Y, button1, button2, button3, button4, button5, button6, button7, button8, button9);
                            swapBotones(temBoton, button9);
                            swapNumMat(obtenerLoacliBoton(temBoton.Location.X, temBoton.Location.Y), obtenerLoacliBoton(button9.Location.X, button9.Location.Y));
                            break;
                        case 2: //Abajo
                            temBoton = obtenerBoton(OCHO_X, OCHO_Y, button1, button2, button3, button4, button5, button6, button7, button8, button9);
                            swapBotones(temBoton, button9);
                            swapNumMat(obtenerLoacliBoton(temBoton.Location.X, temBoton.Location.Y), obtenerLoacliBoton(button9.Location.X, button9.Location.Y));
                            break;
                        case 3: //Derecha
                            temBoton = obtenerBoton(SEIS_X, SEIS_Y, button1, button2, button3, button4, button5, button6, button7, button8, button9);
                            swapBotones(temBoton, button9);
                            swapNumMat(obtenerLoacliBoton(temBoton.Location.X, temBoton.Location.Y), obtenerLoacliBoton(button9.Location.X, button9.Location.Y));
                            break;
                        case 4://Arriba
                            temBoton = obtenerBoton(DOS_X, DOS_Y, button1, button2, button3, button4, button5, button6, button7, button8, button9);
                            swapBotones(temBoton, button9);
                            swapNumMat(obtenerLoacliBoton(temBoton.Location.X, temBoton.Location.Y), obtenerLoacliBoton(button9.Location.X, button9.Location.Y));
                            break;
                    }
                    Debug.WriteLine("9 en Locali5, aleatorio: " + numAleatorio);
                }
                else if (locali6.Num == 9)
                {
                    numAleatorio = rdn.Next(1, 3 + 1); //Tres posibles direcciones
                    switch (numAleatorio)
                    {
                        case 1: //Izquierda
                            temBoton = obtenerBoton(CINCO_X, CINCO_Y, button1, button2, button3, button4, button5, button6, button7, button8, button9);
                            swapBotones(temBoton, button9);
                            swapNumMat(obtenerLoacliBoton(temBoton.Location.X, temBoton.Location.Y), obtenerLoacliBoton(button9.Location.X, button9.Location.Y));
                            break;
                        case 2: //Abajo
                            temBoton = obtenerBoton(NUEVE_X, NUEVE_Y, button1, button2, button3, button4, button5, button6, button7, button8, button9);
                            swapBotones(temBoton, button9);
                            swapNumMat(obtenerLoacliBoton(temBoton.Location.X, temBoton.Location.Y), obtenerLoacliBoton(button9.Location.X, button9.Location.Y));
                            break;
                        case 3: //Arriba
                            temBoton = obtenerBoton(TRES_X, TRES_Y, button1, button2, button3, button4, button5, button6, button7, button8, button9);
                            swapBotones(temBoton, button9);
                            swapNumMat(obtenerLoacliBoton(temBoton.Location.X, temBoton.Location.Y), obtenerLoacliBoton(button9.Location.X, button9.Location.Y));
                            break;
                    }
                    Debug.WriteLine("9 en Locali6, aleatorio: " + numAleatorio);
                }
                else if (locali7.Num == 9)
                {
                    numAleatorio = rdn.Next(1, 2 + 1); //Dos posibles direcciones
                    switch (numAleatorio)
                    {
                        case 1: //Arriba
                            temBoton = obtenerBoton(CUATRO_X, CUATRO_Y, button1, button2, button3, button4, button5, button6, button7, button8, button9);
                            swapBotones(temBoton, button9);
                            swapNumMat(obtenerLoacliBoton(temBoton.Location.X, temBoton.Location.Y), obtenerLoacliBoton(button9.Location.X, button9.Location.Y));
                            break;
                        case 2: //Derecha
                            temBoton = obtenerBoton(OCHO_X, OCHO_Y, button1, button2, button3, button4, button5, button6, button7, button8, button9);
                            swapBotones(temBoton, button9);
                            swapNumMat(obtenerLoacliBoton(temBoton.Location.X, temBoton.Location.Y), obtenerLoacliBoton(button9.Location.X, button9.Location.Y));
                            break;
                    }
                    Debug.WriteLine("9 en Locali7, aleatorio: " + numAleatorio);
                }
                else if (locali8.Num == 9)
                {
                    numAleatorio = rdn.Next(1, 3 + 1); //Tres posibles direcciones
                    switch (numAleatorio)
                    {
                        case 1: //Izquierda
                            temBoton = obtenerBoton(SIETE_X, SIETE_Y, button1, button2, button3, button4, button5, button6, button7, button8, button9);
                            swapBotones(temBoton, button9);
                            swapNumMat(obtenerLoacliBoton(temBoton.Location.X, temBoton.Location.Y), obtenerLoacliBoton(button9.Location.X, button9.Location.Y));
                            break;
                        case 2: //Arriba
                            temBoton = obtenerBoton(CINCO_X, CINCO_Y, button1, button2, button3, button4, button5, button6, button7, button8, button9);
                            swapBotones(temBoton, button9);
                            swapNumMat(obtenerLoacliBoton(temBoton.Location.X, temBoton.Location.Y), obtenerLoacliBoton(button9.Location.X, button9.Location.Y));
                            break;
                        case 3: //Derecha
                            temBoton = obtenerBoton(NUEVE_X, NUEVE_Y, button1, button2, button3, button4, button5, button6, button7, button8, button9);
                            swapBotones(temBoton, button9);
                            swapNumMat(obtenerLoacliBoton(temBoton.Location.X, temBoton.Location.Y), obtenerLoacliBoton(button9.Location.X, button9.Location.Y));
                            break;
                    }
                    Debug.WriteLine("9 en Locali8, aleatorio: " + numAleatorio);
                }
                else if (locali9.Num == 9)
                {
                    numAleatorio = rdn.Next(1, 2 + 1); //Dos posibles direcciones
                    switch (numAleatorio)
                    {
                        case 1: //Izquierda
                            temBoton = obtenerBoton(OCHO_X, OCHO_Y, button1, button2, button3, button4, button5, button6, button7, button8, button9);
                            swapBotones(temBoton, button9);
                            swapNumMat(obtenerLoacliBoton(temBoton.Location.X, temBoton.Location.Y), obtenerLoacliBoton(button9.Location.X, button9.Location.Y));
                            break;
                        case 2: //Arriba
                            temBoton = obtenerBoton(SEIS_X, SEIS_Y, button1, button2, button3, button4, button5, button6, button7, button8, button9);
                            swapBotones(temBoton, button9);
                            swapNumMat(obtenerLoacliBoton(temBoton.Location.X, temBoton.Location.Y), obtenerLoacliBoton(button9.Location.X, button9.Location.Y));
                            break;
                    }
                    Debug.WriteLine("9 en Locali9, aleatorio: " + numAleatorio);
                }
                numIntercambios--;
            }
        }
        static void restablecerModoManual(ComboBox comboConfigu, ComboBox comboNivel, Label labelPosi1, Label labelPosi2, Label labelPosi3, Label labelPosi4, Label labelPosi5, Label labelPosi6, Label labelPosi7, Label labelPosi8, Label labelPosi9, Button button1, Button button2, Button button3, Button button4, Button button5, Button button6, Button button7, Button button8, Button button9,Panel panel1,Panel panel2,Button btnIniciarJuego, Button buttonNuevoJuego,Button buttonPista,Label labelCasilla)
        {
            //Restableciendo comboConfigu
            comboConfigu.Items.Clear();
            comboConfigu.Items.Add("Inferior");
            comboConfigu.Items.Add("Superior");
            comboConfigu.Items.Add("Centro");
            comboConfigu.Items.Add("Vertical");
            comboConfigu.Items.Add("Caracol");
            comboConfigu.Enabled = true;
            //Restableciendo labelposi
            labelPosi1.Text = "  ";
            labelPosi1.BackColor = Color.White;
            labelPosi2.Text = "  ";
            labelPosi2.BackColor = Color.White;
            labelPosi3.Text = "  ";
            labelPosi3.BackColor = Color.White;
            labelPosi4.Text = "  ";
            labelPosi4.BackColor = Color.White;
            labelPosi5.Text = "  ";
            labelPosi5.BackColor = Color.White;
            labelPosi6.Text = "  ";
            labelPosi6.BackColor = Color.White;
            labelPosi7.Text = "  ";
            labelPosi7.BackColor = Color.White;
            labelPosi8.Text = "  ";
            labelPosi8.BackColor = Color.White;
            labelPosi9.Text = "  ";
            labelPosi9.BackColor = Color.White;
            //Restableciendo comboNivel
            comboNivel.Items.Clear();
            comboNivel.Items.Add("1");
            comboNivel.Items.Add("2");
            comboNivel.Items.Add("3");
            comboNivel.Enabled = false;
            //Restableciendo botónes
            List<int> configuEstandar = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };//Lista de configuración de panel; 
            List<Localizacion> configuEstandarListaLocali = new List<Localizacion>();//Lista con las localizaciones de configuraciones   
            configuEstandarListaLocali = llenarListaLocali(configuEstandarListaLocali, configuEstandar);//Se llenan las listas de configuraciones asignando valores a las coordenadas de los objetos locali
            actualizarLocalizacionDeBotones(configuEstandar, configuEstandarListaLocali, button1, button2, button3, button4, button5, button6, button7, button8, button9);
            panel1.Enabled = false;
            btnIniciarJuego.Enabled = false;
            buttonNuevoJuego.Enabled = false;
            //Restableciendo cronómetro
            SW.Stop();
            SW.Reset();
            panel2.Enabled = false;
            //Restableciendo lista de movimientos
            movimientos.Clear();
            //Restablecer pistas 
            buttonPista.Enabled = false;
            labelCasilla.Text = "";
            pistasCont = 0;
        }

        public void win(Timer timer1,Panel panel1)
        {
            panel1.Enabled = false;
            SW.Stop();
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, (int)SW.ElapsedMilliseconds);
            string horas = ts.Hours.ToString().Length < 2 ? "0" + ts.Hours.ToString() : ts.Hours.ToString();
            string minutos = ts.Minutes.ToString().Length < 2 ? "0" + ts.Minutes.ToString() : ts.Minutes.ToString();
            string segundos = ts.Seconds.ToString().Length < 2 ? "0" + ts.Seconds.ToString() : ts.Seconds.ToString();
            string totMovimientos = Convert.ToString(movimientos.Count() - 1);

            if (nivelSelect == 1)//puntuacion inicial
            {
                puntos = 500;

            }
            else if (nivelSelect == 2)
            {
                puntos = 1000;

            }
            else if (nivelSelect == 3)
            {
                puntos = 1500;

            }
            pistas = pistasCont;
            tiempo = ts.Seconds + (ts.Minutes * 60) + (ts.Hours * 3600);//tiempo del puntaje
            puntuacion = puntos - (((tiempo + movimientos.Count) * (pistas + 1)));//puntuacion final del juego
            Console.WriteLine(movimientos.Count);
            Console.WriteLine(tiempo);
            Console.WriteLine(puntos);
            Console.WriteLine(pistas);


            DialogResult resul = MessageBox.Show("¡¡¡VICTORIA!!!\n\nTotal de movimientos: " + totMovimientos + "\nTiempo: " + horas + ":" + minutos + ":" + segundos + "\nPuntaje: " + puntuacion + "\n\n¿Desea guardar su puntuación?", "Felicidades", MessageBoxButtons.YesNo);
            if (resul == DialogResult.Yes)
            {
                SW.Reset();
                timer1.Enabled = false;
                Registro registro = new Registro(puntuacion);
                musica.Stop();
                this.Visible = false;
                registro.Visible = true;

            }

        }

        static void expandir()
        {
            //Se almacena el numero en las cariables de casillas. 
            int casilla1 = movimientos[(movimientos.Count) - 1].matrizEstado[0, 0]; int casilla2 = movimientos[(movimientos.Count) - 1].matrizEstado[0, 1]; int casilla3 = movimientos[(movimientos.Count) - 1].matrizEstado[0, 2];
            int casilla4 = movimientos[(movimientos.Count) - 1].matrizEstado[1, 0]; int casilla5 = movimientos[(movimientos.Count) - 1].matrizEstado[1, 1]; int casilla6 = movimientos[(movimientos.Count) - 1].matrizEstado[1, 2];
            int casilla7 = movimientos[(movimientos.Count) - 1].matrizEstado[2, 0]; int casilla8 = movimientos[(movimientos.Count) - 1].matrizEstado[2, 1]; int casilla9 = movimientos[(movimientos.Count) - 1].matrizEstado[2, 2];

            if (movimientos[(movimientos.Count) - 1].matrizEstado[0, 0] == 9)//Se crean dos nodos
            {
                //Se crea una lista de configuración intercambiando la casilla1 con la casilla4. 
                List<int> abajo = new List<int> { casilla4, casilla2, casilla3, casilla1, casilla5, casilla6, casilla7, casilla8, casilla9 };
                Nodo nuevoNodo1 = new Nodo(abajo[0], abajo[1], abajo[2], abajo[3], abajo[4], abajo[5], abajo[6], abajo[7], abajo[8], diferencias(abajo), movimientos.Count, casilla4);
                listaAbierta.Add(nuevoNodo1);

                //Se crea una lista de configuración intercambiando la casilla1 con la casilla2. 
                List<int> derecha = new List<int> { casilla2, casilla1, casilla3, casilla4, casilla5, casilla6, casilla7, casilla8, casilla9 };
                Nodo nuevoNodo2 = new Nodo(derecha[0], derecha[1], derecha[2], derecha[3], derecha[4], derecha[5], derecha[6], derecha[7], derecha[8], diferencias(derecha), movimientos.Count, casilla2);
                listaAbierta.Add(nuevoNodo2);

                return;
            }
            else if (movimientos[(movimientos.Count) - 1].matrizEstado[0, 1] == 9)//Se crean tres nodos
            {
                //Se crea una lista de configuracón intercambiando la casilla2 con la casilla1
                List<int> izquierda = new List<int> { casilla2, casilla1, casilla3, casilla4, casilla5, casilla6, casilla7, casilla8, casilla9 };
                Nodo nuevoNodo1 = new Nodo(izquierda[0], izquierda[1], izquierda[2], izquierda[3], izquierda[4], izquierda[5], izquierda[6], izquierda[7], izquierda[8], diferencias(izquierda), movimientos.Count, casilla1);
                listaAbierta.Add(nuevoNodo1);

                //Se crea una lista de configuración intercambiando la casilla2 con la casilla5.
                List<int> abajo = new List<int> { casilla1, casilla5, casilla3, casilla4, casilla2, casilla6, casilla7, casilla8, casilla9 };
                Nodo nuevoNodo2 = new Nodo(abajo[0], abajo[1], abajo[2], abajo[3], abajo[4], abajo[5], abajo[6], abajo[7], abajo[8], diferencias(abajo), movimientos.Count, casilla5);
                listaAbierta.Add(nuevoNodo2);

                //Se crea una lista de configuración intercambiando la casilla2 con la casilla3.
                List<int> derecha = new List<int> { casilla1, casilla3, casilla2, casilla4, casilla5, casilla6, casilla7, casilla8, casilla9 };
                Nodo nuevoNodo3 = new Nodo(derecha[0], derecha[1], derecha[2], derecha[3], derecha[4], derecha[5], derecha[6], derecha[7], derecha[8], diferencias(derecha), movimientos.Count, casilla3);
                listaAbierta.Add(nuevoNodo3);

                return;
            }
            else if (movimientos[(movimientos.Count) - 1].matrizEstado[0, 2] == 9)//Se crean dos nodos
            {
                //Se crea una lista de configuración intercambiando la casilla3 con la casilla2.
                List<int> izquierda = new List<int> { casilla1, casilla3, casilla2, casilla4, casilla5, casilla6, casilla7, casilla8, casilla9 };
                Nodo nuevoNodo1 = new Nodo(izquierda[0], izquierda[1], izquierda[2], izquierda[3], izquierda[4], izquierda[5], izquierda[6], izquierda[7], izquierda[8], diferencias(izquierda), movimientos.Count, casilla2);
                listaAbierta.Add(nuevoNodo1);

                //Se crea una lista de configuración intercambiando la casilla3 con la casilla6.
                List<int> abajo = new List<int> { casilla1, casilla2, casilla6, casilla4, casilla5, casilla3, casilla7, casilla8, casilla9 };
                Nodo nuevoNodo2 = new Nodo(abajo[0], abajo[1], abajo[2], abajo[3], abajo[4], abajo[5], abajo[6], abajo[7], abajo[8], diferencias(abajo), movimientos.Count, casilla6);
                listaAbierta.Add(nuevoNodo2);

                return;
            }
            else if (movimientos[(movimientos.Count) - 1].matrizEstado[1, 0] == 9)//Se crean tres nodos
            {
                //Se crea una lista de configuración intercambiando la casilla4 con la casilla1.
                List<int> arriba = new List<int> { casilla4, casilla2, casilla3, casilla1, casilla5, casilla6, casilla7, casilla8, casilla9 };
                Nodo nuevoNodo1 = new Nodo(arriba[0], arriba[1], arriba[2], arriba[3], arriba[4], arriba[5], arriba[6], arriba[7], arriba[8], diferencias(arriba), movimientos.Count, casilla1);
                listaAbierta.Add(nuevoNodo1);

                //Se crea una lista de configuración intercambiando la casilla4 con la casilla7.
                List<int> abajo = new List<int> { casilla1, casilla2, casilla3, casilla7, casilla5, casilla6, casilla4, casilla8, casilla9 };
                Nodo nuevoNodo2 = new Nodo(abajo[0], abajo[1], abajo[2], abajo[3], abajo[4], abajo[5], abajo[6], abajo[7], abajo[8], diferencias(abajo), movimientos.Count, casilla7);
                listaAbierta.Add(nuevoNodo2);

                //Se crea una lista de configuración intercambiando la casilla4 con la casilla5.
                List<int> derecha = new List<int> { casilla1, casilla2, casilla3, casilla5, casilla4, casilla6, casilla7, casilla8, casilla9 };
                Nodo nuevoNodo3 = new Nodo(derecha[0], derecha[1], derecha[2], derecha[3], derecha[4], derecha[5], derecha[6], derecha[7], derecha[8], diferencias(derecha), movimientos.Count, casilla5);
                listaAbierta.Add(nuevoNodo3);

                return;
            }
            else if (movimientos[(movimientos.Count) - 1].matrizEstado[1, 1] == 9) //Se crean cuatro nodos
            {
                //Se crea una lista de configuración intercambiando la casilla5 con la casilla4.
                List<int> izquierda = new List<int> { casilla1, casilla2, casilla3, casilla5, casilla4, casilla6, casilla7, casilla8, casilla9 };
                Nodo nuevoNodo1 = new Nodo(izquierda[0], izquierda[1], izquierda[2], izquierda[3], izquierda[4], izquierda[5], izquierda[6], izquierda[7], izquierda[8], diferencias(izquierda), movimientos.Count, casilla4);
                listaAbierta.Add(nuevoNodo1);

                //Se crea una lista de configuración intercambiando la casilla5 con la casilla8.
                List<int> abajo = new List<int> { casilla1, casilla2, casilla3, casilla4, casilla8, casilla6, casilla7, casilla5, casilla9 };
                Nodo nuevoNodo2 = new Nodo(abajo[0], abajo[1], abajo[2], abajo[3], abajo[4], abajo[5], abajo[6], abajo[7], abajo[8], diferencias(abajo), movimientos.Count, casilla8);
                listaAbierta.Add(nuevoNodo2);

                //Se crea una lista de configuración intercambiando la casilla5 con la casilla6.
                List<int> derecha = new List<int> { casilla1, casilla2, casilla3, casilla4, casilla6, casilla5, casilla7, casilla8, casilla9 };
                Nodo nuevoNodo3 = new Nodo(derecha[0], derecha[1], derecha[2], derecha[3], derecha[4], derecha[5], derecha[6], derecha[7], derecha[8], diferencias(derecha), movimientos.Count, casilla6);
                listaAbierta.Add(nuevoNodo3);

                //Se crea una lista de configuración intercambiando la casilla5 con la casilla2.
                List<int> arriba = new List<int> { casilla1, casilla5, casilla3, casilla4, casilla2, casilla6, casilla7, casilla8, casilla9 };
                Nodo nuevoNodo4 = new Nodo(arriba[0], arriba[1], arriba[2], arriba[3], arriba[4], arriba[5], arriba[6], arriba[7], arriba[8], diferencias(arriba), movimientos.Count, casilla2);
                listaAbierta.Add(nuevoNodo4);

                return;
            }
            else if (movimientos[(movimientos.Count) - 1].matrizEstado[1, 2] == 9)//Se crean tres nodos
            {
                //Se crea una lista de configuración intercambiando la casilla6 con la casilla5.
                List<int> izquierda = new List<int> { casilla1, casilla2, casilla3, casilla4, casilla6, casilla5, casilla7, casilla8, casilla9 };
                Nodo nuevoNodo1 = new Nodo(izquierda[0], izquierda[1], izquierda[2], izquierda[3], izquierda[4], izquierda[5], izquierda[6], izquierda[7], izquierda[8], diferencias(izquierda), movimientos.Count, casilla5);
                listaAbierta.Add(nuevoNodo1);

                //Se crea una lista de configuración intercambiando la casilla6 con la casilla9.
                List<int> abajo = new List<int> { casilla1, casilla2, casilla3, casilla4, casilla5, casilla9, casilla7, casilla8, casilla6 };
                Nodo nuevoNodo2 = new Nodo(abajo[0], abajo[1], abajo[2], abajo[3], abajo[4], abajo[5], abajo[6], abajo[7], abajo[8], diferencias(abajo), movimientos.Count,casilla9);
                listaAbierta.Add(nuevoNodo2);

                //Se crea una lista de configuración intercambiando la casilla6 con la casilla3.
                List<int> arriba = new List<int> { casilla1, casilla2, casilla6, casilla4, casilla5, casilla3, casilla7, casilla8, casilla9 };
                Nodo nuevoNodo3 = new Nodo(arriba[0], arriba[1], arriba[2], arriba[3], arriba[4], arriba[5], arriba[6], arriba[7], arriba[8], diferencias(arriba), movimientos.Count, casilla3);
                listaAbierta.Add(nuevoNodo3);

                return;
            }
            else if (movimientos[(movimientos.Count) - 1].matrizEstado[2, 0] == 9)//Se crean dos nodos
            {
                //Se crea una lista de configuración intercambiando la casilla7 con la casilla4.
                List<int> arriba = new List<int> { casilla1, casilla2, casilla3, casilla7, casilla5, casilla6, casilla4, casilla8, casilla9 };
                Nodo nuevoNodo1 = new Nodo(arriba[0], arriba[1], arriba[2], arriba[3], arriba[4], arriba[5], arriba[6], arriba[7], arriba[8], diferencias(arriba), movimientos.Count, casilla4);
                listaAbierta.Add(nuevoNodo1);

                //Se crea una lista de configuración intercambiando la casilla7 con la casilla8.
                List<int> derecha = new List<int> { casilla1, casilla2, casilla3, casilla4, casilla5, casilla6, casilla8, casilla7, casilla9 };
                Nodo nuevoNodo2 = new Nodo(derecha[0], derecha[1], derecha[2], derecha[3], derecha[4], derecha[5], derecha[6], derecha[7], derecha[8], diferencias(derecha), movimientos.Count, casilla8);
                listaAbierta.Add(nuevoNodo2);
                return;
            }
            else if (movimientos[(movimientos.Count) - 1].matrizEstado[2, 1] == 9)//Se crean tres nodos
            {
                //Se crea una lista de configuración intercambiando la casilla8 con la casilla7.
                List<int> izquierda = new List<int> { casilla1, casilla2, casilla3, casilla4, casilla5, casilla6, casilla8, casilla7, casilla9 };
                Nodo nuevoNodo1 = new Nodo(izquierda[0], izquierda[1], izquierda[2], izquierda[3], izquierda[4], izquierda[5], izquierda[6], izquierda[7], izquierda[8], diferencias(izquierda), movimientos.Count, casilla7);
                listaAbierta.Add(nuevoNodo1);

                //Se crea una lista de configuración intercambiando la casilla8 con la casilla5.
                List<int> arriba = new List<int> { casilla1, casilla2, casilla3, casilla4, casilla8, casilla6, casilla7, casilla5, casilla9 };
                Nodo nuevoNodo2 = new Nodo(arriba[0], arriba[1], arriba[2], arriba[3], arriba[4], arriba[5], arriba[6], arriba[7], arriba[8], diferencias(arriba), movimientos.Count, casilla5);
                listaAbierta.Add(nuevoNodo2);

                //Se crea una lista de configuración intercambiando la casilla8 con la casilla9.
                List<int> derecha = new List<int> { casilla1, casilla2, casilla3, casilla4, casilla5, casilla6, casilla7, casilla9, casilla8 };
                Nodo nuevoNodo3 = new Nodo(derecha[0], derecha[1], derecha[2], derecha[3], derecha[4], derecha[5], derecha[6], derecha[7], derecha[8], diferencias(derecha), movimientos.Count, casilla9);
                listaAbierta.Add(nuevoNodo3);

                return;
            }
            else if (movimientos[(movimientos.Count) - 1].matrizEstado[2, 2] == 9)//Se crean dos nodos
            {
                //Se crea una lista de configuración intercambiando la casilla9 con la casilla8.
                List<int> izquierda = new List<int> { casilla1, casilla2, casilla3, casilla4, casilla5, casilla6, casilla7, casilla9, casilla8 };
                Nodo nuevoNodo1 = new Nodo(izquierda[0], izquierda[1], izquierda[2], izquierda[3], izquierda[4], izquierda[5], izquierda[6], izquierda[7], izquierda[8], diferencias(izquierda), movimientos.Count, casilla8);
                listaAbierta.Add(nuevoNodo1);

                //Se crea una lista de configuración intercambiando la casilla9 con la casilla6.
                List<int> arriba = new List<int> { casilla1, casilla2, casilla3, casilla4, casilla5, casilla9, casilla7, casilla8, casilla6 };
                Nodo nuevoNodo2 = new Nodo(arriba[0], arriba[1], arriba[2], arriba[3], arriba[4], arriba[5], arriba[6], arriba[7], arriba[8], diferencias(arriba), movimientos.Count, casilla6);
                listaAbierta.Add(nuevoNodo2);
                return;
            }
        }

       

        static int diferencias(List<int> lista)
        {
            int cont = 0;
            int k = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (nodoMeta.matrizEstado[i, j] != lista[k])
                    {
                        cont++;
                    }
                    k++;
                }
            }
            return cont - 1;
        }

        static Nodo menorF()
        {
            if (movimientos.Count == 1)
            {
                Nodo NodoPista = listaAbierta[0];
                for (int i = 0; i < listaAbierta.Count; i++)
                {
                    if (listaAbierta[i].F < NodoPista.F)
                    {
                        NodoPista = listaAbierta[i];
                    }
                }
                return NodoPista;
            }
            else
            {
                for (int i = 0; i < listaAbierta.Count; i++)
                {
                    if (coincidencias(movimientos[(movimientos.Count) - 2].matrizEstado, listaAbierta[i].matrizEstado) == 9)
                    {
                        listaAbierta.Remove(listaAbierta[i]);
                    }
                }


                Nodo NodoPista = listaAbierta[0];
                for (int i = 0; i < listaAbierta.Count; i++)
                {
                    if (listaAbierta[i].F < NodoPista.F)
                    {
                        NodoPista = listaAbierta[i];
                    }
                }
                return NodoPista;
            }

        }
        static int coincidencias(int[,] mat1, int[,] mat2)
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

        //Funciones de Click=========================================================================================================================================================

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = new TimeSpan(0, 0, 0,0,(int)SW.ElapsedMilliseconds);
            labelHora.Text = ts.Hours.ToString().Length < 2 ? "0" + ts.Hours.ToString() : ts.Hours.ToString();
            labelMinuto.Text = ts.Minutes.ToString().Length < 2 ? "0" + ts.Minutes.ToString() : ts.Minutes.ToString();
            labelSegundo.Text = ts.Seconds.ToString().Length < 2 ? "0" + ts.Seconds.ToString() : ts.Seconds.ToString();
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (swapPosible(button1, button9))
            {
                swapBotones(button1, button9);
                swapNumMat(obtenerLoacliBoton(button1.Location.X, button1.Location.Y), obtenerLoacliBoton(button9.Location.X, button9.Location.Y));
                crearNuevoNodo();
                if (coincidencias() == 9)
                {
                    win(timer1, panel1);
                }


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (swapPosible(button2, button9))
            {
                swapBotones(button2, button9);
                swapNumMat(obtenerLoacliBoton(button2.Location.X, button2.Location.Y), obtenerLoacliBoton(button9.Location.X, button9.Location.Y));
                crearNuevoNodo();
                if (coincidencias() == 9)
                {
                    win(timer1, panel1);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (swapPosible(button3, button9))
            {
                swapBotones(button3, button9);
                swapNumMat(obtenerLoacliBoton(button3.Location.X, button3.Location.Y), obtenerLoacliBoton(button9.Location.X, button9.Location.Y));
                crearNuevoNodo();
                if (coincidencias() == 9)
                {
                    win(timer1, panel1);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (swapPosible(button4, button9))
            {
                swapBotones(button4, button9);
                swapNumMat(obtenerLoacliBoton(button4.Location.X, button4.Location.Y), obtenerLoacliBoton(button9.Location.X, button9.Location.Y));
                crearNuevoNodo();
                if (coincidencias() == 9)
                {
                    win(timer1, panel1);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (swapPosible(button5, button9))
            {
                swapBotones(button5, button9);
                swapNumMat(obtenerLoacliBoton(button5.Location.X, button5.Location.Y), obtenerLoacliBoton(button9.Location.X, button9.Location.Y));
                crearNuevoNodo();
                if (coincidencias() == 9)
                {
                    win(timer1, panel1);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (swapPosible(button6, button9))
            {
                swapBotones(button6, button9);
                swapNumMat(obtenerLoacliBoton(button6.Location.X, button6.Location.Y), obtenerLoacliBoton(button9.Location.X, button9.Location.Y));
                crearNuevoNodo();
                if (coincidencias() == 9)
                {
                    win(timer1, panel1);
                }
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (swapPosible(button7, button9))
            {
                swapBotones(button7, button9);
                swapNumMat(obtenerLoacliBoton(button7.Location.X, button7.Location.Y), obtenerLoacliBoton(button9.Location.X, button9.Location.Y));
                crearNuevoNodo();
                if (coincidencias() == 9)
                {
                    win(timer1, panel1);
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (swapPosible(button8, button9))
            {
                swapBotones(button8, button9);
                swapNumMat(obtenerLoacliBoton(button8.Location.X, button8.Location.Y), obtenerLoacliBoton(button9.Location.X, button9.Location.Y));
                crearNuevoNodo();
                if (coincidencias() == 9)
                {
                    win(timer1, panel1);
                }
            }
        }

        private void btnDebug_Click(object sender, EventArgs e)//Imprime la lista de movimientos
        {
            for (int i=0; i<movimientos.Count;i++)
            {
                movimientos[i].imprimirMatriz();
                Debug.Write("\n");
            }
            Debug.Write("************************\n");
            Debug.Write("MATRIZ NODO META\n");
            nodoMeta.imprimirMatriz();
            Debug.Write("MATRIZ ULTIMA DEL LISTA NODO\n");
            movimientos[movimientos.Count() - 1].imprimirMatriz();
            Debug.Write("************************\n");
        }

        private void btnIniciarJuego_Click(object sender, EventArgs e)
        {
            SW.Start(); timer1.Enabled = true; //Inicia tiempo.
            

            comboConfigu.Enabled = false;
            comboNivel.Enabled = false;
            panel2.Enabled = true;
            buttonPista.Enabled = true;
            labelCasilla.Enabled = true
                ;

            revolver(button1,button2,button3,button4,button5,button6,button7,button8,button9);

            panel1.Enabled = true;
            crearNuevoNodo();
            btnIniciarJuego.Enabled = false;
            buttonNuevoJuego.Enabled = true;

        }

        private void comboConfigu_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboNivel.Enabled = true;
            if (comboConfigu.SelectedItem.Equals(comboConfigu.Items[0]))
            {
                List<int> configuMeta1 = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };//Lista de configuración de panel; 
                List<Localizacion> configu1ListaLocali = new List<Localizacion>();//Lista con las localizaciones de configuraciones   
                configu1ListaLocali = llenarListaLocali(configu1ListaLocali, configuMeta1);//Se llenan las listas de configuraciones asignando valores a las coordenadas de los objetos locali
                //Se muestran los numeros en las labelPosi
                labelPosi1.Text = configu1ListaLocali[0].Num.ToString();
                labelPosi2.Text = configu1ListaLocali[1].Num.ToString();
                labelPosi3.Text = configu1ListaLocali[2].Num.ToString();
                labelPosi4.Text = configu1ListaLocali[3].Num.ToString();
                labelPosi5.Text = configu1ListaLocali[4].Num.ToString();
                labelPosi6.Text = configu1ListaLocali[5].Num.ToString();
                labelPosi7.Text = configu1ListaLocali[6].Num.ToString();
                labelPosi8.Text = configu1ListaLocali[7].Num.ToString();
                labelPosi9.Text = configu1ListaLocali[8].Num.ToString();
                colorLabels(labelPosi9, labelPosi1, labelPosi2,labelPosi3, labelPosi4, labelPosi5, labelPosi6, labelPosi7,labelPosi8);
                nodoMeta = new Nodo(configu1ListaLocali[0], configu1ListaLocali[1], configu1ListaLocali[2], configu1ListaLocali[3], configu1ListaLocali[4], configu1ListaLocali[5], configu1ListaLocali[6], configu1ListaLocali[7], configu1ListaLocali[8]);
                actualizarLocalizacionDeBotones(configuMeta1, configu1ListaLocali, button1, button2, button3, button4, button5, button6, button7, button8, button9);
            }
            else if (comboConfigu.SelectedItem == comboConfigu.Items[1])
            {
                List<int> configuMeta2 = new List<int> { 9, 1, 2, 3, 4, 5, 6, 7, 8 };
                List<Localizacion> configu2ListaLocali = new List<Localizacion>();
                configu2ListaLocali = llenarListaLocali(configu2ListaLocali, configuMeta2);

                labelPosi1.Text = configu2ListaLocali[0].Num.ToString();
                labelPosi2.Text = configu2ListaLocali[1].Num.ToString();
                labelPosi3.Text = configu2ListaLocali[2].Num.ToString();
                labelPosi4.Text = configu2ListaLocali[3].Num.ToString();
                labelPosi5.Text = configu2ListaLocali[4].Num.ToString();
                labelPosi6.Text = configu2ListaLocali[5].Num.ToString();
                labelPosi7.Text = configu2ListaLocali[6].Num.ToString();
                labelPosi8.Text = configu2ListaLocali[7].Num.ToString();
                labelPosi9.Text = configu2ListaLocali[8].Num.ToString();
                colorLabels(labelPosi1, labelPosi2, labelPosi3, labelPosi4, labelPosi5, labelPosi6, labelPosi7, labelPosi8,labelPosi9);
                nodoMeta = new Nodo(configu2ListaLocali[0], configu2ListaLocali[1], configu2ListaLocali[2], configu2ListaLocali[3], configu2ListaLocali[4], configu2ListaLocali[5], configu2ListaLocali[6], configu2ListaLocali[7], configu2ListaLocali[8]);
                actualizarLocalizacionDeBotones(configuMeta2, configu2ListaLocali, button1, button2, button3, button4, button5, button6, button7, button8, button9);
            }
            else if (comboConfigu.SelectedItem == comboConfigu.Items[2])
            {
                List<int> configuMeta3 = new List<int> { 1, 2, 3, 4, 9, 5, 6, 7, 8 };
                List<Localizacion> configu3ListaLocali = new List<Localizacion>();
                configu3ListaLocali = llenarListaLocali(configu3ListaLocali, configuMeta3);

                labelPosi1.Text = configu3ListaLocali[0].Num.ToString();
                labelPosi2.Text = configu3ListaLocali[1].Num.ToString();
                labelPosi3.Text = configu3ListaLocali[2].Num.ToString();
                labelPosi4.Text = configu3ListaLocali[3].Num.ToString();
                labelPosi5.Text = configu3ListaLocali[4].Num.ToString();
                labelPosi6.Text = configu3ListaLocali[5].Num.ToString();
                labelPosi7.Text = configu3ListaLocali[6].Num.ToString();
                labelPosi8.Text = configu3ListaLocali[7].Num.ToString();
                labelPosi9.Text = configu3ListaLocali[8].Num.ToString();
                colorLabels(labelPosi5, labelPosi1, labelPosi2, labelPosi3, labelPosi4, labelPosi6, labelPosi7, labelPosi8, labelPosi9);
                nodoMeta = new Nodo(configu3ListaLocali[0], configu3ListaLocali[1], configu3ListaLocali[2], configu3ListaLocali[3], configu3ListaLocali[4], configu3ListaLocali[5], configu3ListaLocali[6], configu3ListaLocali[7], configu3ListaLocali[8]);
                actualizarLocalizacionDeBotones(configuMeta3, configu3ListaLocali, button1, button2, button3, button4, button5, button6, button7, button8, button9);
            }
            else if (comboConfigu.SelectedItem == comboConfigu.Items[3])
            {
                List<int> configuMeta4 = new List<int> { 1, 4, 7, 2, 5, 8, 3, 6, 9 };
                List<Localizacion> configu4ListaLocali = new List<Localizacion>();
                configu4ListaLocali = llenarListaLocali(configu4ListaLocali, configuMeta4);

                labelPosi1.Text = configu4ListaLocali[0].Num.ToString();
                labelPosi2.Text = configu4ListaLocali[1].Num.ToString();
                labelPosi3.Text = configu4ListaLocali[2].Num.ToString();
                labelPosi4.Text = configu4ListaLocali[3].Num.ToString();
                labelPosi5.Text = configu4ListaLocali[4].Num.ToString();
                labelPosi6.Text = configu4ListaLocali[5].Num.ToString();
                labelPosi7.Text = configu4ListaLocali[6].Num.ToString();
                labelPosi8.Text = configu4ListaLocali[7].Num.ToString();
                labelPosi9.Text = configu4ListaLocali[8].Num.ToString();
                colorLabels(labelPosi9, labelPosi1, labelPosi4, labelPosi7, labelPosi2, labelPosi5, labelPosi8, labelPosi3, labelPosi6);
                nodoMeta = new Nodo(configu4ListaLocali[0], configu4ListaLocali[1], configu4ListaLocali[2], configu4ListaLocali[3], configu4ListaLocali[4], configu4ListaLocali[5], configu4ListaLocali[6], configu4ListaLocali[7], configu4ListaLocali[8]);
                actualizarLocalizacionDeBotones(configuMeta4, configu4ListaLocali, button1, button2, button3, button4, button5, button6, button7, button8, button9);
            }
            else if (comboConfigu.SelectedItem == comboConfigu.Items[4])
            {
                List<int> configuMeta5 = new List<int> { 1, 2, 3, 8, 9, 4, 7, 6, 5 };
                List<Localizacion> configu5ListaLocali = new List<Localizacion>();
                configu5ListaLocali = llenarListaLocali(configu5ListaLocali, configuMeta5);

                labelPosi1.Text = configu5ListaLocali[0].Num.ToString();
                labelPosi2.Text = configu5ListaLocali[1].Num.ToString();
                labelPosi3.Text = configu5ListaLocali[2].Num.ToString();
                labelPosi4.Text = configu5ListaLocali[3].Num.ToString();
                labelPosi5.Text = configu5ListaLocali[4].Num.ToString();
                labelPosi6.Text = configu5ListaLocali[5].Num.ToString();
                labelPosi7.Text = configu5ListaLocali[6].Num.ToString();
                labelPosi8.Text = configu5ListaLocali[7].Num.ToString();
                labelPosi9.Text = configu5ListaLocali[8].Num.ToString();
                colorLabels(labelPosi5, labelPosi1, labelPosi2, labelPosi3, labelPosi6, labelPosi9, labelPosi8, labelPosi7, labelPosi4);
                nodoMeta = new Nodo(configu5ListaLocali[0], configu5ListaLocali[1], configu5ListaLocali[2], configu5ListaLocali[3], configu5ListaLocali[4], configu5ListaLocali[5], configu5ListaLocali[6], configu5ListaLocali[7], configu5ListaLocali[8]);
                actualizarLocalizacionDeBotones(configuMeta5, configu5ListaLocali, button1, button2, button3, button4, button5, button6, button7, button8, button9);
            }
        }
        private void comboNivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnIniciarJuego.Enabled = true;
            if (comboNivel.SelectedItem == comboNivel.Items[0])
            {
                nivelSelect = 1;
            }
            if (comboNivel.SelectedItem == comboNivel.Items[1])
            {
                nivelSelect = 2;
            }

            if (comboNivel.SelectedItem == comboNivel.Items[2])
            {
                nivelSelect = 3;
            }


        }

        private void buttonSalir2_Click(object sender, EventArgs e)
        {
            menuPrincipal menu = new menuPrincipal();
            menu.Dispose();
            this.Dispose();
            Application.Exit();
        }
        private void buttonRegresar_Click(object sender, EventArgs e)
        {
            restablecerModoManual(comboConfigu, comboNivel, labelPosi1, labelPosi2, labelPosi3, labelPosi4, labelPosi5, labelPosi6, labelPosi7, labelPosi8, labelPosi9, button1, button2, button3, button4, button5, button6, button7, button8, button9, panel1,panel2, btnIniciarJuego, buttonNuevoJuego, buttonPista, labelCasilla);
            menuPrincipal menu = new menuPrincipal();

            this.Visible = false;
            menu.Visible = true;
            musica.Stop();
        }

        private void buttonNuevoJuego_Click(object sender, EventArgs e)
        {
            DialogResult resul = MessageBox.Show("¿Seguro que quiere iniciar un nuevo juego?", "Nuevo juego", MessageBoxButtons.YesNo);
            if (resul == DialogResult.Yes)
            {
                restablecerModoManual(comboConfigu, comboNivel, labelPosi1, labelPosi2, labelPosi3, labelPosi4, labelPosi5, labelPosi6, labelPosi7, labelPosi8, labelPosi9, button1, button2, button3, button4, button5, button6, button7, button8, button9, panel1,panel2, btnIniciarJuego, buttonNuevoJuego,buttonPista,labelCasilla);
            }
        }
        private void buttonPista_Click(object sender, EventArgs e)
        {
            expandir();
            Nodo NodoPista = menorF();
            labelCasilla.Text = NodoPista.NumCambio.ToString();
            listaAbierta.Clear();
            pistasCont++;
        }

    }
}
