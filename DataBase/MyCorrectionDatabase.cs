using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataBase
{
    public class MyCorrectionDatabase : INotifyPropertyChanged
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
        // Databaseposition 4 - Diameter C correction 
        private float _correctionCforMachine;
        public float CorrectionCforMachine
        {
            get { return _correctionCforMachine; }
            set
            {
                if (_correctionCforMachine != value)
                {
                    _correctionCforMachine = value;
                    OnPropertyChanged("CorrectionCforMachine");
                }
            }
        }
        // Databaseposition 5 - Diameter A2 correction
        private float _correctionA2forMachine;
        public float CorrectionA2forMachine
        {
            get { return _correctionA2forMachine; }
            set
            {
                if (_correctionA2forMachine != value)
                {
                    _correctionA2forMachine = value;
                    OnPropertyChanged("CorrectionA2forMachine");
                }
            }
        }
        // Databaseposition 6 - Diameter A1 correction
        private float _correctionA1forMachine;
        public float CorrectionA1forMachine
        {
            get { return _correctionA1forMachine; }
            set
            {
                if (_correctionA1forMachine != value)
                {
                    _correctionA1forMachine = value;
                    OnPropertyChanged("CorrectionA1forMachine");
                }
            }
        }
        // Databaseposition 7 - Diameter B correction
        private float _correctionBforMachine;
        public float CorrectionBforMachine
        {
            get { return _correctionBforMachine; }
            set
            {
                if (_correctionBforMachine != value)
                {
                    _correctionBforMachine = value;
                    OnPropertyChanged("CorrectionBforMachine");
                }
            }
        }
        // Databaseposition 8 - Diameter B correction
        private float _correctionJforMachine;
        public float CorrectionJforMachine
        {
            get { return _correctionJforMachine; }
            set
            {
                if (_correctionJforMachine != value)
                {
                    _correctionJforMachine = value;
                    OnPropertyChanged("CorrectionJforMachine");
                }
            }
        }
        // Databaseposition 9 - Diameter F correction
        private float _correctionFforMachine;
        public float CorrectionFforMachine
        {
            get { return _correctionFforMachine; }
            set
            {
                if (_correctionFforMachine != value)
                {
                    _correctionFforMachine = value;
                    OnPropertyChanged("CorrectionFforMachine");
                }
            }
        }
        // Databaseposition 10 - Diameter E correction
        private float _correctionEforMachine;
        public float CorrectionEforMachine
        {
            get { return _correctionEforMachine; }
            set
            {
                if (_correctionEforMachine != value)
                {
                    _correctionEforMachine = value;
                    OnPropertyChanged("CorrectionEforMachine");
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
                           "(`IDMjerenje`, `Vrijeme`, `RadniNalog`, `KorekcijaC`, `KorekcijaA2`, `KorekcijaA1`,`KorekcijaB`, `KorekcijaJ`, `KorekcijaF`, `KorekcijaE` ) " +
                           "VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10)";
            // Connect to database
            //MySqlConnection databaseConnection = new MySqlConnection(mySQLconnectionString);
            // Create command for database - 
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.Parameters.AddWithValue("@1", "NULL");
            commandDatabase.Parameters.AddWithValue("@2", inDateTimeToDb);
            commandDatabase.Parameters.AddWithValue("@3", RadniNalog);
            commandDatabase.Parameters.AddWithValue("@4", CorrectionCforMachine);
            commandDatabase.Parameters.AddWithValue("@5", CorrectionA2forMachine);
            commandDatabase.Parameters.AddWithValue("@6", CorrectionA1forMachine);
            commandDatabase.Parameters.AddWithValue("@7", CorrectionBforMachine);
            commandDatabase.Parameters.AddWithValue("@8", CorrectionJforMachine);
            commandDatabase.Parameters.AddWithValue("@9", CorrectionFforMachine);
            commandDatabase.Parameters.AddWithValue("@10", CorrectionEforMachine);
            
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
