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
       private WF_MenuDao dao = new WF_MenuDao();
        public bool save(WF_Menu entity)
        {
            return dao.save(entity);
        }
        public bool update(WF_Menu entity)
        {
            return dao.update(entity);
        }
        public bool del(string id)
        {
            return dao.del(id);
        }
        public WF_Menu getByID(string id)
        {
            return dao.getByID(id);
        }
        public List<WF_Menu> getAll(string rootid)
        {
            WF_MenuDao dao = new WF_MenuDao();
            return dao.getAll(rootid);
        }
        public string getMenu(string rootid)
        {
            List<WF_Menu> menulist = dao.getAll(rootid);
            Menu menu = new Menu();
            initmenu(menu, menulist, rootid);
            return menu.ToJson();
        }

        private void initmenu(Menu menu, List<WF_Menu> menulist, string parentid)
        {
            List<WF_Menu> childs = menulist.Where(p => p.ParenrID == parentid&&p.State==1).ToList();
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

                    if (menulist.Where(p => p.ParenrID == menuitem.ID&&p.State==1).Count() > 0)
                    {
                        initmenu(sub, menulist, menuitem.ID);
                    }
                }
            }
        }
        public List<WF_Menu> getMenuByParentidAndState(string parentid, int state)
        {
            return dao.getMenuByParentidAndState( parentid,  state);
        }
    }
}


