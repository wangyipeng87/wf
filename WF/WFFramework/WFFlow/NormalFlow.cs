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
        }

        private void NormalFlow_beforStartFlow(FlowContent flowContent)
        {
            Console.WriteLine("beforStartFlow:" + flowContent.TaskName);
        }

        private void NormalFlow_afterStartFlow(FlowContent flowContent)
        {
            Console.WriteLine("afterStartFlow:" + flowContent.TaskName);
        }
    }
}
