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
    public class WF_InstanceVariableDao
    {
        public int save(WF_InstanceVariable entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                int id = conn.Insert<WF_InstanceVariable>(entity);
                return id;
            }
        }
        public bool Update(WF_InstanceVariable entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Update<WF_InstanceVariable>(entity);
            }
        }
        public WF_InstanceVariable getbyInstanceAndVarname(int instanceid,string varname)
        {
            string sql = @"   SELECT
                                  	wiv.ID,
                                  	wiv.InstanceID,
                                  	wiv.VarName,
                                  	wiv.DefaultValue,
                                  	wiv.VarType,
                                  	wiv.CreateUserCode,
                                  	wiv.CreateTime,
                                  	wiv.UpdateUserCode,
                                  	wiv.UpdateTime,
                                  	wiv.[State],
                                  	wiv.IsDelete
                                  FROM
                                  	WF_InstanceVariable AS wiv WHERE wiv.InstanceID=@instanceid AND wiv.VarName=@varname
                                  	AND wiv.[State]=1 AND wiv.IsDelete=0";

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Query<WF_InstanceVariable>(sql, new { instanceid= instanceid,varname = varname }).FirstOrDefault();
            }
        }
        public int copyVarByTmpKey(int instanceid, string tmpkey,string usercode)
        {
            string sql = @"    
  
                                      UPDATE WF_InstanceVariable
                                      SET 
                                      	IsDelete = 1
                                      WHERE InstanceID=@instanceID
                                      INSERT INTO WF_InstanceVariable
                                      (
                                      	InstanceID,
                                      	VarName,
                                      	DefaultValue,
                                      	VarType,
                                      	CreateUserCode,
                                      	CreateTime,
                                      	UpdateUserCode,
                                      	UpdateTime,
                                      	[State],
                                      	IsDelete
                                      )
                                      
                                      SELECT
                                      	@instanceID,
                                      	VarName,
                                      	DefaultValue,
                                      	VarType,
                                      	@usercode,
                                      	GETDATE(),
                                      	@usercode,
                                      	GETDATE(),
                                      	1,
                                      	0
                                      FROM
                                      	WF_TemplateVariable AS wtv
                                      WHERE wtv.TmpKey=@tmpkey AND wtv.[State]=1 AND wtv.IsDelete=0";

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Execute(sql, new { instanceID = instanceid, tmpkey = tmpkey, usercode = usercode, });
            }
        }
        public List<WF_InstanceVariable>  getbyInstanceID(int instanceid)
        {
            string sql = @"   SELECT
                                  	wiv.ID,
                                  	wiv.InstanceID,
                                  	wiv.VarName,
                                  	wiv.DefaultValue,
                                  	wiv.VarType,
                                  	wiv.CreateUserCode,
                                  	wiv.CreateTime,
                                  	wiv.UpdateUserCode,
                                  	wiv.UpdateTime,
                                  	wiv.[State],
                                  	wiv.IsDelete
                                  FROM
                                  	WF_InstanceVariable AS wiv WHERE wiv.InstanceID=@instanceid 
                                  	AND wiv.[State]=1 AND wiv.IsDelete=0";

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Query<WF_InstanceVariable>(sql, new { instanceid = instanceid }).ToList();
            }
        }
    }
}


