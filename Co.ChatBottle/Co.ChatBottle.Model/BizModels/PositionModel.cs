using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co.ChatBottle.Model
{
    /// <summary>
    /// 用户请求实体
    /// </summary>
    public class PositionRequest : BaseRequest
    {
        public long ID { get; set; }
        public long UserID { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}
