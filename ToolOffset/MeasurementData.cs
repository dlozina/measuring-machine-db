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

        #region Nominal Values from PLC
        //MACHINE 1 NOMINAL VALUES
        // Nominal Value no.1
        private float _cNominal;
        public float cNominal
        {
            get { return _cNominal; }
            set
            {
                if (_cNominal != value)
                {
                    _cNominal = value;
                    OnPropertyChanged("CNominal");
                }
            }
        }
        // Nominal Value no.2
        private float _aNominal;
        public float aNominal
        {
            get { return _aNominal; }
            set
            {
                if (_aNominal != value)
                {
                    _aNominal = value;
                    OnPropertyChanged("ANominal");
                }
            }
        }
        // Nominal Value no.3
        private float _bNominal;
        public float bNominal
        {
            get { return _bNominal; }
            set
            {
                if (_bNominal != value)
                {
                    _bNominal = value;
                    OnPropertyChanged("BNominal");
                }
            }
        }
        // Nominal Value no.4
        private float _jNominal;
        public float jNominal
        {
            get { return _jNominal; }
            set
            {
                if (_jNominal != value)
                {
                    _jNominal = value;
                    OnPropertyChanged("JNominal");
                }
            }
        }
        // Nominal Value no.5
        private float _fNominal;
        public float fNominal
        {
            get { return _fNominal; }
            set
            {
                if (_fNominal != value)
                {
                    _fNominal = value;
                    OnPropertyChanged("FNominal");
                }
            }
        }
        // Nominal Value no.6
        private float _eNominal;
        public float eNominal
        {
            get { return _eNominal; }
            set
            {
                if (_eNominal != value)
                {
                    _eNominal = value;
                    OnPropertyChanged("ENominal");
                }
            }
        }
        // Nominal Value no.7
        private float _dNominal;
        public float dNominal
        {
            get { return _dNominal; }
            set
            {
                if (_dNominal != value)
                {
                    _dNominal = value;
                    OnPropertyChanged("DNominal");
                }
            }
        }
        // Nominal Value no.8
        private float _gNominal;
        public float gNominal
        {
            get { return _gNominal; }
            set
            {
                if (_gNominal != value)
                {
                    _gNominal = value;
                    OnPropertyChanged("GNmominal");
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
            get { return _correctionA2no4; }
            set
            {
                if (_correctionA2no4 != value)
                {
                    _correctionA2no4 = value;
                    OnPropertyChanged("CorrectionA2no4");
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

        #region J -> Corection data to Machine
        // CORECTION DATA
        private float _correctionJno1;
        public float CorrectionJno1
        {
            get { return _correctionJno1; }
            set
            {
                if (_correctionJno1 != value)
                {
                    _correctionJno1 = value;
                    OnPropertyChanged("CorrectionJno1");
                }
            }
        }

        private float _correctionJno2;
        public float CorrectionJno2
        {
            get { return _correctionJno2; }
            set
            {
                if (_correctionJno2 != value)
                {
                    _correctionJno2 = value;
                    OnPropertyChanged("CorrectionJno2");
                }
            }
        }

        private float _correctionJno3;
        public float CorrectionJno3
        {
            get { return _correctionJno3; }
            set
            {
                if (_correctionJno3 != value)
                {
                    _correctionJno3 = value;
                    OnPropertyChanged("CorrectionJno3");
                }
            }
        }

        private float _correctionJno4;
        public float CorrectionJno4
        {
            get { return _correctionJno4; }
            set
            {
                if (_correctionJno4 != value)
                {
                    _correctionJno4 = value;
                    OnPropertyChanged("CorrectionJno4");
                }
            }
        }

        private float _correctionJno5;
        public float CorrectionJno5
        {
            get { return _correctionJno5; }
            set
            {
                if (_correctionJno5 != value)
                {
                    _correctionJno5 = value;
                    OnPropertyChanged("CorrectionJno5");
                }
            }
        }

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
