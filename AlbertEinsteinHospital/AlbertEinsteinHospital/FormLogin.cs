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
    public partial class FormLogin : Form
    {
        AEH_BDEntities bd = new AEH_BDEntities();
        Utilizador utilizador;
        string nomeUtilizador;
        string password;

        public FormLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nomeUtilizador = tbUtilizador.Text;
            password = tbPassword.Text;

            if (!Login(nomeUtilizador, password))
            {
                MessageBox.Show("Nome de utilizador ou password incorretos!", "Erro");
            }
            else
            {
                FormPrincipal frmPrincipal = new FormPrincipal(utilizador);
                this.Hide();
                frmPrincipal.ShowDialog();
                this.Close();
            }
        }

        public bool Login(string nomeUtilizador, string password)
        {
            utilizador = bd.PessoaSet.OfType<Utilizador>().FirstOrDefault(u => u.NomeUtilizador == nomeUtilizador && u.Password == password);

            if (utilizador != null)
            {
                return true;
            }

            return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormRecuperarPassword frmPassword = new FormRecuperarPassword();
            this.Hide();
            frmPassword.ShowDialog();
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            FormRecuperarPassword frmRecuperar = new FormRecuperarPassword();
            this.Hide();
            frmRecuperar.ShowDialog();
            this.Close();
        }
    }
}
