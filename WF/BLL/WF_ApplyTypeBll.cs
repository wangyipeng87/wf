using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.DAO;
using WF.Entity;

namespace WF.BLL
{
  public  class WF_ApplyTypeBll
    {
        WF_ApplyTypeDao dao = new WF_ApplyTypeDao();
        public List<WF_ApplyType> getAll()
        {
            return dao.getAll();
        }
        public bool save(WF_ApplyType entity)
        {
            return dao.save(entity);
        }
        public bool update(WF_ApplyType entity)
        {
            return dao.update(entity);
        }
        public bool del(int id)
        {
            return dao.del(id);
        }
        public WF_ApplyType getByCode(string code)
        {
            return dao.getByCode(code);
        }
        public WF_ApplyType getByID(int id)
        {
            return dao.getByID(id);
        }
        public List<WF_ApplyType> getAll(string key, int state, int begin, int end, string order, out int count)
        {
            return dao.getAll(key, state, begin, end, order, out count);
        }
    }
}
