CREATE LOGIN expodb WITH PASSWORD = 'Expo123456';
CREATE USER expodb FOR LOGIN expodb;
GO

CREATE DATABASE Expo;
GO

USE Expo;
GO
EXEC [Expo].[dbo].[sp_changedbowner] 'expodb', 'true'
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
	IsEntered bit not null,
    IsGuest bit not null
);

CREATE TABLE PURCHASE (
    PurchaseID int IDENTITY(1,1) PRIMARY KEY,
    PurchaseDate DATETIME not null,
    Amount decimal(19,4) not null,
    Product varchar(255) not null,
	SellerID int not null,
	PurchaserID int not null
);

CREATE TABLE ACCOMMODATION (
    AccommodationID int IDENTITY(1,1) PRIMARY KEY,
    CompanyName varchar(255) not null,
    Hotel varchar(255) not null,
	CheckInDate date not null,
	CheckInTime time not null,
    FirstGuest varchar(255) not null,
    SecondGuest varchar(255),
    ThirdGuest varchar(255),
    NumberOfGuests int not null,
    GuestCompanyName varchar(255),
    Phone varchar(255),
    SNG varchar(50),
    DBL varchar(50),
    TRPL varchar(50),
    QUAT varchar(50),
    SNGCHD varchar(50),
    DBLCHD varchar(50),
    TRPLCHD varchar(50),
    CheckOutDate date not null,
	CheckOutTime time not null,
    _SNG varchar(50),
    _DBL varchar(50),
    _TRPL varchar(50),
    _QUAT varchar(50),
    _SNGCHD varchar(50),
    _DBLCHD varchar(50),
    _TRPLCHD varchar(50),
    Description varchar(2000)
);


CREATE TABLE EXTERNALATTENDANCE (
    ExternalAttendanceID int IDENTITY(1,1) PRIMARY KEY,
    NameSurname varchar(255),
    TCID decimal(19,4),
    NumberOfPeople int,
	Phone varchar(255),
	EntranceTime time,
    ExitTime time,
    Occupancy time,
    EntranceDate date,
    Description varchar(255)
);

CREATE TABLE OTELINFORMATION (
	OtelInformationID int,
    SNG varchar(255),
    DBL varchar(255),
    TRPL varchar(255),
    QUAT varchar(255),
	SNGCHD varchar(255),
	DBLCHD varchar(255),
    TRPLCHD varchar(255),
);

INSERT INTO OTELINFORMATION (OtelInformationID,SNG,DBL,TRPL,QUAT,SNGCHD,DBLCHD,TRPLCHD) VALUES (1,'100','200','300','400','500','600','700')
GO