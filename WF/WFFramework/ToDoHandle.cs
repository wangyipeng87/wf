using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.BLL;
using WF.Entity;

namespace WF.WFFramework
{
    public class ToDoHandle
    {
        WF_ToDoBll todobll = new WF_ToDoBll();
        public int InsertTodo(string todoUserCode, int instanceID, FlowNode node, string nodeKey)
        {
            WF_ToDo todo = new WF_ToDo();
            
            throw new NotImplementedException();
        }

        public int DealTodo(int OperationType, string OperationUserCode, int TodoID)
        {
            throw new NotImplementedException();
        }
    }
}
