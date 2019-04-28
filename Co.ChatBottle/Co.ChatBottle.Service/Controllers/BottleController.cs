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
    public class BottleController : BaseController
    {
        public BottleBiz bottleBiz { get; set; } = new BottleBiz();

        [HttpPost]
        public HttpResponseMessage Add(BottleRequest request)
        {
            if (request == null)
            {
                return null;
            }
            ConvertBaseRequest(request);

            var userInfo = new ACT_Bottle
            {
                ThrowUserID = request.ThrowUserID,
                ReceiveUserID = request.ReceiveUserID,
                BottleDesc = request.BottleDesc,
                Longitude = request.Longitude,
                Latitude = request.Latitude,
                CreatedUserID = request.ThrowUserID,
                UpdateUserID = request.ThrowUserID
            };

            var bottleEntity = bottleBiz.Add(userInfo);
            return EntityToJson(bottleEntity);
        }
        
        [HttpGet]
        public HttpResponseMessage QueryAll()
        {
            var result = bottleBiz.QueryAll<ACT_Bottle>();
            return EntityToJson(result);
        }

        [HttpGet]
        // GET: api/UserApi/5
        public HttpResponseMessage QueryById(long id)
        {
            var result = bottleBiz.Query<ACT_Bottle>(id);
            return EntityToJson(result);
        }

        [HttpGet]
        // GET: api/UserApi/5
        public HttpResponseMessage QueryByUserId(long userId)
        {
            var sql = $"SELECT TOP 1 * FROM ACT_Bottle WHERE ThrowUserID = {userId} OR ReceiveUserID = {userId} ORDER BY UpdateTime DESC ";
            var result = bottleBiz.QueryCustom<ACT_Bottle>(sql);
            return EntityToJson(result);
        }
    }
}
