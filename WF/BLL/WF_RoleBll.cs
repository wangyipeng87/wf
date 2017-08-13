using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WF.Entity;
using WF.DAO;

namespace WF.BLL
{
    public class WF_RoleBll
    {

        private WF_RoleDao dao = new WF_RoleDao();
        public bool save(WF_Role entity)
        {
            return dao.save(entity);
        }
        public bool update(WF_Role entity)
        {
            return dao.update(entity);
        }
        public bool del(int id)
        {
            return dao.del(id);
        }
        public WF_Role getByID(int id)
        {
            return dao.getByID(id);
        }
        public List<WF_Role> getAll(string key, int state, int begin, int end, string order, out int count)
        {
            return dao.getAll( key,  state,  begin,  end,  order, out  count);
        }
    }
}


