using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace ToolOffset
{
    public class MeasurmentCalculation
    {
        // 1. Read data from database - Last 5 members
        private int DataBaseCounter = 0;
        private int DataBaseReadCounter;
        private int LastFiveChanges = 0;
        System.Timers.Timer Clock_ms;
        // Database string - Change if needed
        static string MySQLconnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=mjernastanica;SslMode=none";
        // Select all data in database
        private string query = "SELECT * FROM `stroj2` ORDER BY IDMjerenje DESC LIMIT 5";
        private string queryCount = "SELECT COUNT(*) FROM `stroj2`";
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        private int _rowNum;
        private string _columnName;
        private string _cPoz1;
        private string _cPoz2;
        private float CPoz1Value;
        private float CPoz2Value;


        public MeasurmentCalculation()
        {
            
            Clock_ms = new System.Timers.Timer(60000);
            Clock_ms.Elapsed += OnClockmsTick;
            Clock_ms.AutoReset = false;
        }

        public void StartCyclic()
        {
            Clock_ms.Start();
        }

        private void OnClockmsTick(Object source, System.Timers.ElapsedEventArgs e)
        {
            DatabaseCount();
            Clock_ms.Start();
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

                //***********************************
                // MEASUREMENT NO.1                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 0; // row number
                _columnName = "CPoz1";  // database table column name
                _cPoz1 = dt.Rows[_rowNum][_columnName].ToString();
                CPoz1Value = float.Parse(_cPoz1);

                // POZ 2 VALUE
                _rowNum = 0; // row number
                _columnName = "CPoz2";  // database table column name
                _cPoz2 = dt.Rows[_rowNum][_columnName].ToString();
                CPoz2Value = float.Parse(_cPoz2);

                float CAverageValueMeas1 = (CPoz1Value + CPoz2Value) / 2;

                // COMPARE AVERAGE VALUE WITH NOMINAL


                //***********************************
                // MEASUREMENT NO.2                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 1; // row number
                _columnName = "CPoz1";  // database table column name
                _cPoz1 = dt.Rows[_rowNum][_columnName].ToString();
                CPoz1Value = float.Parse(_cPoz1);

                // POZ 2 VALUE
                _rowNum = 1; // row number
                _columnName = "CPoz2";  // database table column name
                _cPoz2 = dt.Rows[_rowNum][_columnName].ToString();
                CPoz2Value = float.Parse(_cPoz2);

                float CAverageValueMeas2 = (CPoz1Value + CPoz2Value) / 2;

                //***********************************
                // MEASUREMENT NO.3                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 2; // row number
                _columnName = "CPoz1";  // database table column name
                _cPoz1 = dt.Rows[_rowNum][_columnName].ToString();
                CPoz1Value = float.Parse(_cPoz1);

                // POZ 2 VALUE
                _rowNum = 2; // row number
                _columnName = "CPoz2";  // database table column name
                _cPoz2 = dt.Rows[_rowNum][_columnName].ToString();
                CPoz2Value = float.Parse(_cPoz2);

                float CAverageValueMeas3 = (CPoz1Value + CPoz2Value) / 2;

                //***********************************
                // MEASUREMENT NO.4                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 3; // row number
                _columnName = "CPoz1";  // database table column name
                _cPoz1 = dt.Rows[_rowNum][_columnName].ToString();
                CPoz1Value = float.Parse(_cPoz1);

                // POZ 2 VALUE
                _rowNum = 3; // row number
                _columnName = "CPoz2";  // database table column name
                _cPoz2 = dt.Rows[_rowNum][_columnName].ToString();
                CPoz2Value = float.Parse(_cPoz2);

                float CAverageValueMeas4 = (CPoz1Value + CPoz2Value) / 2;

                //***********************************
                // MEASUREMENT NO.5                 *
                //***********************************
                // POZ 1 VALUE
                _rowNum = 4; // row number
                _columnName = "CPoz1";  // database table column name
                _cPoz1 = dt.Rows[_rowNum][_columnName].ToString();
                CPoz1Value = float.Parse(_cPoz1);

                // POZ 2 VALUE
                _rowNum = 4; // row number
                _columnName = "CPoz2";  // database table column name
                _cPoz2 = dt.Rows[_rowNum][_columnName].ToString();
                CPoz2Value = float.Parse(_cPoz2);

                float CAverageValueMeas5 = (CPoz1Value + CPoz2Value) / 2;

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

        // 2. Check do they have same name (Same work order)

        // 3. Check is input OK (Not 0)

        // 4. Do the average calculation

        // 5. Compare to target value

        // 6. Calculate tool offset

    }
}
