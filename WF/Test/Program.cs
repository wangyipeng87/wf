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
                    //启动 
                    //Flow flo = FlowFactory.getFlow("key1", "8454");
                    //flo.StartFlow(null, "8445", "8445", "AP200709262223001");
                    //同意
                    Flow flo = FlowFactory.getFlowByTodo(34, "4045");
                    flo.Operation(null, 34, "4045", WF.Common.Operation.Apply,"同意测试");
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
