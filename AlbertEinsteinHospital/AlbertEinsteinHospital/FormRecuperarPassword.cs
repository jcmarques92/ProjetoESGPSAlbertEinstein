using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlbertEinsteinHospital
{
    public partial class FormRecuperarPassword : Form
    {
        AEH_BDEntities bd = new AEH_BDEntities();

        Utilizador utilizador = new Utilizador();
        

        public FormRecuperarPassword()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormRecuperarPassword_Load(object sender, EventArgs e)
        {

        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            FormLogin frmLogin = new FormLogin();
            this.Hide();
            frmLogin.ShowDialog();
            this.Close();
        }
    }
}
