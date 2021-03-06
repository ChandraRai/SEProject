USE FlashMinder
GO
BEGIN

DROP TABLE IF EXISTS Flashcard;
DROP TABLE IF EXISTS CardType;
DROP TABLE IF EXISTS Category;
DROP TABLE IF EXISTS Flashcard_Category;
DROP TABLE IF EXISTS USERS;
DROP TABLE IF EXISTS Flashcard_Algorithm_Data;

CREATE TABLE [USERS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Privilege] [smallint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
));
END

-- create card type table, match flashcard to type
BEGIN
CREATE TABLE [dbo].[CardType](
	Id INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(255) NULL,
);
INSERT INTO CardType([Name]) VALUES
	('DEFAULT'),
	('MATCH')
END

-- create flashcard table
BEGIN
CREATE TABLE [dbo].[Flashcard](
	UserId INT,
	Id INT PRIMARY KEY IDENTITY,
	FrontText NVARCHAR (500),
	BackText NVARCHAR (500),
	FrontImage NVARCHAR(500),
	BackImage NVARCHAR(500),
	fk_CardType INT NOT NULL,
	FOREIGN KEY (fk_CardType) REFERENCES CardType(Id), 
	FOREIGN KEY (UserId) REFERENCES USERS(Id),
	CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
	ModifiedDate DATETIME
);
END

-- create category table
BEGIN
CREATE TABLE [dbo].[Category](
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	CategoryName NVARCHAR(50),
	CategoryDesc NVARCHAR(500),
	UserId INT,
	FOREIGN KEY (UserId) REFERENCES USERS(Id),
	CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
	ModifiedDate DATETIME
)
END

-- many 2 many table for flashcard & category
BEGIN
CREATE TABLE [dbo].[Flashcard_Category](
	UserID INT,
	CategoryId INT,
	FlashcardId INT,
	ModifiedDate DATETIME,
	FOREIGN KEY (UserId) REFERENCES USERS(Id),
	FOREIGN KEY (CategoryId) REFERENCES Category(Id),
	FOREIGN KEY (FlashcardId) REFERENCES Flashcard(Id),
	PRIMARY KEY (CategoryId,FlashcardId)
);
END

-- create table to store algorithm data
BEGIN
CREATE TABLE [dbo].[Flashcard_Algorithm_Data](
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	FlashcardId INT,
	Easiness FLOAT NOT NULL DEFAULT 2.5,
	Interval INT NOT NULL DEFAULT 1,
	Quality TINYINT NOT NULL DEFAULT 0,
	NextPratice DATETIME NOT NULL DEFAULT GETDATE(),
	Repetitions INT NOT NULL DEFAULT 0,
	ModifiedDate DATETIME,
	FOREIGN KEY (FlashcardId) REFERENCES Flashcard(Id)
);
END

-- create admin with password '123'
BEGIN
INSERT INTO USERS(username, password, email, privilege) values('admin', '49e38d144e3ec4fc3966f0e1ec5731e54756b556', 'admin', 1)
END