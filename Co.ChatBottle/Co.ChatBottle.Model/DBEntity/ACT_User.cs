using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co.ChatBottle.Model
{
    public class ACT_User : CommonEntity
    {
        public long ID { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string QQ { get; set; }
        public string PassChar { get; set; }
        public byte Gender { get; set; }
        public string HeaderImgUrl { get; set; }
        public DateTime Birthday { get; set; } = new DateTime(1990, 01, 01);
        public byte Status { get; set; }
        public string Remark { get; set; }
    }
}
