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
    public class WF_InstanceDao
    {
        public int save(WF_Instance entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                int id = conn.Insert<WF_Instance>(entity);
                return id;
            }
        }
        public WF_Instance getByID(int id)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Get<WF_Instance>(id);
            }
        }
        public bool update(WF_Instance entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Update<WF_Instance>(entity);
            }
        }
    }
}


