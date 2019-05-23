using Co.ChatBottle.Business;
using Co.ChatBottle.Model;
using Co.ChatBottle.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Co.ChatBottle.Service.Controllers
{
    public class ChatRecordController : BaseController
    {
        public ChatRecordBiz chatRecordBiz { get; set; } = new ChatRecordBiz();

        [HttpPost]
        public HttpResponseMessage Add(ACT_ChatRecord request)
        {
            //保存图片
            if (request.ChatType == 1)
            {
                if (string.IsNullOrEmpty(request.ChatText))
                {
                    return ErrorToJson("未检测到图片");
                }
                var fileFullPath = "";
                try
                {
                    var fileName = DateTime.Now.Ticks + AppConst.ImgExtion;
                    var imgFolder = AppConfig.ImgChatFolder + request.BottleID + "/";
                    var directFolder = AppConfig.ImgRootPath + imgFolder;
                    fileFullPath = directFolder + fileName;
                    var success = ImageUtil.SaveImageToLocal(request.ChatText, directFolder, fileName);
                    if (success)
                    {
                        request.ChatText = AppConfig.ImgRootUrl + imgFolder + fileName;
                    }
                }
                catch (Exception ex)
                {
                    var message = $"聊天图片保存 出错 -> 错误信息：{ex.Message}, 堆栈信息：{ex.StackTrace}, 物理地址：{fileFullPath}";
                    WriteLog(message);
                    return ErrorToJson("发送图片失败");
                }
            }

            request.ID = Guid.NewGuid().ToString();
            request.UpdateTime = DateTime.Now;
            request.CreatedTime = DateTime.Now;
            request.CreatedUserID = request.SenderID;
            request.UpdateUserID = request.SenderID;

            var entity = chatRecordBiz.Add(request);
            return EntityToJson(entity);
        }

        [HttpPost]
        public HttpResponseMessage UpdateChatStatus(ChatRecordRequest request)
        {
            var bottleid = request.BottleID;
            var receiveid = request.ReceiverID;
            var updateSql = $@"  UPDATE ACT_ChatRecord SET status = 1,remark = '瓶子详情页面更新已读' WHERE bottleid = {bottleid} AND receiverid = {receiveid} AND status = 0;";
            chatRecordBiz.ExcuteSql(updateSql);

            var sql = $"SELECT top 1 *  FROM ACT_ChatRecord WHERE ReceiverID = {receiveid} AND Status = 0 ";
            var result = chatRecordBiz.QueryCustom<ACT_ChatRecord>(sql);
            return EntityToJson(result);
        }

        /// <summary>
        /// 获取瓶子的聊天记录
        /// </summary>
        /// <param name="bottleid"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage QueryChatByBottleId(long bottleid, long userid = 0)
        {
            var sql = $"SELECT * FROM ACT_ChatRecord WHERE BottleID = {bottleid} ORDER BY CreatedTime ";
            var result = chatRecordBiz.QueryCustom<ACT_ChatRecord>(sql);
            return EntityToJson(result);
        }

        /// <summary>
        /// 是否有未读消息
        /// </summary>
        /// <param name="bottleid"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage HasUnreadMessage(long userid)
        {
            var sql = $"SELECT top 1 *  FROM ACT_ChatRecord WHERE ReceiverID = {userid} AND Status = 0 ";
            var result = chatRecordBiz.QueryCustom<ACT_ChatRecord>(sql);
            return EntityToJson(result);
        }
    }
}
