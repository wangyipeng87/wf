using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WF.BLL;
using WF.Entity;
using WF.WFFramework.WFFlow;

namespace WF.WFFramework
{
    public  class FlowFactory
    {
        static WF_TemplateBll  tmpbll = new WF_TemplateBll();
        static WF_InstanceBll instancebll = new WF_InstanceBll();
        static WF_ToDoBll todobll = new WF_ToDoBll();
        public static Flow getFlow(string tmpkey, string currenUserCode) {
            WF_Template tmp = tmpbll.getByKey(tmpkey);
            Flow flo = GetFlowByTmpKey(tmpkey);
            flo.Tmpkey = tmpkey;
            flo.FormName = tmp.TmpName;
            flo.CurrenUserCode = currenUserCode;
            flo.Init();
            return flo;
        }
        public static Flow getFlowByTodo(int todoid, string currenUserCode)
        {
            WF_ToDo todo = todobll.getByID(todoid);
            WF_Instance instance = instancebll.getByID(todo.InstanceID);
            WF_Template tmp = tmpbll.getByKey(instance.TmpKey);
          
            Flow flo = GetFlowByTmpKey(tmp.key);
            flo.Tmpkey = instance.TmpKey;
            flo.FormName = tmp.TmpName;
            flo.CurrenUserCode = currenUserCode;
            flo.ApplyUserCode = instance.ApplyUserCode;
            flo.FormID = instance.FormID;
            flo.InstanceID = instance.ID;
            flo.InstanceState = instance.State;
            flo.TodoID = todoid;
            flo.WriterUserCode = instance.WriterUserCode;
            
            flo.Init();
            return flo;
        }
        private static Flow GetFlowByTmpKey(string tmpkey)
        {
            WF_Template tmp= tmpbll.getByKey(tmpkey);
            Assembly assembly = Assembly.GetExecutingAssembly();
            dynamic obj = assembly.CreateInstance(tmp.ClassName);
            Flow flo = (Flow)obj;
            return flo;
        }
    }
}
