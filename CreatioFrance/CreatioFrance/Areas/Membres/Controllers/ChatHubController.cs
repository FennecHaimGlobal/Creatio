using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreatioFrance.Areas.Membres.Controllers
{
    public class ChatHubController : MembersBaseController
    {
        // GET: Membres/ChatHub
        public ActionResult Index()
        {
            return View();
        }
    }
}