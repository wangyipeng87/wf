using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.Entity
{
    public class TreeView
    {
        public string id { get; set; }
        public string text { get; set; }
        public string state
        {
            get;
            set;
        }
        public string attributes { get; set; }
        private List<TreeView> _children { get; set; }
        public List<TreeView> children
        {
            get
            {
                if (_children == null)
                {
                    _children = new List<TreeView>();

                }
                return _children;
            }
            set { _children = value; }

        }
    }
}
