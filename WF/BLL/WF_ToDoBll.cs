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
        public List<WF_ToDo> getList(int instanceid, string nodekey, int state)
        {
            return dao.getList(instanceid, nodekey, state);
        }
        public List<WF_ToDo> getMyTodoList(string usercode, int begin, int end, out int count)
        {
            return dao.getMyTodoList(usercode, begin, end, out count);
        }
        public List<WF_ToDo> getMyDoneList(string usercode, int begin, int end, out int count)
        {
            return dao.getMyDoneList(usercode, begin, end, out count);
        }
        public List<WF_Instance> getMyApplyList(string usercode, int state, string keyword, int begin, int end, out int count)
        {
            return dao.getMyApplyList(usercode, state, keyword, begin, end, out count);
        }
        public List<WF_Instance> getCurrentInstanceList(string user, int state, string keyword, int begin, int end, out int count)
        {
            return dao.getCurrentInstanceList(user, state, keyword, begin, end, out count);
        }
        public List<WF_ToDo> getTodoList(string user, int begin, int end, out int count)
        {
            return dao.getTodoList( user,  begin,  end, out  count);
        }
    }
}


