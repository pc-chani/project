CREATE TABLE [dbo].[USERS] (
    [UserCode]   INT           IDENTITY (100, 1) NOT NULL,
    [Name]  NVARCHAR (20) NOT NULL,
    [Adress]     NVARCHAR (50) NOT NULL,
    [Phone]      NCHAR (10)    NOT NULL,
    [Cell-Phone] NCHAR (9)     NOT NULL,
    [E-mail]     NVARCHAR (50) NOT NULL,
    [Password]   NVARCHAR (15) NOT NULL,
    [Permission] NVARCHAR (10) NOT NULL,
    PRIMARY KEY CLUSTERED ([UserCode] ASC)
);

