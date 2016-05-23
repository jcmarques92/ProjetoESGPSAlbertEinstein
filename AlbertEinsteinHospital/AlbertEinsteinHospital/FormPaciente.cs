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
            //int number = 0 - 9;
            //Regex rgx = new Regex(@"[z0-9]$");
            //Match match = Regex.Match("10", numero2);
            //string telefone2 = @"^[0-9]{2}-[0-9]{4}-[0-9]{4}$";

            //string telefone2 = "[0-9]";

            //Match match = Regex.Match("", telefone2);


            //string numeroReal2 = "[0-9]";



            //Match match = Regex.Match(numeroReal2);
            //string email = tbTelefone.Text;
            //string modelo = "[0-9]";


            
            var modelo = new Regex(@"^\d{2}-\d{4}-\d{4}$");
            if (modelo.IsMatch(tbTelefone.Text))
            {
                tbTelefone.BackColor = Color.Green;
                tbTelefone.ForeColor = Color.White;
                label9.BackColor = Color.Green;
                label9.ForeColor = Color.White;
                label9.Text = "Sucesso";
            }
            else
            {
                tbTelefone.BackColor = Color.Red;
                tbTelefone.ForeColor = Color.White;
                label9.BackColor = Color.Red;
                label9.ForeColor = Color.White;
                label9.Text = "Insucesso: Valores entre 0-9";
        }

        }
        
             

    private void tbEmail_TextChanged(object sender, EventArgs e)
        {
            //32767
            Regex modelo = new Regex(@"^([\w\-]+\.)*[\w\- ]+@([\w\- ]+\.)+([\w\-]{2,3})$");
            if (modelo.IsMatch(tbEmail.Text))
            {
                tbEmail.ForeColor = Color.Green;
            }
            else
            {
                tbEmail.BackColor = Color.Red;
                tbEmail.ForeColor = Color.White;
                label13.BackColor = Color.Red;
                label13.ForeColor = Color.White;
                label13.Text = "Insucesso: Formato de email incorreto";
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
                if (tbNome!=null)
                {
                    label15.Visible = false;
                }
                
            }
            else
            {
                tbNome.ForeColor = Color.Red;
                tbNome.Clear();
                label10.BackColor = Color.Red;
                label10.ForeColor = Color.White;
                label10.Text = "Insira letras de aA-zZ";
                

            }
        }

        private void tbSns_TextChanged(object sender, EventArgs e)
        {
            var modelo = new Regex("[0-9]");
            if (modelo.IsMatch(tbSns.Text))
            {
                tbSns.ForeColor = Color.White;
            }
            else
            {
                tbSns.BackColor = Color.Red;
                tbSns.ForeColor = Color.White;
                label14.BackColor = Color.Red;
                label14.ForeColor = Color.White;
                label14.Text = "Insucesso: Valores entre 0-9";
            }
        }

        private void tbMorada_TextChanged(object sender, EventArgs e)
        {
            if (tbMorada.Text != "")
            {
                tbMorada.ForeColor = Color.Green;
                label10.Text = "";
                label11.Text ="";
            }
            else
            {
                tbMorada.ForeColor = Color.Red;
                label11.BackColor = Color.Red;
                label11.ForeColor = Color.White;
                label11.Text = "Insira a morada";

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
    }
}
