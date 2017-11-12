using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.WFFramework.WFFlow
{
   public class NormalFlow:Flow
    {
        public override void Init() {
            this.afterStartFlow += NormalFlow_afterStartFlow;
            this.beforStartFlow += NormalFlow_beforStartFlow;
            this.beforOperation += NormalFlow_beforOperation;
            this.afterOperation += NormalFlow_afterOperation;
            this.endFlow += NormalFlow_endFlow;
        }

        private void NormalFlow_endFlow(FlowContent flowContent)
        {
            Console.WriteLine("endFlow:" + flowContent.TaskName+" FormID:"+ flowContent.FormID);
        }

        private void NormalFlow_afterOperation(FlowContent flowContent)
        {
            Console.WriteLine("afterOperation:" + flowContent.TaskName + " FormID:" + flowContent.FormID);
        }

        private void NormalFlow_beforOperation(FlowContent flowContent)
        {
            Console.WriteLine("beforOperation:" + flowContent.TaskName + " FormID:" + flowContent.FormID);
        }

        private void NormalFlow_beforStartFlow(FlowContent flowContent)
        {
            Console.WriteLine("beforStartFlow:" + flowContent.TaskName + " FormID:" + flowContent.FormID);
        }

        private void NormalFlow_afterStartFlow(FlowContent flowContent)
        {
            Console.WriteLine("afterStartFlow:" + flowContent.TaskName + " FormID:" + flowContent.FormID);
        }
    }
}
