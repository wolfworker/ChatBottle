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
    public class ChatRecordRequest : BaseRequest
    {
        public long BottleID { get; set; } = 0;
        public long ReceiverID { get; set; } = 0;
    }
}
