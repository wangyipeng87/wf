using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.Common;

namespace WF.WFFramework
{
  public  class FlowContent
    {

        public int OperationType { get; set; }
        public string TmpKey { get; set; }
        public string TaskName { get; set; }
        public int InstanceState { get; set; }
        
        public int CurrentInstanceID { get; set; }
        public string CurrentNodeKey { get; set; }
        public string CurrentTodoID { get; set; }
        public string CurrenUserCode { get; set; }

    }
}
