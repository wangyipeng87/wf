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
    public class WF_ToDoDao
    {
        public int save(WF_ToDo entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
               object id= conn.Insert<WF_ToDo>(entity);
                return (int)id;
            }
        }
        public bool update(WF_ToDo entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Update<WF_ToDo>(entity);
            }
        }
        public bool del(int id)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                WF_ToDo todo= conn.Get<WF_ToDo>(id);
                todo.IsDelete = 1;
                return conn.Update<WF_ToDo>(todo);
            }
        }
        public WF_ToDo getByID(int id)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Get<WF_ToDo>(id);
            }
        }

        public List<WF_ToDo> getList(int instanceid,string nodekey,int state)
        {
            string sql = @"   SELECT
                                	wtd.ID,
                                	wtd.Nodekey,
                                	wtd.InstanceID,
                                	wtd.ToDoName,
                                	wtd.[URL],
                                	wtd.ResponseUserCode,
                                	wtd.DealUserCode,
                                	wtd.DealTime,
                                	wtd.OperationType,
                                	wtd.TodoType,
                                	wtd.IsShow,
                                	wtd.PrevID,
                                	wtd.Batch,
                                	wtd.CreateUserCode,
                                	wtd.CreateTime,
                                	wtd.UpdateUserCode,
                                	wtd.UpdateTime,
                                	wtd.[State],
                                	wtd.IsDelete
                                FROM
                                	WF_ToDo AS wtd WHERE wtd.InstanceID=@instanceid AND wtd.Nodekey=@nodekey AND wtd.[State]=@state
                                	AND wtd.IsDelete=0";
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Query<WF_ToDo>(sql, new { instanceid = instanceid, nodekey= nodekey, state= state }).ToList();
            }
        }
        public List<WF_ToDo> getMyTodoList(string usercode, int begin, int end, out int count)
        {
            string sql = @"   ;WITH tmp AS (
                                                SELECT
                                                	wtd.ID,
                                                	wtd.Nodekey,
                                                	wtd.InstanceID,
                                                	wi.FormID,
                                                	wi.CreateTime AS ApplyTime,
                                                	wi.ApplyUserCode,
                                                	e.UserName AS ApplyUserName,
                                                	wi.WriterUserCode,
                                                	e2.UserName AS WriterUserName,
                                                	wtd.ToDoName,
                                                	wtd.[URL],
                                                	wtd.ResponseUserCode,
                                                	wtd.DealUserCode,
                                                	wtd.DealTime,
                                                	wtd.OperationType,
                                                	wtd.TodoType,
                                                	wtd.IsShow,
                                                	wtd.PrevID,
                                                	wtd.Batch,
                                                	wtd.CreateUserCode,
                                                	wtd.CreateTime,
                                                	wtd.UpdateUserCode,
                                                	wtd.UpdateTime,
                                                	wtd.[State],
                                                	wtd.IsDelete,
                                                	ROW_NUMBER() OVER ( ORDER BY wtd.CreateTime DESC ) AS [index]
                                                FROM
                                                	WF_ToDo AS wtd 
                                                	INNER JOIN WF_Instance AS wi ON (wi.ID=wtd.InstanceID AND wi.[State]=1 AND wi.IsDelete=0)
                                                	INNER JOIN Employee AS e ON e.UserCode=wi.ApplyUserCode
                                                	INNER JOIN Employee AS e2 ON e2.UserCode=wi.WriterUserCode
                                                	
                                                
                                                WHERE wtd.IsDelete=0 AND wtd.IsShow=1  AND wtd.[State]=1
                                                	AND (wtd.ResponseUserCode=@usercode
                                                	OR EXISTS(
                                                	SELECT 1 FROM WF_Agent AS wa WHERE wa.[State]=1 AND wa.IsDelete=0 AND wa.AgentUserCode=@usercode 
                                                	AND wa.BeginTime<=GETDATE() AND wa.EndTime>=GETDATE()
                                                	AND wtd.ResponseUserCode=wa.OriginalUserCode
                                                	))
                                                )
                                                  SELECT * FROM tmp AS t WHERE t.[index] BETWEEN @begin AND @end";
            string sqlcount = @"    
                                        SELECT COUNT(1)
                                        FROM
                                        	WF_ToDo AS wtd 
                                        	INNER JOIN WF_Instance AS wi ON (wi.ID=wtd.InstanceID AND wi.[State]=1 AND wi.IsDelete=0)
                                        	INNER JOIN Employee AS e ON e.UserCode=wi.ApplyUserCode
                                        	INNER JOIN Employee AS e2 ON e2.UserCode=wi.WriterUserCode
                                        	
                                        
                                        WHERE wtd.IsDelete=0 AND wtd.IsShow=1  AND wtd.[State]=1
                                        	AND (wtd.ResponseUserCode=@usercode
                                        	OR EXISTS(
                                        	SELECT 1 FROM WF_Agent AS wa WHERE wa.[State]=1 AND wa.IsDelete=0 AND wa.AgentUserCode=@usercode 
                                        	AND wa.BeginTime<=GETDATE() AND wa.EndTime>=GETDATE()
                                        	AND wtd.ResponseUserCode=wa.OriginalUserCode
                                        	)) ";
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                count = conn.Query<int>(sqlcount, new { usercode = usercode }).FirstOrDefault();
                return conn.Query<WF_ToDo>(sql, new { usercode = usercode, begin = begin, end = end }).ToList();
            }
        }
        public List<WF_ToDo> getMyDoneList(string usercode, int begin, int end, out int count)
        {
            string sql = @"   ;WITH tmp AS (
                                                 SELECT wtd.ID,
                                                       wtd.Nodekey,
                                                       wtd.InstanceID,
                                                       wi.FormID,
                                                       wi.CreateTime           AS ApplyTime,
                                                       wi.ApplyUserCode,
                                                       e.UserName              AS ApplyUserName,
                                                       wi.WriterUserCode,
                                                       e2.UserName             AS WriterUserName,
                                                       wtd.ToDoName,
                                                       wtd.[URL],
                                                       wtd.ResponseUserCode,
                                                       wtd.DealUserCode,
                                                       wtd.DealTime,
                                                       wtd.OperationType,
                                                       wtd.TodoType,
                                                       wtd.IsShow,
                                                       wtd.PrevID,
                                                       wtd.Batch,
                                                       wtd.CreateUserCode,
                                                       wtd.CreateTime,
                                                       wtd.UpdateUserCode,
                                                       wtd.UpdateTime,
                                                       wtd.[State],
                                                       wtd.IsDelete,
                                                       ROW_NUMBER() OVER(ORDER BY wtd.CreateTime DESC) AS [index]
                                                FROM   WF_ToDo                 AS wtd
                                                       INNER JOIN WF_Instance  AS wi
                                                            ON  (
                                                                    wi.ID = wtd.InstanceID
                                                                    AND wi.[State] = 1
                                                                    AND wi.IsDelete = 0
                                                                )
                                                       INNER JOIN Employee     AS e
                                                            ON  e.UserCode = wi.ApplyUserCode
                                                       INNER JOIN Employee     AS e2
                                                            ON  e2.UserCode = wi.WriterUserCode
                                                WHERE  wtd.IsDelete = 0
                                                       AND wtd.IsShow = 1
                                                       AND wtd.[State] = 2
                                                       AND (
                                                               wtd.DealUserCode = @usercode
                                                               OR  wtd.ResponseUserCode = @usercode
                                                           )
                                                   )
                                                  SELECT * FROM tmp AS t WHERE t.[index] BETWEEN @begin AND @end";
            string sqlcount = @"    
                                        SELECT COUNT(1)
                                        FROM
                                        	WF_ToDo AS wtd 
                                        	INNER JOIN WF_Instance AS wi ON (wi.ID=wtd.InstanceID AND wi.[State]=1 AND wi.IsDelete=0)
                                        	INNER JOIN Employee AS e ON e.UserCode=wi.ApplyUserCode
                                        	INNER JOIN Employee AS e2 ON e2.UserCode=wi.WriterUserCode
                                        	
                                        
                                        WHERE  wtd.IsDelete = 0
                                                       AND wtd.IsShow = 1
                                                       AND wtd.[State] = 2
                                                       AND (
                                                               wtd.DealUserCode = @usercode
                                                               OR  wtd.ResponseUserCode = @usercode
                                                           ) ";
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                count = conn.Query<int>(sqlcount, new { usercode = usercode }).FirstOrDefault();
                return conn.Query<WF_ToDo>(sql, new { usercode = usercode, begin = begin, end = end }).ToList();
            }
        }


        public List<WF_Instance> getMyApplyList(string usercode,int state,string keyword, int begin, int end, out int count)
        {
            string sql = @"   ;WITH tmp AS (
                                                  SELECT
                                                     	wi.ID,
                                                     	wi.TmpKey,
                                                     	wi.FormID,
                                                     	wt.TmpName,
                                                     	wi.ApplyUserCode,
                                                     	e.UserName AS ApplyUserName,
                                                     	wi.WriterUserCode,
                                                     	e2.UserName AS WriterUserName,
                                                     	wi.CreateUserCode,
                                                     	wi.CreateTime,
                                                     	wi.UpdateUserCode,
                                                     	wi.UpdateTime,
                                                     	wi.[State],
                                                     	wi.IsDelete,
                                                     	wdd.EnumName AS StateName,
                                                      ROW_NUMBER() OVER ( ORDER BY wi.CreateTime DESC ) AS [index]
                                                     FROM
                                                     	WF_Instance AS wi
                                                     	INNER JOIN Employee AS e ON e.UserCode=wi.ApplyUserCode
                                                     	INNER JOIN Employee AS e2 ON e2.UserCode=wi.WriterUserCode
                                                     	INNER JOIN WF_Template AS wt ON wt.[key]=wi.TmpKey
                                                     	INNER JOIN WF_DataDictionary AS wdd ON (wdd.EnumID=wi.[State] AND wdd.[Type]=1)
                                                     WHERE wi.IsDelete=0 
                                                     AND (wi.FormID LIKE '%'+@keyword+'%' OR wt.TmpName LIKE '%'+@keyword+'%' )
                                                     AND (wdd.[State]=@state OR @state=-1)
                                                     AND (wi.ApplyUserCode=@usercode OR wi.WriterUserCode=@usercode)
                                                   )
                                                  SELECT * FROM tmp AS t WHERE t.[index] BETWEEN @begin AND @end";
            string sqlcount = @"    
                                        SELECT COUNT(1)
                                        FROM
                                                     	WF_Instance AS wi
                                                     	INNER JOIN Employee AS e ON e.UserCode=wi.ApplyUserCode
                                                     	INNER JOIN Employee AS e2 ON e2.UserCode=wi.WriterUserCode
                                                     	INNER JOIN WF_Template AS wt ON wt.[key]=wi.TmpKey
                                                     	INNER JOIN WF_DataDictionary AS wdd ON (wdd.EnumID=wi.[State] AND wdd.[Type]=1)
                                                     WHERE wi.IsDelete=0 
                                                     AND (wi.FormID LIKE '%'+@keyword+'%' OR wt.TmpName LIKE '%'+@keyword+'%' )
                                                     AND (wdd.[State]=@state OR @state=-1)
                                                     AND (wi.ApplyUserCode=@usercode OR wi.WriterUserCode=@usercode) ";
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                count = conn.Query<int>(sqlcount, new { usercode = usercode, state= state, keyword=keyword }).FirstOrDefault();
                return conn.Query<WF_Instance>(sql, new { usercode = usercode, state = state, keyword = keyword, begin = begin, end = end }).ToList();
            }
        }

        public List<WF_Instance> getCurrentInstanceList(string user, int state, string keyword, int begin, int end, out int count)
        {
            string sql = @"   ;WITH tmp AS ( 
                                                SELECT wi.ID,
                                                       wi.TmpKey,
                                                       wi.FormID,
                                                       wt.TmpName,
                                                       wi.ApplyUserCode,
                                                       e.UserName                    AS ApplyUserName,
                                                       wi.WriterUserCode,
                                                       e2.UserName                   AS WriterUserName,
                                                       wi.CreateUserCode,
                                                       wi.CreateTime,
                                                       wi.UpdateUserCode,
                                                       wi.UpdateTime,
                                                       wi.[State],
                                                       wi.IsDelete,
                                                       wdd.EnumName                  AS StateName,
                                                       itd.NodeList,
                                                       itd.UserList,
                                                       ROW_NUMBER() OVER(ORDER BY wi.CreateTime DESC) AS [index]
                                                FROM   WF_Instance                   AS wi
                                                       INNER JOIN Employee           AS e
                                                            ON  e.UserCode = wi.ApplyUserCode
                                                       INNER JOIN Employee           AS e2
                                                            ON  e2.UserCode = wi.WriterUserCode
                                                       INNER JOIN WF_Template        AS wt
                                                            ON  wt.[key] = wi.TmpKey
                                                       INNER JOIN WF_DataDictionary  AS wdd
                                                            ON  (wdd.EnumID = wi.[State] AND wdd.[Type] = 1)
                                                       LEFT JOIN V_InstanceToDo AS itd                  
                                                            ON  itd.InstanceID = wi.ID
                                                WHERE  wi.IsDelete = 0
                                                        AND wt.IsDelete = 0
                                                       AND (
                                                               wi.FormID LIKE '%' + @keyword + '%'
                                                               OR wt.TmpName LIKE '%' + @keyword + '%'
                                                           )
                                                       AND (wdd.[State] = @state OR @state = -1)
                                                       AND (
                                                               wi.ApplyUserCode LIKE '%' + @user + '%'
                                                               OR wi.WriterUserCode LIKE '%' + @user + '%'
                                                               OR e.UserName LIKE '%' + @user + '%'
                                                               OR e2.UserName LIKE '%' + @user + '%'
                                                           )
                                                   )
                                                  SELECT * FROM tmp AS t WHERE t.[index] BETWEEN @begin AND @end";
            string sqlcount = @"    
                                        SELECT COUNT(1)
                                         FROM   WF_Instance                   AS wi
                                                       INNER JOIN Employee           AS e
                                                            ON  e.UserCode = wi.ApplyUserCode
                                                       INNER JOIN Employee           AS e2
                                                            ON  e2.UserCode = wi.WriterUserCode
                                                       INNER JOIN WF_Template        AS wt
                                                            ON  wt.[key] = wi.TmpKey
                                                       INNER JOIN WF_DataDictionary  AS wdd
                                                            ON  (wdd.EnumID = wi.[State] AND wdd.[Type] = 1)
                                                WHERE  wi.IsDelete = 0
                                                       AND (
                                                               wi.FormID LIKE '%' + @keyword + '%'
                                                               OR wt.TmpName LIKE '%' + @keyword + '%'
                                                           )
                                                       AND (wdd.[State] = @state OR @state = -1)
                                                       AND (
                                                               wi.ApplyUserCode LIKE '%' + @user + '%'
                                                               OR wi.WriterUserCode LIKE '%' + @user + '%'
                                                               OR e.UserName LIKE '%' + @user + '%'
                                                               OR e2.UserName LIKE '%' + @user + '%'
                                                           )";
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                count = conn.Query<int>(sqlcount, new { user = user, state = state, keyword = keyword }).FirstOrDefault();
                return conn.Query<WF_Instance>(sql, new { user = user, state = state, keyword = keyword, begin = begin, end = end }).ToList();
            }
        }
        public WF_ToDo getPreAddTodo(int todoid)
        {
            string sql = @"  ;WITH tmp 
                                        AS (
                                        	
                                        SELECT 
                                        	wtd.ID,
                                        	wtd.Nodekey,
                                        	wtd.InstanceID,
                                        	wtd.ToDoName,
                                        	wtd.[URL],
                                        	wtd.ResponseUserCode,
                                        	wtd.DealUserCode,
                                        	wtd.DealTime,
                                        	wtd.OperationType,
                                        	wtd.TodoType,
                                        	wtd.IsShow,
                                        	wtd.PrevID,
                                        	wtd.Batch,
                                        	wtd.CreateUserCode,
                                        	wtd.CreateTime,
                                        	wtd.UpdateUserCode,
                                        	wtd.UpdateTime,
                                        	wtd.[State],
                                        	wtd.IsDelete, 
                                        	 1 AS [index]
                                        	 FROM WF_ToDo AS wtd WHERE wtd.ID=@id and wtd.IsDelete=0
                                        	UNION ALL 
                                        	
                                        	SELECT 
                                        	wtd.ID,
                                        	wtd.Nodekey,
                                        	wtd.InstanceID,
                                        	wtd.ToDoName,
                                        	wtd.[URL],
                                        	wtd.ResponseUserCode,
                                        	wtd.DealUserCode,
                                        	wtd.DealTime,
                                        	wtd.OperationType,
                                        	wtd.TodoType,
                                        	wtd.IsShow,
                                        	wtd.PrevID,
                                        	wtd.Batch,
                                        	wtd.CreateUserCode,
                                        	wtd.CreateTime,
                                        	wtd.UpdateUserCode,
                                        	wtd.UpdateTime,
                                        	wtd.[State],
                                        	wtd.IsDelete,
                                        		t.[index]+1  AS [index]
                                        	 FROM WF_ToDo AS wtd 
                                        	INNER JOIN tmp AS t ON t.PrevID=wtd.ID
                                            where   wtd.IsDelete=0
                                        )
                                        
                                        SELECT TOP 1 * FROM tmp AS t
                                        WHERE t.TodoType=3 ORDER BY t.[index] ASC ";
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Query<WF_ToDo>(sql, new { id = todoid }).FirstOrDefault();
            }
        }
        public List<WF_ToDo> getTodoList(string user, int begin, int end, out int count)
        {
            string sql = @"   ;WITH tmp AS (
                                                SELECT
                                                	wtd.ID,
                                                	wtd.Nodekey,
                                                	wtd.InstanceID,
                                                	wi.FormID,
                                                	wi.CreateTime AS ApplyTime,
                                                	wi.ApplyUserCode,
                                                	e.UserName AS ApplyUserName,
                                                	wi.WriterUserCode,
                                                	e2.UserName AS WriterUserName,
                                                	wtd.ToDoName,
                                                	wtd.[URL],
                                                	wtd.ResponseUserCode,
                                                	e3.UserName AS ResponseUserName,
                                                	wtd.DealUserCode,
                                                	wtd.DealTime,
                                                	wtd.OperationType,
                                                	wtd.TodoType,
                                                	wtd.IsShow,
                                                	wtd.PrevID,
                                                	wtd.Batch,
                                                	wtd.CreateUserCode,
                                                	wtd.CreateTime,
                                                	wtd.UpdateUserCode,
                                                	wtd.UpdateTime,
                                                	wtd.[State],
                                                	wtd.IsDelete,
                                                	ROW_NUMBER() OVER ( ORDER BY wtd.CreateTime DESC ) AS [index]
                                                FROM
                                                	WF_ToDo AS wtd 
                                                	INNER JOIN WF_Instance AS wi ON (wi.ID=wtd.InstanceID AND wi.[State]=1 AND wi.IsDelete=0)
                                                	INNER JOIN Employee AS e ON e.UserCode=wi.ApplyUserCode
                                                	INNER JOIN Employee AS e2 ON e2.UserCode=wi.WriterUserCode
                                                	INNER JOIN Employee AS e3 ON e3.UserCode=wtd.ResponseUserCode
                                                	
                                                
                                                WHERE wtd.IsDelete=0 AND wtd.IsShow=1  AND wtd.[State]=1
                                                	AND (wtd.ResponseUserCode like '%'+@user+'%' or e3.UserName like '%'+@user+'%' 
                                                	OR EXISTS(
                                                	SELECT 1 FROM WF_Agent AS wa WHERE wa.[State]=1 AND wa.IsDelete=0 AND (wa.AgentUserCode  like '%'+@user+'%'
                                                    or  wa.AgentName  like '%'+@user+'%')
                                                	AND wa.BeginTime<=GETDATE() AND wa.EndTime>=GETDATE()
                                                	AND wtd.ResponseUserCode=wa.OriginalUserCode
                                                	))
                                                )
                                                  SELECT * FROM tmp AS t WHERE t.[index] BETWEEN @begin AND @end";
            string sqlcount = @"    
                                        SELECT COUNT(1)
                                         FROM
                                                	WF_ToDo AS wtd 
                                                	INNER JOIN WF_Instance AS wi ON (wi.ID=wtd.InstanceID AND wi.[State]=1 AND wi.IsDelete=0)
                                                	INNER JOIN Employee AS e ON e.UserCode=wi.ApplyUserCode
                                                	INNER JOIN Employee AS e2 ON e2.UserCode=wi.WriterUserCode
                                                	INNER JOIN Employee AS e3 ON e3.UserCode=wtd.ResponseUserCode
                                                	
                                                
                                                WHERE wtd.IsDelete=0 AND wtd.IsShow=1  AND wtd.[State]=1
                                                	AND (wtd.ResponseUserCode like '%'+@user+'%' or e3.UserName like '%'+@user+'%' 
                                                	OR EXISTS(
                                                	SELECT 1 FROM WF_Agent AS wa WHERE wa.[State]=1 AND wa.IsDelete=0 AND (wa.AgentUserCode  like '%'+@user+'%'
                                                    or  wa.AgentName  like '%'+@user+'%')
                                                	AND wa.BeginTime<=GETDATE() AND wa.EndTime>=GETDATE()
                                                	AND wtd.ResponseUserCode=wa.OriginalUserCode
                                                	))";
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                count = conn.Query<int>(sqlcount, new { user = user }).FirstOrDefault();
                return conn.Query<WF_ToDo>(sql, new { user = user, begin = begin, end = end }).ToList();
            }
        }
    }
}


