using Co.ChatBottle.Business;
using Co.ChatBottle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Co.ChatBottle.WebUI.Controllers
{
    public class BaseController : Controller
    {
        public ACT_User UserInfo
        {
            get
            {
                var userinfo = new ACT_User();
                try
                {
                    if (Session["userinfo"] != null)
                    {
                        userinfo = (ACT_User)Session["userinfo"];
                    }
                }
                catch (Exception ex)
                {
                    //log
                }
                return userinfo;
            }
            set
            {
                Session["userinfo"] = value;
                Session.Timeout = 24 * 60;//session有效期 1天
            }
        }

        public CommonBiz ServiceBiz { get; set; } = new CommonBiz();
    }
}