using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CreatioUsersBusiness;
using CreatioUsersData;
using CreatioFrance.Models;

namespace CreatioFrance.Controllers
{
    public class CallbackController : BaseController
    {
        #region Members
        private IUsersManagment _usersManagment = UsersManagment.GetInstance;
        #endregion

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> SaveCallback(CallbackViewModel model)
        {
            ViewBag.Message = "Veuillez patienter...";
            try
            {
                if (ModelState.IsValid)
                {
                    var callback = new Callback()
                    {
                        Telephone = model.PhoneNumber,
                        Date = DateTime.Now
                    };
                    await _usersManagment.SaveCallback(callback);

                    ViewBag.Message = "Merci, nous allons vous contacter bientôt";
                }
            }
            catch (ManyRequestsException ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View();
        }
    }

}