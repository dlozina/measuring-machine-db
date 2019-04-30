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
        // DatabaseTimer
        System.Timers.Timer Clock_M1;
        System.Timers.Timer Clock_M2;
        // Static Class definition
        public static PLCInterface.Interface PlcInterface;
        public static MainWindow mwHandle;
        public static MyDatabase Database;
        public MeasurmentCalculation MeasurmentCalculationM1;
        public static MeasurmentCalculation MeasurmentCalculationM2;
        public static MeasurementData MeasurementData;
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
            MeasurmentCalculationM1 = new MeasurmentCalculation();
            MeasurmentCalculationM2 = new MeasurmentCalculation();
            MeasurementData = new MeasurementData();
            PlcInterface.StartCyclic(); // Possible system null reference
            PlcInterface.Update_Online_Flag += new Interface.OnlineMarker(PLCInterface_PLCOnlineChanged);
            PlcInterface.Update_100_ms += new Interface.UpdateHandler(PLC_Update_100_ms);
            // Database check Machine1
            MeasurmentCalculationM1.DatabaseChanged += OnDatabaseChangedM1;
            Clock_M1 = new System.Timers.Timer(30000);
            Clock_M1.Elapsed += OnClockmsTickM1;
            Clock_M1.AutoReset = false;
            // Database check Machine2
            MeasurmentCalculationM2.DatabaseChanged += OnDatabaseChangedM2;
            Clock_M2 = new System.Timers.Timer(30000);
            Clock_M2.Elapsed += OnClockmsTickM2;
            Clock_M2.AutoReset = false;
            // Counter start
            StartCyclic();
        }

        public void StartCyclic()
        {
            Clock_M1.Start();
            Clock_M2.Start();
        }

        private void OnClockmsTickM1(Object source, System.Timers.ElapsedEventArgs e)
        {
            // Get last five values
            MeasurmentCalculationM1.DatabaseCount();
            Clock_M1.Start();
        }

        private void OnClockmsTickM2(Object source, System.Timers.ElapsedEventArgs e)
        {
            // Get last five values
            MeasurmentCalculationM2.DatabaseCount();
            Clock_M2.Start();
        }

        // Calculate correction when we have results for M2
        public void OnDatabaseChangedM1(object source, EventArgs e)
        {
            // Corection value for diameter C
            MeasurementData.CorrectionCno1 = MeasurmentCalculationM1.CAverageValueMeas1 - MeasurementData.cNominalM1;
            MeasurementData.CorrectionCno2 = MeasurmentCalculationM1.CAverageValueMeas2 - MeasurementData.cNominalM1;
            MeasurementData.CorrectionCno3 = MeasurmentCalculationM1.CAverageValueMeas3 - MeasurementData.cNominalM1;
            MeasurementData.CorrectionCno4 = MeasurmentCalculationM1.CAverageValueMeas4 - MeasurementData.cNominalM1;
            MeasurementData.CorrectionCno5 = MeasurmentCalculationM1.CAverageValueMeas1 - MeasurementData.cNominalM1;
            MeasurementData.CorrectionCforMachine = (MeasurementData.CorrectionCno1 + MeasurementData.CorrectionCno2 +
                                                     MeasurementData.CorrectionCno3 + MeasurementData.CorrectionCno4 +
                                                     MeasurementData.CorrectionCno5);
            // Corection value for diameter A (Two Point)
            MeasurementData.CorrectionA2no1 = MeasurmentCalculationM1.AtwoPointAverageValueMeas1 - MeasurementData.aNominalM1;
            MeasurementData.CorrectionA2no2 = MeasurmentCalculationM1.AtwoPointAverageValueMeas1 - MeasurementData.aNominalM1;
            MeasurementData.CorrectionA2no3 = MeasurmentCalculationM1.AtwoPointAverageValueMeas1 - MeasurementData.aNominalM1;
            MeasurementData.CorrectionA2no4 = MeasurmentCalculationM1.AtwoPointAverageValueMeas1 - MeasurementData.aNominalM1;
            MeasurementData.CorrectionA2no5 = MeasurmentCalculationM1.AtwoPointAverageValueMeas1 - MeasurementData.aNominalM1;
            MeasurementData.CorrectionA2forMachine = (MeasurementData.CorrectionA2no1 + MeasurementData.CorrectionA2no2 +
                                                     MeasurementData.CorrectionA2no3 + MeasurementData.CorrectionA2no4 +
                                                     MeasurementData.CorrectionA2no5);
            // Corection value for diameter A (One Point)
            MeasurementData.CorrectionA1no1 = MeasurmentCalculationM1.AonePointAverageValueMeas1 - MeasurementData.aNominalM1;
            MeasurementData.CorrectionA1no2 = MeasurmentCalculationM1.AonePointAverageValueMeas1 - MeasurementData.aNominalM1;
            MeasurementData.CorrectionA1no3 = MeasurmentCalculationM1.AonePointAverageValueMeas1 - MeasurementData.aNominalM1;
            MeasurementData.CorrectionA1no4 = MeasurmentCalculationM1.AonePointAverageValueMeas1 - MeasurementData.aNominalM1;
            MeasurementData.CorrectionA1no5 = MeasurmentCalculationM1.AonePointAverageValueMeas1 - MeasurementData.aNominalM1;
            MeasurementData.CorrectionA1forMachine = (MeasurementData.CorrectionA1no1 + MeasurementData.CorrectionA1no2 +
                                                      MeasurementData.CorrectionA1no3 + MeasurementData.CorrectionA1no4 +
                                                      MeasurementData.CorrectionA1no5);
            // Corection value for diameter B
            MeasurementData.CorrectionBno1 = MeasurmentCalculationM1.BAverageValueMeas1 - MeasurementData.bNominalM1;
            MeasurementData.CorrectionBno2 = MeasurmentCalculationM1.BAverageValueMeas2 - MeasurementData.bNominalM1;
            MeasurementData.CorrectionBno3 = MeasurmentCalculationM1.BAverageValueMeas3 - MeasurementData.bNominalM1;
            MeasurementData.CorrectionBno4 = MeasurmentCalculationM1.BAverageValueMeas4 - MeasurementData.bNominalM1;
            MeasurementData.CorrectionBno5 = MeasurmentCalculationM1.BAverageValueMeas5 - MeasurementData.bNominalM1;
            MeasurementData.CorrectionBforMachine = (MeasurementData.CorrectionBno1 + MeasurementData.CorrectionBno2 +
                                                      MeasurementData.CorrectionBno3 + MeasurementData.CorrectionBno4 +
                                                      MeasurementData.CorrectionBno5);
            // Corection value for diameter J - Add new Measurement

            // Corection value for diameter F
            MeasurementData.CorrectionFno1 = MeasurmentCalculationM1.FAverageValueMeas1 - MeasurementData.fNominalM1;
            MeasurementData.CorrectionFno2 = MeasurmentCalculationM1.FAverageValueMeas2 - MeasurementData.fNominalM1;
            MeasurementData.CorrectionFno3 = MeasurmentCalculationM1.FAverageValueMeas3 - MeasurementData.fNominalM1;
            MeasurementData.CorrectionFno4 = MeasurmentCalculationM1.FAverageValueMeas4 - MeasurementData.fNominalM1;
            MeasurementData.CorrectionFno5 = MeasurmentCalculationM1.FAverageValueMeas5 - MeasurementData.fNominalM1;
            MeasurementData.CorrectionFforMachine = (MeasurementData.CorrectionFno1 + MeasurementData.CorrectionFno2 +
                                                     MeasurementData.CorrectionFno3 + MeasurementData.CorrectionFno4 +
                                                     MeasurementData.CorrectionFno5);
            // Corection value for diameter E
            MeasurementData.CorrectionEno1 = MeasurmentCalculationM1.EAverageValueMeas1 - MeasurementData.eNominalM1;
            MeasurementData.CorrectionEno2 = MeasurmentCalculationM1.EAverageValueMeas2 - MeasurementData.eNominalM1;
            MeasurementData.CorrectionEno3 = MeasurmentCalculationM1.EAverageValueMeas3 - MeasurementData.eNominalM1;
            MeasurementData.CorrectionEno4 = MeasurmentCalculationM1.EAverageValueMeas4 - MeasurementData.eNominalM1;
            MeasurementData.CorrectionEno5 = MeasurmentCalculationM1.EAverageValueMeas5 - MeasurementData.eNominalM1;
            MeasurementData.CorrectionEforMachine = (MeasurementData.CorrectionEno1 + MeasurementData.CorrectionEno2 +
                                                     MeasurementData.CorrectionEno3 + MeasurementData.CorrectionEno4 +
                                                     MeasurementData.CorrectionEno5);
            // Corection value for diameter D
            //MeasurementData.CorrectionDno1 = MeasurmentCalculationM2.DAverageValueMeas1 - MeasurementData.dNominalM2;
            //MeasurementData.CorrectionDno2 = MeasurmentCalculationM2.DAverageValueMeas2 - MeasurementData.dNominalM2;
            //MeasurementData.CorrectionDno3 = MeasurmentCalculationM2.DAverageValueMeas3 - MeasurementData.dNominalM2;
            //MeasurementData.CorrectionDno4 = MeasurmentCalculationM2.DAverageValueMeas4 - MeasurementData.dNominalM2;
            //MeasurementData.CorrectionDno5 = MeasurmentCalculationM2.DAverageValueMeas5 - MeasurementData.dNominalM2;
            //MeasurementData.CorrectionDforMachine = (MeasurementData.CorrectionDno1 + MeasurementData.CorrectionDno2 +
            //                                         MeasurementData.CorrectionDno3 + MeasurementData.CorrectionDno4 +
            //                                         MeasurementData.CorrectionDno5);
        }

        // Calculate correction when we have results for M2
        public void OnDatabaseChangedM2(object source, EventArgs e)
        {
            // Corection value for diameter C
            MeasurementData.CorrectionCno1 = MeasurmentCalculationM2.CAverageValueMeas1 - MeasurementData.cNominalM2;
            MeasurementData.CorrectionCno2 = MeasurmentCalculationM2.CAverageValueMeas2 - MeasurementData.cNominalM2;
            MeasurementData.CorrectionCno3 = MeasurmentCalculationM2.CAverageValueMeas3 - MeasurementData.cNominalM2;
            MeasurementData.CorrectionCno4 = MeasurmentCalculationM2.CAverageValueMeas4 - MeasurementData.cNominalM2;
            MeasurementData.CorrectionCno5 = MeasurmentCalculationM2.CAverageValueMeas1 - MeasurementData.cNominalM2;
            MeasurementData.CorrectionCforMachine = (MeasurementData.CorrectionCno1 + MeasurementData.CorrectionCno2 +
                                                     MeasurementData.CorrectionCno3 + MeasurementData.CorrectionCno4 +
                                                     MeasurementData.CorrectionCno5);
            // Corection value for diameter A (Two Point)
            MeasurementData.CorrectionA2no1 = MeasurmentCalculationM2.AtwoPointAverageValueMeas1 - MeasurementData.aNominalM2;
            MeasurementData.CorrectionA2no2 = MeasurmentCalculationM2.AtwoPointAverageValueMeas1 - MeasurementData.aNominalM2;
            MeasurementData.CorrectionA2no3 = MeasurmentCalculationM2.AtwoPointAverageValueMeas1 - MeasurementData.aNominalM2;
            MeasurementData.CorrectionA2no4 = MeasurmentCalculationM2.AtwoPointAverageValueMeas1 - MeasurementData.aNominalM2;
            MeasurementData.CorrectionA2no5 = MeasurmentCalculationM2.AtwoPointAverageValueMeas1 - MeasurementData.aNominalM2;
            MeasurementData.CorrectionA2forMachine = (MeasurementData.CorrectionA2no1 + MeasurementData.CorrectionA2no2 +
                                                     MeasurementData.CorrectionA2no3 + MeasurementData.CorrectionA2no4 +
                                                     MeasurementData.CorrectionA2no5);
            // Corection value for diameter A (One Point)
            MeasurementData.CorrectionA1no1 = MeasurmentCalculationM2.AonePointAverageValueMeas1 - MeasurementData.aNominalM2;
            MeasurementData.CorrectionA1no2 = MeasurmentCalculationM2.AonePointAverageValueMeas1 - MeasurementData.aNominalM2;
            MeasurementData.CorrectionA1no3 = MeasurmentCalculationM2.AonePointAverageValueMeas1 - MeasurementData.aNominalM2;
            MeasurementData.CorrectionA1no4 = MeasurmentCalculationM2.AonePointAverageValueMeas1 - MeasurementData.aNominalM2;
            MeasurementData.CorrectionA1no5 = MeasurmentCalculationM2.AonePointAverageValueMeas1 - MeasurementData.aNominalM2;
            MeasurementData.CorrectionA1forMachine = (MeasurementData.CorrectionA1no1 + MeasurementData.CorrectionA1no2 +
                                                      MeasurementData.CorrectionA1no3 + MeasurementData.CorrectionA1no4 +
                                                      MeasurementData.CorrectionA1no5);
            // Corection value for diameter B
            MeasurementData.CorrectionBno1 = MeasurmentCalculationM2.BAverageValueMeas1 - MeasurementData.bNominalM2;
            MeasurementData.CorrectionBno2 = MeasurmentCalculationM2.BAverageValueMeas2 - MeasurementData.bNominalM2;
            MeasurementData.CorrectionBno3 = MeasurmentCalculationM2.BAverageValueMeas3 - MeasurementData.bNominalM2;
            MeasurementData.CorrectionBno4 = MeasurmentCalculationM2.BAverageValueMeas4 - MeasurementData.bNominalM2;
            MeasurementData.CorrectionBno5 = MeasurmentCalculationM2.BAverageValueMeas5 - MeasurementData.bNominalM2;
            MeasurementData.CorrectionBforMachine = (MeasurementData.CorrectionBno1 + MeasurementData.CorrectionBno2 +
                                                      MeasurementData.CorrectionBno3 + MeasurementData.CorrectionBno4 +
                                                      MeasurementData.CorrectionBno5);
            // Corection value for diameter J - Add new Measurement

            // Corection value for diameter F
            MeasurementData.CorrectionFno1 = MeasurmentCalculationM2.FAverageValueMeas1 - MeasurementData.fNominalM2;
            MeasurementData.CorrectionFno2 = MeasurmentCalculationM2.FAverageValueMeas2 - MeasurementData.fNominalM2;
            MeasurementData.CorrectionFno3 = MeasurmentCalculationM2.FAverageValueMeas3 - MeasurementData.fNominalM2;
            MeasurementData.CorrectionFno4 = MeasurmentCalculationM2.FAverageValueMeas4 - MeasurementData.fNominalM2;
            MeasurementData.CorrectionFno5 = MeasurmentCalculationM2.FAverageValueMeas5 - MeasurementData.fNominalM2;
            MeasurementData.CorrectionFforMachine = (MeasurementData.CorrectionFno1 + MeasurementData.CorrectionFno2 +
                                                     MeasurementData.CorrectionFno3 + MeasurementData.CorrectionFno4 +
                                                     MeasurementData.CorrectionFno5);
            // Corection value for diameter E
            MeasurementData.CorrectionEno1 = MeasurmentCalculationM2.EAverageValueMeas1 - MeasurementData.eNominalM2;
            MeasurementData.CorrectionEno2 = MeasurmentCalculationM2.EAverageValueMeas2 - MeasurementData.eNominalM2;
            MeasurementData.CorrectionEno3 = MeasurmentCalculationM2.EAverageValueMeas3 - MeasurementData.eNominalM2;
            MeasurementData.CorrectionEno4 = MeasurmentCalculationM2.EAverageValueMeas4 - MeasurementData.eNominalM2;
            MeasurementData.CorrectionEno5 = MeasurmentCalculationM2.EAverageValueMeas5 - MeasurementData.eNominalM2;
            MeasurementData.CorrectionEforMachine = (MeasurementData.CorrectionEno1 + MeasurementData.CorrectionEno2 +
                                                     MeasurementData.CorrectionEno3 + MeasurementData.CorrectionEno4 +
                                                     MeasurementData.CorrectionEno5);
            // Corection value for diameter D
            //MeasurementData.CorrectionDno1 = MeasurmentCalculationM2.DAverageValueMeas1 - MeasurementData.dNominalM2;
            //MeasurementData.CorrectionDno2 = MeasurmentCalculationM2.DAverageValueMeas2 - MeasurementData.dNominalM2;
            //MeasurementData.CorrectionDno3 = MeasurmentCalculationM2.DAverageValueMeas3 - MeasurementData.dNominalM2;
            //MeasurementData.CorrectionDno4 = MeasurmentCalculationM2.DAverageValueMeas4 - MeasurementData.dNominalM2;
            //MeasurementData.CorrectionDno5 = MeasurmentCalculationM2.DAverageValueMeas5 - MeasurementData.dNominalM2;
            //MeasurementData.CorrectionDforMachine = (MeasurementData.CorrectionDno1 + MeasurementData.CorrectionDno2 +
            //                                         MeasurementData.CorrectionDno3 + MeasurementData.CorrectionDno4 +
            //                                         MeasurementData.CorrectionDno5);
        }

        private void PLC_Update_100_ms(Interface sender, InterfaceEventArgs e)
        {
            String msg = "SISTEM SPREMAN";
            // Workpiece Nominal Values from Machine 1
            MeasurementData.cNominalM1 = (float)e.StatusData.Machine1Data.C.Value;
            MeasurementData.aNominalM1 = (float)e.StatusData.Machine1Data.A.Value;
            MeasurementData.bNominalM1 = (float)e.StatusData.Machine1Data.B.Value;
            MeasurementData.jNominalM1 = (float)e.StatusData.Machine1Data.J.Value;
            MeasurementData.fNominalM1 = (float)e.StatusData.Machine1Data.F.Value;
            MeasurementData.eNominalM1 = (float)e.StatusData.Machine1Data.E.Value;
            MeasurementData.dNominalM1 = (float)e.StatusData.Machine1Data.D.Value;
            MeasurementData.gNominalM1 = (float)e.StatusData.Machine1Data.G.Value;
            // Workpiece Nominal Values from Machine 2
            MeasurementData.cNominalM2 = (float)e.StatusData.Machine2Data.C.Value;
            MeasurementData.aNominalM2 = (float)e.StatusData.Machine2Data.A.Value;
            MeasurementData.bNominalM2 = (float)e.StatusData.Machine2Data.B.Value;
            MeasurementData.jNominalM2 = (float)e.StatusData.Machine2Data.J.Value;
            MeasurementData.fNominalM2 = (float)e.StatusData.Machine2Data.F.Value;
            MeasurementData.eNominalM2 = (float)e.StatusData.Machine2Data.E.Value;
            MeasurementData.dNominalM2 = (float)e.StatusData.Machine2Data.D.Value;
            MeasurementData.gNominalM2 = (float)e.StatusData.Machine2Data.G.Value;

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
