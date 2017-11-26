using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WF.DAO;
using WF.Entity;


namespace WF.BLL
{
    public class WF_SignBll
    {

        WF_SignDao dao = new WF_SignDao();
        public int save(WF_Sign entity)
        {
            return dao.save(entity);
        }
        public bool update(WF_Sign entity)
        {
            return dao.update(entity);
        }
        public bool del(int id)
        {
            return dao.del(id);
        }
        public WF_Sign getByID(int id)
        {
            return dao.getByID(id);
        }
    }
}


