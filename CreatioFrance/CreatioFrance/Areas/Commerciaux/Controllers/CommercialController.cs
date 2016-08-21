using CreatioFrance.Areas.Commerciaux.Models;
using CreatioFrance.Controllers;
using CreatioFrance.Models;
using CreatioFranceEntities.Users;
using CreatioUsersBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CreatioFrance.Areas.Commerciaux.Controllers
{
    public class CommercialController : AccountController
    {
        #region Members
        private IUsersManagment _usersManagment = UsersManagment.GetInstance;
        #endregion

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
        public async Task<ActionResult> Register(CommercialInformationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Register.Email, Email = model.Register.Email };
                var result = await UserManager.CreateAsync(user, model.Register.Password);
                if (result.Succeeded)
                {
                    //******************* Add Role To User  **************
                    await UserManager.AddToRoleAsync(user.Id, eRolesInfo.CreatioCommerciaux.ToString());
                    //****************************************************


                    model.Users.Informations.Email = model.Register.Email;
                    model.Users.Informations.Id = user.Id;
                    await _usersManagment.SaveUserInformation(model.Users.Informations);

                    model.Commercials.Id = user.Id;
                    await _usersManagment.SaveCommercialInformation(model.Commercials);

                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        #endregion
    }
}