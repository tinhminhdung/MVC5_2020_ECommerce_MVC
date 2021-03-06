
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter table Products add Price1 nvarchar(50)
alter table Products add Price2 nvarchar(50)
alter table Products add Price3 nvarchar(50)
alter table Products add Price4 nvarchar(50)

GO
/****** Object:  StoredProcedure [dbo].[S_products_Insert]    Script Date: 01/30/2016 11:36:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[S_products_Insert]
	@icid		int,
	@ID_Hang		int,
	@sanxuat int,
	@Code		ntext,
	@Name		ntext,
	@Brief		ntext,
	@Contents		ntext,
	@search		ntext,
	@Images		ntext,
	@ImagesSmall		ntext,
	@Equals		tinyint,
	@Quantity		int,
	@Price		nvarchar(50),
	@OldPrice		nvarchar(50),
	@Chekdata		tinyint,
	@Create_Date		datetime,
	@Modified_Date		datetime,
	@Views		int,
	@lang		nchar(10),
	@News		tinyint,
	@Home		tinyint,
	@Check_01		tinyint,
	@Check_02		tinyint,
	@Check_03		tinyint,
	@Check_04		tinyint,
	@Check_05		tinyint,
	@Status		tinyint,
	@Titleseo		ntext,
	@Meta		ntext,
	@Keyword		ntext,
	@Anh		ntext,
	@Price1		nvarchar(50),
	@Price2		nvarchar(50),
	@Price3		nvarchar(50),
	@Price4		nvarchar(50)
AS
	INSERT INTO [products]([icid], [ID_Hang],[sanxuat], [Code], [Name], [Brief], [Contents], [search], [Images], [ImagesSmall], [Equals], [Quantity], [Price], [OldPrice], [Chekdata], [Create_Date], [Modified_Date], [Views], [lang], [News], [Home], [Check_01], [Check_02], [Check_03], [Check_04], [Check_05], [Status],[Titleseo],[Meta],[Keyword],[Anh],[Price1], [Price2], [Price3], [Price4])
	VALUES(@icid, @ID_Hang, @sanxuat, @Code, @Name, @Brief, @Contents, @search, @Images, @ImagesSmall, @Equals, @Quantity, @Price, @OldPrice, @Chekdata, @Create_Date, @Modified_Date, @Views, @lang, @News, @Home, @Check_01, @Check_02, @Check_03, @Check_04, @Check_05, @Status,@Titleseo,@Meta,@Keyword,@Anh, @Price1, @Price2, @Price3, @Price4)

	
	
	
	GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[S_products_Update]
	@ipid		int,
	@icid		int,
	@ID_Hang		int,
	@sanxuat	int,
	@Code		ntext,
	@Name		ntext,
	@Brief		ntext,
	@Contents		ntext,
	@search		ntext,
	@Images		ntext,
	@ImagesSmall		ntext,
	@Equals		tinyint,
	@Quantity		int,
	@Price		nvarchar(50),
	@OldPrice		nvarchar(50),
	@Chekdata		tinyint,
	@Create_Date		datetime,
	@Modified_Date		datetime,
	@Views		int,
	@lang		nchar(10),
	@News		tinyint,
	@Home		tinyint,
	@Check_01		tinyint,
	@Check_02		tinyint,
	@Check_03		tinyint,
	@Check_04		tinyint,
	@Check_05		tinyint,
	@Status		tinyint,
	@Titleseo		ntext,
	@Meta		ntext,
	@Keyword		ntext,
	@Anh		ntext,
	@Price1		nvarchar(50),
	@Price2		nvarchar(50),
	@Price3		nvarchar(50),
	@Price4		nvarchar(50)
AS
	UPDATE [products] SET [icid] = @icid, [ID_Hang] = @ID_Hang,[sanxuat]=@sanxuat, [Code] = @Code, [Name] = @Name, [Brief] = @Brief, [Contents] = @Contents, [search] = @search, [Images] = @Images, [ImagesSmall] = @ImagesSmall, [Equals] = @Equals, [Quantity] = @Quantity, [Price] = @Price, [OldPrice] = @OldPrice, [Chekdata] = @Chekdata, [Create_Date] = @Create_Date, [Modified_Date] = @Modified_Date, [Views] = @Views, [lang] = @lang, [News] = @News, [Home] = @Home, [Check_01] = @Check_01, [Check_02] = @Check_02, [Check_03] = @Check_03, [Check_04] = @Check_04, [Check_05] = @Check_05, [Status] = @Status,[Titleseo]=@Titleseo,[Meta]=@Meta,[Keyword]=@Keyword,[Anh]=@Anh,[Price1] = @Price1,[Price2] = @Price2,[Price3] = @Price3,[Price4] = @Price4
	 WHERE ipid IN (@ipid)

	
	GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

UPDATE [products] SET [Price1] = 0
UPDATE	[products] SET [Price2] = 0
UPDATE [products] SET [Price3] = 0
UPDATE	[products] SET [Price4] = 0
