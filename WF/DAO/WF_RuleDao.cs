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
    public class WF_RuleDao
    {

        public bool save(WF_Rule entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                conn.Insert<WF_Rule>(entity);
                return true;
            }
        }
        public bool update(WF_Rule entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Update<WF_Rule>(entity);
            }
        }
        public bool del(int id)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                WF_Rule Role = conn.Get<WF_Rule>(id);
                Role.IsDelete = 1;
                return conn.Update<WF_Rule>(Role);
            }
        }
        public int DelByTmpKey(string tmpKey)
        {
            string sql = @"    DELETE FROM WF_Rule WHERE Tmpkey=@tmpkey";

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Execute(sql, new { tmpkey = tmpKey });
            }
        }
    }
}


