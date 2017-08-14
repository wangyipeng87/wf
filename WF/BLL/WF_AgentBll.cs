using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WF.DAO;
using WF.Entity;
using WF.Common;

namespace WF.BLL
{
    public class WF_AgentBll
    {
        WF_AgentDao dao = new WF_AgentDao();

        public List<WF_Agent> getAll(string origina, string user, int state, int begin, int end,  out int count)
        {
            return dao.getAll(origina, user, state, begin, end,  out count);
        }
    }
}


