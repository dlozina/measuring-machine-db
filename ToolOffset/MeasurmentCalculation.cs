using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Annotations;
using MySql.Data.MySqlClient;

namespace ToolOffset
{
    public class MeasurmentCalculation : INotifyPropertyChanged
    {
        // 1. Read data from database - Last 5 members
        private int DataBaseCounter = 0;
        private int DataBaseReadCounter;
        private int LastFiveChanges = 0;
        //System.Timers.Timer Clock_ms;
        // Database string - Change if needed
        //static string MySQLconnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=mjernastanica;SslMode=none";
        // Select all data in database
        //private string query = "SELECT * FROM `stroj2` ORDER BY IDMjerenje DESC LIMIT 5";
        //private string queryCount = "SELECT COUNT(*) FROM `stroj2`";
        private int _rowNum;
        private string _columnName;
        private string _Poz1;
        private string _Poz2;
        // WORK ORDER DATA
        private string LastWorkOrder;
        private string BeforeLastWorkOrder;
        // C VALUE
        private float CPoz1Value;
        private float CPoz2Value;
        // A VALUE (TWO POINT)
        private float A12Poz1Value;
        private float A12Poz2Value;
        private float A11Poz1Value;
        private float A11Poz2Value;
        // A VALUE (ONE POINT)
        private float APoz1Value;
        private float APoz2Value;
        // B VALUE
        private float BPoz1Value;
        private float BPoz2Value;
        // F VALUE
        private float F1T2Poz1Value;
        private float F1T2Poz2Value;
        private float F1T3Poz1Value;
        private float F1T3Poz2Value;
        // E VALUE
        private float EPoz1Value;
        private float EPoz2Value;

        // Event Database change 
        public delegate void DatabaseChangeEventHandler(object source, EventArgs args);
        public event DatabaseChangeEventHandler DatabaseChanged;
        // Measurement value
        public event PropertyChangedEventHandler PropertyChanged;

        //DATABASE CHANGE SIGNAL
        private bool _databaseChange;
        public bool DatabaseChange
        {
            get { return _databaseChange; }
            set
            {
                if (_databaseChange != value)
                {
                    _databaseChange = value;
                    OnPropertyChanged("DatabaseChange");
                }
            }
        }
        // DATABASE ROW NUMBER
        private string _databeseRowNubmer;
        public string DatabaseRowNumber
        {
            get { return _databeseRowNubmer; }
            set
            {
                if (_databeseRowNubmer != value)
                {
                    _databeseRowNubmer = value;
                    OnPropertyChanged("DatabaseRowNumber");
                }
            }
        }
        // CONSECUTIVE WORK ORDERS
        private string _consecutiveOrders;
        public string ConsecutiveOrders
        {
            get { return _consecutiveOrders; }
            set
            {
                if (_consecutiveOrders != value)
                {
                    _consecutiveOrders = value;
                    OnPropertyChanged("ConsecutiveOrders");
                }
            }
        }

        //***********************************
        // C DIAMETER AVERAGE               *
        //***********************************

        #region C diameter average
        //MACHINE AVERAGE VALUES
        private float _CAverageValueMeas1;
        public float CAverageValueMeas1
        {
            get { return _CAverageValueMeas1; }
            set
            {
                if (_CAverageValueMeas1 != value)
                {
                    _CAverageValueMeas1 = value;
                    OnPropertyChanged("CAverageValueMeas1");
                }
            }
        }
        
        //MACHINE AVERAGE VALUES
        private float _CAverageValueMeas2;
        public float CAverageValueMeas2
        {
            get { return _CAverageValueMeas2; }
            set
            {
                if (_CAverageValueMeas2 != value)
                {
                    _CAverageValueMeas2 = value;
                    OnPropertyChanged("CAverageValueMeas2");
                }
            }
        }

        //MACHINE AVERAGE VALUES
        private float _CAverageValueMeas3;
        public float CAverageValueMeas3
        {
            get { return _CAverageValueMeas3; }
            set
            {
                if (_CAverageValueMeas3 != value)
                {
                    _CAverageValueMeas3 = value;
                    OnPropertyChanged("CAverageValueMeas3");
                }
            }
        }

        //MACHINE AVERAGE VALUES
        private float _CAverageValueMeas4;
        public float CAverageValueMeas4
        {
            get { return _CAverageValueMeas4; }
            set
            {
                if (_CAverageValueMeas4 != value)
                {
                    _CAverageValueMeas4 = value;
                    OnPropertyChanged("CAverageValueMeas4");
                }
            }
        }

        //MACHINE AVERAGE VALUES
        private float _CAverageValueMeas5;
        public float CAverageValueMeas5
        {
            get { return _CAverageValueMeas5; }
            set
            {
                if (_CAverageValueMeas5 != value)
                {
                    _CAverageValueMeas5 = value;
                    OnPropertyChanged("CAverageValueMeas5");
                }
            }
        }
        #endregion

        //***********************************
        // A Two POINT DIAMETER AVERAGE     *
        //***********************************

        #region A two point diameter average
        //MACHINE  AVERAGE VALUES
        private float _AtwoPointAverageValueMeas1;
        public float AtwoPointAverageValueMeas1
        {
            get { return _AtwoPointAverageValueMeas1; }
            set
            {
                if (_AtwoPointAverageValueMeas1 != value)
                {
                    _AtwoPointAverageValueMeas1 = value;
                    OnPropertyChanged("AtwoPointAverageValueMeas1");
                }
            }
        }

        //MACHINE  AVERAGE VALUES
        private float _AtwoPointAverageValueMeas2;
        public float AtwoPointAverageValueMeas2
        {
            get { return _AtwoPointAverageValueMeas2; }
            set
            {
                if (_AtwoPointAverageValueMeas2 != value)
                {
                    _AtwoPointAverageValueMeas2 = value;
                    OnPropertyChanged("AtwoPointAverageValueMeas2");
                }
            }
        }

        //MACHINE  AVERAGE VALUES
        private float _AtwoPointAverageValueMeas3;
        public float AtwoPointAverageValueMeas3
        {
            get { return _AtwoPointAverageValueMeas3; }
            set
            {
                if (_AtwoPointAverageValueMeas3 != value)
                {
                    _AtwoPointAverageValueMeas3 = value;
                    OnPropertyChanged("AtwoPointAverageValueMeas3");
                }
            }
        }

        //MACHINE  AVERAGE VALUES
        private float _AtwoPointAverageValueMeas4;
        public float AtwoPointAverageValueMeas4
        {
            get { return _AtwoPointAverageValueMeas4; }
            set
            {
                if (_AtwoPointAverageValueMeas4 != value)
                {
                    _AtwoPointAverageValueMeas4 = value;
                    OnPropertyChanged("AtwoPointAverageValueMeas4");
                }
            }
        }

        //MACHINE  AVERAGE VALUES
        private float _AtwoPointAverageValueMeas5;
        public float AtwoPointAverageValueMeas5
        {
            get { return _AtwoPointAverageValueMeas5; }
            set
            {
                if (_AtwoPointAverageValueMeas5 != value)
                {
                    _AtwoPointAverageValueMeas5 = value;
                    OnPropertyChanged("AtwoPointAverageValueMeas5");
                }
            }
        }
        #endregion

        //***********************************
        // A ONE POINT DIAMETER AVERAGE     *
        //***********************************

        #region A one point diameter average
        //MACHINE  AVERAGE VALUES
        private float _AonePointAverageValueMeas1;
        public float AonePointAverageValueMeas1
        {
            get { return _AonePointAverageValueMeas1; }
            set
            {
                if (_AonePointAverageValueMeas1 != value)
                {
                    _AonePointAverageValueMeas1 = value;
                    OnPropertyChanged("AonePointAverageValueMeas1");
                }
            }
        }

        //MACHINE  AVERAGE VALUES
        private float _AonePointAverageValueMeas2;
        public float AonePointAverageValueMeas2
        {
            get { return _AonePointAverageValueMeas2; }
            set
            {
                if (_AonePointAverageValueMeas2 != value)
                {
                    _AonePointAverageValueMeas2 = value;
                    OnPropertyChanged("AonePointAverageValueMeas2");
                }
            }
        }

        //MACHINE  AVERAGE VALUES
        private float _AonePointAverageValueMeas3;
        public float AonePointAverageValueMeas3
        {
            get { return _AonePointAverageValueMeas3; }
            set
            {
                if (_AonePointAverageValueMeas3 != value)
                {
                    _AonePointAverageValueMeas3 = value;
                    OnPropertyChanged("AonePointAverageValueMeas3");
                }
            }
        }

        //MACHINE  AVERAGE VALUES
        private float _AonePointAverageValueMeas4;
        public float AonePointAverageValueMeas4
        {
            get { return _AonePointAverageValueMeas4; }
            set
            {
                if (_AonePointAverageValueMeas4 != value)
                {
                    _AonePointAverageValueMeas4 = value;
                    OnPropertyChanged("AonePointAverageValueMeas4");
                }
            }
        }

        //MACHINE  AVERAGE VALUES
        private float _AonePointAverageValueMeas5;
        public float AonePointAverageValueMeas5
        {
            get { return _AonePointAverageValueMeas5; }
            set
            {
                if (_AonePointAverageValueMeas5 != value)
                {
                    _AonePointAverageValueMeas5 = value;
                    OnPropertyChanged("AonePointAverageValueMeas5");
                }
            }
        }
        #endregion

        //***********************************
        // B  DIAMETER AVERAGE              *
        //***********************************
        #region B diameter average
        //MACHINE  AVERAGE VALUES
        private float _BAverageValueMeas1;
        public float BAverageValueMeas1
        {
            get { return _BAverageValueMeas1; }
            set
            {
                if (_BAverageValueMeas1 != value)
                {
                    _BAverageValueMeas1 = value;
                    OnPropertyChanged("BAverageValueMeas1");
                }
            }
        }

        //MACHINE  AVERAGE VALUES
        private float _BAverageValueMeas2;
        public float BAverageValueMeas2
        {
            get { return _BAverageValueMeas2; }
            set
            {
                if (_BAverageValueMeas2 != value)
                {
                    _BAverageValueMeas2 = value;
                    OnPropertyChanged("BAverageValueMeas2");
                }
            }
        }

        //MACHINE  AVERAGE VALUES
        private float _BAverageValueMeas3;
        public float BAverageValueMeas3
        {
            get { return _BAverageValueMeas3; }
            set
            {
                if (_BAverageValueMeas3 != value)
                {
                    _BAverageValueMeas3 = value;
                    OnPropertyChanged("BAverageValueMeas3");
                }
            }
        }

        //MACHINE  AVERAGE VALUES
        private float _BAverageValueMeas4;
        public float BAverageValueMeas4
        {
            get { return _BAverageValueMeas4; }
            set
            {
                if (_BAverageValueMeas4 != value)
                {
                    _BAverageValueMeas4 = value;
                    OnPropertyChanged("BAverageValueMeas4");
                }
            }
        }

        //MACHINE  AVERAGE VALUES
        private float _BAverageValueMeas5;
        public float BAverageValueMeas5
        {
            get { return _BAverageValueMeas5; }
            set
            {
                if (_BAverageValueMeas5 != value)
                {
                    _BAverageValueMeas5 = value;
                    OnPropertyChanged("BAverageValueMeas5");
                }
            }
        }
        #endregion

        //***********************************
        // F  GUTTER AVERAGE                *
        //***********************************
        #region F gutter average
        //MACHINE  AVERAGE VALUES
        private float _FAverageValueMeas1;
        public float FAverageValueMeas1
        {
            get { return _FAverageValueMeas1; }
            set
            {
                if (_FAverageValueMeas1 != value)
                {
                    _FAverageValueMeas1 = value;
                    OnPropertyChanged("FAverageValueMeas1");
                }
            }
        }

        //MACHINE  AVERAGE VALUES
        private float _FAverageValueMeas2;
        public float FAverageValueMeas2
        {
            get { return _FAverageValueMeas2; }
            set
            {
                if (_FAverageValueMeas2 != value)
                {
                    _FAverageValueMeas2 = value;
                    OnPropertyChanged("FAverageValueMeas2");
                }
            }
        }

        //MACHINE  AVERAGE VALUES
        private float _FAverageValueMeas3;
        public float FAverageValueMeas3
        {
            get { return _FAverageValueMeas3; }
            set
            {
                if (_FAverageValueMeas3 != value)
                {
                    _FAverageValueMeas3 = value;
                    OnPropertyChanged("FAverageValueMeas3");
                }
            }
        }

        //MACHINE  AVERAGE VALUES
        private float _FAverageValueMeas4;
        public float FAverageValueMeas4
        {
            get { return _FAverageValueMeas4; }
            set
            {
                if (_FAverageValueMeas4 != value)
                {
                    _FAverageValueMeas4 = value;
                    OnPropertyChanged("FAverageValueMeas4");
                }
            }
        }

        //MACHINE  AVERAGE VALUES
        private float _FAverageValueMeas5;
        public float FAverageValueMeas5
        {
            get { return _FAverageValueMeas5; }
            set
            {
                if (_FAverageValueMeas5 != value)
                {
                    _FAverageValueMeas5 = value;
                    OnPropertyChanged("FAverageValueMeas5");
                }
            }
        }
        #endregion

        //***********************************
        // E  GUTTER AVERAGE                *
        //***********************************
        #region E height average
        //MACHINE  AVERAGE VALUES
        private float _EAverageValueMeas1;
        public float EAverageValueMeas1
        {
            get { return _EAverageValueMeas1; }
            set
            {
                if (_EAverageValueMeas1 != value)
                {
                    _EAverageValueMeas1 = value;
                    OnPropertyChanged("EAverageValueMeas1");
                }
            }
        }

        //MACHINE  AVERAGE VALUES
        private float _EAverageValueMeas2;
        public float EAverageValueMeas2
        {
            get { return _EAverageValueMeas2; }
            set
            {
                if (_EAverageValueMeas2 != value)
                {
                    _EAverageValueMeas2 = value;
                    OnPropertyChanged("EAverageValueMeas2");
                }
            }
        }

        //MACHINE  AVERAGE VALUES
        private float _EAverageValueMeas3;
        public float EAverageValueMeas3
        {
            get { return _EAverageValueMeas3; }
            set
            {
                if (_EAverageValueMeas3 != value)
                {
                    _EAverageValueMeas3 = value;
                    OnPropertyChanged("EAverageValueMeas3");
                }
            }
        }

        //MACHINE  AVERAGE VALUES
        private float _EAverageValueMeas4;
        public float EAverageValueMeas4
        {
            get { return _EAverageValueMeas4; }
            set
            {
                if (_EAverageValueMeas4 != value)
                {
                    _EAverageValueMeas4 = value;
                    OnPropertyChanged("EAverageValueMeas4");
                }
            }
        }

        //MACHINE  AVERAGE VALUES
        private float _EAverageValueMeas5;
        public float EAverageValueMeas5
        {
            get { return _EAverageValueMeas5; }
            set
            {
                if (_EAverageValueMeas5 != value)
                {
                    _EAverageValueMeas5 = value;
                    OnPropertyChanged("EAverageValueMeas5");
                }
            }
        }
        #endregion


        // Property changed implementation
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Check database for new entry
        public void CompareWorkOrder(string mySQLconnectionString, string tableName)
        {
            string query = $"SELECT * FROM {tableName} ORDER BY IDMjerenje DESC LIMIT 2";
            MySqlConnection databaseConnection = new MySqlConnection(mySQLconnectionString);
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, mySQLconnectionString);
            // Refresh dataset every time we access DB (Update new data)
            DataSet ds2ResultsSet = new DataSet();
            DataTable dt2ResultsTable = new DataTable();

            try
            {
                // Open com with database
                databaseConnection.Open();
                adapter.Fill(ds2ResultsSet, tableName);
                dt2ResultsTable = ds2ResultsSet.Tables[tableName];

                //***********************************
                // Grab workpiece order data        *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 0; // row number
                _columnName = "RadniNalog"; // database table column name
                LastWorkOrder = dt2ResultsTable.Rows[_rowNum][_columnName].ToString();

                // POZ 2 VALUE
                _rowNum = 1; // row number
                _columnName = "RadniNalog"; // database table column name
                BeforeLastWorkOrder = dt2ResultsTable.Rows[_rowNum][_columnName].ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("Submit error" + e.Message);
            }
            finally
            {
                if (databaseConnection != null)
                {
                    databaseConnection.Close();
                    Console.WriteLine($"Veza sa bazom podataka {tableName} je zatvorena!");
                }
            }
        }

        // Check database for new entry
        public void DatabaseCount(string mySQLconnectionString, string tableName)
        {
            string queryCount = $"SELECT COUNT(*) FROM {tableName}";
            MySqlConnection databaseConnection = new MySqlConnection(mySQLconnectionString);
            MySqlCommand commandDatabase = new MySqlCommand(queryCount, databaseConnection);

            // Good practice add query timeout 30 sec
            commandDatabase.CommandTimeout = 30;

            try
            {
                // APP START CONDITION
                if (DataBaseCounter == 0)
                {
                    // Open com with database
                    databaseConnection.Open();
                    Console.WriteLine($"Veza sa bazom {tableName} ostvarena");
                    //MySqlDataReader myReader = commandDatabase.ExecuteReader();
                    DataBaseReadCounter = int.Parse(commandDatabase.ExecuteScalar().ToString());
                    DataBaseCounter = DataBaseReadCounter;
                    DatabaseRowNumber = DataBaseReadCounter.ToString();
                }

                else
                {
                    // Open com with database
                    databaseConnection.Open();
                    Console.WriteLine($"Veza sa bazom {tableName} ostvarena");
                    //MySqlDataReader myReader = commandDatabase.ExecuteReader();
                    DataBaseReadCounter = int.Parse(commandDatabase.ExecuteScalar().ToString());
                    DatabaseRowNumber = DataBaseReadCounter.ToString();
                }

                if (DataBaseCounter != DataBaseReadCounter && LastWorkOrder != BeforeLastWorkOrder)
                {
                    Console.WriteLine($"U bazu {tableName} dodana nova vrijednost razlicitog naloga");
                    // We are tracking last five changes from same work order
                    LastFiveChanges = 0;
                    ConsecutiveOrders = LastFiveChanges.ToString();
                    DataBaseCounter = DataBaseReadCounter;
                }

                // DETECT DATABSE CHANGE -> NEW RESULT AND SAME WORK ORDER 
                else if (DataBaseCounter != DataBaseReadCounter && LastWorkOrder == BeforeLastWorkOrder)
                {
                    Console.WriteLine($"U bazu {tableName} dodana nova vrijednost istog naloga");
                    LastFiveChanges++;
                    ConsecutiveOrders = LastFiveChanges.ToString();
                    DataBaseCounter = DataBaseReadCounter;
                }
                else
                {
                    Console.WriteLine($"Vrijednosti u bazi {tableName} su iste");
                }

                // IF WE HAVE FIVE RESULTS FROM SAME WP LOAD DATA
                if (LastFiveChanges == 5)
                {
                    MeasurementDataLoaded(mySQLconnectionString, tableName);
                    OnDatabaseChange();
                    DatabaseChange = true;
                    LastFiveChanges = 0;
                    ConsecutiveOrders = LastFiveChanges.ToString();
                }

            }
            catch (Exception b)
            {
                Console.WriteLine("Submit error" + b.Message);
            }
            finally
            {
                if (databaseConnection != null)
                {
                    databaseConnection.Close();
                    Console.WriteLine($"Veza sa bazom podataka {tableName} je zatvorena!");
                }
            }
        }

        // Fill table on page loaded event
        public void MeasurementDataLoaded(string mySQLconnectionString, string tableName)
        {
            string query = $"SELECT * FROM {tableName} ORDER BY IDMjerenje DESC LIMIT 5";
            MySqlConnection databaseConnection = new MySqlConnection(mySQLconnectionString);
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, mySQLconnectionString);
            // Refresh dataset every time we access DB (Update new data)
            DataSet ds5ResultsSet = new DataSet();
            DataTable dt5ResultsTable = new DataTable();

            try
            {
                // Open com with database
                databaseConnection.Open();

                // Fill database with data
                adapter.Fill(ds5ResultsSet, tableName);
                dt5ResultsTable = ds5ResultsSet.Tables[tableName];

                Console.WriteLine($"Rezultati iz {tableName} su ucitani u aplikaciju");

                // To read a specific cell in a row:

                // C MEASSUREMENT
                #region C calculation
                //***********************************
                // MEASUREMENT NO.1                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 0; // row number
                _columnName = "CPoz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                CPoz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 0; // row number
                _columnName = "CPoz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                CPoz2Value = float.Parse(_Poz2);

                CAverageValueMeas1 = (CPoz1Value + CPoz2Value) / 2;

                //***********************************
                // MEASUREMENT NO.2                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 1; // row number
                _columnName = "CPoz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                CPoz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 1; // row number
                _columnName = "CPoz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                CPoz2Value = float.Parse(_Poz2);

                CAverageValueMeas2 = (CPoz1Value + CPoz2Value) / 2;

                //***********************************
                // MEASUREMENT NO.3                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 2; // row number
                _columnName = "CPoz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                CPoz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 2; // row number
                _columnName = "CPoz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                CPoz2Value = float.Parse(_Poz2);

                CAverageValueMeas3 = (CPoz1Value + CPoz2Value) / 2;

                //***********************************
                // MEASUREMENT NO.4                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 3; // row number
                _columnName = "CPoz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                CPoz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 3; // row number
                _columnName = "CPoz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                CPoz2Value = float.Parse(_Poz2);

                CAverageValueMeas4 = (CPoz1Value + CPoz2Value) / 2;

                //***********************************
                // MEASUREMENT NO.5                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 4; // row number
                _columnName = "CPoz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                CPoz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 4; // row number
                _columnName = "CPoz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                CPoz2Value = float.Parse(_Poz2);

                CAverageValueMeas5 = (CPoz1Value + CPoz2Value) / 2;
                #endregion

                // A MEASSUREMENT (TWO POINT MEASUREMENT)
                #region A two point calculation
                //***********************************
                // MEASUREMENT NO.1                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 0; // row number
                _columnName = "A12Poz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                A12Poz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 0; // row number
                _columnName = "A12Poz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                A12Poz2Value = float.Parse(_Poz2);

                // POZ 1 VALUE
                _rowNum = 0; // row number
                _columnName = "A11Poz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                A11Poz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 0; // row number
                _columnName = "A11Poz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                A11Poz2Value = float.Parse(_Poz2);

                AtwoPointAverageValueMeas1 = (A12Poz1Value + A12Poz2Value + A11Poz1Value + A11Poz2Value) / 4;

                //***********************************
                // MEASUREMENT NO.2                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 1; // row number
                _columnName = "A12Poz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                A12Poz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 1; // row number
                _columnName = "A12Poz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                A12Poz2Value = float.Parse(_Poz2);

                // POZ 1 VALUE
                _rowNum = 1; // row number
                _columnName = "A11Poz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                A11Poz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 1; // row number
                _columnName = "A11Poz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                A11Poz2Value = float.Parse(_Poz2);

                AtwoPointAverageValueMeas2 = (A12Poz1Value + A12Poz2Value + A11Poz1Value + A11Poz2Value) / 4;

                //***********************************
                // MEASUREMENT NO.3                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 2; // row number
                _columnName = "A12Poz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                A12Poz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 2; // row number
                _columnName = "A12Poz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                A12Poz2Value = float.Parse(_Poz2);

                // POZ 1 VALUE
                _rowNum = 2; // row number
                _columnName = "A11Poz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                A11Poz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 2; // row number
                _columnName = "A11Poz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                A11Poz2Value = float.Parse(_Poz2);

                AtwoPointAverageValueMeas3 = (A12Poz1Value + A12Poz2Value + A11Poz1Value + A11Poz2Value) / 4;

                //***********************************
                // MEASUREMENT NO.4                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 3; // row number
                _columnName = "A12Poz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                A12Poz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 3; // row number
                _columnName = "A12Poz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                A12Poz2Value = float.Parse(_Poz2);

                // POZ 1 VALUE
                _rowNum = 3; // row number
                _columnName = "A11Poz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                A11Poz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 3; // row number
                _columnName = "A11Poz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                A11Poz2Value = float.Parse(_Poz2);

                AtwoPointAverageValueMeas4 = (A12Poz1Value + A12Poz2Value + A11Poz1Value + A11Poz2Value) / 4;

                //***********************************
                // MEASUREMENT NO.5                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 4; // row number
                _columnName = "A12Poz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                A12Poz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 4; // row number
                _columnName = "A12Poz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                A12Poz2Value = float.Parse(_Poz2);

                // POZ 1 VALUE
                _rowNum = 4; // row number
                _columnName = "A11Poz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                A11Poz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 4; // row number
                _columnName = "A11Poz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                A11Poz2Value = float.Parse(_Poz2);

                AtwoPointAverageValueMeas5 = (A12Poz1Value + A12Poz2Value + A11Poz1Value + A11Poz2Value) / 4;
                #endregion

                // A MEASSUREMENT (ONE POINT MEASUREMENT)
                #region A one point calculation
                //***********************************
                // MEASUREMENT NO.1                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 0; // row number
                _columnName = "APoz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                APoz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 0; // row number
                _columnName = "APoz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                APoz2Value = float.Parse(_Poz2);

                AonePointAverageValueMeas1 = (APoz1Value + APoz2Value) / 2;

                //***********************************
                // MEASUREMENT NO.2                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 1; // row number
                _columnName = "APoz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                APoz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 1; // row number
                _columnName = "APoz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                APoz2Value = float.Parse(_Poz2);

                AonePointAverageValueMeas2 = (APoz1Value + APoz2Value) / 2;

                //***********************************
                // MEASUREMENT NO.3                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 2; // row number
                _columnName = "APoz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                APoz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 2; // row number
                _columnName = "APoz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                APoz2Value = float.Parse(_Poz2);

                AonePointAverageValueMeas3 = (APoz1Value + APoz2Value) / 2;

                //***********************************
                // MEASUREMENT NO.4                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 3; // row number
                _columnName = "APoz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                APoz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 3; // row number
                _columnName = "APoz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                APoz2Value = float.Parse(_Poz2);

                AonePointAverageValueMeas4 = (APoz1Value + APoz2Value) / 2;

                //***********************************
                // MEASUREMENT NO.5                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 4; // row number
                _columnName = "APoz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                APoz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 4; // row number
                _columnName = "APoz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                APoz2Value = float.Parse(_Poz2);

                AonePointAverageValueMeas5 = (APoz1Value + APoz2Value) / 2;
                #endregion

                // B MEASUREMENT
                #region B calculation
                //***********************************
                // MEASUREMENT NO.1                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 0; // row number
                _columnName = "BPoz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                BPoz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 0; // row number
                _columnName = "BPoz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                BPoz2Value = float.Parse(_Poz2);

                BAverageValueMeas1 = (BPoz1Value + BPoz2Value) / 2;

                //***********************************
                // MEASUREMENT NO.2                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 1; // row number
                _columnName = "BPoz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                BPoz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 1; // row number
                _columnName = "BPoz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                BPoz2Value = float.Parse(_Poz2);

                BAverageValueMeas2 = (BPoz1Value + BPoz2Value) / 2;

                //***********************************
                // MEASUREMENT NO.3                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 2; // row number
                _columnName = "BPoz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                BPoz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 2; // row number
                _columnName = "BPoz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                BPoz2Value = float.Parse(_Poz2);

                BAverageValueMeas3 = (BPoz1Value + BPoz2Value) / 2;

                //***********************************
                // MEASUREMENT NO.4                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 3; // row number
                _columnName = "BPoz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                BPoz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 3; // row number
                _columnName = "BPoz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                BPoz2Value = float.Parse(_Poz2);

                BAverageValueMeas4 = (BPoz1Value + BPoz2Value) / 2;

                //***********************************
                // MEASUREMENT NO.5                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 4; // row number
                _columnName = "BPoz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                BPoz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 4; // row number
                _columnName = "BPoz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                BPoz2Value = float.Parse(_Poz2);

                BAverageValueMeas4 = (BPoz1Value + BPoz2Value) / 2;
                #endregion

                // F MEASUREMENT
                #region F two point calculation
                //***********************************
                // MEASUREMENT NO.1                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 0; // row number
                _columnName = "F1Ticalo2Poz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                F1T2Poz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 0; // row number
                _columnName = "F1Ticalo3Poz1";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                F1T3Poz1Value = float.Parse(_Poz2);

                // POZ 1 VALUE
                _rowNum = 0; // row number
                _columnName = "F1Ticalo2Poz2";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                F1T2Poz2Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 0; // row number
                _columnName = "F1Ticalo3Poz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                F1T3Poz2Value = float.Parse(_Poz2);

                FAverageValueMeas1 = (F1T2Poz1Value + F1T3Poz1Value + F1T2Poz2Value + F1T3Poz2Value) / 4;

                //***********************************
                // MEASUREMENT NO.2                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 1; // row number
                _columnName = "F1Ticalo2Poz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                F1T2Poz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 1; // row number
                _columnName = "F1Ticalo3Poz1";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                F1T3Poz1Value = float.Parse(_Poz2);

                // POZ 1 VALUE
                _rowNum = 1; // row number
                _columnName = "F1Ticalo2Poz2";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                F1T2Poz2Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 1; // row number
                _columnName = "F1Ticalo3Poz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                F1T3Poz2Value = float.Parse(_Poz2);

                FAverageValueMeas2 = (F1T2Poz1Value + F1T3Poz1Value + F1T2Poz2Value + F1T3Poz2Value) / 4;

                //***********************************
                // MEASUREMENT NO.3                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 2; // row number
                _columnName = "F1Ticalo2Poz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                F1T2Poz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 2; // row number
                _columnName = "F1Ticalo3Poz1";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                F1T3Poz1Value = float.Parse(_Poz2);

                // POZ 1 VALUE
                _rowNum = 2; // row number
                _columnName = "F1Ticalo2Poz2";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                F1T2Poz2Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 2; // row number
                _columnName = "F1Ticalo3Poz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                F1T3Poz2Value = float.Parse(_Poz2);

                FAverageValueMeas3 = (F1T2Poz1Value + F1T3Poz1Value + F1T2Poz2Value + F1T3Poz2Value) / 4;

                //***********************************
                // MEASUREMENT NO.4                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 3; // row number
                _columnName = "F1Ticalo2Poz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                F1T2Poz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 3; // row number
                _columnName = "F1Ticalo3Poz1";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                F1T3Poz1Value = float.Parse(_Poz2);

                // POZ 1 VALUE
                _rowNum = 3; // row number
                _columnName = "F1Ticalo2Poz2";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                F1T2Poz2Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 3; // row number
                _columnName = "F1Ticalo3Poz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                F1T3Poz2Value = float.Parse(_Poz2);

                FAverageValueMeas4 = (F1T2Poz1Value + F1T3Poz1Value + F1T2Poz2Value + F1T3Poz2Value) / 4;

                //***********************************
                // MEASUREMENT NO.5                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 4; // row number
                _columnName = "F1Ticalo2Poz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                F1T2Poz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 4; // row number
                _columnName = "F1Ticalo3Poz1";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                F1T3Poz1Value = float.Parse(_Poz2);

                // POZ 1 VALUE
                _rowNum = 4; // row number
                _columnName = "F1Ticalo2Poz2";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                F1T2Poz2Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 4; // row number
                _columnName = "F1Ticalo3Poz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                F1T3Poz2Value = float.Parse(_Poz2);

                FAverageValueMeas5 = (F1T2Poz1Value + F1T3Poz1Value + F1T2Poz2Value + F1T3Poz2Value) / 4;
                #endregion

                // E MEASUREMENT
                #region E calculation
                //***********************************
                // MEASUREMENT NO.1                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 0; // row number
                _columnName = "EPoz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                EPoz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 0; // row number
                _columnName = "EPoz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                EPoz2Value = float.Parse(_Poz2);

                EAverageValueMeas1 = (EPoz1Value + EPoz2Value) / 2;

                //***********************************
                // MEASUREMENT NO.2                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 1; // row number
                _columnName = "EPoz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                EPoz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 1; // row number
                _columnName = "EPoz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                EPoz2Value = float.Parse(_Poz2);

                EAverageValueMeas2 = (EPoz1Value + EPoz2Value) / 2;

                //***********************************
                // MEASUREMENT NO.3                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 2; // row number
                _columnName = "EPoz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                EPoz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 2; // row number
                _columnName = "EPoz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                EPoz2Value = float.Parse(_Poz2);

                EAverageValueMeas3 = (BPoz1Value + BPoz2Value) / 2;

                //***********************************
                // MEASUREMENT NO.4                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 3; // row number
                _columnName = "EPoz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                EPoz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 3; // row number
                _columnName = "EPoz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                EPoz2Value = float.Parse(_Poz2);

                EAverageValueMeas4 = (BPoz1Value + BPoz2Value) / 2;

                //***********************************
                // MEASUREMENT NO.5                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 4; // row number
                _columnName = "BPoz1";  // database table column name
                _Poz1 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                EPoz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 4; // row number
                _columnName = "BPoz2";  // database table column name
                _Poz2 = dt5ResultsTable.Rows[_rowNum][_columnName].ToString();
                EPoz2Value = float.Parse(_Poz2);

                EAverageValueMeas4 = (BPoz1Value + BPoz2Value) / 2;
                #endregion

                // CHECK DATA IN COLUMN
                //foreach (DataRow dr in dt5ResultsTable.Rows)
                //{
                //    MessageBox.Show(dr["CPoz1"].ToString());
                //}


            }
            catch (Exception b)
            {
                Console.WriteLine("Submit error" + b.Message);
            }
            finally
            {
                if (databaseConnection != null)
                {
                    databaseConnection.Close();
                    Console.WriteLine($"Veza sa bazom podataka {tableName} je zatvorena!");
                }
            }
        }

        protected virtual void OnDatabaseChange()
        {
            if (DatabaseChanged != null)
                DatabaseChanged(this, EventArgs.Empty);
        }

        // 2. Check do they have same name (Same work order)

        // 3. Check is input OK (Not 0)

        // 4. Do the average calculation

        // 5. Compare to target value

        // 6. Calculate tool offset

    }
}
