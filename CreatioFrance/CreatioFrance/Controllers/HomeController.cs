using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreatioFrance.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    //public class MyClassAuthorizationAttribute : Attribute, IAuthorizationFilter
    //{
    //    public void OnAuthorization(AuthorizationContext filterContext)
    //    {
    //        if (filterContext.HttpContext.User.Identity.IsAuthenticated)
    //        {
    //            filterContext.Controller.ViewData["MyClassInstance"] = new MyClass();
    //        }
    //    }
    //}
}