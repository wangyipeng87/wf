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

        public void StartFlow(string tmpkey,string ApplyUserCode,string WriterUserCode,string FormID) {


        }
        private WF_TemplateNode GetFirst(string tmpkey) {

            return null;
        }
    }
}
