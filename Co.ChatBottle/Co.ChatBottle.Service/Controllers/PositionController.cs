using Co.ChatBottle.Business;
using Co.ChatBottle.Model;
using Co.ChatBottle.Utility;
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
    public class PositionController : BaseController
    {
        public PositionBiz positionBiz { get; set; } = new PositionBiz();

        [HttpPost]
        public HttpResponseMessage Add(PositionRequest request)
        {
            if (request == null)
            {
                return null;
            }

            ConvertBaseRequest(request);
            var positionInfo = new ACT_User_Position
            {
                UserID = request.UserID,
                Longitude = request.Longitude,
                Latitude = request.Latitude
            };

            var positionEntity = positionBiz.Add(positionInfo);

            return EntityToJson(positionEntity);
        }

        [HttpGet]
        public HttpResponseMessage QueryAll()
        {
            var result = positionBiz.QueryAll<ACT_User_Position>();
            return EntityToJson(result);
        }

        [HttpGet]
        // GET: api/UserApi/5
        public HttpResponseMessage QueryByUserId(long userId)
        {
            var sql = $"SELECT TOP 1 * FROM ACT_User_Position WHERE USERID = {userId} ORDER BY CreatedTime DESC ";
            var result = positionBiz.QueryCustom<ACT_User_Position>(sql);
            ACT_User_Position positionEntity = null;
            if (result != null && result.Any())
            {
                positionEntity = result.FirstOrDefault();
            }
            return EntityToJson(positionEntity);
        }
    }
}
