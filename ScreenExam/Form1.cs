using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;



namespace ScreenExam
{
    public partial class Form1 : Form
    {
        public static Bitmap BM = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        public static string path = "";
        public static string from = "detulje@yandex.ru";
        public static string login = "detulje";
        public static string to = "edribnokhod@gmail.com";
        public static string password = "fshvpmwznypalsgq";
        public static string host = "smtp.yandex.ru";
        public static int port = 25;
        public static int numberLastMessange = 0;
        public static DateTime timeOfLastMessange;
        public Form1()
        {            
                InitializeComponent();
                this.KeyPreview = true;
                Task.Run(WaitKey);
            
        }
        private void MakeScreen()
        {
            try
            {
                Graphics GH = Graphics.FromImage(BM as Image);
                GH.CopyFromScreen(0, 0, 0, 0, BM.Size);
                path = $"F:\\CSharp\\AutoScreen\\Screen{DateTime.Now.ToString().Replace(':', '-')}.jpg";
                BM.Save(path);
            }
            catch
            {

            }
        }
        
        private void SendMail()
        {
            try
            {
                if (DateTime.Now != timeOfLastMessange)
                {
                    numberLastMessange++;
                    timeOfLastMessange = DateTime.Now;
                }
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from); // Адрес отправителя
                mail.To.Add(new MailAddress(to)); // Адрес получателя
                mail.Subject = "Скрин";
                mail.Body = $"Скрин-№{numberLastMessange}\nВы получили это письмо так как подписались на данную рассылку";
                mail.Attachments.Add(new Attachment(path));
                SmtpClient client = new SmtpClient();
                client.Host = host;
                client.Port = port; // Обратите внимание что порт 587                
                client.Credentials = new NetworkCredential(login, password); // Ваши логин и пароль
                client.EnableSsl = true;
                client.Send(mail);    
            }
            catch
            {
               
            }
        }
        
        private void WaitKey()
        {
            try
            {
                while (true)
                {

                    System.Threading.Thread.Sleep(1000);
                    if ((ModifierKeys & Keys.Control) != 0)
                    {
                       
                        MakeScreen();
                        SendMail();
                    }
                }
            }
            catch
            {
                
                Application.Restart();
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "")
                    from = textBox1.Text;
                if (textBox3.Text != "")
                    password = textBox3.Text;
                if (textBox2.Text != "")
                    to = textBox2.Text;
            }
            catch
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                this.Opacity = 0;
            }
            catch
            {

            }
        }
    }
}
