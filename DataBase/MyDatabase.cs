using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataBase
{
    public class MyDatabase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Databaseposition 3 - Workpiece name in documentation
        private string _radninalog;
        public string RadniNalog
        {
            get { return _radninalog; }
            set
            {
                if (_radninalog != value)
                {
                    _radninalog = value;
                    OnPropertyChanged("RadniNalog");
                }
            }
        }
        // Databaseposition 4 - Diameter C Position 1
        private float _kotacpoz1;
        public float KotaCPoz1
        {
            get { return _kotacpoz1; }
            set
            {
                if (_kotacpoz1 != value)
                {
                    _kotacpoz1 = value;
                    OnPropertyChanged("KotaCPoz1");
                }
            }
        }
        // Databaseposition 5 - Diameter C Position 2
        private float _kotacpoz2;
        public float KotaCPoz2
        {
            get { return _kotacpoz2; }
            set
            {
                if (_kotacpoz2 != value)
                {
                    _kotacpoz2 = value;
                    OnPropertyChanged("KotaCPoz2");
                }
            }
        }
        // Databaseposition 6 - Diameter A1.1 Position 1
        private float _kotaca11poz1;
        public float KotaA11Poz1
        {
            get { return _kotaca11poz1; }
            set
            {
                if (_kotaca11poz1 != value)
                {
                    _kotaca11poz1 = value;
                    OnPropertyChanged("KotaA11Poz1");
                }
            }
        }
        // Databaseposition 7 - Diameter A1.1 Position 2
        private float _kotaca11poz2;
        public float KotaA11Poz2
        {
            get { return _kotaca11poz2; }
            set
            {
                if (_kotaca11poz2 != value)
                {
                    _kotaca11poz2 = value;
                    OnPropertyChanged("KotaA11Poz2");
                }
            }
        }
        // Databaseposition 8 - Diameter A1.2 Position 1
        private float _kotaca12poz1;
        public float KotaA12Poz1
        {
            get { return _kotaca12poz1; }
            set
            {
                if (_kotaca12poz1 != value)
                {
                    _kotaca12poz1 = value;
                    OnPropertyChanged("KotaA12Poz1");
                }
            }
        }
        // Databaseposition 9 - Diameter A1.2 Position 2
        private float _kotaca12poz2;
        public float KotaA12Poz2
        {
            get { return _kotaca12poz2; }
            set
            {
                if (_kotaca12poz2 != value)
                {
                    _kotaca12poz2 = value;
                    OnPropertyChanged("KotaA12Poz2");
                }
            }
        }
        // Databaseposition 10 - Diameter A Position 1
        private float _kotacapoz1;
        public float KotaAPoz1
        {
            get { return _kotacapoz1; }
            set
            {
                if (_kotacapoz1 != value)
                {
                    _kotacapoz1 = value;
                    OnPropertyChanged("KotaAPoz1");
                }
            }
        }
        // Databaseposition 11 - Diameter A Position 2
        private float _kotacapoz2;
        public float KotaAPoz2
        {
            get { return _kotacapoz2; }
            set
            {
                if (_kotacapoz2 != value)
                {
                    _kotacapoz2 = value;
                    OnPropertyChanged("KotaAPoz2");
                }
            }
        }
        // Databaseposition 12 - Diameter B Position 1
        private float _kotabpoz1;
        public float KotaBPoz1
        {
            get { return _kotabpoz1; }
            set
            {
                if (_kotabpoz1 != value)
                {
                    _kotabpoz1 = value;
                    OnPropertyChanged("KotaBPoz1");
                }
            }
        }
        // Databaseposition 13 - Diameter B Position 2
        private float _kotabpoz2;
        public float KotaBPoz2
        {
            get { return _kotabpoz2; }
            set
            {
                if (_kotabpoz2 != value)
                {
                    _kotabpoz2 = value;
                    OnPropertyChanged("KotaBPoz2");
                }
            }
        }
        // Databaseposition 14 - Dimension F1 with Length Gauge 2 (LG2) Position 1
        private float _kotaf1lg2poz1;
        public float KotaF1LG2Poz1
        {
            get { return _kotaf1lg2poz1; }
            set
            {
                if (_kotaf1lg2poz1 != value)
                {
                    _kotaf1lg2poz1 = value;
                    OnPropertyChanged("KotaF1LG2Poz1");
                }
            }
        }
        // Databaseposition 15 - Dimension F1 with Length Gauge 3 (LG3) Position 1
        private float _kotaf1lg3poz1;
        public float KotaF1LG3Poz1
        {
            get { return _kotaf1lg3poz1; }
            set
            {
                if (_kotaf1lg3poz1 != value)
                {
                    _kotaf1lg3poz1 = value;
                    OnPropertyChanged("KotaF1LG3Poz1");
                }
            }
        }
        // Databaseposition 16 - Dimension F2 with Length Gauge 2 (LG2) Position 1
        private float _kotaf2lg2poz1;
        public float KotaF2LG2Poz1
        {
            get { return _kotaf2lg2poz1; }
            set
            {
                if (_kotaf2lg2poz1 != value)
                {
                    _kotaf2lg2poz1 = value;
                    OnPropertyChanged("KotaF2LG2Poz1");
                }
            }
        }
        // Databaseposition 17 - Dimension F2 with Length Gauge 3 (LG3) Position 1
        private float _kotaf2lg3poz1;
        public float KotaF2LG3Poz1
        {
            get { return _kotaf2lg3poz1; }
            set
            {
                if (_kotaf2lg3poz1 != value)
                {
                    _kotaf2lg3poz1 = value;
                    OnPropertyChanged("KotaF2LG3Poz1");
                }
            }
        }
        // Databaseposition 18 - Dimension F1 with Length Gauge 2 (LG2) Position 2
        private float _kotaf1lg2poz2;
        public float KotaF1LG2Poz2
        {
            get { return _kotaf1lg2poz2; }
            set
            {
                if (_kotaf1lg2poz2 != value)
                {
                    _kotaf1lg2poz2 = value;
                    OnPropertyChanged("KotaF1LG2Poz2");
                }
            }
        }
        // Databaseposition 19 - Dimension F1 with Length Gauge 3 (LG3) Position 2
        private float _kotaf1lg3poz2;
        public float KotaF1LG3Poz2
        {
            get { return _kotaf1lg3poz2; }
            set
            {
                if (_kotaf1lg3poz2 != value)
                {
                    _kotaf1lg3poz2 = value;
                    OnPropertyChanged("KotaF1LG3Poz2");
                }
            }
        }
        // Databaseposition 20 - Dimension F2 with Length Gauge 2 (LG2) Position 2
        private float _kotaf2lg2poz2;
        public float KotaF2LG2Poz2
        {
            get { return _kotaf2lg2poz2; }
            set
            {
                if (_kotaf2lg2poz2 != value)
                {
                    _kotaf2lg2poz2 = value;
                    OnPropertyChanged("KotaF2LG2Poz2");
                }
            }
        }
        // Databaseposition 21 - Dimension F2 with Length Gauge 3 (LG3) Position 2
        private float _kotaf2lg3poz2;
        public float KotaF2LG3Poz2
        {
            get { return _kotaf2lg3poz2; }
            set
            {
                if (_kotaf2lg3poz2 != value)
                {
                    _kotaf2lg3poz2 = value;
                    OnPropertyChanged("KotaF2LG3Poz2");
                }
            }
        }
        // Databaseposition 22 - Height E Position 1
        private float _kotacepoz1;
        public float KotaEPoz1
        {
            get { return _kotacepoz1; }
            set
            {
                if (_kotacepoz1 != value)
                {
                    _kotacepoz1 = value;
                    OnPropertyChanged("KotaEPoz1");
                }
            }
        }
        // Databaseposition 23 - Height E Position 2
        private float _kotacepoz2;
        public float KotaEPoz2
        {
            get { return _kotacepoz2; }
            set
            {
                if (_kotacepoz2 != value)
                {
                    _kotacepoz2 = value;
                    OnPropertyChanged("KotaEPoz2");
                }
            }
        }
        // Databaseposition 24 - Height D Position 1
        private float _kotadPoz1;
        public float KotaDPoz1
        {
            get { return _kotadPoz1; }
            set
            {
                if (_kotadPoz1 != value)
                {
                    _kotadPoz1 = value;
                    OnPropertyChanged("KotaDPoz1");
                }
            }
        }
        // Databaseposition 25 - Height D Position 2
        private float _kotadPoz2;
        public float KotaDPoz2
        {
            get { return _kotadPoz2; }
            set
            {
                if (_kotadPoz2 != value)
                {
                    _kotadPoz2 = value;
                    OnPropertyChanged("KotaDPoz2");
                }
            }
        }
        // Databaseposition 26 - Height H1 Position 1
        private float _kotah1Poz1;
        public float KotaH1Poz1
        {
            get { return _kotah1Poz1; }
            set
            {
                if (_kotah1Poz1 != value)
                {
                    _kotah1Poz1 = value;
                    OnPropertyChanged("KotaH1Poz1");
                }
            }
        }
        // Databaseposition 27 - Height H1 Position 2
        private float _kotah1Poz2;
        public float KotaH1Poz2
        {
            get { return _kotah1Poz2; }
            set
            {
                if (_kotah1Poz2 != value)
                {
                    _kotah1Poz2 = value;
                    OnPropertyChanged("KotaH1Poz2");
                }
            }
        }
        // Databaseposition 28 - Height K Position 1
        private float _kotakPoz1;
        public float KotaKPoz1
        {
            get { return _kotakPoz1; }
            set
            {
                if (_kotakPoz1 != value)
                {
                    _kotakPoz1 = value;
                    OnPropertyChanged("KotaKPoz1");
                }
            }
        }
        // Databaseposition 29 - Height K Position 2
        private float _kotakPoz2;
        public float KotaKPoz2
        {
            get { return _kotakPoz2; }
            set
            {
                if (_kotakPoz2 != value)
                {
                    _kotakPoz2 = value;
                    OnPropertyChanged("KotaKPoz2");
                }
            }
        }
        // Write to database
        public void ModifyDb(string mySQLconnectionString, string tableName)
        {
            DateTime myDateTime = DateTime.Now;
            string inDateTimeToDb = myDateTime.ToString("yyyy-MM-dd H:mm:ss");
            MySqlConnection databaseConnection = new MySqlConnection(mySQLconnectionString);
            // Good practice
            // Datacontext does not acept . ex. `A1.2Poz1`,`A1.2Poz2`, `A1.1Poz1`, `A1.1Poz2`
            string query = $"INSERT INTO {tableName} " +
                           "(`IDMjerenje`, `Vrijeme`, `RadniNalog`, `CPoz1`, `CPoz2`, `A12Poz1`,`A12Poz2`, `A11Poz1`, `A11Poz2`, `BPoz1`, `BPoz2`, `F1Ticalo2Poz1`,`F2Ticalo2Poz1`, `F1Ticalo3Poz1`, `F2Ticalo3Poz1`, `F1Ticalo2Poz2`, `F2Ticalo2Poz2`, `F1Ticalo3Poz2`,`F2Ticalo3Poz2`, `DPoz1`, `DPoz2`, `EPoz1`, `EPoz2`, `H1Poz1`, `H1Poz2`, `KPoz1`, `KPoz2` ) " +
                           "VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17, @18, @19, @20, @21, @22, @23, @24, @25, @26, @27, @28, @29)";
            // Connect to database
            //MySqlConnection databaseConnection = new MySqlConnection(mySQLconnectionString);
            // Create command for database - 
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.Parameters.AddWithValue("@1", "NULL");
            commandDatabase.Parameters.AddWithValue("@2", inDateTimeToDb);
            commandDatabase.Parameters.AddWithValue("@3", RadniNalog);
            commandDatabase.Parameters.AddWithValue("@4", KotaCPoz1);
            commandDatabase.Parameters.AddWithValue("@5", KotaCPoz2);
            commandDatabase.Parameters.AddWithValue("@6", KotaA12Poz1);
            commandDatabase.Parameters.AddWithValue("@7", KotaA12Poz2);
            commandDatabase.Parameters.AddWithValue("@8", KotaA11Poz1);
            commandDatabase.Parameters.AddWithValue("@9", KotaA11Poz2);
            commandDatabase.Parameters.AddWithValue("@10", KotaAPoz1);
            commandDatabase.Parameters.AddWithValue("@11", KotaAPoz2);
            commandDatabase.Parameters.AddWithValue("@12", KotaBPoz1);
            commandDatabase.Parameters.AddWithValue("@13", KotaBPoz2);
            commandDatabase.Parameters.AddWithValue("@14", KotaF1LG2Poz1);
            commandDatabase.Parameters.AddWithValue("@15", KotaF2LG2Poz1);
            commandDatabase.Parameters.AddWithValue("@16", KotaF1LG3Poz1);
            commandDatabase.Parameters.AddWithValue("@17", KotaF2LG3Poz1);
            commandDatabase.Parameters.AddWithValue("@18", KotaF1LG2Poz2);
            commandDatabase.Parameters.AddWithValue("@19", KotaF2LG2Poz2);
            commandDatabase.Parameters.AddWithValue("@20", KotaF1LG3Poz2);
            commandDatabase.Parameters.AddWithValue("@21", KotaF2LG3Poz2);
            commandDatabase.Parameters.AddWithValue("@22", KotaDPoz1);
            commandDatabase.Parameters.AddWithValue("@23", KotaDPoz2);
            commandDatabase.Parameters.AddWithValue("@24", KotaEPoz1);
            commandDatabase.Parameters.AddWithValue("@25", KotaEPoz2);
            commandDatabase.Parameters.AddWithValue("@26", KotaH1Poz1);
            commandDatabase.Parameters.AddWithValue("@27", KotaH1Poz2);
            commandDatabase.Parameters.AddWithValue("@28", KotaKPoz1);
            commandDatabase.Parameters.AddWithValue("@29", KotaKPoz2);
            // Good practice add query timeout 60 sec?
            commandDatabase.CommandTimeout = 60;
            try
            {
                // Open com with database
                databaseConnection.Open();
                // Send command to database
                // When we ex. insert data in DB
                commandDatabase.ExecuteNonQuery();
                Console.WriteLine("Mjerenje je spremljeno u bazi!");
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

        // Property changed implementation
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
