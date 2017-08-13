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
    public class FlowController : BaseController
    {
        WF_RoleBll rolebll = new WF_RoleBll();

        // GET: Flow
        public ActionResult TmpList()
        {
            return View();
        }
        // GET: Flow
        public ActionResult Instance()
        {
            return View();
        }
        // GET: Flow
        public ActionResult ToDoList()
        {
            return View();
        }
        public ActionResult AddRole(int? id)
        {
            if (id==null)
            {
                id = -1;
            }
            ViewBag.ID = id;
            return View();
        }
        
        // GET: Flow
        public ActionResult FlowRole()
        {
            return View();
        }
        // GET: Flow
        public ActionResult Applyauthority()
        {
            return View();
        }

        [HttpPost]
        public ContentResult GetFlowRoleList(string key, int state, string orderfiled, string dir,  int start, int length)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                key = Server.UrlDecode(key);
                #region 获取排序字段
                string orderstr = "wr.CreateTime desc";
                Dictionary<string, string> ordermap = new Dictionary<string, string>();
                ordermap.Add("RoleCode", "wr.RoleCode ");
                ordermap.Add("RoleName", "wr.RoleName ");
                ordermap.Add("State", "wr.State");
                ordermap.Add("CreateTime", "wr.CreateTime");
                if (!string.IsNullOrWhiteSpace(orderfiled))
                {
                    orderstr = ordermap[orderfiled] + " " + dir;
                }
                #endregion
                int count = 0;
                List<WF_Role> emplist = rolebll.getAll(key,  state, start + 1, start + length, orderstr, out count);
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
        public ContentResult getRoleByID(int id)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                WF_Role role = rolebll.getByID(id);
                if (role == null)
                {
                    role = new WF_Role();
                    role.ID = id;
                    role.State = 1;
                }
                res.code = ResultCode.OK;
                res.data = role;
            }
            catch (Exception ex)
            {
                res.code = ResultCode.ERROR;
                res.message = "获取流程角色信息失败";
            }
            return Content(res.ToJson());
        }
        [HttpPost]
        public ContentResult SaveRole(string jsonString)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                WF_Role role = jsonString.ToObject<WF_Role>();
                WF_Role entity = rolebll.getByID(role.ID);
                if (entity != null)
                {
                    role.UpdateTime = DateTime.Now;
                    role.UpdateUserCode = getCurrent().UserCode;
                    role.CreateTime = entity.CreateTime;
                    role.CreateUserCode = entity.CreateUserCode;
                    rolebll.update(role);
                }
                else
                {
                    role.UpdateTime = DateTime.Now;
                    role.UpdateUserCode = getCurrent().UserCode;
                    role.CreateTime = DateTime.Now;
                    role.CreateUserCode = getCurrent().UserCode;
                    rolebll.save(role);
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
        [HttpPost]
        public ContentResult UpdateRoleState(int id, int state)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                WF_Role entity = rolebll.getByID(id);
                if (entity != null)
                {
                    entity.State = state;
                    rolebll.update(entity);
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
        public ContentResult RoleDel(int id)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                rolebll.del(id);
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
    }
}