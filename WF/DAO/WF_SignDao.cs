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
    public class WF_SignDao
    {
        public int save(WF_Sign entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                object id = conn.Insert<WF_Sign>(entity);
                return (int)id;
            }
        }
        public bool update(WF_Sign entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Update<WF_Sign>(entity);
            }
        }
        public bool del(int id)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                WF_Sign todo = conn.Get<WF_Sign>(id);
                todo.IsDelete = 1;
                return conn.Update<WF_Sign>(todo);
            }
        }
        public WF_Sign getByID(int id)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Get<WF_Sign>(id);
            }
        }
    }
}


