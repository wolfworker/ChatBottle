using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co.ChatBottle.Model
{
    public class SYS_RequestLog : CommonEntity
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public byte LogType { get; set; }
        public string LogTypeName { get; set; }
        public string BussiessValue { get; set; }
        public string Remark { get; set; }
    }
}
