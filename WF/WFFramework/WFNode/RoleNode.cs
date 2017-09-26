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
        public override List<string> GetTodoUser(FlowContent flowContent)
        {
            WF_TemplateNode node= nodebll.getByNodeKey(flowContent.TmpKey, flowContent.CurrentNodeKey);
            string rolecode = node.ProcessTypeValue;
            List<WF_Role_User> userlist= roleuserbll.getRoleUserByRoleCode(rolecode);
            List<string> user = new List<string>();
            user = userlist.Select(p => p.UserCode).ToList();
            return user;
        }
    }
}
