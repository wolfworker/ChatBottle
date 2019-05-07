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
using System.Text;
using System.Web.Http;

namespace Co.ChatBottle.Service.Controllers
{
    public class RequestLogController : BaseController
    {
        public RequestLogBiz requestLogBiz { get; set; } = new RequestLogBiz();

        [HttpPost]
        public HttpResponseMessage Add(RequestLogRequest request)
        {
            if (request == null)
            {
                return null;
            }

            ConvertBaseRequest(request);
            var requestInfo = new SYS_RequestLog
            {
                ID = Guid.NewGuid().ToString(),
                UserID = request.UserID,
                LogType = 1,
                LogTypeName = "打开瓶子",
                
                BussiessValue = request.BussiessValue,
                CreatedUserID = request.UserID,
                UpdateUserID = request.UserID,
                CreatedTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };

            var requestlogEntity = requestLogBiz.Add(requestInfo);

            return EntityToJson(requestlogEntity);
        }
    }
}
