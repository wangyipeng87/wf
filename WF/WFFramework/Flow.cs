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
    public abstract class Flow
    {
        WF_InstanceBll instancebll = new WF_InstanceBll();
        WF_TemplateNodeBll nodebll = new WF_TemplateNodeBll();
        WF_TemplateBll tmpbll = new WF_TemplateBll();
        FlowOperationHistory operationbll = new FlowOperationHistory();
        WF_ToDoBll todobll = new WF_ToDoBll();
        WF_RuleBll rulebll = new WF_RuleBll();
        public string Tmpkey { get; set; }
        public int InstanceID { get; set; }
        public string ApplyUserCode { get; set; }
        public string WriterUserCode { get; set; }
        public string FormID { get; set; }
        public string FormName { get; set; }
        public int InstanceState { get; set; }
        public int TodoID { get; set; }
        public string CurrenUserCode { get; set; }

        protected event FlowEvent beforStartFlow;
        protected event FlowEvent afterStartFlow;
        protected event FlowEvent beforOperation;
        protected event FlowEvent afterOperation;
        protected event FlowEvent endFlow;
        public abstract void Init();
        /// <summary>
        /// 流程启动
        /// </summary>
        /// <param name="vallist">流程实例变量</param>
        /// <param name="applyUserCode">申请人员工编号</param>
        /// <param name="writerUserCode">填表人员工编号</param>
        /// <param name="formID">单据编号</param>
        public void StartFlow(Dictionary<string, string> vallist, string applyUserCode, string writerUserCode, string formID)
        {
            //插入流程实例
            int instanceID = this.InsertInstance((int)WF.Common.InstanceState.Enable, formID, this.CurrenUserCode, writerUserCode, applyUserCode);
            WF_Template tmp= tmpbll.getByKey(this.Tmpkey);
            this.InstanceID = instanceID;
            this.ApplyUserCode = applyUserCode;
            this.WriterUserCode = writerUserCode;
            this.FormID = formID;
            this.InstanceState = (int)WF.Common.InstanceState.Enable;

            //触发启动前事件
            FlowContent flowcontent = new FlowContent();
            flowcontent.CurrenUserCode = this.CurrenUserCode;
            flowcontent.TmpKey = this.Tmpkey;
            flowcontent.CurrentInstanceID = instanceID;
            flowcontent.OperationType = (int)Common.Operation.Start;
            flowcontent.TaskName = tmp.TmpName;
            flowcontent.InstanceState = (int)WF.Common.InstanceState.Enable;
            if(this.beforStartFlow!=null)
            {
                this.beforStartFlow(flowcontent);
            }

            //更新流程变量
            FlowVar var = new FlowVar();
            var.InstanceID = instanceID;
            var.TmpKey = this.Tmpkey;
            //先将流程模板变量copy到流程实例
            var.copyVarByTmpKey(this.CurrenUserCode);
            //更新流程实例的变量
            var.UpdateVal(vallist,this.CurrenUserCode);
            //获取第一个节点
            List<FlowNode> firstNode = this.GetFirstNode();
            //获取当前待办人的编号
            List<string> newtodis = new List<string>();
            List<string> newnodekey = new List<string>();
            if (firstNode != null && firstNode.Count > 0)
            {
                foreach (FlowNode node in firstNode)
                {
                    newnodekey.Add(node.NodeKey);
                    flowcontent.CurrentNodeKey = node.NodeKey;
                    List<string> userCodeList = node.Run(flowcontent);
                    //循环遍历插入待办
                    if (userCodeList != null && userCodeList.Count > 0)
                    {
                        ToDoHandle todo = new ToDoHandle();
                        foreach (string item in userCodeList)
                        {
                            int todoid = todo.InsertTodo(item.Trim(), instanceID,(int)TodoIsShow.Show,-1, flowcontent.TaskName, (int)TodoType.Normal, node, node.NodeKey,CurrenUserCode);
                            newtodis.Add(todoid.ToString());
                        }
                    }
                }
            }
            operationbll.Insert(instanceID, -1, CurrenUserCode, (int)Common.Operation.Start, "启动流程");
            flowcontent.CurrentNodeKey = string.Join(", ", newnodekey); ;
            flowcontent.CurrentTodoID = string.Join(", ", newtodis);
            //触发启动后事件
            if (this.afterStartFlow != null)
            {
                this.afterStartFlow(flowcontent);
            }
        }

        /// <summary>
        /// 待办处理操作
        /// </summary>
        /// <param name="vallist">流程实例变量</param>
        /// <param name="todoID">待办id</param>
        /// <param name="operationUserCode">操作人工号 </param>
        /// <param name="operationType">操作类型</param>
        public void Operation(Dictionary<string, string> vallist,int todoID ,string operationUserCode, Operation operationType)
        {
            //禁止流程启动
            if(operationType==Common.Operation.Start)
            {
                throw new Exception("不能执行流程启动操作");
            }
            if(this.beforOperation!=null)
            {
                WF_ToDo todo= todobll.getByID(todoID);
                WF_Instance instance= instancebll.getByID(todo.InstanceID);
                FlowContent flowcontent = new FlowContent();
                flowcontent.CurrenUserCode = this.CurrenUserCode;
                flowcontent.TmpKey = this.Tmpkey;
                flowcontent.CurrentInstanceID = todo.InstanceID;
                flowcontent.OperationType = (int)Common.Operation.Start;
                flowcontent.TaskName = todo.ToDoName;
                flowcontent.InstanceState = instance.State;
                flowcontent.CurrentNodeKey=todo.Nodekey;
                flowcontent.OperationType = (int)operationType;
                flowcontent.CurrentTodoID = todo.ID.ToString(); ;
                this.beforOperation(flowcontent);
            }

            if (this.afterOperation != null)
            {

            }
        }
        private int InsertInstance(int instanceState, string formID, string createUserCode, string writerUserCode, string applyUserCode)
        {
            WF_Instance instance = new WF_Instance();
            instance.ApplyUserCode = applyUserCode;
            instance.CreateTime = DateTime.Now;
            instance.CreateUserCode = createUserCode;
            instance.FormID = formID;
            instance.IsDelete = (int)IsDelete.UnDelete;
            instance.State = instanceState;
            instance.TmpKey = this.Tmpkey;
            instance.UpdateTime = DateTime.Now;
            instance.UpdateUserCode = createUserCode;
            instance.WriterUserCode = writerUserCode;
            return instancebll.save(instance);
        }

        /// <summary>
        /// 获取申请节点后的节点
        /// </summary>
        /// <returns></returns>
        private List<FlowNode> GetFirstNode()
        {
            List<FlowNode> firstNodeList = new List<FlowNode>();
            List<WF_TemplateNode> nodelist= nodebll.getAllByTmpKey(this.Tmpkey);
            List<WF_Rule> rulelist= rulebll.getAllByTmpKey(this.Tmpkey);
            WF_TemplateNode beginNode = null;
            if (nodelist!=null&&nodelist.Count>0)
            {
                foreach (WF_TemplateNode item in nodelist)
                {
                    if(item.NodeType==(int)WFNodeType.BeginNode)
                    {
                        beginNode = item;
                        break;
                    }
                }
            }
           
            if(beginNode!=null)
            {
                FlowNode firstnode = NodeFactory.getFlowNode(beginNode.Tmpkey,beginNode.Nodekey,this.endFlow);
                FlowContent flowcontent = new FlowContent();
                flowcontent.CurrentInstanceID =this.InstanceID;
                firstNodeList = firstnode.GetNextNode(flowcontent);
            }
            return firstNodeList;
        }

    }
}
