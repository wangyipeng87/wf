using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WF.DAO;
using WF.Entity;

namespace WF.BLL
{
    public class WF_TemplateNodeBll
    {
        private WF_TemplateNodeDao dao = new WF_TemplateNodeDao();

        public bool save(WF_TemplateNode entity)
        {
            return dao.save(entity);
        }
        public bool update(WF_TemplateNode entity)
        {
            return dao.update(entity);
        }
        public bool del(int id)
        {
            return dao.del(id);
        }
        public int DelByTmpKey(string tmpKey)
        {
            return dao.DelByTmpKey(tmpKey);
        }
    }
}


