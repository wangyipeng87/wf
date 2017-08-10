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
        public ContentResult GetAllTreeList( )
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
            catch(Exception ex)
            {
                res.code = ResultCode.ERROR;
                res.message = "查询失败";
            }
            return Content(res.ToJson());
        }

    }
}