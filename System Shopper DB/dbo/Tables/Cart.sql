CREATE TABLE [dbo].[Cart] (
    [CartID]            INT      IDENTITY (1, 1) NOT NULL,
    [ShoppingSessionID] INT      NOT NULL,
    [ProductID]         INT      NOT NULL,
    [Quantity]          INT      NOT NULL,
    [CreatedAt]         DATETIME NULL,
    CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED ([CartID] ASC),
    CONSTRAINT [FK_Cart_Cart] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Product] ([ProductID]),
    CONSTRAINT [FK_Cart_ShoppingSession] FOREIGN KEY ([ShoppingSessionID]) REFERENCES [dbo].[ShoppingSession] ([ShoppingSessionID])
);





