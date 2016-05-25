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
    public partial class FormUtilizador : Form
    {
        Utilizador utilizadorLogado;
        string genero;
        bool ativo;
        List<Utilizador> listaUtilizadores;
        Utilizador utilizador;
        int sns;

        public FormUtilizador(Utilizador utilizador)
        {
            InitializeComponent();
            this.utilizadorLogado = utilizador;
            listaUtilizadores = GetUtilizadores().ToList();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            label1.Text = utilizadorLogado.NomeUtilizador;
                        
            ListViewItem item1 = new ListViewItem();

            listView1.FullRowSelect = true;

            for (int i = 0; i < listaUtilizadores.Count; i++)
            {

                item1 = new ListViewItem(listaUtilizadores[i].Nome.ToString());
                item1.SubItems.Add(listaUtilizadores[i].DataNascimento.ToString());
                item1.SubItems.Add(listaUtilizadores[i].NumSns.ToString());
                item1.SubItems.Add(listaUtilizadores[i].TipoUtilizador.ToString());

                listView1.Items.Add(item1);
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnRegistar_Click(object sender, EventArgs e)
        {
            if (rbMasculino.Checked)
            {
                genero = "M";
            }
            else if (rbFeminino.Checked)
            {
                genero = "F";
            }

            if (ckAtivo.Checked == true)
            {
                ativo = false;
            }
            else
            {
                ativo = true;
            }

            try
            {

                RegistarUtilizador(tbNome.Text, dtDataNascim.Value, genero, tbMorada.Text, int.Parse(tbTelefone.Text), ativo, tbEmail.Text, int.Parse(tbSns.Text), tbNomeUtilizador.Text, tbPassword.Text, cbTipoUtilizador.Text);
                MessageBox.Show("Utilizador Registado com Sucesso!", "Sucesso");
                limparCampos();
                atualizar();
            }
            catch (Exception)
            {
                MessageBox.Show("Erro!");
            }
 
        }

        private void atualizar()
        {
            listView1.Items.Clear();

            listaUtilizadores = GetUtilizadores().ToList();

            ListViewItem item1 = new ListViewItem();

            listView1.FullRowSelect = true;

            for (int i = 0; i < listaUtilizadores.Count; i++)
            {

                item1 = new ListViewItem(listaUtilizadores[i].Nome.ToString());
                item1.SubItems.Add(listaUtilizadores[i].DataNascimento.ToString());
                item1.SubItems.Add(listaUtilizadores[i].NumSns.ToString());
                item1.SubItems.Add(listaUtilizadores[i].TipoUtilizador.ToString());

                listView1.Items.Add(item1);
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            limparCampos();
            atualizar();
        }

        public void limparCampos()
        {
            tbNome.Clear();
            tbEmail.Clear();
            tbMorada.Clear();
            tbNomeUtilizador.Clear();
            tbPassword.Clear();
            tbSns.Clear();
            tbTelefone.Clear();
            rbMasculino.Checked = false;
            rbFeminino.Checked = false;
            ckAtivo.Checked = false;
            cbTipoUtilizador.SelectedIndex = -1;
            dtDataNascim.ResetText();
        }

        public void RegistarUtilizador(string nome, DateTime dataNascim, string genero, string morada, int telefone, bool ativo, string email, int sns, string nomeUtilizador, string password, string tipoUtilizador)
        {
            AEH_BDEntities bd = new AEH_BDEntities();

            Utilizador u = new Utilizador();

            u.Nome = nome;
            u.DataNascimento = dataNascim;
            u.Genero = genero;
            u.Morada = morada;
            u.Telefone = telefone;
            u.Ativo = ativo;
            u.Email = email;
            u.NumSns = sns;
            u.NomeUtilizador = nomeUtilizador;
            u.Password = password;
            u.TipoUtilizador = tipoUtilizador;

            bd.PessoaSet.Add(u);
            bd.SaveChanges();

            Utilizador utilizador = bd.PessoaSet.OfType<Utilizador>().Where(i => i.Id == 1).FirstOrDefault();

        }

        public void atualizarAtualizador(string nome, DateTime dataNascim, string genero, string morada, int telefone, bool ativo, string email, int sns, string nomeUtilizador, string password, string tipoUtilizador)
        {
            AEH_BDEntities bd = new AEH_BDEntities();

            Utilizador u = bd.PessoaSet.OfType<Utilizador>().Where(i => i.NumSns == sns).FirstOrDefault();

            u.Nome = nome;
            u.DataNascimento = dataNascim;
            u.Genero = genero;
            u.Morada = morada;
            u.Telefone = telefone;
            u.Ativo = ativo;
            u.Email = email;
            u.NumSns = sns;
            u.NomeUtilizador = nomeUtilizador;
            u.Password = password;
            u.TipoUtilizador = tipoUtilizador;

            bd.SaveChanges();
        }

        public List<Utilizador> GetUtilizadores()
        {
            AEH_BDEntities bd = new AEH_BDEntities();

            List<Utilizador> listaUtilizadores = bd.PessoaSet.OfType<Utilizador>().Where(u => u.Ativo == true).ToList();

            return listaUtilizadores;
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void vScrollBar1_Scroll_1(object sender, ScrollEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            FormPrincipal frmPrincipal = new FormPrincipal(utilizadorLogado);
            this.Hide();
            frmPrincipal.ShowDialog();
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem a certeza que pretende fazer logout?", "Logout", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                FormInicial frmInicial = new FormInicial();
                this.Hide();
                frmInicial.ShowDialog();
                this.Close();
            }
                
        }

        private void btnPacientes_Click(object sender, EventArgs e)
        {
            FormPaciente frmPaciente = new FormPaciente(utilizadorLogado);
            this.Hide();
            frmPaciente.ShowDialog();
            this.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count <= 0)
            {
                limparCampos();
                btnRegistar.Enabled = true;
                btnProcurar.Enabled = true;
                return;
            }

            int intselectedindex = listView1.SelectedIndices[0];

            if (intselectedindex >= 0)
            {
                utilizador = listaUtilizadores[intselectedindex];
                sns = listaUtilizadores[intselectedindex].NumSns;
                preencherFormulario(utilizador);
                btnRegistar.Enabled = false;
                btnProcurar.Enabled = false;                
            }

            
        }


        private void preencherFormulario(Utilizador u)
        {
            limparCampos();
      
            tbNome.Text = u.Nome;
            dtDataNascim.Value = u.DataNascimento;

            if (u.Genero == "M")
            {
                rbMasculino.Checked = true;
            }

            else
            {
                rbFeminino.Checked = true;
            }

            tbMorada.Text = u.Morada;
            tbTelefone.Text = u.Telefone.ToString();

            if (u.Ativo == false)
            {
                ckAtivo.Checked = true;
            }

            tbEmail.Text = u.Email;
            tbSns.Text = u.NumSns.ToString();
            tbNomeUtilizador.Text = u.NomeUtilizador;
            tbPassword.Text = u.Password;
            cbTipoUtilizador.Text = u.TipoUtilizador;
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (rbMasculino.Checked)
            {
                genero = "M";
            }
            else if (rbFeminino.Checked)
            {
                genero = "F";
            }

            if (ckAtivo.Checked == true)
            {
                ativo = false;
            }
            else
            {
                ativo = true;
            }

            try
            {

                atualizarAtualizador(tbNome.Text, dtDataNascim.Value, genero, tbMorada.Text, int.Parse(tbTelefone.Text), ativo, tbEmail.Text, int.Parse(tbSns.Text), tbNomeUtilizador.Text, tbPassword.Text, cbTipoUtilizador.Text);
                MessageBox.Show("Utilizador Atualizado com Sucesso!", "Sucesso");
                atualizar();

            }
            catch (Exception)
            {
                MessageBox.Show("Erro!");
            }
        }

        private void btnProcurar_Click(object sender, EventArgs e)
        {
            procurarUtilizador(tbNome.Text, dtDataNascim.Value, genero, tbMorada.Text, tbEmail.Text, tbSns.Text, tbTelefone.Text, tbNomeUtilizador.Text, cbTipoUtilizador.Text);
        }

        private void procurarUtilizador(string nome, DateTime dt, string genero, string morada, string email, string sns, string telefone, string nomeUtilizador, string tipoUtilizador)
        {
            AEH_BDEntities bd = new AEH_BDEntities();

            List<Utilizador> listaPesquisa = bd.PessoaSet.OfType<Utilizador>().Where(i => i.Nome.ToLower().Contains(nome.ToLower()) || i.DataNascimento.Equals(dt) || i.Genero.ToLower().Equals(genero.ToLower()) || i.Morada.ToLower().Contains(morada.ToLower()) || i.Email.ToLower().Contains(email.ToLower()) || i.NumSns.ToString().ToLower().Contains(sns.ToLower()) || i.Telefone.ToString().ToLower().Contains(telefone.ToLower()) || i.NomeUtilizador.ToLower().Contains(nomeUtilizador.ToLower()) || i.TipoUtilizador.ToLower().Contains(tipoUtilizador.ToLower())).ToList();

            listView1.Items.Clear();

            ListViewItem item1 = new ListViewItem();

            listView1.FullRowSelect = true;

            for (int i = 0; i < listaPesquisa.Count; i++)
            {

                item1 = new ListViewItem(listaPesquisa[i].Nome.ToString());
                item1.SubItems.Add(listaPesquisa[i].DataNascimento.ToString());
                item1.SubItems.Add(listaPesquisa[i].NumSns.ToString());
                item1.SubItems.Add(listaPesquisa[i].TipoUtilizador.ToString());

                listView1.Items.Add(item1);
            }

        }
    }
}
