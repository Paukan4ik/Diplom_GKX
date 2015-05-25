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
    /// Interaction logic for workerwindow.xaml
    /// </summary>
    public partial class workerwindow : Window
    {
        public workerwindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow log = new MainWindow();
            this.Hide();
            log.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mastersList log = new mastersList();
            this.Hide();
            log.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            addGP log = new addGP();
            this.Hide();
            log.Show();
        }
    }
}
