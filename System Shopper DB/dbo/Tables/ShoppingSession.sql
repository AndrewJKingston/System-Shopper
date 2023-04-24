CREATE TABLE [dbo].[ShoppingSession] (
    [ShoppingSessionID] INT          NOT NULL,
    [UserID]            INT          NULL,
    [TotalPrice]        DECIMAL (18) NULL,
    [CreatedAt]         ROWVERSION   NULL,
    CONSTRAINT [PK_ShoppingSession] PRIMARY KEY CLUSTERED ([ShoppingSessionID] ASC),
    CONSTRAINT [FK_ShoppingSession_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID])
);



