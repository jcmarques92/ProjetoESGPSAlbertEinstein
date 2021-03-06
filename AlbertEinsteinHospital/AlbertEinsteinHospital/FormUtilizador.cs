﻿using System;
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
            listaUtilizadores = DadosUtilizador.getUtilizadores().ToList();
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

            if (cbTipoUtilizador.SelectedValue != null)
            {
                label28.Visible = false;
            }

            hideLabels();
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

                if (tbNome.Text!=null && dtDataNascim.Value!=null && genero!=null && tbMorada.Text!=null && tbEmail.Text!=null && tbSns.Text!=null && tbTelefone.Text!=null && tbNomeUtilizador.Text!=null && tbPassword.Text!=null && cbTipoUtilizador.Text!=null)
                {
                    DadosUtilizador.registarUtilizador(tbNome.Text, dtDataNascim.Value, genero, tbMorada.Text, int.Parse(tbTelefone.Text), ativo, tbEmail.Text, int.Parse(tbSns.Text), tbNomeUtilizador.Text, DadosUtilizador.GetMD5(tbPassword.Text), cbTipoUtilizador.Text);
                    MessageBox.Show("Utilizador Registado com Sucesso!", "Sucesso");
                    limparCampos();
                    atualizar();
                }
                else
                {
                    MessageBox.Show("Deve preencher todos os campos!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro!");
            }
 
        }



        private void atualizar()
        {
            listView1.Items.Clear();

            listaUtilizadores = DadosUtilizador.getUtilizadores().ToList();

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
            label28.Visible = true;
            label23.Visible = true;
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
            var totalItems = listView1.Items.Count;

            if (intselectedindex >= 0)
            {
                utilizador = listaUtilizadores[intselectedindex];
                sns = listaUtilizadores[intselectedindex].NumSns;
                preencherFormulario(utilizador);
                btnRegistar.Enabled = false;
                btnProcurar.Enabled = false;
                label30.Text = ("Utilizador " + (intselectedindex + 1) + " de " + totalItems);
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

                DadosUtilizador.atualizarUtilizador(tbNome.Text, dtDataNascim.Value, genero, tbMorada.Text, int.Parse(tbTelefone.Text), ativo, tbEmail.Text, int.Parse(tbSns.Text), tbNomeUtilizador.Text, DadosUtilizador.GetMD5(tbPassword.Text), cbTipoUtilizador.Text);
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

        private void tbNome_TextChanged(object sender, EventArgs e)
        {
            Regex modelo = new Regex(@"^[\p{L} \.\-]+$");
            if (modelo.IsMatch(tbNome.Text))
            {
                tbNome.ForeColor = Color.Green;
                label16.Text = "";
                label15.Visible = false;
            }
            else
            {
                tbNome.ForeColor = Color.Red;
                tbNome.Clear();
                label16.BackColor = Color.Red;
                label16.ForeColor = Color.White;
                label16.Text = "Insira letras de aA-zZ";
                label15.Visible = true;
            }
        }

        private void dtDataNascim_ValueChanged(object sender, EventArgs e)
        {
            if (dtDataNascim != null)
            {
                label23.Visible = false;
            }
            else
            {
                label23.Visible = true;
                label29.BackColor = Color.Red;
                label29.ForeColor = Color.White;
                label29.Text = "Insira a data de nascimento";
            }
        }

        private void rbChecked()
        {
            if (rbFeminino.Checked || rbMasculino.Checked)
            {
                label25.Visible = false;
            }
            else
            {
                label25.Visible = true;
            }
        }

        private void rbMasculino_CheckedChanged(object sender, EventArgs e)
        {
            rbChecked();
        }

        private void rbFeminino_CheckedChanged(object sender, EventArgs e)
        {
            rbChecked();
        }

        private void tbMorada_TextChanged(object sender, EventArgs e)
        {
            if (tbMorada.Text != "")
            {
                tbMorada.ForeColor = Color.Green;
                label12.Text = "";
                label21.Visible = false;
            }
            else
            {
                tbMorada.ForeColor = Color.Red;
                label12.BackColor = Color.Red;
                label12.ForeColor = Color.White;
                label12.Text = "Insira a morada";
                label21.Visible = true;

            }
        }

        private void tbTelefone_TextChanged(object sender, EventArgs e)
        {
            Regex modelo = new Regex("^[0-9]+$");
            if (modelo.IsMatch(tbTelefone.Text))
            {
                tbTelefone.ForeColor = Color.Green;
                label24.Text = "";
                label17.Visible = false;
            }
            else
            {
                tbTelefone.ForeColor = Color.Red;
                label24.BackColor = Color.Red;
                label24.ForeColor = Color.White;
                label24.Text = "Formato de telefone incorreto";
                tbTelefone.Clear();
                label17.Visible = true;
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

        private void tbSns_TextChanged(object sender, EventArgs e)
        {
            Regex modelo = new Regex("^[0-9]+$");
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
                tbSns.Clear();
                label19.Visible = true;
            }
        }

        private void tbNomeUtilizador_TextChanged(object sender, EventArgs e)
        {
            if (tbNomeUtilizador.Text != "")
            {
                tbNomeUtilizador.ForeColor = Color.Green;
                label22.Text = "";
                label20.Visible = false;
            }
            else
            {
                tbNomeUtilizador.ForeColor = Color.Red;
                label22.BackColor = Color.Red;
                label22.ForeColor = Color.White;
                label22.Text = "Insira um nome de utilizador";
                label20.Visible = true;

            }
        }

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {
            if (tbPassword.Text != "")
            {
                tbPassword.ForeColor = Color.Green;
                label26.Text = "";
                label27.Visible = false;
            }
            else
            {
                tbPassword.ForeColor = Color.Red;
                label26.BackColor = Color.Red;
                label26.ForeColor = Color.White;
                label26.Text = "Insira uma password";
                label27.Visible = true;

            }
        }

        private void cbTipoUtilizador_SelectedIndexChanged(object sender, EventArgs e)
        {
            label28.Visible = false;
        }

        private void hideLabels()
        {
            label12.Text = "";
            label13.Text = "";
            label14.Text = "";
            label16.Text = "";
            label22.Text = "";
            label24.Text = "";
            label26.Text = "";
            label29.Text = "";
            label30.Text = "";
        }

        private void lOGOUTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem a certeza que pretende fazer logout?", "Logout", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                FormInicial frmInicial = new FormInicial();
                this.Hide();
                frmInicial.ShowDialog();
                this.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FormPrincipal frmPrincipal  = new FormPrincipal(utilizadorLogado);
            this.Hide();
            frmPrincipal.ShowDialog();
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            listView1.Items[0].Selected = true;
            listView1.Select();
            listView1.Items[0].EnsureVisible();

            int selectedIndex = listView1.SelectedIndices[0];
            var totalItems = listView1.Items.Count;
            label30.Text = ("Utilizador " + (selectedIndex + 1) + " de " + totalItems);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedIndex = listView1.SelectedIndices[0];
                var totalItems = listView1.Items.Count;

                if (selectedIndex != 0)
                {
                    listView1.Items[selectedIndex - 1].Selected = true;
                    listView1.Select();
                    listView1.Items[selectedIndex - 1].EnsureVisible();
                    label30.Text = ("Utilizador " + (selectedIndex) + " de " + totalItems);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Selecione um Utilizador");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedItems = listView1.SelectedIndices[0];
                var totalItems = listView1.Items.Count;

                if (selectedItems <= (totalItems - 1))
                {
                    listView1.Items[selectedItems + 1].Selected = true;
                    listView1.Select();
                    listView1.Items[selectedItems + 1].EnsureVisible();
                    label30.Text = ("Utilizador " + (selectedItems + 1 + 1) + " de " + totalItems);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Selecione um Utilizador");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var items = listView1.Items.Count;
            listView1.Items[items - 1].Selected = true;
            listView1.Select();
            listView1.Items[items - 1].EnsureVisible();

            int selectedIndex = listView1.SelectedIndices[0];
            var totalItems = listView1.Items.Count;
            label30.Text = ("Utilizador " + (selectedIndex + 1) + " de " + totalItems);
        }

        private void label26_Click(object sender, EventArgs e)
        {

        }
    }
}
