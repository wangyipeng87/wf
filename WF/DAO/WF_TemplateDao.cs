using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WF.Common;
using WF.Entity;
using Dapper;
using DapperExtensions;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Transactions;

namespace WF.DAO
{
    public class WF_TemplateDao
    {
        WF_TemplateNodeDao nodedao = new WF_TemplateNodeDao();
        WF_RuleDao ruledao = new WF_RuleDao();
        WF_Node_PeopleDao peopledao = new WF_Node_PeopleDao();
        public bool save(WF_Template entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                conn.Insert<WF_Template>(entity);
                return true;
            }
        }
        public bool save(WFTmp entity)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    nodedao.DelByTmpKey(entity.tmpkey);
                    ruledao.DelByTmpKey(entity.tmpkey);
                    if (entity.nodelist != null && entity.nodelist.Count > 0)
                    {
                        foreach (WF_TemplateNode item in entity.nodelist)
                        {
                            nodedao.save(item);
                            peopledao.del(item.Tmpkey, item.Nodekey);
                            if (item.userlist != null && item.userlist.Count > 0)
                            {
                                foreach (WF_Node_People people in item.userlist)
                                {
                                    peopledao.save(people);
                                }
                            }
                        }
                    }
                    if (entity.rulelist != null && entity.rulelist.Count > 0)
                    {
                        foreach (WF_Rule item in entity.rulelist)
                        {
                            ruledao.save(item);
                        }
                    }
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return true;
        }
        public bool update(WF_Template entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Update<WF_Template>(entity);
            }
        }
        public bool del(int id)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                WF_Template Role = conn.Get<WF_Template>(id);
                Role.IsDelete = 1;
                return conn.Update<WF_Template>(Role);
            }
        }
        public WF_Template getByID(int id)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Get<WF_Template>(id);
            }
        }
        public WF_Template getByKey(string key)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                string sql = @"SELECT
                                	wt.ID,
                                	wt.[key],
                                	wt.TmpName,
                                	wt.[Description],
                                	wt.CreateUserCode,
                                	wt.CreateTime,
                                	wt.UpdateUserCode,
                                	wt.UpdateTime,
                                	wt.[State],
                                	wt.IsDelete,
                                    wt.ClassName
                                FROM
                                	WF_Template AS wt
                                WHERE wt.[key]=@key
                                    and  wt.[IsDelete]=0";
                return conn.Query<WF_Template>(sql, new { key = key }).FirstOrDefault();
            }
        }
        public List<WF_Template> getAll(string key, int state, int begin, int end, string order, out int count)
        {
            string sql = @"   ;WITH tmp 
                                     AS (
                                     SELECT
                                           	wt.ID,
                                           	wt.[key],
                                           	wt.TmpName,
                                           	wt.[Description],
                                           	wt.CreateUserCode,
                                           	wt.CreateTime,
                                           	wt.UpdateUserCode,
                                           	wt.UpdateTime,
                                           	wt.[State],
                                            wt.ClassName,
                                           	wt.IsDelete,
                                           	e.UserName+'('+e.UserCode+')' AS createuser,
                                           	e2.UserName+'('+e2.UserCode+')' AS updateuser,
                                           	ROW_NUMBER() OVER ( ORDER BY " + order + @"  ) AS [index]
                                           FROM
                                           	WF_Template AS wt
                                           	INNER JOIN Employee AS e ON e.UserCode=wt.CreateUserCode
                                           	INNER JOIN Employee AS e2 ON e2.UserCode=wt.UpdateUserCode
                                           WHERE wt.IsDelete=0
                                           AND (wt.[key] LIKE '%'+@key+'%' OR wt.TmpName  LIKE '%'+@key+'%')
                                           AND (@state=-1 OR wt.[State]=@state)
                                     )
                                     
                                     SELECT * FROM tmp AS t where t.[index] BETWEEN @begin AND @end";
            string sqlcount = @"    SELECT  COUNT(1)
                                             FROM
                                             	WF_Template AS wt
                                             WHERE  wt.IsDelete=0
                                           AND (wt.[key] LIKE '%'+@key+'%' OR wt.TmpName  LIKE '%'+@key+'%')
                                           AND (@state=-1 OR wt.[State]=@state)";
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                count = conn.Query<int>(sqlcount, new { state = state, key = key }).FirstOrDefault();
                return conn.Query<WF_Template>(sql, new { state = state, key = key, begin = begin, end = end }).ToList();
            }
        }
    }
}


