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
using ToolOffset;

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
        public static MeasurmentCalculation MeasurmentCalculation;
        // SslMode=none - If local host does not support SSL
        static string MySQLconnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=mjernastanica;SslMode=none";
        private bool _oneCallFlagSaveM1;
        private bool _oneCallFlagSaveM2;
        private string _tableName;

        public App()
        {
            //PlcInterface = new PLCInterface.Interface();
            PlcInterface = new Interface();
            Database = new MyDatabase();
            MeasurmentCalculation = new MeasurmentCalculation();
            PlcInterface.StartCyclic(); // Possible system null reference
            PlcInterface.Update_Online_Flag += new Interface.OnlineMarker(PLCInterface_PLCOnlineChanged);
            PlcInterface.Update_100_ms += new Interface.UpdateHandler(PLC_Update_100_ms);
            MeasurmentCalculation.StartCyclic();
        }

        private void PLC_Update_100_ms(Interface sender, InterfaceEventArgs e)
        {
            String msg = "SISTEM SPREMAN";

            // Signal to fill SQL Database for Machine1
            if ((bool)e.StatusData.Savedata.M1.Value && _oneCallFlagSaveM1)
            {
                _oneCallFlagSaveM1 = false;
                _tableName = "stroj1";
                // Value setting
                // C
                Database.KotaCPoz1 = (float) e.StatusData.MeasuredPos1.C.Value;
                Database.KotaCPoz2 = (float)e.StatusData.MeasuredPos2.C.Value;
                // A1.2
                Database.KotaA12Poz1 = (float)e.StatusData.MeasuredPos1.A12.Value;
                Database.KotaA12Poz2 = (float)e.StatusData.MeasuredPos2.A12.Value;
                // A1.1
                Database.KotaA11Poz1 = (float)e.StatusData.MeasuredPos1.A11.Value;
                Database.KotaA11Poz2 = (float)e.StatusData.MeasuredPos2.A11.Value;
                // B
                Database.KotaBPoz1 = (float)e.StatusData.MeasuredPos1.B.Value;
                Database.KotaBPoz2 = (float)e.StatusData.MeasuredPos2.B.Value;
                // F1 AND F2 POS 1
                Database.KotaF1LG2Poz1 = (float)e.StatusData.MeasuredPos1.F1LG2.Value;
                Database.KotaF2LG2Poz1 = (float)e.StatusData.MeasuredPos1.F2LG2.Value;
                Database.KotaF1LG3Poz1 = (float)e.StatusData.MeasuredPos1.F1LG3.Value;
                Database.KotaF2LG3Poz1 = (float)e.StatusData.MeasuredPos1.F2LG3.Value;
                // F1 AND F2 POS 2
                Database.KotaF1LG2Poz2 = (float)e.StatusData.MeasuredPos2.F1LG2.Value;
                Database.KotaF2LG2Poz2 = (float)e.StatusData.MeasuredPos2.F2LG2.Value;
                Database.KotaF1LG3Poz2 = (float)e.StatusData.MeasuredPos2.F1LG3.Value;
                Database.KotaF2LG3Poz2 = (float)e.StatusData.MeasuredPos2.F2LG3.Value;
                // E
                Database.KotaEPoz1 = (float)e.StatusData.MeasuredPos1.E.Value;
                Database.KotaEPoz2 = (float)e.StatusData.MeasuredPos2.E.Value;
                // D
                Database.KotaDPoz1 = (float)e.StatusData.MeasuredPos1.D.Value;
                Database.KotaDPoz2 = (float)e.StatusData.MeasuredPos2.D.Value;
                // H1
                Database.KotaH1Poz1 = (float)e.StatusData.MeasuredPos1.H1.Value;
                Database.KotaH1Poz2 = (float)e.StatusData.MeasuredPos2.H1.Value;
                // K
                Database.KotaKPoz1 = (float)e.StatusData.MeasuredPos1.K.Value;
                Database.KotaKPoz2 = (float)e.StatusData.MeasuredPos2.K.Value;

                // Workpiecedata
                Database.RadniNalog = (string)e.StatusData.Workpiecedata.RadniNalog.Value;

                // Fill SQL base for Machine 1
                Database.ModifyDb(MySQLconnectionString, _tableName);
            }
            else if (!(bool)e.StatusData.Savedata.M1.Value)
            {
                _oneCallFlagSaveM1 = true;
            }

            // Signal to fill SQL Database for Machine2
            if ((bool)e.StatusData.Savedata.M2.Value && _oneCallFlagSaveM2)
            {
                _oneCallFlagSaveM2 = false;
                _tableName = "stroj2";
                // Value setting
                // C
                Database.KotaCPoz1 = (float)e.StatusData.MeasuredPos1.C.Value;
                Database.KotaCPoz2 = (float)e.StatusData.MeasuredPos2.C.Value;
                // A1.2
                Database.KotaA12Poz1 = (float)e.StatusData.MeasuredPos1.A12.Value;
                Database.KotaA12Poz2 = (float)e.StatusData.MeasuredPos2.A12.Value;
                // A1.1
                Database.KotaA11Poz1 = (float)e.StatusData.MeasuredPos1.A11.Value;
                Database.KotaA11Poz2 = (float)e.StatusData.MeasuredPos2.A11.Value;
                // B
                Database.KotaBPoz1 = (float)e.StatusData.MeasuredPos1.B.Value;
                Database.KotaBPoz2 = (float)e.StatusData.MeasuredPos2.B.Value;
                // F1 AND F2 POS 1
                Database.KotaF1LG2Poz1 = (float)e.StatusData.MeasuredPos1.F1LG2.Value;
                Database.KotaF2LG2Poz1 = (float)e.StatusData.MeasuredPos1.F2LG2.Value;
                Database.KotaF1LG3Poz1 = (float)e.StatusData.MeasuredPos1.F1LG3.Value;
                Database.KotaF2LG3Poz1 = (float)e.StatusData.MeasuredPos1.F2LG3.Value;
                // F1 AND F2 POS 2
                Database.KotaF1LG2Poz2 = (float)e.StatusData.MeasuredPos2.F1LG2.Value;
                Database.KotaF2LG2Poz2 = (float)e.StatusData.MeasuredPos2.F2LG2.Value;
                Database.KotaF1LG3Poz2 = (float)e.StatusData.MeasuredPos2.F1LG3.Value;
                Database.KotaF2LG3Poz2 = (float)e.StatusData.MeasuredPos2.F2LG3.Value;
                // E
                Database.KotaEPoz1 = (float)e.StatusData.MeasuredPos1.E.Value;
                Database.KotaEPoz2 = (float)e.StatusData.MeasuredPos2.E.Value;
                // D
                Database.KotaDPoz1 = (float)e.StatusData.MeasuredPos1.D.Value;
                Database.KotaDPoz2 = (float)e.StatusData.MeasuredPos2.D.Value;
                // H1
                Database.KotaH1Poz1 = (float)e.StatusData.MeasuredPos1.H1.Value;
                Database.KotaH1Poz2 = (float)e.StatusData.MeasuredPos2.H1.Value;
                // K
                Database.KotaKPoz1 = (float)e.StatusData.MeasuredPos1.K.Value;
                Database.KotaKPoz2 = (float)e.StatusData.MeasuredPos2.K.Value;

                // Workpiecedata
                Database.RadniNalog = (string)e.StatusData.Workpiecedata.RadniNalog.Value;

                // Fill SQL base for Machine 1
                Database.ModifyDb(MySQLconnectionString, _tableName);
            }
            else if (!(bool)e.StatusData.Savedata.M2.Value)
            {
                _oneCallFlagSaveM2 = true;
            }

            if (mwHandle != null)
            {
                mwHandle.TbStatusMessage.Dispatcher.BeginInvoke((Action)(() => { mwHandle.TbStatusMessage.Text = msg; }));
            }

        }

        private void WriteToDatabaseM1()
        {

        }

        private void WriteToDatabaseM2()
        {

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
