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
    public class PersonalController : BaseController
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
        // GET: Personal
        public ActionResult AddAgent(int?id)
        {
            if (id==null)
            {
                id = -1;
            }
            ViewBag.ID = id;
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
        [HttpPost]
        public ContentResult updateAgentstate(int id, int state)
        {
            AjaxResult res = new AjaxResult();
            try
            { 
                WF_Agent entity = agentbll.getByID(id);
                if (entity != null)
                {
                    entity.State = state;
                    agentbll.update(entity);
                }
                res.code = ResultCode.OK;
                res.message = "更新状态成功";
            }
            catch (Exception ex)
            {
                res.code = ResultCode.ERROR;
                res.message = "更新状态失败";
            }
            return Content(res.ToJson());
        }
        [HttpPost]
        public ContentResult delAgent(int id)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                agentbll.del(id);
                res.code = ResultCode.OK;
                res.message = "删除成功";
            }
            catch (Exception ex)
            {
                res.code = ResultCode.ERROR;
                res.message = "删除失败";
            }
            return Content(res.ToJson());
        }
        public ContentResult getAgentByID(int id)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                WF_Agent agent = agentbll.getByID(id);
                if (agent == null)
                {
                    agent = new WF_Agent();
                    agent.ID = id;
                    agent.State = 1;

                }
                res.code = ResultCode.OK;
                res.data = agent;
            }
            catch (Exception ex)
            {
                res.code = ResultCode.ERROR;
                res.message = "获取代理人信息失败";
            }
            return Content(res.ToJson());
        }
        [HttpPost]
        public ContentResult saveAgent(string jsonString)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                WF_Agent agent = jsonString.ToObject<WF_Agent>();
                WF_Agent entity = agentbll.getByID(agent.ID);
                if (entity != null)
                {
                    agent.UpdateTime = DateTime.Now;
                    agent.UpdateUserCode = getCurrent().UserCode;
                    agent.CreateTime = entity.CreateTime;
                    agent.CreateUserCode = entity.CreateUserCode;
                    agent.IsDelete = entity.IsDelete;
                    agentbll.update(agent);
                }
                else
                {
                    agent.UpdateTime = DateTime.Now;
                    agent.UpdateUserCode = getCurrent().UserCode;
                    agent.CreateTime = DateTime.Now;
                    agent.CreateUserCode = getCurrent().UserCode;
                    agent.IsDelete=0;
                    agentbll.save(agent);
                }
                res.code = ResultCode.OK;
            }
            catch (Exception ex)
            {
                res.code = ResultCode.ERROR;
                res.message = "保存失败";
            }
            return Content(res.ToJson());
        }

    }
}