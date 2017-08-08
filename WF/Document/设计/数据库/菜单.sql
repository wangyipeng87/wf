USE WF
GO
if exists (select 1
            from  sysobjects
           where  id = object_id('T_CMS_Menu')
            and   type = 'U')
   drop table T_CMS_Menu
go

/*==============================================================*/
/* Table: T_CMS_Menu                                            */
/*==============================================================*/
create table T_CMS_Menu (
   ID                   NVARCHAR(100)         not null,
   Name                  NVARCHAR(200) null,
   Code                  NVARCHAR(100) null,
   URL                   NVARCHAR(2000) null,
   ParenrID             NVARCHAR(100)         null,
   SiteCode              NVARCHAR(200) null,
   State                int                  null,
   CreateUserCode        NVARCHAR(200) null,
   CreateTime           datetime             null,
   UpdateUserCode        NVARCHAR(100) null,
   UpdateTime           datetime             null,
   "Order"              int                  null,
   constraint PK_T_CMS_MENU primary key nonclustered (ID)
)
go


/****** Object:  Table [dbo].[T_CMS_Menu]    Script Date: 08/08/2017 15:43:56 ******/
INSERT [dbo].[T_CMS_Menu] ([ID], [Name], [Code], [URL], [ParenrID], [SiteCode], [State], [CreateUserCode], [CreateTime], [UpdateUserCode], [UpdateTime], [Order]) VALUES (N'0ae88cf9-f380-4c8b-8500-0385dbe06eb2', N'添加站点', N'AddSite', N'/site/add', N'374e6012-cd92-448c-a9e4-70599250ba6d', NULL, 1, N'8454', CAST(0x0000A77700F7CA00 AS DateTime), N'8454', CAST(0x0000A77700F7CA00 AS DateTime), 2)
INSERT [dbo].[T_CMS_Menu] ([ID], [Name], [Code], [URL], [ParenrID], [SiteCode], [State], [CreateUserCode], [CreateTime], [UpdateUserCode], [UpdateTime], [Order]) VALUES (N'd5e23f2b-5a93-4ac8-8bce-1db5a9509545', N'栏目权限', N'channelAuthority', N'/channel/channelAuthority', N'cc1827cf-5623-42ba-a47c-e7bde324162b', NULL, 1, N'8454', CAST(0x0000A77B0156291F AS DateTime), N'8454', CAST(0x0000A77B0156291F AS DateTime), 3)
INSERT [dbo].[T_CMS_Menu] ([ID], [Name], [Code], [URL], [ParenrID], [SiteCode], [State], [CreateUserCode], [CreateTime], [UpdateUserCode], [UpdateTime], [Order]) VALUES (N'71f2a94e-f77a-4187-9e6a-21cec6e0ea58', N'知识库列表', N'knowledgeList', N'/knowledge/knowledgeList', N'8a0f14be-ce1c-43c9-b523-f3f04b877fa4', NULL, 1, N'8454', CAST(0x0000A77B0156291F AS DateTime), N'8454', CAST(0x0000A77B0156291F AS DateTime), 2)
INSERT [dbo].[T_CMS_Menu] ([ID], [Name], [Code], [URL], [ParenrID], [SiteCode], [State], [CreateUserCode], [CreateTime], [UpdateUserCode], [UpdateTime], [Order]) VALUES (N'675e1f06-a562-4949-856a-25eef515d725', N'OA首页链接列表', N'LinkList', N'/link/list?modelcode=M805', N'fcabc610-0f80-4914-9fec-7b2e0b0e67fb', NULL, 1, N'8454', NULL, N'8454', NULL, 5)
INSERT [dbo].[T_CMS_Menu] ([ID], [Name], [Code], [URL], [ParenrID], [SiteCode], [State], [CreateUserCode], [CreateTime], [UpdateUserCode], [UpdateTime], [Order]) VALUES (N'd7bb7429-f34d-4215-8cd6-3069940cf8ca', N'栏目添加', N'channelAdd', N'/channel/add', N'cc1827cf-5623-42ba-a47c-e7bde324162b', N'', 1, N'8454', CAST(0x0000A77D00B61471 AS DateTime), N'8454', CAST(0x0000A77D00B61471 AS DateTime), 2)
INSERT [dbo].[T_CMS_Menu] ([ID], [Name], [Code], [URL], [ParenrID], [SiteCode], [State], [CreateUserCode], [CreateTime], [UpdateUserCode], [UpdateTime], [Order]) VALUES (N'4d358e48-94c4-4e40-b69c-3ea932e8e576', N'模板管理', N'TmpManage', N'#', NULL, NULL, 1, N'8454', CAST(0x0000A77600A47E82 AS DateTime), N'8454', CAST(0x0000A77600A47E82 AS DateTime), 3)
INSERT [dbo].[T_CMS_Menu] ([ID], [Name], [Code], [URL], [ParenrID], [SiteCode], [State], [CreateUserCode], [CreateTime], [UpdateUserCode], [UpdateTime], [Order]) VALUES (N'af0bce22-8ad1-4e5a-a765-4a320c936903', N'栏目列表', N'channelManage', N'/channel/channelList', N'cc1827cf-5623-42ba-a47c-e7bde324162b', NULL, 1, N'8454', CAST(0x0000A77C0126F391 AS DateTime), N'8454', CAST(0x0000A77C0126F391 AS DateTime), 1)
INSERT [dbo].[T_CMS_Menu] ([ID], [Name], [Code], [URL], [ParenrID], [SiteCode], [State], [CreateUserCode], [CreateTime], [UpdateUserCode], [UpdateTime], [Order]) VALUES (N'374e6012-cd92-448c-a9e4-70599250ba6d', N'站点管理', N'SiteManage', N'#', NULL, NULL, 1, N'8454', CAST(0x0000A77600A444DC AS DateTime), N'8454', CAST(0x0000A77600A444DC AS DateTime), 2)
INSERT [dbo].[T_CMS_Menu] ([ID], [Name], [Code], [URL], [ParenrID], [SiteCode], [State], [CreateUserCode], [CreateTime], [UpdateUserCode], [UpdateTime], [Order]) VALUES (N'bf95b879-78e4-46b1-9c4e-74ca65652108', N'模板列表', N'TmpList', N'/tmp/list', N'4d358e48-94c4-4e40-b69c-3ea932e8e576', NULL, 1, N'8454', CAST(0x0000A77600A4A6B9 AS DateTime), N'8454', CAST(0x0000A77600A4A6B9 AS DateTime), 1)
INSERT [dbo].[T_CMS_Menu] ([ID], [Name], [Code], [URL], [ParenrID], [SiteCode], [State], [CreateUserCode], [CreateTime], [UpdateUserCode], [UpdateTime], [Order]) VALUES (N'e9090654-0e89-4c4d-83e6-79799f489f50', N'站点列表', N'SiteManage', N'/site/list', N'374e6012-cd92-448c-a9e4-70599250ba6d', NULL, 1, N'8454', CAST(0x0000A77600A46587 AS DateTime), N'8454', CAST(0x0000A77600A46587 AS DateTime), 1)
INSERT [dbo].[T_CMS_Menu] ([ID], [Name], [Code], [URL], [ParenrID], [SiteCode], [State], [CreateUserCode], [CreateTime], [UpdateUserCode], [UpdateTime], [Order]) VALUES (N'fcabc610-0f80-4914-9fec-7b2e0b0e67fb', N'OA首页链接管理', N'LinkManage', N'#', NULL, NULL, 1, N'8454', NULL, N'8454', NULL, 5)
INSERT [dbo].[T_CMS_Menu] ([ID], [Name], [Code], [URL], [ParenrID], [SiteCode], [State], [CreateUserCode], [CreateTime], [UpdateUserCode], [UpdateTime], [Order]) VALUES (N'abd6ad9d-3540-4e25-ae30-8e6f85bdec4c', N'首页', N'index', N'/', NULL, NULL, 1, N'8454', CAST(0x0000A77600A3EA85 AS DateTime), N'8454', CAST(0x0000A77600A3EA85 AS DateTime), 1)
INSERT [dbo].[T_CMS_Menu] ([ID], [Name], [Code], [URL], [ParenrID], [SiteCode], [State], [CreateUserCode], [CreateTime], [UpdateUserCode], [UpdateTime], [Order]) VALUES (N'8fae614e-bc95-4362-ad80-91a5fc3de877', N'内容列表', N'ContentManage', N'/content/contentList', N'8a0f14be-ce1c-43c9-b523-f3f04b877fa4', NULL, 1, N'8454', CAST(0x0000A77B01565EB9 AS DateTime), N'8454', CAST(0x0000A77B01565EB9 AS DateTime), 3)
INSERT [dbo].[T_CMS_Menu] ([ID], [Name], [Code], [URL], [ParenrID], [SiteCode], [State], [CreateUserCode], [CreateTime], [UpdateUserCode], [UpdateTime], [Order]) VALUES (N'cc1827cf-5623-42ba-a47c-e7bde324162b', N'栏目管理', N'channelManage', N'#', NULL, NULL, 1, N'8454', CAST(0x0000A77C0126D908 AS DateTime), N'8454', CAST(0x0000A77C0126D908 AS DateTime), 4)
INSERT [dbo].[T_CMS_Menu] ([ID], [Name], [Code], [URL], [ParenrID], [SiteCode], [State], [CreateUserCode], [CreateTime], [UpdateUserCode], [UpdateTime], [Order]) VALUES (N'8a0f14be-ce1c-43c9-b523-f3f04b877fa4', N'内容管理', N'ContentManage', N'#', NULL, NULL, 1, N'8454', CAST(0x0000A77B0156291F AS DateTime), N'8454', CAST(0x0000A77B0156291F AS DateTime), 3)
INSERT [dbo].[T_CMS_Menu] ([ID], [Name], [Code], [URL], [ParenrID], [SiteCode], [State], [CreateUserCode], [CreateTime], [UpdateUserCode], [UpdateTime], [Order]) VALUES (N'562e13e6-94b4-414a-9e17-f552ab102753', N'主题管理', N'ThemeManage', N'#', NULL, NULL, 1, N'8454', CAST(0x0000A77B0156291F AS DateTime), N'8454', CAST(0x0000A77B0156291F AS DateTime), 6)
INSERT [dbo].[T_CMS_Menu] ([ID], [Name], [Code], [URL], [ParenrID], [SiteCode], [State], [CreateUserCode], [CreateTime], [UpdateUserCode], [UpdateTime], [Order]) VALUES (N'0e88462a-bed9-4d3a-aaec-ffd314b7d604', N'主题列表', N'ThemeList', N'/theme/list', N'562e13e6-94b4-414a-9e17-f552ab102753', NULL, 1, N'8454', CAST(0x0000A77B0156291F AS DateTime), N'8454', CAST(0x0000A77B0156291F AS DateTime), 1)
