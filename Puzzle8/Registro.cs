using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzle8
{
    public partial class Registro : Form
    {

        /// <summary>
        /// ////////////////////////////////////VARIABLES
        /// </summary>
        static public int puntuacionf;//variable de puntuacion final, se mandara al registro
        public string excluidas = "1234567890 ";//caracteres no permitidos en el nombre de usuario
        public bool bandera = false;
        string fecha;// variable string que guardara la fecha del dia en que se guarde la puntuacion
        public Registro(int puntuacion)
        {
            InitializeComponent();
            CenterToScreen();//centra la ventana
            puntuacionf = puntuacion;
        }





        private void button1_Click(object sender, EventArgs e)
        {
            String nombre = Cajausuario.Text;// nombre de usuario elegido 
            bandera = false;//bandera para verificacion de nombre
            DateTime tiempo = DateTime.Now;//variable para obtener la fecha

            fecha = tiempo.Day + "/" + tiempo.Month + "/" + tiempo.Year;//fecha que se guardara en el registro
            Console.WriteLine(fecha);

            for (int i = 0; i < excluidas.Length; i++)//for para verificar que en el nombre no se usen espacios o numeros
            {
                if (nombre.Contains(excluidas[i]))//si hay un caracter excluido entra y cambia bandera
                {
                    MessageBox.Show("No se permiten espacios o numeros en el nombre");
                    bandera = true;

                }

            }

            if (bandera == false)//si no hay problemas con el nombre
            {
                Tabla Tpuntajes = new Tabla(nombre, puntuacionf, fecha);//crea y abre el objeto Tabla nuevo se envia el nombre puntuacion y fecha
                Tpuntajes.Show();
                this.Visible = false;//se oculta la ventana de registro
            }
        }
    }
}
