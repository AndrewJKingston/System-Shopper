CREATE TABLE [dbo].[ProductType] (
    [ProductTypeID] INT          IDENTITY (1, 1) NOT NULL,
    [ProductType]   VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_ProductType] PRIMARY KEY CLUSTERED ([ProductTypeID] ASC)
);





