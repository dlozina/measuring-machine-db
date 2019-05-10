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
        public machine1Data Machine1Data { get; set; } = new machine1Data();
        public machine2Data Machine2Data { get; set; } = new machine2Data();
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
            public plcTag A { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(10, 0), 0.0f);
            public plcTag A12{ get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(14, 0), 0.0f);
            public plcTag B { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(18, 0), 0.0f);
            public plcTag J { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(22, 0), 0.0f);
            public plcTag F1LG2 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(26, 0), 0.0f);
            public plcTag F1LG3 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(30, 0), 0.0f);
            public plcTag F2LG2 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(34, 0), 0.0f);
            public plcTag F2LG3 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(38, 0), 0.0f);
            public plcTag E { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(42, 0), 0.0f);
            public plcTag D { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(46, 0), 0.0f);
            public plcTag K { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(50, 0), 0.0f);
            public plcTag H1 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(54, 0), 0.0f);
        }

        public class measuredpos2
        {
            public plcTag C { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(58, 0), 0.0f);
            public plcTag A11 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(62, 0), 0.0f);
            public plcTag A { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(66, 0), 0.0f);
            public plcTag A12 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(70, 0), 0.0f);
            public plcTag B { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(74, 0), 0.0f);
            public plcTag J { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(78, 0), 0.0f);
            public plcTag F1LG2 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(82, 0), 0.0f);
            public plcTag F1LG3 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(86, 0), 0.0f);
            public plcTag F2LG2 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(90, 0), 0.0f);
            public plcTag F2LG3 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(94, 0), 0.0f);
            public plcTag E { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(98, 0), 0.0f);
            public plcTag D { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(102, 0), 0.0f);
            public plcTag K { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(106, 0), 0.0f);
            public plcTag H1 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(110, 0), 0.0f);
        }

        public class machine1Data
        {
            public plcTag H1 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(114, 0), 0.0f);
            public plcTag H2 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(118, 0), 0.0f);
            public plcTag H3 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(122, 0), 0.0f);
            public plcTag H4 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(126, 0), 0.0f);
            public plcTag H5 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(130, 0), 0.0f);
            public plcTag H6 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(134, 0), 0.0f);
            public plcTag H7 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(138, 0), 0.0f);
            public plcTag C { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(142, 0), 0.0f);
            public plcTag A { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(146, 0), 0.0f);
            public plcTag B { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(150, 0), 0.0f);
            public plcTag J { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(154, 0), 0.0f);
            public plcTag F { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(158, 0), 0.0f);
            public plcTag E { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(162, 0), 0.0f);
            public plcTag D { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(166, 0), 0.0f);
            public plcTag G { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(170, 0), 0.0f);
            public plcTag Kplus { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(174, 0), 0.0f);
            public plcTag Kminus { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(178, 0), 0.0f);
            public plcTag WorkpieceType { get; set; } = new plcTag(varType.INT, dataType.DB, 48, new Offset(182, 0), 0.0f);
        }
        public class machine2Data
        {
            public plcTag H1 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(184, 0), 0.0f);
            public plcTag H2 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(188, 0), 0.0f);
            public plcTag H3 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(192, 0), 0.0f);
            public plcTag H4 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(196, 0), 0.0f);
            public plcTag H5 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(200, 0), 0.0f);
            public plcTag H6 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(204, 0), 0.0f);
            public plcTag H7 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(208, 0), 0.0f);
            public plcTag C { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(212, 0), 0.0f);
            public plcTag A { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(216, 0), 0.0f);
            public plcTag B { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(220, 0), 0.0f);
            public plcTag J { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(224, 0), 0.0f);
            public plcTag F { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(228, 0), 0.0f);
            public plcTag E { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(232, 0), 0.0f);
            public plcTag D { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(236, 0), 0.0f);
            public plcTag G { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(240, 0), 0.0f);
            public plcTag Kplus { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(244, 0), 0.0f);
            public plcTag Kminus { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(248, 0), 0.0f);
            public plcTag WorkpieceType { get; set; } = new plcTag(varType.INT, dataType.DB, 48, new Offset(252, 0), 0.0f);
        }

        public class workpiecedata
        {
            public plcTag RadniNalog { get; set; } = new plcTag(varType.STRING, dataType.DB, 48, new Offset(254, 0), 0);
            public plcTag WorkOrderM1 { get; set; } = new plcTag(varType.STRING, dataType.DB, 48, new Offset(262, 0), 0);
            public plcTag WorkOrderM2 { get; set; } = new plcTag(varType.STRING, dataType.DB, 48, new Offset(270, 0), 0);
        }
    }
}
