using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WF.DAO;
using WF.Entity;

namespace WF.BLL
{
    public class WF_TemplateVariableBll
    {
        private WF_TemplateVariableDao dao = new WF_TemplateVariableDao();
        public bool save(WF_TemplateVariable entity)
        {
            return dao.save(entity);
        }
        public bool update(WF_TemplateVariable entity)
        {
            return dao.update(entity);
        }
        public bool del(int id)
        {
            return dao.del(id);
        }
        public WF_TemplateVariable getByID(int id)
        {
            return dao.getByID(id);
        }
        public WF_TemplateVariable getVarByID(int id)
        {
            return dao.getVarByID(id);
        }
        public List<WF_TemplateVariable> getAll(string key, string tmpkey, int state, int begin, int end, out int count)
        {
            return dao.getAll( key, tmpkey,  state,  begin,  end, out  count);
        }
    }
}


