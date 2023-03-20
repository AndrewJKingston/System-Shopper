CREATE TABLE [dbo].[ProductType] (
    [ProductTypeID] INT          NOT NULL,
    [ProductID]     INT          NOT NULL,
    [ProductType]   VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_ProductType] PRIMARY KEY CLUSTERED ([ProductTypeID] ASC),
    CONSTRAINT [FK_ProductType_Product] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Product] ([ProductID])
);

