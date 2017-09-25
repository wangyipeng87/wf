﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WF.BLL;
using WF.Common;
using WF.Entity;
using WF.WFFramework.WFNode;

namespace WF.WFFramework
{
    public class NodeFactory
    {
        static WF_TemplateNodeBll nodebll = new WF_TemplateNodeBll();
        static WF_ApplyTypeBll applytypebll = new WF_ApplyTypeBll();

        public static FlowNode getFlowNode(string tmpkey, string nodeKey)
        {
            FlowNode flo = GetNodeByNodeType(tmpkey, nodeKey);
            flo.NodeKey = nodeKey;
            flo.TmpKey = tmpkey;
            return flo;
        }
        public static FlowNode getFlowNode(string tmpkey, string nodeKey,int instanceID)
        {
            FlowNode flo = GetNodeByNodeType(tmpkey, nodeKey);
            flo.NodeKey = nodeKey;
            flo.TmpKey = tmpkey;
            return flo;
        }
        private static FlowNode GetNodeByNodeType(string tmpkey, string nodeKey) {
            WF_TemplateNode node = nodebll.getByNodeKey(tmpkey, nodeKey);
            FlowNode flo = null;
            if (node.ProcessType==(int)WFProcessType.User)
            {
                flo = new UserNode();
            }
            if (node.ProcessType == (int)WFProcessType.Role)
            {
                flo = new RoleNode();
            }
            if (node.ProcessType == (int)WFProcessType.Custom)
            {
                WF_ApplyType apply= applytypebll.getByCode(node.ProcessTypeValue);
                Assembly assembly = Assembly.GetExecutingAssembly(); 
                dynamic obj = assembly.CreateInstance(apply.ClassName); 
                flo = (FlowNode)obj;
            }
            return flo;
        }
    }
}
