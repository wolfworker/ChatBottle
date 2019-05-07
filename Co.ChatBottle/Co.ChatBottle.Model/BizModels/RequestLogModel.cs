using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co.ChatBottle.Model
{
    /// <summary>
    /// 系统日志 请求实体
    /// </summary>
    public class RequestLogRequest : BaseRequest
    {
        public string ID { get; set; }
        public long UserID { get; set; }
        public byte LogType { get; set; }
        public string LogTypeName { get; set; }
        public string BussiessValue { get; set; }
        public string Remark { get; set; }
    }
}
