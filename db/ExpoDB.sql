CREATE LOGIN expodb WITH PASSWORD = 'Expo123456';
CREATE USER expodb FOR LOGIN expodb;
GO

CREATE DATABASE Expo;
GO

USE Expo;
EXEC sp_addrolemember 'db_owner', 'expodb'
GO

CREATE TABLE ADMIN (
    UserName VARCHAR(50) NOT NULL,
    Password VARCHAR(50) NOT NULL
);

-- For default admin login
DECLARE @USERNAME AS VARCHAR(50)
DECLARE @PASSWORD AS VARCHAR(50)
SET @USERNAME = 'admin'
SET @PASSWORD = 'admin'
INSERT INTO ADMIN (UserName,Password) VALUES (@USERNAME,@PASSWORD)

CREATE TABLE COMPANY (
    CompanyID int IDENTITY(1,1) PRIMARY KEY,
    CompanyName varchar(255) not null,
    Phone varchar(255),
	EMail varchar(255),
	Endorsement decimal(19,4) not null,
	IsEntered bit not null
);

CREATE TABLE PURCHASE (
    PurchaseID int IDENTITY(1,1) PRIMARY KEY,
    PurchaseDate DATETIME not null,
    Amount decimal(19,4) not null,
	SellerID int not null,
	PurchaserID int not null
);
GO