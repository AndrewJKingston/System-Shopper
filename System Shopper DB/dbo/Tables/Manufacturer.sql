CREATE TABLE [dbo].[Manufacturer] (
    [ManufacturerID]   INT           IDENTITY (1, 1) NOT NULL,
    [ManufacturerName] VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_Manufacturer] PRIMARY KEY CLUSTERED ([ManufacturerID] ASC)
);

