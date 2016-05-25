using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlbertEinsteinHospital
{
    public partial class FormPaciente : Form
    {
        Utilizador utilizador;

        public FormPaciente(Utilizador utilizador)
        {
            this.utilizador = utilizador;
            InitializeComponent();

        }

       private void btnVoltar_Click(object sender, EventArgs e)
        {
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
        }

        private void btnUtilizadores_Click(object sender, EventArgs e)
        {
        }

        private void btnRegistar_Click(object sender, EventArgs e)
        {
            registarPaciente(tbNome.Text, dtDataNascim.Value, int.Parse(tbSns.Text), "M", tbMorada.Text, int.Parse(tbTelefone.Text), tbEmail.Text);

        }

        public void registarPaciente(string nome, DateTime dataNascim, int numSns, string genero, string morada, int telefone, string email)
        {
            AEH_BDEntities db = new AEH_BDEntities();

            Paciente p = new Paciente();
            p.Nome = nome;
            p.DataNascimento = dataNascim;
            p.NumSns = numSns;
            p.Genero = genero;
            p.Morada = morada;
            p.Telefone = telefone;
            p.Email = email;

            db.PessoaSet.Add(p);
            db.SaveChanges();

            Paciente paciente = db.PessoaSet.OfType<Paciente>().Where(x => x.Id == 1).FirstOrDefault();
            db.Dispose();

        }

        private void tbTelefone_TextChanged(object sender, EventArgs e)
        {
            //@"^\(\d{3}\)\d{3}-\d{4}$" --> "(XXX) XXX-XXXX"
            //@"\d{3}\d{6}$"
            Regex modelo = new Regex(@"\d{9}$");
            if (modelo.IsMatch(tbTelefone.Text))
            {
                tbTelefone.ForeColor = Color.Green;
                label9.Text = "";
                label20.Visible = false;
            }
            else
            {
                tbTelefone.ForeColor = Color.Red;
                label9.BackColor = Color.Red;
                label9.ForeColor = Color.White;
                label9.Text = "Formato de telefone incorreto";
                label20.Visible = true;
            }

        }
        
             

    private void tbEmail_TextChanged(object sender, EventArgs e)
        {
            Regex modelo = new Regex(@"^([\w\-]+\.)*[\w\- ]+@([\w\- ]+\.)+([\w\-]{2,3})$");
            if (modelo.IsMatch(tbEmail.Text))
            {
                tbEmail.ForeColor = Color.Green;
                label13.Text = "";
                label18.Visible = false;
            }
            else
            {
                tbEmail.ForeColor = Color.Red;
                label13.BackColor = Color.Red;
                label13.ForeColor = Color.White;
                label13.Text = "Formato de email incorreto";
                label18.Visible = true;
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Paciente> paciente = new List<Paciente>();

            foreach (var item in paciente)
            {

            }
        }

        private void tbNome_TextChanged(object sender, EventArgs e)
        {
            
            Regex modelo = new Regex(@"^[aA-zZ]+((\s[aA-zZ]+)+)?$");
            if (modelo.IsMatch(tbNome.Text))
            {
                tbNome.ForeColor = Color.Green;
                label10.Text = "";
                label15.Visible = false;
            }
            else
            {
                tbNome.ForeColor = Color.Red;
                tbNome.Clear();
                label10.BackColor = Color.Red;
                label10.ForeColor = Color.White;
                label10.Text = "Insira letras de aA-zZ";
                label15.Visible = true;
            }
        }

        private void tbSns_TextChanged(object sender, EventArgs e)
        {
            Regex modelo = new Regex("[0-9]");
            if (modelo.IsMatch(tbSns.Text))
            {
                tbSns.ForeColor = Color.Green;
                label14.Text = "";
                label19.Visible = false;
            }
            else
            {
                tbSns.ForeColor = Color.Red;
                label14.BackColor = Color.Red;
                label14.ForeColor = Color.White;
                label14.Text = "Valores entre 0-9";
                label19.Visible = true;
            }
        }

        private void tbMorada_TextChanged(object sender, EventArgs e)
        {
            if (tbMorada.Text != "")
            {
                tbMorada.ForeColor = Color.Green;
                label11.Text ="";
                label17.Visible = false;
            }
            else
            {
                tbMorada.ForeColor = Color.Red;
                label11.BackColor = Color.Red;
                label11.ForeColor = Color.White;
                label11.Text = "Insira a morada";
                label17.Visible = true;

            }
        }

        private void FormPaciente_Load(object sender, EventArgs e)
        {
            label9.Text = "";
            label10.Text = "";
            label11.Text = "";
            label13.Text = "";
            label14.Text = "";
        }

        private void dtDataNascim_ValueChanged(object sender, EventArgs e)
        {
            if (dtDataNascim!=null)
            {
                label16.Visible = false;
            }
            else
            {
                label16.Visible = true;
            }
        }

        private void rbChecked()
        {
            if (rbFeminino.Checked || rbMasculino.Checked)
            {
                label21.Visible = false;
            }
            else
            {
                label21.Visible = true;
            }
        }

        private void rbFeminino_CheckedChanged(object sender, EventArgs e)
        {
            rbChecked();
        }

        private void rbMasculino_CheckedChanged(object sender, EventArgs e)
        {
            rbChecked();
        }
    }
}
