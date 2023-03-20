CREATE TABLE [dbo].[OrderItems] (
    [OrderItemsID]   INT      NOT NULL,
    [OrderDetailsID] INT      NOT NULL,
    [ProductID]      INT      NOT NULL,
    [CreatedAt]      DATETIME NOT NULL,
    CONSTRAINT [PK_OrderItems] PRIMARY KEY CLUSTERED ([OrderItemsID] ASC),
    CONSTRAINT [FK_OrderItems_OrderDetails] FOREIGN KEY ([OrderDetailsID]) REFERENCES [dbo].[OrderDetails] ([OrderDetailsID]),
    CONSTRAINT [FK_OrderItems_Product] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Product] ([ProductID])
);

