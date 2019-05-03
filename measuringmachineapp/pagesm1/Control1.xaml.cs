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

namespace MeasuringMachineApp.PagesM1
{
    /// <summary>
    /// Interaction logic for Control1.xaml
    /// </summary>
    public partial class Control1 : Page
    {
        public Control1()
        {
            InitializeComponent();
            DbCountLabel.DataContext = App.MeasurmentCalculationM1;
            DbCountSameOrder.DataContext = App.MeasurmentCalculationM1;
            App.MeasurmentCalculationM1.DatabaseRowNumber = "0";
            App.MeasurmentCalculationM1.ConsecutiveOrders = "0";
        }

        static string MySQLconnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=mjernastanica;SslMode=none";
        private string _tableName;

        private void ReadDb_Click(object sender, RoutedEventArgs e)
        {
            _tableName = "stroj1";
            App.MeasurmentCalculationM2.MeasurementDataLoaded(MySQLconnectionString, _tableName);
        }

        private void OffsetCal_Click(object sender, RoutedEventArgs e)
        {
            _tableName = "stroj1";
            App.MeasurmentCalculationM2.DatabaseCount(MySQLconnectionString, _tableName);
        }
    }
}
