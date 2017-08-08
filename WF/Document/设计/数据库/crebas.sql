USE WF
GO

/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2012                    */
/* Created on:     2017/8/8 20:53:00                            */
/*==============================================================*/


if exists (select 1
            from  sysobjects
           where  id = object_id('Department')
            and   type = 'U')
   drop table Department
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Employee')
            and   type = 'U')
   drop table Employee
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_CMS_Menu')
            and   type = 'U')
   drop table T_CMS_Menu
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WF_Agent')
            and   type = 'U')
   drop table WF_Agent
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WF_DataDictionary')
            and   type = 'U')
   drop table WF_DataDictionary
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WF_Instance')
            and   type = 'U')
   drop table WF_Instance
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WF_InstanceVariable')
            and   type = 'U')
   drop table WF_InstanceVariable
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WF_OperationHistory')
            and   type = 'U')
   drop table WF_OperationHistory
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WF_Role')
            and   type = 'U')
   drop table WF_Role
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WF_Role_User')
            and   type = 'U')
   drop table WF_Role_User
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WF_Rule')
            and   type = 'U')
   drop table WF_Rule
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WF_Sign')
            and   type = 'U')
   drop table WF_Sign
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WF_Template')
            and   type = 'U')
   drop table WF_Template
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WF_TemplateNode')
            and   type = 'U')
   drop table WF_TemplateNode
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WF_TemplateNodeAuthority')
            and   type = 'U')
   drop table WF_TemplateNodeAuthority
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WF_TemplateVariable')
            and   type = 'U')
   drop table WF_TemplateVariable
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WF_ToDo')
            and   type = 'U')
   drop table WF_ToDo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WF_Transfer')
            and   type = 'U')
   drop table WF_Transfer
go

/*==============================================================*/
/* Table: Department                                            */
/*==============================================================*/
create table Department (
   ID                   int                  identity,
   DeptCode             nvarchar(200)        null,
   DeptName             nvarchar(200)        null,
   ParentCode           nvarchar(200)        null,
   ParentName           nvarchar(200)        null,
   AllParentCode        nvarchar(max)          null,
   CreateUserCode       nvarchar(20)         null,
   CreateTime           datetime             null,
   UpdateUserCode       nvarchar(20)         null,
   UpdateTime           datetime             null,
   State                int                  null,
   IsDelete             int                  null,
   constraint PK_DEPARTMENT primary key (ID)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Department')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'State')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Department', 'column', 'State'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示启用，0表示禁用',
   'user', @CurrentUser, 'table', 'Department', 'column', 'State'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Department')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsDelete')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Department', 'column', 'IsDelete'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示已经删除，0表示未删除',
   'user', @CurrentUser, 'table', 'Department', 'column', 'IsDelete'
go

/*==============================================================*/
/* Table: Employee                                              */
/*==============================================================*/
create table Employee (
   ID                   int                  identity,
   UserCode             nvarchar(20)         null,
   UserName             nvarchar(400)        null,
   Sex                  int                  null,
   Email                nvarchar(400)        null,
   PostCode             nvarchar(200)        null,
   PostName             nvarchar(200)        null,
   DepCode              nvarchar(200)        null,
   DeptName             nvarchar(200)        null,
   Phone                nvarchar(200)        null,
   LineManageCode       nvarchar(200)        null,
   CreateUserCode       nvarchar(20)         null,
   CreateTime           datetime             null,
   UpdateUserCode       nvarchar(20)         null,
   UpdateTime           datetime             null,
   State                int                  null,
   IsDelete             int                  null,
   constraint PK_EMPLOYEE primary key (ID)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Employee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Sex')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Employee', 'column', 'Sex'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示男，0表示女',
   'user', @CurrentUser, 'table', 'Employee', 'column', 'Sex'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Employee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'State')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Employee', 'column', 'State'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示启用，0表示禁用',
   'user', @CurrentUser, 'table', 'Employee', 'column', 'State'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Employee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsDelete')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Employee', 'column', 'IsDelete'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示已经删除，0表示未删除',
   'user', @CurrentUser, 'table', 'Employee', 'column', 'IsDelete'
go

/*==============================================================*/
/* Table: T_CMS_Menu                                            */
/*==============================================================*/
create table T_CMS_Menu (
   ID                   varchar(100)         not null,
   Name                 nvarchar(200)        null,
   Code                 nvarchar(100)        null,
   URL                  nvarchar(2000)       null,
   ParenrID             varchar(100)         null,
   SiteCode             nvarchar(200)        null,
   State                int                  null,
   CreateUserCode       nvarchar(200)        null,
   CreateTime           datetime             null,
   UpdateUserCode       nvarchar(100)        null,
   UpdateTime           datetime             null,
   "Order"              int                  null,
   constraint PK_T_CMS_MENU primary key nonclustered (ID)
)
go

/*==============================================================*/
/* Table: WF_Agent                                              */
/*==============================================================*/
create table WF_Agent (
   ID                   int                  identity,
   AgentUserCode        nvarchar(20)         not null,
   AgentName            nvarchar(400)        null,
   OriginalUserCode     nvarchar(20)         null,
   OriginalUserName     nvarchar(400)        null,
   BeginTime            datetime             null,
   EndTime              datetime             null,
   State                int                  null,
   CreateUserCode       nvarchar(20)         null,
   CreateTime           datetime             null,
   UpdateUserCode       nvarchar(20)         null,
   UpdateTime           datetime             null,
   IsDelete             int                  null,
   constraint PK_WF_AGENT primary key (ID)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_Agent')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsDelete')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_Agent', 'column', 'IsDelete'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示已经删除，0表示未删除',
   'user', @CurrentUser, 'table', 'WF_Agent', 'column', 'IsDelete'
go

/*==============================================================*/
/* Table: WF_DataDictionary                                     */
/*==============================================================*/
create table WF_DataDictionary (
   ID                   int                  identity,
   Type                 int                  null,
   TypeName             nvarchar(400)        null,
   EnumID               int                  null,
   EnumName             nvarchar(400)        null,
   CreateUserCode       nvarchar(20)         null,
   CreateTime           datetime             null,
   UpdateUserCode       nvarchar(20)         null,
   UpdateTime           datetime             null,
   State                int                  null,
   IsDelete             int                  null,
   constraint PK_WF_DATADICTIONARY primary key (ID)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_DataDictionary')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'State')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_DataDictionary', 'column', 'State'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示启用，0表示禁用',
   'user', @CurrentUser, 'table', 'WF_DataDictionary', 'column', 'State'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_DataDictionary')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsDelete')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_DataDictionary', 'column', 'IsDelete'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示已经删除，0表示未删除',
   'user', @CurrentUser, 'table', 'WF_DataDictionary', 'column', 'IsDelete'
go

/*==============================================================*/
/* Table: WF_Instance                                           */
/*==============================================================*/
create table WF_Instance (
   ID                   int                  identity,
   TmpKey               nvarchar(50)         null,
   FormID               nvarchar(4000)       null,
   ApplyUserCode        nvarchar(20)         null,
   WriterUserCode       nvarchar(20)         null,
   CreateUserCode       nvarchar(20)         null,
   CreateTime           datetime             null,
   UpdateUserCode       nvarchar(20)         null,
   UpdateTime           datetime             null,
   State                int                  null,
   IsDelete             int                  null,
   constraint PK_WF_INSTANCE primary key (ID)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_Instance')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'State')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_Instance', 'column', 'State'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示启用，0表示禁用，2表示挂起',
   'user', @CurrentUser, 'table', 'WF_Instance', 'column', 'State'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_Instance')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsDelete')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_Instance', 'column', 'IsDelete'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示已经删除，0表示未删除',
   'user', @CurrentUser, 'table', 'WF_Instance', 'column', 'IsDelete'
go

/*==============================================================*/
/* Table: WF_InstanceVariable                                   */
/*==============================================================*/
create table WF_InstanceVariable (
   ID                   int                  identity,
   InstanceID           int                  null,
   VarName              nvarchar(200)        null,
   DefaultValue         nvarchar(max)          null,
   VarType              int                  null,
   CreateUserCode       nvarchar(20)         null,
   CreateTime           datetime             null,
   UpdateUserCode       nvarchar(20)         null,
   UpdateTime           datetime             null,
   State                int                  null,
   IsDelete             int                  null,
   constraint PK_WF_INSTANCEVARIABLE primary key (ID)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_InstanceVariable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'VarType')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_InstanceVariable', 'column', 'VarType'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示int,2表示double，3表示时间,4表示字符串',
   'user', @CurrentUser, 'table', 'WF_InstanceVariable', 'column', 'VarType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_InstanceVariable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'State')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_InstanceVariable', 'column', 'State'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示启用，0表示禁用',
   'user', @CurrentUser, 'table', 'WF_InstanceVariable', 'column', 'State'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_InstanceVariable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsDelete')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_InstanceVariable', 'column', 'IsDelete'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示已经删除，0表示未删除',
   'user', @CurrentUser, 'table', 'WF_InstanceVariable', 'column', 'IsDelete'
go

/*==============================================================*/
/* Table: WF_OperationHistory                                   */
/*==============================================================*/
create table WF_OperationHistory (
   ID                   int                  identity,
   InstanceID           int                  null,
   TodoID               int                  null,
   OperationUserCode    nvarchar(20)         null,
   OperationUserName    nvarchar(400)        null,
   AgentUserCode        nvarchar(20)         null,
   AgentUserName        nvarchar(400)        null,
   OperationTime        datetime             null,
   OperationType        int                  null,
   Comments             nvarchar(max)        null,
   State                int                  null,
   IsDelete             int                  null,
   constraint PK_WF_OPERATIONHISTORY primary key (ID)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_OperationHistory')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'State')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_OperationHistory', 'column', 'State'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示未处理，2表示已处理',
   'user', @CurrentUser, 'table', 'WF_OperationHistory', 'column', 'State'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_OperationHistory')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsDelete')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_OperationHistory', 'column', 'IsDelete'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示已经删除，0表示未删除',
   'user', @CurrentUser, 'table', 'WF_OperationHistory', 'column', 'IsDelete'
go

/*==============================================================*/
/* Table: WF_Role                                               */
/*==============================================================*/
create table WF_Role (
   ID                   int                  identity,
   RoleCode             numeric              not null,
   RoleName             nvarchar(200)        null,
   CreateUserCode       nvarchar(20)         null,
   CreateTime           datetime             null,
   UpdateUserCode       nvarchar(20)         null,
   UpdateTime           datetime             null,
   State                int                  null,
   IsDelete             int                  null,
   constraint PK_WF_ROLE primary key (ID)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_Role')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'State')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_Role', 'column', 'State'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示启用，0表示禁用',
   'user', @CurrentUser, 'table', 'WF_Role', 'column', 'State'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_Role')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsDelete')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_Role', 'column', 'IsDelete'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示已经删除，0表示未删除',
   'user', @CurrentUser, 'table', 'WF_Role', 'column', 'IsDelete'
go

/*==============================================================*/
/* Table: WF_Role_User                                          */
/*==============================================================*/
create table WF_Role_User (
   ID                   int                  identity,
   RoleCode             nvarchar(200)        null,
   UserCode             nvarchar(200)        null,
   CreateUserCode       nvarchar(20)         null,
   CreateTime           datetime             null,
   UpdateUserCode       nvarchar(20)         null,
   UpdateTime           datetime             null,
   State                int                  null,
   IsDelete             int                  null,
   constraint PK_WF_ROLE_USER primary key (ID)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_Role_User')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'State')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_Role_User', 'column', 'State'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示启用，0表示禁用',
   'user', @CurrentUser, 'table', 'WF_Role_User', 'column', 'State'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_Role_User')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsDelete')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_Role_User', 'column', 'IsDelete'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示已经删除，0表示未删除',
   'user', @CurrentUser, 'table', 'WF_Role_User', 'column', 'IsDelete'
go

/*==============================================================*/
/* Table: WF_Rule                                               */
/*==============================================================*/
create table WF_Rule (
   ID                   int                  identity,
   Tmpkey               nvarchar(50)         null,
   Rulekey              nvarchar(50)         null,
   BeginNodeKey         nvarchar(50)         null,
   EndNodekey           nvarchar(50)         null,
   Expression           nvarchar(max)          null,
   Description          nvarchar(4000)       null,
   CreateUserCode       nvarchar(20)         null,
   CreateTime           datetime             null,
   UpdateUserCode       nvarchar(20)         null,
   UpdateTime           datetime             null,
   State                int                  null,
   IsDelete             int                  null,
   constraint PK_WF_RULE primary key (ID)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_Rule')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'State')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_Rule', 'column', 'State'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示启用，0表示禁用',
   'user', @CurrentUser, 'table', 'WF_Rule', 'column', 'State'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_Rule')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsDelete')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_Rule', 'column', 'IsDelete'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示已经删除，0表示未删除',
   'user', @CurrentUser, 'table', 'WF_Rule', 'column', 'IsDelete'
go

/*==============================================================*/
/* Table: WF_Sign                                               */
/*==============================================================*/
create table WF_Sign (
   ID                   int                  identity,
   beforeToDoID         int                  null,
   AfterToDoID          int                  null,
   OperationUserCode    nvarchar(20)         null,
   OperationUserName    nvarchar(400)        null,
   AgentUserCode        nvarchar(20)         null,
   AgentUserName        nvarchar(400)        null,
   OperationTime        datetime             null,
   State                int                  null,
   IsDelete             int                  null,
   constraint PK_WF_SIGN primary key (ID)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_Sign')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'State')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_Sign', 'column', 'State'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示未处理，2表示已处理',
   'user', @CurrentUser, 'table', 'WF_Sign', 'column', 'State'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_Sign')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsDelete')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_Sign', 'column', 'IsDelete'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示已经删除，0表示未删除',
   'user', @CurrentUser, 'table', 'WF_Sign', 'column', 'IsDelete'
go

/*==============================================================*/
/* Table: WF_Template                                           */
/*==============================================================*/
create table WF_Template (
   ID                   int                  identity,
   "key"                nvarchar(50)         null,
   TmpName              nvarchar(200)        null,
   Description          nvarchar(4000)       null,
   CreateUserCode       nvarchar(20)         null,
   CreateTime           datetime             null,
   UpdateUserCode       nvarchar(20)         null,
   UpdateTime           datetime             null,
   State                int                  null,
   IsDelete             int                  null,
   constraint PK_WF_TEMPLATE primary key (ID)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_Template')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'State')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_Template', 'column', 'State'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示启用，0表示禁用',
   'user', @CurrentUser, 'table', 'WF_Template', 'column', 'State'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_Template')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsDelete')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_Template', 'column', 'IsDelete'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示已经删除，0表示未删除',
   'user', @CurrentUser, 'table', 'WF_Template', 'column', 'IsDelete'
go

/*==============================================================*/
/* Table: WF_TemplateNode                                       */
/*==============================================================*/
create table WF_TemplateNode (
   ID                   int                  identity,
   "key"                nvarchar(50)         null,
   Nodekey              nvarchar(50)         null,
   NodeName             nvarchar(200)        null,
   Description          nvarchar(4000)       null,
   ProcessType          int                  null,
   ProcessTypeValue     nvarchar(4000)       null,
   ExecType             int                  null,
   TimeLimit            int                  null,
   NodeType             int                  null,
   URL                  nvarchar(4000)       null,
   IsGoBack             int                  null,
   GoBackType           int                  null,
   CreateUserCode       nvarchar(20)         null,
   CreateTime           datetime             null,
   UpdateUserCode       nvarchar(20)         null,
   UpdateTime           datetime             null,
   State                int                  null,
   IsDelete             int                  null,
   x                    float                null,
   y                    float                null
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_TemplateNode')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ExecType')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_TemplateNode', 'column', 'ExecType'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示抢占模式，2表示并行模式',
   'user', @CurrentUser, 'table', 'WF_TemplateNode', 'column', 'ExecType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_TemplateNode')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TimeLimit')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_TemplateNode', 'column', 'TimeLimit'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '单位分钟',
   'user', @CurrentUser, 'table', 'WF_TemplateNode', 'column', 'TimeLimit'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_TemplateNode')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'NodeType')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_TemplateNode', 'column', 'NodeType'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示开始节点，2表示结束节点，3表示正常节点',
   'user', @CurrentUser, 'table', 'WF_TemplateNode', 'column', 'NodeType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_TemplateNode')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsGoBack')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_TemplateNode', 'column', 'IsGoBack'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示允许退回，0表示不能退回',
   'user', @CurrentUser, 'table', 'WF_TemplateNode', 'column', 'IsGoBack'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_TemplateNode')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'GoBackType')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_TemplateNode', 'column', 'GoBackType'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示退回给申请人 ，2表示退回前一步，3表示退回到某一步，4表示退回到自定义指定步骤',
   'user', @CurrentUser, 'table', 'WF_TemplateNode', 'column', 'GoBackType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_TemplateNode')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'State')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_TemplateNode', 'column', 'State'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示启用，0表示禁用',
   'user', @CurrentUser, 'table', 'WF_TemplateNode', 'column', 'State'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_TemplateNode')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsDelete')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_TemplateNode', 'column', 'IsDelete'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示已经删除，0表示未删除',
   'user', @CurrentUser, 'table', 'WF_TemplateNode', 'column', 'IsDelete'
go

/*==============================================================*/
/* Table: WF_TemplateNodeAuthority                              */
/*==============================================================*/
create table WF_TemplateNodeAuthority (
   ID                   int                  identity,
   TmpKey               nvarchar(50)         null,
   NodeKey              nvarchar(50)         null,
   AuthorityID          int                  null,
   CreateUserCode       nvarchar(20)         null,
   CreateTime           datetime             null,
   UpdateUserCode       nvarchar(20)         null,
   UpdateTime           datetime             null,
   State                int                  null,
   IsDelete             int                  null,
   constraint PK_WF_TEMPLATENODEAUTHORITY primary key (ID)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_TemplateNodeAuthority')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'State')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_TemplateNodeAuthority', 'column', 'State'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示启用，0表示禁用',
   'user', @CurrentUser, 'table', 'WF_TemplateNodeAuthority', 'column', 'State'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_TemplateNodeAuthority')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsDelete')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_TemplateNodeAuthority', 'column', 'IsDelete'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示已经删除，0表示未删除',
   'user', @CurrentUser, 'table', 'WF_TemplateNodeAuthority', 'column', 'IsDelete'
go

/*==============================================================*/
/* Table: WF_TemplateVariable                                   */
/*==============================================================*/
create table WF_TemplateVariable (
   ID                   int                  identity,
   TmpKey               nvarchar(50)         null,
   VarName              nvarchar(200)        null,
   DefaultValue         nvarchar(max)          null,
   VarType              int                  null,
   CreateUserCode       nvarchar(20)         null,
   CreateTime           datetime             null,
   UpdateUserCode       nvarchar(20)         null,
   UpdateTime           datetime             null,
   State                int                  null,
   IsDelete             int                  null,
   constraint PK_WF_TEMPLATEVARIABLE primary key (ID)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_TemplateVariable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'VarType')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_TemplateVariable', 'column', 'VarType'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示int,2表示double，3表示时间,4表示字符串',
   'user', @CurrentUser, 'table', 'WF_TemplateVariable', 'column', 'VarType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_TemplateVariable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'State')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_TemplateVariable', 'column', 'State'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示启用，0表示禁用',
   'user', @CurrentUser, 'table', 'WF_TemplateVariable', 'column', 'State'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_TemplateVariable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsDelete')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_TemplateVariable', 'column', 'IsDelete'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示已经删除，0表示未删除',
   'user', @CurrentUser, 'table', 'WF_TemplateVariable', 'column', 'IsDelete'
go

/*==============================================================*/
/* Table: WF_ToDo                                               */
/*==============================================================*/
create table WF_ToDo (
   ID                   int                  identity,
   Nodekey              nvarchar(50)         null,
   InstanceID           int                  null,
   ToDoName             nvarchar(400)        null,
   URL                  nvarchar(4000)       null,
   ResponseUserCode     nvarchar(20)         null,
   DealUserCode         nvarchar(20)         null,
   DealTime             datetime             null,
   OperationType        int                  null,
   TodoType             int                  null,
   IsShow               int                  null,
   PrevID               int                  null,
   Batch                int                  null,
   CreateUserCode       nvarchar(20)         null,
   CreateTime           datetime             null,
   UpdateUserCode       nvarchar(20)         null,
   UpdateTime           datetime             null,
   State                int                  null,
   IsDelete             int                  null,
   constraint PK_WF_TODO primary key (ID)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_ToDo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TodoType')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_ToDo', 'column', 'TodoType'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1正常待办列表显示的待办，2传阅待办，',
   'user', @CurrentUser, 'table', 'WF_ToDo', 'column', 'TodoType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_ToDo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'State')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_ToDo', 'column', 'State'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示未处理，2表示已处理',
   'user', @CurrentUser, 'table', 'WF_ToDo', 'column', 'State'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_ToDo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsDelete')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_ToDo', 'column', 'IsDelete'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示已经删除，0表示未删除',
   'user', @CurrentUser, 'table', 'WF_ToDo', 'column', 'IsDelete'
go

/*==============================================================*/
/* Table: WF_Transfer                                           */
/*==============================================================*/
create table WF_Transfer (
   ID                   int                  identity,
   beforeToDoID         int                  null,
   AfterToDoID          int                  null,
   OperationUserCode    nvarchar(20)         null,
   OperationUserName    nvarchar(400)        null,
   AgentUserCode        nvarchar(20)         null,
   AgentUserName        nvarchar(400)        null,
   OperationTime        datetime             null,
   State                int                  null,
   IsDelete             int                  null,
   constraint PK_WF_TRANSFER primary key (ID)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_Transfer')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'State')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_Transfer', 'column', 'State'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示未处理，2表示已处理',
   'user', @CurrentUser, 'table', 'WF_Transfer', 'column', 'State'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WF_Transfer')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsDelete')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WF_Transfer', 'column', 'IsDelete'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1表示已经删除，0表示未删除',
   'user', @CurrentUser, 'table', 'WF_Transfer', 'column', 'IsDelete'
go

