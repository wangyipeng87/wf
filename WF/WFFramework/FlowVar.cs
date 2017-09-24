using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.BLL;
using WF.Entity;

namespace WF.WFFramework
{
    public class FlowVar
    {
        WF_InstanceVariableBll bll = new WF_InstanceVariableBll();
        public string TmpKey { get; set; }
        public int InstanceID { get; set; }
        public void UpdateVal(Dictionary<string, string> valList, string operationUserCode)
        {
            if (valList != null && valList.Count > 0)
            {
                foreach (string item in valList.Keys)
                {
                    WF_InstanceVariable var = bll.getbyInstanceAndVarname(this.InstanceID, item);
                    if (var != null)
                    {
                        var.DefaultValue = valList[item];
                        var.UpdateTime = DateTime.Now;
                        var.UpdateUserCode = operationUserCode;
                        bll.Update(var);
                    }
                }

            }
        }
        public void copyVarByTmpKey( string operationUserCode)
        {
            bll.copyVarByTmpKey(this.InstanceID, this.TmpKey, operationUserCode);
        }

    }
}
