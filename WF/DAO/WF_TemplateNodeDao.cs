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
    public class WF_TemplateNodeDao
    {
        public bool save(WF_TemplateNode entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                conn.Insert<WF_TemplateNode>(entity);
                return true;
            }
        }
        public bool update(WF_TemplateNode entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Update<WF_TemplateNode>(entity);
            }
        }
        public bool del(int id)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                WF_TemplateNode Role = conn.Get<WF_TemplateNode>(id);
                Role.IsDelete = 1;
                return conn.Update<WF_TemplateNode>(Role);
            }
        }
        public int DelByTmpKey(string tmpKey)
        {
            string sql = @"    DELETE FROM WF_TemplateNode WHERE Tmpkey=@tmpkey";
             
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Execute(sql, new { tmpkey = tmpKey });
            }
        }
    }
}


