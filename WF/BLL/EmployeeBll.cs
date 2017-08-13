using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WF.Entity;
using WF.DAO;

namespace WF.BLL
{
    public class EmployeeBll
    {
        public Employee getbyAccount(string account)
        {
            EmployeeDao dao = new EmployeeDao();
            return dao.getbyAccount(account);
        }
        public Employee getbyAccountAndPwd(string account, string pwd)
        {
            EmployeeDao dao = new EmployeeDao();
            return dao.getbyAccountAndPwd(account, pwd);
        }
        public List<Employee> getAll(string key, string linemanage, int state, int begin, int end, string order, out int count)
        {
            EmployeeDao dao = new EmployeeDao();
            return dao.getAll( key, linemanage,  state,  begin,  end,  order,out count);
        }
    }
}


