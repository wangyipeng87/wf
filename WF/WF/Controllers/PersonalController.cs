using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WF.Entity;
using WF.BLL;
using WF.Common;

namespace WF.Controllers
{
    public class PersonalController : Controller
    {
        WF_AgentBll agentbll = new WF_AgentBll();
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
        [HttpPost]
        public ContentResult GetAgentList(string origina, string user, int state,  int start, int length)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                origina = Server.UrlDecode(origina);
                user = Server.UrlDecode(user);
                int count = 0;
                List<WF_Agent> emplist = agentbll.getAll(origina, user, state, start + 1, start + length, out count);
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