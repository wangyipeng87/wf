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
    public class Flow
    {
        WF_InstanceBll instancebll = new WF_InstanceBll();
        WF_TemplateNodeBll nodebll = new WF_TemplateNodeBll();
        WF_RuleBll rulebll = new WF_RuleBll();
        public string tmpkey { get; set; }
        public int InstanceID { get; set; }
        public string ApplyUserCode { get; set; }
        public string WriterUserCode { get; set; }
        public string FormID { get; set; }
        public int InstanceState { get; set; }
        public int TodoID { get; set; }
        public string CurrenUserCode { get; set; }

        public event FlowEvent beforStartFlow;
        public event FlowEvent afterStartFlow;
        public event FlowEvent beforOperation;
        public event FlowEvent afterOperation;
        public void StartFlow(Dictionary<string, string> vallist, string tmpkey, string applyUserCode, string writerUserCode, string formID)
        {
            //插入流程实例
            int instanceID = this.InsertInstance(tmpkey, formID, this.CurrenUserCode, writerUserCode, applyUserCode);
            this.InstanceID = instanceID;
            this.ApplyUserCode = applyUserCode;
            this.WriterUserCode = writerUserCode;
            this.FormID = FormID;

            //触发启动前事件
            FlowContent flowcontent = new FlowContent();
            flowcontent.currenUserCode = this.CurrenUserCode;
            flowcontent.tmpKey = tmpkey;
            flowcontent.currentInstanceID = instanceID;
            flowcontent.operationType = (int)Operation.Start;
            this.beforStartFlow(flowcontent);

            //更新流程变量
            FlowVar var = new FlowVar();
            var.InstanceID = instanceID;
            var.TmpKey = tmpkey;
            //先将流程模板变量copy到流程实例
            var.copyVarByTmpKey(this.CurrenUserCode);
            //更新流程实例的变量
            var.UpdateVal(vallist,this.CurrenUserCode);
            //获取第一个节点
            List<FlowNode> firstNode = this.GetFirstNode();
            //获取当前待办人的编号
            if (firstNode != null && firstNode.Count > 0)
            {
                foreach (FlowNode node in firstNode)
                {
                    flowcontent.currentNodeKey = node.NodeKey;
                    List<string> userCodeList = node.GetTodoUser(flowcontent);
                    //循环遍历插入待办
                    if (userCodeList != null && userCodeList.Count > 0)
                    {
                        ToDoHandle todo = new ToDoHandle();
                        FlowOperation operation = new FlowOperation();
                        foreach (string item in userCodeList)
                        {
                            int todoid = todo.InsertTodo(item.Trim(), instanceID, node, node.NodeKey);
                            operation.Insert(todoid, CurrenUserCode, (int)Operation.Start, "启动流程");
                        }
                    }
                }
            }
            flowcontent.currentNodeKey ="";
            //触发启动后事件
            this.afterStartFlow(flowcontent);
        }


        private int InsertInstance(string tmpKey, string formID, string createUserCode, string writerUserCode, string applyUserCode)
        {
            WF_Instance instance = new WF_Instance();
            instance.ApplyUserCode = applyUserCode;
            instance.CreateTime = DateTime.Now;
            instance.CreateUserCode = createUserCode;
            instance.FormID = formID;
            instance.IsDelete = (int)IsDelete.UnDelete;
            instance.State = (int)State.Enable;
            instance.TmpKey = tmpkey;
            instance.UpdateTime = DateTime.Now;
            instance.UpdateUserCode = createUserCode;
            instance.WriterUserCode = writerUserCode;
            return instancebll.save(instance);
        }

        /// <summary>
        /// 获取申请节点后的节点
        /// </summary>
        /// <returns></returns>
        public List<FlowNode> GetFirstNode()
        {
            List<FlowNode> firstNodeList = new List<FlowNode>();
            List<WF_TemplateNode> nodelist= nodebll.getAllByTmpKey(this.tmpkey);
            List<WF_Rule> rulelist= rulebll.getAllByTmpKey(this.tmpkey);
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
                FlowNode firstnode = NodeFactory.getFlowNode(beginNode.Tmpkey,beginNode.Nodekey);
                FlowContent flowcontent = new FlowContent();
                flowcontent.currentInstanceID =this.InstanceID;
                firstNodeList = firstnode.GetNextNode(flowcontent);
            }
            return firstNodeList;
        }

    }
}
