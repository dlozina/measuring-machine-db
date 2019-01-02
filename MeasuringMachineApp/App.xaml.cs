using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using DataBase;
using PLCInterface;

namespace MeasuringMachineApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static PLCInterface.Interface PlcInterface;
        public static MainWindow mwHandle;
        public static MyDatabase Database;
        // SslMode=none - If local host does not support SSL
        static string MySQLconnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=mjernastanica;SslMode=none";
        private bool _oneCallFlagRecord;

        public App()
        {
            //PlcInterface = new PLCInterface.Interface();
            PlcInterface = new Interface();
            Database = new MyDatabase();
            PlcInterface.StartCyclic(); // Possible system null reference
            PlcInterface.Update_Online_Flag += new Interface.OnlineMarker(PLCInterface_PLCOnlineChanged);
            PlcInterface.Update_100_ms += new Interface.UpdateHandler(PLC_Update_100_ms);
        }

        private void PLC_Update_100_ms(Interface sender, InterfaceEventArgs e)
        {
            String msg = "SISTEM SPREMAN";

            // Signal to fill SQL Database
            if ((bool)e.StatusData.Data.Record.Value && _oneCallFlagRecord)
            {
                _oneCallFlagRecord = false;
                // Value setting
                // Database.RadniNalog = "Dino";
                Database.KotaCPoz1 = (float) e.StatusData.Measured.C.Value;
                Database.KotaA11Poz1 = (float)e.StatusData.Measured.A11.Value;
                Database.KotaA12Poz1 = (float)e.StatusData.Measured.A12.Value;
                Database.KotaBPoz1 = (float)e.StatusData.Measured.B.Value;
                Database.KotaF1LG2Poz1 = (float)e.StatusData.Measured.F.Value;
                Database.KotaEPoz1 = (float)e.StatusData.Measured.E.Value;
                Database.KotaDPoz1 = (float)e.StatusData.Measured.D.Value;
                Database.RadniNalog = (string) e.StatusData.Measured.RadniNalog.Value;
                // Fill SQL base
                Database.ModifyDb(MySQLconnectionString);
            }
            else if (!(bool)e.StatusData.Data.Record.Value)
            {
                _oneCallFlagRecord = true;
            }



            if (mwHandle != null)
            {
                mwHandle.TbStatusMessage.Dispatcher.BeginInvoke((Action)(() => { mwHandle.TbStatusMessage.Text = msg; }));
            }

        }
        private void PLCInterface_PLCOnlineChanged(object sender, OnlineMarkerEventArgs e)
        {
            if (e.OnlineMark)
            {
                mwHandle.onlineFlag.Dispatcher.BeginInvoke((Action)(() => { mwHandle.onlineFlag.Fill = new LinearGradientBrush(Colors.Green, Colors.White, 0.0); }));
                mwHandle.TbConnectionStatus.Dispatcher.BeginInvoke((Action)(() => { mwHandle.TbConnectionStatus.Text = "PLC Status: Online"; }));
            }
            else
            {
                mwHandle.onlineFlag.Dispatcher.BeginInvoke((Action)(() => { mwHandle.onlineFlag.Fill = new LinearGradientBrush((Color)ColorConverter.ConvertFromString("#FF979797"), Colors.White, 0.0); }));
                mwHandle.TbConnectionStatus.Dispatcher.BeginInvoke((Action)(() => { mwHandle.TbConnectionStatus.Text = "PLC Status: Offline"; }));
            }
        }
    }
}
