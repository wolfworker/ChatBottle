using Co.ChatBottle.DataAccess;
using Co.ChatBottle.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co.ChatBottle.Business
{
    public class ChatRecordBiz : CommonBiz
    {
        //add by brucezhang,2019-05-09,聊天时不用更新瓶子预览文案，页面的预览文案从聊天记录中直接取
        public ACT_ChatRecord Add(ACT_ChatRecord request)
        {
            try
            {
                var entity = commonDal.Add(request);
                if (entity != null && !string.IsNullOrEmpty(entity.ID))
                {
                    //聊天记录新增一条，对应瓶子的聊天预览文案也要更新
                    var bittleBiz = new BottleBiz();
                    var bottle = bittleBiz.Query<ACT_Bottle>(request.BottleID);
                    bottle.BottleDesc = request.ChatText;
                    bottle.UpdateTime = DateTime.Now;
                    bottle.UpdateUserID = request.SenderID;
                    new BottleBiz().Update(bottle);
                }

                return entity;
            }
            catch (Exception ex)
            {
                var message = $"ChatRecordBiz Add 出错 -> 错误信息：{ex.Message}, 堆栈信息：{ex.StackTrace}, 请求参数：{JsonConvert.SerializeObject(request)}";
                WriteLog(message);
                return null;
            }
        }
    }
}
