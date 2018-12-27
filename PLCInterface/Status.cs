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
        public data Data { get; set; } = new data();
        public measured Measured { get; set; } = new measured();

        // PLC DB Classes
        public class data
        {
            public plcTag Record { get; set; } = new plcTag(varType.BOOL, dataType.DB, 48, new Offset(0, 0), false);
        }

        public class measured
        {
            public plcTag C { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(2, 0), 0.0f);
            public plcTag A11 { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(6, 0), 0.0f);
            public plcTag A12{ get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(10, 0), 0.0f);
            public plcTag B { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(14, 0), 0.0f);
            public plcTag F { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(18, 0), 0.0f);
            public plcTag E { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(22, 0), 0.0f);
            public plcTag D { get; set; } = new plcTag(varType.REAL, dataType.DB, 48, new Offset(26, 0), 0.0f);
            public plcTag RadniNalog { get; set; } = new plcTag(varType.STRING, dataType.DB, 48, new Offset(30, 0), 0);
        }
    }
}
