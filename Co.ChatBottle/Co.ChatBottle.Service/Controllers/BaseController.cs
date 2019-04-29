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

        public HttpResponseMessage ResponseToJson(Object obj)
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
            HttpResponseMessage result = new HttpResponseMessage
            {
                Content = new StringContent(str,
                Encoding.GetEncoding("UTF-8"),
                "application/json")
            };
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public HttpResponseMessage EntityToJson<T>(T obj, string errMsg = "") where T : class
        {
            var response = new BaseResponse<T>();

            if (obj != null)
            {
                response.ErrorCode = 0;
                response.Result = obj;
            }
            else if (!string.IsNullOrEmpty(errMsg))
            {
                response.ErrorMsg = errMsg;
            }
            return ResponseToJson(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public HttpResponseMessage ErrorToJson(string errMsg)
        {
            var response = new BaseResponse<string>();
            response.ErrorMsg = errMsg;
            return ResponseToJson(response);
        }

        public void ConvertBaseRequest(BaseRequest request)
        {
            if (request == null)
            {
                return;
            }
            if (Request != null && Request.Headers != null)
            {
                request.Host = Request.Headers.Host;
                request.UserAgent = Request.Headers.UserAgent.ToString();
            }
        }
    }
}
