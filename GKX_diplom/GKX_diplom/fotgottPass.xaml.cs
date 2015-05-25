using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net;
using System.Net.Mail;


namespace GKX_diplom
{
    /// <summary>
    /// Interaction logic for fotgottPass.xaml
    /// </summary>
    public partial class fotgottPass : Window
    {
        public fotgottPass()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GKX_diplom.App.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Вложение для письма
            //Если нужно больше вложений, для каждого вложения создаем свой объект Attachment с нужным путем к файлу
           // Attachment attachData = new Attachment("D:\Тестовое вложение.zip");
            //Крепим к сообщению подготовленное заранее вложение
           // message.Attachments.Add(attachData);
            //Авторизация на SMTP сервере
            SmtpClient Smtp = new SmtpClient("smtp.yandex.ru", 465);
            Smtp.Credentials = new NetworkCredential("anton2333@voliacable.com", "my9wth");
            Smtp.EnableSsl = false;

            //Формирование письма
            MailMessage message = new MailMessage();
            message.From = new MailAddress("anton2333@voliacable.com");
            message.To.Add(new MailAddress("vip.kud9@mail.ru"));
            message.Subject = "Заголовок";
            message.Body = "Сообщение";

            Smtp.Send(message);//отправка
            MainWindow rw = new MainWindow();
            this.Hide();
            rw.Show();
        }
       
    }
}
