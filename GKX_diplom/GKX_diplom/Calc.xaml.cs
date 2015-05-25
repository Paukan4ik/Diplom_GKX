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

namespace GKX_diplom
{
    /// <summary>
    /// Interaction logic for Calc.xaml
    /// </summary>
    public partial class Calc : Window
    {
        public Calc()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
          UserWindow ys=new UserWindow();
            this.Hide();
            ys.Show();
        }

        private void new_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void old_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void wateradds_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void countBtn_Click(object sender, RoutedEventArgs e)
        {
            
            if (newp.Text != "" && oldp.Text != "")
            {
                int pointer = Convert.ToInt32(newp.Text) - Convert.ToInt32(oldp.Text);
                if (pointer >= 0)
                {
                    if (EPcheck.IsChecked == true)
                    {
                        double Copointer=0;
                        double Copointer2 = 0;
                        if (pointer <=100)
                        {
                            double tarif = (0.3660 * Convert.ToInt32(pilga.Text)) / 100;
                            double tarif2 = (0.3660 * Convert.ToInt32(pilga.Text)) / 100;
                            if (pointer <= 90)
                            {
                                Copointer = (tarif*pointer);
                            }
                            else
                            { Copointer = ((tarif*pointer) + ((pointer - 90) * 0.3660)); }
                            Copointer2 = (tarif2*pointer);  
                        }
                        if (pointer > 101 && pointer<600)
                        {
                            double tarif = (0.6300 * Convert.ToInt32(pilga.Text)) / 100;
                            double tarif2 = 0;
                            if (pointer<=130)
                            {
                                tarif2 = (0.6300 * Convert.ToInt32(pilga.Text)) / 100;
                            Copointer2 = (tarif2*pointer);
                            }
                            else
                            {
                                tarif2 = (0.6300 * Convert.ToInt32(pilga.Text)) / 100;
                            Copointer2 = ((tarif2*pointer) + ((pointer - 130) * 0.6300)); }
                            Copointer = ((tarif*pointer) + ((pointer - 90) * 0.6300));
                            
                        }
                        if (pointer >= 600)
                        {
                            double variable = pointer;
                            double tarif = (1.4070 * Convert.ToInt32(pilga.Text)) / 100;
                            double tarif2 = (1.4070 * Convert.ToInt32(pilga.Text)) / 100;
                            Copointer = ((tarif * pointer) + ((pointer - 90) * 1.4070));
                            Copointer2 = ((tarif2 * pointer) + ((pointer - 130) * 1.4070));
                        }
                             MessageBox.Show(" EП. По вказаним показникам ви маєте сплатити " + Copointer + " грн.(газофікований будинок) "+ Copointer2 + " грн.(електрофікований будинок)");
                    }
                  
                    if (COcheck.IsChecked == true)
                    {
                       
                        simple.Visibility = Visibility.Visible;
                        wateradds.Visibility = Visibility.Visible;
                     
                        double tarif = (16.14 * Convert.ToInt32(pilga.Text))/100;
                        double Copointer = (tarif * Convert.ToInt32(wateradds.Text));
                        MessageBox.Show("ЦО. По вказаній квадратурі ви маєте сплатити "+ Copointer +" грн.");
                    }
                    
                    if (GVPcheck.IsChecked == true)
                    {
                        double tarif = (40.92 * Convert.ToInt32(pilga.Text)) / 100;
                        double tarif2 = (37.91 * Convert.ToInt32(pilga.Text)) / 100;
                        double fpointer = (tarif *pointer);
                        double fpointer2 = (tarif * pointer);
                        MessageBox.Show("ГВП. По вказаним показникам ви маєте сплатити " + fpointer + " грн.(з рушникосушильником) "+ fpointer2 + " грн.(без рушникосушильника)");
                    }
                   
                    if (HVPcheck.IsChecked == true)
                    {
                        double tarif = (7.46 * Convert.ToInt32(pilga.Text)) / 100;
                        double Copointer = (tarif * pointer);
                        MessageBox.Show("ХВП. По вказаним показникам ви маєте сплатити " + Copointer + " грн.");
                    }
                    
                }
                else MessageBox.Show("Некоректні показники");
            }
            else MessageBox.Show("Введіть показники");
           
        }

        private void COcheck_Checked(object sender, RoutedEventArgs e)
        {
            simple.Visibility = Visibility.Visible;
            wateradds.Visibility = Visibility.Visible;
        }

        private void COcheck_Unchecked(object sender, RoutedEventArgs e)
        {
            simple.Visibility = Visibility.Hidden;
            wateradds.Visibility = Visibility.Hidden;
        }


    }
}
