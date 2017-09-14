using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.Entity
{
   public class WFTmp
    {
        public string tmpkey { get; set; }
        public List<WF_Rule> rulelist { get; set; }
        public List<WF_TemplateNode> nodelist { get; set; }

    }
}
