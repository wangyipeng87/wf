using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WF.DAO;
using WF.Entity;
using WF.Common;

namespace WF.BLL
{
    public class WF_MenuBll
    {
        public List<WF_Menu> getAll(string rootid)
        {
            WF_MenuDao dao = new WF_MenuDao();
            return dao.getAll(rootid);
        }
        public string getMenu(string rootid)
        {
            WF_MenuDao dao = new WF_MenuDao();
            List<WF_Menu> menulist = dao.getAll(rootid);
            Menu menu = new Menu();
            initmenu(menu, menulist, rootid);
            return menu.ToJson();
        }

        private void initmenu(Menu menu, List<WF_Menu> menulist, string parentid)
        {
            List<WF_Menu> childs = menulist.Where(p => p.ParenrID == parentid).ToList();
            if (childs != null && childs.Count > 0)
            {
                List<Menu> sublist = new List<Menu>();
                menu.submenu = sublist;
                foreach (WF_Menu menuitem in childs)
                {
                    Menu sub = new Menu();
                    sublist.Add(sub);
                    sub.name = menuitem.Name;
                    sub.url = menuitem.URL;
                    sub.code = menuitem.ID;

                    if (menulist.Where(p => p.ParenrID == menuitem.ID).Count() > 0)
                    {
                        initmenu(sub, menulist, menuitem.ID);
                    }
                }
            }
        }
    }
}


