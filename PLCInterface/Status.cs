using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCInterface
{
    public class Status
    {
        // Instance
        public savedata Savedata { get; set; } = new savedata();
        public measuredpos1 MeasuredPos1 { get; set; } = new measuredpos1();
        public measuredpos2 MeasuredPos2 { get; set; } = new measuredpos2();
        public workpiecedata Workpiecedata { get; set; } = new workpiecedata();
        
        // PLC DB Classes
        public class savedata
        {
            public plcTag M1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 48, new Offset(0, 0), false);
            public plcTag M2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 48, new Offset(0, 1), false);
        }

        public class measuredpos1
        {
            public plcTag C { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(2, 0), 0.0f);
            public plcTag A11 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(6, 0), 0.0f);
            public plcTag A12{ get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(10, 0), 0.0f);
            public plcTag B { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(14, 0), 0.0f);
            public plcTag F1LG2 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(18, 0), 0.0f);
            public plcTag F1LG3 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(22, 0), 0.0f);
            public plcTag F2LG2 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(26, 0), 0.0f);
            public plcTag F2LG3 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(30, 0), 0.0f);
            public plcTag E { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(34, 0), 0.0f);
            public plcTag D { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(38, 0), 0.0f);
            public plcTag K { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(42, 0), 0.0f);
            public plcTag H1 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(46, 0), 0.0f);
        }

        public class measuredpos2
        {
            public plcTag C { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(50, 0), 0.0f);
            public plcTag A11 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(54, 0), 0.0f);
            public plcTag A12 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(58, 0), 0.0f);
            public plcTag B { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(62, 0), 0.0f);
            public plcTag F1LG2 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(66, 0), 0.0f);
            public plcTag F1LG3 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(70, 0), 0.0f);
            public plcTag F2LG2 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(74, 0), 0.0f);
            public plcTag F2LG3 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(78, 0), 0.0f);
            public plcTag E { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(82, 0), 0.0f);
            public plcTag D { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(86, 0), 0.0f);
            public plcTag K { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(90, 0), 0.0f);
            public plcTag H1 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(94, 0), 0.0f);
        }

        public class workpiecedata
        {
            public plcTag RadniNalog { get; set; } = new plcTag(varType.STRING, dataType.DB, 48, new Offset(98, 0), 0);
        }
    }
}
