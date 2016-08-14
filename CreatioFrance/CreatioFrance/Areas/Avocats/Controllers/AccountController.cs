using CreatioFrance.Areas.Avocats.Models;
using CreatioFrance.Models;
using CreatioFranceEntities.Users;
using CreatioUsersBusiness;
using CreatioUtils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace CreatioFrance.Areas.Avocats.Controllers
{
    public class AccountController : CreatioFrance.Controllers.AccountController
    {
        #region Members
        private IUsersManagment _usersManagment = UsersManagment.GetInstance;
        #endregion

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

        // Post: Avocats/Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(AvocatInformationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Register.Email, Email = model.Register.Email };
                var result = await UserManager.CreateAsync(user, model.Register.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, eRolesInfo.CreatioMembers.ToString());

                    model.Users.Informations.Email = model.Register.Email;
                    model.Users.Informations.Id = user.Id;

                    model.Avocats.Photo = Utils.ImageToBase64(Image.FromStream(model.Avocats.PhotoFile.InputStream));

                    await _usersManagment.SaveAvocatsInformation(model.Avocats);

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
    }
}