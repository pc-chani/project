CREATE TABLE [dbo].[CategoryGMH] (
    [CategoryCode]       INT           IDENTITY (1, 1) NOT NULL,
    [CategoryName]       NVARCHAR (50) NOT NULL,
    [MasterCategoryCode] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([CategoryCode] ASC),
    FOREIGN KEY ([MasterCategoryCode]) REFERENCES [dbo].[CategoryGMH] ([CategoryCode])
);

CREATE TABLE [dbo].[GMH] (
    [GmhCode]               INT            IDENTITY (1, 1) NOT NULL,
    [GmeName]               NVARCHAR (50)  NOT NULL,
    [Adress]                NVARCHAR (50)  NOT NULL,
    [UserCode]              INT            NOT NULL,
    [CategoryCode]          INT            NOT NULL,
    [Picture]               IMAGE          NULL,
    [Amount]                INT            NULL,
    [FreeDescription]       NVARCHAR (100) NULL,
    [IsDisposable]          BIT            NOT NULL,
    [SecurityDepositAmount] INT            NULL,
    [Status]                NVARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([GmhCode] ASC),
    FOREIGN KEY ([UserCode]) REFERENCES [dbo].[USERS] ([UserCode]),
    FOREIGN KEY ([CategoryCode]) REFERENCES [dbo].[CategoryGMH] ([CategoryCode])
);

CREATE TABLE [dbo].[LENDINGS] (
    [LendingCode] INT  IDENTITY (1, 1) NOT NULL,
    [UserCode]    INT  NOT NULL,
    [ProductCode] INT  NOT NULL,
    [Amount]      INT  NULL,
    [LendingDate] DATE NULL,
    [ReturnDate]  DATE NULL,
    PRIMARY KEY CLUSTERED ([LendingCode] ASC),
    FOREIGN KEY ([UserCode]) REFERENCES [dbo].[USERS] ([UserCode]),
    FOREIGN KEY ([ProductCode]) REFERENCES [dbo].[Products] ([ProductCode])
);

CREATE TABLE [dbo].[OPINIONS] (
    [OpinionCode] INT           IDENTITY (1, 1) NOT NULL,
    [LandingCode] INT           NOT NULL,
    [Rating]      INT           NULL,
    [Comment]     NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([OpinionCode] ASC),
    FOREIGN KEY ([LandingCode]) REFERENCES [dbo].[LENDINGS] ([LendingCode])
);

CREATE TABLE [dbo].[Products] (
    [ProductCode]  INT           IDENTITY (1, 1) NOT NULL,
    [Productname]  NVARCHAR (50) NOT NULL,
    [CategoryCode] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([ProductCode] ASC),
    FOREIGN KEY ([CategoryCode]) REFERENCES [dbo].[CategoryGMH] ([CategoryCode])
);
CREATE TABLE [dbo].[RequestForLoan] (
    [RequestCode] INT  IDENTITY (1, 1) NOT NULL,
    [UserCode]    INT  NOT NULL,
    [ProductCode] INT  NOT NULL,
    [RequestDate] DATE NOT NULL,
    [Amount]      INT  NULL,
    PRIMARY KEY CLUSTERED ([RequestCode] ASC),
    FOREIGN KEY ([UserCode]) REFERENCES [dbo].[USERS] ([UserCode]),
    FOREIGN KEY ([ProductCode]) REFERENCES [dbo].[Products] ([ProductCode])
);

CREATE TABLE [dbo].[USERS] (
    [UserCode]   INT           IDENTITY (100, 1) NOT NULL,
    [FirstName]  NVARCHAR (20) NOT NULL,
    [LastName]   NVARCHAR (20) NOT NULL,
    [Adress]     NVARCHAR (50) NOT NULL,
    [Phone]      NCHAR (10)    NOT NULL,
    [Cell-Phone] NCHAR (9)     NOT NULL,
    [E-mail]     NVARCHAR (50) NOT NULL,
    [Password]   NVARCHAR (15) NOT NULL,
    [Permission] NVARCHAR (10) NOT NULL,
    PRIMARY KEY CLUSTERED ([UserCode] ASC)
);