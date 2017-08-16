USE master;
   
CREATE DATABASE DbTTT;
GO

USE [DbTTT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO



CREATE TABLE Users([UserID] int NOT NULL IDENTITY(1,1) PRIMARY KEY,
				   [UserEmail] [nvarchar](255) not null,
				   [UserPass] [nvarchar](255) not null,
				   [WinCount]  int not null DEFAULT 0,
				   [LossCount] int not null DEFAULT 0,
				   [DrawCount] int not null DEFAULT 0); 
GO


CREATE TABLE Games([GameID] int NOT NULL IDENTITY(1,1) PRIMARY KEY,
				   [GameState] [nvarchar](255) not null); 
GO

CREATE TABLE Rooms([RoomID] int NOT NULL IDENTITY(1,1) PRIMARY KEY,
				   [User_1] int null foreign key references [dbo].Users([UserID]),
				   [User_2] int null foreign key references [dbo].Users([UserID]),
				   [GameID] int null foreign key references [dbo].Users([UserID])); 
GO

SET ANSI_PADDING OFF
GO

