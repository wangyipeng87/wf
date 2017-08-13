using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WF.Entity;
using WF.DAO;

namespace WF.BLL
{
    public class WF_Role_UserBll
    {
        WF_Role_UserDao dao = new WF_Role_UserDao();
        public bool save(WF_Role_User entity)
        {
            return dao.save(entity);
        }
        public bool update(WF_Role_User entity)
        {
            return dao.update(entity);
        }
        public bool del(int id)
        {
            return dao.del(id);
        }
        public WF_Role_User getByID(int id)
        {
            return dao.getByID(id);
        }
        public List<WF_Role_User> getAll(string key, string rolecode, int state, int begin, int end,  out int count)
        {
            return dao.getAll(key, rolecode, state, begin, end, out count);
        }
    }
}


