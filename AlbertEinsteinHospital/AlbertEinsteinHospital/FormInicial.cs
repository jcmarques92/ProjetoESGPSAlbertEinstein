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
    public partial class FormInicial : Form
    {
        public FormInicial()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            FormLogin frmLogin = new FormLogin();
            this.Hide();
            frmLogin.ShowDialog();
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FormInicial frmInicial = new FormInicial();
            this.Hide();
            frmInicial.ShowDialog();
            this.Close();
        }

        private void FormInicial_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
