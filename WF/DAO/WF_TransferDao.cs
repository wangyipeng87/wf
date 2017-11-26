using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WF.Entity;

namespace WF.DAO
{
    public class WF_TransferDao
    {
        public int save(WF_Transfer entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                object id = conn.Insert<WF_Transfer>(entity);
                return (int)id;
            }
        }
        public bool update(WF_Transfer entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Update<WF_Transfer>(entity);
            }
        }
        public bool del(int id)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                WF_Transfer todo = conn.Get<WF_Transfer>(id);
                todo.IsDelete = 1;
                return conn.Update<WF_Transfer>(todo);
            }
        }
        public WF_Transfer getByID(int id)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Get<WF_Transfer>(id);
            }
        }
    }
}


