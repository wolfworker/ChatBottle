using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co.ChatBottle.Model
{
    /// <summary>
    /// 瓶子请求实体
    /// </summary>
    public class BottleRequest : BaseRequest
    {
        public long BottleID { get; set; }
        public long ThrowUserID { get; set; } = 0;
        public long ReceiveUserID { get; set; } = 0;
        public string BottleDesc { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }

        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string AddressDetail { get; set; }
    }


    /// <summary>
    /// 我的瓶子返回实体
    /// </summary>
    public class BottleResonse
    {
        public long BottleID { get; set; }
        public long ThrowUserID { get; set; } = 0;
        public long ReceiveUserID { get; set; } = 0;
        public string BottleDesc { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string ThrowUserName { get; set; }
        public DateTime UpdateTime { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string HeaderImgUrl { get; set; }
        public byte Gender { get; set; }
        public string UpdateTimeDesc { get; set; }
    }
}
