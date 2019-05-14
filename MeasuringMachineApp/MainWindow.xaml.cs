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
using MeasuringMachineApp.PagesM1;
using MeasuringMachineApp.PagesM2;

namespace MeasuringMachineApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            App.mwHandle = this;
        }

        // Machine 1
        private void Control1_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Control1();
        }

        private void Data1_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Data1();
        }

        // Machine 2
        private void Control2_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Control2();
        }

        private void Data2_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Data2();
        }

        private void Manual1_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ManualInput1();
        }

        private void Manual2_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ManualInput2();
        }

        private void DataCor1_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new DataCorrection1();
        }

        private void DataCor2_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new DataCorrection2();
        }
    }
}
