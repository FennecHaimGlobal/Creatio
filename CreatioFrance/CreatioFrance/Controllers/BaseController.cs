using CreatioFrance.Models;
using CreatioFranceBusiness;
using CreatioFranceEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace CreatioFrance.Controllers
{

    public class BaseController: Controller
    {
        private IMetaDataManager _metaDataManager = MetaDataManager.GetInstance;

       public MembershipUser CurrentUser;

        protected MetaData MyMetadata
        {
            get
            {
                return _metaDataManager.GetMetaDataByControler(this.ToString()).Result;
            }

        }

     
        public BaseController()
        {
            try
            {
                var userFromAuthCookie = System.Threading.Thread.CurrentPrincipal;

                if (userFromAuthCookie != null && userFromAuthCookie.Identity.IsAuthenticated)
                {
                    ViewBag.UserName = userFromAuthCookie.Identity.Name;
                }
               

                ViewData["metaData"] = MyMetadata;
            }
            catch (Exception ex)
            {

            }
        }
    }
}