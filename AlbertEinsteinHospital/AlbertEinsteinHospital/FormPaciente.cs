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

    }
}
