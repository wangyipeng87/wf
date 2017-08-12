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
    public class MenuController : BaseController
    {
        // GET: Menu
        public ActionResult Add(string id)
        {
            if(string.IsNullOrWhiteSpace(id))
            {
                id = Guid.NewGuid().ToString();
            }
            ViewBag.ID = id;
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 菜单
        /// </summary>
        /// <param name="queryJson"></param>
        /// <param name="draw"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        [HttpPost]
        public ContentResult GetAllTreeList()
        {
            AjaxResult res = new AjaxResult();
            try
            {

                WF_MenuBll bll = new WF_MenuBll();
                List<WF_Menu> menulist = bll.getAll("00000000-0000-0000-0000-000000000000");
                res.code = ResultCode.OK;
                res.data = menulist;
                res.totle = menulist.Count;
            }
            catch (Exception ex)
            {
                res.code = ResultCode.ERROR;
                res.message = "查询失败";
            }
            return Content(res.ToJson());
        }
        public ContentResult getMenuByParentidAndState(string parentid, int state)
        {
            List<WF_Menu> menulist = new List<WF_Menu>();
            try
            {

                WF_MenuBll bll = new WF_MenuBll();
                menulist = bll.getMenuByParentidAndState(parentid, state);

            }
            catch (Exception ex)
            {
                menulist = new List<WF_Menu>();
            }
            return Content(menulist.ToJson());
        }
        [HttpPost]
        public ContentResult save(string jsonString)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                WF_Menu menu = jsonString.ToObject<WF_Menu>();
                WF_MenuBll bll = new WF_MenuBll();
                WF_Menu entity = bll.getByID(menu.ID);
                if (entity != null)
                {
                    menu.UpdateTime = DateTime.Now;
                    menu.UpdateUserCode = getCurrent().UserCode;
                    menu.CreateTime = entity.CreateTime;
                    menu.CreateUserCode = entity.CreateUserCode;
                    bll.update(menu);
                }
                else
                {
                    menu.UpdateTime = DateTime.Now;
                    menu.UpdateUserCode = getCurrent().UserCode;
                    menu.CreateTime = DateTime.Now;
                    menu.CreateUserCode = getCurrent().UserCode;
                    bll.save(menu);
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
        public ContentResult updateorder(string id,int ordernum)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                WF_MenuBll bll = new WF_MenuBll();
                WF_Menu entity = bll.getByID(id);
                if (entity != null)
                {
                    entity.Order = ordernum;
                    bll.update(entity);
                }
                res.code = ResultCode.OK;
                res.message = "更新排序成功";
            }
            catch (Exception ex)
            {
                res.code = ResultCode.ERROR;
                res.message = "更新排序失败";
            }
            return Content(res.ToJson());
        }
        [HttpPost]
        public ContentResult updatestate(string id, int state)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                WF_MenuBll bll = new WF_MenuBll();
                WF_Menu entity = bll.getByID(id);
                if (entity != null)
                {
                    entity.State = state;
                    bll.update(entity);
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
        public ContentResult del(string id)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                WF_MenuBll bll = new WF_MenuBll();
                bll.del(id);
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
        


        public ContentResult getByID(string id)
        {
            AjaxResult res = new AjaxResult();
            try
            {
                WF_MenuBll bll = new WF_MenuBll();
                WF_Menu menu=bll.getByID(id);
                if(menu==null)
                {
                    menu = new WF_Menu();
                    menu.ID = id;
                    menu.State = 1;
                    menu.ParenrID = "00000000-0000-0000-0000-000000000000";
                }
                res.code = ResultCode.OK;
                res.data = menu;
            }
            catch (Exception ex)
            {
                res.code = ResultCode.ERROR;
                res.message = "获取菜单信息失败";
            }
            return Content(res.ToJson());
        }
    }
}