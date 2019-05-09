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
    public class ChatRecordController : BaseController
    {
        public ChatRecordBiz chatRecordBiz { get; set; } = new ChatRecordBiz();

        [HttpPost]
        public HttpResponseMessage UpdateChatStatus(ChatRecordRequest request)
        {
            var bottleid = request.BottleID;
            var receiveid = request.ReceiverID;
            var updateSql = $@"  UPDATE ACT_ChatRecord SET status = 1,remark = '瓶子详情页面更新已读' WHERE bottleid = {bottleid} AND receiverid = {receiveid} AND status = 0;";
            chatRecordBiz.ExcuteSql(updateSql);
            return EntityToJson(string.Empty);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bottleid"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage QueryChatByBottleId(long bottleid)
        {
            var sql = $"SELECT * FROM ACT_ChatRecord WHERE BottleID = {bottleid} ORDER BY CreatedTime ";
            var result = chatRecordBiz.QueryCustom<ACT_ChatRecord>(sql);
            return EntityToJson(result);
        }
    }
}
