using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.Entity;

namespace WF.WFFramework
{
  public  class Flow
    {
        public string tmpkey { get; set; }
        public int InstanceID { get; set; }
        public string ApplyUserCode { get; set; }
        public string WriterUserCode { get; set; }
        public int InstanceState { get; set; }
        public int TodoID { get; set; }
        public int NodeKey { get; set; }
        public event FlowEvent beforStartFlow;
        public event FlowEvent afterStartFlow;
        public event FlowEvent beforApply;
        public event FlowEvent afterApply;
        public event FlowEvent beforReject;
        public event FlowEvent afterReject;
        public event FlowEvent beforGoTo;
        public event FlowEvent afterGoTo;
        public event FlowEvent beforTransfer;
        public event FlowEvent afterTransfer;
        public event FlowEvent beforAdd;
        public event FlowEvent afterAdd;
        public event FlowEvent beforRead;
        public event FlowEvent afterRead;
        public event FlowEvent beforCallBack;
        public event FlowEvent afterCallBack;
        public void StartFlow(List<Dictionary<string,string>> vallist, string tmpkey,string ApplyUserCode,string WriterUserCode,string FormID) {

            
        }


        private WF_TemplateNode GetFirst(string tmpkey) {

            return null;
        }
    }
}
