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
using System.Threading.Tasks;
using System.Web.Http;

namespace Co.ChatBottle.Service.Controllers
{
    public class BottleController : BaseController
    {
        public BottleBiz bottleBiz { get; set; } = new BottleBiz();

        [HttpPost]
        public HttpResponseMessage ThrowBottle(BottleRequest request)
        {
            if (request == null)
            {
                return null;
            }
            ConvertBaseRequest(request);

            var bottleInfo = new ACT_Bottle
            {
                ThrowUserID = request.ThrowUserID,
                BottleDesc = request.BottleDesc,
                CreatedUserID = request.ThrowUserID,
                UpdateUserID = request.ThrowUserID,
            };

            var bottleEntity = bottleBiz.Add(bottleInfo);

            //插入第一条聊天记录
            var chatInfo = new ACT_ChatRecord
            {
                BottleID = bottleEntity.ID,
                ChatText = request.BottleDesc,
                SenderID = request.ThrowUserID,
                Remark = "扔瓶子时同步聊天记录",
                CreatedTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                CreatedUserID = request.ThrowUserID,
                UpdateUserID = request.ThrowUserID,
            };

            new ChatRecordBiz().Add(chatInfo);

            //异步记录瓶子位置
            Task.Run(() =>
            {
                var positionInfo = new ACT_User_Position
                {
                    UserID = request.ThrowUserID,
                    BottleID = bottleEntity.ID,
                    Longitude = request.Longitude,
                    Latitude = request.Latitude,
                    CreatedUserID = request.ThrowUserID,
                    UpdateUserID = request.ThrowUserID,
                    Province = request.Province,
                    City = request.City,
                    District = request.District,
                    Street = request.Street,
                    StreetNumber = request.StreetNumber,
                    AddressDetail = request.AddressDetail,
                };

                new PositionBiz().Add(positionInfo);
            });

            if (bottleEntity == null)
            {
                return ErrorToJson("瓶子没扔出去，重新试下吧！");
            }
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

        /// <summary>
        /// 获取我的瓶子（包含捡的和扔的）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        // GET: api/UserApi/5
        public HttpResponseMessage QueryByUserId(long userId)
        {
            var sql = $@"SELECT bottle.ID as BottleID,bottle.BottleDesc,bottle.UpdateTime,users.UserName AS ThrowUserName,users.HeaderImgUrl,users.Gender,
                                position.City,position.District,position.Longitude,position.Latitude,bottle.ThrowUserID,bottle.ReceiveUserID,'' AS UpdateTimeDesc
                         FROM    ACT_Bottle bottle INNER JOIN dbo.ACT_User users ON users.ID = bottle.ThrowUserID
                                                    INNER JOIN ACT_User_Position position on position.BottleID = bottle.ID
                         WHERE   bottle.ThrowUserID = {userId} OR bottle.ReceiveUserID = {userId} ORDER BY bottle.UpdateTime DESC; ";
            var result = bottleBiz.QueryCustom<BottleResonse>(sql);
            
            //头像默认值
            if(result!=null&& result.Any())
            {
                result.ForEach(p =>
                {
                    if (string.IsNullOrEmpty(p.HeaderImgUrl))
                    {
                        p.HeaderImgUrl = System.Configuration.ConfigurationManager.AppSettings.Get("DefaultHeaderUrl");
                    }
                    if (p.UpdateTime > DateTime.Today)
                    {
                        p.UpdateTimeDesc = p.UpdateTime.ToString("hh:mm");
                    }
                    else if (p.UpdateTime > DateTime.Today.AddDays(-1))
                    {
                        p.UpdateTimeDesc = "昨天";
                    }
                    else if (p.UpdateTime < DateTime.Today.AddYears(-1))
                    {
                        p.UpdateTimeDesc = p.UpdateTime.ToString("yy-MM-dd");
                    }
                    else
                    {
                        p.UpdateTimeDesc = p.UpdateTime.ToString("MM-dd");
                    }
                });
            }
            return EntityToJson(result);
        }


        /// <summary>
        /// 捡瓶子
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        // GET: api/UserApi/5
        public HttpResponseMessage PickBottles(long userId)
        {
            var sql = $"SELECT TOP 1 * FROM ACT_Bottle WHERE ThrowUserID != {userId} AND ReceiveUserID = 0 ORDER BY UpdateTime DESC ";
            var result = bottleBiz.QueryCustom<ACT_Bottle>(sql);
            if (result != null && result.Any())
            {
                var bottleInfo = result.FirstOrDefault();
                bottleInfo.ReceiveUserID = userId;
                //bottleInfo.UpdateTime = DateTime.Now;//不更新时间，否则会导致 页面排序出问题，只有真实操作瓶子才更新时间
                if (bottleBiz.Update(bottleInfo))
                {
                    //更新聊天记录的receiveid
                    var updateSql = $@"  UPDATE ACT_ChatRecord SET receiverid = {userId} WHERE bottleid = {bottleInfo.ID} ;";
                    bottleBiz.ExcuteSql(updateSql);

                    return EntityToJson(bottleInfo);
                }
            }
            return ErrorToJson("瓶子太重，捞不上来啦，重新试试吧！");
        }

        [HttpPost]
        public HttpResponseMessage DeleteBottle(BottleRequest request)
        {
            if (bottleBiz.Delete<ACT_Bottle>(request.BottleID))
            {
                return EntityToJson(string.Empty);
            }
            else
            {
                return ErrorToJson("更新失败");
            }
        }
    }
}
