using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co.ChatBottle.Model
{
    public class ACT_User_Position : CommonEntity
    {
        public int ID { get; set; }
        public long UserID { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}
