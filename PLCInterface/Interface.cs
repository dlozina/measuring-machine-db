using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Snap7Library;

namespace PLCInterface
{
    public class InterfaceEventArgs: EventArgs
    {
        private Status statusData;
        public Status StatusData
        {
            get { return statusData; }
            set { statusData = value; }
        }

        public byte[] CyclicStatusBuffer { get; set; }
    }

    public class OnlineMarkerEventArgs : EventArgs
    {
        bool onlineMark;

        public bool OnlineMark
        {
            get { return onlineMark; }
            set { onlineMark = value; }
        }
    }

    // Windows Base Reference needs to be called
    public class Interface : DependencyObject
    {
        private int activeScreen;

        public int ActiveScreen
        {
            get { return activeScreen; }
            set { activeScreen = value; }
        }

        public Status STATUS { get; set; } = new Status();

        public static object StatusControlLock = new object();
        public static object TimerLock = new object();

        public delegate void UpdateHandler(Interface sender, InterfaceEventArgs e);

        public delegate void OnlineMarker(Interface sender, OnlineMarkerEventArgs e);

        public event UpdateHandler Update_100_ms;
        public event OnlineMarker Update_Online_Flag;

        public bool OnlineMark;
        public int Errorcode;
        public S7Client Client;

        System.Timers.Timer Clock_100_ms;
        System.Timers.Timer WatchDogTimer;

        private byte[] CyclicStatusBuffer = new byte[65536];
        //private byte[] ReadBuffer = new byte[65536];
        private byte[] WatchdogBuffer = new byte[2];

        public Interface()
        {
            Client = new S7Client();
            Clock_100_ms = new System.Timers.Timer(100);
            Clock_100_ms.Elapsed += OnClock100msTick;
            Clock_100_ms.AutoReset = false;

            WatchDogTimer = new System.Timers.Timer(2000);
            WatchDogTimer.Elapsed += OnClockWatchdogTick;
            WatchDogTimer.AutoReset = false;
        }

        public void StartCyclic()
        {
            Clock_100_ms.Start();
            WatchDogTimer.Start();
        }

        public void RestartInterface()
        {
            lock (Interface.TimerLock)
            {
                Client = new S7Client();
                Clock_100_ms.Stop();
                Thread.Sleep(1000);

                while (!Client.Connected())
                {
                    // Real PLC
                    //Client.ConnectTo("192.168.20.21", 0, 1);
                    // Simulation PLC
                    Client.ConnectTo("192.168.20.88", 0, 1);
                    Thread.Sleep(200);
                    if (Client.Connected())
                    {
                        Clock_100_ms.Start();
                        WatchDogTimer.Start();
                    }
                }
            }
        }

        private int ReadStatus()
        {
            int result = -99;
            if (Client.Connected())
                // Buffer 64 set for String with *14* characters
                result = Client.DBRead(48, 0, 106, CyclicStatusBuffer);
            if (result == 0)
            {
                lock (StatusControlLock)
                {
                    // Savedata
                    STATUS.Savedata.M1.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Savedata.M2.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    // Measuredpos1
                    STATUS.MeasuredPos1.C.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MeasuredPos1.A11.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MeasuredPos1.A12.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MeasuredPos1.B.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MeasuredPos1.F1LG2.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MeasuredPos1.F1LG3.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MeasuredPos1.F2LG2.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MeasuredPos1.F2LG3.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MeasuredPos1.E.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MeasuredPos1.D.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MeasuredPos1.K.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MeasuredPos1.H1.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    // Measuredpos1
                    STATUS.MeasuredPos2.C.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MeasuredPos2.A11.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MeasuredPos2.A12.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MeasuredPos2.B.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MeasuredPos2.F1LG2.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MeasuredPos2.F1LG3.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MeasuredPos2.F2LG2.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MeasuredPos2.F2LG3.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MeasuredPos2.E.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MeasuredPos2.D.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MeasuredPos2.K.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MeasuredPos2.H1.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    // Workpiecedata
                    STATUS.Workpiecedata.RadniNalog.GetValueFromGroupBuffer(CyclicStatusBuffer);
                }
            }
            return result;
        }

        public int WriteTag(plcTag tag, object value)
        {
            byte[] _tempBuffer = new byte[4];
            int result = -99;
            lock (Interface.TimerLock)
            {
                if (Client.Connected())
                {
                    switch (tag.VType)
                    {
                        case varType.BOOL:
                            result = Client.DBRead(tag.DbNumber, tag.Offset.ByteOffset, 2, _tempBuffer);
                            S7.SetBitAt(ref _tempBuffer, 0, tag.Offset.BitOffset, (bool)value);
                            result += Client.DBWrite(tag.DbNumber, tag.Offset.ByteOffset, 2, _tempBuffer);
                            break;
                        case varType.BYTE:
                            result = Client.DBRead(tag.DbNumber, tag.Offset.ByteOffset, 2, _tempBuffer);
                            S7.SetByteAt(_tempBuffer, 0, (byte)value);
                            result += Client.DBWrite(tag.DbNumber, tag.Offset.ByteOffset, 2, _tempBuffer);
                            break;
                        case varType.WORD:
                            result = Client.DBRead(tag.DbNumber, tag.Offset.ByteOffset, 2, _tempBuffer);
                            S7.SetWordAt(_tempBuffer, 0, (ushort)value);
                            result += Client.DBWrite(tag.DbNumber, tag.Offset.ByteOffset, 2, _tempBuffer);
                            break;
                        case varType.DWORD:
                            result = Client.DBRead(tag.DbNumber, tag.Offset.ByteOffset, 4, _tempBuffer);
                            S7.SetDWordAt(_tempBuffer, 0, (uint)value);
                            result += Client.DBWrite(tag.DbNumber, tag.Offset.ByteOffset, 4, _tempBuffer);
                            break;
                        case varType.INT:
                            result = Client.DBRead(tag.DbNumber, tag.Offset.ByteOffset, 2, _tempBuffer);
                            S7.SetIntAt(_tempBuffer, 0, (short)value);
                            result += Client.DBWrite(tag.DbNumber, tag.Offset.ByteOffset, 2, _tempBuffer);
                            break;
                        case varType.DINT:
                            result = Client.DBRead(tag.DbNumber, tag.Offset.ByteOffset, 4, _tempBuffer);
                            S7.SetDIntAt(_tempBuffer, 0, (int)value);
                            result += Client.DBWrite(tag.DbNumber, tag.Offset.ByteOffset, 4, _tempBuffer);
                            break;
                        case varType.REAL:
                            result = Client.DBRead(tag.DbNumber, tag.Offset.ByteOffset, 4, _tempBuffer);
                            S7.SetRealAt(_tempBuffer, 0, (float)value);
                            result += Client.DBWrite(tag.DbNumber, tag.Offset.ByteOffset, 4, _tempBuffer);
                            break;
                    }
                }
                try
                {
                    if (result != 0)
                        throw new System.InvalidOperationException("write error");
                }
                catch { }
                finally
                {
                }
            }
            return result;
        }

        private void OnClock100msTick(Object source, System.Timers.ElapsedEventArgs e)
        {
            int result;
            lock (TimerLock)
            {
                result = ReadStatus();
            }

            InterfaceEventArgs p1 = new InterfaceEventArgs();
            p1.StatusData = STATUS;
            p1.CyclicStatusBuffer = CyclicStatusBuffer;
            if (Update_100_ms != null)
                Update_100_ms(this, p1);

            Clock_100_ms.Start();
        }

        private void OnClockWatchdogTick(Object source, System.Timers.ElapsedEventArgs e)
        {
            lock (TimerLock)
            {
                int result = -99;
                Array.Clear(WatchdogBuffer, 0, WatchdogBuffer.Length);
                switch (activeScreen)
                {
                    case 0:
                        break;
                    case 1:
                        S7.SetBitAt(ref WatchdogBuffer, 0, 3, true);
                        break;
                    case 2:
                        S7.SetBitAt(ref WatchdogBuffer, 0, 4, true);
                        break;
                    case 3:
                        S7.SetBitAt(ref WatchdogBuffer, 0, 5, true);
                        break;
                    case 4:
                        S7.SetBitAt(ref WatchdogBuffer, 0, 6, true);
                        break;
                    case 5:
                        S7.SetBitAt(ref WatchdogBuffer, 0, 7, true);
                        break;
                    case 6:
                        S7.SetBitAt(ref WatchdogBuffer, 1, 0, true);
                        break;
                    case 7:
                        S7.SetBitAt(ref WatchdogBuffer, 1, 1, true);
                        break;
                    case 8:
                        S7.SetBitAt(ref WatchdogBuffer, 1, 2, true);
                        break;
                }

                S7.SetBitAt(ref WatchdogBuffer, 0, 1, true);
                result = Client.DBWrite(49, 0, 2, WatchdogBuffer);
                if (result == 0)
                    Errorcode = Client.DBRead(51, 0, 1, WatchdogBuffer);
                else
                    OnlineMark = false;

                if (Errorcode == 0)
                {
                    OnlineMark = S7.GetBitAt(WatchdogBuffer, 0, 0);
                }
                else
                {
                    OnlineMark = false;

                }
            }

            OnlineMarkerEventArgs p = new OnlineMarkerEventArgs();
            p.OnlineMark = OnlineMark;
            if (Update_Online_Flag != null)
                Update_Online_Flag(this, p);
            if (OnlineMark)
            {
                WatchDogTimer.Start();
            }
            else
            {
                Errorcode = Errorcode + 0;
                RestartInterface();
            }
        }

    }

    public class plcTag
    {
        varType vType;
        public varType VType
        {
            get
            {
                return vType;
            }
        }

        dataType dType;
        public dataType DType
        {
            get
            {
                return dType;
            }
        }

        int dbNumber;
        public int DbNumber
        {
            get
            {
                return dbNumber;
            }
        }

        Offset offset;
        public Offset Offset
        {
            get { return offset; }
        }


        public object Value { get; set; }


        public plcTag(varType _vType, dataType _dType, int _dbNumber, Offset _offset, object _value)
        {
            vType = _vType;
            dType = _dType;
            offset = _offset;
            if (dType != dataType.DB)
            {
                dbNumber = 0;
            }
            else
            {
                dbNumber = _dbNumber;
            }
        }
        /// <summary>
        /// extract value from raw buffer
        /// </summary>
        /// <param name="buffer"> buffer length must be greater than Offset.ByteOffset+4, else do nothing </param>
        public void GetValueFromGroupBuffer(byte[] buffer)
        {
            if (buffer.Length < Offset.ByteOffset + 4)
                return;
            switch (VType)
            {
                case varType.BOOL:
                    Value = S7.GetBitAt(buffer, Offset.ByteOffset, Offset.BitOffset);
                    break;
                case varType.BYTE:
                    Value = S7.GetByteAt(buffer, Offset.ByteOffset);
                    break;
                case varType.WORD:
                    Value = S7.GetWordAt(buffer, Offset.ByteOffset);
                    break;
                case varType.DWORD:
                    Value = S7.GetDWordAt(buffer, Offset.ByteOffset);
                    break;
                case varType.INT:
                    Value = S7.GetIntAt(buffer, Offset.ByteOffset);
                    break;
                case varType.DINT:
                    Value = S7.GetDIntAt(buffer, Offset.ByteOffset);
                    break;
                case varType.REAL:
                    Value = S7.GetRealAt(buffer, Offset.ByteOffset);
                    break;
                // Added String Read
                case varType.STRING:
                    Value = S7.GetStringAt(buffer, Offset.ByteOffset);
                    break;
            }
        }
    }
    public struct Offset
    {
        short byteOffset;
        public short ByteOffset
        {
            get { return byteOffset; }
            set { byteOffset = value; }
        }

        short bitOffset;
        public short BitOffset
        {
            get { return bitOffset; }
            set { bitOffset = value; }
        }
        public Offset(short _byteOffset, short _bitOffset)
        {
            byteOffset = _byteOffset;
            bitOffset = _bitOffset;
        }
    }
    public enum varType { BOOL, BYTE, WORD, DWORD, INT, DINT, REAL, STRING }; // Enum
    public enum dataType { DB, I, Q, M, L, T };
}
