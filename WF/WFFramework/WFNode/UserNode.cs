using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.BLL;
using WF.Entity;

namespace WF.WFFramework.WFNode
{
    public class UserNode : FlowNode
    {
        WF_Node_PeopleBll people = new WF_Node_PeopleBll();
        public override List<string> GetTodoUser(FlowContent flowContent)
        {
            List<WF_Node_People> user= people.getAllByNode(flowContent.tmpKey, flowContent.currentNodeKey);
            List<string> userlist = new List<string>();
            if (user!=null&& user.Count>0)
            {
               userlist = user.Select(p => p.UserCode).ToList();
            }
            return userlist;
        }
    }
}
