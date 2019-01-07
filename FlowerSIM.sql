


USE [master]
GO

/****** Object:  Database [FlowersSIM]    ******/
CREATE DATABASE [FlowersSIM]
ON  PRIMARY 
( NAME = N'FlowersSIM', FILENAME = N'D:\Data\FlowersSIM.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'FlowersSIM_log', FILENAME = N'D:\Data\FlowersSIM_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

USE FlowersSIM
GO

CREATE TABLE [dbo].[Members](
    	[MembersId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[MembersName] [varchar](20) NOT NULL UNIQUE,
	[MembersPwd] [varchar](50) NOT NULL,
	[Gender] [int] DEFAULT ((0)) NOT NULL,
	[Phone] [varchar](20) NOT NULL,
	[LoginStatus] [int] DEFAULT ((0)) NOT NULL
)
GO

CREATE TABLE [dbo].[Chat](
    [ChatId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ChatContent] [varchar](255) NOT NULL ,
	[ChatCreatTime] [dateTime] NOT NULL,
	[MembersId] [int] NOT NULL FOREIGN KEY REFERENCES [dbo].[Members] ([MembersId] ),
	[SendMemId] [int] NOT NULL,
	[ChatStatus] [int] NOT NULL,
)
GO

CREATE TABLE [dbo].[Admins](
    [AdminsId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[AdminsName] [varchar](20) NOT NULL UNIQUE,
	[AdminsPwd] [varchar](50) NOT NULL,
	[Phone] [varchar](20) NOT NULL,
)
GO
CREATE TABLE [dbo].[Class](
	[ClassId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ClassName] [nvarchar](50) NOT NULL UNIQUE,
)
GO

CREATE TABLE [dbo].[Festival](
	[FestivalId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[FestivalName] [nvarchar](50) NOT NULL UNIQUE,
	[ClassId] [int] NOT NULL FOREIGN KEY REFERENCES [dbo].[Class] ([ClassId]),
)
GO
CREATE TABLE [dbo].Color(
	[ColorId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ColorName] [nvarchar](50) NOT NULL UNIQUE,
	[ClassId] [int] NOT NULL FOREIGN KEY REFERENCES [dbo].[Class] ([ClassId]),
)
GO
CREATE TABLE [dbo].FlowerKind(
	[FlowerKindId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[FlowerKindName] [nvarchar](50) NOT NULL UNIQUE,
	[ClassId] [int] NOT NULL FOREIGN KEY REFERENCES [dbo].[Class] ([ClassId]),
)
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ProductName] [varchar](100) NOT NULL UNIQUE,
	[ProductVolume] [int] NOT NULL,
	[Picture1] [varchar](200) NULL,
	[Picture2] [varchar](200) NULL,
	[Picture3] [varchar](200) NULL,
	[Picture4] [varchar](200) NULL,
	[Picture5] [varchar](200) NULL,
	[Material] [varchar](200) NOT NULL,
	[Package] [varchar](200) NOT NULL,
	[FlowerLanguage] [varchar](200) NOT NULL,
	[OriginalPrice] [decimal] NULL,
	[NowPrice] [decimal] NOT NULL,
	[Inventory] [int] NOT NULL,
	[FestivalId] [int] NOT NULL FOREIGN KEY REFERENCES [dbo].[Festival] ([FestivalId]),
	[ColorId] [int] NOT NULL FOREIGN KEY REFERENCES [dbo].[Color] ([ColorId]),
	[FlowerKindId] [int] NOT NULL FOREIGN KEY REFERENCES [dbo].[FlowerKind] ([FlowerKindId]),
)
GO

CREATE TABLE [dbo].[ShoppingCar](
	[CarId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ProductId] [int] NOT NULL FOREIGN KEY REFERENCES [dbo].[Product] ([ProductId]),
	[MembersId] [int] NOT NULL FOREIGN KEY REFERENCES [dbo].[Members] ([MembersId]),	
	[NowPrice] [decimal] NOT NULL,
	[Num] [int] DEFAULT ((0)) NOT NULL,
	[Price] [decimal] NOT NULL,
	[ProductName] [varchar](100),
	[Picture1] [varchar](200),
)
GO

CREATE TABLE [dbo].[Orders](
	[OrdersId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[MembersId] [int] NOT NULL FOREIGN KEY REFERENCES [dbo].[Members] ([MembersId]),
	[OrdersName] [varchar](20) NOT NULL,
	[OrdersPhone] [varchar](20) NOT NULL,
	[ConsigneeName] [varchar](20) NOT NULL,
	[ConsigneePhone] [varchar](20) NOT NULL,
	[ConsigneeAddress] [text] NOT NULL,
	[carOrproduct] [int] NOT NULL ,
	[ProductIdList] [varchar](20) NOT NULL ,
	[CreateTime] [date] DEFAULT (getdate()) NOT NULL,
	[DeliveryTime] [datetime] NOT NULL,
	[Price] [decimal] NOT NULL,
	[Status] [int]  DEFAULT ((0)) NOT NULL,
	[OrderNumbers] [varchar](255)  NOT NULL,
)
GO


CREATE TABLE [dbo].[OrderGoods](
	[OGId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ProductName] [varchar](100),
	[Picture1] [varchar](200),
	[NowPrice] [decimal],
	[Num] [int] DEFAULT ((0)),
	[Price] [decimal],
)
GO


USE [FlowersSIM]
GO
--�����û�Members������
Insert into Members(MembersName,MembersPwd,Gender,Phone) 
values('kefu','123456',0,'15602205222')
Insert into Members(MembersName,MembersPwd,Gender,Phone) 
values('abc321','123456',1,'15322002121')
Insert into Members(MembersName,MembersPwd,Gender,Phone) 
values('abc132','123456',0,'16620111391')
Insert into Members(MembersName,MembersPwd,Gender,Phone) 
values('abc213','123456',0,'15626060273')

Insert into Members(MembersName,MembersPwd,Gender,Phone) 
values('bca123','123456',1,'13434150076')
Insert into Members(MembersName,MembersPwd,Gender,Phone) 
values('bca321','123456',0,'13424024735')
Insert into Members(MembersName,MembersPwd,Gender,Phone) 
values('bca132','123456',1,'17818889998')
Insert into Members(MembersName,MembersPwd,Gender,Phone) 
values('bca213','123456',0,'16620111351')

Insert into Members(MembersName,MembersPwd,Gender,Phone) 
values('cab123','123456',0,'18320191588')
Insert into Members(MembersName,MembersPwd,Gender,Phone) 
values('cab321','123456',1,'15217337700')
Insert into Members(MembersName,MembersPwd,Gender,Phone) 
values('cab132','123456',1,'19875111113')
Insert into Members(MembersName,MembersPwd,Gender,Phone) 
values('cab213','123456',0,'13926060273')






GO
Insert into Admins values('admin1','123456','12345678901')
Insert into Admins values('admin2','123456','12345678901')

GO
Insert into Class values('����')
Insert into Class values('��ɫ')
Insert into Class values('����')
GO
Insert into Festival values('Ԫ��',1)
Insert into Festival values('����',1)
Insert into Festival values('���˽�',1)
Insert into Festival values('��Ů��',1)
Insert into Festival values('������',1)
Insert into Festival values('ĸ�׽�',1)
Insert into Festival values('��Ϧ���˽�',1)
Insert into Festival values('��ʦ��',1)
Insert into Festival values('ʥ����',1)
GO
Insert into Color values('��ɫ',2)
Insert into Color values('��ɫ',2)
Insert into Color values('��ɫ',2)
Insert into Color values('��ɫ',2)
Insert into Color values('��ɫ',2)
GO
Insert into FlowerKind values('õ��',3)
Insert into FlowerKind values('�ٺ�',3)
Insert into FlowerKind values('���տ�',3)
Insert into FlowerKind values('����ܰ',3)
Insert into FlowerKind values('�ջ�',3)
GO

Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('�������--33֦��õ��',53,'/Image/�������--33֦��õ��.jpg','33֦��õ�壬ʯ��÷Χ��','��ɫ����ֽ����ɫ�д�����','ֻ���������һ�з��գ�����������������������С�',383,298,100,3,1,1)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('һ������--19֦��õ�壬����������',53,'/Image/һ������--9֦��õ�壬����������.jpg','19֦��õ�壬����������','ţƤֽ�����ɫ����ֽ����ɫ�д����ᣬħ����ɽ�Ұ�װ��','�����������΢�磬���Ҵ��ݲ��ȣ�������֪�����Ҷ���ʼ��һ�����',315,249,100,3,1,1)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('����ĳ�ŵ--99֦��õ��',53,'/Image/����ĳ�ŵ--99֦��õ��.jpg','99֦��õ��','��ɫѩ��ֽ����ɫ����ֽ������ֽ���ƺ�ɫ�д�����','�����ʱ�򣬸�����һ����ɡ�������ʱ�򣬸���һ����ů�ı��䣻����ˣ���Զ��һյ��Ϊ������������ʱ������һ����ů�����⡣������������һ��99֦��õ�廨��',599,499,100,3,1,1)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('һ��һ��--õ��11֦����ɫ������0.3��',53,'/Image/һ��һ��--õ��11֦����ɫ������0.3��.jpg','��õ��11֦����ɫ(������ɫ��������0.3��������Ҷ��������','�ڲ��ɫ����ֽ�����ţƤֽ,����ɫ����','11֦õ�壬Ԣ��һ��һ�⣡����֮�ˣ������ǽڡ�һ�亮ů��һ��������һ�䶣�̣�һ���ഫ��һ����˼��һ�����Σ�һ�ݰ��⣬һ��������',139,129,100,3,1,1)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('����--�����ȷ�õ��33֦��ʯ��÷Χ��',53,'/Image/����--�����ȷ�õ��33֦��ʯ��÷Χ��.jpg','�����ȷ�õ��33֦��ʯ��÷Χ��','�ڲ�����ɫ����ֽ������㶹��ѹ��ֽ���Ϻ�ɫ�д�����','ŭ�ŵ������У�Ե�����������أ��򻨴��У�ֻΪѰ�����Ǿ����򷼵����գ�����������ֵ����ȥ���Ľ������㣬����������ľ��',382,298,100,3,3,1)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('�������--�����ȷ�õ��9֦',53,'/Image/�������--�����ȷ�õ��9֦.jpg','�찲��õ��9֦��ǳ��ɫ����������������Ҷ����','��ɫѩ��ֽ��ǳ������������ֽ����ɳɫ�д�����','�����ļ��ڣ����������������������˼���ζ����',215,128,100,3,3,1)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('99֦õ��Ӽ�ֵ68Ԫ��ܽ�����ɿ���98��',53,'/Image/99֦õ������ֵ68Ԫ��ܽ�����ɿ���98��--33֦�����ȡ�66֦��õ�塢1��������.jpg','33֦�����ȡ�66֦��õ�塢1��������+��ܽ�����ɿ���98��','��ɫopp����ֽ6�š��ƺ�ɫ�д�2��','����ʱ����ôת�䣬������ô�ı䣬��İ��������ļ䡣',869,599,100,3,1,1)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('�������--������õ��66֦',53,'/Image/�������--������õ��66֦.jpg','������õ��66֦ ����������Χ��','�ڲ��ɫ����ֽ �������ɫ��ƽ��ӡ����Ҷ�� ����˫ɫ�д�','���������ҽ��������Ҹ����������������۵�ʹ�࣬������һ�����ҵĽ�����û���㣬�Ҿ���һֻ��ʧ�˷���Ĵ���',601,459,5,3,1,1)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('ǣ��һ��--��õ��19֦',53,'/Image/ǣ��һ��--��õ��19֦.jpg','��õ��19֦��������Χ��','��ɫopp����ֽ ǳ������������˫��ֽ ���̴����ƴ�','"19֦��õ��Ԣ�⣺��Զ������鲻�塣����������,�������������ĵط�,�������ĵ�һ��ǣ�֡�����Զ�޷�������Щ������ϲ��,������פ�ļ䣡"',281,219,100,3,1,1)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('���İ���--99֦��33֦�����ȣ�66֦��õ��',53,'/Image/���İ���--99֦��33֦�����ȣ�66֦��õ��.jpg','33֦�����ȡ�66֦��õ�塢1��������','��ɫopp����ֽ6�š��ƺ�ɫ�д�2��','����ʱ����ôת�䣬������ô�ı䣬��İ��������ļ䡣',869,699,100,3,1,1)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('��������--��õ��22֦���ۼ��˷�õ��14֦����ɫ�۹�5֦',53,'/Image/��������--��õ��22֦���ۼ��˷�õ��14֦����ɫ�۹�5֦.jpg','��ɫõ�干36֦����õ��22֦���ۼ��˷�õ��14֦����ɫ�۹�5֦���ȼ���0.5��','��ɫ˫������ֽ���׿����ƴ�����','�ٶ�һ�����룬�Ҿ��ܿ����㡣���������Ŀ�������㵽���ܺ�����',485,459,100,3,4,1)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('�廳--�׾գ��ƾռ��װٺ�',53,'/Image/�廳--�׾գ��ƾռ��װٺ�.jpg','9��׾ջ���7��ƾջ���2֦��ͷ�װٺϣ�����ɢβҶ����ݺ','��ɫƽ��ֽֽ������ɫ����ֽ����ɴ����ɫ�д�����','��˼',269,219,100,5,4,5)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('�����ڻ���B--��õ�壬�װٺϣ��׾ջ�׿���ܰ��׺',53,'/Image/�����ڻ���B--��õ�壬�װٺϣ��׾ջ�׿���ܰ��׺.jpg','��õ��9֦+����ˮ�ٺ�2֦���׾ջ�׿���ܰ��׺������ɫҶ��','��ɫƽ��ֽ2�ŵ����װ����ɫɴ����','��˼',212,192,100,5,4,2)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('�����ڻ���A-- 12֦��ɫõ��',53,'/Image/�����ڻ���A-- 12֦��ɫõ��.jpg','12֦��ɫõ��','����ֽ��װ','����',186,156,100,5,4,1)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('׷��--�׾գ���õ�壬�װٺ�',53,'/Image/׷��--�׾գ���õ�壬�װٺ�.jpg','�׾�6֦����õ��6֦���װٺ�3֦����ͷ��','ǳ��ɫ����ֽԲ�ΰ�װ����ɫ˿������','׷��',266,226,100,5,4,5)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('ܰ������--������õ��11֦����ɫ����ܰ11֧����ɫʯ��÷4֦',53,'/Image/ܰ������--������õ��11֦����ɫ����ܰ11֧����ɫʯ��÷4֦.jpg','�����ȷ�õ��11֦����ɫ����ܰ11֧����ɫʯ��÷4֦������Ҷ4֦','����ɫ��ɫֽ1�ţ���ɫOPP����ֽ1�ף����̴��������Ĵ�1�ף�ħ����ɽ�Һд�','��������˵���ؼҽ�һ�����裬��һ�����Ҹ����¡���ֱ�����ڣ�����ᵽ�������ۣ�ԭ����һֱ�������������...��Ϊ�Њ����ҵ����裡',249,229,100,6,3,4)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('�Ҹ�����--��ɫ����ܰ19֦',53,'/Image/�Ҹ�����--��ɫ����ܰ19֦.jpg','��ɫ����ܰ19֦����������ʯ��÷����ɫ�����ҡ�����Ҷ','�ڲ��Ϻ�ɫ��֯��������ɫ��ţƤֽ��õ��ɫ�д�����','Я��һ���ʻ������������ԣ�����Ũ�����Ҷ�����ף���������ҵ�������Զ������Ư����',215,169,100,6,3,4)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('�м�--29֦�쿵��ܰ,2֦�۰ٺ�',53,'/Image/�м�--29֦�쿵��ܰ,2֦�۰ٺ�.jpg','2֦�۰ٺ�,2֦�۰ٺϣ�29֦�쿵��ܰ��������ݺ����','�ڲ�����ɫ��ֽ����ɫƽ��ֽ����ɫ�д�����','�����ж�֮�ĵ��������Ҹ��ģ������м�֮����������������ģ�',278,218,100,6,1,4)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('ůů���ʺ�--����11֦���װٺ�2֦��ǳ��ɫ����ܰ9֦',53,'/Image/ůů���ʺ�--����11֦���װٺ�2֦��ǳ��ɫ����ܰ9֦.jpg','����11֦���װٺ�2֦��ǳ��ɫ����ܰ9֦����ɫ��˼÷2֦','��ɫ����ֽ1�š��װ�ɫƽ��ֽ1�š�����ɫ�д�1�ס�ħ����ɽ�ҺУ���','����ܰ+�ٺϣ���ů�����࣬����ܰ+õ�壬��ͬ��������İ���ůů�����⣬�û������ͣ�����������ֿ��ף��������ߣ����Ҹ���Զ������!',332,259,100,9,4,4)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('��ס��ʱ��--������1֦����ѩɽõ��6֦',53,'/Image/��ס��ʱ��--������1֦����ѩɽõ��6֦.jpg','������1֦����ѩɽõ��6֦���۽۹�0.3��������Ҷ0.5��','���������Ứ��1��','��ÿ�����⣬���������ӣ������΢Ц���Ȼ���ʢ�ţ�',306,239,100,8,3,1)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('��һ����Ů��--���տ�3֦�������ȷ�õ��3֦',53,'/Image/��һ����Ů��--���տ�3֦�������ȷ�õ��3֦.jpg','���տ�3֦�������ȷ�õ��3֦����ɫ��۹�0.2����������0.1������������,��ɫ����ֽ2.5��','��ɫѩ��ֽ1��,��ɫ/��ɫ�д���1��','���է����֦�׷ɣ����Ц�ݣ��ڳ������ƣ����ӵ������ݣ���Ҳ˳�Ʊ��㵹��',242,188,100,7,2,3)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('Բ��--�۰ٺ�2֦����õ��9֦����ɫ����ܰ15֦',53,'/Image/Բ��--�۰ٺ�2֦����õ��9֦����ɫ����ܰ15֦.jpg','�۰ٺ�2֦����õ��9֦��Ҷ�ϻ�1������ɫ����ܰ15֦����ɫ��۹�0.5��','�б�����һ��','����Ԣ��ϣ�����ʻ�ʻ������Ҹ��ı˰�����?',368,286,100,6,1,4)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('�Ҹ�����--��ͷ�ٺ�2֦��������õ��8֦����̫����6֦',53,'/Image/�Ҹ�����--��ͷ�ٺ�2֦��������õ��8֦����̫����6֦.jpg','��ͷ�ٺ�2֦��5������/֦����������õ��8֦����̫����6֦����ɫʯ��÷0.3��������Ҷ0.5��','Բ�����Ứ��1��','΢΢�����������ʢ�ĵĿ���������ҹ�ע��ĽŲ���Ը������Ҹ����磡',282,219,100,5,4,2)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('��������--11֦�����ȷ�õ�壬�װٺ�2֦',53,'/Image/��������--11֦�����ȷ�õ�壬�װٺ�2֦.jpg','11֦�����ȷ�õ�壬�װٺ�2֦����ɫ��˼÷5֦������Ҷ0.5��','ǳ������������ֽ2�ţ��װ�ɫ�д�1.5��','��һ�����õĴ����������㶼��Ϊ������ֵ�ñ����������Դ���',265,206,100,4,3,1)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('����ܰ��--��ɫ����ܰ66֦����ɫ��ͷ����ܰ15֦',53,'/Image/����ܰ��--��ɫ����ܰ66֦����ɫ��ͷ����ܰ15֦.jpg','��ɫ����ܰ66֦����ɫ��ͷ����ܰ15֦������Ҷ2��','��ɫopp����ֽ3�š�ǳ������������ֽ2�š��׿�ϸ�������Ĵ�1.5��','�����¼�������������Ȼ�����š���Ψ�ʻ����������ã���ʦ�İ�����ů���棡',469,319,100,3,5,4)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('����ף��--�Ϻ�ɫ����ܰ9֦����ɫ��ͷ��ˮ�ٺ�2֦����ɫ�۹�4֦',53,'/Image/����ף��--�Ϻ�ɫ����ܰ9֦����ɫ��ͷ��ˮ�ٺ�2֦����ɫ�۹�4֦.jpg','�Ϻ�ɫ����ܰ9֦����ɫ��ͷ��ˮ�ٺ�2֦����ɫ�۹�4֦�����������ȼ���Ҷ','�װ�ɫƽ��ֽ�����ɫ˫��ţƤֽ������ɫ�д�����','���紺��ķ�һ����ů������Ļ�����ǳǳ��˵',252,195,100,2,5,4)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('���ףԸ--����',53,'/Image/���ףԸ--����.jpg','��ɫ��ˮ�ٺ�3֦����ɫ����ܰ11֦��������2֦��������5֦�����������ɫ��ͷ��������ɫ�۹���ɢβ������֯��4֦����Ҷ����','�б�����һ��','����������Դ����ֿ��̹�ϣ���Ȼ�Ҹ���ת˲���ţ�����ȴ�ܳ־ã�һ�ݳ�ֿ��ף����һ����ϵ��ʺ�Ը�����ÿһ�죡',368,288,100,1,1,2)
GO



--���빺�ﳵShoppingCar������
Insert into ShoppingCar(ProductId,MembersId,NowPrice,Num,Price) 
values(1,1,298,3,298*3)
Insert into ShoppingCar(ProductId,MembersId,NowPrice,Num,Price) 
values(2,1,249,3,249*3)
Insert into ShoppingCar(ProductId,MembersId,NowPrice,Num,Price) 
values(1,2,298,3,298*3)
Insert into ShoppingCar(ProductId,MembersId,NowPrice,Num,Price) 
values(2,2,499,1,499*1)
Insert into ShoppingCar(ProductId,MembersId,NowPrice,Num,Price) 
values(3,2,129,7,129*7)
Insert into ShoppingCar(ProductId,MembersId,NowPrice,Num,Price) 
values(3,3,129,7,129*7)
Insert into ShoppingCar(ProductId,MembersId,NowPrice,Num,Price) 
values(4,4,298,1,298*1)
GO

--���붩��Orders������
Insert into Orders(MembersId,OrdersName,OrdersPhone,ConsigneeName,ConsigneePhone,ConsigneeAddress,carOrproduct,ProductIdList,DeliveryTime,Price,OrderNumbers,Status) 
values(2,'���','12345678901','С��','12345678901','�㶫ʡ��������Դ·789��',0,'1','2018-10-20',298,'3',0)
Insert into Orders(MembersId,OrdersName,OrdersPhone,ConsigneeName,ConsigneePhone,ConsigneeAddress,carOrproduct,ProductIdList,DeliveryTime,Price,OrderNumbers,Status)  
values(2,'���','12345678901','С��','12345678901','�㶫ʡ��������Դ·789��',0,'2,3','2018-10-20',1995,'1,2',1)
Insert into Orders(MembersId,OrdersName,OrdersPhone,ConsigneeName,ConsigneePhone,ConsigneeAddress,carOrproduct,ProductIdList,DeliveryTime,Price,OrderNumbers,Status) 
values(2,'���','12345678901','С��','12345678901','�㶫ʡ��������Դ·789��',0,'1,4,5','2018-10-20',2304,'1,2,3',2)
Insert into Orders(MembersId,OrdersName,OrdersPhone,ConsigneeName,ConsigneePhone,ConsigneeAddress,carOrproduct,ProductIdList,DeliveryTime,Price,OrderNumbers,Status) 
values(2,'���','12345678901','С��','12345678901','�㶫ʡ��������Դ·789��',0,'1,2,3','2018-10-20',2293,'3,2,1',3)
Insert into Orders(MembersId,OrdersName,OrdersPhone,ConsigneeName,ConsigneePhone,ConsigneeAddress,carOrproduct,ProductIdList,DeliveryTime,Price,OrderNumbers,Status) 
values(2,'���','12345678901','С��','12345678901','�㶫ʡ��������Դ·789��',0,'1,2','2018-10-20',796,'1,2',4)

Insert into Orders(MembersId,OrdersName,OrdersPhone,ConsigneeName,ConsigneePhone,ConsigneeAddress,carOrproduct,ProductIdList,DeliveryTime,Price,OrderNumbers,Status) 
values(3,'��Ȩ','12345678901','����ʦ','12345678901','�㶫ʡ��������Դ·789��',1,'4,5,11','2018-10-20',7055,'1,1,1',0)
Insert into Orders(MembersId,OrdersName,OrdersPhone,ConsigneeName,ConsigneePhone,ConsigneeAddress,carOrproduct,ProductIdList,DeliveryTime,Price,OrderNumbers,Status) 
values(3,'��Ȩ','12345678901','����ʦ','12345678901','�㶫ʡ��������Դ·789��',1,'1,4,12','2018-10-20',3442,'1,2,1',1)
Insert into Orders(MembersId,OrdersName,OrdersPhone,ConsigneeName,ConsigneePhone,ConsigneeAddress,carOrproduct,ProductIdList,DeliveryTime,Price,OrderNumbers,Status) 
values(3,'��Ȩ','12345678901','����ʦ','12345678901','�㶫ʡ��������Դ·789��',1,'6,9,10','2018-10-20',9729,'1,3,6',2)
Insert into Orders(MembersId,OrdersName,OrdersPhone,ConsigneeName,ConsigneePhone,ConsigneeAddress,carOrproduct,ProductIdList,DeliveryTime,Price,OrderNumbers,Status)  
values(3,'��Ȩ','12345678901','����ʦ','12345678901','�㶫ʡ��������Դ·789��',1,'5,7','2018-10-20',5683,'2,5',3)
Insert into Orders(MembersId,OrdersName,OrdersPhone,ConsigneeName,ConsigneePhone,ConsigneeAddress,carOrproduct,ProductIdList,DeliveryTime,Price,OrderNumbers,Status) 
values(3,'��Ȩ','12345678901','����ʦ','12345678901','�㶫ʡ��������Դ·789��',1,'2,3,8','2018-10-20',5667,'2,2,2',4)
Insert into Orders(MembersId,OrdersName,OrdersPhone,ConsigneeName,ConsigneePhone,ConsigneeAddress,carOrproduct,ProductIdList,DeliveryTime,Price,OrderNumbers,Status) 
values(2,'��Ȩ','12345678901','����ʦ','12345678901','�㶫ʡ��������Դ·789��',0,'2,3,2','2018-10-20',2493,'2,2,7',1)
Insert into Orders(MembersId,OrdersName,OrdersPhone,ConsigneeName,ConsigneePhone,ConsigneeAddress,carOrproduct,ProductIdList,DeliveryTime,Price,OrderNumbers,Status) 
values(2,'��Ȩ','12345678901','����ʦ','12345678901','�㶫ʡ��������Դ·789��',0,'1,8,2','2018-10-20',4468,'1,2,3',1)
Insert into Orders(MembersId,OrdersName,OrdersPhone,ConsigneeName,ConsigneePhone,ConsigneeAddress,carOrproduct,ProductIdList,DeliveryTime,Price,OrderNumbers,Status) 
values(2,'��Ȩ','12345678901','����ʦ','12345678901','�㶫ʡ��������Դ·789��',0,'1,8,2','2018-10-20',4468,'1,2,3',1)
GO


