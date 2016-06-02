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
        List<Exame> listaExames;

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
            dateTimePicker1.ResetText();
            dateTimePicker2.ResetText();
            textBox2.Clear();
            textBox3.Clear();
            tbCaminho.Clear();
            rtbNotas.Clear();
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
            checkBox9.Checked = false;
            checkBox10.Checked = false;
            checkBox11.Checked = false;
            checkBox12.Checked = false;
            checkBox14.Checked = false;
            checkBox15.Checked = false;
            checkBox16.Checked = false;
            checkBox17.Checked = false;
            checkBox18.Checked = false;
            textBox1.Clear();
            listViewExames.Items.Clear();
            listViewConsultas.Items.Clear();
            rtbDiagnostico.Clear();
            listBoxSintomasPacienteSelecionado.DataSource = null;
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
                mostrarExames();
                mostrarSintomas();
                mostrarConsultas();
                btnRegistar.Enabled = false;
                btnProcurar.Enabled = false;
                listaExames = getExames().ToList();
                label26.Text = ("Paciente " + (intselectedindex + 1) + " de " + totalItems);
            }
        }

        private void mostrarSintomas()
        {
            List<Sintoma> listabool = getSintomas();
            List<string> listaSintomas = new List<string>();

            for (int i = 0; i < listabool.Count; i++)
            {
                if (listabool[i].CompromissoViaAerea == true)
                {
                    listaSintomas.Add("Compromisso da via aérea");
                }
                if (listabool[i].RespiracaoIneficaz == true)
                {
                    listaSintomas.Add("Respiração ineficaz");
                }
                if (listabool[i].CriancaNaoReativa == true)
                {
                    listaSintomas.Add("Criança não reativa");
                }
                if (listabool[i].Choque == true)
                {
                    listaSintomas.Add("Choque");
                }
                if (listabool[i].IncapacidadeArticular == true)
                {
                    listaSintomas.Add("Incapacidade de articular frases completas");
                }
                if (listabool[i].TaquicardiaAcentuada == true)
                {
                    listaSintomas.Add("Taquicardia acentuada");
                }
                if (listabool[i].PEFRmb == true)
                {
                    listaSintomas.Add("PEFR (Peak Expiratory Flow Rate) muito baixo");
                }
                if (listabool[i].SAO2mb == true)
                {
                    listaSintomas.Add("SAO2 (Arterial Oxygen Saturation) muito baixo");
                }
                if (listabool[i].AlteracaoConsciencia == true)
                {
                    listaSintomas.Add("Alteração do estado de consciência");
                }
                if (listabool[i].PEFRb == true)
                {
                    listaSintomas.Add("PEFR (Peak Expiratory Flow Rate) baixo");
                }
                if (listabool[i].SAO2b == true)
                {
                    listaSintomas.Add("SAO2 (Arterial Oxygen Saturation) baixo");
                }
                if (listabool[i].HistoriaAsma == true)
                {
                    listaSintomas.Add("História significativa de asma");
                }
                if (listabool[i].Asma == true)
                {
                    listaSintomas.Add("Asma sem melhoria com o seu tratamento habitual");
                }
                if (listabool[i].Broncospasmo == true)
                {
                    listaSintomas.Add("Broncospasmo");
                }
                if (listabool[i].ProvavelInfecaoRespiratoria == true)
                {
                    listaSintomas.Add("Provável infeção respiratória");
                }
                if (listabool[i].ProblemaRecente == true)
                {
                    listaSintomas.Add("Problema Recente");
                }
                if (listabool[i].Outro == true)
                {
                    listaSintomas.Add(listabool[i].OutroDescricao);
                }
            }
            listBoxSintomasPacienteSelecionado.DataSource = listaSintomas;
        }

        private void mostrarExames()
        {
            List<Exame> listaExames = getExames();

            ListViewItem item1 = new ListViewItem();

            listViewExames.FullRowSelect = true;

            for (int i = 0; i < listaExames.Count; i++)
            {

                item1 = new ListViewItem(listaExames[i].Id.ToString());
                item1.SubItems.Add(listaExames[i].Notas.ToString());

                listViewExames.Items.Add(item1);
            }
        }

        private void mostrarConsultas()
        {
            List<Consulta> listaConsultas = getConsultas();

            ListViewItem item1 = new ListViewItem();

            listViewConsultas.FullRowSelect = true;

            for (int i = 0; i < listaConsultas.Count; i++)
            {

                item1 = new ListViewItem(listaConsultas[i].Id.ToString());
                item1.SubItems.Add(listaConsultas[i].Diagnostico.ToString());

                listViewConsultas.Items.Add(item1);
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
            if (paciente != null)
            {
                try
                {
                    registarMedicacao(dateTimePicker1.Value, dateTimePicker2.Value, textBox2.Text, textBox3.Text);
                    limparCampos();
                    MessageBox.Show("Medicação registada com sucesso");
                }
                catch (Exception)
                {
                    MessageBox.Show("Erro ao registar medicação");
                }
            }
            else
            {
                MessageBox.Show("Para efetuar o registo da medicação deve selecionar um paciente na lista");
            }
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
            if (paciente != null)
            {
                try
                {
                    registarExames(tbCaminho.Text, rtbNotas.Text);
                    MessageBox.Show("Exame registado com sucesso");
                    limparCampos();
                }
                catch (Exception)
                {
                    MessageBox.Show("Erro ao registar exame");
                }
            }
            else
            {
                MessageBox.Show("Para efetuar o registo de um exame deve selecionar um paciente na lista");
            }
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

        private List<Consulta> getConsultas()
        {
            AEH_BDEntities bd = new AEH_BDEntities();

            List<Consulta> listaConsultas = bd.ConsultaSet.Where(i => i.Paciente.NumSns == sns).ToList();

            return listaConsultas;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (paciente != null)
            {
                try
                {
                    registarSintoma();
                    MessageBox.Show("Sintomas registados com sucesso");
                    limparCampos();
                }
                catch (Exception)
                {
                    MessageBox.Show("Erro ao registar sintomas");
                }
            }
            else
            {
                MessageBox.Show("Para efetuar o registo dos sintomas deve selecionar um paciente na lista");
            }
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

                label26.Text = ("Paciente " + (selectedItems + 2) + " de " + totalItems);
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

        private void listViewExames_DoubleClick(object sender, EventArgs e)
        {
            int id = int.Parse(listViewExames.SelectedItems[0].SubItems[0].Text);

            FormExame frmExame = new FormExame(id);
            frmExame.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (paciente != null)
            {
                try
                {
                    registarConsulta();
                    MessageBox.Show("Diagnóstico registado com sucesso");
                    limparCampos();
                }
                catch (Exception)
                {
                    MessageBox.Show("Erro ao registar diagnóstico");
                }
            }
            else
            {
                MessageBox.Show("Para efetuar o registo de um diagnóstico deve selecionar um paciente na lista");
            }
        }

        private void registarConsulta()
        {
            AEH_BDEntities bd = new AEH_BDEntities();

            Paciente paciente = bd.PessoaSet.OfType<Paciente>().Where(i => i.NumSns == sns).FirstOrDefault();

            Consulta c = new Consulta();
            c.Paciente = paciente;
            c.Diagnostico = rtbDiagnostico.Text;
            c.Sintoma = paciente.Sintoma;

            bd.ConsultaSet.Add(c);
            bd.SaveChanges();
            bd.Dispose();
        }

        private void listViewConsultas_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void listViewConsultas_DoubleClick(object sender, EventArgs e)
        {
            int id = int.Parse(listViewConsultas.SelectedItems[0].SubItems[0].Text);

            FormHistorico frmHistorico = new FormHistorico(id, paciente);
            frmHistorico.ShowDialog();
        }
    }
}
