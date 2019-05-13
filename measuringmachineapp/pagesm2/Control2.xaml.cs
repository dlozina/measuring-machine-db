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

namespace MeasuringMachineApp.PagesM2
{
    /// <summary>
    /// Interaction logic for Control2.xaml
    /// </summary>
    public partial class Control2 : Page
    {
        public Control2()
        {
            InitializeComponent();
            // DB counters
            DbCountLabel.DataContext = App.MeasurmentCalculationM2;
            DbCountSameOrder.DataContext = App.MeasurmentCalculationM2;
            // Corection Calculation
            CorectionC.DataContext = App.MeasurementDataM2;
            CorectionA2.DataContext = App.MeasurementDataM2;
            CorectionA1.DataContext = App.MeasurementDataM2;
            CorectionB.DataContext = App.MeasurementDataM2;
            CorectionF.DataContext = App.MeasurementDataM2;
            CorectionE.DataContext = App.MeasurementDataM2;
            // Init values
            //App.MeasurmentCalculationM1.DatabaseRowNumber = "0";
            //App.MeasurmentCalculationM1.ConsecutiveOrders = "0";
            App.MeasurementDataM2.CorrectionCforMachine = 0.0f;
            App.MeasurementDataM2.CorrectionA2forMachine = 0.0f;
            App.MeasurementDataM2.CorrectionA1forMachine = 0.0f;
            App.MeasurementDataM2.CorrectionBforMachine = 0.0f;
            App.MeasurementDataM2.CorrectionFforMachine = 0.0f;
            App.MeasurementDataM2.CorrectionEforMachine = 0.0f;
        }
    }
}
