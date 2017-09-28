using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.WFFramework.WFNode
{
    public class EndNode : FlowNode
    {
        public override List<string> Run(FlowContent flowContent)
        {
            this.RunEndFlowEvent(flowContent);
            return null;
        }
    }
}
