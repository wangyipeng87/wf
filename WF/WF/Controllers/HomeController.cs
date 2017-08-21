using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WF.DAO;
using WF.Entity;
using WF.BLL;
using WF.Common;

namespace WF.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: Personal
        public ActionResult FlowApply()
        {
            return View();
        }
    }
}