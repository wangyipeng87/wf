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
    }
}


