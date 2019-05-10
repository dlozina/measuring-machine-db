using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
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
        // DatabaseTimer
        System.Timers.Timer Clock_M1;
        System.Timers.Timer Clock_M2;
        // Static Class definition
        public static PLCInterface.Interface PlcInterface;
        public static MainWindow mwHandle;
        public static MyDatabase Database;
        public static MyCorrectionDatabase CorrectionDatabase;

        // This is made static to access from another GUI class
        public static MeasurmentCalculation MeasurmentCalculationM1;
        public static MeasurmentCalculation MeasurmentCalculationM2;
        public static MeasurementData MeasurementDataM1;
        public static MeasurementData MeasurementDataM2;
        
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
            CorrectionDatabase = new MyCorrectionDatabase();
            MeasurmentCalculationM1 = new MeasurmentCalculation();
            MeasurmentCalculationM2 = new MeasurmentCalculation();
            MeasurementDataM1 = new MeasurementData();
            MeasurementDataM2 = new MeasurementData();
            PlcInterface.StartCyclic(); // Possible system null reference
            PlcInterface.Update_Online_Flag += new Interface.OnlineMarker(PLCInterface_PLCOnlineChanged);
            PlcInterface.Update_100_ms += new Interface.UpdateHandler(PLC_Update_100_ms);
            // Database check Machine1
            MeasurmentCalculationM1.DatabaseChanged += OnDatabaseChangedM1;
            Clock_M1 = new System.Timers.Timer(30000);
            //Clock_M1.Elapsed += OnClockmsTickM1;
            Clock_M1.AutoReset = false;
            // Database check Machine2
            MeasurmentCalculationM2.DatabaseChanged += OnDatabaseChangedM2;
            Clock_M2 = new System.Timers.Timer(30000);
            //Clock_M2.Elapsed += OnClockmsTickM2;
            Clock_M2.AutoReset = false;
            // Counter start
            StartCyclic();
        }

        public void StartCyclic()
        {
            Clock_M1.Start();
            Clock_M2.Start();
        }
        // New worker thread
        //private void OnClockmsTickM1(Object source, System.Timers.ElapsedEventArgs e)
        //{
        //    _tableName = "stroj1";
        //    // Get last five values
        //    // New thread
        //    //Thread CheckDatabaseM1 = new Thread(() => MeasurmentCalculationM1.DatabaseCount(MySQLconnectionString, _tableName));
        //    //CheckDatabaseM1.Name = "CheckDatabaseM1";
        //    //CheckDatabaseM1.Start();
        //    MeasurmentCalculationM1.CompareWorkOrder(MySQLconnectionString, _tableName);
        //    MeasurmentCalculationM1.DatabaseCount(MySQLconnectionString, _tableName);
        //    Clock_M1.Start();
        //}
        // New worker thread
        //private void OnClockmsTickM2(Object source, System.Timers.ElapsedEventArgs e)
        //{
        //    _tableName = "stroj2";
        //    // Get last five values
        //    // New thread
        //    //Thread CheckDatabaseM2 = new Thread(() => MeasurmentCalculationM2.DatabaseCount(MySQLconnectionString, _tableName));
        //    //CheckDatabaseM2.Name = "CheckDatabaseM2";
        //    //CheckDatabaseM2.Start();
        //    MeasurmentCalculationM2.DatabaseCount(MySQLconnectionString, _tableName);
        //    Clock_M2.Start();
        //}

        // Calculate correction when we have results for M2
        public void OnDatabaseChangedM1(object source, EventArgs e)
        {
            // Corection value for diameter C
            MeasurementDataM1.CorrectionCno1 = MeasurmentCalculationM1.CAverageValueMeas1 - MeasurementDataM1.cNominal;
            MeasurementDataM1.CorrectionCno2 = MeasurmentCalculationM1.CAverageValueMeas2 - MeasurementDataM1.cNominal;
            MeasurementDataM1.CorrectionCno3 = MeasurmentCalculationM1.CAverageValueMeas3 - MeasurementDataM1.cNominal;
            MeasurementDataM1.CorrectionCno4 = MeasurmentCalculationM1.CAverageValueMeas4 - MeasurementDataM1.cNominal;
            MeasurementDataM1.CorrectionCno5 = MeasurmentCalculationM1.CAverageValueMeas5 - MeasurementDataM1.cNominal;
            MeasurementDataM1.CorrectionCforMachine = ((MeasurementDataM1.CorrectionCno1 + MeasurementDataM1.CorrectionCno2 +
                                                     MeasurementDataM1.CorrectionCno3 + MeasurementDataM1.CorrectionCno4 +
                                                     MeasurementDataM1.CorrectionCno5)/5);
            // Corection value for diameter A (Two Point)
            MeasurementDataM1.CorrectionA2no1 = MeasurmentCalculationM1.AtwoPointAverageValueMeas1 - MeasurementDataM1.aNominal;
            MeasurementDataM1.CorrectionA2no2 = MeasurmentCalculationM1.AtwoPointAverageValueMeas2 - MeasurementDataM1.aNominal;
            MeasurementDataM1.CorrectionA2no3 = MeasurmentCalculationM1.AtwoPointAverageValueMeas3 - MeasurementDataM1.aNominal;
            MeasurementDataM1.CorrectionA2no4 = MeasurmentCalculationM1.AtwoPointAverageValueMeas4 - MeasurementDataM1.aNominal;
            MeasurementDataM1.CorrectionA2no5 = MeasurmentCalculationM1.AtwoPointAverageValueMeas5 - MeasurementDataM1.aNominal;
            MeasurementDataM1.CorrectionA2forMachine = ((MeasurementDataM1.CorrectionA2no1 + MeasurementDataM1.CorrectionA2no2 +
                                                     MeasurementDataM1.CorrectionA2no3 + MeasurementDataM1.CorrectionA2no4 +
                                                     MeasurementDataM1.CorrectionA2no5)/5);
            // Corection value for diameter A (One Point)
            MeasurementDataM1.CorrectionA1no1 = MeasurmentCalculationM1.AonePointAverageValueMeas1 - MeasurementDataM1.aNominal;
            MeasurementDataM1.CorrectionA1no2 = MeasurmentCalculationM1.AonePointAverageValueMeas2 - MeasurementDataM1.aNominal;
            MeasurementDataM1.CorrectionA1no3 = MeasurmentCalculationM1.AonePointAverageValueMeas3 - MeasurementDataM1.aNominal;
            MeasurementDataM1.CorrectionA1no4 = MeasurmentCalculationM1.AonePointAverageValueMeas4 - MeasurementDataM1.aNominal;
            MeasurementDataM1.CorrectionA1no5 = MeasurmentCalculationM1.AonePointAverageValueMeas5 - MeasurementDataM1.aNominal;
            MeasurementDataM1.CorrectionA1forMachine = ((MeasurementDataM1.CorrectionA1no1 + MeasurementDataM1.CorrectionA1no2 +
                                                      MeasurementDataM1.CorrectionA1no3 + MeasurementDataM1.CorrectionA1no4 +
                                                      MeasurementDataM1.CorrectionA1no5)/5);
            // Corection value for diameter B
            MeasurementDataM1.CorrectionBno1 = MeasurmentCalculationM1.BAverageValueMeas1 - MeasurementDataM1.bNominal;
            MeasurementDataM1.CorrectionBno2 = MeasurmentCalculationM1.BAverageValueMeas2 - MeasurementDataM1.bNominal;
            MeasurementDataM1.CorrectionBno3 = MeasurmentCalculationM1.BAverageValueMeas3 - MeasurementDataM1.bNominal;
            MeasurementDataM1.CorrectionBno4 = MeasurmentCalculationM1.BAverageValueMeas4 - MeasurementDataM1.bNominal;
            MeasurementDataM1.CorrectionBno5 = MeasurmentCalculationM1.BAverageValueMeas5 - MeasurementDataM1.bNominal;
            MeasurementDataM1.CorrectionBforMachine = ((MeasurementDataM1.CorrectionBno1 + MeasurementDataM1.CorrectionBno2 +
                                                      MeasurementDataM1.CorrectionBno3 + MeasurementDataM1.CorrectionBno4 +
                                                      MeasurementDataM1.CorrectionBno5)/5);
            // Corection value for diameter J - Add new Measurement

            // Corection value for diameter F
            MeasurementDataM1.CorrectionFno1 = MeasurmentCalculationM1.FAverageValueMeas1 - MeasurementDataM1.fNominal;
            MeasurementDataM1.CorrectionFno2 = MeasurmentCalculationM1.FAverageValueMeas2 - MeasurementDataM1.fNominal;
            MeasurementDataM1.CorrectionFno3 = MeasurmentCalculationM1.FAverageValueMeas3 - MeasurementDataM1.fNominal;
            MeasurementDataM1.CorrectionFno4 = MeasurmentCalculationM1.FAverageValueMeas4 - MeasurementDataM1.fNominal;
            MeasurementDataM1.CorrectionFno5 = MeasurmentCalculationM1.FAverageValueMeas5 - MeasurementDataM1.fNominal;
            MeasurementDataM1.CorrectionFforMachine = ((MeasurementDataM1.CorrectionFno1 + MeasurementDataM1.CorrectionFno2 +
                                                     MeasurementDataM1.CorrectionFno3 + MeasurementDataM1.CorrectionFno4 +
                                                     MeasurementDataM1.CorrectionFno5)/5);
            // Corection value for diameter E
            MeasurementDataM1.CorrectionEno1 = MeasurmentCalculationM1.EAverageValueMeas1 - MeasurementDataM1.eNominal;
            MeasurementDataM1.CorrectionEno2 = MeasurmentCalculationM1.EAverageValueMeas2 - MeasurementDataM1.eNominal;
            MeasurementDataM1.CorrectionEno3 = MeasurmentCalculationM1.EAverageValueMeas3 - MeasurementDataM1.eNominal;
            MeasurementDataM1.CorrectionEno4 = MeasurmentCalculationM1.EAverageValueMeas4 - MeasurementDataM1.eNominal;
            MeasurementDataM1.CorrectionEno5 = MeasurmentCalculationM1.EAverageValueMeas5 - MeasurementDataM1.eNominal;
            MeasurementDataM1.CorrectionEforMachine = ((MeasurementDataM1.CorrectionEno1 + MeasurementDataM1.CorrectionEno2 +
                                                     MeasurementDataM1.CorrectionEno3 + MeasurementDataM1.CorrectionEno4 +
                                                     MeasurementDataM1.CorrectionEno5)/5);
            // Corection value for diameter D
            //MeasurementDataM1.CorrectionDno1 = MeasurmentCalculationM2.DAverageValueMeas1 - MeasurementDataM1.dNominalM2;
            //MeasurementDataM1.CorrectionDno2 = MeasurmentCalculationM2.DAverageValueMeas2 - MeasurementDataM1.dNominalM2;
            //MeasurementDataM1.CorrectionDno3 = MeasurmentCalculationM2.DAverageValueMeas3 - MeasurementDataM1.dNominalM2;
            //MeasurementDataM1.CorrectionDno4 = MeasurmentCalculationM2.DAverageValueMeas4 - MeasurementDataM1.dNominalM2;
            //MeasurementDataM1.CorrectionDno5 = MeasurmentCalculationM2.DAverageValueMeas5 - MeasurementDataM1.dNominalM2;
            //MeasurementDataM1.CorrectionDforMachine = (MeasurementDataM1.CorrectionDno1 + MeasurementDataM1.CorrectionDno2 +
            //                                         MeasurementDataM1.CorrectionDno3 + MeasurementDataM1.CorrectionDno4 +
            //                                         MeasurementDataM1.CorrectionDno5);

             // Write corrections in DB
            _tableName = "korekcijestroj1";
            CorrectionDatabase.ModifyDb(MySQLconnectionString, _tableName);
        }

        // Calculate correction when we have results for M2
        public void OnDatabaseChangedM2(object source, EventArgs e)
        {
            // Corection value for diameter C
            MeasurementDataM2.CorrectionCno1 = MeasurmentCalculationM2.CAverageValueMeas1 - MeasurementDataM2.cNominal;
            MeasurementDataM2.CorrectionCno2 = MeasurmentCalculationM2.CAverageValueMeas2 - MeasurementDataM2.cNominal;
            MeasurementDataM2.CorrectionCno3 = MeasurmentCalculationM2.CAverageValueMeas3 - MeasurementDataM2.cNominal;
            MeasurementDataM2.CorrectionCno4 = MeasurmentCalculationM2.CAverageValueMeas4 - MeasurementDataM2.cNominal;
            MeasurementDataM2.CorrectionCno5 = MeasurmentCalculationM2.CAverageValueMeas1 - MeasurementDataM2.cNominal;
            MeasurementDataM2.CorrectionCforMachine = (MeasurementDataM2.CorrectionCno1 + MeasurementDataM2.CorrectionCno2 +
                                                     MeasurementDataM2.CorrectionCno3 + MeasurementDataM2.CorrectionCno4 +
                                                     MeasurementDataM2.CorrectionCno5);
            // Corection value for diameter A (Two Point)
            MeasurementDataM2.CorrectionA2no1 = MeasurmentCalculationM2.AtwoPointAverageValueMeas1 - MeasurementDataM2.aNominal;
            MeasurementDataM2.CorrectionA2no2 = MeasurmentCalculationM2.AtwoPointAverageValueMeas1 - MeasurementDataM2.aNominal;
            MeasurementDataM2.CorrectionA2no3 = MeasurmentCalculationM2.AtwoPointAverageValueMeas1 - MeasurementDataM2.aNominal;
            MeasurementDataM2.CorrectionA2no4 = MeasurmentCalculationM2.AtwoPointAverageValueMeas1 - MeasurementDataM2.aNominal;
            MeasurementDataM2.CorrectionA2no5 = MeasurmentCalculationM2.AtwoPointAverageValueMeas1 - MeasurementDataM2.aNominal;
            MeasurementDataM2.CorrectionA2forMachine = (MeasurementDataM2.CorrectionA2no1 + MeasurementDataM2.CorrectionA2no2 +
                                                     MeasurementDataM2.CorrectionA2no3 + MeasurementDataM2.CorrectionA2no4 +
                                                     MeasurementDataM2.CorrectionA2no5);
            // Corection value for diameter A (One Point)
            MeasurementDataM2.CorrectionA1no1 = MeasurmentCalculationM2.AonePointAverageValueMeas1 - MeasurementDataM2.aNominal;
            MeasurementDataM2.CorrectionA1no2 = MeasurmentCalculationM2.AonePointAverageValueMeas1 - MeasurementDataM2.aNominal;
            MeasurementDataM2.CorrectionA1no3 = MeasurmentCalculationM2.AonePointAverageValueMeas1 - MeasurementDataM2.aNominal;
            MeasurementDataM2.CorrectionA1no4 = MeasurmentCalculationM2.AonePointAverageValueMeas1 - MeasurementDataM2.aNominal;
            MeasurementDataM2.CorrectionA1no5 = MeasurmentCalculationM2.AonePointAverageValueMeas1 - MeasurementDataM2.aNominal;
            MeasurementDataM2.CorrectionA1forMachine = (MeasurementDataM2.CorrectionA1no1 + MeasurementDataM2.CorrectionA1no2 +
                                                      MeasurementDataM2.CorrectionA1no3 + MeasurementDataM2.CorrectionA1no4 +
                                                      MeasurementDataM2.CorrectionA1no5);
            // Corection value for diameter B
            MeasurementDataM2.CorrectionBno1 = MeasurmentCalculationM2.BAverageValueMeas1 - MeasurementDataM2.bNominal;
            MeasurementDataM2.CorrectionBno2 = MeasurmentCalculationM2.BAverageValueMeas2 - MeasurementDataM2.bNominal;
            MeasurementDataM2.CorrectionBno3 = MeasurmentCalculationM2.BAverageValueMeas3 - MeasurementDataM2.bNominal;
            MeasurementDataM2.CorrectionBno4 = MeasurmentCalculationM2.BAverageValueMeas4 - MeasurementDataM2.bNominal;
            MeasurementDataM2.CorrectionBno5 = MeasurmentCalculationM2.BAverageValueMeas5 - MeasurementDataM2.bNominal;
            MeasurementDataM2.CorrectionBforMachine = (MeasurementDataM2.CorrectionBno1 + MeasurementDataM2.CorrectionBno2 +
                                                      MeasurementDataM2.CorrectionBno3 + MeasurementDataM2.CorrectionBno4 +
                                                      MeasurementDataM2.CorrectionBno5);
            // Corection value for diameter J - Add new Measurement

            // Corection value for diameter F
            MeasurementDataM2.CorrectionFno1 = MeasurmentCalculationM2.FAverageValueMeas1 - MeasurementDataM2.fNominal;
            MeasurementDataM2.CorrectionFno2 = MeasurmentCalculationM2.FAverageValueMeas2 - MeasurementDataM2.fNominal;
            MeasurementDataM2.CorrectionFno3 = MeasurmentCalculationM2.FAverageValueMeas3 - MeasurementDataM2.fNominal;
            MeasurementDataM2.CorrectionFno4 = MeasurmentCalculationM2.FAverageValueMeas4 - MeasurementDataM2.fNominal;
            MeasurementDataM2.CorrectionFno5 = MeasurmentCalculationM2.FAverageValueMeas5 - MeasurementDataM2.fNominal;
            MeasurementDataM2.CorrectionFforMachine = (MeasurementDataM2.CorrectionFno1 + MeasurementDataM2.CorrectionFno2 +
                                                     MeasurementDataM2.CorrectionFno3 + MeasurementDataM2.CorrectionFno4 +
                                                     MeasurementDataM2.CorrectionFno5);
            // Corection value for diameter E
            MeasurementDataM2.CorrectionEno1 = MeasurmentCalculationM2.EAverageValueMeas1 - MeasurementDataM2.eNominal;
            MeasurementDataM2.CorrectionEno2 = MeasurmentCalculationM2.EAverageValueMeas2 - MeasurementDataM2.eNominal;
            MeasurementDataM2.CorrectionEno3 = MeasurmentCalculationM2.EAverageValueMeas3 - MeasurementDataM2.eNominal;
            MeasurementDataM2.CorrectionEno4 = MeasurmentCalculationM2.EAverageValueMeas4 - MeasurementDataM2.eNominal;
            MeasurementDataM2.CorrectionEno5 = MeasurmentCalculationM2.EAverageValueMeas5 - MeasurementDataM2.eNominal;
            MeasurementDataM2.CorrectionEforMachine = (MeasurementDataM2.CorrectionEno1 + MeasurementDataM2.CorrectionEno2 +
                                                     MeasurementDataM2.CorrectionEno3 + MeasurementDataM2.CorrectionEno4 +
                                                     MeasurementDataM2.CorrectionEno5);
            // Corection value for diameter D
            //MeasurementDataM1.CorrectionDno1 = MeasurmentCalculationM2.DAverageValueMeas1 - MeasurementDataM1.dNominalM2;
            //MeasurementDataM1.CorrectionDno2 = MeasurmentCalculationM2.DAverageValueMeas2 - MeasurementDataM1.dNominalM2;
            //MeasurementDataM1.CorrectionDno3 = MeasurmentCalculationM2.DAverageValueMeas3 - MeasurementDataM1.dNominalM2;
            //MeasurementDataM1.CorrectionDno4 = MeasurmentCalculationM2.DAverageValueMeas4 - MeasurementDataM1.dNominalM2;
            //MeasurementDataM1.CorrectionDno5 = MeasurmentCalculationM2.DAverageValueMeas5 - MeasurementDataM1.dNominalM2;
            //MeasurementDataM1.CorrectionDforMachine = (MeasurementDataM1.CorrectionDno1 + MeasurementDataM1.CorrectionDno2 +
            //                                         MeasurementDataM1.CorrectionDno3 + MeasurementDataM1.CorrectionDno4 +
            //                                         MeasurementDataM1.CorrectionDno5);
        }

        private void PLC_Update_100_ms(Interface sender, InterfaceEventArgs e)
        {
            String msg = "SISTEM SPREMAN";
            // Workpiece Nominal Values from Machine 1
            MeasurementDataM1.cNominal = (float)e.StatusData.Machine1Data.C.Value;
            MeasurementDataM1.aNominal = (float)e.StatusData.Machine1Data.A.Value;
            MeasurementDataM1.bNominal = (float)e.StatusData.Machine1Data.B.Value;
            MeasurementDataM1.jNominal = (float)e.StatusData.Machine1Data.J.Value;
            MeasurementDataM1.fNominal = (float)e.StatusData.Machine1Data.F.Value;
            MeasurementDataM1.eNominal = (float)e.StatusData.Machine1Data.E.Value;
            MeasurementDataM1.dNominal = (float)e.StatusData.Machine1Data.D.Value;
            MeasurementDataM1.gNominal = (float)e.StatusData.Machine1Data.G.Value;
            // Workpiece Nominal Values from Machine 2
            MeasurementDataM2.cNominal = (float)e.StatusData.Machine2Data.C.Value;
            MeasurementDataM2.aNominal = (float)e.StatusData.Machine2Data.A.Value;
            MeasurementDataM2.bNominal = (float)e.StatusData.Machine2Data.B.Value;
            MeasurementDataM2.jNominal = (float)e.StatusData.Machine2Data.J.Value;
            MeasurementDataM2.fNominal = (float)e.StatusData.Machine2Data.F.Value;
            MeasurementDataM2.eNominal = (float)e.StatusData.Machine2Data.E.Value;
            MeasurementDataM2.dNominal = (float)e.StatusData.Machine2Data.D.Value;
            MeasurementDataM2.gNominal = (float)e.StatusData.Machine2Data.G.Value;

            // Signal to fill SQL Database for Machine1
            if ((bool)e.StatusData.Savedata.M1.Value && _oneCallFlagSaveM1)
            {
                _oneCallFlagSaveM1 = false;
                _tableName = "stroj1";
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
                // A
                Database.KotaAPoz1 = (float)e.StatusData.MeasuredPos1.A.Value;
                Database.KotaAPoz2 = (float)e.StatusData.MeasuredPos2.A.Value;
                // B
                Database.KotaBPoz1 = (float)e.StatusData.MeasuredPos1.B.Value;
                Database.KotaBPoz2 = (float)e.StatusData.MeasuredPos2.B.Value;
                // J
                Database.KotaJPoz1 = (float)e.StatusData.MeasuredPos1.J.Value;
                Database.KotaJPoz2 = (float)e.StatusData.MeasuredPos2.J.Value;
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
                // New thread
                Thread WriteToDatabaseM1 = new Thread(() => Database.ModifyDb(MySQLconnectionString, _tableName));
                WriteToDatabaseM1.Name = "WriteToDatabaseM1";
                WriteToDatabaseM1.Start();

                //Database.ModifyDb(MySQLconnectionString, _tableName);
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
                // A
                Database.KotaAPoz1 = (float)e.StatusData.MeasuredPos1.A.Value;
                Database.KotaAPoz2 = (float)e.StatusData.MeasuredPos2.A.Value;
                // B
                Database.KotaBPoz1 = (float)e.StatusData.MeasuredPos1.B.Value;
                Database.KotaBPoz2 = (float)e.StatusData.MeasuredPos2.B.Value;
                // J
                Database.KotaJPoz1 = (float)e.StatusData.MeasuredPos1.J.Value;
                Database.KotaJPoz2 = (float)e.StatusData.MeasuredPos2.J.Value;
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
                // New thread
                Thread WriteToDatabaseM2 = new Thread(() => Database.ModifyDb(MySQLconnectionString, _tableName));
                WriteToDatabaseM2.Name = "WriteToDatabaseM2";
                WriteToDatabaseM2.Start();

                //Database.ModifyDb(MySQLconnectionString, _tableName);
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
