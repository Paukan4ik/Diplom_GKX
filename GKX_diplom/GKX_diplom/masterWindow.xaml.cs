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
    /// Interaction logic for masterWindow.xaml
    /// </summary>
    public partial class masterWindow : Window
    {
        string adress;
        public masterWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GKX_diplom.App.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserWindow ap = new UserWindow();
            this.Hide();
            ap.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string deadline = DateTime.Now.AddDays(3).ToString();
            if (description.Text != "")
            {
                selectAdress();
                MySqlConnectionStringBuilder mysqlCSB;
                mysqlCSB = new MySqlConnectionStringBuilder();
                mysqlCSB.Server = "hm-kudin.me";
                mysqlCSB.Database = "hm_BD";
                mysqlCSB.UserID = "admin";
                mysqlCSB.CharacterSet = "utf8"; 
                mysqlCSB.Password = "354813kudin354813kudin";
                MySqlConnection cnt = new MySqlConnection();
                cnt.ConnectionString = mysqlCSB.ConnectionString;
                try
                {
                    cnt.Open();
                    MySqlCommand cmd = new MySqlCommand("Insert into masters (description,deadline,adress) values ('"+description.Text+"','"+deadline+"','"+adress+"')", cnt);
                    cmd.ExecuteNonQuery();
                    cnt.Close();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Не вдалося підключитися до БД", ex.Message);
                }
                MessageBox.Show("Майстер прийде до вас приблизно: "+deadline);
                UserWindow ap = new UserWindow();
                this.Hide();
                ap.Show();
            }
            else
                MessageBox.Show("Опишіть проблему!");
        }
         
        private void selectAdress()
        {
            MySqlConnectionStringBuilder mysqlCSB;
                mysqlCSB = new MySqlConnectionStringBuilder();
                mysqlCSB.Server = "hm-kudin.me";
                mysqlCSB.Database = "hm_BD";
                mysqlCSB.UserID = "admin";
                mysqlCSB.CharacterSet = "utf8"; 
                mysqlCSB.Password = "354813kudin354813kudin";

                string queryString = @"Select * From users" + " Where id = '" + GKX_diplom.MainWindow.id + "'";

                using (MySqlConnection con = new MySqlConnection())
                {
                    con.ConnectionString = mysqlCSB.ConnectionString;

                    MySqlCommand com = new MySqlCommand(queryString, con);

                    try
                    {
                        con.Open();

                        using (MySqlDataReader dr = com.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                adress = dr.GetValue(2).ToString().Trim();

                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    con.Close();
                }
        }

    }
}
