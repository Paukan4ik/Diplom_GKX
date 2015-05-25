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
    /// Interaction logic for addPointers.xaml
    /// </summary>
    public partial class addPointers : Window
    {
          int epp;
            int gvp;
            int co;
            int xvp;
        public addPointers()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GKX_diplom.App.Current.Shutdown();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            UserWindow us = new UserWindow();
            this.Hide();
            us.Show();
        }

        private void addPointerBtn_Click(object sender, RoutedEventArgs e)
        {
          
            if (Epointer.Text!="" && Gvpointer.Text!="" && Xvpointer.Text!="")
            {
                pointercount();
                if (epp >= 0 && gvp >= 0 && xvp >= 0)
                {
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
                        MySqlCommand cmd = new MySqlCommand("UPDATE users SET pointer='" + Convert.ToString(epp) + "', gvppointer='" + Convert.ToString(gvp) + "', hvppointer='" + Convert.ToString(xvp) +"' WHERE id='" + GKX_diplom.MainWindow.id + "'", cnt);
                        cmd.ExecuteNonQuery();
                        cnt.Close();
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Не удалось подключится к БД", ex.Message);
                    }
                    UserWindow us = new UserWindow();
                    this.Hide();
                    us.Show();
                }
                else
                    MessageBox.Show("Некоректні показникик! Число меньше за минулі показники");
            }
           
        }


        private void Epointer_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void Gvpointer_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void Xvpointer_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void Copointer_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void pointercount()
        {
            string e;
            string g;
            string x;
          
          
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
                                e = dr.GetValue(4).ToString().Trim();
                                g = dr.GetValue(6).ToString().Trim();
                                x = dr.GetValue(7).ToString().Trim();
                            
                                epp = Convert.ToInt32(Epointer.Text) - Convert.ToInt32(e);
                                gvp = Convert.ToInt32(Gvpointer.Text) - Convert.ToInt32(g);
                                xvp = Convert.ToInt32(Xvpointer.Text) - Convert.ToInt32(x);
                                
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
