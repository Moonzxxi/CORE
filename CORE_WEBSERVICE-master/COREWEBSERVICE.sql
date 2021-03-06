USE [master]
GO
/****** Object:  Database [CoreProyectoDB]    Script Date: 10/9/2019 10:41:51 AM ******/
CREATE DATABASE [CoreProyectoDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ManzanitaDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\ManzanitaDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ManzanitaDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\ManzanitaDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [CoreProyectoDB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CoreProyectoDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CoreProyectoDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CoreProyectoDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CoreProyectoDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CoreProyectoDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CoreProyectoDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [CoreProyectoDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CoreProyectoDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CoreProyectoDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CoreProyectoDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CoreProyectoDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CoreProyectoDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CoreProyectoDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CoreProyectoDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CoreProyectoDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CoreProyectoDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CoreProyectoDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CoreProyectoDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CoreProyectoDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CoreProyectoDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CoreProyectoDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CoreProyectoDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CoreProyectoDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CoreProyectoDB] SET RECOVERY FULL 
GO
ALTER DATABASE [CoreProyectoDB] SET  MULTI_USER 
GO
ALTER DATABASE [CoreProyectoDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CoreProyectoDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CoreProyectoDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CoreProyectoDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CoreProyectoDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'CoreProyectoDB', N'ON'
GO
ALTER DATABASE [CoreProyectoDB] SET QUERY_STORE = OFF
GO
USE [CoreProyectoDB]
GO
/****** Object:  Table [dbo].[accountTable]    Script Date: 10/9/2019 10:41:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[accountTable](
	[IDENTIFIER] [nvarchar](16) NOT NULL,
	[ACCOUNT_NAME] [nvarchar](128) NOT NULL,
	[ACCOUNT_TYPE] [nvarchar](64) NOT NULL,
	[ACCOUNT_STATE] [nvarchar](64) NOT NULL,
	[BALANCE] [decimal](18, 0) NOT NULL,
	[OPENDATE] [datetime] NOT NULL,
 CONSTRAINT [PK_accountTable] PRIMARY KEY CLUSTERED 
(
	[OPENDATE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[clientTable]    Script Date: 10/9/2019 10:41:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[clientTable](
	[IDENTIFIER] [nvarchar](16) NOT NULL,
	[NAME] [nvarchar](128) NOT NULL,
	[LAST] [nvarchar](128) NOT NULL,
	[ACCOUNTS] [int] NOT NULL,
	[PASSWORD] [nvarchar](256) NOT NULL,
	[PIN] [nvarchar](8) NOT NULL,
	[DIRECTION] [nvarchar](128) NOT NULL,
	[EMAIL] [nvarchar](64) NOT NULL,
	[STATE] [nvarchar](16) NOT NULL,
	[REGDATE] [datetime] NOT NULL,
	[LOGDATE] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IDENTIFIER] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[transactionTable]    Script Date: 10/9/2019 10:41:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[transactionTable](
	[IDENTIFIER_ROOT] [nvarchar](16) NOT NULL,
	[IDENTIFIER_AFFECTED] [nvarchar](16) NOT NULL,
	[ACCOUNT_ROOT] [nvarchar](128) NOT NULL,
	[ACCOUNT_AFFECTED] [nvarchar](128) NOT NULL,
	[TYPE] [nvarchar](16) NOT NULL,
	[TRANSDATE] [datetime] NOT NULL,
	[DESCRIPTION] [nvarchar](max) NOT NULL,
	[BALANCE] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_transactionTable] PRIMARY KEY CLUSTERED 
(
	[TRANSDATE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[accountTable] ADD  DEFAULT ('ACTIVA') FOR [ACCOUNT_STATE]
GO
ALTER TABLE [dbo].[accountTable] ADD  DEFAULT ((0.0)) FOR [BALANCE]
GO
ALTER TABLE [dbo].[accountTable] ADD  DEFAULT (getdate()) FOR [OPENDATE]
GO
ALTER TABLE [dbo].[clientTable] ADD  DEFAULT ((0)) FOR [ACCOUNTS]
GO
ALTER TABLE [dbo].[clientTable] ADD  DEFAULT ('SUSCRITO') FOR [STATE]
GO
ALTER TABLE [dbo].[clientTable] ADD  CONSTRAINT [DF_clientTable_REGDATE]  DEFAULT (getdate()) FOR [REGDATE]
GO
ALTER TABLE [dbo].[transactionTable] ADD  CONSTRAINT [DF_transactionTable_TRANSDATE]  DEFAULT (getdate()) FOR [TRANSDATE]
GO
ALTER TABLE [dbo].[accountTable]  WITH CHECK ADD FOREIGN KEY([IDENTIFIER])
REFERENCES [dbo].[clientTable] ([IDENTIFIER])
GO
ALTER TABLE [dbo].[accountTable]  WITH CHECK ADD FOREIGN KEY([IDENTIFIER])
REFERENCES [dbo].[clientTable] ([IDENTIFIER])
GO
/****** Object:  StoredProcedure [dbo].[accountDeactivate]    Script Date: 10/9/2019 10:41:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[accountDeactivate]
	@Identifier NVARCHAR(16),
	@Name NVARCHAR(128)
AS
	UPDATE dbo.accountTable
	SET ACCOUNT_STATE = 'INACTIVA'
	WHERE IDENTIFIER = @Identifier AND ACCOUNT_NAME = @Name;
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[accountReactivate]    Script Date: 10/9/2019 10:41:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[accountReactivate]
	@Identifier NVARCHAR(16),
	@Name NVARCHAR(128)
AS
	UPDATE dbo.accountTable
	SET ACCOUNT_STATE = 'ACTIVA'
	WHERE IDENTIFIER = @Identifier AND ACCOUNT_NAME = @Name;
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[accountRegister]    Script Date: 10/9/2019 10:41:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[accountRegister]
	@Identifier NVARCHAR(16),
	@Name NVARCHAR(128),
	@Type NVARCHAR(64)
AS
	INSERT INTO dbo.accountTable (IDENTIFIER, ACCOUNT_NAME, ACCOUNT_TYPE) 
	VALUES (@Identifier, @Name, @Type)

	UPDATE dbo.clientTable
	SET ACCOUNTS = ACCOUNTS + 1
	WHERE IDENTIFIER = @Identifier
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[bankDeposit]    Script Date: 10/9/2019 10:41:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[bankDeposit]
	@Amount DECIMAL,
	@Identifier NVARCHAR(16),
	@Name NVARCHAR(128),
	@Description NVARCHAR(MAX)
AS
	UPDATE dbo.accountTable
	SET BALANCE = BALANCE + @Amount
	WHERE IDENTIFIER = @Identifier AND ACCOUNT_NAME = @Name

	INSERT INTO dbo.transactionTable (IDENTIFIER_ROOT, IDENTIFIER_AFFECTED, ACCOUNT_ROOT, ACCOUNT_AFFECTED, TYPE, DESCRIPTION, BALANCE)
	VALUES (@Identifier, 'N/A', @Name, 'N/A', 'DEPOSITO', @Description, @Amount)
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[bankTransfer]    Script Date: 10/9/2019 10:41:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[bankTransfer]
	@Amount DECIMAL,
	@Identifier1 NVARCHAR(16),
	@Name1 NVARCHAR(128),
	@Identifier2 NVARCHAR(16),
	@Name2 NVARCHAR(128),
	@Description NVARCHAR(MAX)
AS
	UPDATE dbo.accountTable
	SET BALANCE = BALANCE - @Amount
	WHERE IDENTIFIER = @Identifier1 AND ACCOUNT_NAME = @Name1

	UPDATE dbo.accountTable
	SET BALANCE = BALANCE + @Amount
	WHERE IDENTIFIER = @Identifier2 AND ACCOUNT_NAME = @Name2

	INSERT INTO dbo.transactionTable (IDENTIFIER_ROOT, IDENTIFIER_AFFECTED, ACCOUNT_ROOT, ACCOUNT_AFFECTED, TYPE, DESCRIPTION, BALANCE)
	VALUES (@Identifier1, @Identifier2, @Name1, @Name2, 'TRANSFERENCIA', @Description, @Amount)
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[bankWithdraw]    Script Date: 10/9/2019 10:41:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[bankWithdraw]
	@Amount DECIMAL,
	@Identifier NVARCHAR(16),
	@Name NVARCHAR(128),
	@Description NVARCHAR(MAX)
AS
	UPDATE dbo.accountTable
	SET BALANCE = BALANCE - @Amount
	WHERE IDENTIFIER = @Identifier AND ACCOUNT_NAME = @Name

	INSERT INTO dbo.transactionTable (IDENTIFIER_ROOT, IDENTIFIER_AFFECTED, ACCOUNT_ROOT, ACCOUNT_AFFECTED, TYPE, DESCRIPTION, BALANCE)
	VALUES (@Identifier, 'N/A', @Name, 'N/A', 'RETIRO', @Description, @Amount)
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[clientRegister]    Script Date: 10/9/2019 10:41:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[clientRegister]
	@Identifier NVARCHAR(16),
	@Name NVARCHAR(128),
	@Last NVARCHAR(128),
	@Password NVARCHAR(256),
	@Pin NVARCHAR(8),
	@Direction NVARCHAR(128),
	@Email NVARCHAR(64)
AS
	INSERT INTO dbo.clientTable (IDENTIFIER, NAME, LAST, PASSWORD, PIN, DIRECTION, EMAIL)
	VALUES (@Identifier, @Name, @Last, @Password, @Pin, @Direction, @Email)
GO
/****** Object:  StoredProcedure [dbo].[clientSubscribe]    Script Date: 10/9/2019 10:41:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[clientSubscribe]
	@Identifier NVARCHAR(16)
AS
	UPDATE dbo.clientTable
	SET STATE = 'SUSCRITO'
	WHERE IDENTIFIER = @Identifier;

	UPDATE dbo.accountTable
	SET ACCOUNT_STATE = 'ACTIVA'
	WHERE IDENTIFIER = @Identifier;
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[clientUnsubscribe]    Script Date: 10/9/2019 10:41:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[clientUnsubscribe]
	@Identifier NVARCHAR(16)
AS
	UPDATE dbo.clientTable
	SET STATE = 'INSUSCRITO'
	WHERE IDENTIFIER = @Identifier;

	UPDATE dbo.accountTable
	SET ACCOUNT_STATE = 'ARCHIVADA'
	WHERE IDENTIFIER = @Identifier;
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[getAccountByIdentification]    Script Date: 10/9/2019 10:41:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[getAccountByIdentification]
(
	@Identifier nvarchar(16)
)
AS
	SET NOCOUNT ON;
SELECT        ACCOUNT_NAME, ACCOUNT_TYPE, ACCOUNT_STATE, BALANCE, OPENDATE
FROM            accountTable
WHERE        (IDENTIFIER = @Identifier)
GO
/****** Object:  StoredProcedure [dbo].[getAccountState]    Script Date: 10/9/2019 10:41:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[getAccountState]
(
	@Identifier nvarchar(16),
	@Account_Name nvarchar(128)
)
AS
	SET NOCOUNT ON;
SELECT        ACCOUNT_STATE
FROM            accountTable
WHERE        (IDENTIFIER = @Identifier) AND (ACCOUNT_NAME = @Account_Name)
GO
/****** Object:  StoredProcedure [dbo].[interbankTransfer]    Script Date: 10/9/2019 10:41:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[interbankTransfer]
	-- INSERTAR AQUI PARAMETROS --
AS
	-- INSERTAR AQUI PROCESO INTERBANCARIO --
	-- INSERT INTO dbo.transactionTable (IDENTIFIER_ROOT, IDENTIFIER_AFFECTED, ACCOUNT_ROOT, ACCOUNT_AFFECTED, TYPE, DESCRIPTION, BALANCE) --
	-- VALUES (@Identifier, 'N/A', @Name, 'N/A', 'INTERBANCARIA', @Description, @Amount) --
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[thirdpartyTransfer]    Script Date: 10/9/2019 10:41:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[thirdpartyTransfer]
	@Identifier NVARCHAR(16),
	@Amount DECIMAL,
	@Name NVARCHAR(128),
	@ThirdParty NVARCHAR(256),
	@Description NVARCHAR(MAX)
AS
	UPDATE dbo.accountTable
	SET BALANCE = Balance - @Amount
	WHERE IDENTIFIER = @Identifier AND ACCOUNT_NAME = @Name

	INSERT INTO dbo.transactionTable (IDENTIFIER_ROOT, IDENTIFIER_AFFECTED, ACCOUNT_ROOT, ACCOUNT_AFFECTED, TYPE, DESCRIPTION, BALANCE)
	VALUES (@Identifier, 'N/A', @Name, @ThirdParty, 'TERCEROS', @Description, @Amount)
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[updateLogin]    Script Date: 10/9/2019 10:41:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[updateLogin]
	@Identifier NVARCHAR(16),
	@Password NVARCHAR(256)
AS
	UPDATE dbo.clientTable
	SET LOGDATE = GETDATE()
	WHERE IDENTIFIER = @Identifier AND PASSWORD = @Password
RETURN 0	
GO
USE [master]
GO
ALTER DATABASE [CoreProyectoDB] SET  READ_WRITE 
GO
