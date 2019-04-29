using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        static string MySQLconnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=mjernastanica;SslMode=none";
        // Select all data in database
        private string query = "SELECT * FROM `stroj2` ORDER BY IDMjerenje DESC LIMIT 5";
        private string queryCount = "SELECT COUNT(*) FROM `stroj2`";
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        private int _rowNum;
        private string _columnName;
        private string _Poz1;
        private string _Poz2;
        private float CPoz1Value;
        private float CPoz2Value;
        private float A12Poz1Value;
        private float A12Poz2Value;
        private float A11Poz1Value;
        private float A11Poz2Value;
        private float APoz1Value;
        private float APoz2Value;
        private float BPoz1Value;
        private float BPoz2Value;
        // F VALUE
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

        //***********************************
        // F  GUTTER AVERAGE                *
        //***********************************

        //***********************************
        // E  GUTTER AVERAGE                *
        //***********************************

        // Property changed implementation
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Scan database for new entry
        public void DatabaseCount()
        {
            MySqlConnection databaseConnection = new MySqlConnection(MySQLconnectionString);
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
                    Console.WriteLine("Veza sa bazom ostvarena");
                    //MySqlDataReader myReader = commandDatabase.ExecuteReader();
                    DataBaseReadCounter = int.Parse(commandDatabase.ExecuteScalar().ToString());
                    DataBaseCounter = DataBaseReadCounter;
                    
                }

                else
                {
                    // Open com with database
                    databaseConnection.Open();
                    Console.WriteLine("Veza sa bazom ostvarena");
                    //MySqlDataReader myReader = commandDatabase.ExecuteReader();
                    DataBaseReadCounter = int.Parse(commandDatabase.ExecuteScalar().ToString());
                }

                // DETECT DATABSE CHANGE
                if (DataBaseCounter != DataBaseReadCounter)
                {
                    Console.WriteLine("Vrijednosti u bazi su izmjenjene");
                    LastFiveChanges++;
                    DataBaseCounter = DataBaseReadCounter;
                }
                else
                {
                    Console.WriteLine("Vrijednosti u bazi su iste");
                }

                // IF WE HAVE FIVE RESULTS FROM SAME WP LOAD DATA
                if (LastFiveChanges == 5)
                {
                    MeasurementDataLoaded();
                    OnDatabaseChange();
                    DatabaseChange = true;
                    LastFiveChanges = 0;
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
                    Console.WriteLine("Veza sa bazom podataka je zatvorena!");
                }
            }
        }

        // Fill table on page loaded event
        public void MeasurementDataLoaded()
        {
            MySqlConnection databaseConnection = new MySqlConnection(MySQLconnectionString);
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, MySQLconnectionString);

            try
            {
                // Open com with database
                databaseConnection.Open();

                // Fill database with data
                adapter.Fill(ds, "stroj1");
                dt = ds.Tables["stroj1"];

                Console.WriteLine("Baza je učitana na stranicu!");

                // To read a specific cell in a row:

                // C MEASSUREMENT

                //***********************************
                // MEASUREMENT NO.1                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 0; // row number
                _columnName = "CPoz1";  // database table column name
                _Poz1 = dt.Rows[_rowNum][_columnName].ToString();
                CPoz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 0; // row number
                _columnName = "CPoz2";  // database table column name
                _Poz2 = dt.Rows[_rowNum][_columnName].ToString();
                CPoz2Value = float.Parse(_Poz2);

                CAverageValueMeas1 = (CPoz1Value + CPoz2Value) / 2;

                //***********************************
                // MEASUREMENT NO.2                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 1; // row number
                _columnName = "CPoz1";  // database table column name
                _Poz1 = dt.Rows[_rowNum][_columnName].ToString();
                CPoz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 1; // row number
                _columnName = "CPoz2";  // database table column name
                _Poz2 = dt.Rows[_rowNum][_columnName].ToString();
                CPoz2Value = float.Parse(_Poz2);

                CAverageValueMeas2 = (CPoz1Value + CPoz2Value) / 2;

                //***********************************
                // MEASUREMENT NO.3                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 2; // row number
                _columnName = "CPoz1";  // database table column name
                _Poz1 = dt.Rows[_rowNum][_columnName].ToString();
                CPoz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 2; // row number
                _columnName = "CPoz2";  // database table column name
                _Poz2 = dt.Rows[_rowNum][_columnName].ToString();
                CPoz2Value = float.Parse(_Poz2);

                CAverageValueMeas3 = (CPoz1Value + CPoz2Value) / 2;

                //***********************************
                // MEASUREMENT NO.4                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 3; // row number
                _columnName = "CPoz1";  // database table column name
                _Poz1 = dt.Rows[_rowNum][_columnName].ToString();
                CPoz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 3; // row number
                _columnName = "CPoz2";  // database table column name
                _Poz2 = dt.Rows[_rowNum][_columnName].ToString();
                CPoz2Value = float.Parse(_Poz2);

                CAverageValueMeas4 = (CPoz1Value + CPoz2Value) / 2;

                //***********************************
                // MEASUREMENT NO.5                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 4; // row number
                _columnName = "CPoz1";  // database table column name
                _Poz1 = dt.Rows[_rowNum][_columnName].ToString();
                CPoz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 4; // row number
                _columnName = "CPoz2";  // database table column name
                _Poz2 = dt.Rows[_rowNum][_columnName].ToString();
                CPoz2Value = float.Parse(_Poz2);

                CAverageValueMeas5 = (CPoz1Value + CPoz2Value) / 2;

                // A MEASSUREMENT (TWO POINT MEASUREMENT)

                //***********************************
                // MEASUREMENT NO.1                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 0; // row number
                _columnName = "A12Poz1";  // database table column name
                _Poz1 = dt.Rows[_rowNum][_columnName].ToString();
                A12Poz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 0; // row number
                _columnName = "A12Poz2";  // database table column name
                _Poz2 = dt.Rows[_rowNum][_columnName].ToString();
                A12Poz2Value = float.Parse(_Poz2);

                // POZ 1 VALUE
                _rowNum = 0; // row number
                _columnName = "A11Poz1";  // database table column name
                _Poz1 = dt.Rows[_rowNum][_columnName].ToString();
                A11Poz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 0; // row number
                _columnName = "A11Poz2";  // database table column name
                _Poz2 = dt.Rows[_rowNum][_columnName].ToString();
                A11Poz2Value = float.Parse(_Poz2);

                AtwoPointAverageValueMeas1 = (A12Poz1Value + A12Poz2Value + A11Poz1Value + A11Poz2Value) / 4;

                //***********************************
                // MEASUREMENT NO.2                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 1; // row number
                _columnName = "A12Poz1";  // database table column name
                _Poz1 = dt.Rows[_rowNum][_columnName].ToString();
                A12Poz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 1; // row number
                _columnName = "A12Poz2";  // database table column name
                _Poz2 = dt.Rows[_rowNum][_columnName].ToString();
                A12Poz2Value = float.Parse(_Poz2);

                // POZ 1 VALUE
                _rowNum = 1; // row number
                _columnName = "A11Poz1";  // database table column name
                _Poz1 = dt.Rows[_rowNum][_columnName].ToString();
                A11Poz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 1; // row number
                _columnName = "A11Poz2";  // database table column name
                _Poz2 = dt.Rows[_rowNum][_columnName].ToString();
                A11Poz2Value = float.Parse(_Poz2);

                AtwoPointAverageValueMeas2 = (A12Poz1Value + A12Poz2Value + A11Poz1Value + A11Poz2Value) / 4;

                //***********************************
                // MEASUREMENT NO.3                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 2; // row number
                _columnName = "A12Poz1";  // database table column name
                _Poz1 = dt.Rows[_rowNum][_columnName].ToString();
                A12Poz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 2; // row number
                _columnName = "A12Poz2";  // database table column name
                _Poz2 = dt.Rows[_rowNum][_columnName].ToString();
                A12Poz2Value = float.Parse(_Poz2);

                // POZ 1 VALUE
                _rowNum = 2; // row number
                _columnName = "A11Poz1";  // database table column name
                _Poz1 = dt.Rows[_rowNum][_columnName].ToString();
                A11Poz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 2; // row number
                _columnName = "A11Poz2";  // database table column name
                _Poz2 = dt.Rows[_rowNum][_columnName].ToString();
                A11Poz2Value = float.Parse(_Poz2);

                AtwoPointAverageValueMeas3 = (A12Poz1Value + A12Poz2Value + A11Poz1Value + A11Poz2Value) / 4;

                //***********************************
                // MEASUREMENT NO.4                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 3; // row number
                _columnName = "A12Poz1";  // database table column name
                _Poz1 = dt.Rows[_rowNum][_columnName].ToString();
                A12Poz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 3; // row number
                _columnName = "A12Poz2";  // database table column name
                _Poz2 = dt.Rows[_rowNum][_columnName].ToString();
                A12Poz2Value = float.Parse(_Poz2);

                // POZ 1 VALUE
                _rowNum = 3; // row number
                _columnName = "A11Poz1";  // database table column name
                _Poz1 = dt.Rows[_rowNum][_columnName].ToString();
                A11Poz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 3; // row number
                _columnName = "A11Poz2";  // database table column name
                _Poz2 = dt.Rows[_rowNum][_columnName].ToString();
                A11Poz2Value = float.Parse(_Poz2);

                AtwoPointAverageValueMeas4 = (A12Poz1Value + A12Poz2Value + A11Poz1Value + A11Poz2Value) / 4;

                //***********************************
                // MEASUREMENT NO.5                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 4; // row number
                _columnName = "A12Poz1";  // database table column name
                _Poz1 = dt.Rows[_rowNum][_columnName].ToString();
                A12Poz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 4; // row number
                _columnName = "A12Poz2";  // database table column name
                _Poz2 = dt.Rows[_rowNum][_columnName].ToString();
                A12Poz2Value = float.Parse(_Poz2);

                // POZ 1 VALUE
                _rowNum = 4; // row number
                _columnName = "A11Poz1";  // database table column name
                _Poz1 = dt.Rows[_rowNum][_columnName].ToString();
                A11Poz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 4; // row number
                _columnName = "A11Poz2";  // database table column name
                _Poz2 = dt.Rows[_rowNum][_columnName].ToString();
                A11Poz2Value = float.Parse(_Poz2);

                AtwoPointAverageValueMeas5 = (A12Poz1Value + A12Poz2Value + A11Poz1Value + A11Poz2Value) / 4;

                // A MEASSUREMENT (ONE POINT MEASUREMENT)

                //***********************************
                // MEASUREMENT NO.1                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 0; // row number
                _columnName = "APoz1";  // database table column name
                _Poz1 = dt.Rows[_rowNum][_columnName].ToString();
                APoz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 0; // row number
                _columnName = "APoz2";  // database table column name
                _Poz2 = dt.Rows[_rowNum][_columnName].ToString();
                APoz2Value = float.Parse(_Poz2);

                AonePointAverageValueMeas1 = (APoz1Value + APoz2Value) / 2;

                //***********************************
                // MEASUREMENT NO.2                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 1; // row number
                _columnName = "APoz1";  // database table column name
                _Poz1 = dt.Rows[_rowNum][_columnName].ToString();
                APoz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 1; // row number
                _columnName = "APoz2";  // database table column name
                _Poz2 = dt.Rows[_rowNum][_columnName].ToString();
                APoz2Value = float.Parse(_Poz2);

                AonePointAverageValueMeas2 = (APoz1Value + APoz2Value) / 2;

                //***********************************
                // MEASUREMENT NO.3                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 2; // row number
                _columnName = "APoz1";  // database table column name
                _Poz1 = dt.Rows[_rowNum][_columnName].ToString();
                APoz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 2; // row number
                _columnName = "APoz2";  // database table column name
                _Poz2 = dt.Rows[_rowNum][_columnName].ToString();
                APoz2Value = float.Parse(_Poz2);

                AonePointAverageValueMeas3 = (APoz1Value + APoz2Value) / 2;

                //***********************************
                // MEASUREMENT NO.4                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 3; // row number
                _columnName = "APoz1";  // database table column name
                _Poz1 = dt.Rows[_rowNum][_columnName].ToString();
                APoz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 3; // row number
                _columnName = "APoz2";  // database table column name
                _Poz2 = dt.Rows[_rowNum][_columnName].ToString();
                APoz2Value = float.Parse(_Poz2);

                AonePointAverageValueMeas4 = (APoz1Value + APoz2Value) / 2;

                //***********************************
                // MEASUREMENT NO.5                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 4; // row number
                _columnName = "APoz1";  // database table column name
                _Poz1 = dt.Rows[_rowNum][_columnName].ToString();
                APoz1Value = float.Parse(_Poz1);

                // POZ 2 VALUE
                _rowNum = 4; // row number
                _columnName = "APoz2";  // database table column name
                _Poz2 = dt.Rows[_rowNum][_columnName].ToString();
                APoz2Value = float.Parse(_Poz2);

                AonePointAverageValueMeas4 = (APoz1Value + APoz2Value) / 2;



                // CHECK DATA IN COLUMN
                //foreach (DataRow dr in dt.Rows)
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
                    Console.WriteLine("Veza sa bazom podataka je zatvorena!");
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
