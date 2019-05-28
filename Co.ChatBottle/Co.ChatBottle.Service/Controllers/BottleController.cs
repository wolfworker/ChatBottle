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
            if (request == null || string.IsNullOrEmpty(request.BottleDesc))
            {
                return ErrorToJson("瓶子内容为空，请重试！");
            }

            //ConvertBaseRequest(request);

            var bottleInfo = new ACT_Bottle
            {
                ThrowUserID = request.ThrowUserID,
                BottleDesc = request.BottleDesc,
                CreatedUserID = request.ThrowUserID,
                UpdateUserID = request.ThrowUserID,
            };

            var bottleEntity = bottleBiz.Add(bottleInfo);

            //异步记录瓶子聊天记录和位置
            Task.Run(() =>
            {
                //插入第一条聊天记录
                var chatInfo = new ACT_ChatRecord
                {
                    ID = Guid.NewGuid().ToString(),
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
                commonBiz.WriteRequestLog(request.ThrowUserID, (int)EnumModel.LogType.Bottle_Throw, "", "扔瓶子失败");
                return ErrorToJson("瓶子没扔出去，重新试下吧！");
            }

            commonBiz.WriteRequestLog(request.ThrowUserID, (int)EnumModel.LogType.Bottle_Throw, bottleEntity.ID.ToString());
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
            var sql = $@"
                        SELECT  ( SELECT TOP 1 position.City FROM ACT_User_Position position
                                  WHERE     position.UserID = temp.BottleUserID AND position.CreatedTime > CONVERT(VARCHAR(10), GETDATE(), 120)
                                  ORDER BY  position.CreatedTime DESC ) AS City ,
                                ( SELECT TOP 1 position.District FROM ACT_User_Position position
                                  WHERE     position.UserID = temp.BottleUserID AND position.CreatedTime > CONVERT(VARCHAR(10), GETDATE(), 120)
                                  ORDER BY  position.CreatedTime DESC ) AS District , temp.*
                        FROM    (
                                SELECT bottle.ID as BottleID,bottle.ThrowUserID,bottle.ReceiveUserID,'' AS UpdateTimeDesc,
                                        CASE WHEN bottle.ReceiveUserID = {userId}  THEN  throwusers.UserName WHEN bottle.ThrowUserID = {userId} AND bottle.ReceiveUserID>0 THEN receiveusers.UserName ELSE '我扔的瓶子' END AS BottleUserName ,
		                                CASE WHEN bottle.ReceiveUserID = {userId} OR (bottle.ThrowUserID = {userId} AND bottle.ReceiveUserID = 0)  THEN  throwusers.HeaderImgUrl else receiveusers.HeaderImgUrl END  AS BottleHeaderUrl,
		                                CASE WHEN bottle.ReceiveUserID = {userId} OR (bottle.ThrowUserID = {userId} AND bottle.ReceiveUserID = 0)  THEN  throwusers.Gender else receiveusers.Gender END  AS BottleGender,
                                        CASE WHEN bottle.ReceiveUserID = {userId} OR ( bottle.ThrowUserID = {userId} AND bottle.ReceiveUserID = 0 ) THEN throwusers.ID ELSE receiveusers.ID END AS BottleUserID ,
                                        (SELECT TOP 1 (CASE WHEN chat.ReceiverID != {userId} THEN 1 ELSE chat.Status END) FROM ACT_ChatRecord chat WHERE chat.BottleID = bottle.ID ORDER BY UpdateTime DESC )AS ReadStatus,
								        --(SELECT TOP 1 chat.ChatText FROM ACT_ChatRecord chat WHERE chat.BottleID = bottle.ID ORDER BY UpdateTime DESC )AS BottleDesc,
                                        --(SELECT TOP 1 chat.UpdateTime FROM ACT_ChatRecord chat WHERE chat.BottleID = bottle.ID ORDER BY UpdateTime DESC )AS RealUpdateTime
                                        bottle.BottleDesc AS BottleDesc,
                                        bottle.UpdateTime AS RealUpdateTime
                                 FROM    ACT_Bottle bottle LEFT JOIN dbo.ACT_User throwusers ON throwusers.ID = bottle.ThrowUserID
                                                           LEFT JOIN dbo.ACT_User receiveusers ON receiveusers.ID = bottle.ReceiveUserID
                                 WHERE   bottle.ThrowUserID = {userId} OR bottle.ReceiveUserID = {userId} ) temp ORDER BY temp.RealUpdateTime DESC; ";
            var result = bottleBiz.QueryCustom<BottleResonse>(sql);
            
            //头像默认值
            if(result!=null&& result.Any())
            {
                result.ForEach(p =>
                {
                    if (string.IsNullOrEmpty(p.BottleHeaderUrl))
                    {
                        p.BottleHeaderUrl = AppConfig.ImgDefaultUrl;
                    }

                    if (p.RealUpdateTime > DateTime.Today)
                    {
                        p.UpdateTimeDesc = p.RealUpdateTime.ToString("HH:mm");
                    }
                    else if (p.RealUpdateTime > DateTime.Today.AddDays(-1))
                    {
                        p.UpdateTimeDesc = "昨天";
                    }
                    else if (p.RealUpdateTime < DateTime.Today.AddYears(-1))
                    {
                        p.UpdateTimeDesc = p.RealUpdateTime.ToString("yy-MM-dd");
                    }
                    else
                    {
                        p.UpdateTimeDesc = p.RealUpdateTime.ToString("MM-dd");
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
            //先捞异性的瓶子
            var sql = $@"SELECT TOP 1 bottle.* FROM ACT_Bottle bottle INNER JOIN dbo.ACT_User throwusers ON throwusers.ID = bottle.ThrowUserID
                                LEFT JOIN dbo.ACT_User receiveusers ON receiveusers.ID = {userId} WHERE throwusers.Gender != receiveusers.Gender
                                AND bottle.ReceiveUserID = 0 AND bottle.ThrowUserID != {userId} ORDER BY bottle.UpdateTime DESC; ";
            var result = bottleBiz.QueryCustom<ACT_Bottle>(sql);

            //异性瓶子没有，再捞全部瓶子
            if (result == null || !result.Any())
            {
                sql = $"SELECT TOP 1 * FROM ACT_Bottle WHERE ThrowUserID != {userId} AND  ReceiveUserID = 0 ORDER BY UpdateTime DESC ";
                result = bottleBiz.QueryCustom<ACT_Bottle>(sql);
            }
               
            if (result != null && result.Any())
            {
                var bottleInfo = result.FirstOrDefault();
                bottleInfo.ReceiveUserID = userId;
                //bottleInfo.UpdateTime = DateTime.Now;//不更新时间，否则会导致 页面排序出问题，只有真实操作瓶子才更新时间
                if (bottleBiz.Update(bottleInfo))
                {
                    //更新聊天记录的receiveid
                    var updateSql = $@"  UPDATE ACT_ChatRecord SET receiverid = {userId},UpdateTime = getdate() WHERE bottleid = {bottleInfo.ID} ;";
                    bottleBiz.ExcuteSql(updateSql);

                    commonBiz.WriteRequestLog(userId, (int)EnumModel.LogType.Bottle_Pick, bottleInfo.ID.ToString(), "捞到一个瓶子");
                    return EntityToJson(bottleInfo);
                }
            }

            commonBiz.WriteRequestLog(userId, (int)EnumModel.LogType.Bottle_Pick, "","没捞到瓶子");
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
