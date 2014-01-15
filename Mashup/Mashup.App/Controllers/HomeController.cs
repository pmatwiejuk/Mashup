using System.Web.Mvc;

namespace Mashup.App.Controllers
{
    using System.Web.Security;

    using Mashup.App.Infrastructure;
    using Mashup.Data.Model.dbo;
    using Mashup.Logic.Contracts;
    using Mashup.Logic.Logic;
    using Mashup.App.Models;

    public class HomeController : BaseController<IAccountService>
    {
        public HomeController()
            : base(new AccountService())
        {
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {
            if (this.ModelState.IsValid)
            {
                if (this.Service.ValidateUser(model.Email, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                    return this.RedirectToAction("Index", "Home");
                }
                else
                {
                    this.ModelState.AddModelError("InvalidLoginOrPassword", "Niewłaściwy login lub hasło");
                }
            }

            return this.View();
            
        }

        [Authorize]
        public ActionResult LogOut()
        {
            this.Session.Abandon();
            FormsAuthentication.SignOut();

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult EditProfile()
        {
            if (this.TempData["ViewData"] != null)
            {
                this.ViewData = (ViewDataDictionary)this.TempData["ViewData"];
            }

            var userInfo = this.Service.GetUserInfo(this.User.Identity.Name);
            return this.View(userInfo);
        }

        [Authorize]
        [HttpPost]
        public ActionResult SaveUser(Users result)
        {
            var saveSuccess = this.Service.SaveUser(this.User.Identity.Name, result);

            if (!saveSuccess)
            {
                this.ModelState.AddModelError("UserSaveInfo", "Wystąpił problem w trakcie zapisywania informacji.");
            }

            return this.RedirectToAction("EditProfile", "Home");
        }
    }
}
