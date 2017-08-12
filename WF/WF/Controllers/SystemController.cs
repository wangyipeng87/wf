using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WF.Controllers
{
    public class SystemController : Controller
    {
        // GET: System
        public ActionResult EmployeeList()
        {
            return View();
        }
        // GET: System
        public ActionResult DepartmentList()
        {
            return View();
        }
        
    }
}