using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.BLL;
using WF.Entity;
using WF.Common;

namespace WF.WFFramework
{
    public class FlowOperationHistory
    {
        WF_ToDoBll todobll = new WF_ToDoBll();
        EmployeeBll empbll = new EmployeeBll();
        WF_AgentBll agentbll = new WF_AgentBll();
        WF_OperationHistoryBll operbll = new WF_OperationHistoryBll();
        public int Insert(int instanceID, int todoID, string operationUserCode, int operationType, string comments)
        {
            WF_OperationHistory history = new WF_OperationHistory();
            Employee emp = empbll.getbyUserCode(operationUserCode);
            history.InstanceID = instanceID;
            history.Comments = comments;
            history.IsDelete = (int)IsDelete.UnDelete;
            history.OperationTime = DateTime.Now;
            history.OperationType = operationType;
            WF_ToDo todo = todobll.getByID(todoID);
            history.State = (int)State.Enable;
            if (todo != null)
            {
                history.TodoID = todoID;
                if (todo.ResponseUserCode == operationUserCode)
                {
                    history.OperationUserCode = operationUserCode;
                    history.OperationUserName = emp.UserName;
                }
                else
                {
                    history.AgentUserCode = operationUserCode;
                    history.AgentUserName = emp.UserName;
                }
            }
            else
            {
                history.OperationUserCode = operationUserCode;
                history.OperationUserName = emp.UserName;
                history.TodoID = -1;

            }
            return operbll.save(history);
        }
    }
}
