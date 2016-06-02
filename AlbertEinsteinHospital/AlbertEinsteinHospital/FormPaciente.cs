using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            //if (utilizadorLogado.TipoUtilizador == "U")
            //{
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
            //}
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
            var totalItems = listViewPacientes.Items.Count;

            if (intselectedindex >= 0)
            {
                paciente = listaPacientes[intselectedindex];
                sns = listaPacientes[intselectedindex].NumSns;
                listBoxSintomasPacienteSelecionado.DataSource = paciente.Sintoma.ToList();
                preencherFormulario(paciente);
                mostrarExames(paciente);
                btnRegistar.Enabled = false;
                btnProcurar.Enabled = false;
                label26.Text = ("Paciente " + (intselectedindex + 1) + " de " + totalItems);
            }
        }

        private void mostrarExames(Paciente p)
        {
            listBoxExamesPacienteSelecionado.DataSource = getExames();
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

        private void rbFeminino_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFeminino.Checked)
            {
                label21.Visible = false;
                rbFeminino.ForeColor = Color.Green;
            }
            else
            {
                label21.Visible = true;
                rbFeminino.ForeColor = Color.Black;
            }
        }

        private void rbMasculino_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMasculino.Checked)
            {
                label21.Visible = false;
                rbMasculino.ForeColor = Color.Green;
            }
            else
            {
                label21.Visible = true;
                rbMasculino.ForeColor = Color.Black;
            }
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

        private void button6_Click(object sender, EventArgs e)
        {
            registarMedicacao(dateTimePicker1.Value, dateTimePicker2.Value, textBox2.Text, textBox3.Text);
        }

        private void registarMedicacao(DateTime dtinicio, DateTime dtfim, string medicamento, string dosagem)
        {
            AEH_BDEntities bd = new AEH_BDEntities();

            Paciente paciente = bd.PessoaSet.OfType<Paciente>().Where(i => i.NumSns == sns).FirstOrDefault();

            Medicacao m = new Medicacao();
            m.Paciente = paciente;
            m.DataInicio = dtinicio;
            m.DataFim = dtfim;
            m.Medicamento = medicamento;
            m.Dosagem = dosagem;

            bd.MedicacaoSet.Add(m);
            bd.SaveChanges();
            bd.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            registarExames(tbCaminho.Text, rtbNotas.Text);
        }

        private byte[] converterImagem(string caminhoImagem)
        {
            byte[] data = null;
            FileInfo fInfo = new FileInfo(caminhoImagem);
            long numBytes = fInfo.Length;
            FileStream fStream = new FileStream(caminhoImagem, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fStream);
            data = br.ReadBytes((int)numBytes);
            return data;
        }

        private void registarExames(string caminhoImagem, string notas)
        {
            AEH_BDEntities bd = new AEH_BDEntities();

            Paciente paciente = bd.PessoaSet.OfType<Paciente>().Where(i => i.NumSns == sns).FirstOrDefault();

            Exame e = new Exame();
            e.Paciente = paciente;
            e.Imagem = converterImagem(caminhoImagem);
            e.Notas = notas;

            bd.ExameSet.Add(e);
            bd.SaveChanges();
            bd.Dispose();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string file = openFileDialog1.FileName;
                    tbCaminho.Text = file;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro!");
            }
        }

        private void registarSintoma()
        {
            bool viaAerea = false;
            bool respiracaoIneficaz = false;
            bool crNaoReativa = false;
            bool choque = false;
            bool incArticular = false;
            bool taquicardia = false;
            bool pefrMb = false;
            bool sao2Mb = false;
            bool alterConsciencia = false;
            bool pefrB = false;
            bool sao2B = false;
            bool historiaAsma = false;
            bool asma = false;
            bool broncospasmo = false;
            bool provInfResp = false;
            bool probRecente = false;
            bool outro = false;

            string outroDesc = null;

            if (checkBox1.Checked)
            {
                viaAerea = true;
            }

            if (checkBox2.Checked)
            {
                respiracaoIneficaz = true;
            }

            if (checkBox3.Checked)
            {
                crNaoReativa = true;
            }

            if (checkBox4.Checked)
            {
                choque = true;
            }

            if (checkBox5.Checked)
            {
                incArticular = true;
            }

            if (checkBox6.Checked)
            {
                taquicardia = true;
            }

            if (checkBox7.Checked)
            {
                historiaAsma = true;
            }

            if (checkBox8.Checked)
            {
                sao2B = true;
            }

            if (checkBox9.Checked)
            {
                pefrB = true;
            }

            if (checkBox10.Checked)
            {
                alterConsciencia = true;
            }

            if (checkBox11.Checked)
            {
                sao2Mb = true;
            }

            if (checkBox12.Checked)
            {
                pefrMb = true;
            }

            if (checkBox14.Checked)
            {
                outro = true;
                outroDesc = textBox1.Text;
            }

            if (checkBox15.Checked)
            {
                probRecente = true;
            }

            if (checkBox16.Checked)
            {
                provInfResp = true;
            }

            if (checkBox17.Checked)
            {
                broncospasmo = true;
            }

            if (checkBox18.Checked)
            {
                asma = true;
            }

            AEH_BDEntities bd = new AEH_BDEntities();

            Paciente paciente = bd.PessoaSet.OfType<Paciente>().Where(i => i.NumSns == sns).FirstOrDefault();

            Sintoma s = new Sintoma();
            s.Paciente = paciente;
            s.CompromissoViaAerea = viaAerea;
            s.RespiracaoIneficaz = respiracaoIneficaz;
            s.CriancaNaoReativa = crNaoReativa;
            s.Choque = choque;
            s.IncapacidadeArticular = incArticular;
            s.TaquicardiaAcentuada = taquicardia;
            s.PEFRmb = pefrMb;
            s.SAO2mb = sao2Mb;
            s.AlteracaoConsciencia = alterConsciencia;
            s.PEFRb = pefrB;
            s.SAO2b = sao2B;
            s.HistoriaAsma = historiaAsma;
            s.Asma = asma;
            s.Broncospasmo = broncospasmo;
            s.ProvavelInfecaoRespiratoria = provInfResp;
            s.ProblemaRecente = probRecente;
            s.Outro = outro;
            s.OutroDescricao = outroDesc;
            s.Notas = richTextBox2.Text;

            bd.SintomaSet.Add(s);
            bd.SaveChanges();
            bd.Dispose();
        }

        private List<Sintoma> getSintomas()
        {
            AEH_BDEntities bd = new AEH_BDEntities();

            List<Sintoma> listaSintomas = bd.SintomaSet.Where(i => i.Paciente.NumSns == sns).ToList();

            return listaSintomas;
        }

        private List<Medicacao> getMedicacao()
        {
            AEH_BDEntities bd = new AEH_BDEntities();

            List<Medicacao> listaMedicacao = bd.MedicacaoSet.Where(i => i.Paciente.NumSns == sns).ToList();

            return listaMedicacao;
        }

        private List<Exame> getExames()
        {
            AEH_BDEntities bd = new AEH_BDEntities();

            List<Exame> listaExames = bd.ExameSet.Where(i => i.Paciente.NumSns == sns).ToList();

            return listaExames;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            registarSintoma();
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

        private void button7_Click(object sender, EventArgs e)
        {
            listViewPacientes.Items[0].Selected = true;
            listViewPacientes.Select();
            listViewPacientes.Items[0].EnsureVisible();

            int selectedIndex = listViewPacientes.SelectedIndices[0];
            var totalItems = listViewPacientes.Items.Count;
            label26.Text = ("Paciente " + (selectedIndex + 1) + " de " + totalItems);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int selectedIndex = listViewPacientes.SelectedIndices[0];
            var totalItems = listViewPacientes.Items.Count;

            if (selectedIndex != 0)
            {
                listViewPacientes.Items[selectedIndex - 1].Selected = true;
                listViewPacientes.Select();
                listViewPacientes.Items[selectedIndex - 1].EnsureVisible();

                label26.Text = ("Paciente " + (selectedIndex + 1) + " de " + totalItems);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //int selectedIndex = listViewPacientes.SelectedIndices[0];
            int selectedItems = listViewPacientes.SelectedItems[0].Index;
            var totalItems = listViewPacientes.Items.Count;

            if (selectedItems != (totalItems - 1))
            {
                listViewPacientes.Items[selectedItems + 1].Selected = true;
                listViewPacientes.Select();
                listViewPacientes.Items[selectedItems + 1].EnsureVisible();

                label26.Text = ("Paciente " + (selectedItems + 1) + " de " + totalItems);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var items = listViewPacientes.Items.Count;
            listViewPacientes.Items[items - 1].Selected = true;
            listViewPacientes.Select();
            listViewPacientes.Items[items - 1].EnsureVisible();

            int selectedIndex = listViewPacientes.SelectedIndices[0];
            var totalItems = listViewPacientes.Items.Count;
            label26.Text = ("Paciente " + (selectedIndex + 1) + " de " + totalItems);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FormPrincipal frmPrincipal = new FormPrincipal(utilizadorLogado);
            this.Hide();
            frmPrincipal.ShowDialog();
            this.Close();
        }
    }
}
