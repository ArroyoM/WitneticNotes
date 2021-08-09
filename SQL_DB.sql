CREATE DATABASE notes;

USE [notes]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[IdUser] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Email] [varchar](100)  NOT NULL UNIQUE,
	[Password] [varchar](500) NOT NULL,
	[Token] [varchar](500) DEFAULT NULL,
	[Created_time] [datetime] NOT NULL,
	[Updated_time] [datetime] NOT NULL,
	[Deleted_time] [datetime] DEFAULT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
Go


/*Tabla books*/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[IdBook] [int] IDENTITY(1,1) NOT NULL,
	[IdUser] [int] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](500) NOT NULL,
	[Color] [varchar] (20) NOT NULL,
	[Created_time] [datetime] NOT NULL,
	[Updated_time] [datetime] NOT NULL,
	[Deleted_time] [datetime] DEFAULT NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[IdBook] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
Go

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notes](
	[IdNote] [int] IDENTITY(1,1) NOT NULL,
	[IdBook] [int] NOT NULL,
	[Name] [varchar](700) NOT NULL,
	[Created_time] [datetime] NOT NULL,
	[Updated_time] [datetime] NOT NULL,
	[Deleted_time] [datetime] DEFAULT NULL,
 CONSTRAINT [PK_Notes] PRIMARY KEY CLUSTERED 
(
	[IdNote] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
Go

ALTER TABLE [dbo].[Books]  WITH NOCHECK ADD  CONSTRAINT [FK_Books_Users] FOREIGN KEY([IdUser])
REFERENCES [dbo].[Users] ([IdUser])  ON DELETE CASCADE
GO


ALTER TABLE [dbo].[Notes]  WITH NOCHECK ADD  CONSTRAINT [FK_Notes_Books] FOREIGN KEY([IdBook])
REFERENCES [dbo].[Books] ([IdBook])  ON DELETE CASCADE
GO


