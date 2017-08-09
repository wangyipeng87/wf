using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WF.BLL;
using WF.Common;
using WF.Entity;

namespace WF.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        
        public Employee getCurrent()
        {
            Employee emp = new Employee();
            if(System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                EmployeeBll bll = new EmployeeBll();
                emp= bll.getbyAccount(System.Web.HttpContext.Current.User.Identity.Name);
            }
            return emp;
        }
    }
}