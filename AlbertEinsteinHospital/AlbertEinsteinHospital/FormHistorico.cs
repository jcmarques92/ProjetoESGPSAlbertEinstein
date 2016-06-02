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
    public partial class FormHistorico : Form
    {
        int id;
        Paciente paciente;

        public FormHistorico(int id, Paciente paciente)
        {
            this.id = id;
            this.paciente = paciente;
            InitializeComponent();
        }

        private void FormHistorico_Load(object sender, EventArgs e)
        {
            AEH_BDEntities bd = new AEH_BDEntities();

            Consulta consulta = bd.ConsultaSet.Where(i => i.Id == id).FirstOrDefault();
            richTextBox1.Text = consulta.Diagnostico;
            mostrarSintomas();
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
            listBox1.DataSource = listaSintomas;
        }

        private List<Sintoma> getSintomas()
        {
            AEH_BDEntities bd = new AEH_BDEntities();

            List<Sintoma> listaSintomas = bd.SintomaSet.Where(i => i.Paciente.NumSns == paciente.NumSns).ToList();

            return listaSintomas;
        }
    }
}
