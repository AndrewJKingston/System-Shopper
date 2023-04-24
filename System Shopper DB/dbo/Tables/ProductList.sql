CREATE TABLE [dbo].[ProductList] (
    [SystemListID] INT NOT NULL,
    [ProductID]    INT NOT NULL,
    CONSTRAINT [FK_ProductList_Product] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Product] ([ProductID]),
    CONSTRAINT [FK_ProductList_SystemList] FOREIGN KEY ([SystemListID]) REFERENCES [dbo].[SystemList] ([SystemListID])
);

