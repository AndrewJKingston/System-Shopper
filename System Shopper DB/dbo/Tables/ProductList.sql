CREATE TABLE [dbo].[ProductList] (
    [ProductListID] INT IDENTITY (1, 1) NOT NULL,
    [SystemListID]  INT NOT NULL,
    [ProductID]     INT NOT NULL,
    [Quantity]      INT NULL,
    CONSTRAINT [PK_ProductList] PRIMARY KEY CLUSTERED ([ProductListID] ASC),
    CONSTRAINT [FK_ProductList_Product] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Product] ([ProductID]),
    CONSTRAINT [FK_ProductList_SystemList] FOREIGN KEY ([SystemListID]) REFERENCES [dbo].[SystemList] ([SystemListID])
);







