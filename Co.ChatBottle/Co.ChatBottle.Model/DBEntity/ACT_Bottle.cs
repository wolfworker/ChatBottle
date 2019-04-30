using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co.ChatBottle.Model
{
    public class ACT_Bottle : CommonEntity
    {
        public long ID { get; set; }
        public long ThrowUserID { get; set; } = 0;
        public long ReceiveUserID { get; set; } = 0;
        public string BottleDesc { get; set; }
        public byte Status { get; set; }
        public string Remark { get; set; }
    }
}
