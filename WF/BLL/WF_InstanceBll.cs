using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WF.DAO;
using WF.Entity;

namespace WF.BLL
{
    public class WF_InstanceBll
    {
        WF_InstanceDao dao = new WF_InstanceDao();
        public int save(WF_Instance entity)
        {
            return dao.save(entity);
        }
        public WF_Instance getByID(int id)
        {
            return dao.getByID(id);
        }
    }
}


