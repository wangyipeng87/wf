using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WF.Entity;
using WF.DAO;


namespace WF.BLL
{
    public class WF_TemplateBll
    {
        private WF_TemplateDao dao = new WF_TemplateDao();
        public bool save(WF_Template entity)
        {
            return dao.save(entity);
        }
        public bool update(WF_Template entity)
        {
            return dao.update(entity);
        }
        public bool del(int id)
        {
            return dao.del(id);
        }
        public WF_Template getByKey(string key)
        {
            return dao.getByKey(key);
        }
        public WF_Template getByID(int id)
        {
            return dao.getByID(id);
        }
        public List<WF_Template> getAll(string key, int state, int begin, int end, string order, out int count)
        {
            return dao.getAll( key,  state,  begin,  end,  order, out  count);
        }
    }
}


