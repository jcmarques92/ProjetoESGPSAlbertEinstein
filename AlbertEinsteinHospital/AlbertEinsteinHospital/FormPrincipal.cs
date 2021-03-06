﻿using System;
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
    public partial class FormPrincipal : Form
    {
        Utilizador utilizador;

        public FormPrincipal(Utilizador utilizador)
        {
            InitializeComponent();
            this.utilizador = utilizador;
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            if (utilizador.TipoUtilizador != "Administrador de Sistema")
            {
                btnUtilizadores.Visible = false;
            }

            if (utilizador.TipoUtilizador.Equals("Administrador de Sistema"))
            {
                btnPacientes.Visible = false;
            }

            label1.Text = utilizador.NomeUtilizador;
        }

        private void btnUtilizadores_Click(object sender, EventArgs e)
        {
            FormUtilizador frmUtilizador = new FormUtilizador(utilizador);
            this.Hide();
            frmUtilizador.ShowDialog();
            this.Close();
        }

        private void btnPacientes_Click(object sender, EventArgs e)
        {
            FormPaciente frmPaciente = new FormPaciente(utilizador);
            this.Hide();
            frmPaciente.ShowDialog();
            this.Close();
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
            FormPrincipal frmPrincipal = new FormPrincipal(utilizador);
            this.Hide();
            frmPrincipal.ShowDialog();
            this.Close();
        }
    }
}
