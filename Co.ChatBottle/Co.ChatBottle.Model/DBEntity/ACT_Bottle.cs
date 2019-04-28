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
        public long ThrowUserID { get; set; }
        public long ReceiveUserID { get; set; }
        public string BottleDesc { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public byte Status { get; set; }
        public string Remark { get; set; }
    }
}
