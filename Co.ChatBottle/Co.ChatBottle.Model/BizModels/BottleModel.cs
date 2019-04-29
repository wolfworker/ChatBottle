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
        public long ID { get; set; }
        public long ThrowUserID { get; set; } = 0;
        public long ReceiveUserID { get; set; } = 0;
        public string BottleDesc { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}
