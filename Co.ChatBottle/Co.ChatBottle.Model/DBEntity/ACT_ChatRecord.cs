using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co.ChatBottle.Model
{
    public class ACT_ChatRecord : CommonEntity
    {
        public string ID { get; set; }
        public long BottleID { get; set; } = 0;
        public long SenderID { get; set; } = 0;
        public long ReceiverID { get; set; } = 0;
        public string ChatText { get; set; }
        public byte ChatType { get; set; } = 0;
        public byte Status { get; set; }
        public string Remark { get; set; }
    }
}
