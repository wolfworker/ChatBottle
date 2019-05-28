using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co.ChatBottle.Model
{
    /// <summary>
    /// 用户位置请求实体
    /// </summary>
    public class PositionRequest : BaseRequest
    {
        public long ID { get; set; }
        public long UserID { get; set; }
        public long BottleID { get; set; }
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
