using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WF.Common;
using WF.DAO;
using WF.Entity;
namespace WF.BLL
{
    public class DepartmentBLL
    {
        private DepartmentDao dao = new DepartmentDao();
        public List<Department> getAll(string rootdeptcode)
        {
            return dao.getAll(rootdeptcode);
        }
        public string getDepart(string rootdeptcode)
        {
            List<Department> departlist = dao.getAll(rootdeptcode);
            TreeView tree = new TreeView();
            tree.attributes = "0";
            initTree(tree, departlist, rootdeptcode,true);
            List<TreeView> treelist = new List<TreeView>();
            treelist.Add(tree);
            return treelist.ToJson();
        }

        private void initTree(TreeView tree, List<Department> departlist, string rootdeptcode, bool isroot)
        {
            Department depart = departlist.Where(p => p.DeptCode == rootdeptcode && p.State == 1).FirstOrDefault();
            tree.id = depart.DeptCode;
            tree.text = depart.DeptName;
            List<Department> childs = departlist.Where(p => p.ParentCode == rootdeptcode && p.State == 1).ToList();
            if (childs != null && childs.Count > 0)
            {
                string i = (Convert.ToInt32(tree.attributes) + 1).ToString();
                List<TreeView> sublist = new List<TreeView>();
                tree.children = sublist;
                foreach (Department departitem in childs)
                {
                    TreeView sub = new TreeView();
                    sublist.Add(sub);
                    sub.id = departitem.DeptCode;
                    sub.text = departitem.DeptName;
                    sub.attributes = i;
                    if (departlist.Where(p => p.ParentCode == departitem.DeptCode && p.State == 1).Count() > 0)
                    {
                        initTree(sub, departlist, departitem.DeptCode,false);
                    }
                }
            }
            if (!isroot && tree.children != null && tree.children.Count() > 0)
            {
                tree.state = "closed";
            }
        }
    }
}


