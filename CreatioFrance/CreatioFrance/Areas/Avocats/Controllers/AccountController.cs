using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreatioFrance.Areas.Avocats.Controllers
{
    public class AccountController : Controller
    {
        // GET: Avocats/Account
        public ActionResult Index()
        {
            return View();
        }

        // GET: Avocats/Account/Register
        public ActionResult Register()
        {
            return View();
        }
    }
}