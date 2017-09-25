using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WF.DAO;
using WF.Entity;

namespace WF.BLL
{
    public class WF_InstanceVariableBll
    {

        WF_InstanceVariableDao dao = new WF_InstanceVariableDao();
        public int save(WF_InstanceVariable entity)
        {
            return dao.save(entity);
        }
        public bool Update(WF_InstanceVariable entity)
        {
            return dao.Update(entity);
        }
        public WF_InstanceVariable getbyInstanceAndVarname(int instanceid, string varname) {

            return dao.getbyInstanceAndVarname(instanceid,  varname);
        }

        public int copyVarByTmpKey(int instanceid, string tmpkey, string usercode)
        {
            return dao.copyVarByTmpKey(instanceid, tmpkey, usercode);
        }
        public List<WF_InstanceVariable> getbyInstanceID(int instanceid)
        {
            return dao.getbyInstanceID( instanceid);
        }
    }
}


