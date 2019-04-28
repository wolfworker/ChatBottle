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
    public class UserRequest : BaseRequest
    {
        public long ID { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string QQ { get; set; }
        public byte Gender { get; set; }
        public byte Status { get; set; }
        public DateTime Birthday { get; set; }
        public string Remark { get; set; }
    }

    /// <summary>
    /// 用户返回实体
    /// </summary>
    public class UserResponse
    {
        public long ID { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string QQ { get; set; }
        public byte Gender { get; set; }
        public byte Status { get; set; }
        public DateTime Birthday { get; set; }
        public string Remark { get; set; }
    }
}
