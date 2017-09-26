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
        EmployeeDao dao = new EmployeeDao();
        public List<Employee> getAll()
        {
            return dao.getAll();
        }
        public Employee getbyAccount(string account)
        {
            return dao.getbyAccount(account);
        }
        public Employee getbyUserCode(string usercode)
        {
            return dao.getbyUserCode(usercode);
        }
        public Employee getbyAccountAndPwd(string account, string pwd)
        {
            return dao.getbyAccountAndPwd(account, pwd);
        }
        public List<Employee> getAll(string key, string linemanage, int state, int begin, int end, string order, out int count)
        {
            return dao.getAll( key, linemanage,  state,  begin,  end,  order,out count);
        }
        public List<Employee> getEmpForAutocomplete(string key)
        {
            return dao.getEmpForAutocomplete(key);
        }
    }
}


