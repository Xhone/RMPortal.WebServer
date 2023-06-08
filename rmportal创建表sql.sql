USE Test
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,	
	[UserName] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](200) NULL,
	[DeptId] [int] NOT NULL,
	[Title] [varchar](20) NULL,
	[Level] [varchar](50) NULL,
	[NTID] [nvarchar](50) NOT NULL,
	[Tel] [nvarchar](20) NULL,
	[Email] [nvarchar](100) NULL,
	[CreateBy] [varchar](100) NULL,
	[CreateDate] [datetime] NULL

 CONSTRAINT [PK_Sys_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

insert into Users values('admin','123456','1','','','admin','12345678911','cc@qq.com','admin',GETDATE())
insert into Users values('sin','123456','1','','','xiansin','12345678911','cf@qq.com','admin',GETDATE())

CREATE TABLE [dbo].[Menus](
	[Id] [int] IDENTITY(1,1) NOT NULL,	
	[Name] [nvarchar](100) NOT NULL,
	
	[ParentId] [int] NOT NULL,
	[Url] [varchar](255) NULL,
	[Perms] [varchar](255) NULL,
	
	[CreateBy] [varchar](100) NULL,
	[CreateDate] [datetime] NULL

 CONSTRAINT [PK_Sys_Menu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,	
	[RoleName] [nvarchar](100) NOT NULL,
	
	[Active] [bit],
	
	[CreateBy] [varchar](100) NULL,
	[CreateDate] [datetime] NULL

 CONSTRAINT [PK_Sys_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[Depts](
	[Id] [int] IDENTITY(1,1) NOT NULL,	
	[Name] [nvarchar](100) NOT NULL,
	[CreateBy] [varchar](100) NULL,
	[CreateDate] [datetime] NULL

 CONSTRAINT [PK_Sys_Dept] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]