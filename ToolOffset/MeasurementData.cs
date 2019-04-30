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

        #region Nominal Values for M1 from PLC
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
        #endregion

        #region Nominal values for M2 from PLC
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
        #endregion

        #region C -> Corection data to Machine
        // CORECTION DATA
        private float _correctionCno1;
        public float CorrectionCno1
        {
            get { return _correctionCno1; }
            set
            {
                if (_correctionCno1 != value)
                {
                    _correctionCno1 = value;
                    OnPropertyChanged("CorrectionCno1");
                }
            }
        }

        private float _correctionCno2;
        public float CorrectionCno2
        {
            get { return _correctionCno2; }
            set
            {
                if (_correctionCno2 != value)
                {
                    _correctionCno2 = value;
                    OnPropertyChanged("CorrectionCno2");
                }
            }
        }

        private float _correctionCno3;
        public float CorrectionCno3
        {
            get { return _correctionCno3; }
            set
            {
                if (_correctionCno3 != value)
                {
                    _correctionCno3 = value;
                    OnPropertyChanged("CorrectionCno3");
                }
            }
        }

        private float _correctionCno4;
        public float CorrectionCno4
        {
            get { return _correctionCno4; }
            set
            {
                if (_correctionCno4 != value)
                {
                    _correctionCno4 = value;
                    OnPropertyChanged("CorrectionCno4");
                }
            }
        }

        private float _correctionCno5;
        public float CorrectionCno5
        {
            get { return _correctionCno5; }
            set
            {
                if (_correctionCno5 != value)
                {
                    _correctionCno5 = value;
                    OnPropertyChanged("CorrectionCno5");
                }
            }
        }

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
        #endregion

        #region A one point -> Corection data to Machine
        // CORECTION DATA
        private float _correctionA1no1;
        public float CorrectionA1no1
        {
            get { return _correctionA1no1; }
            set
            {
                if (_correctionA1no1 != value)
                {
                    _correctionA1no1 = value;
                    OnPropertyChanged("CorrectionA1no1");
                }
            }
        }

        private float _correctionA1no2;
        public float CorrectionA1no2
        {
            get { return _correctionA1no2; }
            set
            {
                if (_correctionA1no2 != value)
                {
                    _correctionA1no2 = value;
                    OnPropertyChanged("CorrectionA1no2");
                }
            }
        }

        private float _correctionA1no3;
        public float CorrectionA1no3
        {
            get { return _correctionA1no3; }
            set
            {
                if (_correctionA1no3 != value)
                {
                    _correctionA1no3 = value;
                    OnPropertyChanged("CorrectionA1no3");
                }
            }
        }

        private float _correctionA1no4;
        public float CorrectionA1no4
        {
            get { return _correctionA1no4; }
            set
            {
                if (_correctionA1no4 != value)
                {
                    _correctionA1no4 = value;
                    OnPropertyChanged("CorrectionA1no4");
                }
            }
        }

        private float _correctionA1no5;
        public float CorrectionA1no5
        {
            get { return _correctionA1no5; }
            set
            {
                if (_correctionA1no5 != value)
                {
                    _correctionA1no5 = value;
                    OnPropertyChanged("CorrectionA1no5");
                }
            }
        }

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
        #endregion

        #region A two point -> Corection data to Machine
        // CORECTION DATA
        private float _correctionA2no1;
        public float CorrectionA2no1
        {
            get { return _correctionA2no1; }
            set
            {
                if (_correctionA2no1 != value)
                {
                    _correctionA2no1 = value;
                    OnPropertyChanged("CorrectionA2no1");
                }
            }
        }

        private float _correctionA2no2;
        public float CorrectionA2no2
        {
            get { return _correctionA2no2; }
            set
            {
                if (_correctionA2no2 != value)
                {
                    _correctionA2no2 = value;
                    OnPropertyChanged("CorrectionA2no2");
                }
            }
        }

        private float _correctionA2no3;
        public float CorrectionA2no3
        {
            get { return _correctionA2no3; }
            set
            {
                if (_correctionA2no3 != value)
                {
                    _correctionA2no3 = value;
                    OnPropertyChanged("CorrectionA2no3");
                }
            }
        }

        private float _correctionA2no4;
        public float CorrectionA2no4
        {
            get { return _correctionA1no4; }
            set
            {
                if (_correctionA1no4 != value)
                {
                    _correctionA1no4 = value;
                    OnPropertyChanged("CorrectionA1no4");
                }
            }
        }

        private float _correctionA2no5;
        public float CorrectionA2no5
        {
            get { return _correctionA2no5; }
            set
            {
                if (_correctionA2no5 != value)
                {
                    _correctionA2no5 = value;
                    OnPropertyChanged("CorrectionA2no5");
                }
            }
        }

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
        #endregion

        #region B -> Corection data to Machine
        // CORECTION DATA
        private float _correctionBno1;
        public float CorrectionBno1
        {
            get { return _correctionBno1; }
            set
            {
                if (_correctionBno1 != value)
                {
                    _correctionBno1 = value;
                    OnPropertyChanged("CorrectionBno1");
                }
            }
        }

        private float _correctionBno2;
        public float CorrectionBno2
        {
            get { return _correctionBno2; }
            set
            {
                if (_correctionBno2 != value)
                {
                    _correctionBno2 = value;
                    OnPropertyChanged("CorrectionBno2");
                }
            }
        }

        private float _correctionBno3;
        public float CorrectionBno3
        {
            get { return _correctionBno3; }
            set
            {
                if (_correctionBno3 != value)
                {
                    _correctionBno3 = value;
                    OnPropertyChanged("CorrectionBno3");
                }
            }
        }

        private float _correctionBno4;
        public float CorrectionBno4
        {
            get { return _correctionBno4; }
            set
            {
                if (_correctionBno4 != value)
                {
                    _correctionBno4 = value;
                    OnPropertyChanged("CorrectionBno4");
                }
            }
        }

        private float _correctionBno5;
        public float CorrectionBno5
        {
            get { return _correctionBno5; }
            set
            {
                if (_correctionBno5 != value)
                {
                    _correctionBno5 = value;
                    OnPropertyChanged("CorrectionBno5");
                }
            }
        }

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
        #endregion

        #region F -> Corection data to Machine
        // CORECTION DATA
        private float _correctionFno1;
        public float CorrectionFno1
        {
            get { return _correctionFno1; }
            set
            {
                if (_correctionFno1 != value)
                {
                    _correctionFno1 = value;
                    OnPropertyChanged("CorrectionFno1");
                }
            }
        }

        private float _correctionFno2;
        public float CorrectionFno2
        {
            get { return _correctionFno2; }
            set
            {
                if (_correctionFno2 != value)
                {
                    _correctionFno2 = value;
                    OnPropertyChanged("CorrectionFno2");
                }
            }
        }

        private float _correctionFno3;
        public float CorrectionFno3
        {
            get { return _correctionFno3; }
            set
            {
                if (_correctionFno3 != value)
                {
                    _correctionFno3 = value;
                    OnPropertyChanged("CorrectionFno3");
                }
            }
        }

        private float _correctionFno4;
        public float CorrectionFno4
        {
            get { return _correctionFno4; }
            set
            {
                if (_correctionFno4 != value)
                {
                    _correctionFno4 = value;
                    OnPropertyChanged("CorrectionFno4");
                }
            }
        }

        private float _correctionFno5;
        public float CorrectionFno5
        {
            get { return _correctionFno5; }
            set
            {
                if (_correctionFno5 != value)
                {
                    _correctionFno5 = value;
                    OnPropertyChanged("CorrectionFno5");
                }
            }
        }

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
        #endregion

        #region E -> Corection data to Machine
        // CORECTION DATA
        private float _correctionEno1;
        public float CorrectionEno1
        {
            get { return _correctionEno1; }
            set
            {
                if (_correctionEno1 != value)
                {
                    _correctionEno1 = value;
                    OnPropertyChanged("CorrectionEno1");
                }
            }
        }

        private float _correctionEno2;
        public float CorrectionEno2
        {
            get { return _correctionEno2; }
            set
            {
                if (_correctionEno2 != value)
                {
                    _correctionEno2 = value;
                    OnPropertyChanged("CorrectionEno2");
                }
            }
        }

        private float _correctionEno3;
        public float CorrectionEno3
        {
            get { return _correctionEno3; }
            set
            {
                if (_correctionEno3 != value)
                {
                    _correctionEno3 = value;
                    OnPropertyChanged("CorrectionEno3");
                }
            }
        }

        private float _correctionEno4;
        public float CorrectionEno4
        {
            get { return _correctionEno4; }
            set
            {
                if (_correctionEno4 != value)
                {
                    _correctionEno4 = value;
                    OnPropertyChanged("CorrectionEno4");
                }
            }
        }

        private float _correctionEno5;
        public float CorrectionEno5
        {
            get { return _correctionEno5; }
            set
            {
                if (_correctionEno5 != value)
                {
                    _correctionEno5 = value;
                    OnPropertyChanged("CorrectionEno5");
                }
            }
        }

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
        #endregion

        // Property changed implementation
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
