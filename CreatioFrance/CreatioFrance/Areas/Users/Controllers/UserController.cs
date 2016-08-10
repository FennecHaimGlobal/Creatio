using AreaModels = CreatioFrance.Areas.Users.Models;
using CreatioFrance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CreatioFrance.Controllers;
using System.Web.Security;
using CreatioFranceEntities.Users;
using CreatioUsersBusiness;

namespace CreatioFrance.Areas.Users.Controllers
{
    public class UserController : AccountController
    {
        #region Members
        private IUsersManagment _usersManagment = UsersManagment.GetInstance;
        #endregion

        // GET: Users/User
        public ActionResult Index()
        {
            return View();
        }

        #region Public Methods
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }


        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(AreaModels.UsersViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Register.Email, Email = model.Register.Email };
                var result = await UserManager.CreateAsync(user, model.Register.Password);
                if (result.Succeeded)
                {
                    //******************* Add Role To User  **************
                    await _usersManagment.AddUserToRole(user.Id, eRolesInfo.CreatioMembers.ToString());
                    //****************************************************

                    model.Users.Informations.Email = model.Register.Email;
                    model.Users.Informations.Id = user.Id;
                    await _usersManagment.SaveUserInformation(model.Users.Informations);

                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code, area = "" }, protocol: Request.Url.Scheme);
                    await UserManager.SendEmailAsync(user.Id, "Confirm your account", callbackUrl);

                    return RedirectToAction("Index", "User");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        #endregion
    }
}