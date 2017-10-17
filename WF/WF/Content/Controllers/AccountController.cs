using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WF.BLL;
using WF.Entity;
using WF.Models;
using WF.Common;

namespace WF.Controllers
{
    public class AccountController : BaseController
    {

        EmployeeBll empbll = new EmployeeBll();
        //
        // GET: /Account/
        public ActionResult Login(string username, string password)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                EmployeeBll bll = new EmployeeBll();
                Employee emp = bll.getbyAccountAndPwd(username.Trim(), password.Trim());
                if (emp != null)
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                    return Redirect(Url.Action("index", "Home"));
                }
                else
                {
                    return View();
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect(FormsAuthentication.LoginUrl);
        }
        [HttpPost]
        public ContentResult getEmpForAutocomplete(string key)
        {
            return Content( empbll.getEmpForAutocomplete(key).ToJson());
        }
    }
}