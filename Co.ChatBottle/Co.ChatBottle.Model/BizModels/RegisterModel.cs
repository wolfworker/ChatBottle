﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co.ChatBottle.Model
{
    /// <summary>
    /// 注册请求实体
    /// </summary>
    public class RegisterRequest : BaseRequest
    {
        public string UserName { get; set; }

        public byte Gender { get; set; }
    }

    /// <summary>
    /// 注册返回实体
    /// </summary>
    public class RegisterResponse
    {
        public int ID { get; set; }
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
