using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WF.DAO;
using WF.Entity;

namespace WF.BLL
{
    public class WF_TransferBll
    {
        WF_TransferDao dao = new WF_TransferDao();
        public int save(WF_Transfer entity)
        {
            return dao.save(entity);
        }
        public bool update(WF_Transfer entity)
        {
            return dao.update(entity);
        }
        public bool del(int id)
        {
            return dao.del(id);
        }
        public WF_Transfer getByID(int id)
        {
            return dao.getByID(id);
        }
    }
}


