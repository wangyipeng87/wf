using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.BLL;
using WF.Entity;

namespace WF.WFFramework.WFNode
{
   public class BeginNode : FlowNode
    {
        WF_InstanceBll instanceBll = new WF_InstanceBll();
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
                WF_Instance node = instanceBll.getByID(flowContent.CurrentInstanceID) ;
                List<string> user = new List<string>();
                if(node.ApplyUserCode!= node.WriterUserCode)
                {
                    user.Add(node.ApplyUserCode);
                    user.Add(node.WriterUserCode);
                }
                else
                {
                    user.Add(node.WriterUserCode);
                }
                ret.ToDoUserList = user;
            }
            return ret;
        }
    }
}
