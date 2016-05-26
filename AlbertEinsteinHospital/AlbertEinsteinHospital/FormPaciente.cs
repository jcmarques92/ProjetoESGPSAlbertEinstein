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
        Paciente paciente;
        string genero;
        List<Paciente> listaPacientes;
        Utilizador utilizador;
        int sns;
        Utilizador utilizadorLogado;

        public FormPaciente(Utilizador utilizador)
        {
            InitializeComponent();
            this.utilizadorLogado = utilizador;
            listaPacientes = DadosPaciente.getPacientes().ToList();
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

        private void btnUtilizadores_Click(object sender, EventArgs e)
        {
            FormUtilizador frmUtilizador = new FormUtilizador(utilizadorLogado);
            this.Hide();
            frmUtilizador.ShowDialog();
            this.Close();
        }

        /*public Utilizador tipoUtilizador()
        {
            char tpUtilizador;
            if (utilizadorLogado.TipoUtilizador=="S")
            {
                return Convert.ToChar(tpUtilizador)="S";
            }
        }
        */

        private void btnRegistar_Click(object sender, EventArgs e)
        {
            if (utilizadorLogado.TipoUtilizador == "U")
            {
                if (rbMasculino.Checked)
                {
                    genero = "M";
                }
                else if (rbFeminino.Checked)
                {
                    genero = "F";
                }

                try
                {
                    DadosPaciente.registarPaciente(tbNome.Text, dtDataNascim.Value, int.Parse(tbSns.Text), genero, tbMorada.Text, int.Parse(tbTelefone.Text), tbEmail.Text);
                    MessageBox.Show("Paciente Registado com Sucesso!", "Sucesso");
                    limparCampos();
                    atualizar();
                }
                catch (Exception)
                {
                    MessageBox.Show("Erro!");
                }
            }
        }

        public void limparCampos()
        {
            tbNome.Clear();
            dtDataNascim.ResetText();
            tbSns.Clear();
            rbMasculino.Checked = false;
            rbFeminino.Checked = false;
            tbMorada.Clear();
            tbTelefone.Clear();
            tbEmail.Clear();
        }

        private void atualizar()
        {
            listViewPacientes.Items.Clear();

            listaPacientes = DadosPaciente.getPacientes().ToList();

            ListViewItem item1 = new ListViewItem();

            listViewPacientes.FullRowSelect = true;

            for (int i = 0; i < listaPacientes.Count; i++)
            {
                item1 = new ListViewItem(listaPacientes[i].Nome.ToString());
                item1.SubItems.Add(listaPacientes[i].DataNascimento.ToString());
                item1.SubItems.Add(listaPacientes[i].NumSns.ToString());
                item1.SubItems.Add(listaPacientes[i].Genero.ToString());
                item1.SubItems.Add(listaPacientes[i].Morada.ToString());
                item1.SubItems.Add(listaPacientes[i].Telefone.ToString());
                item1.SubItems.Add(listaPacientes[i].Email.ToString());

                listViewPacientes.Items.Add(item1);
            }
        }

        private void tbTelefone_TextChanged(object sender, EventArgs e)
        {
            //@"^\(\d{3}\)\d{3}-\d{4}$" --> "(XXX) XXX-XXXX"
            //@"\d{3}\d{6}$"
            Regex modelo = new Regex("^[0-9]+$");
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
            if (listViewPacientes.SelectedIndices.Count <= 0)
            {
                limparCampos();
                btnRegistar.Enabled = true;
                btnProcurar.Enabled = true;
                return;
            }

            int intselectedindex = listViewPacientes.SelectedIndices[0];

            if (intselectedindex >= 0)
            {
                paciente = listaPacientes[intselectedindex];
                sns = listaPacientes[intselectedindex].NumSns;
                preencherFormulario(paciente);
                btnRegistar.Enabled = false;
                btnProcurar.Enabled = false;
            }
        }

        private void preencherFormulario(Paciente p)
        {
            limparCampos();
            tbNome.Text = p.Nome;
            dtDataNascim.Value = p.DataNascimento;
            if (p.Genero == "M")
            {
                rbMasculino.Checked = true;
            }
            else
            {
                rbFeminino.Checked = true;
            }
            tbMorada.Text = p.Morada;
            tbEmail.Text = p.Email;
            tbSns.Text = p.NumSns.ToString();
            tbTelefone.Text = p.Telefone.ToString();
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
            label1.Text = utilizadorLogado.NomeUtilizador;

            ListViewItem item1 = new ListViewItem();

            listViewPacientes.FullRowSelect = true;

            for (int i = 0; i < listaPacientes.Count; i++)
            {

                item1 = new ListViewItem(listaPacientes[i].Nome.ToString());
                item1.SubItems.Add(listaPacientes[i].DataNascimento.ToString());
                item1.SubItems.Add(listaPacientes[i].Genero.ToString());
                item1.SubItems.Add(listaPacientes[i].Morada.ToString());
                item1.SubItems.Add(listaPacientes[i].Email.ToString());
                item1.SubItems.Add(listaPacientes[i].NumSns.ToString());
                item1.SubItems.Add(listaPacientes[i].Telefone.ToString());

                listViewPacientes.Items.Add(item1);
            }

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
                rbFeminino.ForeColor = Color.Red;
            }
            else
            {
                label21.Visible = true;
                rbMasculino.ForeColor = Color.Green;
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

            try
            {
                DadosPaciente.atualizarPaciente(tbNome.Text, dtDataNascim.Value, int.Parse(tbSns.Text), genero, tbMorada.Text, int.Parse(tbTelefone.Text), tbEmail.Text);
                MessageBox.Show("Paciente Atualizado com Sucesso!", "Sucesso");
                atualizar();
            }
            catch (Exception)
            {
                MessageBox.Show("Erro!");
            }
        }

        private void btnProcurar_Click(object sender, EventArgs e)
        {
            procurarPaciente(tbNome.Text, dtDataNascim.Value, tbSns.Text, genero, tbMorada.Text, tbTelefone.Text, tbEmail.Text);
        }

        private void procurarPaciente(string nome, DateTime dataNascim, string numSns, string genero, string morada, string telefone, string email)
        {
            AEH_BDEntities bd = new AEH_BDEntities();

            List<Paciente> listaPesquisa = bd.PessoaSet.OfType<Paciente>().Where(i => i.Nome.ToLower().Contains(nome.ToLower()) || i.DataNascimento.Equals(dataNascim) || i.NumSns.ToString().ToLower().Contains(numSns.ToLower()) || i.Genero.ToLower().Equals(genero.ToLower()) || i.Morada.ToLower().Contains(morada.ToLower()) || i.Telefone.ToString().ToLower().Contains(telefone.ToLower()) || i.Email.ToLower().Contains(email.ToLower())).ToList();


            listViewPacientes.Items.Clear();

            ListViewItem item1 = new ListViewItem();

            listViewPacientes.FullRowSelect = true;

            for (int i = 0; i < listaPesquisa.Count; i++)
            {

                item1 = new ListViewItem(listaPesquisa[i].Nome.ToString());
                item1.SubItems.Add(listaPesquisa[i].DataNascimento.ToString());
                item1.SubItems.Add(listaPesquisa[i].NumSns.ToString());

                listViewPacientes.Items.Add(item1);
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            limparCampos();
            atualizar();
        }
    }
}
