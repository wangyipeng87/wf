using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WF.BLL;
using WF.Common;
using WF.Entity;

namespace WF.Controllers
{
    public class SystemController : BaseController
    {
        private EmployeeBll empbll = new EmployeeBll();
        private DepartmentBLL deptbll = new DepartmentBLL();
        // GET: System
        public ActionResult EmployeeList()
        {
            return View();
        }
        // GET: System
        public ActionResult DepartmentList()
        {
            ViewBag.DeptInfo = deptbll.getDepart(ConfigurationManager.AppSettings["RootDeptCode"].ToString());
            return View();
        }
        [HttpPost]
        public ContentResult GetEmpList(string key, string linemanage,string orderfiled,string dir, int state, int start, int length)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                key = Server.UrlDecode(key);
                linemanage = Server.UrlDecode(linemanage);
                #region 获取排序字段
                string orderstr = "e.CreateTime desc";
                Dictionary<string, string> ordermap = new Dictionary<string, string>();
                ordermap.Add("UserName", "e.UserName ");
                ordermap.Add("Sex", "e.Sex ");
                ordermap.Add("Email", "e.Email ");
                ordermap.Add("Account", "e.Account ");
                ordermap.Add("Phone", "e.Phone ");
                ordermap.Add("UserCode", "e.UserCode");
                ordermap.Add("PostName", "e.PostName");
                ordermap.Add("DeptName", "e.DeptName");
                ordermap.Add("CreateTime", "e.CreateTime");
                ordermap.Add("LineManage", "e.LineManageCode");
                ordermap.Add("State", "e.State");
                if (!string.IsNullOrWhiteSpace(orderfiled))
                {
                    orderstr = ordermap[orderfiled] + " " + dir;
                }
                #endregion
                int count = 0;
                List<Employee> emplist = empbll.getAll(key, linemanage, state, start + 1, start + length, orderstr, out count);
                res.code = ResultCode.OK;
                res.data = emplist;
                res.totle = count;
            }
            catch (Exception ex)
            {
                res.code = ResultCode.ERROR;
                res.message = "查询失败";
            }
            return Content(res.ToJson());
        }
    }
}