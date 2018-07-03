using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PurpleNetwork.Models;

namespace PurpleNetwork.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult LoginOrRegister(string returnUrl)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                ViewBag.returnUrl = returnUrl;
                return View();
            }
            else
                return RedirectToAction("Info", "Home");
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.EmailRegister, Email = model.EmailRegister, FirstName = model.FirstName, LastName = model.LastName };
                user.ImageUrl = "https://innmind.com/assets/placeholders/no_avatar-3d6725770296b6a1cce653a203d8f85dcc5298945b71fa7360e3d9aa4a3fc054.svg";
                user.Description = "Lorem ipsum dolor sit amet, et laudem audire sea. Fugit doming appareat usu ut, aperiam legendos pertinax ne usu, in vel eros eirmod eligendi.";

                IdentityResult result = await UserManager.CreateAsync(user, model.PasswordRegister);
                if (result.Succeeded)
                {
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
                                             DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    if (String.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Info", "Home");
                    return Redirect(returnUrl);
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View("LoginOrRegister");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await UserManager.FindAsync(model.EmailLogin, model.PasswordLogin);
                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    if (String.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Info", "Home");
                    return Redirect(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View("LoginOrRegister");
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("LoginOrRegister");
        }
    }
}