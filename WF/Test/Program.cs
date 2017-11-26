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
                    Flow flo = FlowFactory.getFlow("key1", "8454");
                    flo.StartFlow(null, "8454", "4045", "APT0091");
                    //同意
                    //Flow flo = FlowFactory.getFlowByTodo(64, "7352");
                    //Dictionary<string, string> vallist = new Dictionary<string, string>();
                    //vallist.Add("a","2");
                    //flo.Operation(vallist, 64, "7352", WF.Common.Operation.Agree, "驳回测试");
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
