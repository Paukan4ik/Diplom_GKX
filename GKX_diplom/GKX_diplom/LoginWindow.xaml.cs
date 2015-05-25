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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using MySql.Data;
namespace GKX_diplom
{
  
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string id;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            RegisterWin rw = new RegisterWin();
            this.Hide();
            rw.Show();
        }

        private void LoginWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GKX_diplom.App.Current.Shutdown();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginIn();
        }

        public void LoginIn()
        {
            if (LoginBox.Text != "" && passwordB.Password != "")
            {

              
                string password = "";

                MySqlConnectionStringBuilder mysqlCSB;
                mysqlCSB = new MySqlConnectionStringBuilder();
                mysqlCSB.Server = "hm-kudin.me";
                mysqlCSB.Database = "hm_BD";
                mysqlCSB.UserID = "admin";
                mysqlCSB.CharacterSet = "utf8"; 
                mysqlCSB.Password = "354813kudin354813kudin";

                string queryString = @"Select * From password" + " Where id = '" + LoginBox.Text + "'";
               
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
                                id = dr.GetValue(0).ToString().Trim();
                                password = dr.GetValue(1).ToString().Trim();
                               
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    con.Close();

                    string A = LoginBox.Text;
                    if (LoginBox.Text == id && passwordB.Password == password)
                    {
                        for (int i = 0; i < A.Length; i++)
                        {
                            if (A[i] >= '0' && A[i] <= '9')
                            {
                                UserWindow us = new UserWindow();
                                this.Hide();
                                us.Show();
                                break;
                            }
                            else
                            {
                                workerwindow us = new workerwindow();
                                this.Hide();
                                us.Show();
                                break;
                            }
                        }
                       }
                    else
                    {
                        MessageBox.Show("Данні некоректні");
                        LoginBox.Clear();
                        LoginBox.Focus();
                        passwordB.Clear();
                    }
                }
            }
            else
            {
                MessageBox.Show("Заповніть всі поля");
                LoginBox.Clear();
                passwordB.Clear();
            }
        }
        private void passwordB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginIn();
            }
        }

        private void forgotPassword_Click(object sender, RoutedEventArgs e)
        {
            fotgottPass rw = new fotgottPass();
            this.Hide();
            rw.Show();
        }

    
    }
}
