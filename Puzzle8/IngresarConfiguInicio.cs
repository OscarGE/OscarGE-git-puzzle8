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

namespace Puzzle8
{
    public partial class IngresarConfiguInicio : Form
    {
        public IngresarConfiguInicio()
        {
            InitializeComponent();
            CenterToScreen();
        }
        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            soloNumero(e, textBox1);
            colorTexBox(e, textBox1);
        }

        private void textBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            soloNumero(e, textBox2);
            colorTexBox(e, textBox2);
        }

        private void textBox3_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            soloNumero(e, textBox3);
            colorTexBox(e, textBox3);
        }

        private void textBox4_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            soloNumero(e, textBox4);
            colorTexBox(e, textBox4);
        }

        private void textBox5_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            soloNumero(e, textBox5);
            colorTexBox(e, textBox5);
        }

        private void textBox6_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            soloNumero(e, textBox6);
            colorTexBox(e, textBox6);
        }

        private void textBox7_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            soloNumero(e, textBox7);
            colorTexBox(e, textBox7);
        }

        private void textBox8_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            soloNumero(e, textBox8);
            colorTexBox(e, textBox8);
        }

        private void textBox9_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            soloNumero(e, textBox9);
            colorTexBox(e, textBox9);
        }

        public void soloNumero(KeyPressEventArgs e, TextBox tb)
        {

            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;

            }
            if (e.KeyChar == '0')
            {
                e.Handled = true;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else if (char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
            }
            else if (char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;
            }
            else if (char.IsSymbol(e.KeyChar))
            {
                e.Handled = true;
            }
            else if (char.IsNumber(e.KeyChar))
            {
                tb.Clear();
                e.Handled = false;
            }

        }

        public bool validacionDeConfigu(List<int> listaMeta)
        {
            int num;
            for (int i = 0; i < listaMeta.Count; i++)
            {
                num = listaMeta[i];
                for (int j = 0; j < listaMeta.Count; j++)
                {
                    if (!j.Equals(i))
                    {
                        if (num == listaMeta[j] || num == 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void colorTexBox(KeyPressEventArgs e, TextBox tb)
        {
            switch (e.KeyChar)
            {
                case '1': tb.BackColor = Color.Red; tb.ForeColor = Color.Black; break;
                case '2': tb.BackColor = Color.LimeGreen; tb.ForeColor = Color.Black; break;
                case '3': tb.BackColor = Color.DarkOrange; tb.ForeColor = Color.Black; break;
                case '4': tb.BackColor = Color.RoyalBlue; tb.ForeColor = Color.Black; break;
                case '5': tb.BackColor = Color.Purple; tb.ForeColor = Color.Black; break;
                case '6': tb.BackColor = Color.Yellow; tb.ForeColor = Color.Black; break;
                case '7': tb.BackColor = Color.Red; tb.ForeColor = Color.Black; break;
                case '8': tb.BackColor = Color.LimeGreen; tb.ForeColor = Color.Black; break;
                case '9': tb.BackColor = Color.White; tb.ForeColor = Color.LightGray; break;

            }
        }
        public List<int> llenarListaMeta(List<int> listaMeta, TextBox textBox1, TextBox textBox2, TextBox textBox3, TextBox textBox4, TextBox textBox5, TextBox textBox6, TextBox textBox7, TextBox textBox8, TextBox textBox9)
        {
            if (textBox1.Text != "")
            {
                listaMeta.RemoveAt(0);
                listaMeta.Insert(0, Int32.Parse(textBox1.Text));
            }
            if (textBox2.Text != "")
            {
                listaMeta.RemoveAt(1);
                listaMeta.Insert(1, Int32.Parse(textBox2.Text));
            }
            if (textBox3.Text != "")
            {
                listaMeta.RemoveAt(2);
                listaMeta.Insert(2, Int32.Parse(textBox3.Text));
            }
            if (textBox4.Text != "")
            {
                listaMeta.RemoveAt(3);
                listaMeta.Insert(3, Int32.Parse(textBox4.Text));
            }
            if (textBox5.Text != "")
            {
                listaMeta.RemoveAt(4);
                listaMeta.Insert(4, Int32.Parse(textBox5.Text));
            }
            if (textBox6.Text != "")
            {
                listaMeta.RemoveAt(5);
                listaMeta.Insert(5, Int32.Parse(textBox6.Text));
            }
            if (textBox7.Text != "")
            {
                listaMeta.RemoveAt(6);
                listaMeta.Insert(6, Int32.Parse(textBox7.Text));
            }
            if (textBox8.Text != "")
            {
                listaMeta.RemoveAt(7);
                listaMeta.Insert(7, Int32.Parse(textBox8.Text));
            }
            if (textBox9.Text != "")
            {
                listaMeta.RemoveAt(8);
                listaMeta.Insert(8, Int32.Parse(textBox9.Text));
            }
            return listaMeta;
        }

        //Botónes ======================================================================================================

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            //Se llena el vector con los números de la configuración meta.
            List<int> listaInicio = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            listaInicio = llenarListaMeta(listaInicio, textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox9);

            //Verificando que los números no se repitan.
            if (validacionDeConfigu(listaInicio))
            {
                MessageBox.Show("La configuración no es valida", "Error");
                listaInicio.Clear();
            }
            else
            {
                IngresarConfiguMeta FormConfiguMeta = new IngresarConfiguMeta(listaInicio);
                this.Visible = false;

                FormConfiguMeta.Visible = true;
            }
        }

        private void buttonRegresar_Click_1(object sender, EventArgs e)
        {
            menuPrincipal menu = new menuPrincipal();
            this.Visible = false;
            menu.Visible = true;
        }

        private void buttonSalir2_Click_1(object sender, EventArgs e)
        {
            menuPrincipal menu = new menuPrincipal();
            menu.Dispose();
            this.Dispose();
            Application.Exit();
        }

    }
}
