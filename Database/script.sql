USE [master]
GO
/****** Object:  Database [PrintBarcode]    Script Date: 01-05-2025 14:26:19 ******/
CREATE DATABASE [PrintBarcode]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PrintBarcode', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER01\MSSQL\DATA\PrintBarcode.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PrintBarcode_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER01\MSSQL\DATA\PrintBarcode_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [PrintBarcode] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PrintBarcode].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PrintBarcode] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PrintBarcode] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PrintBarcode] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PrintBarcode] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PrintBarcode] SET ARITHABORT OFF 
GO
ALTER DATABASE [PrintBarcode] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PrintBarcode] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PrintBarcode] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PrintBarcode] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PrintBarcode] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PrintBarcode] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PrintBarcode] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PrintBarcode] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PrintBarcode] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PrintBarcode] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PrintBarcode] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PrintBarcode] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PrintBarcode] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PrintBarcode] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PrintBarcode] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PrintBarcode] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PrintBarcode] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PrintBarcode] SET RECOVERY FULL 
GO
ALTER DATABASE [PrintBarcode] SET  MULTI_USER 
GO
ALTER DATABASE [PrintBarcode] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PrintBarcode] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PrintBarcode] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PrintBarcode] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PrintBarcode] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PrintBarcode] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'PrintBarcode', N'ON'
GO
ALTER DATABASE [PrintBarcode] SET QUERY_STORE = ON
GO
ALTER DATABASE [PrintBarcode] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [PrintBarcode]
GO
/****** Object:  Table [dbo].[planData]    Script Date: 01-05-2025 14:26:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[planData](
	[planID] [int] IDENTITY(1,1) NOT NULL,
	[plant] [nvarchar](50) NOT NULL,
	[line] [nvarchar](50) NOT NULL,
	[productName] [nvarchar](50) NOT NULL,
	[unit] [int] NOT NULL,
	[unitPerMono] [int] NOT NULL,
	[monoPerPlan] [int] NOT NULL,
	[date] [datetime] NOT NULL,
 CONSTRAINT [PK_planData] PRIMARY KEY CLUSTERED 
(
	[planID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[plantData]    Script Date: 01-05-2025 14:26:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[plantData](
	[plantID] [int] IDENTITY(1,1) NOT NULL,
	[plantName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_plantData] PRIMARY KEY CLUSTERED 
(
	[plantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[plantMaster]    Script Date: 01-05-2025 14:26:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[plantMaster](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userID] [int] NOT NULL,
	[plantID] [int] NOT NULL,
 CONSTRAINT [PK_plantMaster] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[products]    Script Date: 01-05-2025 14:26:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products](
	[productID] [int] IDENTITY(1,1) NOT NULL,
	[productName] [nvarchar](150) NOT NULL,
	[description1] [nvarchar](max) NULL,
	[description2] [nvarchar](max) NULL,
	[description3] [nvarchar](max) NULL,
	[mrp] [int] NOT NULL,
	[weight] [int] NOT NULL,
	[weightUnit] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_products] PRIMARY KEY CLUSTERED 
(
	[productID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[remainingPlanData]    Script Date: 01-05-2025 14:26:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[remainingPlanData](
	[planID] [int] IDENTITY(1,1) NOT NULL,
	[plant] [nvarchar](50) NOT NULL,
	[line] [nvarchar](50) NOT NULL,
	[productName] [nvarchar](50) NOT NULL,
	[remaningUnit] [int] NOT NULL,
	[remainingMono] [int] NOT NULL,
	[remainingPlan] [int] NOT NULL,
	[date] [datetime] NOT NULL,
 CONSTRAINT [PK_remainingPlanData] PRIMARY KEY CLUSTERED 
(
	[planID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[stickerData]    Script Date: 01-05-2025 14:26:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[stickerData](
	[stickerID] [int] IDENTITY(100000,1) NOT NULL,
	[plant] [nvarchar](50) NOT NULL,
	[line] [nvarchar](50) NOT NULL,
	[productName] [nvarchar](50) NOT NULL,
	[sticker] [nvarchar](max) NOT NULL,
	[barcode] [nvarchar](50) NOT NULL,
	[date] [datetime] NOT NULL,
	[status] [bit] NOT NULL,
	[category] [nvarchar](50) NULL,
 CONSTRAINT [PK_stickerData] PRIMARY KEY CLUSTERED 
(
	[stickerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[stickerSize]    Script Date: 01-05-2025 14:26:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[stickerSize](
	[printID] [int] IDENTITY(1,1) NOT NULL,
	[plant] [nvarchar](50) NOT NULL,
	[category] [nvarchar](50) NOT NULL,
	[line] [nvarchar](max) NOT NULL,
	[height] [int] NOT NULL,
	[width] [int] NOT NULL,
 CONSTRAINT [PK_barcodePrint] PRIMARY KEY CLUSTERED 
(
	[printID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[userData]    Script Date: 01-05-2025 14:26:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[userData](
	[userID] [int] IDENTITY(1,1) NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[plants] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_userData] PRIMARY KEY CLUSTERED 
(
	[userID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[addPlanData]    Script Date: 01-05-2025 14:26:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[addPlanData]
    @plant NVARCHAR(50),
    @line NVARCHAR(50),
    @productName NVARCHAR(50),
    @unit INT,
    @unitPerMono INT,
    @monoPerPlan INT
AS
BEGIN
	begin try
		if exists (select 1 from planData where plant=@plant and line=@line and productName=@productName and date=CAST(GETDATE() AS DATE))
		begin
			select 'Plan Already Exist' as message
		end
		else if not exists (select 1 from stickerSize where plant=@plant and line=@line and category='Unit')
		begin
			SELECT 'For Plant=' + @plant  
			 + ', Line=' + @line  
			 + ', Category=''Unit'' Sticker Size Doesn''t Exist' AS message;
		end
		else if @unitPerMono != 0 AND NOT exists(select 1 from stickerSize where plant=@plant and line=@line and category='Mono')
		begin
			SELECT 'For Plant=' + @plant  
			 + ', Line=' + @line  
			 + ', Category=''Mono'' Sticker Size Doesn''t Exist' AS message;
		end
		else if @monoPerPlan!=0 and NOT exists ( select 1 from stickerSize where plant=@plant and line=@line and category='Plan')
		begin
			SELECT 'For Plant=' + @plant  
			 + ', Line=' + @line  
			 + ', Category=''Plan'' Sticker Size Doesn''t Exist' AS message;
		end
		else 
		begin
			SET NOCOUNT ON;
			declare @remainingMono int=0,@remainingPlan int=0
			if @unitPerMono>0
			begin 
				set @remainingMono=@unit/@unitPerMono;
				if @monoPerPlan >0
				begin
					set @remainingPlan=@remainingMono/@monoPerPlan
				end
			end
			INSERT INTO [dbo].[planData] ([plant], [line], [productName], [unit], [unitPerMono], [monoPerPlan], [date])
				VALUES (@plant, @line, @productName, @unit, @unitPerMono, @monoPerPlan, CAST(GETDATE() AS DATE));
			 INSERT INTO dbo.remainingPlanData
				(plant, line, productName, remaningUnit, remainingMono, remainingPlan,[date])
			VALUES
				(@plant, @line, @productName, @unit, @remainingMono, @remainingPlan, CAST(GETDATE() AS DATE));
			select 'success' as message
		end
	end try
	begin catch
		Select 'Some Error Occured, Try Again!' as message
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[addProduct]    Script Date: 01-05-2025 14:26:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[addProduct]
	@productName nvarchar(150),
	@description1 nvarchar(MAX),
	@description2 nvarchar(MAX),
	@description3 nvarchar(MAX),
	@mrp int,
	@weight int,
	@weightUnit nvarchar(150)
as
begin 
	begin try
	if exists(select 1 from products where productName=@productName)
	begin
		select 'Product Already exist' as message
	end
	else
	begin
		INSERT INTO products (productName,description1,description2,description3,mrp,weight,weightUnit)
		VALUES (@productName,@description1,@description2,@description3,@mrp,@weight,@weightUnit)
		select 'Product Successfully Added' as message
	end
	end try
	begin catch
		select 'Some error occured' as message
	end catch
end
GO
/****** Object:  StoredProcedure [dbo].[checkStickerSize]    Script Date: 01-05-2025 14:26:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[checkStickerSize]
	@plant nvarchar(MAX),
	@category nvarchar(MAX),
	@line nvarchar(MAX)
As
begin
	begin try
		if exists(select 1 from stickerSize where plant=@plant and category=@category and line=@line)
		begin
			SELECT CONCAT('Plan already exists: Plant=', @plant,', Line=', @line,', Category=', @category) AS message;
		end
		else
		begin
			select 'success' as message;
		end
	end try
	begin catch
	select 'Some error occured' as message
	end catch
end
GO
/****** Object:  StoredProcedure [dbo].[fectchHeightWidth]    Script Date: 01-05-2025 14:26:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[fectchHeightWidth]
	@plant nvarchar(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @missingCategories NVARCHAR(MAX) = '';

    -- Check for "unit".
    IF NOT EXISTS (
        SELECT 1 
        FROM barcodePrint 
        WHERE plant=@plant and category LIKE '%unit%'
    )
    BEGIN
        SET @missingCategories = @missingCategories + 'unit ';
    END

    -- Check for "mono"
    IF NOT EXISTS (
        SELECT 1 
        FROM barcodePrint 
        WHERE plant=@plant and category LIKE '%mono%'
    )
    BEGIN
        SET @missingCategories = @missingCategories + 'mono ';
    END

    -- Check for "plan"
    IF NOT EXISTS (
        SELECT 1 
        FROM barcodePrint 
        WHERE plant=@plant and category LIKE '%plan%'
    )
    BEGIN
        SET @missingCategories = @missingCategories + 'plan ';
    END

    IF LEN(@missingCategories) > 0
    BEGIN
        -- Remove the trailing comma and space
        SELECT 'Please add size for: ' + @missingCategories AS message;
    END
    ELSE
    BEGIN
        -- All required categories exist.
        -- Return the height and width values for each category separately.
		Select 'success' as message
        SELECT Category, Height, Width 
        FROM 
        (
            SELECT 'unit' AS Category, height, width
            FROM barcodePrint 
            WHERE plant=@plant and category LIKE '%unit%'
            
            UNION ALL
            
            SELECT 'mono' AS Category, height, width
            FROM barcodePrint 
            WHERE plant=@plant and category LIKE '%mono%'
            
            UNION ALL
            
            SELECT 'plan' AS Category, height, width
            FROM barcodePrint 
            WHERE plant=@plant and category LIKE '%plan%'
        ) AS Combined
        ORDER BY Category;
    END
END
GO
/****** Object:  StoredProcedure [dbo].[fetchPlanData]    Script Date: 01-05-2025 14:26:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[fetchPlanData]
	@plant nvarchar(50),
	@line nvarchar(50),
	@productName nvarchar(50)
as 
begin
	begin try
		if not exists(select 1 from planData where plant=@plant and line=@line and productName=@productName and [date]=cast(getdate()as date))
		begin
			select 'Plan Doesn''t exist' as message
		end
		else
		begin
			select 'success' as message
			select * from planData where plant=@plant and line=@line and productName=@productName and [date]=cast(getdate()as date)
			select * from remainingPlanData where plant=@plant and line=@line and productName=@productName and [date]=cast(getdate()as date)
		end
	end try
	begin catch
		select 'Some Error Occured' as message
	end catch
end
GO
/****** Object:  StoredProcedure [dbo].[fetchSingleProduct]    Script Date: 01-05-2025 14:26:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[fetchSingleProduct]
	@productName nvarchar(50)
as
begin
	begin try
		select 'success' as message
		select * from products where productName=@productName
	end try
	begin catch
		select 'Some Error Occurred, Try Again!' as message
	end catch
end
GO
/****** Object:  StoredProcedure [dbo].[fetchSingleSticker]    Script Date: 01-05-2025 14:26:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[fetchSingleSticker]
	@stickerID int
AS
begin
	begin try
		if exists(select 1 from stickerData where stickerID=@stickerID)
		begin
			select 'success' as message
			select sticker,productName,category from stickerData where stickerID=@stickerID
		end
		else 
		begin
			select 'Sticker Doesn''t Exist' as message
		end
	end try
		
	begin catch
		select 'Some error occured' as message
	end catch
end
GO
/****** Object:  StoredProcedure [dbo].[fetchStickerData]    Script Date: 01-05-2025 14:26:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchStickerData]
    @category NVARCHAR(50) = NULL,
    @date     DATE         = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        stickerID,
        productName,
        sticker,
        barcode,
        [date],
        status,
        category
    FROM dbo.stickerData
    WHERE
        -- category filter (optional)
        (@category IS NULL OR category = @category)
        -- date filter: if no @date supplied, use today’s date
        AND CAST([date] AS DATE) = ISNULL(@date, CAST(GETDATE() AS DATE))
    ORDER BY 
        stickerID ASC;
END
GO
/****** Object:  StoredProcedure [dbo].[fetchStickerHeightWidth]    Script Date: 01-05-2025 14:26:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[fetchStickerHeightWidth]
	@plant nvarchar(50),
	@line nvarchar(50),
	@category nvarchar(50)
as
begin
	begin try
		select 'success' as message
		select height,width from  stickerSize where plant=@plant and line=@line and category=@category;
	end try
	begin catch
		select 'Some Error Occured, Try Again!' as message
	end catch
end;
GO
/****** Object:  StoredProcedure [dbo].[fetchUser]    Script Date: 01-05-2025 14:26:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[fetchUser]
    @email    NVARCHAR(50),
    @password NVARCHAR(50)
AS
BEGIN
    BEGIN TRY
        DECLARE @userID INT;

        -- 1) Get the userID for these credentials
        SELECT @userID = userID
        FROM dbo.userData
        WHERE email    = @email
          AND password = @password;

        -- 2) If no match, return "Invalid credentials"
        IF @userID IS NULL
        BEGIN
            SELECT 'Invalid credentials' AS message;
            RETURN;
        END

        -- 3) Valid login: first return success message...
        SELECT 'success' AS message;

        -- 4) ...then return all plants for that user
        SELECT 
            pd.plantName
        FROM dbo.plantMaster pm
        INNER JOIN dbo.plantData  pd
            ON pm.plantID = pd.plantID
        WHERE pm.userID = @userID;
    END TRY
    BEGIN CATCH
        SELECT 'Some Error Occurred' AS message;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[GetBarcodePrints]    Script Date: 01-05-2025 14:26:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetBarcodePrints]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        printID,
        plant,
        category,
        line,
        height,
        width
    FROM dbo.stickerSize
    ORDER BY
        plant ASC,
		line ASC,
        category ASC
END
GO
/****** Object:  StoredProcedure [dbo].[GetBarcodesByDate]    Script Date: 01-05-2025 14:26:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetBarcodesByDate]
    @SelectedDate DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT [plantLineID],
           [plant],
           [line],
           [units],
           [mono],
           [plan],
           [date]
    FROM [PrintBarcode].[dbo].[barcodePrinted]
    WHERE CAST([date] AS DATE) = @SelectedDate
    ORDER BY [plant], [line];
END;
GO
/****** Object:  StoredProcedure [dbo].[GetPlanData]    Script Date: 01-05-2025 14:26:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetPlanData]
    @selectedDate DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [dbo].[planData]
    WHERE CAST([date] AS DATE) = @selectedDate;
END
GO
/****** Object:  StoredProcedure [dbo].[GetProducts]    Script Date: 01-05-2025 14:26:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetProducts]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        productID,
        productName,
        description1,
        description2,
        description3,
        mrp,
        weight,
        weightUnit
    FROM dbo.products
    ORDER BY productName ASC;
END
GO
/****** Object:  StoredProcedure [dbo].[InsertStickerData]    Script Date: 01-05-2025 14:26:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** 1) Alter InsertStickerData to defer sticker & barcode insertion ******/
CREATE PROCEDURE [dbo].[InsertStickerData]
    @plant        NVARCHAR(50),
    @line         NVARCHAR(50),
    @productName  NVARCHAR(50),
    @category     NVARCHAR(50)
AS
BEGIN
	begin try
		SET NOCOUNT ON;

		INSERT INTO dbo.stickerData (plant, line, productName, sticker, barcode, [date], status, category)
		VALUES(@plant,@line,@productName,
			 '',          -- sticker to be added later
			 '',          -- barcode     to be added later
			 Cast(GETDATE()as date),     -- timestamp
			 0,             -- initial status
			 @category);

		DECLARE @newStickerID INT = SCOPE_IDENTITY();
		SELECT 'success' AS message,@newStickerID AS stickerID;
	end try
	begin catch
	select 'Some Error Occured, Try Again!' as message
	end catch

END
GO
/****** Object:  StoredProcedure [dbo].[insertStickerSize]    Script Date: 01-05-2025 14:26:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[insertStickerSize]
	@plant nvarchar(MAX),
	@category nvarchar(MAX),
	@line nvarchar(MAX),
	@height int,
	@width int

As
begin
	begin try
		if exists(select 1 from stickerSize where plant=@plant and category=@category and line=@line)
		begin
			select 'Plan Already Exist' as message
		end
		else
		begin
			INSERT INTO stickerSize(plant, category, line, height, width)
			VALUES (@plant, @category, @line, @height, @width);
			select 'success' as message;
		end
	end try
	begin catch
	select 'Some error occured' as message
	end catch
end
GO
/****** Object:  StoredProcedure [dbo].[updateRemainingPlanData]    Script Date: 01-05-2025 14:26:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateRemainingPlanData]
	@plant nvarchar(50),
	@line nvarchar(50),
	@productName nvarchar(50),
	@remainingUnit   INT,
    @remainingMono   INT,
    @remainingPlan   INT
as
begin
	begin try
		SET NOCOUNT ON;
		UPDATE dbo.remainingPlanData SET remaningUnit   = @remainingUnit,remainingMono  = @remainingMono,remainingPlan  = @remainingPlan
        WHERE plant = @plant AND line = @line AND productName = @productName AND CAST([date] AS DATE) = CAST(GETDATE() AS DATE);
		SELECT 'success' AS message;
	end try
	begin catch
		select 'Some Error Occured,Try Again!' as message
	end catch
end
GO
/****** Object:  StoredProcedure [dbo].[UpdateStickerData]    Script Date: 01-05-2025 14:26:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateStickerData]
    @stickerID   INT,
    @sticker     NVARCHAR(MAX),
    @barcode     NVARCHAR(50)
AS
BEGIN
	begin try
		SET NOCOUNT ON;
		UPDATE dbo.stickerData SET sticker = @sticker, barcode = @barcode WHERE stickerID = @stickerID;
		SELECT 'success' AS message;
	end try
	begin catch
		select 'Some Error Occured' as message;
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateStickerStatus]    Script Date: 01-05-2025 14:26:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateStickerStatus]
    @stickerID  INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.stickerData
    SET status = 1
    WHERE stickerID = @stickerID;

    IF @@ROWCOUNT = 0
    BEGIN
		select 'No sticker found' as message
    END
    ELSE
    BEGIN
		select 'success' as message
    END
END
GO
/****** Object:  StoredProcedure [dbo].[UpsertBarcodePrinted]    Script Date: 01-05-2025 14:26:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpsertBarcodePrinted]
    @plant    NVARCHAR(50),
    @line     NVARCHAR(50),
    @units    INT,
    @mono     INT,
    @plan     INT,
    @date     DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- default to today if no date supplied
    IF @date IS NULL
        SET @date = CAST(GETDATE() AS DATE);

    IF EXISTS (
        SELECT 1
        FROM dbo.barcodePrinted
        WHERE plant = @plant
          AND line  = @line
          AND [date] = @date
    )
    BEGIN
        -- already have a row for this plant/line/date → add to the existing totals
        UPDATE dbo.barcodePrinted
           SET units = units + @units,
               mono  = mono  + @mono,
               [plan]  = [plan]  + @plan
        WHERE plant = @plant
          AND line  = @line
          AND [date] = @date;
    END
    ELSE
    BEGIN
        -- no row yet → insert a brand‑new one
        INSERT INTO dbo.barcodePrinted
            (plant, line, units, mono, [plan], [date])
        VALUES
            (@plant, @line, @units, @mono, @plan, @date);
    END
END;
GO
USE [master]
GO
ALTER DATABASE [PrintBarcode] SET  READ_WRITE 
GO
