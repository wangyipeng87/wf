using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.BLL;
using WF.Entity;

namespace WF.WFFramework.WFNode
{
    public class EndNode : FlowNode
    {
        WF_InstanceBll instancebll = new WF_InstanceBll();
        public override NodeReturn Run(FlowContent flowContent)
        {
            this.RunEndFlowEvent(flowContent);
            WF_Instance instance = instancebll.getByID(flowContent.CurrentInstanceID);
            if(instance!=null)
            {
                instance.State = (int)Common.InstanceState.End;
                instance.UpdateTime = DateTime.Now;
                instance.UpdateUserCode = flowContent.CurrenUserCode;
                instancebll.update(instance);
            }
            NodeReturn ret = new NodeReturn();
            ret.isOver = true;
            return ret;
        }
    }
}
