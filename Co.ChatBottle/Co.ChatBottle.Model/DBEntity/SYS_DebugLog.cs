using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co.ChatBottle.Model
{
    public class SYS_DebugLog : CommonEntity
    {
        public string ID { get; set; }

        /// <summary>
        /// 日志等级   1：错误  2：警告  3：正常
        /// </summary>
        public byte LogLevel { get; set; }

        public string LogContent { get; set; }
        public string Remark { get; set; }
    }
}
