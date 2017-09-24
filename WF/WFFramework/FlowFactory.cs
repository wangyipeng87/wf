using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.BLL;
using WF.Entity;

namespace WF.WFFramework
{
    public  class FlowFactory
    {
        static WF_TemplateBll  tmpbll = new WF_TemplateBll();
        public static Flow getFlow(string tmpkey) {
            Flow flo = new Flow();
            flo.tmpkey = tmpkey;
            return flo;
        }
        public static Flow getFlowByTodo(int todoid)
        {
            return new Flow();
        }
    }
}
