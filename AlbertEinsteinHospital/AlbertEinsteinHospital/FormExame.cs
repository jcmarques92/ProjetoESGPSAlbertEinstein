using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlbertEinsteinHospital
{
    public partial class FormExame : Form
    {
        int id;

        public FormExame(int id)
        {
            this.id = id;
            InitializeComponent();
        }

        private Image convertByteToImage(byte[] photo)
        {
            Image imagem;
            using (MemoryStream ms = new MemoryStream(photo, 0, photo.Length))
            {
                ms.Write(photo, 0, photo.Length);
                imagem = Image.FromStream(ms, true);
            }
            return imagem;
        }

        private void FormExame_Load(object sender, EventArgs e)
        {
            AEH_BDEntities bd = new AEH_BDEntities();

            Exame exame = bd.ExameSet.Where(i => i.Id == id).FirstOrDefault();

            pictureBox1.Image = convertByteToImage(exame.Imagem);
            richTextBox1.Text = exame.Notas;
        }
    }
}
