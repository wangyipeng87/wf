using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WF.DAO;
using WF.Entity;

namespace WF.BLL
{
    public class WF_RuleBll
    {
        private WF_RuleDao dao = new WF_RuleDao();
        public List<WF_Rule> getAllByTmpKey(string tmpkey)
        {
            return dao.getAllByTmpKey(tmpkey);
        }
        public List<WF_Rule> getRuleByTmpKeyAndBeginNodeKey(string tmpkey, string beginNodeKey)
        {
            return dao.getRuleByTmpKeyAndBeginNodeKey(tmpkey, beginNodeKey);
        }
    }
}


