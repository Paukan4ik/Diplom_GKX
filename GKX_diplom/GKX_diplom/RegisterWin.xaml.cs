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
    /// Interaction logic for RegisterWin.xaml
    /// </summary>
    public partial class RegisterWin : Window
    {
        public RegisterWin()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow login = new MainWindow();
            this.Hide();
            login.Show();
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            this.Hide();
            login.Show();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            type.Visibility = Visibility.Visible;
            number.Visibility = Visibility.Visible;
            poinetercheck.Visibility = Visibility.Visible;
            type.Content = "Номер особового рахунку";
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            type.Visibility = Visibility.Visible;
            number.Visibility = Visibility.Visible;
            poinetercheck.Visibility = Visibility.Hidden;
            type.Content = "Ваш особистий номер";
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (passinput.Password != "" && requestpass.Password != "" && number.Text != "")
            {
                if (passinput.Password == requestpass.Password)
                {
                    passinput.BorderBrush = Brushes.Green;
                    requestpass.BorderBrush = Brushes.Green;
                    string A = number.Text;
                    for (int i = 0; i < A.Length;i++)
                    {
                        if (A[i] >= '0' && A[i] <= '9')
                        {
                            if (poinetercheck.Text != "")
                            {
                                string id = "";

                                string pointer = "";

                                MySqlConnectionStringBuilder mysqlCSB;
                                mysqlCSB = new MySqlConnectionStringBuilder();
                                mysqlCSB.Server = "hm-kudin.me";
                                mysqlCSB.Database = "hm_BD";
                                mysqlCSB.UserID = "admin";
                                mysqlCSB.CharacterSet = "utf8"; 
                                mysqlCSB.Password = "354813kudin354813kudin";

                                string queryString = @"Select * From users" + " Where id = '" + number.Text + "' and pointer='" + poinetercheck.Text + "'";

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
                                                pointer = dr.GetValue(4).ToString().Trim();
                                            }
                                        }
                                    }

                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                    con.Close();
                                }
                                if (number.Text == id && poinetercheck.Text == pointer)
                                {
                                    register();
                                    MainWindow login = new MainWindow();
                                    this.Hide();
                                    login.Show();
                                }
                                else MessageBox.Show("Данні некоректні");
                            }
                            else 
                                MessageBox.Show("Вкажіть останні показники");
                                break;
                          }
                            else
                            {
                                string id = "";
                                MySqlConnectionStringBuilder mysqlCSB;
                                mysqlCSB = new MySqlConnectionStringBuilder();
                                mysqlCSB.Server = "hm-kudin.me";
                                mysqlCSB.Database = "hm_BD";
                                mysqlCSB.UserID = "admin";
                                mysqlCSB.CharacterSet = "utf8"; 
                                mysqlCSB.Password = "354813kudin354813kudin";

                                string queryString = @"Select * From worker" + " Where id = '" + number.Text + "'";

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
                                            }
                                        }
                                    }

                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                    con.Close();
                                }
                                if (number.Text == id)
                                {
                                    register();
                                    MainWindow login = new MainWindow();
                                    this.Hide();
                                    login.Show();
                                }
                                else MessageBox.Show("Данні некоректні");
                                break;
                            }
                        
                    }
                }
                else
                {
                    passinput.BorderBrush = Brushes.Red;
                    requestpass.BorderBrush = Brushes.Red;
                }
            }
            else
                MessageBox.Show("Деякі поля не заповнені");
        }

        public void register()
        {
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
                try
                {
                    con.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Insert into password" +
"(id,Password) Values (@id,@password)", con))
                    {
                        cmd.Parameters.AddWithValue("@id", number.Text);
                        cmd.Parameters.AddWithValue("@password", requestpass.Password);
                        cmd.ExecuteNonQuery();
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
