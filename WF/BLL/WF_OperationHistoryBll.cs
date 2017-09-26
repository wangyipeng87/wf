using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WF.DAO;
using WF.Entity;

namespace WF.BLL
{
    public class WF_OperationHistoryBll
    {
        WF_OperationHistoryDao dao = new WF_OperationHistoryDao();
        public int save(WF_OperationHistory entity)
        {
            return dao.save(entity);
        }
    }
}


