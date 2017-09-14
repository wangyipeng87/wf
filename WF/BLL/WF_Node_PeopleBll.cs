using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.DAO;
using WF.Entity;

namespace WF.BLL
{
  public  class WF_Node_PeopleBll
    {
        private WF_Node_PeopleDao dao = new WF_Node_PeopleDao();
        public bool save(WF_Node_People entity)
        {
            return dao.save(entity);
        }

        public int del(string tmpkey, string nodekey)
        {
            return dao.del(tmpkey, nodekey);
        }

        public List<WF_Node_People> getAllByNode(string tmpkey, string nodekey)
        {
            return dao.getAllByNode( tmpkey,  nodekey);

        }
    }
}
