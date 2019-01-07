


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
--插入用户Members表数据
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
Insert into Class values('节日')
Insert into Class values('颜色')
Insert into Class values('花材')
GO
Insert into Festival values('元旦',1)
Insert into Festival values('春节',1)
Insert into Festival values('情人节',1)
Insert into Festival values('妇女节',1)
Insert into Festival values('清明节',1)
Insert into Festival values('母亲节',1)
Insert into Festival values('七夕情人节',1)
Insert into Festival values('教师节',1)
Insert into Festival values('圣诞节',1)
GO
Insert into Color values('红色',2)
Insert into Color values('黄色',2)
Insert into Color values('粉色',2)
Insert into Color values('白色',2)
Insert into Color values('紫色',2)
GO
Insert into FlowerKind values('玫瑰',3)
Insert into FlowerKind values('百合',3)
Insert into FlowerKind values('向日葵',3)
Insert into FlowerKind values('康乃馨',3)
Insert into FlowerKind values('菊花',3)
GO

Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('忘情巴黎--33枝红玫瑰',53,'/Image/忘情巴黎--33枝红玫瑰.jpg','33枝红玫瑰，石竹梅围绕','黑色条纹纸，红色缎带花结','只想和你忘掉一切烦恼，尽情沉醉在最浪漫的气氛中。',383,298,100,3,1,1)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('一往情深--19枝红玫瑰，勿忘我适量',53,'/Image/一往情深--9枝红玫瑰，勿忘我适量.jpg','19枝红玫瑰，勿忘我适量','牛皮纸和深咖啡色条纹纸，银色缎带花结，魔力铁山灰包装盒','你的轻柔像阵微风，让我从容不迫，想让你知道，我对你始终一往情深。',315,249,100,3,1,1)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('不变的承诺--99枝红玫瑰',53,'/Image/不变的承诺--99枝红玫瑰.jpg','99枝红玫瑰','黑色雪梨纸，黑色条纹纸，玻璃纸卷，酒红色缎带花结','下雨的时候，给她撑一把雨伞；寒冷的时候，给她一个温暖的臂弯；天黑了，永远有一盏灯为她点亮；晨起时，给她一缕温暖的阳光。爱她，就送她一束99枝的玫瑰花！',599,499,100,3,1,1)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('一心一意--玫瑰11枝，粉色勿忘我0.3扎',53,'/Image/一心一意--玫瑰11枝，粉色勿忘我0.3扎.jpg','红玫瑰11枝，粉色(或者紫色）勿忘我0.3扎，栀子叶适量搭配','内层白色雾面纸，外层牛皮纸,咖啡色花结','11枝玫瑰，寓意一心一意！有情之人，天天是节。一句寒暖，一线相喧；一句叮咛，一笺相传；一份相思，一心相盼；一份爱意，一生相恋。',139,129,100,3,1,1)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('眷念--戴安娜粉玫瑰33枝，石竹梅围绕',53,'/Image/眷念--戴安娜粉玫瑰33枝，石竹梅围绕.jpg','戴安娜粉玫瑰33枝，石竹梅围绕','内层银灰色厚棉纸，外层豌豆花压纹纸，紫红色缎带花结','怒放的生命中，缘分让我们相守，万花丛中，只为寻到你那惊艳万芳的容颜，今生今世，值得我去爱的仅仅是你，你就是我最大的眷念。',382,298,100,3,3,1)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('你的香气--戴安娜粉玫瑰9枝',53,'/Image/你的香气--戴安娜粉玫瑰9枝.jpg','黛安娜玫瑰9枝，浅紫色勿忘我适量，栀子叶适量','白色雪梨纸，浅紫香芋紫人造纸，豆沙色缎带花结','花开的季节，我迷恋你的香气，那是我思念的味道！',215,128,100,3,3,1)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('99枝玫瑰加价值68元德芙心语巧克力98克',53,'/Image/99枝玫瑰赠价值68元德芙心语巧克力98克--33枝戴安娜、66枝红玫瑰、1扎满天星.jpg','33枝戴安娜、66枝红玫瑰、1扎满天星+德芙心语巧克力98克','粉色opp雾面纸6张、酒红色缎带2米','不管时空怎么转变，世界怎么改变，你的爱总在我心间。',869,599,100,3,1,1)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('不变的心--戴安娜玫瑰66枝',53,'/Image/不变的心--戴安娜玫瑰66枝.jpg','戴安娜玫瑰66枝 勿忘我适量围绕','内层粉色厚棉纸 外层银灰色短平板印花树叶草 粉紫双色缎带','爱上你是我今生最大的幸福，想你是我最甜蜜的痛苦，和你在一起是我的骄傲。没有你，我就像一只迷失了方向的船。',601,459,5,3,1,1)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('牵手一生--红玫瑰19枝',53,'/Image/牵手一生--红玫瑰19枝.jpg','红玫瑰19枝，满天星围绕','白色opp雾面纸 浅紫香芋紫人造双面纸 白绿粗罗纹带','"19枝红玫瑰寓意：永远爱你此情不渝。想念那条街,想念与你遇见的地方,想念和你的第一次牵手……永远无法忘记这些往昔的喜悦,它将永驻心间！"',281,219,100,3,1,1)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('用心爱你--99枝：33枝戴安娜＋66枝红玫瑰',53,'/Image/用心爱你--99枝：33枝戴安娜＋66枝红玫瑰.jpg','33枝戴安娜、66枝红玫瑰、1扎满天星','粉色opp雾面纸6张、酒红色缎带2米','不管时空怎么转变，世界怎么改变，你的爱总在我心间。',869,699,100,3,1,1)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('甜美公主--白玫瑰22枝，粉佳人粉玫瑰14枝，粉色桔梗5枝',53,'/Image/甜美公主--白玫瑰22枝，粉佳人粉玫瑰14枝，粉色桔梗5枝.jpg','各色玫瑰共36枝：白玫瑰22枝，粉佳人粉玫瑰14枝；粉色桔梗5枝，尤加利0.5扎','绿色双面人造纸，白咖罗纹带花结','再多一点点距离，我就能靠近你。清晰甜美的空气里，爱你到不能呼吸。',485,459,100,3,4,1)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('缅怀--白菊，黄菊及白百合',53,'/Image/缅怀--白菊，黄菊及白百合.jpg','9朵白菊花，7朵黄菊花，2枝多头白百合，搭配散尾叶、黄莺','白色平面纸纸，深蓝色条纹纸，白纱及白色缎带包扎','哀思',269,219,100,5,4,5)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('清明节花束B--白玫瑰，白百合，白菊或白康乃馨点缀',53,'/Image/清明节花束B--白玫瑰，白百合，白菊或白康乃馨点缀.jpg','白玫瑰9枝+白香水百合2枝，白菊或白康乃馨点缀，配绿色叶材','白色平面纸2张单面包装，白色纱花结','哀思',212,192,100,5,4,2)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('清明节花束A-- 12枝白色玫瑰',53,'/Image/清明节花束A-- 12枝白色玫瑰.jpg','12枝白色玫瑰','塑料纸包装','哀悼',186,156,100,5,4,1)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('追忆--白菊，白玫瑰，白百合',53,'/Image/追忆--白菊，白玫瑰，白百合.jpg','白菊6枝，白玫瑰6枝，白百合3枝（多头）','浅灰色皱纹纸圆形包装，白色丝带束扎','追忆',266,226,100,5,4,5)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('馨情无限--戴安娜玫瑰11枝，红色康乃馨11支，红色石竹梅4枝',53,'/Image/馨情无限--戴安娜玫瑰11枝，红色康乃馨11支，红色石竹梅4枝.jpg','戴安娜粉玫瑰11枝，红色康乃馨11支，红色石竹梅4枝，栀子叶4枝','蓝灰色绝色纸1张，粉色OPP雾面纸1米，白绿粗条纹罗文带1米，魔力铁山灰盒大','曾经有人说“回家叫一声妈妈，是一件很幸福的事”。直到现在，才体会到这种甜蜜，原来我一直都过得如此美满...因为有妳，我的妈妈！',249,229,100,6,3,4)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('幸福绽放--粉色康乃馨19枝',53,'/Image/幸福绽放--粉色康乃馨19枝.jpg','粉色康乃馨19枝，搭配适量石竹梅、紫色勿忘我、栀子叶','内层紫红色不织布，外层粉色白牛皮纸，玫红色缎带花结','携带一束鲜花来到您的身旁，花儿浓缩了我对您的祝福，您在我的心里永远美丽、漂亮！',215,169,100,6,3,4)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('感激--29枝红康乃馨,2枝粉百合',53,'/Image/感激--29枝红康乃馨,2枝粉百合.jpg','2枝粉百合,2枝粉百合，29枝红康乃馨，适量黄莺搭配','内层香槟色皱纸，红色平面纸，绿色缎带花结','常怀感恩之心的人是最幸福的，常怀感激之情的生活是最甜美的！',278,218,100,6,1,4)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('暖暖的问候--苏醒11枝、白百合2枝、浅粉色康乃馨9枝',53,'/Image/暖暖的问候--苏醒11枝、白百合2枝、浅粉色康乃馨9枝.jpg','苏醒11枝、白百合2枝、浅粉色康乃馨9枝、白色相思梅2枝','绿色条纹纸1张、米白色平面纸1张、银灰色缎带1米、魔力铁山灰盒（大）','康乃馨+百合，温暖、纯洁，康乃馨+玫瑰，共同表达对妈妈的爱。暖暖的心意，让花儿传送：请你把这最诚挚的祝福带在身边，让幸福永远伴随你!',332,259,100,9,4,4)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('留住好时光--粉绣球1枝，粉雪山玫瑰6枝',53,'/Image/留住好时光--粉绣球1枝，粉雪山玫瑰6枝.jpg','粉绣球1枝，粉雪山玫瑰6枝，粉桔梗0.3扎，栀子叶0.5扎','长方形手提花篮1个','让每点阳光，洒于你脸庞；令你的微笑，比花更盛放！',306,239,100,8,3,1)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('风一样的女子--向日葵3枝，戴安娜粉玫瑰3枝',53,'/Image/风一样的女子--向日葵3枝，戴安娜粉玫瑰3枝.jpg','向日葵3枝，戴安娜粉玫瑰3枝，白色洋桔梗0.2扎，满天星0.1扎，龙柳少许,粉色雾面纸2.5米','白色雪梨纸1张,米色/粉色缎带各1米','秋风乍起，柳枝纷飞，你的笑容，在初秋萦绕，像风拥抱了青草，我也顺势被倾倒。',242,188,100,7,2,3)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('圆满--粉百合2枝、红玫瑰9枝、红色康乃馨15枝',53,'/Image/圆满--粉百合2枝、红玫瑰9枝、红色康乃馨15枝.jpg','粉百合2枝、红玫瑰9枝、叶上花1扎、红色康乃馨15枝、粉色洋桔梗0.5扎','有柄花篮一个','载着寓意希望的鲜花驶向快乐幸福的彼岸……?',368,286,100,6,1,4)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('幸福满溢--多头百合2枝，卡罗拉玫瑰8枝，红太阳花6枝',53,'/Image/幸福满溢--多头百合2枝，卡罗拉玫瑰8枝，红太阳花6枝.jpg','多头百合2枝（5朵以上/枝），卡罗拉玫瑰8枝，红太阳花6枝，混色石竹梅0.3扎，栀子叶0.5扎','圆型手提花篮1个','微微的秋意带走了盛夏的酷暑，带不走我关注你的脚步，愿你快乐幸福满溢！',282,219,100,5,4,2)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('柔情蜜意--11枝戴安娜粉玫瑰，白百合2枝',53,'/Image/柔情蜜意--11枝戴安娜粉玫瑰，白百合2枝.jpg','11枝戴安娜粉玫瑰，白百合2枝，白色相思梅5枝，栀子叶0.5扎','浅紫香芋紫人造纸2张，米白色缎带1.5米','用一切美好的词语来形容你都不为过，你值得被世界温柔以待。',265,206,100,4,3,1)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('优雅馨情--紫色康乃馨66枝，粉色多头康乃馨15枝',53,'/Image/优雅馨情--紫色康乃馨66枝，粉色多头康乃馨15枝.jpg','紫色康乃馨66枝，粉色多头康乃馨15枝，栀子叶2扎','粉色opp雾面纸3张、浅紫香芋紫人造纸2张、白咖细条纹罗文带1.5米','“岁月极美，在于它必然的流逝”，唯鲜花带来的美好，教师的爱与温暖永存！',469,319,100,3,5,4)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('温情祝福--紫红色康乃馨9枝，粉色多头香水百合2枝，紫色桔梗4枝',53,'/Image/温情祝福--紫红色康乃馨9枝，粉色多头香水百合2枝，紫色桔梗4枝.jpg','紫红色康乃馨9枝，粉色多头香水百合2枝，紫色桔梗4枝，搭配适量尤加利叶','米白色平面纸，青灰色双面牛皮纸，银灰色缎带花结','你如春天的风一般温暖，深深的话我们浅浅地说',252,195,100,2,5,4)
Insert into Product(ProductName,ProductVolume,Picture1,Material,Package,FlowerLanguage,OriginalPrice,NowPrice,Inventory,FestivalId,ColorId,FlowerKindId) 
values('真诚祝愿--花篮',53,'/Image/真诚祝愿--花篮.jpg','粉色香水百合3枝，红色康乃馨11枝，天堂鸟2枝，跳舞兰5枝，填充适量红色多头康和香槟色桔梗，散尾葵（编织）4枝，绿叶适量','有柄花篮一个','所有美丽都源于真挚与坦诚，虽然幸福会转瞬即逝，快乐却能持久，一份诚挚的祝福，一份真诚的问候，愿你快乐每一天！',368,288,100,1,1,2)
GO



--插入购物车ShoppingCar表数据
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

--插入订单Orders表数据
Insert into Orders(MembersId,OrdersName,OrdersPhone,ConsigneeName,ConsigneePhone,ConsigneeAddress,carOrproduct,ProductIdList,DeliveryTime,Price,OrderNumbers,Status) 
values(2,'周瑜','12345678901','小乔','12345678901','广东省广州市天源路789号',0,'1','2018-10-20',298,'3',0)
Insert into Orders(MembersId,OrdersName,OrdersPhone,ConsigneeName,ConsigneePhone,ConsigneeAddress,carOrproduct,ProductIdList,DeliveryTime,Price,OrderNumbers,Status)  
values(2,'周瑜','12345678901','小乔','12345678901','广东省广州市天源路789号',0,'2,3','2018-10-20',1995,'1,2',1)
Insert into Orders(MembersId,OrdersName,OrdersPhone,ConsigneeName,ConsigneePhone,ConsigneeAddress,carOrproduct,ProductIdList,DeliveryTime,Price,OrderNumbers,Status) 
values(2,'周瑜','12345678901','小乔','12345678901','广东省广州市天源路789号',0,'1,4,5','2018-10-20',2304,'1,2,3',2)
Insert into Orders(MembersId,OrdersName,OrdersPhone,ConsigneeName,ConsigneePhone,ConsigneeAddress,carOrproduct,ProductIdList,DeliveryTime,Price,OrderNumbers,Status) 
values(2,'周瑜','12345678901','小乔','12345678901','广东省广州市天源路789号',0,'1,2,3','2018-10-20',2293,'3,2,1',3)
Insert into Orders(MembersId,OrdersName,OrdersPhone,ConsigneeName,ConsigneePhone,ConsigneeAddress,carOrproduct,ProductIdList,DeliveryTime,Price,OrderNumbers,Status) 
values(2,'周瑜','12345678901','小乔','12345678901','广东省广州市天源路789号',0,'1,2','2018-10-20',796,'1,2',4)

Insert into Orders(MembersId,OrdersName,OrdersPhone,ConsigneeName,ConsigneePhone,ConsigneeAddress,carOrproduct,ProductIdList,DeliveryTime,Price,OrderNumbers,Status) 
values(3,'孙权','12345678901','步练师','12345678901','广东省广州市天源路789号',1,'4,5,11','2018-10-20',7055,'1,1,1',0)
Insert into Orders(MembersId,OrdersName,OrdersPhone,ConsigneeName,ConsigneePhone,ConsigneeAddress,carOrproduct,ProductIdList,DeliveryTime,Price,OrderNumbers,Status) 
values(3,'孙权','12345678901','步练师','12345678901','广东省广州市天源路789号',1,'1,4,12','2018-10-20',3442,'1,2,1',1)
Insert into Orders(MembersId,OrdersName,OrdersPhone,ConsigneeName,ConsigneePhone,ConsigneeAddress,carOrproduct,ProductIdList,DeliveryTime,Price,OrderNumbers,Status) 
values(3,'孙权','12345678901','步练师','12345678901','广东省广州市天源路789号',1,'6,9,10','2018-10-20',9729,'1,3,6',2)
Insert into Orders(MembersId,OrdersName,OrdersPhone,ConsigneeName,ConsigneePhone,ConsigneeAddress,carOrproduct,ProductIdList,DeliveryTime,Price,OrderNumbers,Status)  
values(3,'孙权','12345678901','步练师','12345678901','广东省广州市天源路789号',1,'5,7','2018-10-20',5683,'2,5',3)
Insert into Orders(MembersId,OrdersName,OrdersPhone,ConsigneeName,ConsigneePhone,ConsigneeAddress,carOrproduct,ProductIdList,DeliveryTime,Price,OrderNumbers,Status) 
values(3,'孙权','12345678901','步练师','12345678901','广东省广州市天源路789号',1,'2,3,8','2018-10-20',5667,'2,2,2',4)
Insert into Orders(MembersId,OrdersName,OrdersPhone,ConsigneeName,ConsigneePhone,ConsigneeAddress,carOrproduct,ProductIdList,DeliveryTime,Price,OrderNumbers,Status) 
values(2,'孙权','12345678901','步练师','12345678901','广东省广州市天源路789号',0,'2,3,2','2018-10-20',2493,'2,2,7',1)
Insert into Orders(MembersId,OrdersName,OrdersPhone,ConsigneeName,ConsigneePhone,ConsigneeAddress,carOrproduct,ProductIdList,DeliveryTime,Price,OrderNumbers,Status) 
values(2,'孙权','12345678901','步练师','12345678901','广东省广州市天源路789号',0,'1,8,2','2018-10-20',4468,'1,2,3',1)
Insert into Orders(MembersId,OrdersName,OrdersPhone,ConsigneeName,ConsigneePhone,ConsigneeAddress,carOrproduct,ProductIdList,DeliveryTime,Price,OrderNumbers,Status) 
values(2,'孙权','12345678901','步练师','12345678901','广东省广州市天源路789号',0,'1,8,2','2018-10-20',4468,'1,2,3',1)
GO


