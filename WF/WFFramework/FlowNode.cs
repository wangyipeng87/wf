using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.BLL;
using WF.Entity;

namespace WF.WFFramework
{
    public abstract class FlowNode
    {
       
        WF_RuleBll rulebll = new WF_RuleBll();
        WF_InstanceVariableBll varbll = new WF_InstanceVariableBll();
        WF_TemplateNodeBll nodebll = new WF_TemplateNodeBll();
        public string TmpKey { get; set; }
        public string NodeKey { get; set; }
        public abstract List<string> GetTodoUser(FlowContent flowContent);
       
        public List<FlowNode> GetNextNode(FlowContent flowContent)
        {
            List<WF_Rule> ruleList = rulebll.getRuleByTmpKeyAndBeginNodeKey(this.TmpKey, this.NodeKey);
            List<WF_InstanceVariable> varlist = varbll.getbyInstanceID(flowContent.currentInstanceID);
            List<WF_Rule> enableRule = new List<WF_Rule>();
            List<Val> val = new List<Val>();
            List<WF_TemplateNode> tmpNodeList = new List<WF_TemplateNode>();
            List<FlowNode> flowNodeList = new List<FlowNode>();
            if (varlist != null && varlist.Count > 0)
            {
                foreach (WF_InstanceVariable item in varlist)
                {
                    Val v = new Val();
                    v.name = item.VarName;
                    v.type = item.VarType;
                    v.value = item.DefaultValue;
                    val.Add(v);
                }
            }
            if (ruleList != null && ruleList.Count > 0)
            {
                foreach (WF_Rule item in ruleList)
                {
                    if (RuleEngine.IsConformExpress(val,item.Expression))
                    {
                        enableRule.Add(item);
                    }
                }
            }
            if(enableRule!=null&&enableRule.Count>0)
            {
                foreach (WF_Rule item in enableRule)
                {
                    WF_TemplateNode afternode= nodebll.getByNodeKey(this.TmpKey, item.EndNodekey);
                    tmpNodeList.Add(afternode);
                }
            }
            if (tmpNodeList != null && tmpNodeList.Count > 0)
            {
                foreach (WF_TemplateNode item in tmpNodeList)
                {
                    FlowNode fn= NodeFactory.getFlowNode(item.Tmpkey, item.Nodekey);
                    flowNodeList.Add(fn);
                }
            }
            return flowNodeList;
        }

    }
}
