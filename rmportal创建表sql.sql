USE RMPortal
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

CREATE TABLE [dbo].TxMpoHd(
	Id int identity(1,1) not null,
	MpoNo nvarchar(20) not null,
	MpoType nvarchar(30),
	Revision nvarchar(3),
	MpoDate datetime,
	Heading nvarchar(12),
	SuppCode nvarchar(20),
	Terms nvarchar(60),
	DeliAdd nvarchar(255),
	ShipDate datetime,
	ShipMode nvarchar(20) default ''
	Lighting nvarchar(20),
	Ccy nvarchar(3),
	Attn nvarchar(25),
	Remark text,
	Status char(1),
	Payment nvarchar(60),
	SubconFlag char(1),
	SubconType nvarchar(20),
	JobNoStr nvarchar(255),
	InCharge nvarchar(20),
	RevisetedDate datetime,
	ShippedTo nvarchar(50),
	AllowPurchase decimal(7,4)
	constraint PK_MpoHd primary key clustered(
	MpoNo asc
	)with (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

insert into TxMpoHd(MpoNo)values('ffff')
insert into TxMpoHd(MpoNo)VALUES('SSSS')
insert into TxMpoDet values(1,'SSSS',1,'','','','','',1,1,1,1,'',1,'',1,1)
insert into TxMpoDet(MpoNo,MpoDetId)values('SSSS',1)
insert into TxMpoDet(MpoNo,MpoDetId)values('ffff',2)
drop table TxMpoDet

--TxMpoDet
CREATE TABLE dbo.TxMpoDet(
MpoDetId int ,
MpoNo nvarchar(20),
Seq int,
MatCode nvarchar(40),
TempMat nvarchar(150),
ColorCode nvarchar(20),
Color nvarchar(150),
Size nvarchar(20),
Qty decimal(14,3),
MrQty decimal(14,3),
StockQty decimal(14,3),
FirstMrQty decimal(14,3),
BuyUnit nvarchar(8),
Upx decimal(12,4),
PxUnit nvarchar(10),
Width decimal(7,4),
Weight decimal(7,4)
constraint PK_MpoDet primary key clustered(
	MpoDetId,MpoNo asc
	)with (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE dbo.TxMpoDet DROP COLUMN TxMpoHdId

ALTER TABLE dbo.TxMpoDet ADD TxMpoHdId int default 0 

ALTER TABLE dbo.TxMpoDet ADD CONSTRAINT FK_MpoNo
FOREIGN KEY (MpoNo) REFERENCES TxMpoHd(MpoNo)

--TxMpoMatDet
CREATE TABLE TxMpoMatDet(
MpoMatDetId int identity(1,1),
MpoNo nvarchar(20),
MatCode nvarchar(40),
Remark text,
BuyUnit nvarchar(8),
BuyUnitFactor decimal(10,4),
MatDesc nvarchar(255),
PriceUnit nvarchar(8),
PriceUnitFactor decimal(10,4),
TempMat nvarchar(150),
Width decimal(7,3),
Weight decimal(7,3),
Origin nvarchar(20),
ArticleNo nvarchar(20),
Vendor nvarchar(20),
constraint PK_MpoMatDet primary key clustered(
	MpoMatDetId,MpoNo asc
	)with (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE TxMpoMatDet ADD CONSTRAINT FK_MpoNo
FOREIGN KEY (MpoNo) REFERENCES TxMpoDet(MpoNo)

--TxMpoSurcharge
CREATE TABLE TxMpoSurcharge(
MpoSurId INT IDENTITY(1,1),
MpoNo NVARCHAR(20),
SurType NVARCHAR(10),
SurDescription NVARCHAR(40),
SurPercent DECIMAL(5,2),
SurAmount DECIMAL(15,2)
 CONSTRAINT [PK_MpoSurcharge] PRIMARY KEY CLUSTERED 
(
	MpoSurId,MpoNo ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE TxMpoSurcharge ADD CONSTRAINT FK_MpoSurcharge
FOREIGN KEY (MpoNo) REFERENCES TxMpoHd(MpoNo)


GO

CREATE TABLE [dbo].[TxMpoDet](
	[MpoNo] nvarchar(20) NOT NULL,
	[MpoDetId] int NOT NULL,
	[Seq] int NULL,
	[MatCode] [nvarchar](40) NULL,
	[TempMat] [nvarchar](150) NULL,
	[ColorCode] [nvarchar](20) NULL,
	[Color] [nvarchar](150) NULL,
	[Size] [nvarchar](20) NULL,
	[Qty] [decimal](14, 3) NULL,
	[MrQty] [decimal](14, 3) NULL,
	[StockQty] [decimal](14, 3) NULL,
	[FirstMrQty] [decimal](14, 3) NULL,
	[UPx] [decimal](12, 4) NULL,
	[PxUnit] [nvarchar(10)],
	[Width] decimal(7,4),
	[Weight] decimal(7,4)
	
 CONSTRAINT [PkTxMpoDet] PRIMARY KEY NONCLUSTERED 
(
	[MpoNo] ASC,
	[MpoDetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY],
 CONSTRAINT [IxTxMpoDet_MatKey] UNIQUE NONCLUSTERED 
(
	[MpoNo] ASC,
	[MatCode] ASC,
	[TempMat] ASC,
	[Color] ASC,	
	[ColorCode] ASC,
	[Size] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO




ALTER TABLE [dbo].[TxMpoDet]  WITH CHECK ADD  CONSTRAINT [FkTxMpoDet_Hd] FOREIGN KEY([MpoNo])
REFERENCES [dbo].[TxMpoHd] ([MpoNo])
GO

ALTER TABLE [dbo].[TxMpoDet] CHECK CONSTRAINT [FkTxMpoDet_Hd]
GO
