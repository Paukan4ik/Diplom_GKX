using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using MySql.Data.MySqlClient;
using MySql.Data;

namespace GKX_diplom
{
    /// <summary>
    /// Interaction logic for Gp.xaml
    /// </summary>
    public partial class Gp : Window
    {
        DataSet dsSample;
        string deadline = DateTime.Now.ToString();
        public Gp()
        {
            InitializeComponent();
            gpShow();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GKX_diplom.App.Current.Shutdown();
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            UserWindow us = new UserWindow();
            this.Hide();
            us.Show();
        }

        private void gpShow()
        {
            string CommandText = "Select * From Gp where End_of_Work>='"+deadline+"'";
            MySqlConnectionStringBuilder mysqlCSB;
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = "hm-kudin.me";
            mysqlCSB.Database = "hm_BD";
            mysqlCSB.CharacterSet = "utf8"; 
            mysqlCSB.UserID = "admin";
            mysqlCSB.Password = "354813kudin354813kudin";
            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = mysqlCSB.ConnectionString;

                MySqlCommand com = new MySqlCommand(CommandText, con);
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(CommandText, con))
                {
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    gplist.ItemsSource = dt.DefaultView;
                }
                con.Close();
            }
        }
    }
}
