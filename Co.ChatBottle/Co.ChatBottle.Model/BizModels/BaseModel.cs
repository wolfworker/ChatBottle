using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co.ChatBottle.Model
{
    /// <summary>
    /// 返回实体基类
    /// </summary>
    public class BaseResponse<T>
    {
        /// <summary>
        /// 0：成功  -1：失败
        /// </summary>
        public int ErrorCode { get; set; } = -1;

        /// <summary>
        /// 失败描述
        /// </summary>
        public string ErrorMsg { get; set; } = string.Empty;

        /// <summary>
        /// 业务实体
        /// </summary>
        public T Result { get; set; }
    }


    /// <summary>
    /// 请求实体基类
    /// </summary>
    public class BaseRequest
    {
        public string Host { get; set; }

        public string UserAgent { get; set; }
    }
}
