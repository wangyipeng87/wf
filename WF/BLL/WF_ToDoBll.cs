using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WF.DAO;
using WF.Entity;

namespace WF.BLL
{
    public class WF_ToDoBll
    {
        WF_ToDoDao dao = new WF_ToDoDao();
        public int save(WF_ToDo entity)
        {
            return dao.save(entity);
        }
        public bool update(WF_ToDo entity)
        {
            return dao.update(entity);
        }
        public bool del(int id)
        {
            return dao.del(id);
        }
        public WF_ToDo getByID(int id)
        {
            return dao.getByID(id);
        }
    }
}


