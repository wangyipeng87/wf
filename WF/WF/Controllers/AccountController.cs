using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WF.Models;

namespace WF.Controllers
{
    public class AccountController : Controller
    {

        public ActionResult Login()
        {
            return View();
        }
        //
        // GET: /Account/
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                bool result =true;
                if (result)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return Redirect(Url.Action("Home", "index"));
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect(FormsAuthentication.LoginUrl);
        }
	}
}