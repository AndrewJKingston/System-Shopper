CREATE TABLE [dbo].[Discount] (
    [DiscountID]      INT             IDENTITY (1, 1) NOT NULL,
    [DiscountName]    VARCHAR (25)    NOT NULL,
    [DiscountPercent] DECIMAL (18, 2) NOT NULL,
    [DiscountCreated] ROWVERSION      NOT NULL,
    CONSTRAINT [PK_Discount] PRIMARY KEY CLUSTERED ([DiscountID] ASC)
);



