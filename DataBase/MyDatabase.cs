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

        // Databaseposition 2 - Workpiece name in documentation
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
        // Databaseposition 3 - Diameter C Position 1
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
        // Databaseposition 4 - Diameter C Position 2
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
        // Databaseposition 5 - Diameter A1.1 Position 1
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
        // Databaseposition 6 - Diameter A1.1 Position 2
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
        // Databaseposition 7 - Diameter A1.2 Position 1
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
        // Databaseposition 8 - Diameter A1.2 Position 2
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
        // Databaseposition 9 - Diameter B Position 1
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
        // Databaseposition 10 - Diameter B Position 2
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
        // Databaseposition 11 - Dimension F1 with Length Gauge 2 (LG2) Position 1
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
        // Databaseposition 12 - Dimension F1 with Length Gauge 3 (LG3) Position 1
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
        // Databaseposition 13 - Dimension F2 with Length Gauge 2 (LG2) Position 1
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
        // Databaseposition 14 - Dimension F2 with Length Gauge 3 (LG3) Position 1
        private float _kotaf2lg3poz1;
        public float KotaF2LG3Pos1
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
        // Databaseposition 15 - Dimension F1 with Length Gauge 2 (LG2) Position 2
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
        // Databaseposition 16 - Dimension F1 with Length Gauge 3 (LG3) Position 2
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
        // Databaseposition 17 - Dimension F2 with Length Gauge 2 (LG2) Position 2
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
        // Databaseposition 18 - Dimension F2 with Length Gauge 3 (LG3) Position 2
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
        // Databaseposition 19 - Height E Position 1
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
        // Databaseposition 20 - Height E Position 2
        private float _kotacepoz2;
        public float KotaEPos2
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
        // Databaseposition 21 - Height D Position 1
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
        // Databaseposition 22 - Height D Position 2
        private float _kotadPoz2;
        public float KotaDPos2
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
        // Write to database
        public void ModifyDb(string mySQLconnectionString)
        {
            DateTime myDateTime = DateTime.Now;
            string inDateTimeToDb = myDateTime.ToString("yyyy-MM-dd H:mm:ss");
            MySqlConnection databaseConnection = new MySqlConnection(mySQLconnectionString);
            // Good practice
            string query = "INSERT INTO mjerenevrijednosti(`IDMjerenje`,`DateTime`, `RadniNalog`, `KotaC`, `KotaA11`, `KotaA12`, `KotaB`, `KotaF`, `KotaE`, `KotaD`) VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9)";
            // Connect to database
            //MySqlConnection databaseConnection = new MySqlConnection(mySQLconnectionString);
            // Create command for database
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.Parameters.AddWithValue("@0", "NULL");
            commandDatabase.Parameters.AddWithValue("@1", inDateTimeToDb);
            commandDatabase.Parameters.AddWithValue("@2", RadniNalog);
            commandDatabase.Parameters.AddWithValue("@3", KotaCPoz1);
            commandDatabase.Parameters.AddWithValue("@4", KotaA11Poz1);
            commandDatabase.Parameters.AddWithValue("@5", KotaA12Poz1);
            commandDatabase.Parameters.AddWithValue("@6", KotaBPoz1);
            commandDatabase.Parameters.AddWithValue("@7", KotaF1LG2Poz1);
            commandDatabase.Parameters.AddWithValue("@8", KotaEPoz1);
            commandDatabase.Parameters.AddWithValue("@9", KotaDPoz1);
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
