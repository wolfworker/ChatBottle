using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Co.ChatBottle.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int sourceFlag = -1)
        {
            ViewBag.resource = sourceFlag;
            return View();
        }

        public ActionResult MyBottle()
        {
            return View();
        }

        public ActionResult PickBottle()
        {
            return View();
        }

        public ActionResult ThrowBottle()
        {
            return View();
        }

        public ActionResult ProfileSetting()
        {
            return View();
        }

        public ActionResult BottleDetail()
        {
            return View();
        }
    }
}