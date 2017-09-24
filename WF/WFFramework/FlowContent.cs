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

        public int operationType { get; set; }
        public string tmpKey { get; set; }
        public int currentInstanceID { get; set; }
        public int currentTodoID { get; set; }
        public string currenUserCode { get; set; }

    }
}
