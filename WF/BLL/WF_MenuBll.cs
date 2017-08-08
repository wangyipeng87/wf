using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WF.DAO;
using WF.Entity;

namespace WF.BLL
{
    public class WF_MenuBll
    {
        public List<WF_Menu> getAll(string rootid)
        {
            WF_MenuDao dao = new WF_MenuDao();
            return dao.getAll(rootid);
        }
        public Menu getMenu(string rootid)
        {
            WF_MenuDao dao = new WF_MenuDao();
            List<WF_Menu> menulist = dao.getAll(rootid);
            Menu menu = new Menu();
            initmenu(menu, menulist, rootid);
            return menu;
        }

        private void initmenu(Menu menu, List<WF_Menu> menulist, string parentid)
        {
            WF_Menu parent = menulist.Where(p => p.ID == parentid).FirstOrDefault();
            if (parent != null)
            {
                menu.name = parent.Name;
                menu.url = parent.URL;
                if (menulist.Where(p => p.ParenrID == parentid).Count() > 0)
                {
                    List<WF_Menu> list = menulist.Where(p => p.ParenrID == parentid).ToList();
                    List<Menu> sublist = new List<Menu>();
                    menu.submenu = sublist;
                    foreach (WF_Menu item in list)
                    {
                        Menu sub = new Menu();
                        sub.name = item.Name;
                        sub.url = item.URL;
                        if (menulist.Where(p => p.ParenrID == item.ID).Count() > 0)
                        {
                            initmenu(sub, menulist, item.ID);
                        }
                        sublist.Add(sub);
                    }
                }
            }
        }
    }
}


