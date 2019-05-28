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
        public long UserID { get; set; } = 0;
        public long BottleID { get; set; } = 0;
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string IP { get; set; }


        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string AddressDetail { get; set; }
    }
}
