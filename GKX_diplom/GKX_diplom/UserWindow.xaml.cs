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
using Excel = Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using System.Globalization;

namespace GKX_diplom
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public UserWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GKX_diplom.App.Current.Shutdown();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow log = new MainWindow();
            this.Hide();
            log.Show();
        }

        private void Calc_Click(object sender, RoutedEventArgs e)
        {
            Calc cl = new Calc();
            this.Hide();
            cl.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Gp gp = new Gp();
            this.Hide();
            gp.Show();
        }

        private void AddPointers_Click(object sender, RoutedEventArgs e)
        {
            addPointers ap = new addPointers();
            this.Hide();
            ap.Show();
        }

        private void MasterCall_Click(object sender, RoutedEventArgs e)
        {
            masterWindow ap = new masterWindow();
            this.Hide();
            ap.Show();
        }
        string epointer;
        string pilga;
        string gvppointer;
        string xvppointer;
        string copointer;
        string FIO;
        string adress;
        double summarry;
        double cosumm;
         double epsumm;
         double xvpsumm;
         double gvpsumm;


        private void Billing_Click(object sender, RoutedEventArgs e)
        {
            getpointers();
                   
                      
                         epsumm = 0;
                        double EPpoointers=Convert.ToDouble(epointer);
                        if (EPpoointers <= 100)
                        {
                            double tarif2 = (0.3660 * Convert.ToInt32(pilga)) / 100;
                            epsumm = (tarif2 * EPpoointers);
                        }
                        if (EPpoointers > 101 && EPpoointers < 600)
                        {
                            double tarif2 = 0;
                            if (EPpoointers <= 130)
                            {
                                tarif2 = (0.6300 * Convert.ToInt32(pilga)) / 100;
                                epsumm = (tarif2 * EPpoointers);
                            }
                            else
                            {
                                tarif2 = (0.6300 * Convert.ToInt32(pilga)) / 100;
                                epsumm = ((tarif2 * EPpoointers) + ((EPpoointers - 130) * 0.6300));
                            }
                        
                        }
                        if (EPpoointers >= 600)
                        {

                            double tarif2 = (1.4070 * Convert.ToInt32(pilga)) / 100;
                            epsumm = ((tarif2 * EPpoointers) + ((EPpoointers - 130) * 1.4070));
                        }
                   

                        double COtarif = (16.14 * Convert.ToInt32(pilga)) / 100;
                         cosumm = (COtarif * Convert.ToDouble(copointer));
            
                   
                        double GVPtarif = (40.92 * Convert.ToInt32(pilga)) / 100;
                         gvpsumm = (GVPtarif * Convert.ToDouble(gvppointer));
                        
                    
                        double XVPtarif = (7.46 * Convert.ToInt32(pilga)) / 100;
                         xvpsumm = (XVPtarif * Convert.ToDouble(xvppointer));

                        summarry = epsumm + cosumm + gvpsumm + xvpsumm;

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
                            MySqlCommand cmd = new MySqlCommand("UPDATE users SET summ='" + Convert.ToString(summarry) + "' WHERE id='" + GKX_diplom.MainWindow.id + "'", cnt);
                            cmd.ExecuteNonQuery();
                            cnt.Close();
                        }
                        catch (MySqlException ex)
                        {
                            MessageBox.Show("Не удалось подключится к БД", ex.Message);
                        }

                        billInExcel();
                   

                    
                        
                        
        }
        private Excel.Application excelapp;
        private Excel.Window excelWindow;
        private Excel.Sheets excelsheets;
        private Excel.Worksheet excelworksheet;
        private Excel.Range excelcells;
        private Excel.Workbooks excelappworkbooks ;
        private Excel.Workbook excelappworkbook;
        string[] myDateTimePatterns = new string[] { "MM/dd/yy", "MM/dd/yyyy" };


        public void billInExcel()
        {
            // Get the en-US culture.
            CultureInfo ci = new CultureInfo("ru-Ru");
            // Get the DateTimeFormatInfo for the en-US culture.
            DateTimeFormatInfo dtfi = ci.DateTimeFormat;

            string date=DateTime.Now.ToString("dd:MM:yy");
            string fileName = System.AppDomain.CurrentDomain.BaseDirectory + "\\" + "Blank" + ".xlsx";
            excelapp = new Excel.Application();
            excelapp.Visible = false;
            //Книга.
            excelappworkbooks = excelapp.Workbooks;
            excelapp.Workbooks.Open(fileName,
                                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                      Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                      Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                      Type.Missing, Type.Missing);
            excelappworkbook = excelappworkbooks[1];
            excelsheets = excelappworkbook.Worksheets;    
            excelworksheet = (Excel.Worksheet)excelsheets.get_Item(1);
            excelcells = excelworksheet.get_Range("A2", "A2");
            excelcells.Value2 =FIO;
            excelcells = excelworksheet.get_Range("C2", "C2");
            excelcells.Value2 = adress;
            excelcells = excelworksheet.get_Range("E3", "E3");
            excelcells.Value2 = epsumm + " грн."; ;
            excelcells = excelworksheet.get_Range("E5", "E5");
            excelcells.Value2 = gvpsumm + " грн."; ;
            excelcells = excelworksheet.get_Range("E7", "E7");
            excelcells.Value2 = xvpsumm + " грн."; ;
            excelcells = excelworksheet.get_Range("E9", "E9");
            excelcells.Value2 = cosumm + " грн."; ;
            excelcells = excelworksheet.get_Range("E11", "E11");
            excelcells.Value2 = pilga+"%";
            excelcells = excelworksheet.get_Range("E1", "E1");
            excelcells.Value2 ="Сформовано "+ date;
            excelcells = excelworksheet.get_Range("E12", "E12");
            excelcells.Value2 = summarry + " грн.";
            excelappworkbooks = excelapp.Workbooks;
            excelappworkbook = excelappworkbooks[1];
            excelapp.Visible = true;
        
       
        }

        public void getpointers()
        {
            MySqlConnectionStringBuilder mysqlCSB;
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = "hm-kudin.me";
            mysqlCSB.Database = "hm_BD";
            mysqlCSB.UserID = "admin";
            mysqlCSB.Password = "354813kudin354813kudin";
            mysqlCSB.CharacterSet = "utf8"; 

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
                            pilga = dr.GetValue(9).ToString().Trim();                       
                            epointer = dr.GetValue(4).ToString().Trim();
                            gvppointer = dr.GetValue(6).ToString().Trim();
                            xvppointer = dr.GetValue(7).ToString().Trim();
                            copointer = dr.GetValue(8).ToString().Trim();
                            FIO = dr.GetValue(1).ToString().Trim();
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
