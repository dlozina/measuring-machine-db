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
            // DB counters
            DbCountLabel.DataContext = App.MeasurmentCalculationM1;
            DbCountSameOrder.DataContext = App.MeasurmentCalculationM1;
            // Corection Calculation
            CorectionC.DataContext = App.MeasurementDataM1;
            CorectionA2.DataContext = App.MeasurementDataM1;
            CorectionA1.DataContext = App.MeasurementDataM1;
            CorectionB.DataContext = App.MeasurementDataM1;
            CorectionJ.DataContext = App.MeasurementDataM1;
            CorectionF.DataContext = App.MeasurementDataM1;
            CorectionE.DataContext = App.MeasurementDataM1;
            // Init values
            //App.MeasurmentCalculationM1.DatabaseRowNumber = "0";
            //App.MeasurmentCalculationM1.ConsecutiveOrders = "0";
            //App.MeasurementDataM1.CorrectionCforMachine = 0.0f;
            //App.MeasurementDataM1.CorrectionA2forMachine = 0.0f;
            //App.MeasurementDataM1.CorrectionA1forMachine = 0.0f;
            //App.MeasurementDataM1.CorrectionBforMachine = 0.0f;
            //App.MeasurementDataM1.CorrectionJforMachine = 0.0f;
            //App.MeasurementDataM1.CorrectionFforMachine = 0.0f;
            //App.MeasurementDataM1.CorrectionEforMachine = 0.0f;
        }

        static string MySQLconnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=mjernastanica;SslMode=none";
        private string _tableName;

        // Test method
        private void ReadDb_Click(object sender, RoutedEventArgs e)
        {
            _tableName = "stroj1";
            App.MeasurmentCalculationM2.MeasurementDataLoaded(MySQLconnectionString, _tableName);
        }
        // Test method
        private void OffsetCal_Click(object sender, RoutedEventArgs e)
        {
            _tableName = "stroj1";
            App.MeasurmentCalculationM2.DatabaseCount(MySQLconnectionString, _tableName);
        }
    }
}
