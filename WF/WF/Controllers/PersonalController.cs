using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WF.Controllers
{
    public class PersonalController : Controller
    {
        // GET: Personal
        public ActionResult MyApply()
        {
            return View();
        }
        // GET: Personal
        public ActionResult MyToDo()
        {
            return View();
        }
        // GET: Personal
        public ActionResult MyDone()
        {
            return View();
        }
        // GET: Personal
        public ActionResult Agent()
        {
            return View();
        }
        
    }
}