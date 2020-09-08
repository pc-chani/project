CREATE TABLE [dbo].[PRODUCTtoGMH] (
    [ProductCodeToGMH]               INT            IDENTITY (1, 1) NOT NULL,
    [ProductCode]               INT   NOT NULL,
    [GmhCode]                INT  NOT NULL,
    [Picture]               IMAGE          NULL,
    [Amount]                INT            NULL,
    [FreeDescription]       NVARCHAR (100) NULL,
    [IsDisposable]          BIT            NOT NULL,
    [SecurityDepositAmount] INT            NULL,
    [Status]                NVARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([ProductCodeToGMH] ASC),
    FOREIGN KEY ([GmhCode]) REFERENCES [dbo].[GMH] ([GmhCode]),
    FOREIGN KEY ([ProductCode]) REFERENCES [dbo].[Products] ([ProductCode])
);