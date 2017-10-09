using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.BLL;
using WF.Common;
using WF.Entity;

namespace WF.WFFramework
{
    public static class ToDoHandle
    {
         static WF_ToDoBll todobll = new WF_ToDoBll();
        static WF_TemplateNodeBll nodebll = new WF_TemplateNodeBll();
        static WF_InstanceBll instanceBll = new WF_InstanceBll();
        public static int InsertTodo(string todoUserCode, int instanceID, int isShow, int prevID, string taskName, int todotype, FlowNode node, string nodeKey, string currenUserCode)
        {
            WF_ToDo todo = new WF_ToDo();
            WF_Instance instance = instanceBll.getByID(instanceID);
            WF_TemplateNode tmpNode = nodebll.getByNodeKey(instance.TmpKey, nodeKey);
            //todo  插入批号
            todo.Batch = -1;
            todo.CreateTime = DateTime.Now;
            todo.CreateUserCode = currenUserCode;
            todo.InstanceID = instanceID;
            todo.IsDelete = 0;
            todo.IsShow = isShow;
            todo.Nodekey = nodeKey;
            todo.PrevID = prevID;
            todo.ResponseUserCode = todoUserCode;
            todo.State = (int)TodoState.UnDo;
            todo.ToDoName = taskName;
            todo.TodoType = todotype;
            todo.UpdateTime = DateTime.Now;
            todo.UpdateUserCode = currenUserCode;
            todo.URL = tmpNode.URL; 
            int todoid = todobll.save(todo);
            return todoid;
        }

        public static void DealTodo(int operationType, string operationUserCode, int todoID)
        {
            WF_ToDo todo = todobll.getByID(todoID);
            todo.DealTime = DateTime.Now;
            todo.DealUserCode = operationUserCode;
            todo.OperationType = operationType;
            todo.UpdateTime = DateTime.Now;
            todo.UpdateUserCode = operationUserCode;
            todo.State= (int)TodoState.Done;
            todobll.update(todo);
        }
        public static int Reopen(string todoUserCode, int instanceID, int isShow, int prevID, string taskName, int todotype, FlowNode node, string nodeKey, string currenUserCode)
        {
            WF_ToDo todo = new WF_ToDo();
            WF_Instance instance = instanceBll.getByID(instanceID);
            WF_TemplateNode tmpNode = nodebll.getByNodeKey(instance.TmpKey, nodeKey);
            //todo  插入批号
            todo.Batch = -1;
            todo.CreateTime = DateTime.Now;
            todo.CreateUserCode = currenUserCode;
            todo.InstanceID = instanceID;
            todo.IsDelete = 0;
            todo.IsShow = isShow;
            todo.Nodekey = nodeKey;
            todo.PrevID = prevID;
            todo.ResponseUserCode = todoUserCode;
            todo.State = (int)TodoState.UnDo;
            todo.ToDoName = taskName;
            todo.TodoType = todotype;
            todo.UpdateTime = DateTime.Now;
            todo.UpdateUserCode = currenUserCode;
            todo.URL = tmpNode.URL;
            int todoid = todobll.save(todo);
            return todoid;
        }
    }
}
