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
using MySql.Data.MySqlClient;
using MySql.Data;

namespace GKX_diplom
{
    /// <summary>
    /// Interaction logic for addGP.xaml
    /// </summary>
    public partial class addGP : Window
    {
        public addGP()
        {
            InitializeComponent();
        }

        private void addPointerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (adress.Text != "" && type.Text != "" && start.Text != "" && end.Text != "" && description.Text!="")
            {
                MySqlConnectionStringBuilder mysqlCSB;
                mysqlCSB = new MySqlConnectionStringBuilder();
                mysqlCSB.Server = "hm-kudin.me";
                mysqlCSB.Database = "hm_BD";
                mysqlCSB.UserID = "admin";
                mysqlCSB.Password = "354813kudin354813kudin";
                mysqlCSB.CharacterSet = "utf8"; 
                MySqlConnection cnt = new MySqlConnection();
                cnt.ConnectionString = mysqlCSB.ConnectionString;
                try
                {
                    cnt.Open();
                    MySqlCommand cmd = new MySqlCommand("Insert into Gp (Type,Getting_Started,End_Of_Work,Description,Adress) values ('" + type.Text + "','" + start.Text + "','" + end.Text + "','" + description.Text + "','" + adress.Text + "')", cnt);
                    cmd.ExecuteNonQuery();
                    cnt.Close();
                }
                   
                catch (MySqlException ex)
                {
                    MessageBox.Show("Не вдалося підключитися до БД", ex.Message);
                }
                workerwindow log = new workerwindow();
                this.Hide();
                log.Show();
            }
            else
                MessageBox.Show("Заповніть поля");
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            workerwindow log = new workerwindow();
            this.Hide();
            log.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GKX_diplom.App.Current.Shutdown();
        }
    }
}
