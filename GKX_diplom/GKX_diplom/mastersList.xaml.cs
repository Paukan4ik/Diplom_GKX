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
using System.Data;

namespace GKX_diplom
{
    /// <summary>
    /// Interaction logic for mastersList.xaml
    /// </summary>
    public partial class mastersList : Window
    {
        public mastersList()
        {
            string deadline = DateTime.Now.ToString();
            InitializeComponent();
            string CommandText = "Select * From masters where deadline>='"+deadline+"'";
            MySqlConnectionStringBuilder mysqlCSB;
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = "hm-kudin.me";
            mysqlCSB.Database = "hm_BD";
            mysqlCSB.UserID = "admin";
            mysqlCSB.CharacterSet = "utf8"; 
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
                    mesterslist.ItemsSource = dt.DefaultView;
                }
                con.Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GKX_diplom.App.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            workerwindow log = new workerwindow();
            this.Hide();
            log.Show();
        }
    }
}
