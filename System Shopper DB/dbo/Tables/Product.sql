CREATE TABLE [dbo].[Product] (
    [ProductID]          INT             IDENTITY (1, 1) NOT NULL,
    [ProductName]        VARCHAR (300)   NOT NULL,
    [ProductDescription] VARCHAR (1000)  NOT NULL,
    [ManufacturerID]     INT             NOT NULL,
    [Price]              DECIMAL (18, 2) NOT NULL,
    [DiscountID]         INT             NULL,
    [ProductImage]       VARCHAR (300)   NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([ProductID] ASC),
    CONSTRAINT [FK_Product_Discount] FOREIGN KEY ([DiscountID]) REFERENCES [dbo].[Discount] ([DiscountID]) ON DELETE SET NULL,
    CONSTRAINT [FK_Product_Manufacturer] FOREIGN KEY ([ManufacturerID]) REFERENCES [dbo].[Manufacturer] ([ManufacturerID]) ON DELETE CASCADE
);













