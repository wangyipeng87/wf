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
        public static Flow getFlow(string tmpkey, string currenUserCode) {
            Flow flo = new Flow();
            flo.tmpkey = tmpkey;
            flo.CurrenUserCode = currenUserCode;
            return flo;
        }
        public static Flow getFlowByTodo(int todoid, string currenUserCode)
        {
            return new Flow();
        }
    }
}
