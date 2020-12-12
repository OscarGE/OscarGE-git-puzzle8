using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Puzzle8
{
    public partial class Tabla : Form
    {
        public struct usuarios//estructura de datos para usuario
        {
            //variables del usuario
            public int numero;
            public string nombre;
            public int puntuacion;
            public string fecha;
        };

        /// <summary>
        /// /////////////////////////////////////////////////////////////////Variables//////////////////
        /// </summary>
        TextWriter archivo;//archivo que usaremos 

        public List<usuarios> listausuarios = new List<usuarios>();//lista para los usuarios que leemos del archivo Tablapuntuaciones.txt
        public List<usuarios> listaorden = new List<usuarios>();//lista para los usuarios una vez ordenados por puntuacion descendente
        IEnumerable<usuarios> variabledeorden;//guarda los usuarios en orden de lectura luego los ordena por puntuacion
        public usuarios primero, temporal, entrante; //3 usuarios que usaremos 
        //primero es el usuario en caso de que no exista Tablapuntuaciones.txt osea el primer usuario en guardar puntuacion
        //temporal es el usuario que guardara los datos que se leen del archivo en caso de que exista , osea guardados anteriores
        //entrante es el nuevo usuario que se guardara con los anteriores, para despues ser ordenado en su lugar de puntuacion
        public string frase, linea;//frase es los datos obtenidos en string para guardarlos en el archivo y linea es lo leido del archivo por renglon
        public int contador = 0, numero = 0;// contador usado para el proceso de leer la linea de archivo, ubica donde esta leyendo
                                            //numero es la posicion en la Tabla de puntuaciones

        /// /////////////////////////////////////////////////////////////////Variables//////////////////
        public Tabla(String nombre, int puntuacion, string fecha)//constructor que se usara
        {

            InitializeComponent();
            CenterToScreen();//centrado de ventana
            if (File.Exists("Tablapuntuaciones.txt"))//si existe el archivo
            {
                listausuarios.Clear();
                listaorden.Clear();
                //reinicia o vacia las listas para evitar duplicados en caso de un segundo juego

                Console.WriteLine("existe el archivo");
                StreamReader leer = new StreamReader("Tablapuntuaciones.txt");//variable para leer el archivo

                try
                {

                    linea = leer.ReadLine();//linea toma el primer renglon leido del archivo
                    int prueba = 1;// es el contador que se le da a temporal.numero
                    int i;// nuestra variable de iteracion

                    while (linea != null)//hasta que el archivo este vacio
                    {
                        i = 0;//reinicia el iterador en cada renglon
                        temporal.numero = 0;
                        temporal.nombre = null;
                        temporal.puntuacion = 0;
                        temporal.fecha = null;
                        //reinicia usuario temporal para evitar duplicados

                        for (i = 0; i <= linea.Length; i++)//for del tamaño de la linea
                        {
                            if (linea[i].Equals(' '))//primer espacio y segundo osea lectura del numero y el nombre empieza 
                            {

                                contador++;// = 1, 2

                            }
                            else if (contador == 1)
                            {
                                temporal.numero = prueba;//se le da el numero del usuario

                            }
                            else if (contador == 2)//si el contador paso el numero empieza la lectura del nombre
                            {

                                while (!(linea[i].Equals(' ')))//hasta el siguiente espacio
                                {

                                    temporal.nombre += linea[i];//concatena letra por letra el nombre en el usuario temporal
                                    i++;//itera hasta que acabe el nombre

                                }
                                contador++;// = 3

                            }
                            else if (contador == 3)//paso la lactura del nombre sigue la de la puntuacion
                            {
                                string p = null;// variable de puntuacion en string se reinicia
                                while (!(linea[i].Equals(' ')))
                                {

                                    p += linea[i];//concatena la puntuacion en p
                                    temporal.puntuacion = (Convert.ToInt32(p));//convierte en int el string p y lo guarda en temporal

                                    i++;//itera hasta que acabe puntuacion

                                }//fin while interno
                                contador++;// = 4 

                            }
                            else if (contador == 4)//paso puntuacion por ultimo sigue fecha
                            {
                                while (i != linea.Length)//hasta que llegue al final de la linea para evitar errores de lectura
                                {
                                    temporal.fecha += linea[i];//concatena letra por letra la fecha en temporal
                                    i++;//itera hasta que acabe la linea
                                }
                                contador++;//contador 5 nos guia por la lectura del archivo

                            }//fin if else if

                        }// fin for i linea 

                        listausuarios.Add(temporal);//agrega el usuario temporal a la lista de usuarios vacio uno a uno

                        contador = 0;//reinicia para otra linea

                        linea = leer.ReadLine();//lee la siguiente linea y evita que se rompa el while

                        prueba++;//numero de usuarios y numero en archivo

                    }// fin while linea y acaba la lectura del archivo

                    entrante.numero = listausuarios.Count + 1;
                    entrante.nombre = nombre;
                    entrante.puntuacion = puntuacion;
                    entrante.fecha = fecha;
                    //guardamos los datos del registro en usuario entrante 

                    listausuarios.Add(entrante);//mete entrante en la lista con los usuarios recopilados

                    for (int j = 0; j < listausuarios.Count; j++)//ejemplo de impresion
                    {
                        Console.WriteLine(Convert.ToString(listausuarios[j].numero) + " " + listausuarios[j].nombre + " " + Convert.ToString(listausuarios[j].puntuacion + " " + listausuarios[j].fecha));
                    }


                    ////////////////////////////////////////////////////Ordenamiento///////////////////////////////////////
                    variabledeorden = listausuarios.OrderByDescending(usuarios => usuarios.puntuacion);

                    foreach (usuarios listausuarios in variabledeorden)//ejemplo de impresion en orden
                    {
                        Console.WriteLine("{0} - {1} - {2} - {3}", listausuarios.numero, listausuarios.nombre, listausuarios.puntuacion, listausuarios.fecha);

                    }

                    listaorden = variabledeorden.ToList<usuarios>();// se guarda en lista orden por eso el nombre

                    leer.Close();// cerramos la lectura para evitar errores
                    File.Delete("Tablapuntuaciones.txt");// se borra el archivo que acabamos de leer
                    archivo = new StreamWriter("Tablapuntuaciones.txt");//se crea uno con el mismo nombre

                    for (int k = 0; k < listausuarios.Count; k++)//for que guarda los usuarios ordenados en el nuevo archivo
                    {
                        numero = k + 1;
                        frase = " " + Convert.ToString(numero) + " " + listaorden[k].nombre + " " + Convert.ToString(listaorden[k].puntuacion + " " + listaorden[k].fecha);
                        archivo.WriteLine(frase);
                    }

                    MessageBox.Show("Se ha guardado correctamente");
                    archivo.Close();//cerramos el archivo de escritura

                    StreamReader imprimir = new StreamReader("Tablapuntuaciones.txt");//abrimos archivo de lectura
                    linea = imprimir.ReadLine();//leemos primer renglon
                    while (linea != null)//hasta que este vacio
                    {
                        richTextBox1.AppendText(linea + "\n");//imprimimos en la caja de texto con un espacio
                        linea = imprimir.ReadLine();//leemos el siguiente renglon
                    }

                }
                catch
                {
                    MessageBox.Show("Error");//mensaje de error
                }

            }
            else
            {//si no existe se crea 

                archivo = new StreamWriter("Tablapuntuaciones.txt");//archivo de escritura

                primero.numero = 1;
                primero.nombre = nombre;
                primero.puntuacion = puntuacion;
                primero.fecha = fecha;
                //primer usuario en guardar 

                frase = " " + Convert.ToString(primero.numero) + " " + primero.nombre + " " + Convert.ToString(primero.puntuacion) + " " + primero.fecha;
                Console.WriteLine(frase);

                archivo.WriteLine(frase);//guarda la frase armada en el archivo

                MessageBox.Show("Se ha guardado correctamente");

                archivo.Close();//cerramos el archivo en lectura

                /////leemos el archivo entero(que solo tiene un usuario) e imprimimos en la caja de texto
                StreamReader leer = new StreamReader("Tablapuntuaciones.txt");
                linea = leer.ReadLine();
                while (linea != null)
                {
                    richTextBox1.AppendText(linea + "\n");
                    linea = leer.ReadLine();

                }

                Console.WriteLine("No existe el archivo");

            }//fin if else



        }//fin constructor que se usara

        public Tabla()//contructor para solo ver puntuacion
        {
            InitializeComponent();
            CenterToScreen();//centrado de ventana
            try
            {
                StreamReader leer = new StreamReader("Tablapuntuaciones.txt");
                linea = leer.ReadLine();
                while (linea != null)
                {
                    richTextBox1.AppendText(linea + "\n");
                    linea = leer.ReadLine();

                }

            }
            catch
            {

                MessageBox.Show("Error no existe nungun archivo");

            }


        }

        private void button1_Click_1(object sender, EventArgs e)//vuelve al menu principal (boton regresar)
        {
            menuPrincipal menunuevo = new menuPrincipal();//crea un nuevo objeto menuprincipal y lo muestra
            this.Visible = false;
            menunuevo.Visible = true;
        }

        private void button2_Click_1(object sender, EventArgs e)//boton salir
        {
            menuPrincipal menu = new menuPrincipal();
            menu.Dispose();
            this.Dispose();
            Application.Exit();
        }



        /// <summary>
        /// ////////////////Miembros que no se usaran
        /// </summary>


        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void Tabla_Load(object sender, EventArgs e)
        {

        }
    }
}
