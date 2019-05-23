using Co.ChatBottle.Business;
using Co.ChatBottle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Co.ChatBottle.WebUI.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index(int sourceFlag = -1, long userid = 0)
        {
            if (userid > 0)
            {
                UserInfo = new UserBiz().Query<ACT_User>(userid);
            }
            ServiceBiz.WriteRequestLog(UserInfo.ID, (int)EnumModel.LogType.Sys_OpenIndex);
            ViewBag.sourceFlag = sourceFlag;
            return View(); 
        }

        public ActionResult MyBottle()
        {
            ServiceBiz.WriteRequestLog(UserInfo.ID, (int)EnumModel.LogType.Bottle_My);
            return View();
        }

        public ActionResult PickBottle()
        {
            ServiceBiz.WriteRequestLog(UserInfo.ID, (int)EnumModel.LogType.Bottle_OpenPickPage);
            return View();
        }

        public ActionResult ThrowBottle()
        {
            ServiceBiz.WriteRequestLog(UserInfo.ID, (int)EnumModel.LogType.Bottle_OpenThrowPage);
            return View();
        }

        public ActionResult ProfileInfo()
        {
            ServiceBiz.WriteRequestLog(UserInfo.ID, (int)EnumModel.LogType.Profile_SelfPage);
            return View();
        }

        public ActionResult ProfileInfoView(int userid = 0)
        {
            ServiceBiz.WriteRequestLog(UserInfo.ID, (int)EnumModel.LogType.Profile_FriendPage, userid.ToString());
            ViewBag.userid = userid;
            return View();
        }

        public ActionResult Setting()
        {
            ServiceBiz.WriteRequestLog(UserInfo.ID, (int)EnumModel.LogType.Sys_OpenSetting);
            return View();
        }

        public ActionResult BottleDetail(long bottleId)
        {
            ServiceBiz.WriteRequestLog(UserInfo.ID, (int)EnumModel.LogType.Bottle_OpenDetail,bottleId.ToString());
            ViewBag.bottleId = bottleId;
            return View();
        }

        public JsonResult SetUserSession(long userid)
        {
            if (userid > 0)
            {
                UserInfo = new UserBiz().Query<ACT_User>(userid);
            }
            return new JsonResult();
        }
    }
}