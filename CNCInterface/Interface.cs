using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNCInterface
{
    public class Interface : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        // CNC Machine IP
        //public string CNCAdress; //= "190.190.11.11";
        // CNC Port Number
        //public ushort PortNumber; //= 8193;
        // TimeOut
        // Error
        private string _errorCode;

        public string ErrorCode
        {
            get { return _errorCode; }
            set
            {
                if (_errorCode != value)
                {
                    _errorCode = value;
                    OnPropertyChanged("ErrorCode");
                }
            }
        }

        public int TimeOut = 10;
        // CNC handle
        public ushort Handle;
        // CNC return value
        public short ComReturnValue;
        // Tool Info Variables
        public short OfsType;
        public short UseNo;
        //Tool Correction X
        public int ToolXcorrection;
        //Tool Correction Y
        public int ToolYcorrection;
        //Tool Correction Z
        public int ToolZcorrection;

        public int ret;

        public void TestConnection(string cncAdress, ushort portNumber)
        {
            Focas1.ODBST buf = new Focas1.ODBST();
            ComReturnValue = Focas1.cnc_allclibhndl3(cncAdress, portNumber, TimeOut, out Handle);
            if (ComReturnValue == Focas1.EW_OK)
            {
                // Get CNC info
                Focas1.cnc_statinfo(Handle, buf);
                // Close COM handle
                Focas1.cnc_freelibhndl(Handle);
            }
            else
            {
                Console.WriteLine("ERROR!({0})", ComReturnValue);
                ErrorCode = $"({ComReturnValue})";
            }
        }

        public void ReadMachineToolInfo(string cncAdress, ushort portNumber)
        {
            Focas1.ODBTLINF buf = new Focas1.ODBTLINF();
            ComReturnValue = Focas1.cnc_allclibhndl3(cncAdress, portNumber, TimeOut, out Handle);
            if (ComReturnValue == Focas1.EW_OK)
            {
                // Get CNC info
                Focas1.cnc_rdtofsinfo(Handle, buf);
                // Store Number and Offset type for tool
                UseNo = 120; //buf.use_no;
                OfsType = buf.ofs_type;
                // Close COM handle
                Focas1.cnc_freelibhndl(Handle);
            }
            else
            {
                Console.WriteLine("ERROR!({0})", ComReturnValue);
                ErrorCode = $"({ComReturnValue})";
            }
        }

        public void ReadToolOffset(string cncAdress, ushort portNumber)
        {

            Focas1.ODBTOFS bufX = new Focas1.ODBTOFS();
            Focas1.ODBTOFS bufZ = new Focas1.ODBTOFS();
            Focas1.ODBTOFS bufR = new Focas1.ODBTOFS();
            ComReturnValue = Focas1.cnc_allclibhndl3(cncAdress, portNumber, TimeOut, out Handle);
            if (ComReturnValue == Focas1.EW_OK)
            {
                ret = Focas1.cnc_rdtofs(Handle, UseNo, 0, 8, bufX);
                Console.WriteLine("X({0}) = {1}", UseNo, bufX.data);
                ret = Focas1.cnc_rdtofs(Handle, UseNo, 2, 8, bufZ);
                Console.WriteLine("Z({0}) = {1}", UseNo, bufZ.data);
                ret = Focas1.cnc_rdtofs(Handle, UseNo, 4, 8, bufR);
                Console.WriteLine("R({0}) = {1}", UseNo, bufR.data);
            }
            else
            {
                Console.WriteLine("ERROR!({0})", ComReturnValue);
                ErrorCode = $"({ComReturnValue})";
            }
        }

        public void WriteToolOffsetX(string cncAdress, ushort portNumber, int value)
        {
            ComReturnValue = Focas1.cnc_allclibhndl3(cncAdress, portNumber, TimeOut, out Handle);
            if (ComReturnValue == Focas1.EW_OK)
            {
                ToolXcorrection = value;
                // X tool offset write
                Focas1.cnc_wrtofs(Handle, UseNo, 0, 8, ToolXcorrection);
            }
            else
            {
                Console.WriteLine("ERROR!({0})", ComReturnValue);
                ErrorCode = $"({ComReturnValue})";
            }
        }

        public void WriteToolOffsetZ(string cncAdress, ushort portNumber, int value)
        {
            ComReturnValue = Focas1.cnc_allclibhndl3(cncAdress, portNumber, TimeOut, out Handle);
            if (ComReturnValue == Focas1.EW_OK)
            {
                ToolYcorrection = value;
                // Y tool offset write
                Focas1.cnc_wrtofs(Handle, UseNo, 2, 8, ToolYcorrection);
            }
            else
            {
                Console.WriteLine("ERROR!({0})", ComReturnValue);
                ErrorCode = $"({ComReturnValue})";
            }
        }

        public void WriteToolOffsetR(string cncAdress, ushort portNumber, int value)
        {
            ErrorCode = "None";
            ComReturnValue = Focas1.cnc_allclibhndl3(cncAdress, portNumber, TimeOut, out Handle);
            if (ComReturnValue == Focas1.EW_OK)
            {
                ToolZcorrection = value;
                // Z tool offset write
                Focas1.cnc_wrtofs(Handle, UseNo, 4, 8, ToolZcorrection);
            }
            else
            {
                Console.WriteLine("ERROR!({0})", ComReturnValue);
                ErrorCode = $"({ComReturnValue})";
            }
        }

        // Property changed implementation
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
