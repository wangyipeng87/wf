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

        public void GetOrderInfoFormURL(out string orderdata, out string orderdir)
        {
            if (!string.IsNullOrWhiteSpace(Request.QueryString["order[0][column]"]))
            {
                string ind = Request.QueryString["order[0][column]"];
                orderdir = Request.QueryString["order[0][dir]"];
                orderdata = Request.QueryString[string.Format("columns[{0}][data]", ind)];
            }
            else
            {
                orderdir = "";
                orderdata = "";
            }
        }
        public void GetOrderInfoFormBody(out string orderdata, out string orderdir)
        {
            if (!string.IsNullOrWhiteSpace(Request.Form["order[0][column]"]))
            {
                string ind = Request.Form["order[0][column]"];
                orderdir = Request.Form["order[0][dir]"];
                orderdata = Request.Form[string.Format("columns[{0}][data]", ind)];
            }
            else
            {
                orderdir = "";
                orderdata = "";
            }
        }
    }
}