using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.BLL;
using WF.Entity;

namespace WF.WFFramework.WFNode
{
    public class RoleNode : FlowNode
    {
        WF_TemplateNodeBll nodebll = new WF_TemplateNodeBll();
        WF_Role_UserBll roleuserbll = new WF_Role_UserBll();
        public override NodeReturn Run(FlowContent flowContent)
        {
            NodeReturn ret = new NodeReturn();
            if (flowContent.CurrentNodeKey.Split(',').Contains(this.NodeKey))
            {
                ret.isOver = true;
            }
            else
            {
                ret.isOver = false;
                WF_TemplateNode node = nodebll.getByNodeKey(flowContent.TmpKey, this.NodeKey);
                string rolecode = node.ProcessTypeValue;
                List<WF_Role_User> userlist = roleuserbll.getRoleUserByRoleCode(rolecode);
                List<string> user = new List<string>();
                user = userlist.Select(p => p.UserCode).ToList();
                ret.ToDoUserList = user;
            }
            return ret;
        }
    }
}
