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
        WF_AgentBll agentBll = new WF_AgentBll();
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
            WF_Template tmp = tmpbll.getByKey(this.Tmpkey);
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
            flowcontent.CurrentNodeKey = "";
            if (this.beforStartFlow != null)
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
            var.UpdateVal(vallist, this.CurrenUserCode);
            //获取第一个节点
            List<FlowNode> firstNode = this.GetFirstNode();
            //获取当前待办人的编号
            List<string> newtodis = new List<string>();
            List<string> newnodekey = new List<string>();
            if (firstNode != null && firstNode.Count > 0)
            {
                foreach (FlowNode node in firstNode)
                {
                    NodeReturn noderet = node.Run(flowcontent);
                    if (!noderet.isOver)
                    {
                        newnodekey.Add(node.NodeKey);
                        List<string> userCodeList = noderet.ToDoUserList;
                        //循环遍历插入待办
                        if (userCodeList != null && userCodeList.Count > 0)
                        {
                            foreach (string item in userCodeList)
                            {
                                int todoid = ToDoHandle.InsertTodo(item.Trim(), instanceID, (int)TodoIsShow.Show, -1, flowcontent.TaskName, (int)TodoType.Normal, node, node.NodeKey, CurrenUserCode);
                                newtodis.Add(todoid.ToString());
                            }
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
        public void Operation(Dictionary<string, string> vallist, int todoID, string operationUserCode, Operation operationType, string common = null, string toNodeKey = null,string todoUserCode=null)
        {
            //禁止流程启动
            if (operationType == Common.Operation.Start)
            {
                throw new Exception("不能执行流程启动操作");
            }
            WF_ToDo todo = todobll.getByID(todoID);
            WF_Instance instance = instancebll.getByID(todo.InstanceID);

            if(instance.State==(int)Common.InstanceState.Enable)
            {
                throw new Exception("当前流程实例状态不是启用状态");
            }
            if (todo.ResponseUserCode != operationUserCode)
            {
                List<WF_Agent> agentlist = agentBll.getAgentByOrg(todo.ResponseUserCode);
                if (agentlist == null || agentlist.Count == 0 || agentlist.Where(p => p.AgentUserCode == operationUserCode).Count() == 0)
                {
                    throw new Exception("当前操作人不是待办责任人，也不是待办责任人的代理人");
                }
            }
            if (todo.State == (int)Common.TodoState.UnDo)
            {
                throw new Exception("当前待办已经被处理了");
            }
            FlowContent flowcontent = new FlowContent();
            flowcontent.CurrenUserCode = operationUserCode;
            flowcontent.TmpKey = this.Tmpkey;
            flowcontent.CurrentInstanceID = todo.InstanceID;
            flowcontent.OperationType = (int)Common.Operation.Start;
            flowcontent.TaskName = todo.ToDoName;
            flowcontent.InstanceState = instance.State;
            flowcontent.CurrentNodeKey = todo.Nodekey;
            flowcontent.OperationType = (int)operationType;
            flowcontent.CurrentTodoID = todo.ID.ToString();

            if (this.beforOperation != null)
            {
                this.beforOperation(flowcontent);
            }
            FlowNode node = NodeFactory.getFlowNode(instance.TmpKey, todo.Nodekey, this.endFlow);
            // 处理 同意
            if (operationType == Common.Operation.Apply)
            {
                this.Apply(vallist, todoID, operationUserCode, operationType, common, flowcontent, node);
            }
            //todo 处理 撤回
            //if (operationType == Common.Operation.CallBack)
            //{
                //this.Apply(vallist, todoID, operationUserCode, operationType, common, flowcontent, node);
            //}
            // 处理 流程跳转
            if (operationType == Common.Operation.GoTo)
            {
                this.GoTo(vallist, todoID, operationUserCode, operationType, common, flowcontent, node, toNodeKey);
            }
            //todo 处理 传阅
            //if (operationType == Common.Operation.Read)
            //{
            //    this.Apply(vallist, todoID, operationUserCode, operationType, common, flowcontent, node);
            //}
            // 处理 驳回
            if (operationType == Common.Operation.Reject)
            {
                this.Reject(vallist, todoID, operationUserCode, operationType, common, flowcontent, node,toNodeKey);
            }
            // 处理 转签
            if (operationType == Common.Operation.Redirect)
            {
                this.Redirect(vallist, todoID, operationUserCode, operationType, common, flowcontent, node, todoUserCode);
            }
            // 处理 加签
            if (operationType == Common.Operation.Add)
            {
                this.Add(vallist, todoID, operationUserCode, operationType, common, flowcontent, node, todoUserCode);
            }
            if (this.afterOperation != null)
            {
                this.afterOperation(flowcontent);
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
        /// 同意
        /// </summary>
        /// <param name="vallist"></param>
        /// <param name="todoID"></param>
        /// <param name="operationUserCode"></param>
        /// <param name="operationType"></param>
        /// <param name="common"></param>
        /// <param name="flowcontent"></param>
        /// <param name="node"></param>
        private void Apply(Dictionary<string, string> vallist, int todoID, string operationUserCode, Operation operationType, string common, FlowContent flowcontent, FlowNode node)
        {
            ToDoHandle.DealTodo((int)operationType, operationUserCode, todoID);
            FlowVar var = new FlowVar();
            var.UpdateVal(vallist, this.CurrenUserCode);
            WF_ToDo todo = todobll.getByID(todoID);
            // 加签类型
            if(todo.TodoType==(int)TodoType.Add)
            {
                WF_ToDo nextodo = todobll.getByID(todo.PrevID);
                int newtodiid= ToDoHandle.Reopen(nextodo.ResponseUserCode, nextodo.InstanceID, nextodo.IsShow, todoID, nextodo.ToDoName, nextodo.TodoType,node, nextodo.Nodekey,operationUserCode);
                flowcontent.CurrentTodoID = string.Join(", ", newtodiid);
            }
            // 一般同意 转签类型
            if (todo.TodoType == (int)TodoType.Normal|| todo.TodoType == (int)TodoType.Redirect)
            {
                NodeReturn ret = node.Run(flowcontent);

                //获取当前待办人的编号
                List<string> newtodis = new List<string>();
                List<string> newnodekey = new List<string>();
                if (ret.isOver)
                {
                    List<FlowNode> nextNode = node.GetNextNode(flowcontent);
                    if (nextNode != null && nextNode.Count > 0)
                    {
                        foreach (FlowNode nxitem in nextNode)
                        {
                            NodeReturn noderet = nxitem.Run(flowcontent);
                            if (!noderet.isOver)
                            {
                                newnodekey.Add(nxitem.NodeKey);
                                List<string> userCodeList = noderet.ToDoUserList;
                                //循环遍历插入待办
                                if (userCodeList != null && userCodeList.Count > 0)
                                {
                                    foreach (string user in userCodeList)
                                    {
                                        int todoid = ToDoHandle.InsertTodo(user.Trim(), flowcontent.CurrentInstanceID, (int)TodoIsShow.Show, -1, flowcontent.TaskName, (int)TodoType.Normal, nxitem, nxitem.NodeKey, CurrenUserCode);
                                        newtodis.Add(todoid.ToString());
                                    }
                                }
                            }
                        }
                    }
                    flowcontent.CurrentNodeKey = string.Join(", ", newnodekey);
                    flowcontent.CurrentTodoID = string.Join(", ", newtodis);
                }
                else
                {
                    List<string> userCodeList = ret.ToDoUserList;
                    //循环遍历插入待办
                    if (userCodeList != null && userCodeList.Count > 0)
                    {
                        foreach (string user in userCodeList)
                        {
                            int todoid = ToDoHandle.InsertTodo(user.Trim(), flowcontent.CurrentInstanceID, (int)TodoIsShow.Show, -1, flowcontent.TaskName, (int)TodoType.Normal, node, node.NodeKey, CurrenUserCode);
                            newtodis.Add(todoid.ToString());
                        }
                    }

                    flowcontent.CurrentTodoID = string.Join(", ", newtodis);
                }
            }
            operationbll.Insert(flowcontent.CurrentInstanceID, todoID, CurrenUserCode, (int)operationType, common);
        }

        /// <summary>
        /// 跳转
        /// </summary>
        /// <param name="vallist"></param>
        /// <param name="todoID"></param>
        /// <param name="operationUserCode"></param>
        /// <param name="operationType"></param>
        /// <param name="common"></param>
        /// <param name="flowcontent"></param>
        /// <param name="node"></param>
        private void GoTo(Dictionary<string, string> vallist, int todoID, string operationUserCode, Operation operationType, string common, FlowContent flowcontent, FlowNode node, string toNodeKey)
        {
            ToDoHandle.DealTodo((int)operationType, operationUserCode, todoID);
            FlowVar var = new FlowVar();
            var.UpdateVal(vallist, this.CurrenUserCode);

            List<WF_ToDo> undolist = todobll.getList(flowcontent.CurrentInstanceID, node.NodeKey, (int)TodoState.UnDo);

            if (undolist != null && undolist.Count > 0)
            {
                foreach (WF_ToDo item in undolist)
                {
                    ToDoHandle.DeleteTodo((int)operationType, operationUserCode, item.ID);
                }
            }
            FlowNode toNode = NodeFactory.getFlowNode(flowcontent.TmpKey, toNodeKey, this.endFlow);

            NodeReturn ret = toNode.Run(flowcontent);

            //获取当前待办人的编号
            List<string> newtodis = new List<string>();
            List<string> newnodekey = new List<string>();
            if (!ret.isOver)
            {
                List<string> userCodeList = ret.ToDoUserList;
                //循环遍历插入待办
                if (userCodeList != null && userCodeList.Count > 0)
                {
                    foreach (string user in userCodeList)
                    {
                        int todoid = ToDoHandle.InsertTodo(user.Trim(), flowcontent.CurrentInstanceID, (int)TodoIsShow.Show, -1, flowcontent.TaskName, (int)TodoType.Normal, toNode, toNode.NodeKey, CurrenUserCode);
                        newtodis.Add(todoid.ToString());
                    }
                }
                flowcontent.CurrentTodoID = string.Join(", ", newtodis);
                flowcontent.CurrentNodeKey = toNodeKey;
            }
            operationbll.Insert(flowcontent.CurrentInstanceID, todoID, CurrenUserCode, (int)operationType, common);
        }

        /// <summary>
        /// 驳回
        /// </summary>
        /// <param name="vallist"></param>
        /// <param name="todoID"></param>
        /// <param name="operationUserCode"></param>
        /// <param name="operationType"></param>
        /// <param name="common"></param>
        /// <param name="flowcontent"></param>
        /// <param name="node"></param>
        /// <param name="toNodeKey"></param>
        private void Reject(Dictionary<string, string> vallist, int todoID, string operationUserCode, Operation operationType, string common, FlowContent flowcontent, FlowNode node, string toNodeKey)
        {
            ToDoHandle.DealTodo((int)operationType, operationUserCode, todoID);
            FlowVar var = new FlowVar();
            var.UpdateVal(vallist, this.CurrenUserCode);

            List<WF_ToDo> undolist = todobll.getList(flowcontent.CurrentInstanceID, node.NodeKey, (int)TodoState.UnDo);

            if (undolist != null && undolist.Count > 0)
            {
                foreach (WF_ToDo item in undolist)
                {
                    ToDoHandle.DeleteTodo((int)operationType, operationUserCode, item.ID);
                }
            }
            WF_TemplateNode tmpnode = nodebll.getByNodeKey(flowcontent.TmpKey, node.NodeKey);
            if (tmpnode.IsGoBack == 0)
            {
                throw new Exception("改节点禁止退回");
            }
            if (tmpnode.GoBackType == "every")
            {
                if (string.IsNullOrWhiteSpace(toNodeKey))
                {
                    throw new Exception("请选择需要退回到哪一个节点");
                }
            }
            else
            {
                toNodeKey = tmpnode.GoBackType;
            }
            FlowNode toNode = NodeFactory.getFlowNode(flowcontent.TmpKey, toNodeKey, this.endFlow);

            NodeReturn ret = toNode.Run(flowcontent);

            //获取当前待办人的编号
            List<string> newtodis = new List<string>();
            List<string> newnodekey = new List<string>();
            if (!ret.isOver)
            {
                List<string> userCodeList = ret.ToDoUserList;
                //循环遍历插入待办
                if (userCodeList != null && userCodeList.Count > 0)
                {
                    foreach (string user in userCodeList)
                    {
                        int todoid = ToDoHandle.InsertTodo(user.Trim(), flowcontent.CurrentInstanceID, (int)TodoIsShow.Show, -1, flowcontent.TaskName, (int)TodoType.Normal, toNode, toNode.NodeKey, CurrenUserCode);
                        newtodis.Add(todoid.ToString());
                    }
                }
                flowcontent.CurrentTodoID = string.Join(", ", newtodis);
                flowcontent.CurrentNodeKey = toNodeKey;
            }
            operationbll.Insert(flowcontent.CurrentInstanceID, todoID, CurrenUserCode, (int)operationType, common);
        }

        /// <summary>
        /// 转签
        /// </summary>
        /// <param name="vallist"></param>
        /// <param name="todoID"></param>
        /// <param name="operationUserCode"></param>
        /// <param name="operationType"></param>
        /// <param name="common"></param>
        /// <param name="flowcontent"></param>
        /// <param name="node"></param>
        /// <param name="toNodeKey"></param>
        private void Redirect(Dictionary<string, string> vallist, int todoID, string operationUserCode, Operation operationType, string common, FlowContent flowcontent, FlowNode node, string todoUserCode)
        {
            ToDoHandle.DealTodo((int)operationType, operationUserCode, todoID);
            FlowVar var = new FlowVar();
            var.UpdateVal(vallist, this.CurrenUserCode);
            //获取当前待办人的编号
            List<string> newtodis = new List<string>();
            List<string> newnodekey = new List<string>();
            int todoid = ToDoHandle.InsertTodo(todoUserCode.Trim(), flowcontent.CurrentInstanceID, (int)TodoIsShow.Show, todoID, flowcontent.TaskName, (int)TodoType.Redirect, node, node.NodeKey, CurrenUserCode);
            newtodis.Add(todoid.ToString());
            flowcontent.CurrentTodoID = string.Join(", ", newtodis);
            flowcontent.CurrentNodeKey = node.NodeKey;
            operationbll.Insert(flowcontent.CurrentInstanceID, todoID, CurrenUserCode, (int)operationType, common);
        }
        /// <summary>
        /// 加签
        /// </summary>
        /// <param name="vallist"></param>
        /// <param name="todoID"></param>
        /// <param name="operationUserCode"></param>
        /// <param name="operationType"></param>
        /// <param name="common"></param>
        /// <param name="flowcontent"></param>
        /// <param name="node"></param>
        /// <param name="toNodeKey"></param>
        private void Add(Dictionary<string, string> vallist, int todoID, string operationUserCode, Operation operationType, string common, FlowContent flowcontent, FlowNode node, string todoUserCode)
        {
            ToDoHandle.DealTodo((int)operationType, operationUserCode, todoID);
            FlowVar var = new FlowVar();
            var.UpdateVal(vallist, this.CurrenUserCode);
            //获取当前待办人的编号
            List<string> newtodis = new List<string>();
            List<string> newnodekey = new List<string>();
            int todoid = ToDoHandle.InsertTodo(todoUserCode.Trim(), flowcontent.CurrentInstanceID, (int)TodoIsShow.Show, todoID, flowcontent.TaskName, (int)TodoType.Add, node, node.NodeKey, CurrenUserCode);
            newtodis.Add(todoid.ToString());
            flowcontent.CurrentTodoID = string.Join(", ", newtodis);
            flowcontent.CurrentNodeKey = node.NodeKey;
            operationbll.Insert(flowcontent.CurrentInstanceID, todoID, CurrenUserCode, (int)operationType, common);
        }
        /// <summary>
        /// 获取申请节点后的节点
        /// </summary>
        /// <returns></returns>
        private List<FlowNode> GetFirstNode()
        {
            List<FlowNode> firstNodeList = new List<FlowNode>();
            List<WF_TemplateNode> nodelist = nodebll.getAllByTmpKey(this.Tmpkey);
            List<WF_Rule> rulelist = rulebll.getAllByTmpKey(this.Tmpkey);
            WF_TemplateNode beginNode = null;
            if (nodelist != null && nodelist.Count > 0)
            {
                foreach (WF_TemplateNode item in nodelist)
                {
                    if (item.NodeType == (int)WFNodeType.BeginNode)
                    {
                        beginNode = item;
                        break;
                    }
                }
            }

            if (beginNode != null)
            {
                FlowNode firstnode = NodeFactory.getFlowNode(beginNode.Tmpkey, beginNode.Nodekey, this.endFlow);
                FlowContent flowcontent = new FlowContent();
                flowcontent.CurrentInstanceID = this.InstanceID;
                firstNodeList = firstnode.GetNextNode(flowcontent);
            }
            return firstNodeList;
        }

    }
}
