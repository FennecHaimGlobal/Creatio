using CreatioFrance.Entities;
using CreatioFrance.Models;
using CreatioFranceBusiness;
using CreatioFranceEntities;
using CreatioFranceEntities.Users;
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
    public class BaseController : AccountController
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

        public List<ActionLink> MyMenu { get; set; }

        public BaseController()
        {
            try
            {
                var userFromAuthCookie = System.Threading.Thread.CurrentPrincipal;

                if (userFromAuthCookie != null && userFromAuthCookie.Identity.IsAuthenticated)
                {
                    string email = userFromAuthCookie.Identity.Name;

                    string name = email.Remove(email.IndexOf('@'), email.Length - email.IndexOf('@'));

                    ViewBag.Name = name;
                    ViewBag.EMail = email;

                    MyMenu = new List<ActionLink>();

                    MyMenu.Add(new ActionLink() { linkText = "Acceuil", actionName = "Index", controllerName = "Home", area = "" });
                    MyMenu.Add(new ActionLink() { linkText = "A Propos", actionName = "About", controllerName = "Home", area = "" });
                    MyMenu.Add(new ActionLink() { linkText = "Contactez-nous", actionName = "Contact", controllerName = "Home", area = "" });

                    var profilClaims = ((ClaimsIdentity)(userFromAuthCookie.Identity)).Claims
                                                                                      .Select(x=>x.Value)
                                                                                      .ToList();

                    if (profilClaims.Contains(eRolesInfo.CreatioAdmin.ToString()))
                    {
                        MyMenu.Add(new ActionLink() { linkText = "Inscription des Avocats", actionName = "Register", controllerName = "Account", area = "Avocats" });
                        MyMenu.Add(new ActionLink() { linkText = "Inscription des Commerciaux", actionName = "Register", controllerName = "Account", area = "Commerciaux" });
                    }
                    else if (profilClaims.Contains(eRolesInfo.CreatioAvocats.ToString()))
                    {

                    }
                    else if (profilClaims.Contains(eRolesInfo.CreatioCommerciaux.ToString()))
                    {

                    }
                    else if (profilClaims.Contains(eRolesInfo.CreatioMembers.ToString()))
                    {
                    }
                }

                ViewData["menuData"] = MyMenu;
                ViewData["metaData"] = MyMetadata;
            }
            catch (Exception ex)
            {

            }
        }
    }
}