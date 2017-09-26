﻿using System;
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
            WF_Template tmp = tmpbll.getByKey(tmpkey);
            Flow flo = new Flow();
            flo.Tmpkey = tmpkey;
            flo.FormName = tmp.TmpName;
            flo.CurrenUserCode = currenUserCode;
            return flo;
        }
        public static Flow getFlowByTodo(int todoid, string currenUserCode)
        {
            return new Flow();
        }
    }
}
