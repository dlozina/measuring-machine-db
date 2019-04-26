using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolOffset
{
    public class MeasurementData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //MACHINE 1 NOMINAL VALUES
        // Nominal Value no.1
        private float _cNominalM1;
        public float cNominalM1
        {
            get { return _cNominalM1; }
            set
            {
                if (_cNominalM1 != value)
                {
                    _cNominalM1 = value;
                    OnPropertyChanged("CNominalM1");
                }
            }
        }
        // Nominal Value no.2
        private float _aNominalM1;
        public float aNominalM1
        {
            get { return _aNominalM1; }
            set
            {
                if (_aNominalM1 != value)
                {
                    _aNominalM1 = value;
                    OnPropertyChanged("ANominalM1");
                }
            }
        }
        // Nominal Value no.3
        private float _bNominalM1;
        public float bNominalM1
        {
            get { return _bNominalM1; }
            set
            {
                if (_bNominalM1 != value)
                {
                    _bNominalM1 = value;
                    OnPropertyChanged("BNominalM1");
                }
            }
        }
        // Nominal Value no.4
        private float _jNominalM1;
        public float jNominalM1
        {
            get { return _jNominalM1; }
            set
            {
                if (_jNominalM1 != value)
                {
                    _jNominalM1 = value;
                    OnPropertyChanged("JNominalM1");
                }
            }
        }
        // Nominal Value no.5
        private float _fNominalM1;
        public float fNominalM1
        {
            get { return _fNominalM1; }
            set
            {
                if (_fNominalM1 != value)
                {
                    _fNominalM1 = value;
                    OnPropertyChanged("FNominalM1");
                }
            }
        }
        // Nominal Value no.6
        private float _eNominalM1;
        public float eNominalM1
        {
            get { return _eNominalM1; }
            set
            {
                if (_eNominalM1 != value)
                {
                    _eNominalM1 = value;
                    OnPropertyChanged("ENominalM1");
                }
            }
        }
        // Nominal Value no.7
        private float _dNominalM1;
        public float dNominalM1
        {
            get { return _dNominalM1; }
            set
            {
                if (_dNominalM1 != value)
                {
                    _dNominalM1 = value;
                    OnPropertyChanged("DNominalM1");
                }
            }
        }
        // Nominal Value no.8
        private float _gNominalM1;
        public float gNominalM1
        {
            get { return _gNominalM1; }
            set
            {
                if (_gNominalM1 != value)
                {
                    _gNominalM1 = value;
                    OnPropertyChanged("GNmominalM1");
                }
            }
        }

        //MACHINE 2 NOMINAL VALUES
        // Nominal Value no.1
        private float _cNominalM2;
        public float cNominalM2
        {
            get { return _cNominalM2; }
            set
            {
                if (_cNominalM2 != value)
                {
                    _cNominalM2 = value;
                    OnPropertyChanged("CNominalM2");
                }
            }
        }
        // Nominal Value no.2
        private float _aNominalM2;
        public float aNominalM2
        {
            get { return _aNominalM2; }
            set
            {
                if (_aNominalM2 != value)
                {
                    _aNominalM2 = value;
                    OnPropertyChanged("ANominalM2");
                }
            }
        }
        // Nominal Value no.3
        private float _bNominalM2;
        public float bNominalM2
        {
            get { return _bNominalM2; }
            set
            {
                if (_bNominalM2 != value)
                {
                    _bNominalM2 = value;
                    OnPropertyChanged("BNominalM2");
                }
            }
        }
        // Nominal Value no.4
        private float _jNominalM2;
        public float jNominalM2
        {
            get { return _jNominalM2; }
            set
            {
                if (_jNominalM2 != value)
                {
                    _jNominalM2 = value;
                    OnPropertyChanged("JNominalM2");
                }
            }
        }
        // Nominal Value no.5
        private float _fNominalM2;
        public float fNominalM2
        {
            get { return _fNominalM2; }
            set
            {
                if (_fNominalM2 != value)
                {
                    _fNominalM2 = value;
                    OnPropertyChanged("FNominalM2");
                }
            }
        }
        // Nominal Value no.6
        private float _eNominalM2;
        public float eNominalM2
        {
            get { return _eNominalM2; }
            set
            {
                if (_eNominalM2 != value)
                {
                    _eNominalM2 = value;
                    OnPropertyChanged("ENominalM2");
                }
            }
        }
        // Nominal Value no.7
        private float _dNominalM2;
        public float dNominalM2
        {
            get { return _dNominalM2; }
            set
            {
                if (_dNominalM2 != value)
                {
                    _dNominalM2 = value;
                    OnPropertyChanged("DNominalM2");
                }
            }
        }
        // Nominal Value no.8
        private float _gNominalM2;
        public float gNominalM2
        {
            get { return _gNominalM2; }
            set
            {
                if (_gNominalM2 != value)
                {
                    _gNominalM2 = value;
                    OnPropertyChanged("GNmominalM2");
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
