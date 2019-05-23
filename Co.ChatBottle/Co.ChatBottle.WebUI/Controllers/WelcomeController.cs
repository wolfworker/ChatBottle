using Co.ChatBottle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Co.ChatBottle.WebUI.Controllers
{
    public class WelcomeController : BaseController
    {
        public ActionResult Index(int flag = 0)
        {
            //清除后台用户数据
            if (flag == 1 && UserInfo != null)
            {
                ServiceBiz.WriteRequestLog(UserInfo.ID, (int)EnumModel.LogType.Sys_Quit);
            }

            UserInfo = null;
            ServiceBiz.WriteRequestLog(0, (int)EnumModel.LogType.Welcome_OpenPage);
            
            return View();
        }
    }
}