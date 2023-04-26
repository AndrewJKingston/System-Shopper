CREATE TABLE [dbo].[ShoppingSession] (
    [ShoppingSessionID] INT          IDENTITY (1, 1) NOT NULL,
    [UserID]            INT          NULL,
    [TotalPrice]        DECIMAL (18) NULL,
    [CreatedAt]         ROWVERSION   NULL,
    [SessionID]         INT          NULL,
    CONSTRAINT [PK_ShoppingSession] PRIMARY KEY CLUSTERED ([ShoppingSessionID] ASC),
    CONSTRAINT [FK_ShoppingSession_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID])
);







