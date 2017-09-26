using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using WF.WFFramework;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    Flow flo = FlowFactory.getFlow("key1", "8454");
                    flo.beforStartFlow += Flo_beforStartFlow;
                    flo.afterStartFlow += Flo_afterStartFlow;
                    flo.StartFlow(null, "8445", "8445", "AP200709262223001");
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            }
        }

        private static void Flo_afterStartFlow(FlowContent flowContent)
        {
            Console.WriteLine(flowContent.TaskName);
        }

        private static void Flo_beforStartFlow(FlowContent flowContent)
        {
            flowContent.TaskName = "测试节点";
        }
    }
}
