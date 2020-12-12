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
    public partial class menuPrincipal : Form
    {
        public menuPrincipal()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void btnModoManual_Click(object sender, EventArgs e)
        {
            modoManual manual = new modoManual();
            this.Visible = false;

            manual.Visible = true;
        }

        public void buttonSalir1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Application.Exit();
        }

        private void btnModoInteligente_Click(object sender, EventArgs e)
        {
            IngresarConfiguInicio FormConfiguInicio = new IngresarConfiguInicio();
            this.Visible = false;
            FormConfiguInicio.Visible = true; 
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Tabla tabla = new Tabla();
            tabla.Visible = true;
   
        }
    }
}
