using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Co.ChatBottle.WebUI.Controllers
{
    public class WelcomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}