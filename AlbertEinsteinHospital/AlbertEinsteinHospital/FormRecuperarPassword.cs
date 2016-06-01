using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlbertEinsteinHospital
{
    public partial class FormRecuperarPassword : Form
    {
        AEH_BDEntities bd = new AEH_BDEntities();

        Utilizador utilizador = new Utilizador();

        NetworkCredential login;
        SmtpClient client;
        MailMessage msg;

        public FormRecuperarPassword()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormRecuperarPassword_Load(object sender, EventArgs e)
        {

        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            FormLogin frmLogin = new FormLogin();
            this.Hide();
            frmLogin.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AEH_BDEntities bd = new AEH_BDEntities();

            string email = textBox1.Text;

            Utilizador utilizador = bd.PessoaSet.OfType<Utilizador>().Where(u => u.Email.ToLower().Equals(email.ToLower())).FirstOrDefault();

            string novaPassword = createPassword(6);

            login = new NetworkCredential("alberteinsteinhospitalesgps@gmail.com", "projetoesgps2016");
            client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.EnableSsl = true;
            client.Credentials = login;
            msg = new MailMessage { From = new MailAddress("alberteinsteinhospitalesgps@gmail.com") };
            msg.To.Add(new MailAddress(email));
            msg.Subject = "Recuperação de Password";
            msg.Body = "A sua nova password é: " + novaPassword;
            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.Normal;
            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            string userstate = "Sending...";
            client.SendAsync(msg, userstate);

            utilizador.Password = novaPassword;

            bd.SaveChanges();
            bd.Dispose();
        }

        public string createPassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}
