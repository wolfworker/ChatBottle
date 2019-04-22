using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co.ChatBottle.Model
{
    public enum LogLevel
    {
        [Description("错误")]
        Error = 1,

        [Description("警告")]
        Worning = 2,

        [Description("正常")]
        Normal = 3
    }
}
