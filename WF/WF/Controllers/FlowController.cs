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
        WF_Role_UserBll roleuserbll = new WF_Role_UserBll();
        WF_TemplateBll tmpbll = new WF_TemplateBll();
        EmployeeBll empbll = new EmployeeBll();
        WF_ApplyTypeBll applytypebll = new WF_ApplyTypeBll();
        WF_TemplateVariableBll varbll = new WF_TemplateVariableBll();
        WF_TemplateNodeBll nodebll = new WF_TemplateNodeBll();
        WF_RuleBll rulebll = new WF_RuleBll();
        WF_Node_PeopleBll peoplebll = new WF_Node_PeopleBll();

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
        // GET: FlowDesign
        public ActionResult FlowDesign(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                ViewBag.key = key;
                ViewBag.tmp = tmpbll.getByKey(key);
            }
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
        public ActionResult AddTmp(int? id)
        {
            if (id == null)
            {
                id = -1;
            }
            ViewBag.ID = id;
            return View();
        }

        // GET: Flow
        public ActionResult FlowRole(string key)
        {
            ViewBag.key = key;
            return View();
        }
        // GET: Flow
        public ActionResult FlowTypeList()
        {
            return View();
        }
        // GET: Flow
        public ActionResult AddFlowType(int? id)
        {
            if (id == null)
            {
                id = -1;
            }
            ViewBag.ID = id;
            return View();
        }
        
        // GET: Flow
        public ActionResult RuleDetail(string key)
        {
            ViewBag.key = key;
            return View();
        }
        // GET: Flow
        public ActionResult NodeDetail(string key)
        {
            ViewBag.applytypelist = applytypebll.getAll();
            ViewBag.rolelist = rolebll.getAll();
            ViewBag.key = key;
            return View();
        }

        // GET: Flow
        public ActionResult FlowRoleUserList(string rolecode, string rolename)
        {
            ViewBag.rolecode = rolecode;
            ViewBag.rolename = rolename;
            return View();
        }
        // GET: Flow
        public ActionResult AddRoleUser(int? id, string rolecode, string rolename)
        {
            ViewBag.rolecode = rolecode;
            if(id==null)
            {
                id = -1;
            }
            ViewBag.id = id;
            ViewBag.rolename = rolename;
            return View();
        }
        // GET: Flow
        public ActionResult VarList(string tmpkey, string tmpname)
        {
            ViewBag.tmpkey = tmpkey;
            ViewBag.tmpname = tmpname;
            return View();
        }
        // GET: Flow
        public ActionResult AddVar(int? id, string tmpkey, string tmpname)
        {
            if (id == null)
            {
                id = -1;
            }
            ViewBag.id = id;
            ViewBag.tmpkey = tmpkey;
            ViewBag.tmpname = tmpname;
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

        [HttpPost]
        public ContentResult GetFlowRoleUserList(string key, string rolecode, int state, int start, int length)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                key = Server.UrlDecode(key);
                int count = 0;
                List<WF_Role_User> emplist = roleuserbll.getAll(key, rolecode, state,start + 1, start + length,  out count);
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
        public ContentResult getRoleUserByID(int id)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                WF_Role_User role = roleuserbll.getByID(id);
                if (role == null)
                {
                    role = new WF_Role_User();
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
        public ContentResult SaveRoleUser(string jsonString)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                WF_Role_User role = jsonString.ToObject<WF_Role_User>();
                WF_Role_User entity = roleuserbll.getByID(role.ID);
                if (entity != null)
                {
                    role.UpdateTime = DateTime.Now;
                    role.UpdateUserCode = getCurrent().UserCode;
                    role.CreateTime = entity.CreateTime;
                    role.CreateUserCode = entity.CreateUserCode;
                    role.IsDelete = entity.IsDelete;
                    roleuserbll.update(role);
                }
                else
                {
                    role.UpdateTime = DateTime.Now;
                    role.UpdateUserCode = getCurrent().UserCode;
                    role.CreateTime = DateTime.Now;
                    role.CreateUserCode = getCurrent().UserCode;
                    role.IsDelete = 0;
                    roleuserbll.save(role);
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
        public ContentResult UpdateRoleUserState(int id, int state)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                WF_Role_User entity = roleuserbll.getByID(id);
                if (entity != null)
                {
                    entity.State = state;
                    roleuserbll.update(entity);
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
        public ContentResult RoleUserDel(int id)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                roleuserbll.del(id);
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

        [HttpPost]
        public ContentResult GetTmpList(string key, int state, string orderfiled, string dir, int start, int length)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                key = Server.UrlDecode(key);
                #region 获取排序字段
                string orderstr = "wt.CreateTime desc";
                Dictionary<string, string> ordermap = new Dictionary<string, string>();
                ordermap.Add("key", "wt.key ");
                ordermap.Add("TmpName", "wt.TmpName ");
                ordermap.Add("State", "wt.State");
                ordermap.Add("CreateTime", "wt.CreateTime");
                ordermap.Add("createuser", "wt.CreateUserCode"); 
                if (!string.IsNullOrWhiteSpace(orderfiled))
                {
                    orderstr = ordermap[orderfiled] + " " + dir;
                }
                #endregion
                int count = 0;
                List<WF_Template> emplist = tmpbll.getAll(key, state, start + 1, start + length, orderstr, out count);
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
        public ContentResult SaveTmp(string jsonString)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                WF_Template role = jsonString.ToObject<WF_Template>();
                WF_Template entity = tmpbll.getByID(role.ID);
                if (entity != null)
                {
                    role.UpdateTime = DateTime.Now;
                    role.UpdateUserCode = getCurrent().UserCode;
                    role.CreateTime = entity.CreateTime;
                    role.CreateUserCode = entity.CreateUserCode;
                    tmpbll.update(role);
                }
                else
                {
                    role.UpdateTime = DateTime.Now;
                    role.UpdateUserCode = getCurrent().UserCode;
                    role.CreateTime = DateTime.Now;
                    role.CreateUserCode = getCurrent().UserCode;
                    tmpbll.save(role);
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
        public ContentResult getTmpByID(int id)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                WF_Template role = tmpbll.getByID(id);
                if (role == null)
                {
                    role = new WF_Template();
                    role.ID = id;
                    role.State = 1;
                }
                res.code = ResultCode.OK;
                res.data = role;
            }
            catch (Exception ex)
            {
                res.code = ResultCode.ERROR;
                res.message = "获取流程模板信息失败";
            }
            return Content(res.ToJson());
        }
        [HttpPost]
        public ContentResult UpdateTmpState(int id, int state)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                WF_Template entity = tmpbll.getByID(id);
                if (entity != null)
                {
                    entity.State = state;
                    tmpbll.update(entity);
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
        public ContentResult TmpDel(int id)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                tmpbll.del(id);
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


        [HttpPost]
        public ContentResult GetApplyTypeList(string key, int state, string orderfiled, string dir, int start, int length)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                key = Server.UrlDecode(key);
                #region 获取排序字段
                string orderstr = "wt.CreateTime desc";
                Dictionary<string, string> ordermap = new Dictionary<string, string>();
                ordermap.Add("Code", "wt.Code ");
                ordermap.Add("ApplyName", "wt.ApplyName ");
                ordermap.Add("State", "wt.State");
                ordermap.Add("CreateTime", "wt.CreateTime");
                ordermap.Add("createuser", "wt.CreateUserCode");
                if (!string.IsNullOrWhiteSpace(orderfiled))
                {
                    orderstr = ordermap[orderfiled] + " " + dir;
                }
                #endregion
                int count = 0;
                List<WF_ApplyType> emplist = applytypebll.getAll(key, state, start + 1, start + length, orderstr, out count);
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
        public ContentResult SaveApplyType(string jsonString)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                WF_ApplyType role = jsonString.ToObject<WF_ApplyType>();
                WF_ApplyType entity = applytypebll.getByID(role.ID);
                if (entity != null)
                {
                    role.UpdateTime = DateTime.Now;
                    role.UpdateUserCode = getCurrent().UserCode;
                    role.CreateTime = entity.CreateTime;
                    role.CreateUserCode = entity.CreateUserCode;
                    applytypebll.update(role);
                }
                else
                {
                    role.UpdateTime = DateTime.Now;
                    role.UpdateUserCode = getCurrent().UserCode;
                    role.CreateTime = DateTime.Now;
                    role.CreateUserCode = getCurrent().UserCode;
                    applytypebll.save(role);
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
        public ContentResult getApplyTypeByID(int id)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                WF_ApplyType role = applytypebll.getByID(id);
                if (role == null)
                {
                    role = new WF_ApplyType();
                    role.ID = id;
                    role.State = 1;
                }
                res.code = ResultCode.OK;
                res.data = role;
            }
            catch (Exception ex)
            {
                res.code = ResultCode.ERROR;
                res.message = "获取流程模板信息失败";
            }
            return Content(res.ToJson());
        }
        [HttpPost]
        public ContentResult UpdateApplyTypeState(int id, int state)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                WF_ApplyType entity = applytypebll.getByID(id);
                if (entity != null)
                {
                    entity.State = state;
                    applytypebll.update(entity);
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
        public ContentResult ApplyTypeDel(int id)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                applytypebll.del(id);
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
        [HttpPost]
        public ContentResult GetVarList(string key, string tmpkey, int state, int start, int length)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                key = Server.UrlDecode(key);
                int count = 0;
                List<WF_TemplateVariable> emplist = varbll.getAll(key, tmpkey, state, start + 1, start + length, out count);
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

        public ContentResult getVarByID(int id)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                WF_TemplateVariable role = varbll.getByID(id);
                if (role == null)
                {
                    role = new WF_TemplateVariable();
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
        public ContentResult SaveVar(string jsonString)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                WF_TemplateVariable role = jsonString.ToObject<WF_TemplateVariable>();
                WF_TemplateVariable entity = varbll.getByID(role.ID);
                if (entity != null)
                {
                    role.UpdateTime = DateTime.Now;
                    role.UpdateUserCode = getCurrent().UserCode;
                    role.CreateTime = entity.CreateTime;
                    role.CreateUserCode = entity.CreateUserCode;
                    role.IsDelete = entity.IsDelete;
                    varbll.update(role);
                }
                else
                {
                    role.UpdateTime = DateTime.Now;
                    role.UpdateUserCode = getCurrent().UserCode;
                    role.CreateTime = DateTime.Now;
                    role.CreateUserCode = getCurrent().UserCode;
                    role.IsDelete = 0;
                    varbll.save(role);
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
        public ContentResult UpdateVarState(int id, int state)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                WF_TemplateVariable entity = varbll.getByID(id);
                if (entity != null)
                {
                    entity.State = state;
                    varbll.update(entity);
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
        public ContentResult VarDel(int id)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                varbll.del(id);
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

        [HttpPost]
        public ContentResult SaveWFDesign(string jsonString)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                WFTmp tmp = jsonString.ToObject<WFTmp>();
                if (tmp.nodelist != null && tmp.nodelist.Count > 0)
                {
                    foreach (WF_TemplateNode item in tmp.nodelist)
                    {
                        item.IsDelete = 0;
                        item.State = 1;
                        item.CreateTime = DateTime.Now;
                        item.CreateUserCode = getCurrent().UserCode;
                        item.UpdateTime = DateTime.Now;
                        item.UpdateUserCode = getCurrent().UserCode;

                        if (item.userlist != null && item.userlist.Count > 0)
                        {
                            foreach (WF_Node_People people in item.userlist)
                            {
                                people.IsDelete = 0;
                                people.State = 1;
                                people.CreateTime = DateTime.Now;
                                people.CreateUserCode = getCurrent().UserCode;
                                people.UpdateTime = DateTime.Now;
                                people.UpdateUserCode = getCurrent().UserCode;
                                people.NodeKey = item.Nodekey;
                                people.Tmpkey = item.Tmpkey;
                            }
                        }
                    }
                }
                if (tmp.rulelist != null && tmp.rulelist.Count > 0)
                {
                    foreach (WF_Rule item in tmp.rulelist)
                    {
                        item.IsDelete = 0;
                        item.State = 1;
                        item.CreateTime = DateTime.Now;
                        item.CreateUserCode = getCurrent().UserCode;
                        item.UpdateTime = DateTime.Now;
                        item.UpdateUserCode = getCurrent().UserCode;
                    }
                }

                if (tmpbll.save(tmp))
                {
                    res.code = ResultCode.OK;
                }
                else
                {
                    res.code = ResultCode.ERROR;
                }
            }
            catch (Exception ex)
            {
                res.code = ResultCode.ERROR;
                res.message = "保存失败";
            }
            return Content(res.ToJson());
        }
        [HttpPost]
        public ContentResult GetWFDesign(string tmpkey)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                WFTmp tmp = new WFTmp();
                tmp.nodelist = nodebll.getAllByTmpKey(tmpkey);
                if (tmp.nodelist != null && tmp.nodelist.Count > 0)
                {
                    foreach (WF_TemplateNode item in tmp.nodelist)
                    {
                        item.userlist = peoplebll.getAllByNode(tmpkey, item.Nodekey);                    }
                }
                tmp.rulelist = rulebll.getAllByTmpKey(tmpkey);
                res.code = ResultCode.OK;
                res.data = tmp;
            }
            catch (Exception ex)
            {
                res.code = ResultCode.ERROR;
                res.message = "获取失败";
            }
            return Content(res.ToJson());
        }
    }
}