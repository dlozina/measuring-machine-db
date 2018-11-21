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

        private float _kotac;
        public float KotaC
        {
            get { return _kotac; }
            set
            {
                if (_kotac != value)
                {
                    _kotac = value;
                    OnPropertyChanged("KotaC");
                }
            }
        }

        private float _kotaca11;
        public float KotaA11
        {
            get { return _kotaca11; }
            set
            {
                if (_kotaca11 != value)
                {
                    _kotaca11 = value;
                    OnPropertyChanged("KotaA11");
                }
            }
        }

        private float _kotaca12;
        public float KotaA12
        {
            get { return _kotaca12; }
            set
            {
                if (_kotaca12 != value)
                {
                    _kotaca12 = value;
                    OnPropertyChanged("KotaA12");
                }
            }
        }

        private float _kotacab;
        public float KotaB
        {
            get { return _kotacab; }
            set
            {
                if (_kotacab != value)
                {
                    _kotacab = value;
                    OnPropertyChanged("KotaB");
                }
            }
        }

        private float _kotacaf;
        public float KotaF
        {
            get { return _kotacaf; }
            set
            {
                if (_kotacaf != value)
                {
                    _kotacaf = value;
                    OnPropertyChanged("KotaF");
                }
            }
        }

        private float _kotacae;
        public float KotaE
        {
            get { return _kotacae; }
            set
            {
                if (_kotacae != value)
                {
                    _kotacae = value;
                    OnPropertyChanged("KotaE");
                }
            }
        }

        private float _kotacad;
        public float KotaD
        {
            get { return _kotacad; }
            set
            {
                if (_kotacad != value)
                {
                    _kotacad = value;
                    OnPropertyChanged("KotaD");
                }
            }
        }


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
            commandDatabase.Parameters.AddWithValue("@3", KotaC);
            commandDatabase.Parameters.AddWithValue("@4", KotaA11);
            commandDatabase.Parameters.AddWithValue("@5", KotaA12);
            commandDatabase.Parameters.AddWithValue("@6", KotaB);
            commandDatabase.Parameters.AddWithValue("@7", KotaF);
            commandDatabase.Parameters.AddWithValue("@8", KotaE);
            commandDatabase.Parameters.AddWithValue("@9", KotaD);
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
