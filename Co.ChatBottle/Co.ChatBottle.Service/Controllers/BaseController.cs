using Co.ChatBottle.Business;
using Co.ChatBottle.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace Co.ChatBottle.Service.Controllers
{
    public class BaseController : ApiController
    {
        public CommonBiz commonBiz { get; set; } = new CommonBiz();

        public void WriteLog(string logContent, byte level = 1)
        {
            try
            {
                var debugLog = new SYS_DebugLog
                {
                    ID = Guid.NewGuid().ToString(),
                    LogLevel = level,
                    LogContent = logContent
                };
                commonBiz.Add(debugLog);
            }
            catch (Exception ex)
            {
                //log error
            }
        }

        public HttpResponseMessage ResponseToJson(Object obj,string jsonp="")
        {
            String str;
            if (obj is String || obj is Char)
            {
                str = obj.ToString();
            }
            else
            {
                str = JsonConvert.SerializeObject(obj);
            }
            if (!string.IsNullOrEmpty(jsonp))
            {
                str = $"{jsonp}({str})";
            }
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(str, Encoding.GetEncoding("UTF-8"), "application/json") };
            return result;
        }

        public void ConvertBaseRequest(BaseRequest request)
        {
            if (Request != null && Request.Headers != null)
            {
                request.Host = Request.Headers.Host;
                request.UserAgent = Request.Headers.UserAgent.ToString();
            }
        }
    }
}
