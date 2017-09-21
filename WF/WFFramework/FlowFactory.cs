using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.WFFramework
{
    public  class FlowFactory
    {
        public static Flow getFlow(string tmpkey) {

            return new Flow();
        }
        public static Flow getFlowByTodo(int todoid)
        {
            return new Flow();
        }
    }
}
