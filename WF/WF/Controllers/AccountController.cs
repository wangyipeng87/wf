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

      
        //
        // GET: /Account/
        public ActionResult Login(string username, string password)
        {
            bool result = true;
            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);
                return Redirect(Url.Action("index", "Home"));
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