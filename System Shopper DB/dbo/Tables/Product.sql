CREATE TABLE [dbo].[Product] (
    [ProductID]          INT            IDENTITY (1, 1) NOT NULL,
    [ProductName]        VARCHAR (150)  NOT NULL,
    [ProductDescription] VARCHAR (1000) NOT NULL,
    [ManufacturerID]     INT            NOT NULL,
    [Price]              DECIMAL (18)   NOT NULL,
    [DiscountID]         INT            NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([ProductID] ASC),
    CONSTRAINT [FK_Product_Discount] FOREIGN KEY ([DiscountID]) REFERENCES [dbo].[Discount] ([DiscountID])
);

