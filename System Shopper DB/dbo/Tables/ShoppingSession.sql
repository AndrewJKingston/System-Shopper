CREATE TABLE [dbo].[ShoppingSession] (
    [ShoppingSessionID] INT          NOT NULL,
    [UserID]            INT          NOT NULL,
    [TotalPrice]        DECIMAL (18) NOT NULL,
    [CreatedAt]         ROWVERSION   NOT NULL,
    CONSTRAINT [PK_ShoppingSession] PRIMARY KEY CLUSTERED ([ShoppingSessionID] ASC),
    CONSTRAINT [FK_ShoppingSession_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID])
);

